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

        public DebateController(IHubContext<DebateHub> hubContext, Random rand)
        {
            _hubContext = hubContext;
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

            AIDebateHandler _handler = new AIDebateHandler(BASE_AI.MODEL);
            _handler.setTopic(request.Topic);

            await _hubContext.Clients.Group(debateId).SendAsync("DebateStarted", new
            {
                debateId,
                topic = request.Topic
            });

            _ = debate(debateId, request, _handler);

            return Ok(new { debateId });
        }

        private async Task debate(string debateId, StartDebateDTO info, AIDebateHandler _handler)
        {
            Task.Delay(7000).Wait(); // wait for clients to connect to the hub
            Dictionary<Persona, int> spoken = info.People.people.Select(p => PersonaDTO.CreatePersona(p)).ToDictionary(p => p, p => 0);

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
