using System.Text.RegularExpressions;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 14 of the coding challenge.
/// </summary>
public static class Day14
{
    private static readonly Dictionary<string, int> _distancesByReindeer = [];
    private const int TotalFlySeconds = 2503;

    /// <summary>
    /// Calculates the maximum distance flown by any participant based on the provided input data for Part 1 of the
    /// challenge.
    /// </summary>
    /// <param name="input">An array of strings, each representing a participant's flying and resting characteristics in the specified input
    /// format. Cannot be null.</param>
    /// <returns>A string representation of the greatest distance flown by any participant.</returns>
    public static string SolvePart1(string[] input)
    {
        foreach (string line in input)
        {
            (string olympic, int speedFly, int seconds, int rest) = ParseReindeerInfo(line);
            _distancesByReindeer[olympic] = CalculateTotalDistance(speedFly, seconds, rest);
        }
        return _distancesByReindeer.Values.Max().ToString();
    }

    /// <summary>
    /// Calculates the total score of the winning reindeer after simulating the race using the point-based scoring
    /// system described in Part 2 of the challenge.
    /// </summary>
    /// <param name="input">An array of strings, each representing a participant's flying and resting characteristics in the specified input
    /// format. Cannot be null.</param>
    /// <returns>A string representation of the highest score archived by any reindeer at the end of the race.</returns>
    public static string SolvePart2(string[] input)
    {
        var reindeers = new List<Day14_ReindeerState>();
        foreach (string line in input)
        {
            (string olympic, int speedFly, int seconds, int rest) = ParseReindeerInfo(line);
            reindeers.Add(new Day14_ReindeerState(name: olympic, speed: speedFly, flyTime: seconds, restTime: rest));
        }

        return SimulateReindeerRace(reindeers).ToString();
    }

    /// <summary>
    /// Simulates a reindeer race and determines the highest score achieved by any reindeer.
    /// </summary>
    /// <param name="reindeers">A list of reindeer states representing the participants in the race. Each reindeer state is updated throughout
    /// the simulation.</param>
    /// <returns>The maximum number of points earned by any reindeer at the end of the race.</returns>
    private static int SimulateReindeerRace(List<Day14_ReindeerState> reindeers)
    {
        for (int second = 1; second <= TotalFlySeconds; second++)
        {
            foreach (var reindeer in reindeers)
            {
                reindeer.UpdateState();
            }
            int maxDistanceThisSecond = reindeers.Max(r => r.DistanceTraveled);
            foreach (var reindeer in reindeers.Where(r => r.DistanceTraveled == maxDistanceThisSecond))
            {
                reindeer.Points++;
            }
        }

        return reindeers.Max(r => r.Points);
    }

    /// <summary>
    /// Parses a reindeer description string and extracts the name, flying speed, flying duration, and rest duration.
    /// </summary>
    /// <param name="input">The input string containing the reindeer's name, speed, flying time, and rest time in a specific textual format.
    /// Cannot be null.</param>
    /// <returns>A tuple containing the reindeer's name, flying speed in kilometers per second, flying duration in seconds, and
    /// rest duration in seconds.</returns>
    private static (string olympic, int speedFly, int seconds, int rest) ParseReindeerInfo(string input)
    {
        var match = Regex.Matches(input, @"(\d+)").Select(n => int.Parse(n.Value)).ToList();
        var splitString = input.Split(' ');

        int speedFly = match[0];
        int seconds = match[1];
        int rest = match[2];

        return (splitString.First(), speedFly, seconds, rest);
    }

    /// <summary>
    /// Calculates the total distance traveled based on flying speed, flying duration, and rest period per cycle.
    /// </summary>
    /// <param name="speedFly">The speed at which the entity flies, in units per second. Must be non-negative.</param>
    /// <param name="seconds">The number of seconds the entity spends flying in each cycle. Must be non-negative.</param>
    /// <param name="rest">The number of seconds the entity rests after each flying period. Must be non-negative.</param>
    /// <returns>The total distance traveled, in units, after completing all flying and resting cycles.</returns>
    private static int CalculateTotalDistance(int speedFly, int seconds, int rest)
    {
        int cycleDuration = seconds + rest;
        int fullCyclesFlyingTime = (TotalFlySeconds / cycleDuration) * seconds;
        int remainingFlyingTime = Math.Min(TotalFlySeconds % cycleDuration, seconds);
        return speedFly * (fullCyclesFlyingTime + remainingFlyingTime);
    }
}
