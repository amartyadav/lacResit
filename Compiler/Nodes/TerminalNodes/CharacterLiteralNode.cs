using Compiler.Tokenization;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a character literal
    /// </summary>
    public class CharacterLiteralNode : IAbstractSyntaxTreeNode
    {
        /// <summary>
        /// The character literal token
        /// </summary>
        public Token CharacterLiteralToken { get; }

        /// <summary>
        /// The value of the character literal
        /// </summary>
        public char Value { get { return CharacterLiteralToken.Spelling[1]; } }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return CharacterLiteralToken.Position; } }

        /// <summary>
        /// Creates a character literal node
        /// </summary>
        /// <param name="CharacterLiteralToken">The Character literal token</param>
        public CharacterLiteralNode(Token characterLiteralToken)
        {
            CharacterLiteralToken = characterLiteralToken;
        }
    }
}