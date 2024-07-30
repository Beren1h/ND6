namespace nd6.definitions;

public static class BaselineBoons
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
            ["+1 boon"] = [
                new (
                    Neutral: [3,4],
                    Boon: [5,6],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [1,2,3,4,5],
                    Boon: [6],
                    Bane: []
                )
            ],
            ["+2 boon"] = [
                new (
                    Neutral: [3,4],
                    Boon: [5,6],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [1,2,3,4,5],
                    Boon: [6],
                    Bane: []
                ),
                new (
                    Neutral: [1,2,3,4,5],
                    Boon: [6],
                    Bane: []
                )
            ],
            ["+3 boon"] = [
                new (
                    Neutral: [3,4],
                    Boon: [5,6],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [1,2,3,4,5],
                    Boon: [6],
                    Bane: []
                ),
                new (
                    Neutral: [1,2,3,4,5],
                    Boon: [6],
                    Bane: []
                ),
                new (
                    Neutral: [1,2,3,4,5],
                    Boon: [6],
                    Bane: []
                )                
            ]
        };
}
