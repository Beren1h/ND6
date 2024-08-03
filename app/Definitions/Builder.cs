namespace nd6.definitions;

public static class Builder
{
    public static Dictionary<string, List<Die>> Build(this string path)
    {
        var definition = new Dictionary<string, List<Die>>();

        var index = 0;

        IEnumerable<int> boonFaces = [];
        IEnumerable<int> baneFaces = [];
        Die task = default;

        foreach(var line in File.ReadLines(path))
        {
            if (index == 0)
            {
                var split = line.Split("|");
                boonFaces = split[0].Split(",").Select(int.Parse).AsEnumerable();
                baneFaces = split[1].Split(",").Select(int.Parse).AsEnumerable();;
                index++;
                continue;
            }

            if (index == 1)
            {
                var die = line switch {
                    "impossible" => new Die (
                        Boon: [],
                        Neutral: [],
                        Bane: [1,2,3,4,5,6]
                    ),
                    "very hard" => new Die (
                        Boon: [6],
                        Neutral: [],
                        Bane: [1,2,3,4,5]
                    ),
                    "hard" => new Die (
                        Boon: [5,6],
                        Neutral: [],
                        Bane: [1,2,3,4]                    
                    ),
                    "neutral" => new Die (
                        Boon: [4,5,6],
                        Neutral: [],
                        Bane: [1,2,3]
                    ),
                    "easy" => new Die (
                        Boon: [3,4,5,6],
                        Neutral: [],
                        Bane: [1,2]
                    ),
                    "very easy" => new Die (
                        Boon: [2,3,4,5,6],
                        Neutral: [],
                        Bane: [1]
                    ),
                    "guaranteed" => new Die (
                        Boon: [1,2,3,4,5,6],
                        Neutral: [],
                        Bane: []
                    ),
                    _ => new Die (
                        Boon: [],
                        Neutral: [],
                        Bane: []
                    )
                };

                task = die;
                definition.Add(line, [task]);
                index++;
                continue;
            }

            var boonCount = int.Parse(line[..1]);
            var baneCount = int.Parse(line.Substring(1,1));
            var range = new List<int> {1,2,3,4,5,6};
            
            definition.Add(line,[task]);

            if (boonCount > 0)
            {
                for(var i = 0; i < boonCount; i++)
                {
                    //var boonFaces = boon.Select(b => int.Parse(b)).ToList();

                    var die = new Die (
                        Boon: boonFaces.ToList(),
                        Neutral: range.Except(boonFaces).ToList(),
                        Bane: []
                    );

                    definition[line].Add(die);
                }
            }

            if (baneCount > 0)
            {
                for(var i = 0; i < baneCount; i++)
                {
                    //var baneFaces = bane.Select(b => int.Parse(b)).ToList();

                    var die = new Die (
                        Boon: [],
                        Neutral: range.Except(baneFaces).ToList(),
                        Bane: baneFaces.ToList()
                    );

                    definition[line].Add(die);
                }                
            }

            index++;
        }

        return definition;
    }
}