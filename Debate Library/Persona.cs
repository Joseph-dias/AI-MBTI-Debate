using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Debate_Library.Personality;

namespace Debate_Library
{
    public class Persona
    {
        public string? Name { get; private set; }
        public MbtiType personality { get; set; }
        public List<Trait> traits { get; private set; }
        public Vocation job { get; set; }
        public string experiences { get; set; }

        private Persona(MbtiType personality, List<Trait> traits, Vocation job, string experiences)
        {
            this.personality = personality;
            this.traits = traits;
            this.job = job;
            this.experiences = experiences;
        }

        public async static Task<Persona> CreatePerson(string AIModel, MbtiType personality, List<Trait> traits, Vocation job, string experiences)
        {
            Persona persona = new Persona(personality, traits, job, experiences);

            persona.Name = await AINamer.getInstance(AIModel).getName(persona) ?? throw new IOException("Couldn't get a name");

            return persona;
        }
    }
}
