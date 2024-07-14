namespace nd6;

public static class Trio
{
    static Trio()
    {
        Build();
    }

    public static List<string> Combinations { get; private set; }
    public static Dictionary<int, Dictionary<int, int>> PatternMatches { get; private set; } = [];

    public static void Build()
    {
        var result = new List<(int, int, int)>();

        for(var a = Patterns.LOWER; a <= Patterns.UPPER; a++)
        {
            for(var b = Patterns.LOWER; b <= Patterns.UPPER; b++)
            {
                for(var c = Patterns.LOWER; c <= Patterns.UPPER; c++)
                {
                    result.Add((a, b, c));
                }

            }
        }

        Combinations = result.Select(i => $"{i.Item1}{i.Item2}{i.Item3}").ToList();
    }
}
