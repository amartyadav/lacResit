namespace Compiler.Nodes
{
    /// <summary>
    /// Implemented by all nodes that have a declaration, e.g. variables and functions
    /// </summary>
    public interface IDeclaredNode : IAbstractSyntaxTreeNode
    {
        /// <summary>
        /// The declaration of this node
        /// </summary>
        IDeclarationNode Declaration { get; set; }
    }
}
