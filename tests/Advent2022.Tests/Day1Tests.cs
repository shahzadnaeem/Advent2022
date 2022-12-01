using Advent2022;
using Xunit;

namespace Advent2022Lib.Tests;

public class Day1Tests
{
    [Fact]
    public void ATest()
    {
        var day1 = new Day1();

        // (top calories, top 3 calories)
        var expected = (68923, 200044);
        var result = day1.Answer();

        Assert.Equal(expected, result);
    }
}
