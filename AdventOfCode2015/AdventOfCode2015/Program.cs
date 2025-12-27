using AdventOfCode2015.ResolvingDays;
using AdventOfCode2015.Tools;

#region Start
Console.Title = "Advent of code 2015";
var legendDefault = ConsoleColor.Green;
Console.ForegroundColor = legendDefault;
PrettyPrintExtensions.PrintWelcome();
#endregion

var days = new (int Day, string Title, string FileName, bool UseLines, Func<string[], string> Part1, Func<string[], string> Part2)[]
{
    (1, "Not Quite Lisp", "Day1.txt", false, input => Day1.SolvePart1(input[0]), input => Day1.SolvePart2(input[0])),
    (2, "I Was Told There Would Be No Math", "Day2.txt", true, Day2.SolvePart1, Day2.SolvePart2),
    (3, "Perfectly Spherical Houses in a Vacuum", "Day3.txt", false, input => Day3.SolvePart1(input[0]), input => Day3.SolvePart2(input[0])),
    (4, "The Ideal Stocking Stuffer", "Day4.txt", false, input => Day4.SolvePart1(input[0]), input => Day4.SolvePart2(input[0])),
    (5, "Doesn't He Have Intern-Elves For This?", "Day5.txt", true, Day5.SolvePart1, Day5.SolvePart2),
    (6, "Probably a Fire Hazard", "Day6.txt", true, Day6.SolvePart1, Day6.SolvePart2),
    (7, "Some Assembly Required", "Day7.txt", true, Day7.SolvePart1, Day7.SolvePart2),
    (8, "Matchsticks", "Day8.txt", true, Day8.SolvePart1, Day8.SolvePart2),
    (9, "All in a Single Night", "Day9.txt", true, Day9.SolvePart1, Day9.SolvePart2),
    (10, "Elves Look, Elves Say", "Day10.txt", false, input => Day10.SolvePart1(input[0]), input => Day10.SolvePart2(input[0]))
};

for (int i = 0; i < days.Length; i++)
{
    var (Day, Title, FileName, UseLines, Part1, Part2) = days[i];
    var input = UseLines
        ? ReadTaskExtensions.ReadTextLines(FileName, Day)
        : [ReadTaskExtensions.ReadText(FileName, Day)];

    double elapsedPart1 = TimerExtension.StopAndGetSeconds();
    double elapsedPart2 = TimerExtension.StopAndGetSeconds();
    PrettyPrintExtensions.PrintResult(Title, Day, Part1(input), Part2(input), elapsedPart1, elapsedPart2);
}

Console.ReadLine();
#region End
Console.CancelKeyPress += (sender, e) =>
{
    Console.ForegroundColor = legendDefault;
};
#endregion