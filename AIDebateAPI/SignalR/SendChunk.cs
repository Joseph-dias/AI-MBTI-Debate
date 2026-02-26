using Microsoft.AspNetCore.SignalR;

namespace AIDebateAPI.SignalR
{
    public static class SendChunk
    {
        public static async Task Send<T>(IHubContext<T> _hubContext, string debateId, string text) where T : Hub
        {
            await _hubContext.Clients.Group(debateId).SendAsync("ReceiveChunk", new
            {
                debateId,
                content = text
            });
        }
    }
}
