using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Debate_Library.MBTI;

namespace Debate_Library
{
    public class AI_Handler : BASE_AI
    {
        public AI_Handler(string model) : base(model) 
        {
        }

        /// <summary>
        /// Set the topic of discussion.  Also clears message history.
        /// </summary>
        /// <param name="topic"></param>
        public void setTopic(string topic)
        {
            messages.Clear();
            messages.Add(new SystemChatMessage("")); //Placeholder for system message
            messages.Add(new UserChatMessage("Debate Topic: " + topic)); //Setting the topic
        }

        public async IAsyncEnumerable<char> getResponse(MbtiType type)
        {
            messages[0] = new SystemChatMessage(writeSystemMessage(type));

            StringBuilder sb = new StringBuilder();
            await foreach (StreamingChatCompletionUpdate update in sendRequestAsync())
            {
                foreach (ChatMessageContentPart part in update.ContentUpdate)
                {
                    if (part.Kind == ChatMessageContentPartKind.Text && !string.IsNullOrEmpty(part.Text))
                    {
                        foreach (char c in part.Text)
                        {
                            sb.Append(c);
                            yield return c;
                        }
                    }
                }

                var toolCallIndex = 0;
                List<ChatToolCall> toolCalls = new List<ChatToolCall>();
                var assistantMsg = new AssistantChatMessage("This is a placeholder");

                foreach (var toolUpdate in update.ToolCallUpdates)
                {
                    toolCallIndex = toolUpdate.Index;
                    if (toolCalls.Count <= toolCallIndex)
                    {
                        toolCalls.Add(ChatToolCall.CreateFunctionToolCall(toolUpdate.ToolCallId, toolUpdate.FunctionName, toolUpdate.FunctionArgumentsUpdate));
                    }
                }

                if (update.FinishReason.HasValue)
                {
                    if (update.FinishReason.Value == ChatFinishReason.ToolCalls && toolCalls.Count > 0)
                    {
                        assistantMsg = new AssistantChatMessage(toolCalls);
                        messages.Add(assistantMsg);

                        // Add placeholder tool responses (empty for xAI built-in tools)
                        foreach (var toolCall in assistantMsg.ToolCalls)
                        {
                            messages.Add(new ToolChatMessage(toolCall.Id, "{}"));
                        }

                        // Break to start new request
                        break;
                    }
                    else
                    {
                        // Normal finish — done
                        if (sb.Length > 0)
                        {
                            messages.Add(new UserChatMessage(sb.ToString()));
                        }
                        yield break;
                    }
                }
            }
        }

        //Private methods
        private string writeSystemMessage(MbtiType type)
        {
            return "You are a " + type.ToString() + " personality type.  A description of you is this: " + MbtiDescriptions[type] + "  You are in a debate with other personality types.  You may call them out by name and respond to points.  You don't need to call out every other type that has spoken and you do not have to respond to every point.  Pick the most important points.  Only call out the other types if it's important and do it in the third person.  Your job right now is to respond to the messages as your type (without saying explicitly what your type is).  Prioritize the last message for your response and keep in mind the message labeled as the debate topic.  Do not go off topic.  Keep response to 100 tokens or less.";
        }
    }
}
