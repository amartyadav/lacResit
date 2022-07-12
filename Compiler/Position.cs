namespace Compiler
{
    /// <summary>
    /// A position in a file
    /// </summary>
    public class Position
    {
        /// <summary>
        /// The line number in the file
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// The index position in the line of the file
        /// </summary>
        public int PositionInLine { get; }

        /// <summary>
        /// Creates a new file position
        /// </summary>
        /// <param name="lineNumber">The line number in the file</param>
        /// <param name="positionInLine">The index position in the line of the file</param>
        public Position(int lineNumber, int positionInLine)
        {
            LineNumber = lineNumber;
            PositionInLine = positionInLine;
        }

        /// <inheritDoc />
        public override string ToString()
        {
            if (this == BuiltIn)
                return "System defined";
            else
                return $"Line {LineNumber}, Column {PositionInLine}";
        }

        /// <summary>
        /// A constant to use as the position of system defined items
        /// </summary>
        public static Position BuiltIn { get; } = new Position(-1, -1);
    }
}