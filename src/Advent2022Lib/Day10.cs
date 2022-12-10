namespace Advent2022;

public class Day10
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public enum OpCode
    {
        NOOP,
        ADDX
    }

    public class Model
    {
        const int DisplayWidth = 40;
        const int DisplayHeight = 6;

        private string Data { get; init; } = null!;
        public (OpCode, int, int)[] Instructions { get; init; } = null!;

        public long CycleNum { get; set; } = 1;
        public long XReg { get; set; } = 1;
        public long PrevXReg { get; set; } = 1;
        public HashSet<long> CheckCycles { get; } = new() { 20, 60, 100, 140, 180, 220 };
        public char[,] Display { get; set; } = new char[DisplayWidth, DisplayHeight];

        public Model(string input)
        {
            Data = input;

            var lines = Data.Split(Environment.NewLine);

            var instructions = new List<(OpCode, int, int)>();

            foreach (var line in lines)
            {
                var parts = line.Split(' ');

                if (parts[0] == "noop")
                {
                    instructions.Add((OpCode.NOOP, 1, 0));
                }
                else
                {
                    instructions.Add((OpCode.ADDX, 2, int.Parse(parts[1])));
                }
            }

            Instructions = instructions.ToArray();

            ClearDisplay();
        }

        public IEnumerable<(int x, int y)> DisplayRange()
        {
            var range = from y in Enumerable.Range(0, DisplayHeight)
                        from x in Enumerable.Range(0, DisplayWidth)
                        select (x, y);

            return range;
        }

        public void ClearDisplay()
        {
            var range = DisplayRange();

            foreach ((int x, int y) r in range)
            {
                Display[r.x, r.y] = '.';
            }
        }

        public void DrawPixel(long pos, char sym = '#')
        {
            Display[(pos - 1) % DisplayWidth, (pos - 1) / DisplayWidth] = sym;
        }

        public void NextCycle()
        {
            var xPos = CycleNum % DisplayWidth;

            if (xPos >= XReg && xPos < XReg + 3)
            {
                DrawPixel(CycleNum);
            }

            CycleNum++;
        }

        public string DisplayToString()
        {
            var sw = new StringWriter();

            var range = DisplayRange();

            foreach ((int x, int y) r in range)
            {
                sw.Write($"{Display[r.x, r.y]}");

                if (r.x == (DisplayWidth - 1))
                {
                    sw.WriteLine();
                }
            }

            return sw.ToString();
        }

        public long Strength(bool checkPrevToo)
        {
            if (CheckCycles.Contains(CycleNum))
            {
                return CycleNum * XReg;
            }
            else if (checkPrevToo && (CheckCycles.Contains(CycleNum - 1)))
            {
                return (CycleNum - 1) * PrevXReg;
            }
            else
            {
                return 0;
            }
        }

        public long Part1()
        {
            long strength = 0;

            foreach (var (instruction, i) in Instructions.Select((inst, i) => (inst, i)))
            {
                var (opCode, cycles, param) = instruction;

                bool checkPrevToo = false;

                if (opCode == OpCode.NOOP)
                {
                    NextCycle();
                }
                else
                {
                    PrevXReg = XReg;
                    NextCycle();
                    NextCycle();
                    XReg += param;
                    checkPrevToo = true;
                }

                strength += Strength(checkPrevToo);

                // Console.WriteLine($"Instruction# = {i}, Cycle# = {CycleNum}, X = {XReg}, Strength = {strength}");
            }

            return strength;
        }

        public override string ToString()
        {
            var sw = new StringWriter();

            sw.Write($"#Instructions = {Instructions.Length}, Cycle# = {CycleNum}, X = {XReg}");
            // sw.Write(DisplayToString());

            return sw.ToString();
        }
    }

    public Day10()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day10Data.INPUT : Day10Data.SAMPLE);
    }

    private long Part1(Model model)
    {
        return model.Part1();
    }

    private long Part2(Model model)
    {
        // All done in Part1
        return 0;
    }

    public (long, long) Answer()
    {
        // TODO: Start with SAMPLE data
        var model = GetModel(DataType.INPUT);

        Console.WriteLine($"Day 10 - #INSTRUCTIONS = {model.Instructions.Length}");

        Console.WriteLine(model);

        // Part 1
        var result1 = (Part1(model), 0);

        Console.WriteLine($"Part 1 = {result1}");

        // Part 2
        // var result2 = (Part2(model), 0);

        // Console.WriteLine($"Part 2 = {result2}");
        Console.WriteLine($"Part 2\n{model.DisplayToString()}");

        // Final result in a tuple
        return (result1.Item1, 0);
    }
}
