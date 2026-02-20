// See https://aka.ms/new-console-template for more information

using Debate_Library;

AI_Handler handler = new AI_Handler("grok-4-fast-reasoning");

Console.Write("What's the debate topic?: ");

handler.setTopic(Console.ReadLine());

List<MBTI.MbtiType> spokenTypes = new List<MBTI.MbtiType>();

for (int x = 0; x < 5; x++)
{
    MBTI.MbtiType type = RandomMBTI.ChooseFromExcluding(spokenTypes);
    spokenTypes.Add(type);
    Console.Write(type.ToString() + ": ");
    await foreach (char c in handler.getResponse(type))
    {
        Console.Write(c);
    }
    Console.WriteLine();
    Console.WriteLine();
}

Console.ReadLine();
