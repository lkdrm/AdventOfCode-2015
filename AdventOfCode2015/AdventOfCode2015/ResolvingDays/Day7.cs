using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 7 of the coding challenge.
/// </summary>
public static class Day7
{
    private const int UShortMask = 0xFFFF;
    private static readonly Dictionary<string, ushort> _cache = new();
    private static readonly Dictionary<string, string> _instructions = new();

    /// <summary>
    /// Processes the input instructions and returns the signal value for wire 'a' as a string.
    /// </summary>
    /// <param name="input">An array of strings representing the circuit instructions to be evaluated. Each string should define a wire
    /// assignment or operation.</param>
    /// <returns>A string representation of the computed signal value for wire 'a'.</returns>
    public static string SolvePart1(string[] input)
    {
        TimerExtension.Start();
        foreach (var line in input)
        {
            ParseAttributeValues(line);
        }
        return GetSignal("a").ToString();
    }

    /// <summary>
    /// Solves the second part of the puzzle using the provided input and returns the signal value for wire 'a' as a
    /// string.
    /// </summary>
    /// <param name="input">An array of strings representing the puzzle input instructions.</param>
    /// <returns>A string representation of the signal value for wire 'a' after applying the modified instructions for part 2.</returns>
    public static string SolvePart2(string[] input)
    {
        TimerExtension.Start();
        var b = SolvePart1(input);
        _instructions["b"] = b;
        _cache.Clear();
        return GetSignal("a").ToString();
    }

    /// <summary>
    /// Parses an instruction line and updates the internal instruction mapping with the extracted attribute and its
    /// corresponding value.
    /// </summary>
    /// <param name="line">The instruction line to parse, expected in the format "value -> attribute". Cannot be null.</param>
    private static void ParseAttributeValues(string line)
    {
        var parts = line.Split(" -> ");
        _instructions[parts[1]] = parts[0];
    }

    /// <summary>
    /// Calculates the signal value for the specified wire based on the defined instructions and any previously computed
    /// results.
    /// </summary>
    /// <param name="wire">The name of the wire for which to compute the signal value. This can be a wire identifier or a numeric value
    /// represented as a string.</param>
    /// <returns>The computed signal value for the specified wire as an unsigned 16-bit integer.</returns>
    private static ushort GetSignal(string wire)
    {
        // If already computed, return cached value
        if (_cache.TryGetValue(wire, out var chachedValue))
        {
            return chachedValue;
        }

        // Compute the value
        if (ushort.TryParse(wire, out ushort value))
        {
            return value;
        }

        // Get the instruction for the wire
        _instructions.TryGetValue(wire, out var instruction);

        var splitWire = instruction.Split(' ');
        ushort result = 0;

        // Evaluate based on the instruction format
        if (splitWire.Length == 1)
        {
            result = GetSignal(splitWire.First());
        }
        else if (splitWire.First() == "NOT")
        {
            result = (ushort)(~GetSignal(splitWire[1]) & UShortMask);
        }
        else
        {
            var leftOperand = GetSignal(splitWire[0]);
            var rightOperand = GetSignal(splitWire[2]);
            result = splitWire[1] switch
            {
                "AND" => (ushort)(leftOperand & rightOperand),
                "OR" => (ushort)(leftOperand | rightOperand),
                "LSHIFT" => (ushort)(leftOperand << int.Parse(splitWire[2])),
                "RSHIFT" => (ushort)(leftOperand >> int.Parse(splitWire[2])),
                _ => throw new NotImplementedException(),
            };
        }
        _cache[wire] = result;
        return result;
    }
}
