// using System.Collections.Concurrent;
// using System.Diagnostics;
// using System.Text;

// namespace nd6;

// public class Odds2
// {
//     public static async Task Build(string selection)
//     {
//         var sw = new Stopwatch();
//         sw.Start();

//         var definitions = $"c:\\lab\\nd6\\app\\.runs\\{selection}".FromFile();

//         var initialSums = definitions.ToDictionary (
//             x => x.Key,
//             x => new Dictionary<int, double>()
//         );

//         var adjustedSums = definitions.ToDictionary (
//             x => x.Key,
//             x => new Dictionary<int, double>()
//         );

//         var results = new ConcurrentDictionary<string, ConcurrentDictionary<string, int>>();

//         //var initialSums2 = new Dictionary<string, Dictionary<string, Dictionary<int, double>>>();
//         //var adjustedSums2 = new Dictionary<string, Dictionary<string, Dictionary<int, double>>>();

//         var analysisPerDefinition = new Dictionary<string, Dictionary<int, double>>();

//         var definitionTasks = new List<Task>();

//         foreach (var key in definitions.Keys)
//         {
//             // initialSums2.TryAdd(key, definitions.ToDictionary (
//             //     x => x.Key,
//             //     x => new Dictionary<int, double>()
//             // ));

//             // adjustedSums2.TryAdd(key, definitions.ToDictionary (
//             //     x => x.Key,
//             //     x => new Dictionary<int, double>()
//             // ));

//             results.TryAdd(key, []);
//         }

//         foreach (var key in definitions.Keys)
//         {
//             definitionTasks.Add(Task.Run(async () => {

//                 var definition = definitions[key];
//                 var combinations = definition.Count.GetCombinations();
//                 var size = (int)Math.Ceiling(combinations.Count() / 10000m);
//                 var chunked = combinations.Chunk(size);
//                 var calculationTasks = new List<Task>();

//                 foreach (var chunk in chunked)
//                 {
//                     calculationTasks.Add(Task.Run(() => {

//                         for (var i = 0; i < chunk.Length; i++)
//                         {
//                             var success = 0;
//                             var failure = 0;

//                             for (var j = 0; j < chunk[i].Length; j++)
//                             {
//                                 var faces = definition[j];
//                                 var roll = chunk[i].Substring(j, 1);

//                                 if (faces.Boon.Contains(int.Parse(roll)))
//                                 {
//                                     success += 1;
//                                 }

//                                 if (faces.Bane.Contains(int.Parse(roll)))
//                                 {
//                                     failure -= 1;
//                                 }
//                             }

//                             results[key][chunk[i]] = success+failure;
//                         }
//                     }));
//                 }

//                 await Task.WhenAll(calculationTasks);

//                 //var analysisPerDefinition = new Dictionary<string, Dictionary<int, double>>();
                
//                 // var analysis = results[key].Select(x => x.Value).Distinct().ToDictionary(
//                 //     x => x,
//                 //     x => 0d
//                 // );

//                 // analysisPerDefinition.Add(key, analysis);

//                 analysisPerDefinition.Add(key, results[key].Select(x => x.Value).Distinct().ToDictionary(
//                     x => x,
//                     x => 0d
//                 ));

//                 foreach (var a in analysisPerDefinition[key])
//                 {
//                     var total = (double)results[key].Where(r => r.Value == a.Key).Count();
//                     analysisPerDefinition[key][a.Key] = total / results[key].Count;
//                 }

//                 initialSums[key] = analysisPerDefinition[key];

//             }));
//         }

//         //Console.WriteLine($"{definitionTasks.Count}");
//         await Task.WhenAll(definitionTasks);

//         var range = initialSums.SelectMany(a => a.Value.Keys).Distinct();

//         foreach(var initialSum in initialSums)
//         {
//             var odds = initialSums[initialSum.Key];
//             var sums = odds.Select(x => x.Key);
//             var missingSums = range.Except(sums);

//             foreach(var sum in missingSums)
//             {
//                 odds.Add(sum, 0f);
//             }

//             adjustedSums[initialSum.Key] = odds.OrderBy(o => o.Key).ToDictionary (
//                 x => x.Key,
//                 x => x.Value
//             );
//         }

//         var csv = adjustedSums.SelectMany(a => a.Value.Keys).Distinct().ToDictionary(
//             x => x,
//             x => new List<double>()
//         );

//         var content = new StringBuilder();
//         var file = definitions.GetFileName();

//         content.AppendLine(definitions.GetHeader());

