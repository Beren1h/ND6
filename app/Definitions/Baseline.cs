namespace nd6.definitions;

public static class Baseline
{
    public static readonly Dictionary<string, List<Die>> Value = new()
    {
            ["baseline"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                )
            ]
        };
}
