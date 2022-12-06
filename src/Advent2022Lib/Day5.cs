using System.Text.RegularExpressions;
using Sprache;

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

        public string[] Items { get; init; } = null!;
        public (int, int, int)[] Commands { get; init; } = null!;

        public Stack<char>[] Stacks { get; init; } = null!;

        public Model(string input)
        {
            Data = input;
            var lines = Data.Split(Environment.NewLine);

            var stacksIdentLineNo = lines.ToList().FindIndex(l => l == "") - 1;

            Stacks = ParseStacks(lines[stacksIdentLineNo]);

            Items = lines.Take(stacksIdentLineNo)
                .Select(l => Regex.Replace(l, @"[\[\]]", ""))
                .Select(l => Regex.Replace(l, @"\s\s\s\s", " ."))
                .ToArray();

            // Console.WriteLine($"> NumStacks={Stacks.Length}");

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

            // Convert lines back to a string as we are using a parser now
            Commands = ParseCommands(String.Join(Environment.NewLine, lines.Skip(stacksIdentLineNo + 2)));
        }

        public string[] ParseItems(string lines)
        {
            // TODO: Not sure this will do much more than the above

            return new string[] { };
        }

        public Stack<char>[] ParseStacks(string line)
        {
            var parser =
                from _s in Parse.Regex(@"\s+").Optional()
                from stacks in Parse.Number.Select(int.Parse).DelimitedBy(Parse.Regex(@"\s+")).Optional()
                from _t in Parse.Regex(@"\s+").Optional()
                select stacks.GetOrElse(new int[] { });

            return parser.Parse(line)
                    .Select(x => new Stack<char>())
                    .ToArray();
        }

        public (int, int, int)[] ParseCommands(string lines)
        {
            var command =
                from _m in Parse.String("move").Token()
                from n in Parse.Number.Token().Select(int.Parse)
                from _fr in Parse.String("from").Token()
                from fr in Parse.Number.Token().Select(int.Parse)
                from _to in Parse.String("to").Token()
                from too in Parse.Number.Token().Select(int.Parse)
                select (n, fr, too);

            // Multiple commands parser - needs a single input string
            var commands =
                from cmds in Parse.Ref(() => command).Many().End()
                select cmds;

            return commands.Parse(lines).ToArray();
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

            sw.WriteLine($"# Stacks = {Stacks.Length}");
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
        Console.WriteLine($"Day 5 - #Stacks = {model.Stacks.Length}, #Commands = {model.Commands.Length}, START={model.GetTop()}");

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
