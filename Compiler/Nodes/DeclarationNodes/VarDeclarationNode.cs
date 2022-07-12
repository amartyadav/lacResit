using Compiler.CodeGeneration;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a variable declaration
    /// </summary>
    public class VarDeclarationNode : IVariableDeclarationNode
    {
        /// <summary>
        /// The name of the variable
        /// </summary>
        public IdentifierNode Identifier { get; }

        /// <summary>
        /// The name of the type of the variable
        /// </summary>
        public TypeDenoterNode TypeDenoter { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }
        
        /// <summary>
        /// The entity type of the variable
        /// </summary>
        public SimpleTypeDeclarationNode EntityType { get { return TypeDenoter.Type; } }

        /// <summary>
        /// The location of the entity in the generated code
        /// </summary>
        public IRuntimeEntity RuntimeEntity { get; set; }

        /// <summary>
        /// Creates a new variable declaration node
        /// </summary>
        /// <param name="identifier">The name of the variable</param>
        /// <param name="typeDenoter">The name of the type of the variable</param>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public VarDeclarationNode(IdentifierNode identifier, TypeDenoterNode typeDenoter, Position position)
        {
            Identifier = identifier;
            TypeDenoter = typeDenoter;
            Position = position;
        }

        /// <inheritDoc />
        public override string ToString()
        {
            if (EntityType == null)
                return "User-defined variable (Unknown Type)";
            else
                return $"User-defined variable ({EntityType.Name})";
        }
    }
}