using Microsoft.AspNetCore.SignalR;

namespace AIDebateAPI.SignalR
{

    public class DebateHub : Hub
    {
        public async Task StartDebate(string debateId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, debateId);
        }

        public async Task StopDebate(string debateId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, debateId);
            await Clients.Group(debateId).SendAsync("DebateStarted", debateId);
        }
    }
}
