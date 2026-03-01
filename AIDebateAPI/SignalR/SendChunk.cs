using Microsoft.AspNetCore.SignalR;

namespace AIDebateAPI.SignalR
{
    public static class SendChunk
    {
        public static async Task Send<T>(IHubContext<T> _hubContext, string debateId, string name, string type, string text, bool changeSpeaker, bool summary) where T : Hub
        {
            await _hubContext.Clients.Group(debateId).SendAsync("ReceiveChunk", new
            {
                debateId,
                name,
                type,
                content = text,
                ChangeSpeaker = changeSpeaker,
                summary
            });
        }
    }
}
