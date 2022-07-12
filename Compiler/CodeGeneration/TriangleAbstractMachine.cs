using System.Collections.Generic;
using System.Collections.Immutable;

namespace Compiler.CodeGeneration
{
    /// <summary>
    /// Enumerations and Constants associated with the Abstract Machine
    /// </summary>
    public static class TriangleAbstractMachine
    {
        /// <summary>
        /// The operations on the machine
        /// </summary>
        public enum OpCode : byte
        {
            /// <summary>
            /// LOAD REGISTER LENGTH OFFSET
            /// </summary>
            /// <remarks>
            /// Load LENGTH words onto the stack starting from address (REGISTER contents + OFFSET)
            /// </remarks>
            LOAD = 0,
            /// <summary>
            /// LOADA REGISTER _ OFFSET
            /// </summary>
            /// <remarks>
            /// Push (REGISTER contents + OFFSET) onto the stack
            /// </remarks>
            LOADA = 1,
            /// <summary>
            /// LOADI _ LENGTH _ 
            /// </summary>
            /// <remarks>
            /// Pop an address from the top of the stack and load LENGTH words onto the stack starting from the popped address
            /// </remarks>
            LOADI = 2,
            /// <summary>
            /// LOADL _ _ VALUE
            /// </summary>
            /// <remarks>
            /// Push VALUE onto the stack
            /// </remarks>
            LOADL = 3,
            /// <summary>
            /// STORE REGISTER LENGTH OFFSET
            /// </summary>
            /// <remarks>
            /// Pop LENGTH words from the stack and save them to memory starting at address (REGISTER contents + OFFSET)
            /// </remarks>
            STORE = 4,
            /// <summary>
            /// STOREI _ LENGTH _
            /// </summary>
            /// <remarks>
            /// Pop an address from the top of the stack, then pop LENGTH words from the stack and save them to memory starting at the popped address
            /// </remarks>
            STOREI = 5,
            /// <summary>
            /// CALL REGISTER _ OFFSET
            /// </summary>
            /// <remarks>
            /// Call the built-in function at address (REGISTER contents + OFFSET)
            /// 
            /// Can be used with user-defined functions too, but that is beyond the scope of this course
            /// </remarks>
            CALL = 6,
            /// <summary>
            /// CALLI _ _ _
            /// </summary>
            /// <remarks>
            /// Pop an address from the top of the stack, then call the built-in function at the popped address
            /// 
            /// Can be used with user-defined functions too, but that is beyond the scope of this course
            /// </remarks>
            CALLI = 7,
            /// <summary>
            /// RETURN _ RESULT PARAMTERS
            /// </summary>
            /// <remarks>
            /// Return from a function
            /// 
            /// Pop RESULT words off the top of the stack, then pop the local frame + PARAMTERS words off the top of the stack, 
            /// then push the original RESULT words back onto the pop of the stack, then jump back to the address the function
            /// was called from
            /// </remarks>
            RETURN = 8,
            /// <summary>
            /// PUSH _ _ VALUE
            /// </summary>
            /// <remarks>
            /// Push VALUE words onto the stack
            /// </remarks>
            PUSH = 9,
            /// <summary>
            /// POP _ KEEP DISCARD
            /// </summary>
            /// <remarks>
            /// Pop KEEP words off the top of the stack, then pop DISCARD words off the top of the stack, 
            /// then push the original KEEP words back onto the pop of the stack
            /// </remarks>
            POP = 10,
            /// <summary>
            /// JUMP REGISTER _ OFFSET
            /// </summary>
            /// <remarks>
            /// Jump to address (REGISTER contents + OFFSET)
            /// </remarks>
            JUMP = 11,
            /// <summary>
            /// JUMPI _ _ _
            /// </summary>
            /// <remarks>
            /// Pop an address from the stack then jump to this address
            /// </remarks>
            JUMPI = 12,
            /// <summary>
            /// JUMPIF REGISTER VALUE OFFSET
            /// </summary>
            /// <remarks>
            /// Pop a value off the top of the stack, if it is equal to VALUE then jump to address (REGISTER contents + OFFSET)
            /// </remarks>
            JUMPIF = 13,
            /// <summary>
            /// HALT _ _ _
            /// </summary>
            /// <remarks>
            /// Stop the machine
            /// </remarks>
            HALT = 14
        }

