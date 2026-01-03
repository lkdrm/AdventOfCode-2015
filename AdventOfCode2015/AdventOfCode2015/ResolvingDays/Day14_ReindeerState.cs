namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Represents the current state of a reindeer, including its name, speed, flying and resting durations, distance
/// traveled, and accumulated points during a race simulation.
/// </summary>
public class Day14_ReindeerState
{
    public string Name { get; set; }
    public int Speed { get; set; }
    public int FlyTime { get; set; }
    public int RestTime { get; set; }
    public int DistanceTraveled { get; set; }
    public int Points { get; set; }
    public int TimeInCycle { get; set; }

    /// <summary>
    /// Initializes a new instance of the Day14_ReindeerState class with the specified name, speed, flying time, and
    /// resting time.
    /// </summary>
    /// <param name="name">The name of the reindeer. Cannot be null or empty.</param>
    /// <param name="speed">The speed of the reindeer in kilometers per second. Must be a non-negative integer.</param>
    /// <param name="flyTime">The duration, in seconds, that the reindeer can fly before needing to rest. Must be a positive integer.</param>
    /// <param name="restTime">The duration, in seconds, that the reindeer must rest after flying. Must be a non-negative integer.</param>
    public Day14_ReindeerState(string name, int speed, int flyTime, int restTime)
    {
        Name = name;
        Speed = speed;
        FlyTime = flyTime;
        RestTime = restTime;
        DistanceTraveled = 0;
        Points = 0;
        TimeInCycle = 0;
    }

    /// <summary>
    /// Determines whether the entity is currently in the flying phase of its cycle.
    /// </summary>
    /// <returns><see langword="true"/> if the entity is flying; otherwise, <see langword="false"/>.</returns>
    public bool IsFlying() => TimeInCycle < FlyTime;

    /// <summary>
    /// Advances the object's state by one time unit, updating its distance traveled and cycle timing as appropriate.
    /// </summary>
    public void UpdateState()
    {
        if (IsFlying())
        {
            DistanceTraveled += Speed;
        }
        TimeInCycle++;

        int cycleTime = FlyTime + RestTime;

        if (TimeInCycle >= cycleTime)
        {
            TimeInCycle = 0;
        }
    }
}
