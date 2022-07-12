namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a var parameter
    /// </summary>
    public class VarParameterNode : IParameterNode
    {
        /// <summary>
        /// The identifier associated with the parameter
        /// </summary>
        public IdentifierNode Identifier { get; }

        /// <summary>
        /// The type of the parameter
        /// </summary>
        public SimpleTypeDeclarationNode Type { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates a new var parameter node
        /// </summary>
        /// <param name="identifier">The identifier associated with the parameter</param>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public VarParameterNode(IdentifierNode identifier, Position position)
        {
            Identifier = identifier;
            Position = position;
        }
    }
}