using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debate_Library
{
    public interface IPersonFactory
    {
        Task<Persona> CreatePerson(string model);
        Task<List<Persona>> CreatePeople(string model, int num);
    }
}
