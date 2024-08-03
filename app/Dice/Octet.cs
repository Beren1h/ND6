namespace nd6;

public static class Octet
{
    static Octet()
    {
        Build();
    }

    public static List<string> Combinations { get; private set; }

    public static void Build()
    {
        var result = new List<(int, int, int, int, int, int, int, int)>();

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
                                for(var g = Patterns.LOWER; g <= Patterns.UPPER; g++)
                                {
                                    for(var h = Patterns.LOWER; h <= Patterns.UPPER; h++)
                                    {
                                        result.Add((a, b, c, d, e, f, g, h));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        Combinations = result.Select(i => $"{i.Item1}{i.Item2}{i.Item3}{i.Item4}{i.Item5}{i.Item6}{i.Item7}{i.Item8}").ToList();
    }
}
