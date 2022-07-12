using Compiler.Tokenization;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an integer literal
    /// </summary>
    public class IntegerLiteralNode : IAbstractSyntaxTreeNode
    {
        /// <summary>
        /// The integer literal token
        /// </summary>
        public Token IntegerLiteralToken { get; }

        /// <summary>
        /// The value of the integer literal
        /// </summary>
        public int Value { get { return int.Parse(IntegerLiteralToken.Spelling); } }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return IntegerLiteralToken.Position; } }

        /// <summary>
        /// Creates an integer literal node
        /// </summary>
        /// <param name="integerLiteralToken">The integer literal token</param>
        public IntegerLiteralNode(Token integerLiteralToken)
        {
            IntegerLiteralToken = integerLiteralToken;
        }
    }
}