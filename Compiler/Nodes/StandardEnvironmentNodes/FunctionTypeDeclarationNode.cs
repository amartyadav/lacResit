using System.Collections.Immutable;
using System.Text;

namespace Compiler.Nodes
{
    /// <summary>
    /// A node correpsonding to a type declaration for a function type
    /// </summary>
    public class FunctionTypeDeclarationNode : ITypeDeclarationNode
    {
        /// <summary>
        /// The parameter types of the function
        /// </summary>
        public ImmutableArray<(SimpleTypeDeclarationNode type, bool byRef)> Parameters { get; }

        /// <summary>
        /// The return type of the function
        /// </summary>
        public SimpleTypeDeclarationNode ReturnType { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return Position.BuiltIn; } }

        /// <summary>
        /// Creates a new function type node
        /// </summary>
        /// <param name="returnType">The return type of the function</param>
        /// <param name="parameters">The parameter types of the function</param>
        public FunctionTypeDeclarationNode(SimpleTypeDeclarationNode returnType, params (SimpleTypeDeclarationNode type, bool byRef)[] parameters)
        {
            Parameters = parameters.ToImmutableArray();
            ReturnType = returnType;
        }

        /// <inheritDoc />
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            if (Parameters.Length > 0)
            {
                for (int i = 0; i < Parameters.Length - 1; i++)
                {
                    sb.Append(ParameterToString(Parameters[i]));
                    sb.Append(",");
                }
                sb.Append(ParameterToString(Parameters[Parameters.Length - 1]));
            }
            sb.Append(")=>");
            sb.Append(ReturnType);
            return sb.ToString();
        }

        /// <summary>
        /// Converts a single parameter type to a string
        /// </summary>
        /// <param name="parameter">The parameter to convert</param>
        /// <returns>The parameter as a string</returns>
        private static string ParameterToString((SimpleTypeDeclarationNode type, bool byRef) parameter)
        {
            return (parameter.byRef ? "var " : "") + parameter.type;
        }
    }
}
