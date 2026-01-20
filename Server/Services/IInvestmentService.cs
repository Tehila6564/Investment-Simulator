using Server.Dtos;
using Server.Models;

namespace Server.Services;

public interface IInvestmentService
{
    Task<List<InvestmentOptionDto>> GetAvailableOptions();
    Task<UserStateDto> GetUserState(string username);
    Task<(bool Success, string? Error)> TryInvest(string username, string optionId);
    Task ProcessExpiredInvestmentsAsync();
}