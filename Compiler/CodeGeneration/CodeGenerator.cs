using Compiler.IO;
using Compiler.Nodes;
using System.Reflection;
using static Compiler.CodeGeneration.TriangleAbstractMachine;
using static System.Reflection.BindingFlags;

namespace Compiler.CodeGeneration
{
    /// <summary>
    /// Generates code in the target language
    /// </summary>
    public class CodeGenerator
    {
        /// <summary>
        /// The error reporter
        /// </summary>
        public ErrorReporter Reporter { get; }

        /// <summary>
        /// The code generated
        /// </summary>
        private TargetCode code;

        /// <summary>
        /// The sizes of the declaration scopes
        /// </summary>
        private ScopeSizeRecorder scopes;

        /// <summary>
        /// Creates a new code generator
        /// </summary>
        /// <param name="reporter">The error reporter</param>
        public CodeGenerator(ErrorReporter reporter)
        {
            Reporter = reporter;
        }

        /// <summary>
        /// Carries out code generation for a program
        /// </summary>
        /// <param name="program">The program to generate code for</param>
        public TargetCode GenerateCodeFor(ProgramNode program)
        {
            code = new TargetCode(CodeBase, Reporter);
            scopes = new ScopeSizeRecorder(Reporter);
            GenerateCodeForProgram(program);
            return code;
        }

        /// <summary>
        /// Carries out code generation for a node
        /// </summary>
        /// <param name="node">The node to generate code for</param>
        private TargetCode GenerateCodeFor(IAbstractSyntaxTreeNode node)
        {
            if (node is null)
            {
                // Shouldn't have null nodes - there is a problem with your parsing
                Debugger.Write("Tried to generate code for a null tree node");
                return null;
            }
            else if (node is ErrorNode)
            {
                // Shouldn't have error nodes - there is a problem with your parsing
                Debugger.Write("Tried to generate code for an error tree node");
                return null;
            }
            else
            {
                string functionName = "GenerateCodeFor" + node.GetType().Name.Remove(node.GetType().Name.Length - 4);
                MethodInfo function = this.GetType().GetMethod(functionName, NonPublic | Public | Instance | Static);
                if (function == null)
                {
                    // There is not a correctly named function below
                    Debugger.Write($"Couldn't find the function {functionName} when generating code");
                    return null;
                }
                else
                    return (TargetCode)function.Invoke(this, new[] { node });
            }
        }



        /// <summary>
        /// Generates code for a program node
        /// </summary>
        /// <param name="programNode">The node to generate code for</param>
        private void GenerateCodeForProgram(ProgramNode programNode)
        {
            Debugger.Write("Generating code for Program");
            GenerateCodeFor(programNode.Command);
            code.AddInstruction(OpCode.HALT, 0, 0, 0);
        }



        /// <summary>
        /// Generates code for an assign command node
        /// </summary>
        /// <param name="assignCommand">The node to generate code for</param>
        private void GenerateCodeForAssignCommand(AssignCommandNode assignCommand)
        {
            Debugger.Write("Generating code for Assign Command");
            GenerateCodeFor(assignCommand.Expression);
            if (assignCommand.Identifier.Declaration is IVariableDeclarationNode varDeclaration)
                code.AddInstruction(varDeclaration.RuntimeEntity.GenerateInstructionToStore());
            else
                Debugger.Write("The identifier is not a variable and you should have picked this problem up during type checking");
        }

        /// <summary>
        /// Generates code for a blank command node
        /// </summary>
        /// <param name="blankCommand">The node to generate code for</param>
        private void GenerateCodeForBlankCommand(BlankCommandNode blankCommand)
        {
            Debugger.Write("Generating code for Blank Command");
        }

        /// <summary>
        /// Generates code for a call command node
        /// </summary>
        /// <param name="callCommand">The node to generate code for</param>
        private void GenerateCodeForCallCommand(CallCommandNode callCommand)
        {
            Debugger.Write("Generating code for Call Command");
            GenerateCodeFor(callCommand.Parameter);
            GenerateCodeFor(callCommand.Identifier);
        }

        /// <summary>
        /// Generates code for an if command node
        /// </summary>
        /// <param name="ifCommand">The node to generate code for</param>
        private void GenerateCodeForIfCommand(IfCommandNode ifCommand)
        {
            Debugger.Write("Generating code for If Command");
            GenerateCodeFor(ifCommand.Expression);
            Address ifJumpAddress = code.NextAddress;
            code.AddInstruction(OpCode.JUMPIF, Register.CB, FalseValue, 0);
            GenerateCodeFor(ifCommand.ThenCommand);
            Address thenJumpAddress = code.NextAddress;
            code.AddInstruction(OpCode.JUMP, Register.CB, 0, 0);
            code.PatchInstructionToJumpHere(ifJumpAddress);
            GenerateCodeFor(ifCommand.ElseCommand);
            code.PatchInstructionToJumpHere(thenJumpAddress);
        }

