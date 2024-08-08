using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace nd6;

public class Odds
{
    public static async Task Build(string selection)
    {
        var sw = new Stopwatch();
        sw.Start();

        var results = new ConcurrentDictionary<string, int>();
        var definitions = $"c:\\lab\\nd6\\app\\.runs\\{selection}".FromFile();
        
        var combinationsPerDefinition = definitions.ToDictionary (
            d => d.Key,
            d => d.Value.Count.GetCombinations()
        );

        var calculationsRemaining = combinationsPerDefinition.Sum(d => d.Value.Count());

        var initialSums = definitions.ToDictionary (
            x => x.Key,
            x => new Dictionary<int, double>()
        );

        var adjustedSums = definitions.ToDictionary (
            x => x.Key,
            x => new Dictionary<int, double>()
        );

        var tokenSource = new CancellationTokenSource();
        var token = tokenSource.Token;

        _ = Task.Run(async () => {
            var once = true;
            while(!token.IsCancellationRequested)
            {
                if (calculationsRemaining != 0)
                {
                    Console.WriteLine($"{sw.Display()} - calculations remaining: {calculationsRemaining}");
                }
                else
                {
                    if (once)
                    {
                        once = false;
                        Console.WriteLine($"{sw.Display()} - calculations remaining: {calculationsRemaining}");
                    }
                    else
                    {
                        Console.WriteLine($"{sw.Display()} - aggregating results");
                    }
                }

                await Task.Delay(2000);
            }
        });

        foreach(var key in definitions.Keys)
        {
            var definition = definitions[key];
            
            var size = (int)Math.Ceiling(combinationsPerDefinition[key].Count() / 10000m);
            var chunkedCalcuations = combinationsPerDefinition[key].Chunk(size);

            var tasks = new List<Task>();

            foreach(var chunk in chunkedCalcuations)
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
                        Interlocked.Decrement(ref calculationsRemaining);
                    }
                }));
            }

            await Task.WhenAll(tasks);

            var analysis = results.Select(x => x.Value).Distinct().ToDictionary(
                x => x,
                x => 0d
            );

            size = (int)Math.Ceiling(analysis.Count / 10000m);
            var chunkedAnalysis = analysis.Chunk(size);
            var threadSafeAnalysis = new ConcurrentDictionary<int, double>();

            tasks = [];

            foreach(var analyzedResult in analysis)
            {
                threadSafeAnalysis.TryAdd(analyzedResult.Key, analyzedResult.Value);

                tasks.Add(Task.Run(() => {
                    var total = (double)results.Where(r => r.Value == analyzedResult.Key).Count();
                    threadSafeAnalysis[analyzedResult.Key] = total / results.Count;
                }));
            }

            await Task.WhenAll(tasks);

            initialSums[key] = threadSafeAnalysis.ToDictionary(
                x => x.Key,
                x => x.Value
            );

            results.Clear();
        }

        tokenSource.Cancel();

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

        sw.Stop();

        Console.WriteLine($"{sw.Display()} - complete");
        Console.WriteLine();
        Console.WriteLine(content.ToString());
    }
}

