namespace AdventOfCode2015.Tools
{
    /// <summary>
    /// Provides extension methods and constants for formatting and displaying text in a visually distinctive,
    /// human-readable style.
    /// </summary>
    public static class PrettyPrintExtensions
    {
        private const string Welcome = "Advent of Code of 2015 year!";
        private const int StarWidth = 60;

        /// <summary>
        /// Writes the specified text to the console, centered and surrounded by a border of asterisks.
        /// </summary>
        public static void PrintWelcome()
        {
            Console.WriteLine();
            Console.WriteLine(new string('*', StarWidth));
            int padding = (StarWidth - Welcome.Length - 2) / 2;
            Console.WriteLine("*" + new string(' ', padding) + $"{Welcome} " + new string(' ', padding - 1) + "*");
            Console.WriteLine(new string('*', StarWidth));
            Console.WriteLine();
        }

        /// <summary>
        /// Writes a formatted result for the specified day to the console output.
        /// </summary>
        /// <param name="dayNumber">The day number to include in the result display. Typically represents the day of a challenge or event.</param>
        /// <param name="part">The part number of the day's challenge or task.</param>
        /// <param name="answer">The answer or result text to display for the specified day.</param>
        public static void PrintResult(int dayNumber, int part, string answer)
        {
            string answerText = $"DAY {dayNumber} PART:{part} ANSWER: {answer}";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(new string('=', StarWidth));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(CenterText(answerText, StarWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', StarWidth));
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Center text within a given width by adding spaces on both sides.
        /// </summary>
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
