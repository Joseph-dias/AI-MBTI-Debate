// See https://aka.ms/new-console-template for more information

using Debate_Library;

AIDebateHandler handler = new AIDebateHandler("grok-4-fast-reasoning");

Console.Write("What's the debate topic?: ");

handler.setTopic(Console.ReadLine());

List<Personality.MbtiType> spokenTypes = new List<Personality.MbtiType>();

for (int x = 0; x < 5; x++)
{
    Personality.MbtiType type = RandomMBTI.ChooseFromExcluding(spokenTypes);
    spokenTypes.Add(type);
    Console.Write(type.ToString() + ": ");
    await foreach (char c in handler.getDebateResponse(type))
    {
        Console.Write(c);
    }
    Console.WriteLine();
    Console.WriteLine();
}

Console.ReadLine();
