namespace Advent2022;

public class Day11
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    const int SELF_PARAM = 0;
    const long PART1_ROUNDS = 20;
    const long PART2_ROUNDS = 10000;

    public class Monkey
    {
        public int Id { get; init; }
        public Queue<long> Items { get; init; } = null!;
        public long InspectionCount { get; set; }
        private Func<long, (long, bool)> Operation { get; init; } = null!;
        public Monkey TrueMonkey { get; set; } = null!;
        public Monkey FalseMonkey { get; set; } = null!;

        public Monkey(int id, Queue<long> items, char op, long param, long mod, long div, long mod2)
        {
            Id = id;
            Items = items;
            InspectionCount = 0;
            Operation = MakeOperation(op, param, mod, div, mod2);
        }

        public void AddItem(long item)
        {
            Items.Enqueue(item);
        }

        public void ProcessItems()
        {
            while (Items.Count > 0)
            {
                InspectionCount++;

                var item = Items.Dequeue();

                (long val, bool ok) result = Operation(item);

                if (result.ok)
                    TrueMonkey.AddItem(result.val);
                else
                    FalseMonkey.AddItem(result.val);
            }
        }

        public Func<long, (long, bool)> MakeOperation(char op, long param, long mod, long div, long mod2)
        {
            // Capture the operation in a function
            //   Deals with the type and whether to apply with a parameter or to self

            Func<long, long> opFn;

            if (op == '+')
                opFn = (long i) => param != 0 ? i + param : i + i;
            else if (op == '*')
                opFn = (long i) => param != 0 ? i * param : i * i;
            else
            {
                throw new Exception($"Unknown op '{op}'");
            }

            // We pass in a divisor so that we can deal with the /3 or not (/1) cases
            // We always keep the result modulo mod2 - to stop overflows but still be able to test

            return (long item) =>
            {
                var val = (opFn(item) / div) % mod2;
                return ((val, val % mod == 0));
            };
        }

        public override string ToString()
        {
            var sw = new StringWriter();

            sw.Write($"Monkey {Id}: Inspections={InspectionCount}, Items=");
            sw.Write(string.Join(", ", Items.Select(i => i).ToArray()));

            return sw.ToString();
        }

    }

    public class Model
    {
        private long Rounds { get; init; }

        public List<Monkey> Monkeys { get; init; } = null!;

        public Model(string input, long rounds)
        {
            Rounds = rounds;

            Monkeys = new List<Monkey>();

            // Parsing time!
            var lines = input.Split(Environment.NewLine);

            var digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var ops = new char[] { '+', '*' };


            long div = rounds == PART2_ROUNDS ? 1 : 3;

            // NOTE: HOW TO STOP OVERFLOWS - w/o using BigInteger
            // Calculate the product of all the Test: divisors
            // Do this to keep the result of the operation modulo this value
            // Will prevent the answer from overflowing but still be correct
            //   We throw away any multiples as that will not affect the final test and what we do with items
            var mod2 = 1L;

            foreach (var line in lines)
            {
                if (line.Contains("Test"))
                {
                    var b = line.IndexOfAny(digits);
                    mod2 *= long.Parse(line.Substring(b));
                }
            }

            // Console.WriteLine($"# mod2 = {mod2}");

            int monkeyId = 0;
            var items = new Queue<long>();
            int trueMonkeyId = 0;
            int falseMonkeyId = 0;
            char op = '!';
            int param = SELF_PARAM;
            int mod = 0;

            var monkeyTargets = new List<(int m, int t, int f)>();

            foreach (var line in lines)
            {
                if (line == "")
                {
                    monkeyTargets.Add((monkeyId, trueMonkeyId, falseMonkeyId));

                    Monkeys.Add(new Monkey(monkeyId, items, op, param, mod, div, mod2));

                    // Next monkey
                    monkeyId++;
                    items = new Queue<long>();
                    trueMonkeyId = 0;
                    falseMonkeyId = 0;
                    op = '!';
                    param = SELF_PARAM;
                    mod = 0;
                }
                else if (line.Contains("Monkey"))
                {
                    var b = line.IndexOfAny(digits);
                    var e = line.IndexOf(':');
                    monkeyId = int.Parse(line.Substring(b, e - b));
                }
                else if (line.Contains("Starting items"))
                {
                    var b = line.IndexOfAny(digits);
                    var items_s = line.Substring(b);

                    var its = items_s.Split(", ");
                    foreach (var it in its)
                    {
                        items.Enqueue(int.Parse(it));
                    }
                }
                else if (line.Contains("Operation"))
                {
                    var b = line.IndexOfAny(ops);
                    op = line[b];

                    b = line.IndexOfAny(digits);
                    if (b != -1)
                        param = int.Parse(line.Substring(b));
                    else
                        param = SELF_PARAM;
                }
                else if (line.Contains("Test"))
                {
                    var b = line.IndexOfAny(digits);
                    mod = int.Parse(line.Substring(b));
                }
                else if (line.Contains("If true"))
                {
                    var b = line.IndexOfAny(digits);
                    trueMonkeyId = int.Parse(line.Substring(b));

                }
                else if (line.Contains("If false"))
                {
                    var b = line.IndexOfAny(digits);
                    falseMonkeyId = int.Parse(line.Substring(b));
                }
                else
                {
                    throw new Exception("Invalid line!");
                }
            }

            // Add the final monkey
            monkeyTargets.Add((monkeyId, trueMonkeyId, falseMonkeyId));

            Monkeys.Add(new Monkey(monkeyId, items, op, param, mod, div, mod2));

            // Join up the monkeys
            foreach ((int m, int t, int f) mt in monkeyTargets)
            {
                Monkeys[mt.m].TrueMonkey = Monkeys[mt.t];
                Monkeys[mt.m].FalseMonkey = Monkeys[mt.f];
            }

        }

        public long Part1and2(long numRounds, bool debug = false)
        {
            if (debug)
            {
                Console.WriteLine($"\nStarting {numRounds} rounds");
                Console.WriteLine($"{this}");
            }


            for (int i = 1; i <= numRounds; i++)
            {
                foreach (var monkey in Monkeys)
                {
                    monkey.ProcessItems();
                }

                if (debug)
                {
                    if (i <= 20 || i % 1000 == 0 || i == numRounds)
                    {
                        Console.WriteLine($"Round {i}");
                        Console.WriteLine(this);
                    }
                }
            }

            var top2 = Monkeys.Select(m => m.InspectionCount).OrderByDescending(i => i).Take(2).ToArray();

            long score = top2.Aggregate((long acc, long v) => acc * v);

            return score;
        }

        public override string ToString()
        {
            var sw = new StringWriter();

            foreach (var monkey in Monkeys)
            {
                sw.Write($"{monkey}");
                sw.WriteLine();
            }

            return sw.ToString();
        }
    }

    public Day11() { }

    private Model GetModel(DataType which = DataType.SAMPLE, long rounds = 20)
    {
        return new Model(which == DataType.INPUT ? Day11Data.INPUT : Day11Data.SAMPLE, rounds);
    }

    public (long, long) Answer()
    {
        DataType which = DataType.INPUT;
        var model = GetModel(which);

        Console.WriteLine($"Day 11 - #MONKEYS = {model.Monkeys.Count}");

        var result1 = (model.Part1and2(PART1_ROUNDS), model.Monkeys.Count);
        Console.WriteLine($"Part 1 = {result1}");

        model = GetModel(which, PART2_ROUNDS);
        var result2 = (model.Part1and2(PART2_ROUNDS), model.Monkeys.Count);
        Console.WriteLine($"Part 2 = {result2}");

        return (result1.Item1, result2.Item1);
    }
}
