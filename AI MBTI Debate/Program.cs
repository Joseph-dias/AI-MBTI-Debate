// See https://aka.ms/new-console-template for more information

using Debate_Library;
using static System.Random;

string model = "grok-4-fast-reasoning";
Random rand = new Random();

AIDebateHandler handler = new AIDebateHandler(model);

Console.Write("Loading... (Please Wait)");

IPersonFactory personFactory = new PersonFactory();
List<Persona> people = await personFactory.CreatePeople(model, 5);
Dictionary<Persona, int> spoken = people.ToDictionary(p => p, p => 0);

List<Persona> finishedDebaters = new List<Persona>();

Console.Clear();



Console.Write("What's the debate topic?: ");

handler.setTopic(Console.ReadLine());

List<Personality.MbtiType> spokenTypes = new List<Personality.MbtiType>();

while(people.Count > 0)
{
    Persona person = people[rand.Next(people.Count)];
    Console.Write(person.Name + " (" + person.personality + "): ");
    await foreach (char c in handler.getDebateResponse(person))
    {
        Console.Write(c);
    }
    spoken[person]++;
    if (spoken[person] > 2)
    {
        spoken.Remove(person);
        people.Remove(person);
        finishedDebaters.Add(person);
    }
    Console.WriteLine();
    Console.WriteLine();
}

//Summarize the debate
await foreach (char c in handler.getSummaryResponse())
{
    Console.Write(c);
}

Console.ReadLine();
