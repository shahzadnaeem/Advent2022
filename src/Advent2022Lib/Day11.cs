namespace Advent2022;

public class Day11
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public class Model
    {
        private string Data { get; set; } = "";

        // TODO: Define and initialise...
        public string[] Lines { get; init; } = null!;

        public Model(string input)
        {
            Data = input;

            Lines = Data.Split(Environment.NewLine);

            // TODO: Preprocessing for Part1 and Part2 below
        }

        // TODO: Parts 1 and 2
        public long Part1()
        {
            // TODO: Stuff...
            return -1;
        }

        public long Part2()
        {
            // TODO: Stuff...
            return -1;
        }

        public override string ToString()
        {
            // TODO: Additional details?

            var sw = new StringWriter();

            sw.Write($"Day N model - #Lines = {Lines.Length}");

            return sw.ToString();
        }
    }

    public Day11()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? DayNData.INPUT : DayNData.SAMPLE);
    }

    public (long, long) Answer()
    {
        // TODO: Start with SAMPLE data
        var model = GetModel(DataType.SAMPLE);

        Console.WriteLine($"Day N - #LINES = {model.Lines.Length}");

        // Part 1
        var result1 = (model.Part1(), model.Lines.Length);

        Console.WriteLine($"Part 1 = {result1}");

        // Part 2
        var result2 = (model.Part2(), model.Lines.Length);

        Console.WriteLine($"Part 2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
