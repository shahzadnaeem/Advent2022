namespace Advent2022;

public class Day2
{
    public enum Piece
    {
        Rock,
        Paper,
        Scissors
    }

    public enum DesiredResult
    {
        Win,
        Lose,
        Draw
    }

    public class Model
    {
        private string Data { get; set; } = "";
        public (Piece, Piece)[] Rows { get; init; } = null!;

        // Access is ScoreLookip[their,mine]
        private static long[,] ScoreLookup = {
            { 4, 8, -3 },
            { -1, 5, 9 },
            { 7, -2, 6 }
        };

        private static Piece[] WinLookup = {
            Piece.Paper, Piece.Scissors, Piece.Rock
        };

        private static Piece[] LoseLookup = {
            Piece.Scissors, Piece.Rock, Piece.Paper
        };

        private static DesiredResult[] DesiredResultLookup = {
            DesiredResult.Lose, DesiredResult.Draw, DesiredResult.Win
        };

        public Piece PieceLookup(char tag)
        {
            if (tag == 'A' || tag == 'X') return Piece.Rock;
            if (tag == 'B' || tag == 'Y') return Piece.Paper;
            if (tag == 'C' || tag == 'Z') return Piece.Scissors;

            throw new ArgumentException($"Invalid Piece - '{tag}'");
        }

        public Piece PickPieceForDesiredResult(Piece their, Piece mine)
        {
            DesiredResult res = DesiredResultLookup[(int)mine];

            if (res == DesiredResult.Draw)
            {
                return their;
            }
            else if (res == DesiredResult.Lose)
            {
                return LoseLookup[(int)their];
            }
            else
            {
                return WinLookup[(int)their];
            }
        }

        public long GameScore(Piece their, Piece mine)
        {
            var score = ScoreLookup[(int)their, (int)mine];
            return Math.Abs(score);
        }

        public long GameScore2(Piece their, Piece rule)
        {
            var score = ScoreLookup[(int)their, (int)PickPieceForDesiredResult(their, rule)];
            return Math.Abs(score);
        }

        public Model(string input)
        {
            Data = input;
            Rows = Data.Split(Environment.NewLine)
                .Select(l =>
                {
                    var row = l.Split(' ');
                    return (PieceLookup(row[0][0]), PieceLookup(row[1][0]));
                }).ToArray();
        }


        public override string ToString()
        {
            return $"[{string.Join(',', Rows)}]";
        }
    }

    public Day2()
    {
    }

    private Model GetModel()
    {
        return new Model(Day2Data.INPUT);
    }

    public long PlayGames(Model model)
    {
        return model.Rows.Select(game => model.GameScore(game.Item1, game.Item2)).Sum();
    }

    public long PlayGames2(Model model)
    {
        return model.Rows.Select(game => model.GameScore2(game.Item1, game.Item2)).Sum();
    }

    public (long, long) Answer()
    {
        var model = GetModel();

        Console.WriteLine($"#ROWS = {model.Rows.Length}");

        // Part 1
        var result1 = (PlayGames(model), model.Rows.Length);

        Console.WriteLine($"Result1 = {result1}");

        // Part 2
        var result2 = (PlayGames2(model), model.Rows.Length);

        Console.WriteLine($"Result2 = {result2}");

        // Final result in a tuple
        return (result1.Item1, result2.Item1);
    }
}
