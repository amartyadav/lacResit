namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an assign command
    /// </summary>
    public class AssignCommandNode : ICommandNode
    {
        /// <summary>
        /// The identifier being assigned to
        /// </summary>
        public IdentifierNode Identifier { get; }

        /// <summary>
        /// The expression being assigned
        /// </summary>
        public IExpressionNode Expression { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Identifier.Position; } }

        /// <summary>
        /// Creates a new assign node
        /// </summary>
        /// <param name="identifier">The identifier beign assigned to</param>
        /// <param name="expression">The expression being assigned</param>
        public AssignCommandNode(IdentifierNode identifier, IExpressionNode expression)
        {
            Identifier = identifier;
            Expression = expression;
        }
    }
}