        /// <summary>
        /// Generates code for a let command node
        /// </summary>
        /// <param name="letCommand">The node to generate code for</param>
        private void GenerateCodeForLetCommand(LetCommandNode letCommand)
        {
            Debugger.Write("Generating code for Let Command");
            scopes.AddScope();
            GenerateCodeFor(letCommand.Declaration);
            GenerateCodeFor(letCommand.Command);
            code.AddInstruction(OpCode.POP, 0, 0, scopes.GetLocalScopeSize());
            scopes.RemoveScope();
        }

        /// <summary>
        /// Generates code for a sequential command node
        /// </summary>
        /// <param name="sequentialCommand">The node to generate code for</param>
        private void GenerateCodeForSequentialCommand(SequentialCommandNode sequentialCommand)
        {
            Debugger.Write("Generating code for Sequential Command");
            foreach (ICommandNode command in sequentialCommand.Commands)
                GenerateCodeFor(command);
        }

        /// <summary>
        /// Generates code for a while command node
        /// </summary>
        /// <param name="whileCommand">The node to generate code for</param>
        private void GenerateCodeForWhileCommand(WhileCommandNode whileCommand)
        {
            Debugger.Write("Generating code for While Command");
            Address jumpAddress = code.NextAddress;
            code.AddInstruction(OpCode.JUMP, Register.CB, 0, 0);
            Address loopAddress = code.NextAddress;
            GenerateCodeFor(whileCommand.Command);
            code.PatchInstructionToJumpHere(jumpAddress);
            GenerateCodeFor(whileCommand.Expression);
            code.AddInstruction(OpCode.JUMPIF, Register.CB, TrueValue, loopAddress);
            return;
        }



        /// <summary>
        /// Generates code for a const declaration node
        /// </summary>
        /// <param name="constDeclaration">The node to generate code for</param>
        private void GenerateCodeForConstDeclaration(ConstDeclarationNode constDeclaration)
        {
            Debugger.Write("Generating code for Const Declaration");
            if (constDeclaration.Expression is CharacterExpressionNode charLiteral)
                constDeclaration.RuntimeEntity = new RuntimeKnownConstant((short)charLiteral.CharLit.Value);
            else if (constDeclaration.Expression is IntegerExpressionNode intLiteral)
                constDeclaration.RuntimeEntity = new RuntimeKnownConstant((short)intLiteral.IntLit.Value);
            else
            {
                GenerateCodeFor(constDeclaration.Expression);
                byte constantSize = constDeclaration.EntityType.Size;
                short currentScopeSize = scopes.GetLocalScopeSize();
                constDeclaration.RuntimeEntity = new RuntimeUnknownConstant(currentScopeSize, constantSize);
                scopes.AddToLocalScope(constantSize);
            }
        }

        /// <summary>
        /// Generates code for a sequential declaration node
        /// </summary>
        /// <param name="sequentialDeclaration">The node to generate code for</param>
        private void GenerateCodeForSequentialDeclaration(SequentialDeclarationNode sequentialDeclaration)
        {
            Debugger.Write("Generating code for Sequential Declaration");
            foreach (IDeclarationNode declaration in sequentialDeclaration.Declarations)
                GenerateCodeFor(declaration);
        }

        /// <summary>
        /// Generates code for a var declaration node
        /// </summary>
        /// <param name="varDeclaration">The node to generate code for</param>
        private void GenerateCodeForVarDeclaration(VarDeclarationNode varDeclaration)
        {
            Debugger.Write("Generating code for Var Declaration");
            byte variableSize = varDeclaration.EntityType.Size;
            short currentScopeSize = scopes.GetLocalScopeSize();
            code.AddInstruction(OpCode.PUSH, 0, 0, variableSize);
            varDeclaration.RuntimeEntity = new RuntimeVariable(currentScopeSize, variableSize);
            scopes.AddToLocalScope(variableSize);
        }



        /// <summary>
        /// Generates code for a binary expression node
        /// </summary>
        /// <param name="binaryExpression">The node to generate code for</param>
        private void GenerateCodeForBinaryExpression(BinaryExpressionNode binaryExpression)
        {
            Debugger.Write("Generating code for Binary Expression");
            GenerateCodeFor(binaryExpression.LeftExpression);
            GenerateCodeFor(binaryExpression.RightExpression);
            GenerateCodeFor(binaryExpression.Op);
        }

        /// <summary>
        /// Generates code for a character expression node
        /// </summary>
        /// <param name="characterExpression">The node to generate code for</param>
        private void GenerateCodeForCharacterExpression(CharacterExpressionNode characterExpression)
        {
            Debugger.Write("Generating code for Character Expression");
            GenerateCodeFor(characterExpression.CharLit);
        }

