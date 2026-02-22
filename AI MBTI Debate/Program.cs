// See https://aka.ms/new-console-template for more information

using Debate_Library;

string model = "grok-4-fast-reasoning";

AIDebateHandler handler = new AIDebateHandler(model);

IPersonFactory personFactory = new PersonFactory();

Console.Write("What's the debate topic?: ");

handler.setTopic(Console.ReadLine());

List<Personality.MbtiType> spokenTypes = new List<Personality.MbtiType>();

for (int x = 0; x < 5; x++)
{
    Persona person = await personFactory.CreatePerson(model);
    Console.Write(person.Name + " (" + person.personality + "): ");
    await foreach (char c in handler.getDebateResponse(person))
    {
        Console.Write(c);
    }
    Console.WriteLine();
    Console.WriteLine();
}

Console.ReadLine();
