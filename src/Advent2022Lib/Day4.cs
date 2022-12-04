namespace Advent2022;

public class Day4
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
        public ((int, int), (int, int))[] Rows { get; init; } = null!;

        public Model(string input)
        {
            Data = input;

            Rows = Data.Split(Environment.NewLine)
            .Select(line =>
            {
                var pairs = line.Split(',');
                return (ParseRange(pairs[0]), ParseRange(pairs[1]));
            })
            .ToArray();

            // TODO: Preprocessing for Part1 and Part2 below
        }

        private (int, int) ParseRange(string range)
        {
            var digits = range.Split('-')
            .Select(n => int.Parse(n))
            .ToArray();

            return (digits[0], digits[1]);
        }

        // TODO: Helper/calculation methods as required
        public long LookupSomething()
        {
            // TODO: Stuff...
            return -1;
        }


        public override string ToString()
        {
            StringWriter sw = new();

            sw.WriteLine("[");
            foreach (var row in Rows)
            {
                sw.Write("  (");
                sw.Write(row.Item1.ToString());
                sw.Write(',');
                sw.Write(row.Item2.ToString());
                sw.WriteLine(')');
            }
            sw.WriteLine("]");

            return sw.ToString();
        }
    }

    public Day4()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day4Data.INPUT : Day4Data.SAMPLE);
    }

    private int Containment(((int, int), (int, int)) it)
    {
        var firstContained = it.Item2.Item1 <= it.Item1.Item1 && it.Item2.Item2 >= it.Item1.Item2;
        var secondContained = it.Item1.Item1 <= it.Item2.Item1 && it.Item1.Item2 >= it.Item2.Item2;

        if (firstContained || secondContained)
        {
            return 1;
        }

        return 0;
    }

    private int Overlap(((int, int), (int, int)) it)
    {
        var before = (it.Item1.Item2 < it.Item2.Item1);
        var after = (it.Item1.Item1 > it.Item2.Item2);

        if (!before && !after)
        {
            return 1;
        }

        return 0;
    }

    private long Part1(Model model)
    {
        return model.Rows.Select(row => Containment(row)).Sum();
    }

    private long Part2(Model model)
    {
        return model.Rows.Select(row => Overlap(row)).Sum();
    }

    public (long, long) Answer()
    {
        var model = GetModel(DataType.INPUT);

        // Console.WriteLine($"{model}");
        Console.WriteLine($"Day 4 - #ROWS = {model.Rows.Length}");

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
