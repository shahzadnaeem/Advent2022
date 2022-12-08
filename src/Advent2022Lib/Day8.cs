namespace Advent2022;

public class Day8
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public class Model
    {
        private string Data { get; set; } = "";

        public int Width { get; init; } = 0!;
        public int Height { get; init; } = 0!;

        public int[,] Forest { get; init; } = null!;

        public Model(string input)
        {
            Data = input;
            var lines = Data.Split(Environment.NewLine);

            Width = lines[0].Length;
            Height = lines.Length;

            Forest = new int[Width, Height];

            foreach (var (l, y) in lines.Select((l, y) => (l, y)))
                foreach (var (c, x) in l.ToArray().Select((c, x) => (c, x)))
                    Forest[x, y] = c - '0';
        }

        public int TreeHeight(int x, int y)
        {
            return Forest[x, y];
        }

        public bool TreeIsVisible(int x, int y)
        {
            if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
            {
                return true;
            }

            var height = TreeHeight(x, y);
            var invisibleCount = 0;

            for (var xr = x + 1; xr < Width; xr++)
            {
                if (TreeHeight(xr, y) >= height)
                {
                    invisibleCount++;
                    break;
                }
            }

            for (var xl = x - 1; xl >= 0; xl--)
            {
                if (TreeHeight(xl, y) >= height)
                {
                    invisibleCount++;
                    break;
                }
            }

            for (var yb = y + 1; yb < Height; yb++)
            {
                if (TreeHeight(x, yb) >= height)
                {
                    invisibleCount++;
                    break;
                }
            }

            for (var yt = y - 1; yt >= 0; yt--)
            {
                if (TreeHeight(x, yt) >= height)
                {
                    invisibleCount++;
                    break;
                }
            }

            return invisibleCount != 4;
        }

        public int ScenicScore(int x, int y)
        {
            var height = TreeHeight(x, y);
            var scores = new int[] { 0, 0, 0, 0 };

            for (var xr = x + 1; xr < Width; xr++)
            {
                var h = TreeHeight(xr, y);

                scores[0]++;

                if (h >= height) break;
            }

            for (var xl = x - 1; xl >= 0; xl--)
            {
                var h = TreeHeight(xl, y);

                scores[1]++;

                if (h >= height) break;
            }

            for (var yb = y + 1; yb < Height; yb++)
            {
                var h = TreeHeight(x, yb);

                scores[2]++;

                if (h >= height) break;
            }

            for (var yt = y - 1; yt >= 0; yt--)
            {
                var h = TreeHeight(x, yt);

                scores[3]++;

                if (h >= height) break;
            }

            return scores.Aggregate(1, (acc, curr) => acc *= curr);
        }

        public int VisibleTrees()
        {
            var numVisible = 0;

            foreach (var y in Enumerable.Range(0, Height))
            {
                foreach (var x in Enumerable.Range(0, Width))
                {
                    if (TreeIsVisible(x, y))
                    {
                        numVisible++;
                    }
                }
            }

            return numVisible;
        }

        public int HighestScenicScore()
        {
            var highest = 0;

            foreach (var y in Enumerable.Range(0, Height))
            {
                foreach (var x in Enumerable.Range(0, Width))
                {
                    var score = ScenicScore(x, y);
                    highest = Math.Max(highest, score);
                }
            }

            return highest;
        }

        public override string ToString()
        {
            var sw = new StringWriter();

            sw.Write($"({Width}x{Height}) forest");

            return sw.ToString();
        }
    }

    public Day8()
    {
    }

    private Model GetModel(DataType which = DataType.INPUT)
    {
        return new Model(which == DataType.INPUT ? Day8Data.INPUT : Day8Data.SAMPLE);
    }

    private long Part1(Model model)
    {
        return model.VisibleTrees();
    }

    private long Part2(Model model)
    {
        return model.HighestScenicScore();
    }

    public (long, long) Answer()
    {
        var model = GetModel(DataType.INPUT);

        Console.WriteLine($"Day 8 - {model}");

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
