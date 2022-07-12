namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a blank command
    /// </summary>
    public class BlankCommandNode : ICommandNode
    {
        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates a new blank command node
        /// </summary>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public BlankCommandNode(Position position)
        {
            Position = position;
        }
    }
}