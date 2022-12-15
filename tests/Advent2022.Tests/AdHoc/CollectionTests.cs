using Xunit;

using Advent2022Lib.Tests.Helpers;

namespace Advent2022Lib.Tests.AdHoc;

public class CollectionTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void InvalidChunkWidthThrows(int width)
    {
        var range = Enumerable.Range(0, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => { var chunks = range.Chunk(width); });
    }

    [Theory]
    [InlineData(4, 177, 0)]
    [InlineData(177, 4, 0)]
    [InlineData(177, 19, 1)]
    public void Chunking(int num, int width, int start)
    {
        var count = num * width;

        var range = Enumerable.Range(start, count);

        var chunks = range.Chunk(width);

        TestHelpers.CheckChunks(chunks, start, width);
    }

    [Theory]
    [InlineData(123457, 32, 123455)]
    public void HugeChunking(int num, int width, int start)
    {
        var count = num * width;

        var range = Enumerable.Range(start, count);

        var chunks = range.Chunk(width);

        var results = chunks.Select((xs, i) => (xs, i)).ToList();

        Console.WriteLine($"Memory used = {TestHelpers.GetMBsUsed()}MB");

        TestHelpers.CheckChunks(chunks, start, width);

        Console.WriteLine($"Memory used = {TestHelpers.GetMBsUsed()}MB");
    }

    enum Stuff { This, And, That };

    [Theory]
    [InlineData("a", "a")]
    [InlineData(true, true)]
    [InlineData(Stuff.And, Stuff.And)]
    public void HelpersTests<T>(T exp, T res)
    {
        TestHelpers.Compare(exp, res);
    }

    [Theory]
    [InlineData(6)]
    public void EmptyChunking(int width)
    {
        var range = Enumerable.Range(0, 0);

        var chunks = range.Chunk(width);

        Assert.Equal(0, chunks.Count());
    }


    [Theory]
    [InlineData(3, 6)]
    [InlineData(5, 6)]
    public void UndeflowChunking(int num, int width)
    {
        var start = 1;
        var count = 1;

        var range = Enumerable.Range(start, num);

        var chunks = range.Chunk(width);

        Assert.Equal(count, chunks.Count());

        Assert.Equal(range, chunks.ElementAt(0));
    }
}
