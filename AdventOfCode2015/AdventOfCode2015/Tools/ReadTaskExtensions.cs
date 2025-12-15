using System.Reflection;

namespace AdventOfCode2015.Tools
{
    /// <summary>
    /// Provides extension methods for reading text files related to task processing.
    /// </summary>
    public static class ReadTaskExtensions
    {
        /// <summary>
        /// Represents the format string used to construct folder paths for daily tasks.
        /// </summary>
        private const string FolderDestionationFormat = "Tasks/Day{0}/";

        /// <summary>
        /// This method gets the current directory of the assembly.
        /// </summary>
        private static readonly string CurrentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        /// <summary>
        /// This method reads the input file and returns the lines as an array of strings.
        /// </summary>
        /// <param name="currentDayTask">Current day secret</param>
        /// <param name="numberOfDay">Number of the day</param>
        /// <returns>Array of strings</returns>
        public static string[] ReadTextLines(string currentDayTask, int numberOfDay) => File.ReadAllLines(Path.Combine(CurrentDirectory, string.Format(FolderDestionationFormat, numberOfDay), currentDayTask));

        /// <summary>
        /// This method reads the input file and returns the lines of strings.
        /// </summary>
        /// <param name="currentDayTask">Current day secret</param>
        /// <param name="numberOfDay">Number of the day</param>
        /// <returns>Array of strings</returns>
        public static string ReadText(string currentDayTask, int numberOfDay) => File.ReadAllText(Path.Combine(CurrentDirectory, string.Format(FolderDestionationFormat, numberOfDay), currentDayTask));
    }
}
