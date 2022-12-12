namespace Advent2022;

public class Day12
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    // TODO: Other enums/consts/classes ...
    const int NOPE = -1;
    const char START = 'S';
    const int START_HEIGHT = 1;
    const char TARGET = 'E';
    const int TARGET_HEIGHT = 26;

    public class Model
    {
        public string[] Lines { get; init; } = null!;
        public int Width { get; init; } = NOPE;
        public int Height { get; init; } = NOPE;
        // (height,visited)
        public (int, bool)[,] Map { get; init; } = null!;
        public (int, int) Start { get; init; } = (NOPE, NOPE);
        public (int, int) Target { get; init; } = (NOPE, NOPE);

        public Model(string input)
        {
            Lines = input.Split(Environment.NewLine);

            Width = Lines[0].Length;
            Height = Lines.Length;

            Map = new (int, bool)[Width, Height];

            int y = 0;
            foreach (var line in Lines)
            {
                foreach (var (c, x) in line.Select((c, x) => (c, x)))
                {
                    if (c == START)
                    {
                        Start = (x, y);
                    }
                    else if (c == TARGET)
                    {
                        Target = (x, y);
                    }

                    Map[x, y] = (ToHeight(c), false);
                }

                y++;
            }
        }

        public int ToHeight(char c)
        {
            if (c == START) return START_HEIGHT;
            if (c == TARGET) return TARGET_HEIGHT;
            return c - 'a' + 1;
        }

        public bool OnMap((int x, int y) p)
        {
            return p.x >= 0 && p.x < Width && p.y >= 0 && p.y < Height;
        }

        public bool IsVisted((int x, int y) p)
        {
            return Map[p.x, p.y].Item2;
        }

        public bool SetVisted((int x, int y) p)
        {
            return Map[p.x, p.y].Item2 = true;
        }

        public bool ClearVisted((int x, int y) p)
        {
            return Map[p.x, p.y].Item2 = false;
        }

        public int HeightAtPos((int x, int y) p)
        {
            return Map[p.x, p.y].Item1;
        }

        public List<(int, int)> NextMoves((int x, int y) p)
        {
            var options = new List<(int, int)>();

            var currHeight = HeightAtPos(p);

            foreach (var delta in new (int x, int y)[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
            {
                var opt = (p.x + delta.x, p.y + delta.y);
                if (OnMap(opt) && !IsVisted(opt))
                {
                    var diff = HeightAtPos(opt) - currHeight;
                    if (diff <= 1)
                        options.Add(opt);
                }
            }

            return options;
        }

        public void ResetMap()
        {
            foreach (var x in Enumerable.Range(0, Width))
                foreach (var y in Enumerable.Range(0, Height))
                    Map[x, y] = (Map[x, y].Item1, false);
        }

        public string MapToString()
        {
            var sw = new StringWriter();

            foreach (var y in Enumerable.Range(0, Height))
            {
                foreach (var x in Enumerable.Range(0, Width))
                {
                    var p = (x, y);

                    if (p == Start) sw.Write(START);
                    else if (p == Target) sw.Write(TARGET);
                    else
                    {
                        if (IsVisted(p))
                        {
                            sw.Write('.');
                        }
                        else
                        {
                            sw.Write((char)('a' + Map[x, y].Item1 - 1));
                        }

                    }
                }

                if (y != Height - 1) sw.WriteLine();
            }


            return sw.ToString();
        }

        // TODO: Parts 1 and 2
        public long Part1(bool debug = false)
        {
            // pos = Start
            // steps = 0

            // doSolve( pos, (steps, minSteps) ) => int (newSteps, minSteps) or NOPE

            int minSteps = Width * Height + 1;
            int solutions = 0;

            int numSet = 0;
            long ticks = 0;

            var path = new Stack<(int x, int y)>();

            (int, int) doSolve((int x, int y) pos, int steps)
            {
                SetVisted(pos);

                var nextMoves = NextMoves(pos);

                foreach (var next in nextMoves)
                {
                    if (next == Target)
                    {
                        solutions++;
                        steps++;

                        if (steps < minSteps)
                        {
                            minSteps = steps;
                        }

                        Console.WriteLine($"#{solutions}, steps={steps}, minsteps={minSteps}");

                        Console.WriteLine(MapToString());
                    }
                    else
                    {
                        doSolve(next, steps + 1);
                    }
                }
                ClearVisted(pos);

                return (solutions == 0 ? 0 : minSteps, solutions);
            }


            // ticks++;
            // if (pos == Target)
            // {
            //     solutions++;

            //     if (steps < minSteps)
            //     {
            //         minSteps = steps;
            //     }

            //     Console.WriteLine($"... positions set={numSet}, ticks={ticks}");
            //     Console.WriteLine($"#{solutions}, steps={steps}, minsteps={minSteps}");

            //     Console.WriteLine(MapToString());
            // }
            // else
            // {

            //     if (numSet % 10 == 0 || ticks % 10000 == 0)
            //     {
            //         Console.WriteLine($"... positions set={numSet}, ticks={ticks}");
            //     }

            //     var nextMoves = NextMoves(pos);

            //     foreach (var next in nextMoves)
            //     {
            //         numSet++;
            //         SetVisted(pos);
            //         doSolve(next, steps + 1);
            //         ClearVisted(pos);
            //         numSet--;
            //     }
            // }

            var result = doSolve(Start, 0);

            Console.WriteLine($"Part1: {result}");

            return result.Item1;
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

            sw.WriteLine($"#LINES = {Lines.Length}, Width = {Width}, Height = {Height}");
            sw.Write(MapToString());

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
        DataType which = DataType.SAMPLE;

        var model = GetModel(which);

        var day = Utils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}");
        Console.WriteLine($"{model}");

        var result1 = new Result(model.Part1(), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        // TODO: Possible altenative model - or keep using above version
        model = GetModel(which, 123);
        var result2 = new Result(model.Part2(), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
