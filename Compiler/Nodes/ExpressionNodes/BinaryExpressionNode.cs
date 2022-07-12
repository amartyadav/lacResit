namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a binary expression
    /// </summary>
    public class BinaryExpressionNode : IExpressionNode
    {
        /// <summary>
        /// The lefthand expression
        /// </summary>
        public IExpressionNode LeftExpression { get; }

        /// <summary>
        /// The operation
        /// </summary>
        public OperatorNode Op { get; }

        /// <summary>
        /// The righthand expression
        /// </summary>
        public IExpressionNode RightExpression { get; }

        /// <summary>
        /// The type of the node
        /// </summary>
        public SimpleTypeDeclarationNode Type { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return LeftExpression.Position; } }

        /// <summary>
        /// Creates a new binary expression node
        /// </summary>
        /// <param name="leftExpression">The lefthand expression</param>
        /// <param name="op">The operation</param>
        /// <param name="rightExpression">The righthand expression</param>
        public BinaryExpressionNode(IExpressionNode leftExpression, OperatorNode op, IExpressionNode rightExpression)
        {
            LeftExpression = leftExpression;
            Op = op;
            RightExpression = rightExpression;
        }
    }
}