namespace Compiler.Nodes
{
    /// <summary>
    /// Implemented by all nodes that appear in the abstract syntax tree
    /// </summary>
    public interface IAbstractSyntaxTreeNode
    {
        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        Position Position { get; }
    }
}
