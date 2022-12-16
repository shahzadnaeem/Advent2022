using System.Linq;
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

    const int PART_2_RES_X_SCALE = 4_000_000;

    public record Pos(int X, int Y);
    public record Range(Pos start, Pos end);

    public record Beacon(Pos Pos);
    public record Sensor(Pos Pos, Beacon NearestBeacon, int MinDist);

    public class Model
    {
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
            var lines = input.Split(Environment.NewLine);

            var beacons = new HashSet<Beacon>();
            var sensors = new HashSet<Sensor>();

            // Parse ...

            var r = new Regex(@"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");

            lines.Select((l, i) => (l, i)).ToList().ForEach(line =>
            {
                var regexed = r.Replace(line.l, @"$1,$2 $3,$4");

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
                    throw new Exception($"BAD LINE:#{line.i} {line.l}");
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
                        result = x * limit + y;
                        return result;
                    }
                }
            }

            return result;
        }

        private List<List<Range>> MakeBoard(int size)
        {
            var board = Enumerable.Range(0, size + 1).Select(y =>
            {
                var row = new List<Range>();
                row.Add(new Range(new Pos(0, y), new Pos(size, y)));
                return row;
            });

            return board.ToList();
        }

        private List<Range> ProcessSensedRow(List<Range> row, Sensor s)
        {
            // if (row[0].start.Y == 11 && row.Count == 2)
            // {
            //     var p = 1;
            //     p++;
            // }

            var newRow = row.SelectMany(range =>
            {
                var (from, to) = range;
                if (s.Pos.X < from.X)
                {
                    var dist = s.MinDist - ManhattanDist(s.Pos, from);
                    if (dist >= 0)
                    {
                        var newX = from.X + dist + 1;
                        if (newX >= to.X)
                        {
                            return new List<Range>();
                        }
                        else
                        {
                            return new List<Range> { new Range(from, new Pos(newX, to.Y)) };
                        }
                    }
                    else
                    {
                        return new List<Range>() { range };
                    }
                }
                else if (s.Pos.X > to.X)
                {
                    var dist = s.MinDist - ManhattanDist(s.Pos, to);
                    if (dist >= 0)
                    {
                        var newX = to.X - dist - 1;
                        if (newX <= from.X)
                        {
                            return new List<Range>();
                        }
                        else
                        {
                            return new List<Range> { new Range(from, new Pos(newX, to.Y)) };
                        }
                    }
                    else
                    {
                        return new List<Range>() { range };
                    }
                }
                else
                {
                    var dist = s.MinDist - ManhattanDist(new Pos(0, s.Pos.Y), new Pos(0, from.Y)) + 1;
                    if (dist > 0)
                    {
                        var newRow = new List<Range>();

                        var newX = s.Pos.X - dist;
                        if (from.X <= newX)
                        {
                            newRow.Add(new Range(from, new Pos(newX, to.Y)));
                        }

                        newX = s.Pos.X + dist;
                        if (newX <= to.X)
                        {
                            newRow.Add(new Range(new Pos(newX, from.Y), to));
                        }

                        return newRow;
                    }
                    else
                    {
                        return new List<Range>() { range };
                    }
                }
            });

            var result = newRow.ToList();

            // var rowEntries = row.Count;
            // var newRowEntries = result.Count;
            // var rowNum = row[0].start.Y;

            // Console.WriteLine($"row #{rowNum}: {rowEntries} -> {newRowEntries}");

            // if (newRowEntries > rowEntries || newRowEntries == 1)
            // {
            //     Console.WriteLine($"\n\nrow #{result[0].start.Y} changed from {rowEntries} to {newRowEntries}");
            //     row.ForEach(range =>
            //     {
            //         Console.WriteLine($"{range.start} -> {range.end}");
            //     });

            //     Console.WriteLine("\n");

            //     result.ForEach(range =>
            //     {
            //         Console.WriteLine($"{range.start} -> {range.end}");
            //     });
            // }

            return result;
        }

        private List<List<Range>> RemoveSensedArea(List<List<Range>> board, Sensor s)
        {
            var newBoard = board
                .Select(row => ProcessSensedRow(row, s))
                .Where(row => row.Count != 0)
                .ToList();

            return newBoard;
        }

        private List<List<Range>> RemoveSensedAreas(List<List<Range>> board)
        {
            var newBoard = Sensors
                .Aggregate<Sensor, List<List<Range>>>(board, (board, sensor) =>
                {
                    return RemoveSensedArea(board, sensor);
                });

            return newBoard;
        }

        public long Part2Alt(int limit, bool debug = false)
        {
            var result = 0L;

            var board = MakeBoard(limit);

            var newBoard = RemoveSensedAreas(board);

            Console.WriteLine($"Part2 done: resulting board rowCount = {newBoard.Count}!");

            if (newBoard.Count == 1 && newBoard[0].Count == 1)
            {
                var range = newBoard[0];
                var row = range[0];
                result = row.start.X * PART_2_RES_X_SCALE + row.start.Y;
            }

            if (newBoard.Count != 1)
            {
                // What happened???
                newBoard.Take(10).Select((r, i) => (r, i)).ToList().ForEach(r =>
                {
                    Console.Write($"Row: {r.i}: ");

                    if (r.r.Count != 0)
                    {
                        Console.WriteLine($"{r.r[0]}");
                    }
                    else
                    {
                        Console.WriteLine("EMPTY!");
                    }
                });
            }

            return result;
        }


        public override string ToString()
        {
            var sw = new StringWriter();

            sw.WriteLine($"#SENSORS = {Sensors.Length}, #BEACONS = {Beacons.Length}");
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
        var part2Limit = which == DataType.SAMPLE ? 20 : PART_2_RES_X_SCALE;

        var model = GetModel(which);
        // Console.WriteLine(model);

        var day = RunUtils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #SENSORS = {model.Sensors.Length}, #BEACONS = {model.Beacons.Length}");

        var result1 = new Result(model.Part1(part1Row));
        Console.WriteLine($"Part 1 = {result1}");

        // Only works for small boards!
        // var result2 = new Result(model.Part2(part2Limit));
        // Console.WriteLine($"Part 2 = {result2}");

        var result2 = new Result(model.Part2Alt(part2Limit));
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
