// See https://aka.ms/new-console-template for more information

using AI_MBTI_Debate;
using AIDebateAPI.DTO;
using Debate_Library;
using System.Text;
using System.Text.Json;
using static System.Random;

// Temporary key - matches the placeholder in the API's appsettings.json for local dev.
// When the API is set up with a real key via environment variable, update this to match.
const string ApiKey = "CHANGE_ME";

HttpClient client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7139/")
};
// Stamp every outgoing request with the key so we don't have to add it per-call
client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);

JsonSerializerOptions options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

//Generating the people

var response = await client.PostAsync($"People/Generate/5", null);

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine($"Generate failed: {response.StatusCode}");
    throw new Exception($"Failed to generate people: {response.StatusCode}");
}

var json = await response.Content.ReadAsStringAsync();
PeopleDTO? results = JsonSerializer.Deserialize<PeopleDTO>(json, options);

if (results == null || results.people == null || results.people.Count == 0)
{
    Console.WriteLine("No people generated.");
    throw new Exception("No people generated.");
}else
{
    Console.WriteLine("Generated People:");
    foreach (var person in results.people)
    {
        Console.WriteLine($"Name: {person.Name}, Personality: {person.personality}");
    }
}

//Starting the debate

Console.Write("Enter a debate topic: ");

var request = new StartDebateDTO
{
    Topic = Console.ReadLine(),
    People = results
};

var jsonDebate = JsonSerializer.Serialize(request);
var content = new StringContent(jsonDebate, Encoding.UTF8, "application/json");

var responseDebate = await client.PostAsync("Debate/start", content);

if (!responseDebate.IsSuccessStatusCode)
{
    Console.WriteLine($"Start debate failed: {responseDebate.StatusCode}");
    var error = await responseDebate.Content.ReadAsStringAsync();
    Console.WriteLine(error);
    return;
}

var resultJson = await responseDebate.Content.ReadAsStringAsync();
var result = JsonSerializer.Deserialize<Dictionary<string, string>>(resultJson);

string? debateId = null;

if (result?.TryGetValue("debateId", out debateId) == true)
{
    Console.WriteLine($"Debate started! Debate ID = {debateId}");
}

await ConnectToSignalR.Listen(debateId ?? string.Empty, ApiKey);
