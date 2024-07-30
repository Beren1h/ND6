using nd6.definitions;

namespace nd6;

public static class Selector
{
    public static Dictionary<string, List<Die>> Get(string name)
    {
        return name switch {
            "baseline+pips" => BaselineAndPips.Value,
            "baseline+1pip+boons" => BaselineOnePipBoons.Value,
            "baseline+2pip+boons" => BaselineTwoPipBoons.Value,
            "baseline+boons" => BaselineBoons.Value,
            _ => Baseline.Value
        };
    }
}