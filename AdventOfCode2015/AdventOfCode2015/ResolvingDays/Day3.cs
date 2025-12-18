using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 3 of the coding challenge.
/// </summary>
public static class Day3
{
    // Represents a position on the 2D grid
    // 1. Value semantics (comparing by value, not reference)
    // 2. Automatic equality comparison (needed for HashSet)
    // 3. Immutability (readonly properties)
    private readonly record struct Position(int X, int Y);

    // HashSet automatically handles uniqueness - we can't add duplicates
    // This is perfect for tracking which houses have been visited
    private static HashSet<Position> _visitedHouses;

    // Current position of Santa on the grid
    private static Position _currentSantaPosition;

    // Current position of Robo-Santa on the grid
    private static Position _currentSantaRoboPosition;

    /// <summary>
    /// Calculates the number of unique locations visited based on the specified delivery route instructions.
    /// </summary>
    /// <param name="input">A string containing movement instructions for the delivery route. Each character represents a direction to move.</param>
    /// <returns>A string representation of the total number of unique locations visited after processing the delivery route.</returns>
    public static string SolvePart1(string input)
    {
        TimerExtension.Start();
        _currentSantaPosition = new Position(0, 0);
        _visitedHouses = new HashSet<Position> { _currentSantaPosition };

        var result = ProcessDeliveryRoute(input);

        return result.ToString();
    }

    /// <summary>
    /// Calculates the number of unique houses visited by Santa and Robo-Santa based on the provided movement
    /// instructions.
    /// </summary>
    /// <param name="input">A string containing movement instructions, where each character represents a direction for Santa or Robo-Santa
    /// to move. Valid characters typically include '^', 'v', '<', and '>'.</param>
    /// <returns>A string representation of the total number of unique houses visited by either Santa or Robo-Santa after
    /// following all movement instructions.</returns>
    public static string SolvePart2(string input)
    {
        TimerExtension.Start();
        _currentSantaPosition = new Position(0, 0);
        _currentSantaRoboPosition = new Position(0, 0);
        _visitedHouses = new HashSet<Position> { new Position(0, 0) };

        // Process each character in the directions string
        for (int move = 0; move < input.Length; move++)
        {
            // Alternate moves between Santa and Robo-Santa
            if (move % 2 == 0)
            {
                // Santa` turn
                _currentSantaPosition = MoveToNextHouse(_currentSantaPosition, input[move]);
                _visitedHouses.Add(_currentSantaPosition);
            }
            else
            {
                // Robo-Santa`s turn
                _currentSantaRoboPosition = MoveToNextHouse(_currentSantaRoboPosition, input[move]);
                _visitedHouses.Add(_currentSantaRoboPosition);
            }
        }
        return _visitedHouses.Count.ToString();
    }

    /// <summary>
    /// Processes a series of directional instructions and tracks unique houses visited.
    /// </summary>
    /// <param name="directions">String containing direction characters (^, v, >, <)</param>
    /// <returns>The total number of unique houses that received at least one present</returns>
    private static int ProcessDeliveryRoute(string directions)
    {
        // Process each character in the directions string
        foreach (char direction in directions)
        {
            // Move Santa to the next position based on the direction
            MoveToNextHouse(direction);

            // Add the new position to the visited set
            // If it's already there, HashSet.Add() simply returns false and does nothing
            // This is why we don't need to check "if not visited" - HashSet handles it
            _visitedHouses.Add(_currentSantaPosition);
        }

        // The count of unique houses is simply the size of our HashSet
        return _visitedHouses.Count;
    }

    /// <summary>
    /// Updates Santa's position based on a direction character.
    /// North/South affect Y coordinate, East/West affect X coordinate.
    /// </summary>
    /// <param name="direction">The direction character (^, v, >, <)</param>
    private static void MoveToNextHouse(char direction)
    {
        // We use a coordinate system where:
        // - X increases going East (right), decreases going West (left)
        // - Y increases going North (up), decreases going South (down)
        // This is a standard Cartesian coordinate system

        _currentSantaPosition = direction switch
        {
            '^' => new Position(_currentSantaPosition.X, _currentSantaPosition.Y + 1),  // North: Y increases
            'v' => new Position(_currentSantaPosition.X, _currentSantaPosition.Y - 1),  // South: Y decreases
            '>' => new Position(_currentSantaPosition.X + 1, _currentSantaPosition.Y),  // East: X increases
            '<' => new Position(_currentSantaPosition.X - 1, _currentSantaPosition.Y),  // West: X decreases

            // If we encounter an unexpected character, stay at the current position
            // In production code, you might want to throw an exception here
            _ => _currentSantaPosition
        };
    }

    /// <summary>
    /// Updates Santa's position based on a direction character.
    /// North/South affect Y coordinate, East/West affect X coordinate.
    /// </summary>
    /// <param name="direction">The direction character (^, v, >, <)</param>
    private static Position MoveToNextHouse(Position position, char direction)
    {
        // We use a coordinate system where:
        // - X increases going East (right), decreases going West (left)
        // - Y increases going North (up), decreases going South (down)
        // This is a standard Cartesian coordinate system

        return direction switch
        {
            '^' => new Position(position.X, position.Y + 1),  // North: Y increases
            'v' => new Position(position.X, position.Y - 1),  // South: Y decreases
            '>' => new Position(position.X + 1, position.Y),  // East: X increases
            '<' => new Position(position.X - 1, position.Y),  // West: X decreases

            // If we encounter an unexpected character, stay at the current position
            // In production code, you might want to throw an exception here
            _ => position
        };
    }
}
