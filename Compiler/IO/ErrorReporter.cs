using System;

namespace Compiler.IO
{
    /// <summary>
    /// An object for reporting errors in the compilation process
    /// </summary>
    public class ErrorReporter
    {
        /// <summary>
        /// Whether or not any errors have been encountered
        /// </summary>
        public bool HasErrors { get; }
    }
}