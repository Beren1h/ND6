using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace nd6;

public static class Extensions
{
    public static string Display(this Stopwatch sw)
    {
        var e = sw.Elapsed;

        return $"{e.Hours:00}h {e.Minutes:00}m {e.Seconds:00}s {e.Milliseconds:000}m ";
    }

    public static string GetFileName(this Dictionary<string, List<Die>> definitions)
    {
        var name = new StringBuilder();

        foreach(var definition in definitions)
        {
            name.Append(' ');

            var lastIndex = definition.Value.Count - 1;

            if (lastIndex != 0)
            {
                name.Append('[');
            }

            for(var i = 0; i < definition.Value.Count; i++)
            {
                if (i > 0)
                {
                    name.Append(' ');
                }                
                
                name.Append($"{definition.Value[i].Neutral.Count}{definition.Value[i].Boon.Count}{definition.Value[i].Bane.Count}");

                if (i == lastIndex && i > 0)
                {
                    name.Append(']');
                }   
            }
        }

        return name.ToString().Trim();
    }

    // public static string GetHeader(this Dictionary<string, List<Die>> definitions)
    // {
    //     var header = new StringBuilder("result");

    //     foreach(var definition in definitions)
    //     {
    //         header.Append($",{definition.Key}");

            // var boons = 0;
            // var banes = 0;

            // foreach(var die in definition.Value)
            // {
            //     if (die.Boon.Count > 0 && die.Bane.Count == 0)
            //     {
            //         boons++;
            //     }

            //     if (die.Bane.Count > 0 && die.Boon.Count == 0)
            //     {
            //         banes++;
            //     }
            // }

            // var column = boons + banes == 0 ?
            // ",task" :
            // $",{boons}|{banes}";

            // header.Append(column);
        //}

        //return header.ToString();
    //}
}
