namespace AdventOfCode2015.Tools
{
    /// <summary>
    /// Provides extension methods and constants for formatting and displaying text in a visually distinctive,
    /// human-readable style.
    /// </summary>
    public static class PrettyPrintExtensions
    {
        private const int StarWidth = 60;

        /// <summary>
        /// Writes the specified text to the console, centered and surrounded by a border of asterisks.
        /// </summary>
        public static void PrintWelcome()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CenterText("*    *    *", StarWidth));
            Console.WriteLine(CenterText("  *    *", StarWidth));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CenterText("*** ADVENT OF CODE ***", StarWidth));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(CenterText("~ 2015 ~", StarWidth));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CenterText("<< Ho Ho Ho! >>", StarWidth));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CenterText("  *    *", StarWidth));
            Console.WriteLine(CenterText("*    *    *", StarWidth));
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Writes a formatted result for the specified day to the console output.
        /// </summary>
        /// <param name="title">The title or description of the day's challenge or task.</param>
        /// <param name="dayNumber">The day number of the challenge or task.</param>
        /// <param name="part1Answer">The answer or result for part 1 of the challenge.</param>
        /// <param name="part2Answer">The answer or result for part 2 of the challenge.</param>
        public static void PrintResult(string title, int dayNumber, string part1Answer, string part2Answer)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Day {dayNumber}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Part 1: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(part1Answer);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" | Part 2: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(part2Answer);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Centers the specified text within a field of a given width by adding padding spaces to both sides.
        /// </summary>
        /// <param name="text">The text to center. If the length of the text exceeds the specified width, the text is truncated.</param>
        /// <param name="width">The total width of the resulting string, including padding. Must be greater than or equal to zero.</param>
        /// <returns>A new string containing the centered text padded with spaces to achieve the specified width. If the text is
        /// longer than the width, a substring of the specified width is returned.</returns>
        private static string CenterText(string text, int width)
        {
            if (text.Length > width)
            {
                return text[..width];
            }
            int leftPadding = (width - text.Length) / 2;
            int rightPedding = width - text.Length - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPedding);
        }
    }
}
