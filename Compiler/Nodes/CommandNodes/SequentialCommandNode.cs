using System.Collections.Generic;
using System.Collections.Immutable;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a sequence of commands
    /// </summary>
    public class SequentialCommandNode : ICommandNode
    {
        /// <summary>
        /// The commands in the sequence
        /// </summary>
        public ImmutableArray<ICommandNode> Commands { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates anew sequence of commands
        /// </summary>
        /// <param name="commands">The commands in the sequence</param>
        public SequentialCommandNode(IEnumerable<ICommandNode> commands)
        {
            Commands = commands.ToImmutableArray();
        }
    }
}