        /// <summary>
        /// The registers available on the machine
        /// </summary>
        public enum Register : byte
        {
            /// <summary>
            /// CodeBase - start of the program
            /// </summary>
            CB = 0,
            /// <summary>
            /// CodeTop - end of the program
            /// </summary>
            CT = 1,
            /// <summary>
            /// PrimitiveBase - start of built-in functions
            /// </summary>
            PB = 2,
            /// <summary>
            /// PrimitiveTop - end of built-in functions
            /// </summary>
            PT = 3,
            /// <summary>
            /// StackBase - bottom of stack
            /// </summary>
            SB = 4,
            /// <summary>
            /// StackTop - top of stack
            /// </summary>
            ST = 5,
            /// <summary>
            /// HeapBase - bottom of heap
            /// </summary>
            HB = 6,
            /// <summary>
            /// HeapTop - top of heap
            /// </summary>
            HT = 7,
            /// <summary>
            /// LocalBase - bottom of local frame on stack
            /// </summary>
            LB = 8,
            /// <summary>
            /// ParentOfLocalBase- bottom of frame containing local frame on stack
            /// </summary>
            L1 = 9,
            /// <summary>
            /// ParentOfParentOfLocalBase- bottom of frame containing frame containing local frame on stack
            /// </summary>
            L2 = 10,
            /// <summary>
            /// ParentOfParentOfParentOfLocalBase- bottom of frame containing frame containing frame containing local frame on stack
            /// </summary>
            L3 = 11,
            /// <summary>
            /// ParentOfParentOfParentOfParentOfLocalBase- bottom of frame containing frame containing frame containing frame containing local frame on stack
            /// </summary>
            L4 = 12,
            /// <summary>
            /// ParentOfParentOfParentOfParentOfParentOfLocalBase- bottom of frame containing frame containing frame containing frame containing frame containing local frame on stack
            /// </summary>
            L5 = 13,
            /// <summary>
            /// ParentOfParentOfParentOfParentOfParentOfParentOfLocalBase- bottom of frame containing frame containing frame containing frame containing frame containing frame containing local frame on stack
            /// </summary>
            L6 = 14,
            /// <summary>
            /// CodePointer - next intrustion to execute
            /// </summary>
            CP = 15
        }

