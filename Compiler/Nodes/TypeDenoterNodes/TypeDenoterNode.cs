namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a type denoter
    /// </summary>
    public class TypeDenoterNode : ITypedNode
    {
        /// <summary>
        /// The identifier associated with the node, i.e. the name of the type
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
        /// Creates a new type denoter node
        /// </summary>
        /// <param name="identifier">The identifier associated with the node, i.e. the name of the type</param>
        public TypeDenoterNode(IdentifierNode identifier)
        {
            Identifier = identifier;
        }
    }
}