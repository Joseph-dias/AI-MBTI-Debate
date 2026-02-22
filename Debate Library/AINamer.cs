using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debate_Library
{
    public class AINamer : BASE_AI
    {
        private static AINamer? NAME_OBJ = null;
        private AINamer(string model) : base(model)
        {
        }

        public static AINamer getInstance(string model)
        {
            if (NAME_OBJ != null && model == NAME_OBJ.model) return NAME_OBJ;

            NAME_OBJ = new AINamer(model);

            return NAME_OBJ;
        }

        public async Task<string?> getName(Persona person)
        {
            string systemMessage = "You are tasked with giving a name to a character.  You will be given the characters traits and personality and your job is to respond with the name and the name only.  No other data.  First name only.";
            messages.Clear();
            messages.Add(new SystemChatMessage(systemMessage));
            messages.Add(new UserChatMessage($"The person has the personality of {person.personality.ToString()}.  This person has the following personality traits {string.Join(",", person.traits.Select(t => t.ToString()))}.  This person has the following job: {person.job.ToString()}.  And this person has the following experiences in life: {person.experiences}"));
            var response = await sendRequestAsync();

            return response?.Content[0]?.Text;
        }
    }
}
