namespace Advent2022;

// TODO: An idea...

public class Day
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public class BaseModel
    {
        public string Data { get; init; } = "";
        public long NumLines { get; init; } = 0;

        public BaseModel(string input)
        {
            Data = input;
            var lines = Data.Split(Environment.NewLine);
            NumLines = lines.Length;

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

    public Day()
    {
    }

    private BaseModel GetModel(DataType which = DataType.INPUT)
    {
        return new BaseModel(which == DataType.INPUT ? DayNData.INPUT : DayNData.SAMPLE);
    }

    private long Part1(BaseModel model)
    {
        // TODO: ...
        return -1;
    }

    private long Part2(BaseModel model)
    {
        // TODO: ...
        return -1;
    }

    public (long, long) Answer()
    {
        // TODO: Start with SAMPLE data
        var model = GetModel(DataType.SAMPLE);

        Console.WriteLine($"#LINES = {model.NumLines}");

        // Part 1
        var result1 = (Part1(model), 0);

        Console.WriteLine($"Part 1 = {result1}");

        // Part 2
        var result2 = (Part2(model), 0);

        Console.WriteLine($"Part 2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
