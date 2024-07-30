namespace nd6.definitions;

public static class BaselineAndPips
{
    public static readonly Dictionary<string, List<Die>> Value = new()
    {
            ["baseline"] = [
                new (
                    Neutral: [3,4],
                    Boon: [5,6],
                    Bane: [1,2]
                )
            ],
            ["+1 pip"] = [
                new (
                    Neutral: [2,3],
                    Boon: [4,5,6],
                    Bane: [1]
                )
            ],
            ["+2 pip"] = [
                new (
                    Neutral: [2],
                    Boon: [3,4,5,6],
                    Bane: [1]
                )
            ],
            ["+3 pip"] = [
                new (
                    Neutral: [],
                    Boon: [2,3,4,5,6],
                    Bane: [1]
                )
            ]
        };
}