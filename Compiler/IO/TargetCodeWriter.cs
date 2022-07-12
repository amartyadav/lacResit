using Compiler.CodeGeneration;
using System;
using System.IO;

namespace Compiler.IO
{
    /// <summary>
    /// A writer of compiled code
    /// </summary>
    public class TargetCodeWriter
    {
        /// <summary>
        /// The file to write the executable target code to
        /// </summary>
        public string BinaryOutputFile { get; }

        /// <summary>
        /// The file to write the human-readable version of the target code to
        /// </summary>
        public string TextOutputFile { get; }

        /// <summary>
        /// The error reporter
        /// </summary>
        public ErrorReporter Reporter { get; }

        /// <summary>
        /// Creates a new output writer
        /// </summary>
        /// <param name="binaryOutputFile">The file to write the excutable to</param>
        /// <param name="textOutputFile">The file to write the human-readable code to</param>
        /// <param name="reporter">The error reporter to use</param>
        public TargetCodeWriter(string binaryOutputFile, string textOutputFile, ErrorReporter reporter)
        {
            BinaryOutputFile = binaryOutputFile;
            TextOutputFile = textOutputFile;
            Reporter = reporter;
        }

        /// <summary>
        /// Writes the compiled code to the output files
        /// </summary>
        /// <param name="targetCode">The code to write to files</param>
        public void WriteToFiles(TargetCode targetCode)
        {
            try
            {
                File.WriteAllBytes(BinaryOutputFile, targetCode.ToBinary());
            }
            catch (Exception ex)
            {
                // Error: Problem writing binary output file
            }
            try
            {
                File.WriteAllText(TextOutputFile, targetCode.ToString());
            }
            catch (Exception ex)
            {
                // Error: Problem writing text output file
            }
        }
    }
}
