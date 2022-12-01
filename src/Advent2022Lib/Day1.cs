using System.Collections.Generic;

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

    public List<(long, long)> CaloriesByElf(Model model)
    {
        var result = new List<(long, long)>();

        long elfNum = 1;
        long currTotal = 0;

        foreach (var value in model.Rows)
        {
            if (value == 0)
            {
                result.Add((currTotal, elfNum));
                elfNum++;
                currTotal = 0;
            }
            else
            {
                currTotal += value;
            }
        }

        if (currTotal != 0)
        {
            result.Add((currTotal, elfNum++));
        }

        result = result.OrderByDescending(entry => entry.Item1).ToList();

        return result;
    }

    public (long, long) Answer()
    {
        var model = GetModel();

        // Console.WriteLine($"ROWS = {model}");
        Console.WriteLine($"#ROWS = {model.Rows.Length}");

        // Part 1
        var result1 = CaloriesByElf(model).Take(1).Single();

        Console.WriteLine($"Result1 = {result1}");

        // Part 2
        var result2 = (CaloriesByElf(model).Take(3).Select(e => e.Item1).Sum(), 0);

        Console.WriteLine($"Result2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
