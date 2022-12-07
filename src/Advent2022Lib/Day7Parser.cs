using System.Text.RegularExpressions;
using Sprache;

namespace Advent2022;

public class Day7Parser
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public class Entry
    {
        public string Type { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string[] Values { get; init; } = null!;

        public Entry(string type, string name, string[] values)
        {
            Type = type;
            Name = name;
            Values = values;
        }
    }


    public class Model
    {
        public string Data { get; init; } = null!;

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

    public Day7Parser()
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

    private Entry[] ParseModel(Model model)
    {
        var command_p = Parse.Char('$').Token().Then(_ => Parse.LetterOrDigit.Many().Token().Text());

        var value_p = (Parse.AnyChar.Except(Parse.WhiteSpace)).Many().Token().Text();

        var cmd_p =
            from command in command_p
            from values in value_p.Many().End().Select(items => items.ToArray())
            select new Entry("$", command, values);

        var dir_p =
            from dir in Parse.String("dir").Token().Text()
            from name in Parse.AnyChar.Many().Token().Text().Once().End()
            select new Entry("#", dir, new string[] { name.Single() });

        var file_p =
            from size in Parse.Numeric.Many().Token().Text()
            from name in Parse.AnyChar.Many().Token().Text().Once().End()
            select new Entry("F", name.Single(), new string[] { size.ToString() });

        var wildcard_p =
            from theRest in Parse.AnyChar.Many().Text().End()
            select new Entry("!", "TODO", new string[] { theRest });

        var commands_p =
            from cmd in cmd_p.Or(dir_p).Or(file_p).Or(wildcard_p)
            select cmd;

        var result = model.Lines.Select(line => commands_p.Parse(line)).ToArray();

        return result;
    }

    public void Run()
    {
        var dataType = DataType.SAMPLE;

        var model = GetModel(dataType);

        Console.WriteLine($"#LINES = {model.Lines.Length}");

        var entries = ParseModel(model);

        Console.WriteLine($"Number of parsed lines: {entries.Length}");
        var Cwd = new Stack<string>();
        var DirTotals = new Dictionary<string, long>();
        var currentDir = "";
        var prevDir = "";

        foreach (var (e, i) in entries.Select((e, i) => (e, i)))
        {
            currentDir = "/" + string.Join('/', Cwd.ToArray().Reverse().Skip(1));

            if (currentDir != prevDir)
            {
                Console.WriteLine($"CWD: {currentDir}");
                prevDir = currentDir;
            }

            if (e.Name == "cd")
            {
                var dir = e.Values.Single();

                if (dir == "/")
                {
                    Cwd.Clear();
                    Cwd.Push(dir);
                }
                else if (dir == "..")
                {
                    Cwd.Pop();
                }
                else
                {
                    Cwd.Push(dir);
                }
            }
            else if (e.Type == "F")
            {
                var bytes = long.Parse(e.Values.Single());

                if (!DirTotals.ContainsKey(currentDir))
                {
                    DirTotals.Add(currentDir, 0L);
                }

                foreach (var key in DirTotals.Keys)
                {
                    if (currentDir.StartsWith(key))
                    {
                        DirTotals[key] += bytes;
                    }
                }

                Console.WriteLine($"F: {(currentDir != "/" ? currentDir : "")}/{e.Name}  {bytes}");
                Console.WriteLine($"TOTAL: {DirTotals["/"]}");
            }
        }
    }
}
