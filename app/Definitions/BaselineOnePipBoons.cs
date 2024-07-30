namespace nd6.definitions;

public static class BaselineOnePipBoons
{
    public static readonly Dictionary<string, List<Die>> Value = new()
    {
            ["baseline + 1pip"] = [
                new (
                    Neutral: [2,3],
                    Boon: [4,5,6],
                    Bane: [1]
                )
            ],
            ["+1 boon"] = [
                new (
                    Neutral: [2,3],
                    Boon: [4,5,6],
                    Bane: [1]
                ),
                new (
                    Neutral: [1,2,3,4,5],
                    Boon: [6],
                    Bane: []
                )
            ],
            ["+2 boon"] = [
                new (
                    Neutral: [2,3],
                    Boon: [4,5,6],
                    Bane: [1]
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
                    Neutral: [2,3],
                    Boon: [4,5,6],
                    Bane: [1]
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