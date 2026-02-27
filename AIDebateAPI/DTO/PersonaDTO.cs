using Debate_Library;
using static Debate_Library.Personality;

namespace AIDebateAPI.DTO
{
    public class PersonaDTO
    {
        public string? Name { get; set; }
        public string personality { get; set; } = string.Empty;
        public List<string> traits { get; set; } = new();
        public string job { get; set; } = string.Empty;
        public string experiences { get; set; } = string.Empty;

        public static async Task<Persona> CreatePersona(PersonaDTO persona)
        {
            return await Persona.CreatePerson(BASE_AI.MODEL, (MbtiType)Enum.Parse(typeof(MbtiType), persona.personality), persona.traits.Select(t => (Trait)Enum.Parse(typeof(Trait), t)).ToList(), (Vocation)Enum.Parse(typeof(Vocation), persona.job), persona.experiences);
        }

        public static async Task<PersonaDTO> CreateDTO(Persona persona)
        {
            return await Task.Run(() => new PersonaDTO() { Name = persona.Name, personality = persona.personality.ToString(), traits = persona.traits.Select(t => t.ToString()).ToList(), job = persona.job.ToString(), experiences = persona.experiences });
        }
    }
}
