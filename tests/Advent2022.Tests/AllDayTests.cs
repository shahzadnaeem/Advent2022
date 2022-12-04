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
}
