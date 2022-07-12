namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an integer expression
    /// </summary>
    public class IntegerExpressionNode : IExpressionNode
    {
        /// <summary>
        /// The integer literal
        /// </summary>
        public IntegerLiteralNode IntLit { get; }

        /// <summary>
        /// The type of the node
        /// </summary>
        public SimpleTypeDeclarationNode Type { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return IntLit.Position; } }

        /// <summary>
        /// Creates a new integer literal expression node
        /// </summary>
        /// <param name="intLit">The integer literal</param>
        public IntegerExpressionNode(IntegerLiteralNode intLit)
        {
            IntLit = intLit;
        }
    }
}