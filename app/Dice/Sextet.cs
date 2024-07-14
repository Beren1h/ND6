namespace nd6;

public static class Sextet
{
    static Sextet()
    {
        Build();
    }

    public static IEnumerable<string> Combinations { get; private set; }

    public static void Build()
    {
        var result = new List<(int, int, int, int, int, int)>();

        for(var a = Patterns.LOWER; a <= Patterns.UPPER; a++)
        {
            for(var b = Patterns.LOWER; b <= Patterns.UPPER; b++)
            {
                for(var c = Patterns.LOWER; c <= Patterns.UPPER; c++)
                {
                    for(var d = Patterns.LOWER; d <= Patterns.UPPER; d++)
                    {
                        for(var e = Patterns.LOWER; e <= Patterns.UPPER; e++)
                        {
                            for(var f = Patterns.LOWER; f <= Patterns.UPPER; f++)
                            {
                                result.Add((a, b, c, d, e, f));
                            }
                        }
                    }
                }
            }
        }

        Combinations = result.Select(i => $"{i.Item1}{i.Item2}{i.Item3}{i.Item4}{i.Item5}{i.Item6}");
    }
}
