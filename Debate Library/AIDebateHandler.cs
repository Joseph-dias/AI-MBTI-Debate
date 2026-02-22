using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Debate_Library.Personality;

namespace Debate_Library
{
    public class AIDebateHandler : BASE_AI
    {
        public AIDebateHandler(string model) : base(model) 
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

        public async IAsyncEnumerable<char> getDebateResponse(Persona person)
        {
            messages[0] = new SystemChatMessage(writeDebaterSystemMessage(person));

            bool repeat;

            do
            {
                repeat = false;
                StringBuilder sb = new StringBuilder(person.Name + " (" + person.personality + "): ");
                StringBuilder toolcallStr = new StringBuilder(); //Building a tool call string if the tool call needs it
                var toolCallIndex = 0;
                List<ChatToolCall> toolCalls = new List<ChatToolCall>(); //Creating list of tool calls to send to the API if necessary
                var assistantMsg = new AssistantChatMessage("This is a placeholder");
                await foreach (StreamingChatCompletionUpdate update in sendStreamingRequestWithWebSearchAsync())
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
                        if (update.FinishReason.Value == ChatFinishReason.ToolCalls && toolCalls.Count > 0) //Tool call finish
                        {
                            assistantMsg = new AssistantChatMessage(toolCalls);
                            if(toolcallStr.Length > 0) //Add context text if it exists
                            {
                                assistantMsg.Content.Add(ChatMessageContentPart.CreateTextPart(toolcallStr.ToString()));
                            }

                            messages.Add(assistantMsg); //Add tool call assistant message

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
                            if (sb.Length > (person.Name + " (" + person.personality + "): ").Count())
                            {
                                messages.Add(new UserChatMessage(sb.ToString())); //Adding the full "user" chat message to the list
                            }
                            yield break;
                        }
                    }
                }
            } while (repeat);
        }

        public async IAsyncEnumerable<char> getSummaryResponse()
        {
            messages[0] = new SystemChatMessage(writeSummarySystemMessage());
            await foreach (StreamingChatCompletionUpdate update in sendStreamingRequestWithoutToolsAsync())
            {
                foreach (ChatMessageContentPart part in update.ContentUpdate)
                {
                    if (part.Kind == ChatMessageContentPartKind.Text && !string.IsNullOrEmpty(part.Text))
                    {
                        foreach (char c in part.Text)
                        {
                            yield return c;
                        }
                    }
                }

                if (update.FinishReason.HasValue)
                {
                    yield break;
                }
            }
        }

        //Private methods
        private string writeDebaterSystemMessage(Persona person)
        {
            return $"You are a {person.personality} personality type named {person.Name}.  A description of you is this: {MbtiDescriptions[person.personality]}. Your career is this: {person.job}.  Here is a list of your other personality traits: {string.Join(",", person.traits.Select(t => t.ToString()))}.  Here's a description your past experience: {person.experiences}.  Use all of that to inform your response without explicitly saying any of it.  You are in a debate with other people.  You may call them out by name and respond to points.  Each user message response from another person should start with their name, their personality type in parentheses, and a colon.  You should not start a response with your name, your personality type in parentheses, and a colon.  If you've been called out by name (your name is {person.Name}), you should prioritize responding to them unless you already have, and you should respond to other people by calling them out in the third person.  You don't need to call out every other person that has spoken and you do not have to respond to every point.  Pick the most important points.  Only call out the other people if it's important and necessary and you're directly responding to what they said and do it in the third person.  Your job right now is to respond to the user messages based on your individual characteristics (without saying explicitly that's what you're doing).  Prioritize the last user message for your response unless you've been called out and keep in mind the user message labeled as the debate topic.  Do not go off topic.  Keep response to 100 tokens or less.  When calling out people or when being called out, only refer to names.  Not the type in parentheses after the name.";
        }

        private string writeSummarySystemMessage()
        {
            return $"You are the moderator of this debate.  You have no opinion on the topic nor commentary.  Your job is to summarize what has been discussed by the various people.  Take each user response that starts with their name, they personality, and a colon.  Then take the debate topic.  Decide the main two or three opinions.  Summarize those opinions and list the people who agree with that opinion (first name only, not their type), include one sentence on what each person's main point was.  Use a maximum of 500 tokens.  But fewer tokens is preferrable.";
        }
    }
}
