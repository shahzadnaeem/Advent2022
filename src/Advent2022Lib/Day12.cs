namespace Advent2022;

public class Day12
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    // TODO: Other enums/consts/classes ...

    public class Model
    {
        public string[] Lines { get; init; } = null!;

        public Model(string input)
        {
            Lines = input.Split(Environment.NewLine);

            // TODO: Define and initialise...
        }

        // TODO: Parts 1 and 2
        public long Part1(bool debug = false)
        {
            // TODO: Stuff...
            return -1;
        }

        public long Part2(bool debug = false)
        {
            // TODO: Stuff...
            return -1;
        }

        public override string ToString()
        {
            // TODO: Additional details?

            var sw = new StringWriter();

            sw.Write($"Model info - like sample");

            return sw.ToString();
        }
    }

    public Day12() { }

    private Model GetModel(DataType which = DataType.INPUT, int control1 = 0, bool debug = false)
    {
        // control1 - optional control parameter (sometimes parts 1 and 2 have different behaviour)
        // debug - optional debug parameter

        return new Model(which == DataType.INPUT ? Day12Data.INPUT : Day12Data.SAMPLE);
    }

    public Result Answer()
    {
        // TODO: Start with SAMPLE data
        DataType which = DataType.SAMPLE;

        var model = GetModel(which);

        var day = Utils.NumSpace(this.GetType().Name);

        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}");

        var result1 = new Result(model.Part1(), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        // TODO: Possible altenative model - or keep using above version
        model = GetModel(which, 123);
        var result2 = new Result(model.Part2(), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
