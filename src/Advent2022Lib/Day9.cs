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

            return positions.Count();
        }

        // NOTE: From this! https://github.com/betaveros/advent-of-code-2022/blob/main/p9.noul

        public long TailPositionsVisitedV2()
        {
            var positions = new HashSet<(int, int)>();

            (int, int) head = (0, 0);
            var tail = head;

            positions.Add(head);

            foreach ((char D, int C) inst in Instructions)
            {
                // Console.WriteLine($"Instruction: {inst}");

                for (var i = 0; i < inst.C; i++)
                {
                    var newHead = Step(head, inst.D);

                    var delta = Delta(newHead, tail);

                    // Work out offset (delta to apply) for Tail relative to Head (which is not how I was doing this)

                    var offset = delta switch
                    {
                        (2, _) => (1, 0),
                        (-2, _) => (-1, 0),
                        (_, 2) => (0, 1),
                        (_, -2) => (0, -1),
                        _ => delta
                    };

                    tail = (newHead.Item1 + offset.Item1, newHead.Item2 + offset.Item2);
                    positions.Add(tail);

                    Console.WriteLine($"  head: {head}->{newHead}, tail: {tail}, delta: {delta}, offset: {offset}");

                    head = newHead;
                }
            }

            return positions.Count();
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

        // NOTE: Also from this! https://github.com/betaveros/advent-of-code-2022/blob/main/p9.noul

        public List<(int, int)> ApplyStepToRope(List<(int, int)> rope, (int, int) newHeadPos, int numKnots)
        {
            // Console.WriteLine($"  HEAD: {rope[0]} -> {newHeadPos}");
            // Console.WriteLine($"    ROPE:    {RopeToString(rope)}");

            var newRope = new List<(int, int)>();

            newRope.Add(newHeadPos);

            for (int i = 1; i < numKnots; i++)
            {
                var newPos = newRope[i - 1];
                var delta = Delta(newPos, rope[i]);

                // More complex offset calculation for 2nd case

                var offset = delta switch
                {
                    (2, 2) => (1, 1),
                    (2, -2) => (1, -1),
                    (-2, 2) => (-1, 1),
                    (-2, -2) => (-1, -1),
                    (2, _) => (1, 0),
                    (-2, _) => (-1, 0),
                    (_, 2) => (0, 1),
                    (_, -2) => (0, -1),
                    _ => delta
                };

                newRope.Add((newPos.Item1 + offset.Item1, newPos.Item2 + offset.Item2));
            }

            // Console.WriteLine($"    NEWROPE: {RopeToString(newRope)}");

            return newRope;
        }

        public long TailPositionsVisited(int numKnots)
        {
            int HEAD = 0;
            int TAIL = numKnots - 1;

            var positions = new HashSet<(int, int)>();

            var rope = new List<(int, int)>();
            rope.AddRange(Enumerable.Range(0, numKnots).Select(_ => (0, 0)));

            positions.Add(rope[HEAD]);

            // Console.WriteLine($"Knots = {numKnots}, Head = {rope[HEAD]}, Tail = {rope[TAIL]}");

            foreach ((char D, int C) inst in Instructions)
            {
                // Console.WriteLine($"instruction: {inst}");

                var origRope = rope.ToList();

                for (var i = 0; i < inst.C; i++)
                {
                    var newHeadPos = Step(rope[HEAD], inst.D);

                    rope = ApplyStepToRope(rope, newHeadPos, numKnots);

                    positions.Add(rope[TAIL]);
                }
            }

            return positions.Count();
        }


        public override string ToString()
        {
            var sw = new StringWriter();

            sw.Write($"{Instructions.Count()} instructions");

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
        return model.TailPositionsVisited();
    }

    private long Part2(Model model)
    {
        return model.TailPositionsVisited(10);
    }

    public (long, long) Answer()
    {
        var model = GetModel(DataType.INPUT);

        Console.WriteLine($"Day 9 - #INSTRUCTIONS = {model.Instructions.Length}");

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
