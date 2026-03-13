using AIDebateAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Debate_Library;

namespace AIDebateAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonFactory _personFactory;
        public PeopleController(IPersonFactory personFactory)
        {
            _personFactory = personFactory ?? throw new ArgumentNullException(nameof(personFactory));
        }

        [HttpPost("Generate/{num}")]
        public async Task<IActionResult> Generate(int num)
        {
            if (num < 1)
                return BadRequest("Must generate at least 1 person.");
            if (num > 10) 
                return BadRequest("Maximum 10 people per request.");

            PeopleDTO PeopleDTO = new PeopleDTO(); //Creating the list of people

            try
            {
                List<Persona> people = await _personFactory.CreatePeople(BASE_AI.MODEL, num) ?? new();
                PeopleDTO.people = people != null ? (await Task.WhenAll(people.Select(p => PersonaDTO.CreateDTO(p)))).ToList() : new(); //Attempting to create people
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return Problem(
                    detail: "Failed to generate the people. Please try again shortly.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Generation Failed"
                );
            }

            return Ok(PeopleDTO);
        }
    }
}