        /// <summary>
        /// Generates code for an ID expression node
        /// </summary>
        /// <param name="idExpression">The node to generate code for</param>
        private void GenerateCodeForIdExpression(IdExpressionNode idExpression)
        {
            Debugger.Write("Generating code for ID Expression");
            if (idExpression.Identifier.Declaration is IEntityDeclarationNode entityDeclaration)
                code.AddInstruction(entityDeclaration.RuntimeEntity.GenerateInstructionToLoad());
            else
                Debugger.Write("The identifier is not a constant or variable and you should have picked this problem up during type checking");
        }

        /// <summary>
        /// Generates code for an integer expression node
        /// </summary>
        /// <param name="integerExpression">The node to generate code for</param>
        private void GenerateCodeForIntegerExpression(IntegerExpressionNode integerExpression)
        {
            Debugger.Write("Generating code for Integer Expression");
            GenerateCodeFor(integerExpression.IntLit);
        }

        /// <summary>
        /// Generates code for a unary expression node
        /// </summary>
        /// <param name="unaryExpression">The node to generate code for</param>
        private void GenerateCodeForUnaryExpression(UnaryExpressionNode unaryExpression)
        {
            Debugger.Write("Generating code for Unary Expression");
            GenerateCodeFor(unaryExpression.Expression);
            GenerateCodeFor(unaryExpression.Op);
        }



        /// <summary>
        /// Generates code for a blank parameter node
        /// </summary>
        /// <param name="blankParameter">The node to generate code for</param>
        private void GenerateCodeForBlankParameter(BlankParameterNode blankParameter)
        {
            Debugger.Write("Generating code for Blank Parameter");
        }

        /// <summary>
        /// Generates code for an expression parameter node
        /// </summary>
        /// <param name="expressionParameter">The node to generate code for</param>
        private void GenerateCodeForExpressionParameter(ExpressionParameterNode expressionParameter)
        {
            Debugger.Write("Generating code for Expression Parameter");
            GenerateCodeFor(expressionParameter.Expression);
        }

        /// <summary>
        /// Generates code for a var parameter node
        /// </summary>
        /// <param name="varParameter">The node to generate code for</param>
        private void GenerateCodeForVarParameter(VarParameterNode varParameter)
        {
            Debugger.Write("Generating code for Var Parameter");
            if (varParameter.Identifier.Declaration is IVariableDeclarationNode varDeclaration)
                code.AddInstruction(varDeclaration.RuntimeEntity.GenerateInstructionToLoadAddress());
            else
                Debugger.Write("Error: The identifier is not a variable and you should have picked this problem up during type checking");
        }



        /// <summary>
        /// Generates code for a type denoter node
        /// </summary>
        /// <param name="typeDenoter">The node to generate code for</param>
        private void GenerateCodeForTypeDenoter(TypeDenoterNode typeDenoter)
        {
            Debugger.Write("Generating code for Type Denoter");
        }



        /// <summary>
        /// Generates code for a character literal node
        /// </summary>
        /// <param name="characterLiteral">The node to generate code for</param>
        private void GenerateCodeForCharacterLiteral(CharacterLiteralNode characterLiteral)
        {
            Debugger.Write("Generating code for CharacterLiteral");
            code.AddInstruction(OpCode.LOADL, 0, 0, (short)characterLiteral.Value);
        }

        /// <summary>
        /// Generates code for an identifier node
        /// </summary>
        /// <param name="identifier">The node to generate code for</param>
        private void GenerateCodeForIdentifier(IdentifierNode identifier)
        {
            Debugger.Write("Generating code for Identifier");
            if (identifier.Declaration is IPrimitiveDeclarationNode primitiveDeclaration)
                code.AddInstruction(OpCode.CALL, Register.PB, (byte)Register.SB, (short)primitiveDeclaration.Primitive);
            else
                Debugger.Write("Error: The identifier declaration isn't one of the built in functions and you should have picked this problem up during type checking");
        }

        /// <summary>
        /// Generates code for an integer literal node
        /// </summary>
        /// <param name="integerLiteral">The node to generate code for</param>
        private void GenerateCodeForIntegerLiteral(IntegerLiteralNode integerLiteral)
        {
            Debugger.Write("Generating code for Integer Literal");
            code.AddInstruction(OpCode.LOADL, 0, 0, (short)integerLiteral.Value);
        }

        /// <summary>
        /// Generates code for an operation node
        /// </summary>
        /// <param name="operation">The node to generate code for</param>
        private void GenerateCodeForOperator(OperatorNode operation)
        {
            Debugger.Write("Generating code for Operator");
            if (operation.Declaration is IPrimitiveDeclarationNode primativeDeclaration)
                code.AddInstruction(OpCode.CALL, Register.PB, 0, (short)primativeDeclaration.Primitive);
            else
                Debugger.Write("Error: The operator declaration isn't one of the built in operations and you should have picked this problem up during type checking");
        }
    }
}
