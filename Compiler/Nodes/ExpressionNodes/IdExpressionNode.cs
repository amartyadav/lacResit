namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an ID expression
    /// </summary>
    public class IdExpressionNode : IExpressionNode
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public IdentifierNode Identifier { get; }

        /// <summary>
        /// The type of the node
        /// </summary>
        public SimpleTypeDeclarationNode Type { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Identifier.Position; } }

        /// <summary>
        /// Creates a new ID expression node
        /// </summary>
        /// <param name="identifier">The identifier</param>
        public IdExpressionNode(IdentifierNode identifier)
        {
            Identifier = identifier;
        }
    }
}