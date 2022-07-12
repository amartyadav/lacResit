using Compiler.IO;
using Compiler.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SemanticAnalysis
{
    /// <summary>
    /// A symbol table for use in symbol identification
    /// </summary>
    public class SymbolTable
    {
        /// <summary>
        /// A stack of currently open scopes, with the most local scope at the top of the stack
        /// </summary>
        private Stack<Dictionary<string, IDeclarationNode>> Scopes { get; }

        /// <summary>
        /// Creates a new symbol table
        /// </summary>
        public SymbolTable()
        {
            Scopes = new Stack<Dictionary<string, IDeclarationNode>>();
            Scopes.Push(new Dictionary<string, IDeclarationNode>());
        }

        /// <summary>
        /// Opens a new local scope
        /// </summary>
        public void OpenScope()
        {
            Debugger.Write("Opening a scope");
            Scopes.Push(new Dictionary<string, IDeclarationNode>());
        }

        /// <summary>
        /// Closes the local scope
        /// </summary>
        public void CloseScope()
        {
            Debugger.Write("Closing a scope");
            if (Scopes.Count == 0) throw new InvalidOperationException("Trying to close a scope but no scopes are open");
            Scopes.Pop();
        }

        /// <summary>
        /// Enters a symbol in the table
        /// </summary>
        /// <param name="symbol">The symbol to enter</param>
        /// <param name="declaration">The declaration of the symbol</param>
        /// <returns>True if the symbol could be entered or false if an error occurred</returns>
        public bool Enter(string symbol, IDeclarationNode declaration)
        {
            Debugger.Write($"Adding {symbol} to the symbol table");
            Dictionary<string, IDeclarationNode> currentScope = Scopes.Peek();
            if (currentScope.ContainsKey(symbol))
            {
                Debugger.Write($"{symbol} was already declared in the current scope");
                return false;
            }
            else
            {
                Debugger.Write($"Successfully added {symbol} to the current scope");
                currentScope.Add(symbol, declaration);
                return true;
            }
        }

        /// <summary>
        /// Looks up a symbol in the table
        /// </summary>
        /// <param name="symbol">The symbol to lookup</param>
        /// <returns>
        /// The declaration associated with the symbol at the most local level at which it is found, 
        /// or null if it is not currently in scope
        /// </returns>
        public IDeclarationNode Retrieve(string symbol)
        {
            Debugger.Write($"Looking up {symbol} in the symbol table");
            foreach (Dictionary<string, IDeclarationNode> scope in Scopes)
            {
                if (scope.TryGetValue(symbol, out IDeclarationNode declaration))
                {
                    Debugger.Write($"Found {symbol} in the symbol table");
                    return declaration;
                }
            }
            Debugger.Write($"{symbol} is not in the symbol table");
            return null;
        }

        /// <inheritDoc />
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int depth = Scopes.Count;
            foreach (Dictionary<string, IDeclarationNode> scope in Scopes)
            {
                foreach (KeyValuePair<string, IDeclarationNode> entry in scope)
                {
                    sb.AppendLine($"{depth}: \"{entry.Key}\" = {entry.Value}");
                }
                sb.AppendLine();
                depth -= 1;
            }
            return sb.ToString();
        }
    }
}
