using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

namespace nd6;

public class Cruncher
{
    public int Crunch(
        List<string> combinations,
        List<Die> definition
    ){
            var x = new ConcurrentDictionary<int, double>();

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
                
                // results[combinations[i]] = success+failure;

                // var analysis = results.Select(x => x.Value).Distinct().ToDictionary(
                //     x => x,
                //     x => 0d
                // );

                // foreach (var a in analysis)
                // {
                //     var total = (double)results.Where(r => r.Value == a.Key).Count();
                //     analysis[a.Key] = total / results.Count;
                // }

                // initialSums[key] = analysis;


            }               
        }        
    }