        /// <summary>
        /// The built-in functions available on the machine
        /// </summary>
        public enum Primitive : short
        {
            /// <summary>
            /// void ID() - do nothing
            /// </summary>
            ID = 0,
            /// <summary>
            /// boolean NOT(boolean X) - !x
            /// </summary>
            NOT = 1,
            /// <summary>
            /// boolean AND(boolean X, boolean Y) - X && Y
            /// </summary>
            AND = 2,
            /// <summary>
            /// boolean OR(boolean X, boolean Y) - X || Y
            /// </summary>
            OR = 3,
            /// <summary>
            /// int SUCC(int X) - X+1
            /// </summary>
            SUCC = 4,
            /// <summary>
            /// int PRED(int X) - X-1
            /// </summary>
            PRED = 5,
            /// <summary>
            /// int NEG(int X) - -X
            /// </summary>
            NEG = 6,
            /// <summary>
            /// int ADD(int X, int Y) - X + Y
            /// </summary>
            ADD = 7,
            /// <summary>
            /// int SUB(int X, int Y) - X - Y
            /// </summary>
            SUB = 8,
            /// <summary>
            /// int MULT(int X, int Y) - X * Y
            /// </summary>
            MULT = 9,
            /// <summary>
            /// int DIV(int X, int Y) - floor of X / Y (integer division)
            /// </summary>
            DIV = 10,
            /// <summary>
            /// int MOD(int X, int Y) - remainder of X / Y
            /// </summary>
            MOD = 11,
            /// <summary>
            /// boolean LT(any X, any Y) - Check if X < Y
            /// </summary>
            LT = 12,
            /// <summary>
            /// boolean LE(any X, any Y) - Check if X <= Y
            /// </summary>
            LE = 13,
            /// <summary>
            /// boolean GE(any X, any Y) - Check if X >= Y
            /// </summary>
            GE = 14,
            /// <summary>
            /// boolean GT(any X, any Y) - Check if X > Y
            /// </summary>
            GT = 15,
            /// <summary>
            /// boolean EQ(any X, any Y) - Check if X = Y
            /// </summary>
            EQ = 16,
            /// <summary>
            /// boolean NE(any X, any Y) - Check if X != Y
            /// </summary>
            NE = 17,
            /// <summary>
            /// char EOL() - Return a newline character
            /// </summary>
            EOL = 18,
            /// <summary>
            /// char EOF() - Return an end of file character
            /// </summary>
            EOF = 19,
            /// <summary>
            /// void GET(address ADDRESS) - Get a character from input and place it at address ADDRESS
            /// </summary>
            GET = 20,
            /// <summary>
            /// void PUT(char CHAR) - Write CHAR to output
            /// </summary>
            PUT = 21,
            /// <summary>
            /// void GETEOL() - Read and discard input until a new line is read
            /// </summary>
            GETEOL = 22,
            /// <summary>
            /// void PUTEOL() - Write a newline character to output
            /// </summary>
            PUTEOL = 23,
            /// <summary>
            /// void GETINT(address ADDRESS) - Get an int from input and place it at address ADDRESS
            /// </summary>
            GETINT = 24,
            /// <summary>
            /// void PUTINT(int INT) - write INT to output
            /// </summary>
            PUTINT = 25,
            /// <summary>
            /// address NEW(int SIZE) - allocate a block of SIZE words in the heap and return the address of this block
            /// </summary>
            NEW = 26,
            /// <summary>
            /// void DISPOSE(address ADDRESS) - free heap memory, not implemented at present
            /// </summary>
            DISPOSE = 27
        }

        /// <summary>
        /// Types available on the machine
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// TRUE or FALSE
            /// </summary>
            BOOLEAN,
            /// <summary>
            /// A single character
            /// </summary>
            CHARACTER,
            /// <summary>
            /// An integer
            /// </summary>
            INTEGER,
            /// <summary>
            /// A memory address
            /// </summary>
            ADDRESS
        }

        /// <summary>
        /// The size (in words) of each type on the machine
        /// </summary>
        public static ImmutableDictionary<Type, byte> TypeSize { get; } = new Dictionary<Type, byte>()
        {
            { Type.BOOLEAN, 1 },
            { Type.CHARACTER, 1 },
            { Type.INTEGER, 1 },
            { Type.ADDRESS, 1 }
        }.ToImmutableDictionary();

        /// <summary>
        /// The value used for a BOOLEAN value of TRUE
        /// </summary>
        public static byte TrueValue { get; } = 1;

        /// <summary>
        /// The value used for a BOOLEAN value of FALSE
        /// </summary>
        public static byte FalseValue { get; } = 0;

        /// <summary>
        /// The maximum value of integer that can be represented
        /// </summary>
        public static short MaxintValue { get; } = short.MaxValue;

        /// <summary>
        /// The minimum value of integer that can be represented
        /// </summary>
        public static short MinintValue { get; } = short.MinValue;

        /// <summary>
        /// The address to use for the first instruction
        /// </summary>
        public static Address CodeBase { get; } = 0;
    }
}
