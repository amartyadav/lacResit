using Compiler.CodeGeneration;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node that can be placed in the syntax tree when a parsing error has occurred 
    /// </summary>
    /// <remarks>
    /// Note that all interfaces are implemented in order to allow this class to be used as
    /// widely as possible, e.g. it can appear where we expect to see a command or where we
    /// expect to find an expression.
    /// 
    /// All the properties below have to be implemented to satisfy the interfaces but
    /// shouldn't ever actually be used.
    /// </remarks>
    public class ErrorNode : IAbstractSyntaxTreeNode, ICommandNode, IDeclarationNode, IDeclaredNode, IEntityDeclarationNode, IExpressionNode, IParameterNode, ITypeDeclarationNode, ITypedNode, IVariableDeclarationNode
    {
        /// <summary>
        /// The declaration of the node
        /// </summary>
        public IDeclarationNode Declaration
        {
            get { return null; }
            set { throw new System.NotImplementedException("Can't set the declaration of an error node"); }
        }

        /// <summary>
        /// The entity type of the node
        /// </summary>
        public SimpleTypeDeclarationNode EntityType
        {
            get { return null; }
        }

        /// <summary>
        /// The type of the node
        /// </summary>
        public SimpleTypeDeclarationNode Type
        {
            get { return null; }
            set { throw new System.NotImplementedException("Can't set the type of an error node"); }
        }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// The location of the entity in the generated code
        /// </summary>
        public IRuntimeEntity RuntimeEntity
        {
            get { return null; }
            set { throw new System.NotImplementedException("Can't set the runtime entity of an error node"); }
        }

        /// <inheritDoc />
        public override string ToString()
        {
            return "ERROR";
        }

        /// <summary>
        /// Creates a new error node
        /// </summary>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public ErrorNode(Position position)
        {
            Position = position;
        }
    }
}