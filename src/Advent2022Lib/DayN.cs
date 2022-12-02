namespace Advent2022;

public class DayN
{
    public class Model
    {
        private string Data { get; set; } = "";
        // TODO: Define and initialise...
        public long Rows { get; init; } = 0;

        public Model(string input)
        {
            Data = input;
        }


        public override string ToString()
        {
            return Data;
        }
    }

    public DayN()
    {
    }

    private Model GetModel()
    {
        return new Model(DayNData.INPUT);
    }

    public (long, long) Answer()
    {
        var model = GetModel();

        Console.WriteLine($"#ROWS = TODO:{model.Rows}");

        // Part 1
        var result1 = (0, 0);

        Console.WriteLine($"Result1 = {result1}");

        // Part 2
        var result2 = (0, 0);

        Console.WriteLine($"Result2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
