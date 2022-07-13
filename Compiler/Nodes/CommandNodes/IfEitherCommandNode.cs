namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an ifeither command
    /// </summary>P
    public class IfEitherCommandNode : ICommandNode // adding this file to support ParseIfEitherCommand() in Parser.cs
    {
        /// <summary>
        /// The IfEither condition expression
        /// </summary>
        public IExpressionNode IfEitherExpression { get; }
        
        /// <summary>
        /// The Or condition expression
        /// </summary>
        public IExpressionNode OrExpression { get; }
        
        /// <summary>
        /// The then branch command
        /// </summary>
        public ICommandNode ThenCommand { get; }

        /// <summary>
        /// The else branch command
        /// </summary>
        public ICommandNode ElseCommand { get; }
        
        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates a new ifeither command node
        /// </summary>
        /// <param name="ifEitherExpression">The ifeither condition expression</param>
        /// <param name="orExpression">The or condition expression</param>
        /// <param name="thenCommand">The then branch command</param>
        /// <param name="elseCommand">The else branch command</param>
        /// <param name="position">The position in the code where the content associated with the node begins</param>

        public IfEitherCommandNode(IExpressionNode ifEitherExpression, IExpressionNode orExpression,
            ICommandNode thenCommand, ICommandNode elseCommand, Position position)
        {
            IfEitherExpression = ifEitherExpression;
            OrExpression = orExpression;
            ThenCommand = thenCommand;
            ElseCommand = elseCommand;
            Position = position;
        }
    }
}