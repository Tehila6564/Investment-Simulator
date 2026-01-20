using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Dtos;
using Server.Hubs; 
using Server.Models;

namespace Server.Services;

public class InvestmentService : IInvestmentService
{
    private readonly AppDbContext _context;
    private readonly IHubContext<InvestmentHub> _hubContext; 

    public InvestmentService(AppDbContext context, IHubContext<InvestmentHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task<List<InvestmentOptionDto>> GetAvailableOptions()
    {
        return await _context.InvestmentOptions
            .Select(o => new InvestmentOptionDto(o.Id, o.Name, o.RequiredAmount, o.ExpectedReturn, o.DurationSeconds))
            .ToListAsync();
    }

    public async Task<UserStateDto> GetUserState(string username)
    {
        var key = username.Trim().ToLowerInvariant();
        var user = await _context.Users
            .Include(u => u.ActiveInvestments)
            .FirstOrDefaultAsync(u => u.Username == key);

        if (user == null)
        {
            user = new User { Username = key, Balance = 5000m };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        return MapToStateDto(user);
    }

    public async Task<(bool Success, string? Error)> TryInvest(string username, string optionId)
    {
        var key = username.Trim().ToLowerInvariant();

        var option = await _context.InvestmentOptions.FindAsync(optionId);
        var user = await _context.Users.Include(u => u.ActiveInvestments).FirstOrDefaultAsync(u => u.Username == key);

        if (option == null) return (false, "Investment option not found.");
        if (user == null) return (false, "User not found.");

        if (user.Balance < option.RequiredAmount) return (false, "Insufficient balance.");
        if (user.ActiveInvestments.Any(i => i.Name == option.Name)) return (false, "You already have an active investment of this type.");

        var newInvestment = new DbActiveInvestment
        {
            Name = option.Name,
            InvestedAmount = option.RequiredAmount,
            ExpectedReturn = option.ExpectedReturn,
            EndsAt = DateTime.UtcNow.AddSeconds(option.DurationSeconds),
            Username = key
        };

        user.Balance -= option.RequiredAmount;
        user.LastBalanceUpdate = DateTime.UtcNow;
        _context.ActiveInvestments.Add(newInvestment);

        await _context.SaveChangesAsync();

       
        await SendUpdateToUser(key);

        return (true, null);
    }

    public async Task ProcessExpiredInvestmentsAsync()
    {
        var now = DateTime.UtcNow;

        var expiredInvestments = await _context.ActiveInvestments
            .Where(i => i.EndsAt <= now)
            .ToListAsync();

        if (!expiredInvestments.Any()) return;

   
        var usernamesToUpdate = expiredInvestments.Select(i => i.Username).Distinct().ToList();

        foreach (var inv in expiredInvestments)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == inv.Username);
            if (user != null)
            {
                user.Balance += inv.ExpectedReturn;
                user.LastBalanceUpdate = DateTime.UtcNow;
                _context.ActiveInvestments.Remove(inv);
            }
        }

        await _context.SaveChangesAsync();

      
        foreach (var userKey in usernamesToUpdate)
        {
            await SendUpdateToUser(userKey);
        }
    }

    private async Task SendUpdateToUser(string username)
    {
        var user = await _context.Users
            .Include(u => u.ActiveInvestments)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user != null)
        {
            var state = MapToStateDto(user);
           
            await _hubContext.Clients.Group(username).SendAsync("state-updated", state);
        }
    }

   
    private UserStateDto MapToStateDto(User user)
    {
        var now = DateTime.UtcNow;
        var activeDtos = user.ActiveInvestments.Select(i => new ActiveInvestmentDto(
            Guid.Parse(i.Id),
            i.Name,
            i.InvestedAmount,
            i.ExpectedReturn,
            (int)Math.Max(0, (i.EndsAt - now).TotalSeconds)
        )).ToList();

        return new UserStateDto(user.Balance, activeDtos, user.LastBalanceUpdate);
    }
}