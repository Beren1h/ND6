namespace nd6;

public static class Duo
{
    static Duo()
    {
        Build();
    }

    public static List<string> Combinations { get; private set; }
    
    public static void Build()
    {
        var result = new List<(int, int)>();

        for(var a = Patterns.LOWER; a <= Patterns.UPPER; a++)
        {
            for(var b = Patterns.LOWER; b <= Patterns.UPPER; b++)
            {
                result.Add((a, b));
            }
        }

        Combinations = result.Select(i => $"{i.Item1}{i.Item2}").ToList();
    }
}
