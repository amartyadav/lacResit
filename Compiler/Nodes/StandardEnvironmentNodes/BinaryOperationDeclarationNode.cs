using static Compiler.CodeGeneration.TriangleAbstractMachine;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a binary operation declaration
    /// </summary>
    public class BinaryOperationDeclarationNode : IPrimitiveDeclarationNode
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
        /// Creates a new binary operation node
        /// </summary>
        /// <param name="name">The symbol for the operation</param>
        /// <param name="primitive">The built-in function that the declaration refers to</param>
        /// <param name="leftType">The type of the lefthand operand</param>
        /// <param name="rightType">The type of the righthand operand</param>
        /// <param name="returnType">The return type of the operation</param>
        public BinaryOperationDeclarationNode(string name, Primitive primitive, SimpleTypeDeclarationNode leftType, SimpleTypeDeclarationNode rightType, SimpleTypeDeclarationNode returnType)
        {
            Name = name;
            Primitive = primitive;
            Type = new FunctionTypeDeclarationNode(returnType, (leftType, false), (rightType, false));
        }

        /// <inheritDoc />
        public override string ToString()
        {
            return Name + Type;
        }
    }
}