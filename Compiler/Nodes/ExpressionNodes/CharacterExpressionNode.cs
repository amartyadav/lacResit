namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a character literal expression
    /// </summary>
    public class CharacterExpressionNode : IExpressionNode
    {
        /// <summary>
        /// The character literal
        /// </summary>
        public CharacterLiteralNode CharLit { get; }

        /// <summary>
        /// The type of the node
        /// </summary>
        public SimpleTypeDeclarationNode Type { get; set; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get { return CharLit.Position; } }

        /// <summary>
        /// Creates a new character literal expression node
        /// </summary>
        /// <param name="charLit">The character literal</param>
        public CharacterExpressionNode(CharacterLiteralNode charLit)
        {
            CharLit = charLit;
        }
    }
}