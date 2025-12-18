using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 2 of the coding challenge.
/// </summary>
public static class Day2
{
    /// <summary>
    /// Calculates the total required wrapping paper for a set of box dimensions.
    /// </summary>
    /// <param name="input">An array of strings, each representing the dimensions of a box in the format "LxWxH", where L, W, and H are
    /// integers specifying the length, width, and height.</param>
    /// <returns>A string representation of the total square feet of wrapping paper needed for all boxes in the input.</returns>
    public static string SolvePart1(string[] input)
    {
        TimerExtension.Start();
        var result = 0;
        foreach (var line in input)
        {
            var splitString = line.Split("x");
            var (length, width, height) = (int.Parse(splitString[0]), int.Parse(splitString[1]), int.Parse(splitString[2]));

            var findArea = 2 * (length * width + width * height + height * length);
            var smallestArea = Math.Min(Math.Min(length * width, width * height), height * length);
            result += findArea + smallestArea;
        }
        return result.ToString();
    }

    /// <summary>
    /// Calculates the total required wrapping paper for a set of box dimensions.
    /// </summary>
    /// <param name="input">An array of strings, each representing the dimensions of a box in the format "LxWxH", where L, W, and H are
    /// integers specifying the length, width, and height.</param>
    /// <returns>A string representation of the total square feet of wrapping paper needed for all boxes in the input.</returns>
    public static string SolvePart2(string[] input)
    {
        TimerExtension.Start();
        var result = 0;
        foreach (var line in input)
        {
            var splitString = line.Split("x");
            var (length, width, height) = (int.Parse(splitString[0]), int.Parse(splitString[1]), int.Parse(splitString[2]));

            var v = length * width * height;
            int[] findRibbon = [.. splitString.Select(int.Parse).Order()];
            result += v + (2 * findRibbon[0] + 2 * findRibbon[1]);
        }
        return result.ToString();
    }
}
