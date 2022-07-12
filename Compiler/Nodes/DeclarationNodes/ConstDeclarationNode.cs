using Compiler.CodeGeneration;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a constant declaration
    /// </summary>
    public class ConstDeclarationNode : IEntityDeclarationNode
    {
        /// <summary>
        /// The name of the constant
        /// </summary>
        public IdentifierNode Identifier { get; }

        /// <summary>
        /// The expression to initialise the constant with
        /// </summary>
        public IExpressionNode Expression { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// The entity type of the constant
        /// </summary>
        public SimpleTypeDeclarationNode EntityType { get { return Expression.Type; } }

        /// <summary>
        /// The location of the entity in the generated code
        /// </summary>
        public IRuntimeEntity RuntimeEntity { get; set; }

        /// <summary>
        /// Creates a new constant declaration node
        /// </summary>
        /// <param name="identifier">The name of the constant</param>
        /// <param name="expression">The expression to initialise the constant with</param>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public ConstDeclarationNode(IdentifierNode identifier, IExpressionNode expression, Position position)
        {
            Identifier = identifier;
            Expression = expression;
            Position = position;
        }

        /// <inheritDoc />
        public override string ToString()
        {
            if (EntityType == null)
                return "User-defined constant (Unknown Type)";
            else
                return $"User-defined constant ({EntityType.Name})";
        }
    }
}