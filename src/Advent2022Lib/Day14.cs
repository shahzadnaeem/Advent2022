namespace Advent2022;

using Advent2022Lib.AdHoc;

public class Day14
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    // TODO: Other enums/consts/classes ...
    public enum BoardContents
    {
        EMPTY,
        WALL,
        SAND
    }

    public record Position(int X, int Y);

    public class Model
    {
        const int SOURCE_X = 500;

        public string[] Lines { get; init; } = null!;
        public int Height { get; init; } = 0;
        public int Width { get; init; } = 0;
        public int FirstX { get; init; } = 0;
        public bool SourceValid { get; init; } = false;
        public List<List<(Position, Position)>> Paths { get; init; } = null!;
        public BoardContents[,] Board { get; init; } = null!;

        public Model(string input)
        {
            Lines = input.Split(Environment.NewLine);

            Paths = Lines.Select((l, i) =>
            {
                var positions = l.Split(" -> ").ToList()
                    .Select(pos =>
                    {
                        var xy = pos.Split(',').Select(c => int.Parse(c)).ToArray();
                        return new Position(xy[0], xy[1]);
                    });

                var pairs = new List<(Position, Position)>();

                XEnumerable.Window(positions, 2).ToList()
                .ForEach(pair =>
                {
                    pairs.Add((pair[0], pair[1]));
                });

                return pairs;
            }).ToList();

            Height = Paths.Max(path => path.Max(pair => Math.Max(pair.Item1.Y, pair.Item2.Y))) + 1;
            FirstX = Paths.Min(path => path.Min(pair => Math.Min(pair.Item1.X, pair.Item2.X)));
            var lastX = Paths.Max(path => path.Max(pair => Math.Max(pair.Item1.X, pair.Item2.X)));
            Width = lastX - FirstX + 1;

            SourceValid = SOURCE_X >= FirstX && SOURCE_X < FirstX + Width;

            // Create the board...
            Board = new BoardContents[Width, Height];
            Reset();
        }

        private void Reset()
        {
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                {
                    Board[x, y] = BoardContents.EMPTY;
                }

            AddWalls();
        }

        private void AddWalls()
        {
            Paths.ForEach(path =>
            {
                path.ForEach(pair => AddWall(pair.Item1, pair.Item2));
            });
        }

        private void AddWall(Position from, Position to)
        {
            if (from.X == to.X)
            {
                // Vertical
                for (int y = Math.Min(from.Y, to.Y); y <= Math.Max(from.Y, to.Y); y++)
                {
                    Board[from.X - FirstX, y] = BoardContents.WALL;
                }
            }
            else
            {
                // Horizontal
                for (int x = Math.Min(from.X, to.X); x <= Math.Max(from.X, to.X); x++)
                {
                    Board[x - FirstX, from.Y] = BoardContents.WALL;
                }
            }
        }

        private bool TheAbyss(Position p)
        {
            return p.X < 0 || p.X >= Width || p.Y >= Height;
        }

        public bool CanDrop(Position p, int dx = 0, int dy = 1)
        {
            bool canDrop = false;

            if (Board[p.X, p.Y] != BoardContents.EMPTY)
            {
                return false;
            }

            var newP = new Position(p.X + dx, p.Y + dy);

            if (TheAbyss(newP))
            {
                throw new Exception($"Abyss!: p = ({p.X},{p.Y}), newP = ({newP.X},{newP.Y})");
            }

            if (Board[newP.X, newP.Y] == BoardContents.EMPTY)
            {
                return CanDrop(newP);
            }
            else
            {
                if (!CanDrop(new Position(p.X - 1, p.Y + 1)) && !CanDrop(new Position(p.X + 1, p.Y + 1)))
                {
                    Board[p.X, p.Y] = BoardContents.SAND;
                    canDrop = true;
                }
            }

            return canDrop;
        }


        public bool TryDrop()
        {
            var start = new Position(SOURCE_X - FirstX, 0);

            return CanDrop(start);
        }

        // TODO: Parts 1 and 2
        public long Part1(bool debug = false)
        {
            int grain = 0;

            try
            {
                while (TryDrop())
                {
                    grain++;
                    Console.WriteLine($"\nGrain: {grain}\n{BoardToString()}");
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nGrain = {grain}: {e.Message}\n");
                // We hit the abyss!
            }

            return grain;
        }

        public long Part2(bool debug = false)
        {
            // TODO: Stuff...
            return -1;
        }

        public string BoardToString()
        {
            var sw = new StringWriter();

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (x == 0)
                    {
                        sw.Write($"{y:D3} ");
                    }

                    var c = Board[x, y];

                    if (c == BoardContents.WALL)
                        sw.Write('#');
                    else if (c == BoardContents.SAND)
                        sw.Write('o');
                    else if (y == 0 && x == SOURCE_X - FirstX)
                        sw.Write('+');
                    else
                        sw.Write('.');
                }

                if (y != Height - 1)
                {
                    sw.WriteLine();
                }
            }

            return sw.ToString();
        }

        public override string ToString()
        {
            // TODO: Additional details?

            var sw = new StringWriter();

            sw.Write($"#PATHS = {Lines.Length}, Height = {Height}, Width = {Width}  [{FirstX}-{FirstX + Width - 1} x 0-{Height - 1}]");
            sw.WriteLine($", Source ({SOURCE_X}) is {(SourceValid ? "OK" : "INVALID!!!")}");

            sw.Write(BoardToString());

            return sw.ToString();
        }
    }

    public Day14() { }

    private Model GetModel(DataType which = DataType.INPUT, int control1 = 0, bool debug = false)
    {
        // control1 - optional control parameter (sometimes parts 1 and 2 have different behaviour)
        // debug - optional debug parameter

        return new Model(which == DataType.INPUT ? Day14Data.INPUT : Day14Data.SAMPLE);
    }

    public Result Answer()
    {
        // TODO: Start with SAMPLE data
        DataType which = DataType.SAMPLE;

        var model = GetModel(which);

        var day = Utils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}");
        Console.WriteLine(model);

        var result1 = new Result(model.Part1(), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        // TODO: Possible altenative model - or keep using above version
        model = GetModel(which, 123);
        var result2 = new Result(model.Part2(), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
