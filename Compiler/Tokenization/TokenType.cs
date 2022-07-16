using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using static Compiler.Tokenization.TokenType;

namespace Compiler.Tokenization
{
    /// <summary>
    /// The types of token in the language
    /// </summary>
    public enum TokenType
    {
        // non-terminals
        IntLiteral, Identifier, Operator, CharLiteral, 

        // reserved words - terminals
        // (Added Pass, Endif, Either, Or) (Removed Begin, End)
        Const, Do, Else, If, In, Let, Then, Var, While, Pass, Endif, IfEither, Or,

        // punctuation - terminals (Becomes is for assignment (:=) , Is is for constants (~))
        // Leaving 'Becomes' as it is although the keyword is no longer used in 22s. := will be called 'Becomes' for 22s as well since they do the same thing
        // Added LeftBraces, RightBraces in place of Begin and End
        Colon, Semicolon, Becomes, Is, LeftBracket, RightBracket, LeftBraces, RightBraces,

        // special tokens
        EndOfText, Error
    }

    /// <summary>
    /// Utility functions for working with the tokens
    /// </summary>
    public static class TokenTypes
    {
        /// <summary>
        /// A mapping from keyword to the token type for that keyword
        /// </summary>
        public static ImmutableDictionary<string, TokenType> Keywords { get; } = new Dictionary<string, TokenType>()
        {
            // { "begin", Begin }, // removed
            { "const", Const },
            { "do", Do },
            { "else", Else },
            // { "end", End }, // removed
            { "if", If },
            { "in", In },
            { "let", Let },
            { "then", Then },
            { "var", Var },
            { "while", While },
            { "pass", Pass}, // added
            { "endif", Endif}, //added 
            { "ifeither", IfEither}, //added
            { "or", Or}, //added
            
            
        }.ToImmutableDictionary();

        /// <summary>
        /// Checks whether a word is a keyword
        /// </summary>
        /// <param name="word">The word to check</param>
        /// <returns>True if and only if the word is a keyword</returns>
        public static bool IsKeyword(StringBuilder word)
        {
            return Keywords.ContainsKey(word.ToString());
        }

        /// <summary>
        /// Gets the token for a keyword
        /// </summary>
        /// <param name="word">The keyword to get the token for</param>
        /// <returns>The token associated with the given keyword</returns>
        /// <remarks>If the word is not a keyword then an exception is thrown</remarks>
        public static TokenType GetTokenForKeyword(StringBuilder word)
        {
            if (!IsKeyword(word)) throw new ArgumentException("Word is not a keyword");
            return Keywords[word.ToString()];
        }
    }
}
