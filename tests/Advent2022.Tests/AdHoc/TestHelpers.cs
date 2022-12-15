using Xunit;

namespace Advent2022Lib.Tests.Helpers;

public class TestHelpers
{
    public const long aMB = 1024 * 1024;

    public static long GetMBsUsed()
    {
        return GC.GetTotalMemory(false) / aMB;
    }

    public static void Compare<T>(T exp, T act)
    {
        Assert.Equal(exp, act);
    }

    public static void CheckChunks<T>(T chunks, int start, int width) where T : IEnumerable<int[]>
    {
        var results = chunks.Select((xs, i) => (xs, i)).ToList();

        results.ForEach(r =>
        {
            // Console.WriteLine($"#{r.i}: {Utils.ArrayToString(r.xs)}");
            var expected = Enumerable.Range(start + r.i * width, width);
            Assert.Equal(expected, r.xs);
        });
    }
}
