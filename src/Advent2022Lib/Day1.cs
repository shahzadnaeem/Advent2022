namespace Advent2022;

public class Day1
{
    public class Model
    {
        public string Data { get; init; } = String.Empty;
        public long[] Rows { get; init; } = null!;

        public Model(string input)
        {
            Data = input;
            Rows = Data.Split(Environment.NewLine).Select(l => l != String.Empty ? long.Parse(l) : 0).ToArray();
        }


        public override string ToString()
        {
            return $"[{string.Join(',', Rows)}]";
        }
    }

    public Day1()
    {
    }

    private Model GetModel()
    {
        return new Model(Day1Data.INPUT);
    }

    public (long, long) GetMaxCalories(long[] counts, long lessThan = long.MaxValue)
    {
        long max = 0;
        long elf = 1;
        long maxElf = elf;
        long currSum = 0;

        foreach (var count in counts)
        {
            if (count != 0)
            {
                currSum += count;
            }
            else
            {
                if (currSum > max && currSum < lessThan)
                {
                    max = currSum;
                    maxElf = elf;
                }
                elf++;
                currSum = 0;
            }
        }

        // Final check
        if (currSum > max && currSum < lessThan)
        {
            max = currSum;
            maxElf = elf;
        }

        return (max, maxElf);
    }

    public (long, long) Answer()
    {
        var model = GetModel();

        // Console.WriteLine($"ROWS = {model}");
        Console.WriteLine($"#ROWS = {model.Rows.Length}");

        // Part 1
        var result1 = GetMaxCalories(model.Rows);

        Console.WriteLine($"Result1 = {result1}");

        // Part 2
        long tot2 = 0;

        var result2 = GetMaxCalories(model.Rows);
        tot2 += result2.Item1;

        result2 = GetMaxCalories(model.Rows, result2.Item1);
        tot2 += result2.Item1;

        result2 = GetMaxCalories(model.Rows, result2.Item1);
        tot2 += result2.Item1;

        result2 = (tot2, 0);

        Console.WriteLine($"Result2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
