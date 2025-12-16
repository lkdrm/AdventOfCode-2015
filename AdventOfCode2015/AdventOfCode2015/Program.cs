using AdventOfCode2015.ResolvingDays;
using AdventOfCode2015.Tools;

#region Start
Console.Title = "Advent of code 2015";
var legendDefault = ConsoleColor.Green;
Console.ForegroundColor = legendDefault;
PrettyPrintExtensions.PrintWelcome();
#endregion

#region Day 1: Not Quite Lisp
string day1Part1 = Day1.SolvePart1(ReadTaskExtensions.ReadText("Day1.txt", 1));
string day1Part2 = Day1.SolvePart2(ReadTaskExtensions.ReadText("Day1.txt", 1));
PrettyPrintExtensions.PrintResult("Not Quite Lisp", 1, day1Part1, day1Part2);
#endregion

#region Day 2: I Was Told There Would Be No Math
string day2Part1 = Day2.SolvePart1(ReadTaskExtensions.ReadTextLines("Day2.txt", 2));
string day2Part2 = Day2.SolvePart2(ReadTaskExtensions.ReadTextLines("Day2.txt", 2));
PrettyPrintExtensions.PrintResult("I Was Told There Would Be No Math", 2, day2Part1, day2Part2);
#endregion

#region Day 3: Perfectly Spherical Houses in a Vacuum
string day3Part1 = Day3.SolvePart1(ReadTaskExtensions.ReadText("Day3.txt", 3));
string day3Part2 = Day3.SolvePart2(ReadTaskExtensions.ReadText("Day3.txt", 3));
PrettyPrintExtensions.PrintResult("Perfectly Spherical Houses in a Vacuum", 3, day3Part1, day3Part2);
#endregion

#region Day 4: The Ideal Stocking Stuffer
string day4Part1 = Day4.SolvePart1(ReadTaskExtensions.ReadText("Day4.txt", 4));
string day4Part2 = Day4.SolvePart2(ReadTaskExtensions.ReadText("Day4.txt", 4));
PrettyPrintExtensions.PrintResult("The Ideal Stocking Stuffer", 4, day4Part1, day4Part2);
#endregion

Console.ReadLine();
#region End
Console.CancelKeyPress += (sender, e) =>
{
    Console.ForegroundColor = legendDefault;
};
#endregion