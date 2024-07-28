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
            var column = string.Empty;

            foreach(var die in definition.Value)
            {
                column += $" {die.Neutral.Count}{die.Boon.Count}{die.Bane.Count}";
            }

            column = column.Trim();
            header.Append($",{column}");            
        }

        return header.ToString();
    }
}
