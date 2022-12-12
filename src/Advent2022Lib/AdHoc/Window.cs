using System.Collections.Generic;

namespace Advent2022Lib.AdHoc;

public class XEnumerable
{
    public static IEnumerable<T[]> Window<T>(IEnumerable<T> xs, int width)
    {
        // NOTE: NOT cloned - just copied

        var result = new List<T[]>();

        var numWins = xs.Count() - width + 1;

        if (numWins <= 0)
        {
            throw new ArgumentException("Not enough data to create windows");
        }

        if (numWins == 1)
        {
            result.Add(xs.ToArray());
            return result;
        }

        // NOTE: We have at least two windows worth of data
        var windows = Enumerable.Range(0, numWins)
                        .ToList()
                        .Select(i => new List<T>())
                        .ToArray();

        var numTargets = 1;

        for (int start = 0; start < numWins; start++)
        {
            for (int ix = start; ix < start + width; ix++)
            {
                var x = xs.ElementAt(ix);

                // Add this `x` to the required number of lists
                for (int i = 0; i < numTargets; i++)
                {
                    windows[i].Add(x);
                    if (windows[i].Count == width)
                    {
                        result.Add(windows[i].ToArray());
                        windows[i].Clear();
                    }
                }

                numTargets = Math.Min(width, numTargets++);
            }
        }

        // Assert xs.Length >= width
        // num = xs.Legth - width + 1

        // Create all `num` windows of `width`
        // Loop over all items in `xs` as `x`

        // Add `x` to each window
        // When a window is full
        //   add it to the output list to the output
        //   clear this window
        // Keep going until all numbers have been processed

        // At end some windows will have partial contens - throw them away

        return result;

        // TODO: OR OR OR!!! No copying needed!

        // See - https://github.com/daniel-gustafsson/WindowToLinq/blob/master/WindowToLinq/Window.cs

        // Create an enumerator for each window range
        //   using the following
        //   win        range
        //   ----------------------------------
        //   0:         start, width
        //   1:         1+start, width
        //   num-2:     num-2+start, width
        //   num-1:     n-1+start, width
    }
}
