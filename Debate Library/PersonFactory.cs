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

        /// <summary>
        /// Runs all the create person tasks in parallel and returns a list of the created personas.
        /// </summary>
        /// <param name="model">AI Model</param>
        /// <param name="num">Number of people to create</param>
        /// <returns></returns>
        public async Task<List<Persona>> CreatePeople(string model, int num)
        {
            var tasks = Enumerable.Range(0, num).Select(_ => CreatePerson(model));

            return (await Task.WhenAll(tasks)).ToList();
        }
    }
}
