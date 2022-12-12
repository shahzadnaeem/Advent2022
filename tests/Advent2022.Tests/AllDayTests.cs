using Advent2022;
using Xunit;

namespace Advent2022Lib.Tests;

public class AllDayTests
{
    [Fact]
    public void Day1Test()
    {
        var day = new Day1();

        // (top calories, top 3 calories)
        var expected = (68923, 200044);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day2Test()
    {
        var day = new Day2();

        // (game 1 score, game 2 score)
        var expected = (15422, 15442);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day3Test()
    {
        var day = new Day3();

        // one sack, three sacks
        var expected = (7917, 2585);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day4Test()
    {
        var day = new Day4();

        // contained, overlapping
        var expected = (595, 952);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day5Test()
    {
        var day = new Day5();

        // stacks top, stacks top CrateMover 9001
        var expected = ("WHTLRMZRC", "GMPMLWNMG");
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day6Test()
    {
        var day = new Day6();

        // start packet, start message
        var expected = (1210, 3476);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day7Test()
    {
        var day = new Day7();

        // sum, min free dir
        var expected = (1886043, 3842121);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day8Test()
    {
        var day = new Day8();

        // visible, best scenic score
        var expected = (1859, 332640);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day9Test()
    {
        var day = new Day9();

        // 2 knot rope, 10 knot rope
        var expected = (6354, 2651);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day10Test()
    {
        var day = new Day10();

        // signal strengh, (display output)
        var expected = (13180, 0);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day11Test()
    {
        var day = new Day11();

        // monkey business (20 rounds), monkey business (10000 rounds)
        var expected = (119715, 18085004878);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Day12Test()
    {
        var day = new Day12();

        // shortest from Start, shortest overall
        var expected = new Advent2022.Result(408, 399);
        var result = day.Answer();

        Assert.Equal(expected, result);
    }
}
