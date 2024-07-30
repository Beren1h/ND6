namespace nd6.definitions;

public static class BaselineTwoPipBoons
{
    private static readonly Die Baseline = new (
        Neutral: [3],
        Boon: [3,4,5,6],
        Bane: [1]
    );

    private static readonly Die Boon = new (
        Neutral: [1,2,3,4,5],
        Boon: [6],
        Bane: []       
    );

    public static readonly Dictionary<string, List<Die>> Value = new()
    {
            ["baseline + 2pip"] = [
                Baseline
            ],
            ["+1 boon"] = [
                Baseline,
                Boon
            ],
            ["+2 boon"] = [
                Baseline,
                Boon,
                Boon
            ],
            ["+3 boon"] = [
                Baseline,
                Boon,
                Boon,
                Boon
            ]
        };
}
