using static Compiler.CodeGeneration.TriangleAbstractMachine;

namespace Compiler.Nodes
{
    /// <summary>
    /// Implemented by all nodes which are a declaration of a TAM primative function
    /// </summary>
    public interface IPrimitiveDeclarationNode : IDeclarationNode
    {
        /// <summary>
        /// The built-in function that the declaration refers to
        /// </summary>
        Primitive Primitive { get; }
    }
}
