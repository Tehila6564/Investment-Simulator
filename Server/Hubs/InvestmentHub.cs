using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class InvestmentHub : Hub
    {
        public async Task JoinGroup(string username)
        {
            var group = username?.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(group)) return;

            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            
        }
    }
}