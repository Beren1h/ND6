namespace nd6.definitions;

public static class Extensions
{
    public const string SPREAD = "spreads";
    public const string ROLLS = "rolls";
    private readonly static Dictionary<string, Die> Spreads = [];
    private readonly static Dictionary<string, List<Die>> Definition = [];

    public static Dictionary<string, List<Die>> FromFile2(this string path)
    {
        var spreads = File.ReadAllLines($"c:\\lab\\nd6\\app\\.runs\\spreads");
        var rolls = File.ReadAllLines(path);

        var mode = string.Empty;

        //foreach(var line in File.ReadAllLines(path))
        foreach(var line in spreads.Concat(rolls).ToArray())
        {
            // name:boon[56]|bane[12]|neutral[34]

            mode = line switch {
                "# spreads" => SPREAD,
                "# rolls" => ROLLS,
                _ => mode
            };

            if (line.StartsWith('#'))
            {
                continue;
            }

            if (mode == SPREAD)
            {
                var split0 = line.Split(':');
                Spreads.Add(split0[0], BuildDie(split0[1]));
                continue;
            }

            if (mode == ROLLS)
            {
                Definition.Add(line, AddDice(line));
            }
        }

        return Definition;
    }

    private static List<Die> AddDice(string definition)
    {
        var pairs = definition.Split('|');
        List<Die> dice = [];

        foreach(var pair in pairs)
        {
            var a = pair[..1];
            var count = int.Parse(pair[..1]);
            var type = pair.Substring(1,1);

            for(var i = 0; i < count; i++)
            {
                dice.Add(Spreads[type]);
            }
        }
       
        return dice;
    }

    private static Die BuildDie(string definition)
    {
        var faces = definition.Split('|');
        
        List<int> neutral = [];
        List<int> boon = [];
        List<int> bane = [];

        foreach(var face in faces)
        {
            var range = face.Split('[', ']');

            if (range[0] == "neutral")
            {
                neutral = range[1].Select(n => int.Parse(n.ToString())).ToList();
            }

            if (range[0] == "boon")
            {
                boon = range[1].Select(n => int.Parse(n.ToString())).ToList();
            }

            if (range[0] == "bane")
            {
                bane = range[1].Select(n => int.Parse(n.ToString())).ToList();
            }     
        }

        return new Die (
            Neutral: neutral,
            Boon: boon,
            Bane: bane
        );
    }

    public static Dictionary<string, List<Die>> FromFile(this string path)
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
                    "certain" => new Die (
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

            if (line.StartsWith('#') || string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
            {
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
