using System.Diagnostics;

namespace AdventOfCode2015.Tools;

/// <summary>
/// Provides extension methods for measuring elapsed time using a high-resolution timer.
/// </summary>
public static class TimerExtension
{
    private static readonly Stopwatch Stopwatch = new();

    /// <summary>
    /// Gets the total elapsed time, in seconds, since the timer started.
    /// </summary>
    public static double ElapsedSeconds => Stopwatch.Elapsed.TotalSeconds;

    /// <summary>
    /// Starts or restarts the timer, resetting the elapsed time to zero.
    /// </summary>
    public static void Start() => Stopwatch.Restart();

    /// <summary>
    /// Stops the timer and returns the total elapsed time in seconds.
    /// </summary>
    /// <returns>The total number of seconds that have elapsed since the timer was started.</returns>
    public static double StopAndGetSeconds()
    {
        Stopwatch.Stop();
        return ElapsedSeconds;
    }
}
