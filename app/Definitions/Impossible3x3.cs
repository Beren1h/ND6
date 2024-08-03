namespace nd6.definitions;

public static class Impossible3x3
{
    public static readonly Dictionary<string, List<Die>> Value = new()
    {
            ["impossible"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                )
            ],
            ["0 boon 1 bane"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                )
            ],
            ["0 boon 2 bane"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                )
            ],
            ["0 boon 3 bane"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                ),
            ],
            ["1 boon 0 bane"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                ),
                new (
                    Neutral: [1,2,3,4],
                    Boon: [5,6],
                    Bane: []
                )
            ],
            ["1 boon 1 bane"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                ),
                new (
                    Neutral: [1,2,3,4],
                    Boon: [5,6],
                    Bane: []
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                )
            ],
            ["1 boon 2 bane"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                ),
                new (
                    Neutral: [1,2,3,4],
                    Boon: [5,6],
                    Bane: []
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                )
            ],
            ["1 boon 3 bane"] = [
                new (
                    Neutral: [],
                    Boon: [],
                    Bane: [1,2,3,4,5,6]
                ),
                new (
                    Neutral: [1,2,3,4],
                    Boon: [5,6],
                    Bane: []
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                ),
                new (
                    Neutral: [3,4,5,6],
                    Boon: [],
                    Bane: [1,2]
                )
            ],
        };
}
