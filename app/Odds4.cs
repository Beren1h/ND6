using System.Xml.Schema;
using nd6.definitions;

namespace nd6;

public class Odds4
{
    static int[] MakeComb(int n, int k)
    {
    int[] result = new int[k];
    for (int i = 0; i < k; i++)
        result[i] = i;
    return result;
    }

    static void ShowComb(int[] comb)
    {
    int n = comb.Length;
    for (int i = 0; i < n; ++i)
        Console.Write(comb[i] + " ");
    Console.WriteLine("");
    }

    public static void Build(string selection)
    {
        int n = 5; int k = 3;
        int[] c = MakeComb(n, k); 
        Console.WriteLine("Initial Combination(n=5, k=3) is: ");
        ShowComb(c);

        // int a = 7;
        // int factorial = 1;
        // for (int x = 1; x <= a; x++)
        // {
        //     factorial *= x;
        // }

        // Console.WriteLine(factorial);

        // n! / (k! * (n-k)!))
    }
}
