using System.Collections.Concurrent;
using System.Text;

namespace nd6;

public class Odds
{
    public static async Task Build(string selection)
    {
        var results = new ConcurrentDictionary<string, int>();

        var definitions = $"c:\\lab\\nd6\\app\\.runs\\{selection}".FromFile();

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
            
            var combinations = definition.Count.GetCombinations();
            
            var size = (int)Math.Ceiling(combinations.Count() / 10000m);
            
            var chunked = combinations.Chunk(size);

            var tasks = new List<Task>();

            foreach(var chunk in chunked)
            {
                tasks.Add(Task.Run(() => {
                    
                    for (var i = 0; i < chunk.Length; i++)
                    {
                        var success = 0;
                        var failure = 0;

                        for (var j = 0; j < chunk[i].Length; j++)
                        {
                            var faces = definition[j];
                            var roll = chunk[i].Substring(j, 1);

                            if (faces.Boon.Contains(int.Parse(roll)))
                            {
                                success += 1;
                            }

                            if (faces.Bane.Contains(int.Parse(roll)))
                            {
                                failure -= 1;
                            }
                        }

                        results[chunk[i]] = success+failure;
                    }
                }));
            }

            await Task.WhenAll(tasks);

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

//             Console.WriteLine(@$"
// rolls: {definition.Count},
// combinations: {combinations.Count()},
// chunks: {chunked.Count()},
// tasks: {tasks.Count} successful={tasks.Where(t => t.IsCompletedSuccessfully).Count()},
// results: {results.Count}
//             ");

            results.Clear();
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
    }
}
