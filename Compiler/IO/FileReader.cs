using System;
using System.IO;

namespace Compiler.IO
{
    /// <summary>
    /// Simplifies reading characters from a file
    /// </summary>
    public class FileReader : IFileReader
    {
        /// <summary>
        /// The underlying file reader
        /// </summary>
        public StreamReader Reader { get; }

        /// <summary>
        /// The text on the current line being processed
        /// </summary>
        private string currentLine;

        /// <summary>
        /// Whether the reader has reached the end of the file
        /// </summary>
        private bool AtEndOfFile => currentLine == null;

        /// <summary>
        /// The line number of the line currently being processed
        /// </summary>
        private int currentLineNumber = 0;

        /// <summary>
        /// The index of the current character in the line being processed
        /// </summary>
        private int currentPositionInLine = 0;

        /// <summary>
        /// The current character in the file
        /// </summary>
        /// <remarks>If the end of file has been reached then returns default(char)</remarks>
        public char Current { get { return AtEndOfFile ? default : currentLine[currentPositionInLine - 1]; } }

        /// <summary>
        /// The position of the current character in the file
        /// </summary>
        /// <remarks>Lines and columns both start from 1, not 0</remarks>
        public Position CurrentPosition { get { return new Position(currentLineNumber, currentPositionInLine); } }

        /// <summary>
        /// Move to the next character in the file
        /// </summary>
        /// <returns>True if there is another character in the file or false if the end of file has been reached</returns>
        public bool MoveNext()
        {
            if (!AtEndOfFile)
                if (currentPositionInLine < currentLine.Length)
                    currentPositionInLine++;
                else
                    ReadNextLine();
            return !AtEndOfFile;
        }

        /// <summary>
        /// Skips forward to the beginning of the next line
        /// </summary>
        public void SkipRestOfLine()
        {
            if (!AtEndOfFile)
                ReadNextLine();
        }

        /// <summary>
        /// Reads the next line from the file
        /// </summary>
        private void ReadNextLine()
        {
            currentLine = Reader.ReadLine();
            if (currentLine != null)
                currentLine += "\n";
            currentLineNumber += 1;
            currentPositionInLine = 1;
        }

        /// <summary>
        /// Closes the file
        /// </summary>
        public void Close()
        {
            Reader.Close();
        }

        /// <summary>
        /// Creates a new file reader
        /// </summary>
        /// <param name="inputFile">The file to read from</param>
        public FileReader(string inputFile)
        {
            Reader = new StreamReader(inputFile);
            ReadNextLine();
        }
    }
}