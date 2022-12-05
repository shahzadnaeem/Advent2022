using System.Text.RegularExpressions;

namespace Advent2022;

public class Day5
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
        public string[] Items { get; init; } = null!;
        public (int, int, int)[] Commands { get; init; } = null!;

        public int NumStacks { get; init; } = 0!;
        public Stack<char>[] Stacks { get; init; } = null!;

        public Model(string input)
        {
            Data = input;
            var lines = Data.Split(Environment.NewLine);

            var crateIdentLineNo = lines.ToList().FindIndex(l => l.Contains(" 1"));
            var splitter = new Regex(@"\s+");
            NumStacks = splitter.Split(lines[crateIdentLineNo].Trim()).Length;

            Items = lines.Take(crateIdentLineNo)
                .Select(l => Regex.Replace(l, @"[\[\]]", ""))
                .Select(l => Regex.Replace(l, @"\s\s\s\s", " ."))
                .ToArray();

            // Console.WriteLine($"> NumStacks={NumStacks}");

            // Create the required number of stacks
            Stacks = new Stack<char>[NumStacks]
                .Select(x => new Stack<char>())
                .ToArray();

            foreach (var (row, i) in Items.Reverse().Select((it, ix) => (it, ix)))
            {
                var splitRow = row.Split(' ').Select((it, ix) => (it, ix));

                // Console.WriteLine($"> Row='{row}'");

                foreach (var (it, sx) in splitRow)
                {
                    if (it != ".")
                    {
                        Stacks[sx].Push(it[0]);
                    }
                }
            }

            Commands = lines.Skip(crateIdentLineNo + 2)
                .Select(l => Regex.Replace(l, @"(move|from|to)\s", ""))
                .Select(l => l.Split(' '))
                .Select(xs => (int.Parse(xs[0]), int.Parse(xs[1]), int.Parse(xs[2])))
                .ToArray();
        }

        public string GetTop()
        {
            var top = Stacks.Select(st =>
            {
                if (st.Count != 0)
                {
                    return st.Peek();
                }
                else
                {
                    return '.';
                }
            }).ToArray();

            return String.Join("", top);
        }


        public override string ToString()
        {
            StringWriter sw = new();

            sw.WriteLine($"# Stacks = {NumStacks}");
            sw.WriteLine("Items");
            foreach (var line in Items)
            {
                sw.WriteLine(line);
            }

            sw.WriteLine($"# Commands = {Commands.Length}");
            sw.WriteLine("Commands");
            foreach (var line in Commands)
            {
                sw.WriteLine(line);
            }

            sw.WriteLine($"TOP = {GetTop()}");

            return sw.ToString();

        }
    }

    public Day5()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day5Data.INPUT : Day5Data.SAMPLE);
    }

    private string Part1(Model model)
    {
        foreach (var cmd in model.Commands)
        {
            for (var i = 0; i < cmd.Item1; i++)
            {
                model.Stacks[cmd.Item3 - 1].Push(model.Stacks[cmd.Item2 - 1].Pop());
            }
        }

        return model.GetTop();
    }

    private string Part2(Model model)
    {
        foreach (var cmd in model.Commands)
        {
            var poppedItems = new List<char>();

            for (var i = 0; i < cmd.Item1; i++)
            {
                var item = model.Stacks[cmd.Item2 - 1].Pop();
                poppedItems.Add(item);
            }

            poppedItems.Reverse();

            foreach (var item in poppedItems)
            {
                model.Stacks[cmd.Item3 - 1].Push(item);
            }
        }

        return model.GetTop();
    }

    public (string, string) Answer()
    {
        var model = GetModel(DataType.INPUT);

        // Console.WriteLine(model);
        Console.WriteLine($"Day 5 - #Stacks = {model.NumStacks}, #Commands = {model.Commands.Length}, START={model.GetTop()}");

        // Part 1
        var result1 = (Part1(model), 0);

        Console.WriteLine($"Part 1 = {result1}");

        // Part 2
        model = GetModel(DataType.INPUT);
        var result2 = (Part2(model), 0);

        Console.WriteLine($"Part 2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
