using System.Runtime.InteropServices;
using System.Security.Cryptography;
using nd6.definitions;

namespace nd6;

public class Odds3
{
    public static void Build(string selection)
    {
        var definitions = $"c:\\lab\\nd6\\app\\Definitions\\{selection}.ini".Build();

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
                7 => Septet.Combinations,
                8 => Octet.Combinations,
                9 => Nontet.Combinations,
                10 => Decade.Combinations,
                11 => Eleven.Combinations,
                _ => []
            };

            var size = 2;


            if (combinations.Count == 7776)
            {
                size = 500;
            }

            var chunked = combinations.Chunk(size);

            Console.WriteLine($"{combinations.Count} {size}");
        }
    }
}
