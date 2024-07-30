using System.Text;

namespace nd6;

public class Odds2
{
    public static void Build(string selection)
    {
        var definitions = Selector.Get(selection);
        // var definitions = new Dictionary<string, List<Die>> {
        //     ["baseline"] = [
        //         new (
        //             Neutral: [3,4],
        //             Boon: [5,6],
        //             Bane: [1,2]
        //         )
        //     ],
        //     ["+1 pip"] = [
        //         new (
        //             Neutral: [2,3],
        //             Boon: [4,5,6],
        //             Bane: [1]
        //         )
        //     ],
        //     ["+2 pip"] = [
        //         new (
        //             Neutral: [2],
        //             Boon: [3,4,5,6],
        //             Bane: [1]
        //         )
        //     ],
        //     ["+3 pip"] = [
        //         new (
        //             Neutral: [],
        //             Boon: [2,3,4,5,6],
        //             Bane: [1]
        //         )
        //     ]
        // };

        var initialSums = definitions.ToDictionary (
            x => x.Key,
            x => new Dictionary<int, double>()
        );

        var adjustedSums = definitions.ToDictionary (
            x => x.Key,
            x => new Dictionary<int, double>()
        );

        foreach(var key in definitions.Keys)
        {
            var definition = definitions[key];

            var combinations = definition.Count switch {
                1 => Solo.Combinations,
                2 => Duo.Combinations,
                3 => Trio.Combinations,
                4 => Quartet.Combinations,
                5 => Quintet.Combinations,
                6 => Sextet.Combinations,
                _ => []
            };

            var results = combinations.ToDictionary (
                x => x,
                x => 0
            );

            for (var i = 0; i < combinations.Count; i++)
            {
                var success = 0;
                var failure = 0;

                for (var j = 0; j < combinations[i].Length; j++)
                {
                    var faces = definition[j];
                    var roll = combinations[i].Substring(j, 1);

                    if (faces.Boon.Contains(int.Parse(roll)))
                    {
                        success += 1;
                    }

                    if (faces.Bane.Contains(int.Parse(roll)))
                    {
                        failure -= 1;
                    }
                }

                results[combinations[i]] = success+failure;

                var analysis = results.Select(x => x.Value).Distinct().ToDictionary(
                    x => x,
                    x => 0d
                );

                foreach (var a in analysis)
                {
                    var total = (double)results.Where(r => r.Value == a.Key).Count();
                    analysis[a.Key] = total / results.Count;
                }

                initialSums[key] = analysis;
            }               
        }

        var range = initialSums.SelectMany(a => a.Value.Keys).Distinct();

        foreach(var initialSum in initialSums)
        {
            var odds = initialSums[initialSum.Key];
            var sums = odds.Select(x => x.Key);
            var missingSums = range.Except(sums);

            foreach(var sum in missingSums)
            {
                odds.Add(sum, 0f);
            }

            adjustedSums[initialSum.Key] = odds.OrderBy(o => o.Key).ToDictionary (
                x => x.Key,
                x => x.Value
            );
        }

        var csv = adjustedSums.SelectMany(a => a.Value.Keys).Distinct().ToDictionary(
            x => x,
            x => new List<double>()
        );

        var content = new StringBuilder();
        var file = definitions.GetFileName();

        content.AppendLine(definitions.GetHeader());

        foreach (var line in csv)
        {
            var row = $"{line.Key}";

            foreach(var sum in adjustedSums.Values)
            {
                var odds = sum[line.Key];
                line.Value.Add(odds);
                row += $",{odds}";
            }

            content.AppendLine(row);
        }
       
        Console.WriteLine(content.ToString());

        File.WriteAllText($"c:\\lab\\ND6\\app\\Runs\\{file}.csv", content.ToString());
    }   
}