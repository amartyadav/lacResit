using Compiler.Nodes;
using Compiler.Tokenization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Compiler.IO
{
    /// <summary>
    /// Printer for abstract syntax trees
    /// </summary>
    public static class TreePrinter
    {
        /// <summary>
        /// Converts the subtree belonging to a node to a string
        /// </summary>
        /// <param name="lastChild">Whether or not this node is the last child at each parent level of the tree</param>
        /// <param name="node">The node to print</param>
        /// <param name="children">The children of the node to print in the desired order</param>
        /// <returns>A string representation of the subtree rooted at the given node</returns>
        private static string NodeToString(ImmutableList<bool> lastChild, IAbstractSyntaxTreeNode node, params IAbstractSyntaxTreeNode[] children)
        {
            return GeneralNodeToString(lastChild, node, null, children);
        }

        /// <summary>
        /// Converts the subtree belonging to a node to a string
        /// </summary>
        /// <param name="lastChild">Whether or not this node is the last child at each parent level of the tree</param>
        /// <param name="node">The node to print</param>
        /// <param name="token">The token associated with the node</param>
        /// <returns>A string representation of the subtree rooted at the given node</returns>
        private static string TerminalNodeToString(ImmutableList<bool> lastChild, IAbstractSyntaxTreeNode node, Token token)
        {
            return GeneralNodeToString(lastChild, node, token, null);
        }

        /// <summary>
        /// Converts the subtree belonging to a node to a string
        /// </summary>
        /// <param name="lastChild">Whether or not this node is the last child at each parent level of the tree</param>
        /// <param name="node">The node to print</param>
        /// <param name="token">The token associated with the node</param>
        /// <param name="children">The children of the node to print in the desired order</param>
        /// <returns>A string representation of the subtree rooted at the given node</returns>
        private static string GeneralNodeToString(ImmutableList<bool> lastChild, IAbstractSyntaxTreeNode node, Token token, IAbstractSyntaxTreeNode[] children)
        {
            if (node == null)
                return DrawIndent(lastChild) + "NULL" + System.Environment.NewLine;
            else
            {
                return DrawIndent(lastChild) +
                    GetNodeName(node) +
                    (token == null ? "" : $" \"{token.Spelling}\"") +
                    GetPropertyString(node) +
                    System.Environment.NewLine +
                    (children == null ? "" : ChildrenToString(lastChild, children));
            }
        }

        /// <summary>
        /// Gets the name of a node
        /// </summary>
        /// <param name="node">The node to get the name of</param>
        /// <returns>A name based on the node's type</returns>
        private static string GetNodeName(IAbstractSyntaxTreeNode node)
        {
            string name = node.GetType().Name;
            return name.EndsWith("Node") ? name.Substring(0, name.Length - 4) : name;
        }

        /// <summary>
        /// Converts the children of a node into a string representation
        /// </summary>
        /// <param name="lastChild">Whether or not the node is the last child at each parent level of the tree</param>
        /// <param name="children">The children of the node</param>
        /// <returns>A string representation of the children of a node</returns>
        private static string ChildrenToString(ImmutableList<bool> lastChild, IAbstractSyntaxTreeNode[] children)
        {
            StringBuilder sb = new StringBuilder();
            int numChildren = children.Length;
            for (int i = 0; i < numChildren; i++)
                sb.Append(ToString(lastChild.Add(i == numChildren - 1), children[i]));
            return sb.ToString();
        }

        /// <summary>
        /// Draws the tree-based indentation for a node
        /// </summary>
        /// <param name="lastChild">Whether or not this node is the last child at each parent level of the tree</param>
        /// <returns>A string to use as the indent before the node</returns>
        private static string DrawIndent(ImmutableList<bool> lastChild)
        {
            StringBuilder sb = new StringBuilder();
            int numLevels = lastChild.Count;
            if (numLevels > 0)
            {
                for (int i = 0; i < numLevels - 1; i++)
                    if (lastChild[i])
                        sb.Append("   ");
                    else
                        sb.Append("│  ");
                if (lastChild[numLevels - 1])
                    sb.Append("└──");
                else
                    sb.Append("├──");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets a node's properties as a string
        /// </summary>
        /// <param name="node">The node to get the string for</param>
        /// <returns>A string containing type and declaration properties of the node, as appropriate</returns>
        private static string GetPropertyString(IAbstractSyntaxTreeNode node)
        {
            if (node is ITypedNode typedNode)
                if (node is IDeclaredNode declaredNode)
                    return " [" + GetDeclarationString(declaredNode) + ", " + GetTypeString(typedNode) + "]";
                else
                    return " [" + GetTypeString(typedNode) + "]";
            else if (node is IDeclaredNode declaredNode)
                return " [" + GetDeclarationString(declaredNode) + "]";
            else
                return "";
        }

        /// <summary>
        /// Gets a node's declaration as a string
        /// </summary>
        /// <param name="declaredNode">The node to get the declaration of</param>
        /// <returns>A string describing the declaration of the node</returns>
        private static string GetDeclarationString(IDeclaredNode declaredNode)
        {
            return $"Declaration: {(declaredNode.Declaration is null ? "not set" : declaredNode.Declaration.ToString())}";
        }

        /// <summary>
        /// Gets a node's type as a string
        /// </summary>
        /// <param name="typedNode">The node to get the type of</param>
        /// <returns>A string describing the type of the node</returns>
        private static string GetTypeString(ITypedNode typedNode)
        {
            return $"Type: {(typedNode.Type is null ? "not set" : typedNode.Type.ToString())}";
        }

        /// <summary>
        /// Gets a node and its subtree as a string
        /// </summary>
        /// <param name="node">The node to get the string for</param>
        /// <returns>A string representation of the node and its descendents</returns>
        public static string ToString(IAbstractSyntaxTreeNode node)
        {
            return ToString(ImmutableList<bool>.Empty, node);
        }

        /// <summary>
        /// Converts the subtree belonging to a node to a string
        /// </summary>
        /// <param name="lastChild">Whether or not this node is the last child at each parent level of the tree</param>
        /// <param name="node">The node to print</param>
        /// <returns>A string representation of the subtree rooted at the given node</returns>
        private static string ToString(ImmutableList<bool> lastChild, IAbstractSyntaxTreeNode node)
        {
            switch (node)
            {
                // Program
                case ProgramNode programNode:
                    return NodeToString(lastChild, programNode, programNode.Command);

                // Commands
                case AssignCommandNode assignCommand:
                    return NodeToString(lastChild, assignCommand, assignCommand.Identifier, assignCommand.Expression);

                case CallCommandNode callCommand:
                    return NodeToString(lastChild, callCommand, callCommand.Identifier, callCommand.Parameter);

                case IfCommandNode ifCommand:
                    return NodeToString(lastChild, ifCommand, ifCommand.Expression, ifCommand.ThenCommand, ifCommand.ElseCommand);

                case LetCommandNode letCommand:
                    return NodeToString(lastChild, letCommand, letCommand.Declaration, letCommand.Command);

                case SequentialCommandNode sequentialCommand:
                    return NodeToString(lastChild, sequentialCommand, sequentialCommand.Commands.ToArray());

                case BlankCommandNode blankCommand:
                    return NodeToString(lastChild, blankCommand);

                case WhileCommandNode whileCommand:
                    return NodeToString(lastChild, whileCommand, whileCommand.Expression, whileCommand.Command);

                // Declarations
                case ConstDeclarationNode constDeclaration:
                    return NodeToString(lastChild, constDeclaration, constDeclaration.Identifier, constDeclaration.Expression);

                case SequentialDeclarationNode sequentialDeclaration:
                    return NodeToString(lastChild, sequentialDeclaration, sequentialDeclaration.Declarations.ToArray());

                case VarDeclarationNode varDeclaration:
                    return NodeToString(lastChild, varDeclaration, varDeclaration.TypeDenoter, varDeclaration.Identifier);

                // Expressions
                case BinaryExpressionNode binaryExpression:
                    return NodeToString(lastChild, binaryExpression, binaryExpression.LeftExpression, binaryExpression.Op, binaryExpression.RightExpression);

                case CharacterExpressionNode characterExpression:
                    return NodeToString(lastChild, characterExpression, characterExpression.CharLit);

                case IdExpressionNode idExpression:
                    return NodeToString(lastChild, idExpression, idExpression.Identifier);

                case IntegerExpressionNode integerExpression:
                    return NodeToString(lastChild, integerExpression, integerExpression.IntLit);

                case UnaryExpressionNode unaryExpression:
                    return NodeToString(lastChild, unaryExpression, unaryExpression.Op, unaryExpression.Expression);

                // Parameters
                case BlankParameterNode blankParameter:
                    return NodeToString(lastChild, blankParameter);

                case ExpressionParameterNode expressionParameter:
                    return NodeToString(lastChild, expressionParameter, expressionParameter.Expression);

                case VarParameterNode varParameter:
                    return NodeToString(lastChild, varParameter, varParameter.Identifier);

                // Types
                case TypeDenoterNode typeDenoter:
                    return NodeToString(lastChild, typeDenoter, typeDenoter.Identifier);

                // Terminals
                case CharacterLiteralNode characterLiteral:
                    return TerminalNodeToString(lastChild, characterLiteral, characterLiteral.CharacterLiteralToken);

                case IdentifierNode identifier:
                    return TerminalNodeToString(lastChild, identifier, identifier.IdentifierToken);

                case IntegerLiteralNode integerLiteral:
                    return TerminalNodeToString(lastChild, integerLiteral, integerLiteral.IntegerLiteralToken);

                case OperatorNode operation:
                    return TerminalNodeToString(lastChild, operation, operation.OperatorToken);

                // Error
                case ErrorNode errorNode:
                    return NodeToString(lastChild, errorNode);

                // Anything without a case written for it ends up here
                default:
                    if (node == null)
                    {
                        // Null - not a lot we can do
                        Debugger.Write("Tried to print a null tree node");
                        return NodeToString(lastChild, node);
                    }
                    else
                    {
                        // Use reflection to try to draw node
                        Debugger.Write($"Tried to print an unknown tree node type {node.GetType().Name}");
                        IAbstractSyntaxTreeNode[] children = GetChildNodes(node).Concat(GetCollectionOfChildNodes(node)).ToArray();
                        Token token = GetToken(node);
                        return GeneralNodeToString(lastChild, node, token, children.Length == 0 ? null : children);
                    }
            }
        }

        /// <summary>
        /// Gets the children of a node
        /// </summary>
        /// <param name="node">The node to get children for</param>
        /// <returns>The children of the node</returns>
        private static List<IAbstractSyntaxTreeNode> GetChildNodes(IAbstractSyntaxTreeNode node)
        {
            return node.GetType().GetProperties()
                       .Where(property => property.PropertyType
                                                  .GetInterfaces()
                                                  .Contains(typeof(IAbstractSyntaxTreeNode)))
                       .Select(property => (IAbstractSyntaxTreeNode)property.GetValue(node))
                       .ToList();
        }

        /// <summary>
        /// Gets children of a node which are in a collection
        /// </summary>
        /// <param name="node">The node to get children for</param>
        /// <returns>The children of the node which are in an immutable array</returns>
        private static List<IAbstractSyntaxTreeNode> GetCollectionOfChildNodes(IAbstractSyntaxTreeNode node)
        {
            // Find if there is a property of type ImmutableArray<X>, where X is a kind of node
            IEnumerable childCollection = node.GetType()
                                              .GetProperties()
                                              .Where(property => property.PropertyType.IsGenericType)
                                              .Where(property => property.PropertyType.GetGenericTypeDefinition() == typeof(ImmutableArray<>))
                                              .Where(property => property.PropertyType
                                                                         .GetGenericArguments()
                                                                         .SingleOrDefault()
                                                                         .GetInterfaces()
                                                                         .Contains(typeof(IAbstractSyntaxTreeNode)))
                                              .Select(property => property.GetValue(node))
                                              .FirstOrDefault()
                                              as IEnumerable;

            List<IAbstractSyntaxTreeNode> children = new List<IAbstractSyntaxTreeNode>();
            if (childCollection != null)
                foreach (object child in childCollection)
                    children.Add((IAbstractSyntaxTreeNode)child);
            return children;
        }

        /// <summary>
        /// Gets the token associated with a node
        /// </summary>
        /// <param name="node">The node to get the token for</param>
        /// <returns>The token associated with this node</returns>
        private static Token GetToken(IAbstractSyntaxTreeNode node)
        {
            return node.GetType().GetProperties()
                       .Where(property => property.PropertyType.GetType() == typeof(Token))
                       .Select(property => (Token)property.GetValue(node))
                       .FirstOrDefault();
        }
    }
}
