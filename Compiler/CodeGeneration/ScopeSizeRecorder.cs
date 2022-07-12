using Compiler.IO;
using System.Collections.Generic;

namespace Compiler.CodeGeneration
{
    /// <summary>
    /// Records how much space is allocated at each local scope
    /// </summary>
    public class ScopeSizeRecorder
    {
        /// <summary>
        /// The error reporter
        /// </summary>
        public ErrorReporter Reporter { get; }

        /// <summary>
        /// The number of words of memory allocated at each scope
        /// </summary>
        /// <remarks>
        /// The most local scope is on top of the stack and the global scope is at the bottom
        /// </remarks>
        private Stack<short> DeclarationsSize { get; } = new Stack<short>();

        /// <summary>
        /// Creates a new scope size recorder
        /// </summary>
        /// <param name="reporter">The error reporter</param>
        public ScopeSizeRecorder(ErrorReporter reporter)
        {
            Reporter = reporter;
        }

        /// <summary>
        /// Adds a new local scope
        /// </summary>
        public void AddScope() { DeclarationsSize.Push(0); }

        /// <summary>
        /// Removes the information about the local scope
        /// </summary>
        public void RemoveScope() { DeclarationsSize.Pop(); }

        /// <summary>
        /// Gets the current size of the local scope
        /// </summary>
        /// <returns>The number of words of memory allocated in the current local scope</returns>
        public short GetLocalScopeSize() { return DeclarationsSize.Peek(); }

        /// <summary>
        /// Adds an extra delcaration to the local scope
        /// </summary>
        /// <param name="size">The size of the extra space allocated in words</param>
        public void AddToLocalScope(byte size)
        {
            short currentSize = DeclarationsSize.Pop();
            int newSize = currentSize + size;
            if (newSize <= short.MaxValue)
                DeclarationsSize.Push((short)newSize);
            else
            {
                DeclarationsSize.Push(short.MaxValue);
                // Error: Too much memory taken by local declarations
            }
        }
    }
}
