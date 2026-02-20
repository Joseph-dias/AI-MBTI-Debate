using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace Debate_Library
{
    public abstract class BASE_AI
    {
        private string? apiKey;
        protected OpenAIClient client;
        protected string model;

        //Chat message list
        protected abstract List<ChatMessage> messages { get; set; }
        public BASE_AI(string model) 
        {
            apiKey = Environment.GetEnvironmentVariable("XAI_API_KEY") ?? throw new InvalidOperationException("Set XAI_API_KEY environment variable");
            client = new OpenAIClient( //Creating the client
            new ApiKeyCredential(apiKey),
            new OpenAIClientOptions
            {
                Endpoint = new Uri("https://api.x.ai/v1")
            });

            this.model = model; //Set the model
        }

        protected async IAsyncEnumerable<StreamingChatCompletionUpdate> sendRequestAsync()
        {
            await foreach (StreamingChatCompletionUpdate update in client.GetChatClient(model).CompleteChatStreamingAsync(messages))
            {
                yield return update;
            }
        }
    }
}