//         foreach (var line in csv)
//         {
//             var row = $"{line.Key}";

//             foreach(var sum in adjustedSums.Values)
//             {
//                 var odds = sum[line.Key];
//                 line.Value.Add(odds);
//                 row += $",{odds}";
//             }

//             content.AppendLine(row);
//         }
       
//         sw.Stop();
//         Console.WriteLine(content.ToString());
//         Console.WriteLine(sw.Elapsed.TotalSeconds);
//     }
// }

// //         var results = new ConcurrentDictionary<string, int>();

// //         var initialSums = definitions.ToDictionary (
// //             x => x.Key,
// //             x => new Dictionary<int, double>()
// //         );

// //         var adjustedSums = definitions.ToDictionary (
// //             x => x.Key,
// //             x => new Dictionary<int, double>()
// //         );

// //         _ = Task.Run(async () => {
// //             while(true)
// //             {
// //                 foreach(var key in results.Keys)
// //                 {
// //                     Console.WriteLine(key);
// //                 }
                
// //                 await Task.Delay(2000);
// //             }
// //         });

// //         var outerTasks = new List<Task>();


// //         foreach(var key in definitions.Keys)
// //         {
// //             outerTasks.Add(Task.Run(() => {

// //             }));

// //             var definition = definitions[key];
            
// //             var combinations = definition.Count.GetCombinations();
            
// //             var size = (int)Math.Ceiling(combinations.Count() / 10000m);
            
// //             var chunked = combinations.Chunk(size);

// //             var tasks = new List<Task>();

// //             foreach(var chunk in chunked)
// //             {
// //                 tasks.Add(Task.Run(() => {
                    
// //                     for (var i = 0; i < chunk.Length; i++)
// //                     {
// //                         var success = 0;
// //                         var failure = 0;

// //                         for (var j = 0; j < chunk[i].Length; j++)
// //                         {
// //                             var faces = definition[j];
// //                             var roll = chunk[i].Substring(j, 1);

// //                             if (faces.Boon.Contains(int.Parse(roll)))
// //                             {
// //                                 success += 1;
// //                             }

// //                             if (faces.Bane.Contains(int.Parse(roll)))
// //                             {
// //                                 failure -= 1;
// //                             }
// //                         }

// //                         results[chunk[i]] = success+failure;
// //                     }
// //                 }));
// //             }

// //             await Task.WhenAll(tasks);

// //             var analysis = results.Select(x => x.Value).Distinct().ToDictionary(
// //                 x => x,
// //                 x => 0d
// //             );

// //             foreach (var a in analysis)
// //             {
// //                 var total = (double)results.Where(r => r.Value == a.Key).Count();
// //                 analysis[a.Key] = total / results.Count;
// //             }

// //             initialSums[key] = analysis;

// // //             Console.WriteLine(@$"
// // // rolls: {definition.Count},
// // // combinations: {combinations.Count()},
// // // chunks: {chunked.Count()},
// // // tasks: {tasks.Count} successful={tasks.Where(t => t.IsCompletedSuccessfully).Count()},
// // // results: {results.Count}
// // //             ");

// //             results.Clear();
// //         }

// //         var range = initialSums.SelectMany(a => a.Value.Keys).Distinct();

// //         foreach(var initialSum in initialSums)
// //         {
// //             var odds = initialSums[initialSum.Key];
// //             var sums = odds.Select(x => x.Key);
// //             var missingSums = range.Except(sums);

// //             foreach(var sum in missingSums)
// //             {
// //                 odds.Add(sum, 0f);
// //             }

// //             adjustedSums[initialSum.Key] = odds.OrderBy(o => o.Key).ToDictionary (
// //                 x => x.Key,
// //                 x => x.Value
// //             );
// //         }

// //         var csv = adjustedSums.SelectMany(a => a.Value.Keys).Distinct().ToDictionary(
// //             x => x,
// //             x => new List<double>()
// //         );

// //         var content = new StringBuilder();
// //         var file = definitions.GetFileName();

// //         content.AppendLine(definitions.GetHeader());

// //         foreach (var line in csv)
// //         {
// //             var row = $"{line.Key}";

// //             foreach(var sum in adjustedSums.Values)
// //             {
// //                 var odds = sum[line.Key];
// //                 line.Value.Add(odds);
// //                 row += $",{odds}";
// //             }

// //             content.AppendLine(row);
// //         }
       
// //         sw.Stop();
// //         Console.WriteLine(content.ToString());
// //    }
// //}
