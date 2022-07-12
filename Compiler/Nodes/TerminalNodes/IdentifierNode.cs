using Compiler.Tokenization;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an identifer
    /// </summary>
    public class IdentifierNode : IDeclaredNode
    {
        /// <summary>
        /// The identifier token
        /// </summary>
        public Token IdentifierToken { get; }

        /// <summary>
        /// The declaration of the identifier
        /// </summary>
        public IDeclarationNode Declaration { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return IdentifierToken.Position; } }

        /// <summary>
        /// Creates an identifier node
        /// </summary>
        /// <param name="identifierToken">The identifier token</param>
        public IdentifierNode(Token identifierToken)
        {
            IdentifierToken = identifierToken;
        }
    }
}