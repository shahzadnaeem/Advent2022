using System.Text.RegularExpressions;

namespace Advent2022;

public record Result(long P1, long P2 = 0);

public class RunUtils
{
    public const int YEAR = 2022;
    public const int ADVENT = 12;

    public static void White() { Console.ForegroundColor = ConsoleColor.White; }
    public static void Red() { Console.ForegroundColor = ConsoleColor.Red; }
    public static void Green() { Console.ForegroundColor = ConsoleColor.Green; }
    public static void Yellow() { Console.ForegroundColor = ConsoleColor.Yellow; }

    public static string ArrayToString<T>(T[] items)
    {
        var wr = new StringWriter();

        var c = items.Length;

        wr.Write("[ ");

        foreach (var item in items)
        {
            wr.Write($"{item}");
            c--;
            if (c > 0) wr.Write(',');
        }

        wr.Write(" ]");

        return wr.ToString();
    }

    // For "Day14" to "Day 14"
    public static string NumSpace(string s)
    {
        var r = new Regex(@"(\D+)(\d+)");

        return r.Replace(s, @"$1 $2");
    }

    public static void Title(string title)
    {
        Console.WriteLine("\n");
        RunUtils.Red();
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
        RunUtils.White();
    }


    public static void Day(int day)
    {
        var adventDay = DateTime.Now.Day;
        var live = DateTime.Now.Month == ADVENT && DateTime.Now.Year == YEAR;

        if (live && adventDay == day)
        {
            Title($"👍 Today is Day {day} 👍");
        }

        RunUtils.Green();
        Console.WriteLine($"\nDay {day}");
        RunUtils.Yellow();
    }

    public static void Result(string result)
    {
        RunUtils.White();
        Console.WriteLine(result);
    }

    // Create a multi line C# string from a VERY long string, split into 'chunkSize' chunks
    //   VSCode really does not like VERY long strings! (4095 chars when spotted)
    public static string PrepInput(string input, int chunkSize)
    {
        int numChunks = input.Length / chunkSize + Math.Sign(input.Length % chunkSize);

        var sw = new StringWriter();

        sw.WriteLine(@"public const string INPUT =");
        for (int chunk = 0; chunk < numChunks; chunk++)
        {
            int lastChunkSize = input.Length % chunkSize == 0 ? chunkSize : input.Length % chunkSize;
            bool lastChunk = chunk == numChunks - 1;

            var line = input.Substring(chunk * chunkSize, lastChunk ? lastChunkSize : chunkSize);

            sw.Write($"\"{line}\"");

            sw.WriteLine(lastChunk ? ";" : "+");
        }

        return sw.ToString();
    }

    public static string PrepInputFile(string path, int chunkSize)
    {
        var input = GetFileContents(path);

        return PrepInput(input, chunkSize);
    }

    public static string GetFileContents(string path)
    {
        return System.IO.File.ReadAllText(path);
    }
}
