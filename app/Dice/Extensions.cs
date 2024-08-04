namespace nd6.dice;

public static class Extensions
{
    private const int LOWER = 1;
    private const int UPPER = 6;

    public static IEnumerable<string> GetCombinations(this int rolls)
    {
        IEnumerable<string> combinations = [""];

        var possibleOutcomesPerRoll = new List<List<string>>();

        for (var i = 0; i < rolls; i++)
        {
            var possibleRolls = new List<string>();

            for (var j = LOWER; j <= UPPER; j++)
            {
                possibleRolls.Add(j.ToString());
            }

            //possibleOutcomesPerRoll.Add(["1", "2", "3", "4", "5", "6"]);
            possibleOutcomesPerRoll.Add(possibleRolls);
        }

        foreach (var roll in possibleOutcomesPerRoll)
        {
            combinations = combinations.SelectMany(a => roll.Select(b => b + a));
        }

        return combinations;
    }
}
