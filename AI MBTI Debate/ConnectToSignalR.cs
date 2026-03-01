using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_MBTI_Debate
{
    public static class ConnectToSignalR
    {
        public static async Task Listen(string debateId)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7139/hubs/debate")
                //.ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Trace))
                .Build();
            connection.On<object>("ReceiveChunk", payload =>
            {
                if (payload is System.Text.Json.JsonElement elem && elem.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    string debateIdFromMsg = elem.TryGetProperty("debateId", out var d) ? d.GetString() ?? "" : "";
                    string name = elem.TryGetProperty("name", out var n) ? n.GetString() ?? "" : "";
                    string type = elem.TryGetProperty("type", out var t) ? t.GetString() ?? "" : "";
                    string content = elem.TryGetProperty("content", out var c) ? c.GetString() ?? "" : "";
                    bool changeSpeaker = elem.TryGetProperty("changeSpeaker", out var cs) && cs.ValueKind == System.Text.Json.JsonValueKind.True;
                    bool summary = elem.TryGetProperty("summary", out var s) && s.ValueKind == System.Text.Json.JsonValueKind.True;

                    if (summary && changeSpeaker)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                    }else if (changeSpeaker)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write($"{name} ({type}): ");
                    }
                    Console.Write(content);
                    Console.Out.Flush();
                }
            });

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("StartDebate", debateId);

                await Task.Delay(Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }
            finally
            {
                await connection.DisposeAsync();
            }
        }
    }
}
