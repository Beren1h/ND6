using System.Text;

namespace nd6;

public static class Extensions
{
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

    public static string GetHeader(this Dictionary<string, List<Die>> definitions)
    {
        var header = new StringBuilder("result");

        foreach(var definition in definitions)
        {
            //var column = string.Empty;
            var boons = 0;
            var banes = 0;
            //var task = 0;

            foreach(var die in definition.Value)
            {
                if (die.Boon.Count > 0 && die.Bane.Count == 0)
                {
                    boons++;
                }

                if (die.Bane.Count > 0 && die.Boon.Count == 0)
                {
                    banes++;
                }

                //column += $" {die.Neutral.Count}{die.Boon.Count}{die.Bane.Count}";
            }

            // var a = "1Bo1Ba";
            // var b = "1boon 1bane";
            // var c = "1+2-";
            // var d = "1oon1ane";

            // var boonBit = boons == 1 ?
            //         "1 boon" :
            //         $"{boons} boons";

            // var baneBit = banes == 1 ?
            //         "1 bane" :
            //         $"{banes} banes";

            // var boonBit = boons == 0 ?
            //     string.Empty :
            //         boons == 1 ?
            //         "1 boon" :
            //         $"{boons} boons";

            // var baneBit = banes == 0 ?
            //     string.Empty :
            //         banes == 1 ?
            //         "1 bane" :
            //         $"{banes} banes";

            //var column = $",{boonBit} {baneBit}";

            var column = boons + banes == 0 ?
            ",task" :
            $",{boons}|{banes}";

            //column = column.Trim();
            //header.Append($",{column}");
            header.Append(column);
        }

        return header.ToString();
    }
}
