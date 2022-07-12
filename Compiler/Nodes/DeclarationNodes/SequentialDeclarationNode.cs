using System.Collections.Generic;
using System.Collections.Immutable;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a sequence of declarations
    /// </summary>
    public class SequentialDeclarationNode : IDeclarationNode
    {
        /// <summary>
        /// The declarations
        /// </summary>
        public ImmutableArray<IDeclarationNode> Declarations { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Declarations[0].Position; } }

        /// <summary>
        /// Creates a sequential declaration node
        /// </summary>
        /// <param name="declarations">The declarations</param>
        public SequentialDeclarationNode(IEnumerable<IDeclarationNode> declarations)
        {
            Declarations = declarations.ToImmutableArray();
        }
    }
}