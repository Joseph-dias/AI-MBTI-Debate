using AIDebateAPI.DTO;
using AIDebateAPI.SignalR;
using Debate_Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static AIDebateAPI.SignalR.SendChunk;

namespace AIDebateAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DebateController : ControllerBase
    {
        private readonly IHubContext<DebateHub> _hubContext;
        private readonly Random _random;
        private readonly AIDebateHandler _handler;

        public DebateController(IHubContext<DebateHub> hubContext, AIDebateHandler handler, Random rand)
        {
            _hubContext = hubContext;
            _handler = handler;
            _random = rand;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartDebate([FromBody] StartDebateDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Topic))
                return BadRequest("Topic is required");
            if ((request.People?.people) == null || request.People.people.Count == 0)
                return BadRequest("People are required.  Please call 'People/Generate' first.");

            string debateId = Guid.NewGuid().ToString("N")[..16];

            await _hubContext.Clients.Group(debateId).SendAsync("DebateStarted", new
            {
                debateId,
                topic = request.Topic
            });

            _ = debate(debateId, request);

            return Ok(new { debateId });
        }

        private async Task debate(string debateId, StartDebateDTO info)
        {

            Dictionary<Persona, int> spoken = (await Task.WhenAll((info.People.people).Select(p => PersonaDTO.CreatePersona(p)))).ToDictionary(p => p, p => 0);

            List<Persona> finishedDebaters = new List<Persona>();

            while (finishedDebaters.Count() < info.People.people.Count())
            {
                Persona person = spoken.Keys.ToList()[_random.Next(spoken.Count)];
                bool nextSpeaker = true;
                await foreach (char c in _handler.getDebateResponse(person))
                {
                    await Send(_hubContext, debateId, person.Name ?? string.Empty, person.personality.ToString(), c.ToString(), nextSpeaker);
                    if(nextSpeaker)
                        nextSpeaker = false;
                }
                spoken[person]++;
                if (spoken[person] > 2)
                {
                    spoken.Remove(person);
                    finishedDebaters.Add(person);
                }
            }
        }
    }
}
