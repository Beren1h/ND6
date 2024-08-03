using nd6.definitions;

namespace nd6;

public static class Selector
{
    public static Dictionary<string, List<Die>> Get(string name)
    {
        return name switch {
            "impossible3x3" => Impossible3x3.Value,
            _ => Baseline.Value
        };
    }
}