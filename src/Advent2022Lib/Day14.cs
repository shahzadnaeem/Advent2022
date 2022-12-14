namespace Advent2022;

using Advent2022Lib.AdHoc;

public class Day14
{
    enum DataType
    {
        INPUT,
        SAMPLE
    }

    public enum BoardContents
    {
        EMPTY,
        WALL,
        SAND
    }

    const int PART_1 = 1;
    const int PART_2 = 2;

    public record Position(int X, int Y);

    public class Model
    {
        const int SOURCE_X = 500;

        public string[] Lines { get; init; } = null!;
        public int Height { get; init; } = 0;
        public int Width { get; init; } = 0;
        public int FirstX { get; init; } = 0;
        public int XOffset { get; set; } = 0;
        public bool SourceValid { get; init; } = false;
        public int SourceX { get; set; } = 0;
        public List<List<(Position, Position)>> Paths { get; init; } = null!;
        public BoardContents[,] Board { get; init; } = null!;

        public Model(string input, int part = PART_1)
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
            SourceX = SOURCE_X;

            if (part == 2)
            {
                // Part 2 adjustments
                Width += (2 * Height);
                XOffset = Height - 1;
                Height += 2;
            }

            // Create the board...
            Board = new BoardContents[Width, Height];
            Reset(part);
        }

        private void Reset(int part = 1)
        {
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                {
                    Board[x, y] = BoardContents.EMPTY;
                }

            AddWalls();

            if (part == 2)
            {
                AddWall(new Position(FirstX + 0, Height - 1), new Position(FirstX + Width - 1, Height - 1), 0);
            }
        }

        private void AddWalls()
        {
            Paths.ForEach(path =>
            {
                path.ForEach(pair =>
                {
                    AddWall(pair.Item1, pair.Item2, XOffset);
                });
            });
        }

        private void AddWall(Position from, Position to, int xOffset = 0)
        {
            if (from.X == to.X)
            {
                // Vertical
                for (int y = Math.Min(from.Y, to.Y); y <= Math.Max(from.Y, to.Y); y++)
                {
                    Board[xOffset + from.X - FirstX, y] = BoardContents.WALL;
                }
            }
            else
            {
                // Horizontal
                for (int x = Math.Min(from.X, to.X); x <= Math.Max(from.X, to.X); x++)
                {
                    Board[xOffset + x - FirstX, from.Y] = BoardContents.WALL;
                }
            }
        }

        private bool TheAbyss(Position p)
        {
            return p.X < 0 || p.X >= Width || p.Y >= Height;
        }

        public bool DoDrop(int grain, Position p, int dx = 0, int dy = 1)
        {
            bool canDrop = false;

            var diag = dx != 0;

            // Console.WriteLine($"#{grain}: p = ({p.X},{p.Y}), dir2here = {(diag ? "diag" : "down")}, dx = {dx}, dy = {dy}");

            if (Board[p.X, p.Y] == BoardContents.EMPTY)
            {
                var newP = new Position(p.X + dx, p.Y + dy);

                if (TheAbyss(newP))
                {
                    throw new Exception($"Abyss!: p = ({p.X},{p.Y}), newP = ({newP.X},{newP.Y})");
                }

                if (Board[newP.X, newP.Y] == BoardContents.EMPTY)
                {
                    return DoDrop(grain, newP);
                }
                else
                {
                    if (!diag)
                    {
                        // If we just dropped down to here, we check the diagonals
                        canDrop = true;

                        if (!DoDrop(grain, p, -1, 1) && !DoDrop(grain, p, 1, 1))
                        {
                            Board[p.X, p.Y] = BoardContents.SAND;
                        }
                    }
                }
            }

            return canDrop;
        }


        public bool Drop(int grain)
        {
            var start = new Position(XOffset + SourceX - FirstX, 0);

            return DoDrop(grain, start);
        }

        public long Solve(bool debug = false)
        {
            int grain = 0;

            try
            {
                while (Drop(grain))
                {
                    grain++;
                };
            }
            catch (Exception)
            {
                // We hit the abyss!
            }

            return grain;
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
                    else if (y == 0 && x == XOffset + SourceX - FirstX)
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
            sw.WriteLine($", Source ({SourceX}) is {(SourceValid ? "OK" : "INVALID!!!")}");

            sw.Write(BoardToString());

            return sw.ToString();
        }
    }

    public Day14() { }

    private Model GetModel(DataType which = DataType.INPUT, int part = PART_1)
    {
        return new Model(which == DataType.INPUT ? Day14Data.INPUT : Day14Data.SAMPLE, part);
    }

    public Result Answer()
    {
        DataType which = DataType.INPUT;

        var model = GetModel(which);

        var day = Utils.NumSpace(this.GetType().Name);
        Console.WriteLine($"{day} - #LINES = {model.Lines.Length}");
        // Console.WriteLine(model);

        var result1 = new Result(model.Solve(), model.Lines.Length);
        Console.WriteLine($"Part 1 = {result1}");

        model = GetModel(which, PART_2);
        var result2 = new Result(model.Solve(), model.Lines.Length);
        Console.WriteLine($"Part 2 = {result2}");

        return new Result(result1.P1, result2.P1);
    }
}
