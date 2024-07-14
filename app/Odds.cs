using System.Text;

namespace nd6;

public class Odds
{
    public static void Build()
    {
        var definition = new List<Die> {
            new (
                Boon: [5,6],
                Neutral: [3, 4],
                Bane: [1,2]
            ),
            new (
                Boon: [6],
                Neutral: [1, 2, 3, 4, 5],
                Bane: []
            ),
            new (
                Boon: [6],
                Neutral: [1, 2, 3, 4, 5],
                Bane: []
            ),
            new (
                Boon: [6],
                Neutral: [1, 2, 3, 4, 5],
                Bane: []
            )                      
            // new (
            //     Boon: 2,
            //     Neutral: 2,
            //     Bane: 2
            // ),
            // new (
            //     Boon: 2,
            //     Neutral: 2,
            //     Bane: 2
            // )            
        };

        var name = new StringBuilder();

        foreach(var die in definition)
        {
            name.Append($"o{die.Boon.Count}a{die.Bane.Count}n{die.Neutral.Count}_");
        }

        var combinations = definition.Count switch {
            1 => Solo.Combinations,
            2 => Duo.Combinations,
            3 => Trio.Combinations,
            4 => Quartet.Combinations,
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
        }

        var analysis = results.Select(x => x.Value).Distinct().ToDictionary(
            x => x,
            x => 0d
        );

        foreach (var a in analysis)
        {
            var total = (double)results.Where(r => r.Value == a.Key).Count();
            analysis[a.Key] = total / results.Count;
        }

        var checksum = analysis.Select(a => a.Value).Sum();
        
        var content = new StringBuilder();
        content.AppendLine("result,odds");
        
        foreach(var a in analysis)
        {
            //Console.WriteLine($"{a.Key} = {Math.Round(a.Value*100, 2)}%");
            Console.WriteLine($"{a.Value}");
            content.AppendLine($"{a.Key},{a.Value}");
        }

        File.WriteAllText($"c:\\lab\\ND6\\app\\Runs\\{name.ToString().Trim('_')}.csv", content.ToString());

        Console.WriteLine($"name={name.ToString().Trim('_')}");
        Console.WriteLine($"combinations={combinations.Count}");
        Console.WriteLine($"checksum={checksum}");
    }   
}