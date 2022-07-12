using static Compiler.CodeGeneration.TriangleAbstractMachine;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a unary operation declaration
    /// </summary>
    public class UnaryOperationDeclarationNode : IPrimitiveDeclarationNode
    {
        /// <summary>
        /// The symbol for the operation
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type signature of the operation
        /// </summary>
        public FunctionTypeDeclarationNode Type { get; }

        /// <summary>
        /// The built-in function that the declaration refers to
        /// </summary>
        public Primitive Primitive { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Position.BuiltIn; } }

        /// <summary>
        /// Creates a new unary operation node
        /// </summary>
        /// <param name="name">The symbol for the operation</param>
        /// <param name="primitive">The built-in function that the declaration refers to</param>
        /// <param name="argumentType">The type of the operation argument</param>
        /// <param name="returnType">The return type of the operation</param>
        public UnaryOperationDeclarationNode(string name, Primitive primitive, SimpleTypeDeclarationNode argumentType, SimpleTypeDeclarationNode returnType)
        {
            Name = name;
            Primitive = primitive;
            Type = new FunctionTypeDeclarationNode(returnType, (argumentType, false));
        }

        /// <inheritDoc />
        public override string ToString()
        {
            return Name + Type;
        }
    }
}