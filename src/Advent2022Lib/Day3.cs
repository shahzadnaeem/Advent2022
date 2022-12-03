namespace Advent2022;

public class Day3
{
    public class Model
    {
        private string Data { get; set; } = "";
        public (string, char)[] Rows { get; init; } = null!;
        public (string, char)[] GroupedRows { get; init; } = null!;

        public Model(string input)
        {
            Data = input;

            var lines = input.Split(Environment.NewLine);

            Rows = lines
                .Select(l =>
                {
                    var left = l.Substring(0, l.Length / 2);
                    var right = l.Substring(l.Length / 2);

                    var intersection = left.Intersect(right).First();

                    return (l, intersection);
                }).ToArray();

            var groupedRows = new List<(string, char)>();

            var groupTracker = new string[3] { "", "", "" };

            foreach (var (l, i) in lines.Select((l, i) => (l, i)))
            {
                groupTracker[i % 3] = l;

                if (i > 0 && (i + 1) % 3 == 0)
                {
                    var is1 = groupTracker[0].Intersect(groupTracker[1]);
                    var is2 = groupTracker[1].Intersect(groupTracker[2]);
                    var is3 = is1.Intersect(is2);

                    if (is3.Count() == 0)
                    {
                        throw new Exception($"No common item in three elf group! #{i / 3}");
                    }

                    var fullLine = string.Join(' ', groupTracker);

                    groupedRows.Add((fullLine, is3.First()));

                    groupTracker = groupTracker.Select(_ => "").ToArray();
                }
            }

            GroupedRows = groupedRows.ToArray();
        }

        public int GetPriority(char item)
        {
            if (Char.IsLower(item))
            {
                return item - 'a' + 1;
            }
            else
            {
                return item - 'A' + 26 + 1;
            }
        }

        public override string ToString()
        {
            StringWriter sw = new();

            sw.WriteLine("[");
            foreach (var row in Rows)
            {
                sw.Write("  ");
                sw.Write(row.ToString());
                sw.WriteLine(',');

            }
            sw.WriteLine("]");


            sw.WriteLine("");

            sw.WriteLine("[");
            foreach (var row in GroupedRows)
            {
                sw.Write("  ");
                sw.Write(row.ToString());
                sw.WriteLine(',');

            }
            sw.WriteLine("]");

            return sw.ToString();
        }
    }

    public Day3()
    {
    }

    private Model GetModel()
    {
        return new Model(Day3Data.INPUT);
    }

    private long Part1(Model model)
    {
        return model.Rows.Select(row => model.GetPriority(row.Item2)).Sum();
    }

    private long Part2(Model model)
    {
        return model.GroupedRows.Select(row => model.GetPriority(row.Item2)).Sum();
    }

    public (long, long) Answer()
    {
        var model = GetModel();

        // Console.WriteLine($"{model}");
        Console.WriteLine($"#ROWS = {model.Rows.Length}");

        // Part 1
        var result1 = (Part1(model), model.Rows.Length);

        Console.WriteLine($"Result1 = {result1}");

        // Part 2
        var result2 = (Part2(model), model.Rows.Length);

        Console.WriteLine($"Result2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
