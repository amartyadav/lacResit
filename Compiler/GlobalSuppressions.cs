// This file just stops Visual Studio suggesting some code "improvements" that it is a bad idea to accept

// These issues occur because we use reflection to call functions
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Scope = "type", Target = "Compiler.CodeGeneration.CodeGenerator")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Scope = "type", Target = "Compiler.SemanticAnalysis.TypeChecker")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Scope = "type", Target = "Compiler.SemanticAnalysis.DeclarationIdentifier")]

// These issues are because we need to pass a AST node but then don't use it
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Scope = "type", Target = "Compiler.CodeGeneration.CodeGenerator")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Scope = "type", Target = "Compiler.SemanticAnalysis.TypeChecker")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Scope = "type", Target = "Compiler.SemanticAnalysis.DeclarationIdentifier")]

// The code for this method is ugly enough without this crazy suggestion
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Scope = "member", Target = "~M:Compiler.IO.TreePrinter.GetCollectionOfChildNodes(Compiler.Nodes.IAbstractSyntaxTreeNode)~System.Collections.Generic.List{Compiler.Nodes.IAbstractSyntaxTreeNode}")]

// These suggestions are generally good, but makes the code less clear here
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028:Simplify collection initialization", Scope = "member", Target = "~M:Compiler.SyntacticAnalysis.Parser.ParseCommand~Compiler.Nodes.ICommandNode")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028:Simplify collection initialization", Scope = "member", Target = "~M:Compiler.SyntacticAnalysis.Parser.ParseDeclaration~Compiler.Nodes.IDeclarationNode")]
