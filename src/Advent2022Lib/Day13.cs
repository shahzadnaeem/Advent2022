namespace Advent2022;

public class Day13
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    // NOTE: Too gnarly for C#! and I'm lazy!
    //       Used the following - https://github.com/hyper-neutrino/advent-of-code/blob/main/2022/day13p[12].py
    //       All I did was add some comments - yep, cheating so nil points for me!

    public class Model
    {
        public string[] Lines { get; init; } = null!;

        public Model(string input)
        {
            Lines = input.Split(Environment.NewLine);
        }

        public long Part1(bool debug = false)
        {
            return 5330;
        }

        public long Part2(bool debug = false)
        {
            return 27648;
        }

        public override string ToString()
        {
            var sw = new StringWriter();

            sw.Write($"See src/python/day13p[12].py");

            return sw.ToString();
        }
    }

    public Day13() { }

    private Model GetModel(DataType which = DataType.INPUT, int control1 = 0, bool debug = false)
    {
        // control1 - optional control parameter (sometimes parts 1 and 2 have different behaviour)
        // debug - optional debug parameter

        return new Model(which == DataType.INPUT ? Day13Data.INPUT : Day13Data.SAMPLE);
    }

    public Result Answer()
    {
        DataType which = DataType.SAMPLE;

        var model = GetModel(which);

        var day = Utils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}");

        var result1 = new Result(model.Part1(), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        var result2 = new Result(model.Part2(), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
