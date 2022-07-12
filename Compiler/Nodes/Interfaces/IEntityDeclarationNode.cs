using Compiler.CodeGeneration;

namespace Compiler.Nodes
{
    /// <summary>
    /// Implemented by all nodes that are declarations of variables or constants
    /// </summary>
    public interface IEntityDeclarationNode : IDeclarationNode
    {
        /// <summary>
        /// The type of the variable or constant
        /// </summary>
        SimpleTypeDeclarationNode EntityType { get; }

        /// <summary>
        /// The location of the entity in the generated code
        /// </summary>
        IRuntimeEntity RuntimeEntity { get; set; }
    }
}
