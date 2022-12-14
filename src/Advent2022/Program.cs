using Advent2022;

// Usage:
//   Advent
//     make run => dotnet run --project src/Advent2022
//   Utils
//     make RUN_ARGS=parse run => dotnet run --project src/Advent2022 parse


if (args.Length > 0)
{
    if (args[0] == "prep")
    {
        RunInputPrep(args);
    }
    else if (args[0] == "parse")
    {
        var day7parser = new Day7Parser();

        day7parser.Run();
    }
    else
    {
        Console.WriteLine($"ERROR: Don't know how to '{args[0]}'");
    }

}
else
{
    RunAdvent(args);
}

void RunAdvent(string[] args)
{
    var day = DateTime.Now.Day;

    Utils.Title($"Advent of Code 2022 - Day {day}");

    Utils.Day(1);
    Utils.Result($"Answer = {new Day1().Answer().ToString()}");

    Utils.Day(2);
    Utils.Result($"Answer = {new Day2().Answer().ToString()}");

    Utils.Day(3);
    Utils.Result($"Answer = {new Day3().Answer().ToString()}");

    Utils.Day(4);
    Utils.Result($"Answer = {new Day4().Answer().ToString()}");

    Utils.Day(5);
    Utils.Result($"Answer = {new Day5().Answer().ToString()}");

    Utils.Day(6);
    Utils.Result($"Answer = {new Day6().Answer().ToString()}");

    Utils.Day(7);
    Utils.Result($"Answer = {new Day7().Answer().ToString()}");

    Utils.Day(8);
    Utils.Result($"Answer = {new Day8().Answer().ToString()}");

    Utils.Day(9);
    Utils.Result($"Answer (HELPED!) = {new Day9().Answer().ToString()}");

    Utils.Day(10);
    Utils.Result($"Answer = {new Day10().Answer().ToString()}");

    Utils.Day(11);
    Utils.Result($"Answer = {new Day11().Answer().ToString()}");

    Utils.Day(12);
    Utils.Result($"Answer = {new Day12().Answer().ToString()}");

    Utils.Day(13);
    Utils.Result($"Answer (NOT MINE!) = {new Day13().Answer().ToString()}");

    Utils.Day(14);
    Utils.Result($"Answer = {new Day14().Answer().ToString()}");
}

void RunInputPrep(string[] args)
{
    var day = DateTime.Now.Day;

    Utils.Title("Input Prep = Day {day}");

    const string DAY6DATA_PATH = @"src/Advent2022Lib/Day6Data.txt";

    var path = DAY6DATA_PATH;
    var chunkSize = 100;

    var inputFile = Utils.GetFileContents(path);
    var preppedFile = Utils.PrepInputFile(path, chunkSize);

    Utils.Day(6);
    Utils.Result($"# path = {path}, chunkSize = {chunkSize}\n");
    Utils.Result($"\n# input - {inputFile.Split(Environment.NewLine).Count()} lines\n{inputFile}");
    Utils.Result($"\n# prepped - {preppedFile.Split(Environment.NewLine).Count()} lines\n{preppedFile}");
}
