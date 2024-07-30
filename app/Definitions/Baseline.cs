namespace nd6.definitions;

public static class Baseline
{
    public static readonly Dictionary<string, List<Die>> Value = new()
    {
            ["baseline"] = [
                new (
                    Neutral: [3,4],
                    Boon: [5,6],
                    Bane: [1,2]
                )
            ]
        };
}
