namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a call command
    /// </summary>
    public class CallCommandNode : ICommandNode
    {
        /// <summary>
        /// The function name
        /// </summary>
        public IdentifierNode Identifier { get; }

        /// <summary>
        /// The function call parameter
        /// </summary>
        public IParameterNode Parameter { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Identifier.Position; } }

        /// <summary>
        /// Creates a new call command node
        /// </summary>
        /// <param name="identifier">The function name</param>
        /// <param name="parameter">The function call parameter</param>
        public CallCommandNode(IdentifierNode identifier, IParameterNode parameter)
        {
            Identifier = identifier;
            Parameter = parameter;
        }
    }
}