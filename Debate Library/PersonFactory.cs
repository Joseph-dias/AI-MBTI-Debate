using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Random;

namespace Debate_Library
{
    public class PersonFactory : IPersonFactory
    {
        public async Task<Persona> CreatePerson(string model)
        {
            Random rnd = new Random();
            return await Persona.CreatePerson(model, rnd.ChooseFromAllMBTI(), rnd.ChooseTraits(), rnd.ChooseVocation(), rnd.ChooseExperiences());
        }

        public async Task<List<Persona>> CreatePeople(string model, int num)
        {
            List<Persona> toReturn = new List<Persona>();
            for (int i = 0; i < num; i++)
            {
                toReturn.Add(await CreatePerson(model));
            }

            return toReturn;
        }
    }
}
