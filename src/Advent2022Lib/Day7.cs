using System.Text.RegularExpressions;

namespace Advent2022;

public class Day7
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public class Model
    {
        private string Data { get; set; } = "";

        public string[] Lines { get; init; } = null!;

        public Model(string input)
        {
            Data = input;
            Lines = Data.Split(Environment.NewLine);
        }

        public override string ToString()
        {
            return Data;
        }
    }

    public Day7()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day7Data.INPUT : Day7Data.SAMPLE);
    }

    private string ToPath(List<string> location)
    {
        return "/" + string.Join('/', location.Skip(1));
    }

    private Dictionary<string, long> ProcessInput(Model model, bool debug = false)
    {
        var rawCounts = new Dictionary<string, long>();
        var dirs = new List<string>();
        var result = new Dictionary<string, long>();

        var location = new List<string>();
        var cdRx = new Regex(@"^\$\s+cd\s+(?<dir>[\w/\.]+)$");
        var lsRx = new Regex(@"^\$\s+ls$");
        var dirRx = new Regex(@"^dir\s+(?<dir>\w+)$");
        var fileRx = new Regex(@"^(?<bytes>\d+)\s+(?<file>[\w.]+)$");

        foreach (var (line, i) in model.Lines.Select((l, i) => (l, i)))
        {
            if (cdRx.IsMatch(line))
            {
                var matches = cdRx.Match(line);
                var dir = matches.Groups[1].Captures[0].ToString();

                if (dir == "..")
                {
                    location.RemoveAt(location.Count - 1);
                }
                else
                {
                    location.Add(dir);
                    var path = ToPath(location);
                    if (!rawCounts.ContainsKey(path))
                    {
                        rawCounts.Add(path, 0);
                        dirs.Add(path);
                    }
                }

                if (debug) Console.WriteLine($"# cd {dir} - {ToPath(location)}");
            }
            else if (lsRx.IsMatch(line))
            {
                if (debug) Console.WriteLine($"# ls");
            }
            else if (dirRx.IsMatch(line))
            {
                var matches = dirRx.Match(line);
                var dir = matches.Groups[1].Captures[0].ToString();

                if (debug) Console.WriteLine($"# dir {dir}");
            }
            else if (fileRx.IsMatch(line))
            {
                var matches = fileRx.Match(line);
                var bytes = matches.Groups[1].Captures[0].ToString();
                var file = matches.Groups[2].Captures[0].ToString();

                var path = ToPath(location);
                var newBytes = rawCounts[path] + long.Parse(bytes);

                rawCounts[path] = newBytes;


                if (debug) Console.WriteLine($"# {bytes} {file}");
            }
            else
            {
                Console.WriteLine($"ERROR {i}: {line}");
            }
        }

        dirs.Sort();
        dirs = dirs.Distinct().ToList();

        foreach (var dir in dirs)
        {
            if (debug) Console.WriteLine($"{dir}");

            var keys = rawCounts.Keys.Where(k => k.StartsWith(dir));

            var sum = 0L;

            foreach (var key in keys)
            {
                sum += rawCounts[key];
            }

            result.Add(dir, sum);
        }

        return result;
    }

    private long Part1(Model model, bool debug = false)
    {
        var results = ProcessInput(model);

        foreach (var (k, v) in results)
        {
            if (debug) Console.WriteLine($"{k}: {v} bytes");
        }

        long sum = results.Where(kvp => kvp.Value < 100000).Select(kvp => kvp.Value).Sum();

        return sum;
    }

    private long Part2(Model model, DataType dataType)
    {
        var results = ProcessInput(model);

        var currFreeSpace = 70000000L - results["/"];
        var minRequired = 30000000 - currFreeSpace;

        var limit = dataType == DataType.INPUT ? minRequired : 100000L;

        var result = results
            .Where(kvp => kvp.Value >= limit)
            .Select(kvp => kvp.Value)
            .OrderBy(v => v)
            .Take(1)
            .Single();

        return result;
    }

    public (long, long) Answer()
    {
        var dataType = DataType.INPUT;

        var model = GetModel(dataType);

        Console.WriteLine($"Day 7 - #LINES = {model.Lines.Length}");

        // Part 1
        var result1 = (Part1(model), 0);

        Console.WriteLine($"Part 1 = {result1}");

        // Part 2
        var result2 = (Part2(model, dataType), 0);

        Console.WriteLine($"Part 2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
