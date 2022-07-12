namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a unary expression
    /// </summary>
    public class UnaryExpressionNode : IExpressionNode
    {
        /// <summary>
        /// The operation
        /// </summary>
        public OperatorNode Op { get; }

        /// <summary>
        /// The expression the operation is applied to
        /// </summary>
        public IExpressionNode Expression { get; }

        /// <summary>
        /// The type of the node
        /// </summary>
        public SimpleTypeDeclarationNode Type { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Op.Position; } }

        /// <summary>
        /// Creates a new unary expression node
        /// </summary>
        /// <param name="op">The operation</param>
        /// <param name="expression">The expression the operation is applied to</param>
        public UnaryExpressionNode(OperatorNode op, IExpressionNode expression)
        {
            Op = op;
            Expression = expression;
        }
    }
}