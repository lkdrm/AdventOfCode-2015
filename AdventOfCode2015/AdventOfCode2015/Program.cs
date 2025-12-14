using AdventOfCode2015.ResolvingDays;
using AdventOfCode2015.Tools;

#region Start
Console.Title = "Advent of code 2015";
var legendDefault = ConsoleColor.Green;
Console.ForegroundColor = legendDefault;
PrettyPrintExtensions.PrintWelcome();
#endregion

#region Day 1
PrettyPrintExtensions.PrintResult(1, 1, Day1.ResultPart1(ReadTaskExtensions.ReadText("Day1.txt", 1)));
PrettyPrintExtensions.PrintResult(1, 2, Day1.ResultPart2(ReadTaskExtensions.ReadText("Day1.txt", 1)));
#endregion

Console.ReadLine();

#region End
Console.CancelKeyPress += (sender, e) =>
{
    Console.ForegroundColor = legendDefault;
};
#endregion