using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Debate_Library
{
    public static class AI_Tools
    {
        public static readonly ChatTool WebSearchTool = ChatTool.CreateFunctionTool(
            functionName: "web_search",
            functionDescription: "Search the web in real-time and browse web pages to find information.",
            functionParameters: BinaryData.FromString("{\"type\":\"object\",\"properties\":{},\"required\":[]}")
        );
    }
}
