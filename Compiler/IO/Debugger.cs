namespace Compiler.IO
{
    /// <summary>
    /// Writes debugging logs
    /// </summary>
    public static class Debugger
    {
        /// <summary>
        /// Whether to write debugging logs
        /// </summary>
        private const bool DEBUG = false;

        /// <summary>
        /// Write a debugging message if the logging is turned on
        /// </summary>
        /// <param name="message">The message to write to the screen</param>
        public static void Write(string message)
        {
            if (DEBUG)
                System.Console.WriteLine($"DEBUGGING INFO: {message}");
        }
    }
}
