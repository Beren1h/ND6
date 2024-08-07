using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace nd6;

public class Logger
{
    public ConcurrentDictionary<string, ConcurrentBag<int>> Calculations { get; set; } = [];
    public ConcurrentDictionary<string, int> Combinations { get; set; } = [];
    public int Total = 0;
}

public class Odds
{
    public static async Task Build(string selection)
    {
        var sw = new Stopwatch();
        

        var logger = new Logger();

        //var logging = new Dictionary<string, Logger>();
        //var logging = new ConcurrentDictionary<string, Logger>();
        //var calc = new ConcurrentDictionary<string, ConcurrentBag<int>>();
        //var comb = new ConcurrentDictionary<string, int>();

        // var calculationAccumulator = 0;
        // var log_Combinations = 0;
        // string log_Definition = "";


        sw.Start();

        var results = new ConcurrentDictionary<string, int>();
        //var log = "";

        var definitions = $"c:\\lab\\nd6\\app\\.runs\\{selection}".FromFile();

        foreach(var key in definitions.Keys)
        {
            var definition = definitions[key];
            var combinations = definition.Count.GetCombinations();

            logger.Total += combinations.Count();
            logger.Calculations.TryAdd(key, []);
            logger.Combinations.TryAdd(key, 0);
        }

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
                if (logger.Total != 0)
                {
                    Console.WriteLine($"calculations remaining: {logger.Total}, elapsed: {sw.ElapsedMilliseconds}ms");
                }
                else
                {
                    if (once)
                    {
                        once = false;
                        Console.WriteLine($"calculations remaining: {logger.Total}, elapsed: {sw.ElapsedMilliseconds}ms");
                    }
                    else
                    {
                        Console.WriteLine($"finishing up, elapsed: {sw.ElapsedMilliseconds}ms");
                    }
                }

                await Task.Delay(2000);
            }
        });

        foreach(var key in definitions.Keys)
        {
            var definition = definitions[key];
            
            var combinations = definition.Count.GetCombinations();

            logger.Combinations[key] = combinations.Count();
            
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

                            //logging[key].Calculations++;
                        }

                        results[chunk[i]] = success+failure;
                        //calc[key].Add(1);
                        logger.Calculations[key].Add(1);
                        Interlocked.Decrement(ref logger.Total);
                    }
                    
                    //log = $"{key}: {chunk.Length} of {combinations.Count()}";
                    //Console.WriteLine($"{key}: {index} of {chunked.Count()}");
                }));
            }

            await Task.WhenAll(tasks);

            var analysis = results.Select(x => x.Value).Distinct().ToDictionary(
                x => x,
                x => 0d
            );

            var size2 = (int)Math.Ceiling(analysis.Count / 10000m);
            var chucked2 = analysis.Chunk(size2);
            //Console.WriteLine(chucked2.Count());
            var analysis2 = new ConcurrentDictionary<int, double>();
            
            foreach(var a in analysis)
            {
                analysis2.TryAdd(a.Key, a.Value);
            }

            var tasks2 = new List<Task>();

            foreach(var a in analysis)
            {
                analysis2.TryAdd(a.Key, a.Value);
                tasks2.Add(Task.Run(() => {
                    var total = (double)results.Where(r => r.Value == a.Key).Count();
                    analysis2[a.Key] = total / results.Count;
                    //Console.WriteLine($"{total}, elpased: {sw.ElapsedMilliseconds}ms ");
                }));
            }

            await Task.WhenAll(tasks2);

            // foreach (var a in analysis)
            // {
            //     var total = (double)results.Where(r => r.Value == a.Key).Count();
            //     analysis[a.Key] = total / results.Count;
            //     Console.WriteLine($"{total}, elpased: {sw.ElapsedMilliseconds}ms ");
            // }

            initialSums[key] = analysis2.ToDictionary(
                x => x.Key,
                x => x.Value
            );

            
//             Console.WriteLine(@$"
// rolls: {definition.Count},
// combinations: {combinations.Count()},
// size: {size},
// chunks: {chunked.Count()},
// calculations: {chunked.Select(c => c.ToList().Count).Sum()},
// tasks: {tasks.Count} successful={tasks.Where(t => t.IsCompletedSuccessfully).Count()},
// results: {results.Count}
//             ");

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

        Console.WriteLine($"completed {sw.ElapsedMilliseconds}ms");
        Console.WriteLine();
        Console.WriteLine(content.ToString());
    }
}
