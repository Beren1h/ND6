namespace nd6;

public static class Solo
{
    static Solo()
    {
        Build();
    }

    public static List<string> Combinations { get; private set; }

    public static void Build()
    {
        var result = new List<int>();

        for(var a = Patterns.LOWER; a <= Patterns.UPPER; a++)
        {
            result.Add(a);
        }

        Combinations = result.Select(i => $"{i}").ToList();
    }
}
