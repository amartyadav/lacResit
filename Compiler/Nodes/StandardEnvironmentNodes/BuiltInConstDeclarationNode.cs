using Compiler.CodeGeneration;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a built in constant declaration
    /// </summary>
    public class BuiltInConstDeclarationNode : IEntityDeclarationNode
    {
        /// <summary>
        /// The name of the conatant
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the constant
        /// </summary>
        public SimpleTypeDeclarationNode EntityType { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Position.BuiltIn; } }

        /// <summary>
        /// The location of the entity in the generated code
        /// </summary>
        public IRuntimeEntity RuntimeEntity { get; set; }

        /// <summary>
        /// Creates a new built in constant
        /// </summary>
        /// <param name="name">The name of the constant</param>
        /// <param name="type">The type of the constant</param>
        public BuiltInConstDeclarationNode(string name, SimpleTypeDeclarationNode type)
        {
            Name = name;
            EntityType = type;
        }

        /// <inheritDoc />
        public override string ToString()
        {
            return $"{Name} (Built-in ${EntityType})";
        }
    }
}