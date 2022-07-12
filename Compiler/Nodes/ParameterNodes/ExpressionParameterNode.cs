namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an expression parameter
    /// </summary>
    public class ExpressionParameterNode : IParameterNode
    {
        /// <summary>
        /// The expression associated with the parameter
        /// </summary>
        public IExpressionNode Expression { get; }

        /// <summary>
        /// The type of the parameter
        /// </summary>
        public SimpleTypeDeclarationNode Type { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Expression.Position; } }

        /// <summary>
        /// Creates a new expression parameter node
        /// </summary>
        /// <param name="expression">The expression associated with the parameter</param>
        public ExpressionParameterNode(IExpressionNode expression)
        {
            Expression = expression;
        }
    }
}