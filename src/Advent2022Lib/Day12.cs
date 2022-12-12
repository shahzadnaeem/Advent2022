using System.Collections.Generic;

namespace Advent2022;

public class Day12
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    const int PART1 = 1;
    const int PART2 = 2;

    const int NOPE = -1;
    const char START = 'S';
    const int START_HEIGHT = 1;
    const char TARGET = 'E';
    const int TARGET_HEIGHT = 26;
    const char EMPTY = '.';

    public class Model
    {
        public string[] Lines { get; init; } = null!;
        public int Width { get; init; } = NOPE;
        public int Height { get; init; } = NOPE;
        // (height,visited)
        public (int, char)[,] Map { get; init; } = null!;
        public (int, int) Start { get; init; } = (NOPE, NOPE);
        public (int, int) Target { get; init; } = (NOPE, NOPE);

        public Model(string input)
        {
            Lines = input.Split(Environment.NewLine);

            Width = Lines[0].Length;
            Height = Lines.Length;

            Map = new (int, char)[Width, Height];

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

                    Map[x, y] = (ToHeight(c), EMPTY);
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
            return Map[p.x, p.y].Item2 != EMPTY;
        }

        public void SetVisted((int x, int y) p, char d)
        {
            Map[p.x, p.y].Item2 = d;
        }

        public void ClearVisted((int x, int y) p)
        {
            Map[p.x, p.y].Item2 = EMPTY;
        }

        public int HeightAtPos((int x, int y) p)
        {
            return Map[p.x, p.y].Item1;
        }

        public List<((int, int), char)> NextMoves((int x, int y) p)
        {
            var options = new List<((int, int), char)>();

            var currHeight = HeightAtPos(p);

            foreach (var delta in new (int x, int y, char d)[] { (0, 1, 'V'), (0, -1, '^'), (1, 0, '<'), (-1, 0, '>') })
            {
                var opt = (p.x + delta.x, p.y + delta.y);
                if (OnMap(opt) && !IsVisted(opt))
                {
                    var diff = HeightAtPos(opt) - currHeight;
                    if (diff <= 1)
                        options.Add((opt, delta.d));
                }
            }

            return options;
        }

        public List<(int x, int y)> AllPositions()
        {
            var positions = new List<(int x, int y)>();

            foreach (var x in Enumerable.Range(0, Width))
                foreach (var y in Enumerable.Range(0, Height))
                    positions.Add((x, y));

            return positions;
        }

        public void Reset()
        {
            AllPositions().ForEach(pos => ClearVisted(pos));
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
                            // sw.Write(Map[p.x, p.y].Item2);
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

        public long Solve(int part = PART1, bool debug = false)
        {
            Reset();

            var queue = new Queue<(((int x, int y), char), int st)>();

            if (part == PART1)
            {
                queue.Enqueue(((Start, START), 0));
            }
            else
            {
                AllPositions().ForEach(pos =>
                {
                    if (HeightAtPos(pos) == 1)
                        queue.Enqueue(((pos, EMPTY), 0));
                });
            }

            int doSolve()
            {
                int result = -1;

                while (queue.Count > 0)
                {
                    var ((pos, d), st) = queue.Dequeue();

                    if (!IsVisted(pos))
                    {
                        SetVisted(pos, d);
                        if (pos == Target)
                        {
                            result = st;
                            break;
                        }
                        else
                        {
                            var nextMoves = NextMoves(pos);
                            foreach (var next in NextMoves(pos))
                                queue.Enqueue((next, st + 1));
                        }
                    }
                }

                return result;
            }

            var result = doSolve();

            if (debug)
            {
                Console.WriteLine($"Part1: {result}");
                Console.WriteLine(MapToString());
            }

            return result;
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

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day12Data.INPUT : Day12Data.SAMPLE);
    }

    public Result Answer()
    {
        DataType which = DataType.SAMPLE;

        var model = GetModel(which);

        var day = Utils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}");
        // Console.WriteLine($"{model}");

        var result1 = new Result(model.Solve(PART1), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        var result2 = new Result(model.Solve(PART2), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
