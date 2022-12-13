using Advent2022;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using Advent2022Lib.Tests.Helpers;

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
    [InlineData(12, 6)]
    [InlineData(1200, 200)]
    public void TwoWindowTest(int size, int width)
    {
        var input = Enumerable.Range(1, size).ToList();
        int NUM_WINDOWS = size / width;

        var res = XEnumerable.Window(input, width).ToArray();

        Assert.Equal(input.Count - width + 1, res.Count());

        for (var win = 0; win < NUM_WINDOWS; win++)
        {
            Assert.Equal(Enumerable.Range(win + 1, width).ToArray(), res[win]);
        }
    }
}