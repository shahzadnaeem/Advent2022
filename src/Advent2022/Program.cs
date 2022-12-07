using Advent2022;

if (args.Length > 0)
{
    RunInputPrep(args);
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
}

void RunInputPrep(string[] args)
{
    var day = DateTime.Now.Day;

    Utils.Title("Input Prep = Day {day}");

    const string DAY6DATA_PATH = @"src/Advent2022Lib/Day6Data.txt";
    const string TESTDATA_PATH = @"src/Advent2022Lib/TestData.txt";

    var path = DAY6DATA_PATH;
    var chunkSize = 100;

    var inputFile = Utils.GetFileContents(path);
    var preppedFile = Utils.PrepInputFile(path, chunkSize);

    Utils.Day(6);
    Utils.Result($"# path = {path}, chunkSize = {chunkSize}\n");
    Utils.Result($"\n# input - {inputFile.Split(Environment.NewLine).Count()} lines\n{inputFile}");
    Utils.Result($"\n# prepped - {preppedFile.Split(Environment.NewLine).Count()} lines\n{preppedFile}");
}
