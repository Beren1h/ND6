namespace nd6;

public static class Quartet
{
    static Quartet()
    {
        Build();
    }

    public static List<string> Combinations { get; private set; }

    public static void Build()
    {
        var result = new List<(int, int, int, int)>();

        for(var a = Patterns.LOWER; a <= Patterns.UPPER; a++)
        {
            for(var b = Patterns.LOWER; b <= Patterns.UPPER; b++)
            {
                for(var c = Patterns.LOWER; c <= Patterns.UPPER; c++)
                {
                    for(var d = Patterns.LOWER; d <= Patterns.UPPER; d++)
                    {
                        result.Add((a, b, c, d));
                    }
                }
            }
        }

        Combinations = result.Select(i => $"{i.Item1}{i.Item2}{i.Item3}{i.Item4}").ToList();
    }
}
