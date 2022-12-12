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

        public List<((int, int), (int, int), char)> NextMoves((int x, int y) p)
        {
            var options = new List<((int, int), (int, int), char)>();

            var currHeight = HeightAtPos(p);

            foreach (var delta in new (int x, int y, char d)[] { (0, 1, 'V'), (0, -1, '^'), (1, 0, '>'), (-1, 0, '<') })
            {
                var opt = (p.x + delta.x, p.y + delta.y);
                if (OnMap(opt) && !IsVisted(opt))
                {
                    var diff = HeightAtPos(opt) - currHeight;
                    if (diff <= 1)
                        options.Add((p, opt, delta.d));
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

        public string MapToString(bool final = false)
        {
            var sw = new StringWriter();

            bool startInCol1 = Start.Item1 == 0;

            foreach (var y in Enumerable.Range(0, Height))
            {
                // if (startInCol1)
                // {
                //     if (y == Start.Item2)
                //         sw.Write('S');
                //     else
                //         sw.Write(' ');
                // }

                foreach (var x in Enumerable.Range(0, Width))
                {
                    var p = (x, y);

                    if (p == Target)
                    {
                        sw.Write(' ');
                        sw.Write(TARGET);
                    }
                    else if (p == Start)
                    {
                        sw.Write(START);
                        sw.Write(Map[p.x, p.y].Item2);
                    }
                    else
                    {
                        if (!final || true)
                            sw.Write((char)('a' + Map[x, y].Item1 - 1));
                        else
                            sw.Write('.');

                        if (IsVisted(p))
                        {
                            sw.Write(Map[p.x, p.y].Item2);
                        }
                        else
                        {
                            sw.Write('.');
                        }
                    }
                    sw.Write('|');
                }

                if (y != Height - 1)
                {
                    sw.WriteLine();
                    foreach (var x in Enumerable.Range(0, Width))
                        sw.Write("---");
                    sw.WriteLine();
                }
            }


            return sw.ToString();
        }

        public long SolveByPath(int part = PART1, bool debug = false)
        {
            Reset();

            // NOTE: Creates the correct path(s) for small problem spaces
            //       Failed to give a solution for the puzzle input

            int minSteps = Width * Height + 1;
            int solutions = 0;

            int numSet = 0;
            long ticks = 0;

            var path = new Stack<(int x, int y)>();

            (int, int) doSolve((int x, int y) pos, int steps)
            {
                ticks++;
                if (pos == Target)
                {
                    solutions++;

                    if (steps < minSteps)
                    {
                        minSteps = steps;
                    }

                    // Console.WriteLine($"... positions set={numSet}, ticks={ticks}");
                    Console.WriteLine($"\n#{solutions}, steps={steps}, minsteps={minSteps}\n");

                    Console.WriteLine(MapToString(true));
                }
                else
                {

                    if (numSet % 10 == 0 || ticks % 10000 == 0)
                    {
                        // Console.WriteLine($"... positions set={numSet}, ticks={ticks}");
                    }

                    var nextMoves = NextMoves(pos);

                    foreach (var next in nextMoves)
                    {
                        numSet++;
                        SetVisted(next.Item1, next.Item3);
                        doSolve(next.Item2, steps + 1);
                        ClearVisted(next.Item1);
                        numSet--;
                    }
                }

                return (solutions == 0 ? 0 : minSteps, solutions);
            }
            var result = doSolve(Start, 0);

            Console.WriteLine($"Part {part}: {result}");

            return result.Item1;
        }

        public long Solve(int part = PART1, bool debug = false)
        {
            // Breadth fist search - correct answer but no path.

            Reset();

            var queue = new Queue<(((int x, int y), (int x, int y), char), int st)>();

            if (part == PART1)
            {
                queue.Enqueue(((Start, Start, START), 0));
            }
            else
            {
                AllPositions().ForEach(pos =>
                {
                    if (HeightAtPos(pos) == 1)
                        queue.Enqueue(((pos, pos, EMPTY), 0));
                });
            }

            int doSolve()
            {
                int result = -1;

                while (queue.Count > 0)
                {
                    var ((prev, pos, d), st) = queue.Dequeue();

                    if (!IsVisted(pos))
                    {
                        // TODO: Set where we came from to the direction - IS THIS CORRECT???
                        SetVisted(prev, d);

                        if (pos == Target)
                        {
                            result = st;
                            break;
                        }
                        else
                        {
                            // TODO: A current marker
                            SetVisted(pos, '#');

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
                Console.WriteLine($"\nPart {part}: {result}");
                Console.WriteLine(MapToString(true));
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
        DataType which = DataType.INPUT;

        var model = GetModel(which);

        var day = Utils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}");
        // Console.WriteLine($"{model}");

        // var result = new Result(model.SolveByPath(PART1, true), model.Lines.Length);
        // Console.WriteLine($"Part 1 = {result}");

        var result1 = new Result(model.Solve(PART1, false), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        var result2 = new Result(model.Solve(PART2, false), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
