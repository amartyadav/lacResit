namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a let command
    /// </summary>
    public class LetCommandNode : ICommandNode
    {
        /// <summary>
        /// The declarations for the let block
        /// </summary>
        public IDeclarationNode Declaration { get; }

        /// <summary>
        /// The command inside the let block
        /// </summary>
        public ICommandNode Command { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates a new let command node
        /// </summary>
        /// <param name="declaration">The declarations for the let block</param>
        /// <param name="command">The command inside the let block</param>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public LetCommandNode(IDeclarationNode declaration, ICommandNode command, Position position)
        {
            Declaration = declaration;
            Command = command;
            Position = position;
        }
    }
}