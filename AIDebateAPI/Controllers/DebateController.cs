using AIDebateAPI.DTO;
using AIDebateAPI.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AIDebateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebateController : ControllerBase
    {
        private readonly IHubContext<DebateHub> _hubContext;

        public DebateController(IHubContext<DebateHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartDebate([FromBody] StartDebateDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Topic))
                return BadRequest("Topic is required");

            string debateId = Guid.NewGuid().ToString("N")[..16];

            await _hubContext.Clients.Group(debateId).SendAsync("DebateStarted", new
            {
                debateId,
                topic = request.Topic
            });

            return Ok(new { debateId });
        }
    }
}
