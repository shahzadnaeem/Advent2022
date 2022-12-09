namespace Advent2022;

public class Day9
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    const char UP = 'U';
    const char DOWN = 'D';
    const char RIGHT = 'R';
    const char LEFT = 'L';

    public class Model
    {
        private string Data { get; set; } = "";

        public (char, int)[] Instructions { get; init; } = null!;

        public Model(string input)
        {
            Data = input;
            var lines = Data.Split(Environment.NewLine);

            var instructions = new List<(char, int)>();

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                instructions.Add((parts[0][0], int.Parse(parts[1])));
            }

            Instructions = instructions.ToArray();
        }

        private (int, int) Delta((int X, int Y) p1, (int X, int Y) p2)
        {
            return (p2.X - p1.X, p2.Y - p1.Y);

        }

        private double Distance((int X, int Y) p1, (int X, int Y) p2)
        {
            (int X, int Y) d = Delta(p1, p2);

            return Math.Sqrt(d.X * d.X + d.Y * d.Y);
        }

        private (int, int) Step((int X, int Y) pos, char dir)
        {
            if (dir == UP) return (pos.X, pos.Y + 1);
            if (dir == DOWN) return (pos.X, pos.Y - 1);
            if (dir == LEFT) return (pos.X - 1, pos.Y);
            if (dir == RIGHT) return (pos.X + 1, pos.Y);

            throw new ArgumentException($"Bad direction: {dir}");
        }

        // NOTE: No longer needed - Part1 solution
        public long TailPositionsVisited()
        {
            var positions = new HashSet<(int, int)>();

            (int, int) head = (0, 0);
            var tail = head;

            positions.Add(head);

            foreach ((char D, int C) inst in Instructions)
            {
                for (var i = 0; i < inst.C; i++)
                {
                    var newHead = Step(head, inst.D);
                    var dist = Distance(newHead, tail);

                    // Console.WriteLine($"  head: {head}->{newHead}, tail: {tail}: dist: {dist}");

                    if (dist >= 2)
                    {
                        // Tail needs to catch up
                        tail = head;
                        positions.Add(tail);
                    }

                    head = newHead;
                }
            }

            return positions.Count;
        }

        private string RopeToString(List<(int, int)> rope)
        {
            var sw = new StringWriter();

            sw.Write("[ ");
            foreach (var knot in rope)
            {
                sw.Write($"({knot.Item1},{knot.Item2}), ");
            }
            sw.Write(" ]");

            return sw.ToString();
        }

        private char PosToSym(int i, int count)
        {
            char sym;

            if (i == 0)
            {
                sym = 'H';
            }
            else
            {
                if (i != count - 1)
                {
                    sym = (char)((int)'1' + i);
                }
                else
                {
                    sym = 'T';
                }
            }

            return sym;
        }

        //  TODO: Hard coded dimensions. Need to calculate by using processing instructions
        private string RopeToBoardString(List<(int, int)> rope, List<int> diagMoveKnots, bool large = false)
        {
            var sw = new StringWriter();

            // var minX = rope.Min(kn => kn.Item1);
            // var maxX = rope.Max(kn => kn.Item1);

            // var minY = rope.Min(kn => kn.Item2);
            // var maxY = rope.Max(kn => kn.Item2);

            // var boundsX = (minX, maxX);
            // var boundsY = (minY, maxY);

            // var width = maxX - minX + 1;
            // var height = maxY - minY + 1;

            // var xo = 11; //(int)((28 - width) / 2) - 1;
            // var yo = 5; //(int)((21 - height) / 2) - 1;

            // NOTE: Values that work for the SAMPLE data
            var width = 26;
            var height = 21;
            var xo = 11;
            var yo = 5;

            if (large)
            {
                // TODO: This is not big enough! - even 4x isn't
                width = 100;
                height = 200;
                xo = 30;
                yo = 20;
            }

            // Console.WriteLine($"width = {width}, height = {height}");

            var board = new char[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (y > 0 && y % 5 == 0)
                    {
                        board[x, y] = '‥';
                    }
                    else if (x > 0 && x % 10 == 0)
                    {
                        board[x, y] = '⸽';
                    }
                    else
                    {
                        board[x, y] = '·';
                    }
                }
            }

            board[xo, yo] = '⨂';

            foreach (((int x, int y), int i) in rope.Select((k, i) => (k, i)).Reverse())
            {
                char sym = PosToSym(i, rope.Count);

                var xi = x + xo;
                var yi = y + yo;

                board[xi, yi] = sym;
            }

            var knots = diagMoveKnots.Select(k => PosToSym(k, rope.Count));
            Console.WriteLine($"Diagonal moves: {String.Join(' ', knots)}");

            // Output the board
            for (var y = height - 1; y >= 0; y--)
            {
                for (var x = 0; x < width; x++)
                {
                    sw.Write(board[x, y]);
                }
                sw.WriteLine();
            }

            return sw.ToString();
        }

        // NOTE: Had a look at this - https://github.com/betaveros/advent-of-code-2022/blob/main/p9.noul

        public long TailPositionsVisited(int numKnots, bool debug = false)
        {
            int HEAD = 0;
            int TAIL = numKnots - 1;

            var positions = new HashSet<(int, int)>();

            var rope = new List<(int, int)>();
            rope.AddRange(Enumerable.Range(0, numKnots).Select(_ => (0, 0)));

            positions.Add(rope[HEAD]);

            if (debug)
                Console.WriteLine($"Knots = {numKnots}, Head = {rope[HEAD]}, Tail = {rope[TAIL]}");

            var iNum = 0;
            var largeBoard = Instructions.Length > 100;

            foreach ((char D, int C) inst in Instructions)
            {
                iNum++;

                for (var i = 0; i < inst.C; i++)
                {
                    // Update head and then the remaining knots
                    rope[HEAD] = Step(rope[HEAD], inst.D);

                    var diagMovesKnots = new List<int>();

                    for (int k = 1; k < numKnots; k++)
                    {
                        var prevKnotPos = rope[k - 1];

                        var delta = Delta(prevKnotPos, rope[k]);

                        // Additional offset calculation cases for diagonal movement
                        var offset = delta switch
                        {
                            // Extra cases for three or more knots
                            (2, 2) => (1, 1),
                            (2, -2) => (1, -1),
                            (-2, 2) => (-1, 1),
                            (-2, -2) => (-1, -1),

                            // Two knots cases
                            (2, _) => (1, 0),
                            (-2, _) => (-1, 0),
                            (_, 2) => (0, 1),
                            (_, -2) => (0, -1),

                            // All other cases mean NO change (undo updated prevKnotPos)
                            _ => delta
                        };

                        var newK = (prevKnotPos.Item1 + offset.Item1, prevKnotPos.Item2 + offset.Item2);

                        if (Math.Abs(delta.Item1) + Math.Abs(delta.Item2) == 4)
                        {
                            diagMovesKnots.Add(k);
                            if (debug)
                                Console.WriteLine($"⇧{k + 1}:{rope[k]} - ⇩{k}:{prevKnotPos} -> ẟ:{offset} => ⇩{k}{newK}");
                        }

                        rope[k] = newK;
                    }

                    positions.Add(rope[TAIL]);

                    if (debug)
                    {
                        if (diagMovesKnots.Count == 0)
                        {
                            Console.Clear();
                        }

                        if (diagMovesKnots.Count > 0)
                            Console.WriteLine();

                        Console.WriteLine($"Instruction: {inst.D} {i + 1}/{inst.C}  [{iNum}/{Instructions.Length}]");
                        Console.WriteLine($"{RopeToBoardString(rope, diagMovesKnots, largeBoard)}");
                        if (diagMovesKnots.Count > 0)
                        {
                            // Console.WriteLine("Press a key to continue");
                            // var ch = Console.ReadKey();
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(50);
                    }
                }
            }

            return positions.Count;
        }


        public override string ToString()
        {
            var sw = new StringWriter();

            sw.Write($"{Instructions.Length} instructions");

            return sw.ToString();
        }
    }

    public Day9()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day9Data.INPUT : Day9Data.SAMPLE);
    }

    private long Part1(Model model)
    {
        return model.TailPositionsVisited(2);
    }

    private long Part2(Model model, bool debug = false)
    {
        return model.TailPositionsVisited(10, debug);
    }

    public (long, long) Answer()
    {
        var model = GetModel(DataType.INPUT);

        Console.WriteLine($"Day 9 - #INSTRUCTIONS = {model.Instructions.Length}");

        // Part 1
        var result1 = (Part1(model), 0);

        Console.WriteLine($"Part 1 = {result1}");

        // Part 2
        // NOTE: Add true 2nd parameter to Part to see visualisation
        //         works only with the following above: var model = GetModel(DataType.SAMPLE)
        var result2 = (Part2(model), 0);

        Console.WriteLine($"Part 2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
