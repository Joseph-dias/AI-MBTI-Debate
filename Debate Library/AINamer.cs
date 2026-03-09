using OpenAI.Chat;
using System.Linq;
using System.Threading.Tasks;

namespace Debate_Library
{
    public class AINamer : BASE_AI
    {
        public AINamer(string model) : base(model)
        {
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

        public async Task<string?> getName()
        {
            string systemMessage = "You are tasked with giving a name to a character.  Your job is to respond with a name and a name only.  No other data.  First name only.";
            messages.Clear();
            messages.Add(new SystemChatMessage(systemMessage));
            var response = await sendRequestAsync();
            return response?.Content[0]?.Text;
        }
    }
}