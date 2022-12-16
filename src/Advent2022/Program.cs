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

    RunUtils.Title($"Advent of Code 2022 - Day {day}");

    RunUtils.Day(1);
    RunUtils.Result($"Answer = {new Day1().Answer().ToString()}");

    RunUtils.Day(2);
    RunUtils.Result($"Answer = {new Day2().Answer().ToString()}");

    RunUtils.Day(3);
    RunUtils.Result($"Answer = {new Day3().Answer().ToString()}");

    RunUtils.Day(4);
    RunUtils.Result($"Answer = {new Day4().Answer().ToString()}");

    RunUtils.Day(5);
    RunUtils.Result($"Answer = {new Day5().Answer().ToString()}");

    RunUtils.Day(6);
    RunUtils.Result($"Answer = {new Day6().Answer().ToString()}");

    RunUtils.Day(7);
    RunUtils.Result($"Answer = {new Day7().Answer().ToString()}");

    RunUtils.Day(8);
    RunUtils.Result($"Answer = {new Day8().Answer().ToString()}");

    RunUtils.Day(9);
    RunUtils.Result($"Answer (HELPED!) = {new Day9().Answer().ToString()}");

    RunUtils.Day(10);
    RunUtils.Result($"Answer = {new Day10().Answer().ToString()}");

    RunUtils.Day(11);
    RunUtils.Result($"Answer = {new Day11().Answer().ToString()}");

    RunUtils.Day(12);
    RunUtils.Result($"Answer = {new Day12().Answer().ToString()}");

    RunUtils.Day(13);
    RunUtils.Result($"Answer (NOT MINE!) = {new Day13().Answer().ToString()}");

    RunUtils.Day(14);
    RunUtils.Result($"Answer = {new Day14().Answer().ToString()}");

    RunUtils.Day(15);
    RunUtils.Result($"Answer (HELPED/DEBUGGED!) = {new Day15().Answer().ToString()}");
}

void RunInputPrep(string[] args)
{
    var day = DateTime.Now.Day;

    RunUtils.Title("Input Prep = Day {day}");

    const string DAY6DATA_PATH = @"src/Advent2022Lib/Day6Data.txt";

    var path = DAY6DATA_PATH;
    var chunkSize = 100;

    var inputFile = RunUtils.GetFileContents(path);
    var preppedFile = RunUtils.PrepInputFile(path, chunkSize);

    RunUtils.Day(6);
    RunUtils.Result($"# path = {path}, chunkSize = {chunkSize}\n");
    RunUtils.Result($"\n# input - {inputFile.Split(Environment.NewLine).Count()} lines\n{inputFile}");
    RunUtils.Result($"\n# prepped - {preppedFile.Split(Environment.NewLine).Count()} lines\n{preppedFile}");
}
