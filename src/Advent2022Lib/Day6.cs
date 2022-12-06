namespace Advent2022;

public class Day6
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public class Model
    {
        public string Data { get; init; } = null!;

        public Model(string input)
        {
            Data = input;

            // TODO: Preprocessing for Part1 and Part2 below
        }

        // TODO: Helper/calculation methods as required
        public long LookupSomething()
        {
            // TODO: Stuff...
            return -1;
        }


        public override string ToString()
        {
            // TODO: Additional details?
            return Data;
        }
    }

    public Day6()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day6Data.INPUT : Day6Data.SAMPLE);
    }

    private long Part1(Model model, int length)
    {
        for (var i = 0; i < model.Data.Length - length; i++)
        {
            var subStr = model.Data.Substring(i, length);
            var numUnique = subStr.ToArray().Distinct().Count();

            if (numUnique == length)
            {
                return i + length;
            }
        }

        return -1;
    }

    private long Part2(Model model, int length)
    {
        return Part1(model, length);
    }

    public (long, long) Answer()
    {
        // TODO: Start with SAMPLE data
        var model = GetModel(DataType.INPUT);

        Console.WriteLine($"Day 6 - #CHARS = {model.Data.Length}");

        // Part 1
        var result1 = (Part1(model, 4), 0);

        Console.WriteLine($"Part 1 = {result1}");

        // Part 2
        var result2 = (Part2(model, 14), 0);

        Console.WriteLine($"Part 2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
