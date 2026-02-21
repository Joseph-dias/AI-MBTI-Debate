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
        public MbtiType personality { get; set; }
        public List<Trait> traits { get; private set; }
        public Vocation job { get; set; }

        public Persona(MbtiType personality, List<Trait> traits, Vocation job)
        {
            this.personality = personality;
            this.traits = traits;
            this.job = job;
        }
    }
}
