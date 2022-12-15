using System.Text.RegularExpressions;

namespace Advent2022;

public class Day15
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    const int PART_1 = 1;
    const int PART_2 = 2;

    public record Pos(int X, int Y);

    public record Beacon(Pos Pos);
    public record Sensor(Pos Pos, Beacon NearestBeacon, int MinDist);

    public class Model
    {
        public string[] Lines { get; init; } = null!;
        public Beacon[] Beacons { get; init; } = null!;
        public Sensor[] Sensors { get; init; } = null!;
        public int MinX { get; init; } = 0;
        public int MaxX { get; init; } = 0;
        public int MinY { get; init; } = 0;
        public int MaxY { get; init; } = 0;
        public int Width { get; init; } = 0;
        public int Height { get; init; } = 0;

        public Model(string input)
        {
            Lines = input.Split(Environment.NewLine);

            var beacons = new HashSet<Beacon>();
            var sensors = new HashSet<Sensor>();

            // Parse ...

            var r = new Regex(@"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");

            Lines.ToList().ForEach(line =>
            {
                var regexed = r.Replace(line, @"$1,$2 $3,$4");

                // Console.WriteLine($"#  {line}\n#  {regexed}");

                if (!regexed.Contains("Sensor"))
                {
                    var posPair = Read2Pos(regexed);

                    var beacon = new Beacon(posPair.Item2);
                    var sensor = new Sensor(posPair.Item1, beacon, ManhattanDist(posPair.Item1, posPair.Item2));

                    beacons.Add(beacon);
                    sensors.Add(sensor);
                }
                else
                {
                    Console.WriteLine($"BAD LINE: {line}");
                }
            });

            Beacons = beacons.ToArray();
            Sensors = sensors.ToArray();

            MaxX = Beacons.Select((b) => (b.Pos.X)).Concat(Sensors.Select(s => s.Pos.X)).Max();
            MinX = Beacons.Select((b) => (b.Pos.X)).Concat(Sensors.Select(s => s.Pos.X)).Min();
            MaxY = Beacons.Select((b) => (b.Pos.Y)).Concat(Sensors.Select(s => s.Pos.Y)).Max();
            MinY = Beacons.Select((b) => (b.Pos.Y)).Concat(Sensors.Select(s => s.Pos.Y)).Min();

            Width = MaxX - MinX + 1;
            Height = MaxY - MinY + 1;
        }

        private Pos ReadPos(string s, string sep = ",")
        {
            var split = s.Split(sep);
            var x = int.Parse(split[0]);
            var y = int.Parse(split[1]);

            return new Pos(x, y);
        }

        private (Pos, Pos) Read2Pos(string s, string sep = " ")
        {
            var split = s.Split(sep);
            var pos1 = ReadPos(split[0]);
            var pos2 = ReadPos(split[1]);

            return (pos1, pos2);
        }

        private int ManhattanDist(Pos p1, Pos p2)
        {
            return Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
        }

        public long Part1(int y, bool debug = false)
        {
            var result = 0L;

            var beacon = Beacons.Where(b => b.Pos.Y == y).Single();

            // Adjust for the 'diamond' otherwise answer is too small!
            var minX = Sensors.Min(s => s.Pos.X - s.MinDist);
            var maxX = Sensors.Max(s => s.Pos.X + s.MinDist);

            // Console.WriteLine($"Beacon = {beacon}, MinX = {MinX}, MaxX = {MinX + Width - 1} => {minX}, {maxX}");

            for (int x = minX; x < maxX; x++)
            {
                var pos = new Pos(MinX + x, y);

                if (Sensors.Any(s =>
                {
                    var distToPos = ManhattanDist(pos, s.Pos);
                    var distToNearestBeacon = s.MinDist;
                    return distToPos <= distToNearestBeacon;
                }))
                {
                    if (pos != beacon.Pos)
                        result++;
                }
            }

            return result;
        }

        public long Part2(int limit, bool debug = false)
        {
            var result = 0L;

            for (int x = 0; x <= limit; x++)
            {
                if (x % 1 == 0)
                {
                    Console.WriteLine($"{x}...");
                }

                for (int y = 0; y <= limit; y++)
                {
                    var pos = new Pos(x, y);

                    var allSensorsHaveCloserBeacon = Sensors.All(s =>
                    {
                        var distToPos = ManhattanDist(s.Pos, pos);
                        var distToNearestBeacon = s.MinDist;

                        return distToPos > distToNearestBeacon;
                    });

                    if (allSensorsHaveCloserBeacon)
                    {
                        // We have an answer!
                        Console.WriteLine($"Answer is: {pos}");

                        result = x * 4000000 + y;
                        return result;
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            var sw = new StringWriter();

            sw.WriteLine($"#LINES = {Lines.Length}, #SENSORS = {Sensors.Length}, #BEACONS = {Beacons.Length}");
            sw.Write($"Width = {Width}, Height = {Height}, Range = ({MinX},{MinY})->({MaxX},{MaxY})");

            return sw.ToString();
        }
    }

    public Day15() { }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day15Data.INPUT : Day15Data.SAMPLE);
    }

    public Result Answer()
    {
        DataType which = DataType.INPUT;

        var part1Row = which == DataType.SAMPLE ? 10 : 2000000;
        var part2Limit = which == DataType.SAMPLE ? 20 : 4000000;

        var model = GetModel(which);
        Console.WriteLine(model);

        var day = RunUtils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}, #SENSORS = {model.Sensors.Length}, #BEACONS = {model.Beacons.Length}");

        var result1 = new Result(model.Part1(part1Row), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        var result2 = new Result(model.Part2(part2Limit), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
