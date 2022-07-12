using Compiler.Tokenization;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an operator
    /// </summary>
    public class OperatorNode : IDeclaredNode
    {
        /// <summary>
        /// The operator token
        /// </summary>
        public Token OperatorToken { get; }

        /// <summary>
        /// The declaration of the operator
        /// </summary>
        public IDeclarationNode Declaration { get; set; }
        
        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return OperatorToken.Position; } }

        /// <summary>
        /// Creates a new operator node
        /// </summary>
        /// <param name="operatorToken">The operator token</param>
        public OperatorNode(Token operatorToken)
        {
            OperatorToken = operatorToken;
        }
    }
}