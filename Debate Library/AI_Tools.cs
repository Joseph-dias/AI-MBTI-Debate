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
            functionParameters: BinaryData.FromBytes(JsonSerializer.SerializeToUtf8Bytes(new
            {
                type = "object",
                properties = new
                {
                    allowed_domains = new
                    {
                        type = "array",
                        items = new { type = "string" },
                        maxItems = 5,
                        description = "Only search within specific domains (max 5)"
                    },
                    excluded_domains = new
                    {
                        type = "array",
                        items = new { type = "string" },
                        maxItems = 5,
                        description = "Exclude specific domains from search (max 5)"
                    },
                    enable_image_understanding = new
                    {
                        type = "boolean",
                        description = "Enable analysis of images found during browsing"
                    }
                },
                additionalProperties = false
            }))
        );
    }
}
