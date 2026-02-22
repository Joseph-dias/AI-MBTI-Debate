using OpenAI;
using OpenAI.Assistants;
using OpenAI.Chat;
using OpenAI.Responses;
using System.ClientModel;
using static Debate_Library.AI_Tools;

namespace Debate_Library
{
    public abstract class BASE_AI
    {
        private string? apiKey;
        protected OpenAIClient client;
        protected string model;

        //Chat message list
        protected List<ChatMessage> messages { get; set; }
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
            messages = new List<ChatMessage>(); //Initialize message list
            messages.Add(new SystemChatMessage(""));
        }

        /// <summary>
        /// Sends the messages and returns the response in one chunk
        /// </summary>
        /// <returns></returns>
        protected async Task<ChatCompletion> sendRequestAsync()
        {
            return await client.GetChatClient(model).CompleteChatAsync(messages);
        }

        /// <summary>
        /// Sends the messages to the API and streams back the response
        /// </summary>
        /// <returns></returns>
        protected async IAsyncEnumerable<StreamingChatCompletionUpdate> sendStreamingRequestAsync()
        {
            var options = new ChatCompletionOptions()
            {
                MaxOutputTokenCount = 100
            };

            options.Tools.Add(AI_Tools.WebSearchTool);
            options.ToolChoice = ChatToolChoice.CreateAutoChoice();

            await foreach (StreamingChatCompletionUpdate update in client.GetChatClient(model).CompleteChatStreamingAsync(messages, options))
            {
                yield return update;
            }
        }
    }
}
