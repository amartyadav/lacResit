using System;
using System.Collections.Generic;
using Compiler.Tokenization;

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
        public bool HasErrors { get; set; }

        /// <summary>
        /// List to store all the errors
        /// </summary>
        public List<String> Errors = new List<string>();
        

        /// <summary>
        /// Add a new error to the error list
        /// </summary>
        public void AddError(String newErrorToken)
        {
            Errors.Add(newErrorToken);
            HasErrors = true;
        }

        /// <summary>
        /// Return the error list
        /// </summary>
        public List<String> GetErrorList()
        {
            return Errors;
        }
        
    }
}