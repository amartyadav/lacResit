using Compiler.IO;
using Compiler.Nodes;
using Compiler.Tokenization;
using System.Collections.Generic;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace Compiler.SemanticAnalysis
{
    /// <summary>
    /// Matches identifiers to their declaration
    /// </summary>
    public class DeclarationIdentifier
    {
        /// <summary>
        /// The error reporter
        /// </summary>
        public ErrorReporter Reporter { get; }

        /// <summary>
        /// The symbol table
        /// </summary>
        public SymbolTable SymbolTable { get; }

        /// <summary>
        /// Creates a new declaration identifier
        /// </summary>
        /// <param name="reporter">The error reporter</param>
        public DeclarationIdentifier(ErrorReporter reporter)
        {
            Reporter = reporter;
            SymbolTable = new SymbolTable();
        }

        /// <summary>
        /// Carries out identification of symbols on a program
        /// </summary>
        /// <param name="program">The program to carry out identification for</param>
        public void PerformIdentification(ProgramNode program)
        {
            foreach (KeyValuePair<string, IDeclarationNode> item in StandardEnvironment.GetItems())
                SymbolTable.Enter(item.Key, item.Value);
            PerformIdentificationOnProgram(program);
        }

        /// <summary>
        /// Carries out identification on a node
        /// </summary>
        /// <param name="node">The node to perform identification on</param>
        private void PerformIdentification(IAbstractSyntaxTreeNode node)
        {
            if (node is null)
                // Shouldn't have null nodes - there is a problem with your parsing
                Debugger.Write("Tried to perform identification on a null tree node");
            else if (node is ErrorNode)
                // Shouldn't have error nodes - there is a problem with your parsing
                Debugger.Write("Tried to perform identification on an error tree node");
            else
            {
                string functionName = "PerformIdentificationOn" + node.GetType().Name.Remove(node.GetType().Name.Length - 4);
                MethodInfo function = this.GetType().GetMethod(functionName, NonPublic | Public | Instance | Static);
                if (function == null)
                    // There is not a correctly named function below
                    Debugger.Write($"Couldn't find the function {functionName} when doing identification");
                else
                    function.Invoke(this, new[] { node });
            }
        }



        /// <summary>
        /// Carries out identification on a program node
        /// </summary>
        /// <param name="programNode">The node to perform identification on</param>
        private void PerformIdentificationOnProgram(ProgramNode programNode)
        {
            PerformIdentification(programNode.Command);
        }



        /// <summary>
        /// Carries out identification on an assign command node
        /// </summary>
        /// <param name="assignCommand">The node to perform identification on</param>
        private void PerformIdentificationOnAssignCommand(AssignCommandNode assignCommand)
        {
            PerformIdentification(assignCommand.Identifier);
            PerformIdentification(assignCommand.Expression);
        }

        /// <summary>
        /// Carries out identification on a blank command node
        /// </summary>
        /// <param name="blankCommand">The node to perform identification on</param>
        private void PerformIdentificationOnBlankCommand(BlankCommandNode blankCommand)
        {
        }

        /// <summary>
        /// Carries out identification on a call command node
        /// </summary>
        /// <param name="callCommand">The node to perform identification on</param>
        private void PerformIdentificationOnCallCommand(CallCommandNode callCommand)
        {
            PerformIdentification(callCommand.Identifier);
            PerformIdentification(callCommand.Parameter);
        }

        /// <summary>
        /// Carries out identification on an if command node
        /// </summary>
        /// <param name="ifCommand">The node to perform identification on</param>
        private void PerformIdentificationOnIfCommand(IfCommandNode ifCommand)
        {
            PerformIdentification(ifCommand.Expression);
            PerformIdentification(ifCommand.ThenCommand);
            PerformIdentification(ifCommand.ElseCommand);
        }

        /// <summary>
        /// Carries out identification on a let command node
        /// </summary>
        /// <param name="letCommand">The node to perform identification on</param>
        private void PerformIdentificationOnLetCommand(LetCommandNode letCommand)
        {
            SymbolTable.OpenScope();
            PerformIdentification(letCommand.Declaration);
            PerformIdentification(letCommand.Command);
            SymbolTable.CloseScope();
        }

        /// <summary>
        /// Carries out identification on a sequential command node
        /// </summary>
        /// <param name="sequentialCommand">The node to perform identification on</param>
        private void PerformIdentificationOnSequentialCommand(SequentialCommandNode sequentialCommand)
        {
            foreach (ICommandNode command in sequentialCommand.Commands)
                PerformIdentification(command);
        }

        /// <summary>
        /// Carries out identification on a while command node
        /// </summary>
        /// <param name="whileCommand">The node to perform identification on</param>
        private void PerformIdentificationOnWhileCommand(WhileCommandNode whileCommand)
        {
            PerformIdentification(whileCommand.Expression);
            PerformIdentification(whileCommand.Command);
        }



        /// <summary>
        /// Carries out identification on a const declaration node
        /// </summary>
        /// <param name="constDeclaration">The node to perform identification on</param>
        private void PerformIdentificationOnConstDeclaration(ConstDeclarationNode constDeclaration)
        {
            Token token = constDeclaration.Identifier.IdentifierToken;
            bool success = SymbolTable.Enter(token.Spelling, constDeclaration);
            PerformIdentification(constDeclaration.Expression);
        }

        /// <summary>
        /// Carries out identification on a sequential declaration node
        /// </summary>
        /// <param name="sequentialDeclaration">The node to perform identification on</param>
        private void PerformIdentificationOnSequentialDeclaration(SequentialDeclarationNode sequentialDeclaration)
        {
            foreach (IDeclarationNode declaration in sequentialDeclaration.Declarations)
                PerformIdentification(declaration);
        }

        /// <summary>
        /// Carries out identification on a var declaration node
        /// </summary>
        /// <param name="varDeclaration">The node to perform identification on</param>
        private void PerformIdentificationOnVarDeclaration(VarDeclarationNode varDeclaration)
        {
            PerformIdentification(varDeclaration.TypeDenoter);
            Token token = varDeclaration.Identifier.IdentifierToken;
            bool success = SymbolTable.Enter(token.Spelling, varDeclaration);
        }



        /// <summary>
        /// Carries out identification on a binary expression node
        /// </summary>
        /// <param name="binaryExpression">The node to perform identification on</param>
        private void PerformIdentificationOnBinaryExpression(BinaryExpressionNode binaryExpression)
        {
            PerformIdentification(binaryExpression.LeftExpression);
            PerformIdentification(binaryExpression.Op);
            PerformIdentification(binaryExpression.RightExpression);
        }

        /// <summary>
        /// Carries out identification on a character expression node
        /// </summary>
        /// <param name="characterExpression">The node to perform identification on</param>
        private void PerformIdentificationOnCharacterExpression(CharacterExpressionNode characterExpression)
        {
            PerformIdentification(characterExpression.CharLit);
        }

        /// <summary>
        /// Carries out identification on an ID expression node
        /// </summary>
        /// <param name="idExpression">The node to perform identification on</param>
        private void PerformIdentificationOnIdExpression(IdExpressionNode idExpression)
        {
            PerformIdentification(idExpression.Identifier);
        }

        /// <summary>
        /// Carries out identification on an integer expression node
        /// </summary>
        /// <param name="integerExpression">The node to perform identification on</param>
        private void PerformIdentificationOnIntegerExpression(IntegerExpressionNode integerExpression)
        {
            PerformIdentification(integerExpression.IntLit);
        }

        /// <summary>
        /// Carries out identification on a unary expression node
        /// </summary>
        /// <param name="unaryExpression">The node to perform identification on</param>
        private void PerformIdentificationOnUnaryExpression(UnaryExpressionNode unaryExpression)
        {
            PerformIdentification(unaryExpression.Op);
            PerformIdentification(unaryExpression.Expression);
        }



        /// <summary>
        /// Carries out identification on a blank parameter node
        /// </summary>
        /// <param name="blankParameter">The node to perform identification on</param>
        private void PerformIdentificationOnBlankParameter(BlankParameterNode blankParameter)
        {
        }

        /// <summary>
        /// Carries out identification on an expression parameter node
        /// </summary>
        /// <param name="expressionParameter">The node to perform identification on</param>
        private void PerformIdentificationOnExpressionParameter(ExpressionParameterNode expressionParameter)
        {
            PerformIdentification(expressionParameter.Expression);
        }

        /// <summary>
        /// Carries out identification on a var parameter node
        /// </summary>
        /// <param name="varParameter">The node to perform identification on</param>
        private void PerformIdentificationOnVarParameter(VarParameterNode varParameter)
        {
            PerformIdentification(varParameter.Identifier);
        }



        /// <summary>
        /// Carries out identification on a type denoter node
        /// </summary>
        /// <param name="typeDenoter">The node to perform identification on</param>
        private void PerformIdentificationOnTypeDenoter(TypeDenoterNode typeDenoter)
        {
            PerformIdentification(typeDenoter.Identifier);
        }



        /// <summary>
        /// Carries out identification on a character literal node
        /// </summary>
        /// <param name="characterLiteral">The node to perform identification on</param>
        private void PerformIdentificationOnCharacterLiteral(CharacterLiteralNode characterLiteral)
        {
        }

        /// <summary>
        /// Carries out identification on an identifier node
        /// </summary>
        /// <param name="identifier">The node to perform identification on</param>
        private void PerformIdentificationOnIdentifier(IdentifierNode identifier)
        {
            IDeclarationNode declaration = SymbolTable.Retrieve(identifier.IdentifierToken.Spelling);
            identifier.Declaration = declaration;
        }

        /// <summary>
        /// Carries out identification on an integer literal node
        /// </summary>
        /// <param name="integerLiteral">The node to perform identification on</param>
        private void PerformIdentificationOnIntegerLiteral(IntegerLiteralNode integerLiteral)
        {
        }

        /// <summary>
        /// Carries out identification on an operation node
        /// </summary>
        /// <param name="operation">The node to perform identification on</param>
        private void PerformIdentificationOnOperator(OperatorNode operation)
        {
            IDeclarationNode declaration = SymbolTable.Retrieve(operation.OperatorToken.Spelling);
            operation.Declaration = declaration;
        }
    }
}
