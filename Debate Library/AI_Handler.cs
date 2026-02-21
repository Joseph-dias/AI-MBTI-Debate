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

            bool repeat;

            do
            {
                repeat = false;
                StringBuilder sb = new StringBuilder(type.ToString() + ": ");
                StringBuilder toolcallStr = new StringBuilder();
                var toolCallIndex = 0;
                List<ChatToolCall> toolCalls = new List<ChatToolCall>();
                var assistantMsg = new AssistantChatMessage("This is a placeholder");
                await foreach (StreamingChatCompletionUpdate update in sendRequestAsync())
                {
                    foreach (ChatMessageContentPart part in update.ContentUpdate)
                    {
                        if (part.Kind == ChatMessageContentPartKind.Text && !string.IsNullOrEmpty(part.Text) && toolCalls.Count == 0)
                        {
                            foreach (char c in part.Text)
                            {
                                sb.Append(c);
                                yield return c;
                            }
                        }else if(toolCalls.Count > 0)
                        {
                            toolcallStr.Append(part.Text);
                        }
                    }

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
                            if(toolcallStr.Length > 0) //Add context text if it exists
                            {
                                assistantMsg.Content.Add(ChatMessageContentPart.CreateTextPart(toolcallStr.ToString()));
                            }

                            messages.Add(assistantMsg);

                            // Add placeholder tool responses
                            foreach (var toolCall in toolCalls)
                            {
                                messages.Add(new ToolChatMessage(toolCall.Id, "Search performed."));
                            }

                            toolCalls.Clear();
                            toolCallIndex = 0;
                            toolcallStr.Clear();

                            // Break to start new request
                            repeat = true;
                            break;
                        }
                        else
                        {
                            // Normal finish — done
                            if (sb.Length > (type.ToString() + ": ").Count())
                            {
                                messages.Add(new UserChatMessage(sb.ToString()));
                            }
                            yield break;
                        }
                    }
                }
            } while (repeat);
        }

        //Private methods
        private string writeSystemMessage(MbtiType type)
        {
            return "You are a " + type.ToString() + " personality type.  A description of you is this: " + MbtiDescriptions[type] + "  You are in a debate with other personality types.  You may call them out by name and respond to points.  Each user message response from another type should start with their type and a colon.  You don't need to call out every other type that has spoken and you do not have to respond to every point.  Pick the most important points.  Only call out the other types if it's important and do it in the third person.  Your job right now is to respond to the user messages as your type (without saying explicitly what your type is).  Prioritize the last user message for your response and keep in mind the user message labeled as the debate topic.  Do not go off topic.  Keep response to 100 tokens or less.";
        }
    }
}
