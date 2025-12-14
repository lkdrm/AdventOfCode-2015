using AdventOfCode2015.ResolvingDays;
using AdventOfCode2015.Tools;

#region Start
Console.Title = "Advent of code 2015";
var legendDefault = ConsoleColor.Green;
Console.ForegroundColor = legendDefault;
PrettyPrintExtensions.PrintWelcome();
#endregion

#region Day 1: Not Quite Lisp
PrettyPrintExtensions.PrintResult("Day 1: Not Quite Lisp", 1, Day1.ResultPart1(ReadTaskExtensions.ReadText("Day1.txt", 1)));
PrettyPrintExtensions.PrintResult("Day 1: Not Quite Lisp", 2, Day1.ResultPart2(ReadTaskExtensions.ReadText("Day1.txt", 1)));
#endregion

#region Day 2: I Was Told There Would Be No Math
PrettyPrintExtensions.PrintResult("Day 2: I Was Told There Would Be No Math", 1, Day2.ResultPart1(ReadTaskExtensions.ReadTextLines("Day2.txt", 2)));
PrettyPrintExtensions.PrintResult("Day 2: I Was Told There Would Be No Math", 2, Day2.ResultPart2(ReadTaskExtensions.ReadTextLines("Day2.txt", 2)));
#endregion

Console.ReadLine();

#region End
Console.CancelKeyPress += (sender, e) =>
{
    Console.ForegroundColor = legendDefault;
};
#endregion