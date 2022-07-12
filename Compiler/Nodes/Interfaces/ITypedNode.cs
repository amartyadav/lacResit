namespace Compiler.Nodes
{
    /// <summary>
    /// Implemented by all nodes that have a type
    /// </summary>
    public interface ITypedNode : IAbstractSyntaxTreeNode
    {
        /// <summary>
        /// The type of the node
        /// </summary>
        SimpleTypeDeclarationNode Type { get; set; }
    }
}
