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
double elapsedPart1Day1 = TimerExtension.StopAndGetSeconds();
string day1Part2 = Day1.SolvePart2(ReadTaskExtensions.ReadText("Day1.txt", 1));
double elapsedPart2Day1 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("Not Quite Lisp", 1, day1Part1, day1Part2, elapsedPart2Day1, elapsedPart2Day1);
#endregion

#region Day 2: I Was Told There Would Be No Math
string day2Part1 = Day2.SolvePart1(ReadTaskExtensions.ReadTextLines("Day2.txt", 2));
double elapsedPart1Day2 = TimerExtension.StopAndGetSeconds();
string day2Part2 = Day2.SolvePart2(ReadTaskExtensions.ReadTextLines("Day2.txt", 2));
double elapsedPart2Day2 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("I Was Told There Would Be No Math", 2, day2Part1, day2Part2, elapsedPart1Day2, elapsedPart2Day2);
#endregion

#region Day 3: Perfectly Spherical Houses in a Vacuum
string day3Part1 = Day3.SolvePart1(ReadTaskExtensions.ReadText("Day3.txt", 3));
double elapsedPart1Day3 = TimerExtension.StopAndGetSeconds();
string day3Part2 = Day3.SolvePart2(ReadTaskExtensions.ReadText("Day3.txt", 3));
double elapsedPart2Day3 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("Perfectly Spherical Houses in a Vacuum", 3, day3Part1, day3Part2, elapsedPart1Day3, elapsedPart2Day3);
#endregion

#region Day 4: The Ideal Stocking Stuffer
string day4Part1 = Day4.SolvePart1(ReadTaskExtensions.ReadText("Day4.txt", 4));
double elapsedPart1Day4 = TimerExtension.StopAndGetSeconds();
string day4Part2 = Day4.SolvePart2(ReadTaskExtensions.ReadText("Day4.txt", 4));
double elapsedPart2Day4 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("The Ideal Stocking Stuffer", 4, day4Part1, day4Part2, elapsedPart1Day4, elapsedPart2Day4);
#endregion

#region Day 5: Doesn't He Have Intern-Elves For This?
string day5Part1 = Day5.SolvePart1(ReadTaskExtensions.ReadTextLines("Day5.txt", 5));
double elapsedPart1Day5 = TimerExtension.StopAndGetSeconds();
string day5Part2 = Day5.SolvePart2(ReadTaskExtensions.ReadTextLines("Day5.txt", 5));
double elapsedPart2Day5 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("Doesn't He Have Intern-Elves For This?", 5, day5Part1, day5Part2, elapsedPart1Day5, elapsedPart2Day5);
#endregion

#region Day 6: Probably a Fire Hazard
string day6Part1 = Day6.SolvePart1(ReadTaskExtensions.ReadTextLines("Day6.txt", 6));
double elapsedPart1Day6 = TimerExtension.StopAndGetSeconds();
string day6Part2 = Day6.SolvePart2(ReadTaskExtensions.ReadTextLines("Day6.txt", 6));
double elapsedPart2Day6 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("Probably a Fire Hazard", 6, day6Part1, day6Part2, elapsedPart1Day6, elapsedPart2Day6);
#endregion

#region Day 7: Some Assembly Required
string day7Part1 = Day7.SolvePart1(ReadTaskExtensions.ReadTextLines("Day7.txt", 7));
double elapsedPart1Day7 = TimerExtension.StopAndGetSeconds();
string day7Part2 = Day7.SolvePart2(ReadTaskExtensions.ReadTextLines("Day7.txt", 7));
double elapsedPart2Day7 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("Some Assembly Required", 7, day7Part1, day7Part2, elapsedPart1Day7, elapsedPart2Day7);
#endregion

#region Day 8: Matchsticks
string day8Part1 = Day8.SolvePart1(ReadTaskExtensions.ReadTextLines("Day8.txt", 8));
double elapsedPart1Day8 = TimerExtension.StopAndGetSeconds();
string day8Part2 = Day8.SolvePart2(ReadTaskExtensions.ReadTextLines("Day8.txt", 8));
double elapsedPart2Day8 = TimerExtension.StopAndGetSeconds();
PrettyPrintExtensions.PrintResult("Day 8: Matchsticks", 8, day8Part1, day8Part2, elapsedPart1Day8, elapsedPart2Day8);
#endregion

Console.ReadLine();
#region End
Console.CancelKeyPress += (sender, e) =>
{
    Console.ForegroundColor = legendDefault;
};
#endregion