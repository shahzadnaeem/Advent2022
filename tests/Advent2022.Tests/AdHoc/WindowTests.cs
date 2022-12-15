using Xunit;

using Advent2022Lib.AdHoc;

namespace Advent2022Lib.Tests.AdHoc;

public class WindowTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void InvalidChunkWidthThrows(int width)
    {
        var input = Enumerable.Range(0, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => { var res = XEnumerable.Window(input, width); });
    }

    [Fact]
    public void InsuffientDataReturnsOneWindow()
    {
        var input = new List<int> { 1, 2, 3, 4 };
        var res = XEnumerable.Window(input, 6);

        Assert.Equal(input, res.Single());
    }

    [Fact]
    public void SingleWindowTest()
    {
        var input = new List<int> { 1, 2, 3, 4 };
        var res = XEnumerable.Window(input, 4);

        Assert.Equal(input, res.Single());
    }

    [Theory]
    [InlineData(6, 3)]
    [InlineData(12, 7)]
    [InlineData(1200, 127)]
    public void MultipleWindowTest(int size, int width)
    {
        var rng = new System.Random();

        var input = Enumerable.Range(1, size).Select(i => rng.Next()).ToList();
        int NUM_WINDOWS = size - width + 1;

        var res = XEnumerable.Window(input, width).ToArray();

        Assert.Equal(input.Count - width + 1, res.Count());

        for (var win = 0; win < NUM_WINDOWS; win++)
        {
            Assert.Equal(Enumerable.Range(win, width).Select(i => input[i]).ToArray(), res[win]);
        }
    }
}