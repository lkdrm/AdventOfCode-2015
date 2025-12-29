using System.Diagnostics;

namespace AdventOfCode2015.Tools;

/// <summary>
/// Provides extension methods for measuring elapsed time using a high-resolution timer.
/// </summary>
public static class TimerExtension
{
    private static readonly Stopwatch Stopwatch = new();

    /// <summary>
    /// Starts or restarts the timer, resetting the elapsed time to zero.
    /// </summary>
    public static void Start() => Stopwatch.Restart();

    /// <summary>
    /// Stops the underlying stopwatch and returns the total elapsed time in seconds.
    /// </summary>
    /// <returns>The total number of seconds that have elapsed since the stopwatch was started, including fractional seconds.</returns>
    public static double StopAndGetSeconds() => Stopwatch.Elapsed.TotalSeconds;
}
