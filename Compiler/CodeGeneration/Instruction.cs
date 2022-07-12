using System;
using static Compiler.CodeGeneration.TriangleAbstractMachine;

namespace Compiler.CodeGeneration
{
    /// <summary>
    /// A single target language instruction
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// The operation
        /// </summary>
        public OpCode OpCode { get; }

        /// <summary>
        /// The register involved
        /// </summary>
        public Register Register { get; }

        /// <summary>
        /// The length argument
        /// </summary>
        public byte Length { get; }

        /// <summary>
        /// The offset or value argument
        /// </summary>
        public short OffsetOrValue { get; }

        /// <summary>
        /// Creates a new instruction
        /// </summary>
        /// <param name="opCode">The operation</param>
        /// <param name="register">The register involved</param>
        /// <param name="length">The length argument</param>
        /// <param name="offsetOrValue">The offset or value argument</param>
        public Instruction(OpCode opCode, Register register, byte length, short offsetOrValue)
        {
            OpCode = opCode;
            Length = length;
            Register = register;
            OffsetOrValue = offsetOrValue;
        }

        /// <inheritDoc />
        public override string ToString()
        {
            switch (OpCode)
            {
                case OpCode.LOAD:
                    return $"LOAD   {Length} {GetName(Register)}+{OffsetOrValue}";
                case OpCode.LOADA:
                    return $"LOADA  {GetName(Register)}+{OffsetOrValue}";
                case OpCode.LOADI:
                    return $"LOADI  {Length}";
                case OpCode.LOADL:
                    return $"LOADL  {OffsetOrValue}";
                case OpCode.STORE:
                    return $"STORE  {Length} {GetName(Register)}+{OffsetOrValue}";
                case OpCode.STOREI:
                    return $"STOREI {Length}";
                case OpCode.CALL:
                    return $"CALL   {GetName((Primitive)OffsetOrValue)}";
                case OpCode.CALLI:
                    return "CALLI";
                case OpCode.RETURN:
                    return $"RETURN {Length} {OffsetOrValue}";
                case OpCode.PUSH:
                    return $"PUSH   {OffsetOrValue}";
                case OpCode.POP:
                    return $"POP    {Length} {OffsetOrValue}";
                case OpCode.JUMP:
                    return $"JUMP   {GetName(Register)}+{OffsetOrValue}";
                case OpCode.JUMPI:
                    return "JUMPI";
                case OpCode.JUMPIF:
                    return $"JUMPIF {(Length == TrueValue ? "TRUE" : "FALSE")} {GetName(Register)}+{OffsetOrValue}";
                case OpCode.HALT:
                    return "HALT";
                default:
                    return $"Unknown OpCode";
            }
        }

        /// <summary>
        /// Creates a low-level representation of the instruction
        /// </summary>
        /// <returns>The instruction as just numbers</returns>
        public string ToSimpleString() { return $"{(byte)OpCode} {(byte)Register} {(byte)Length} {(short)OffsetOrValue}"; }

        /// <summary>
        /// Converts the instruction to binary
        /// </summary>
        /// <returns>The instruction as bytes of binary code</returns>
        public byte[] ToBinary()
        {
            byte[] offsetAsTwoBytes = BitConverter.GetBytes(OffsetOrValue);
            return new byte[] { (byte)OpCode, (byte)Register, Length, offsetAsTwoBytes[0], offsetAsTwoBytes[1] };
        }

        /// <summary>
        /// Gets the name of an enum member
        /// </summary>
        /// <param name="value">The value to get the name of</param>
        /// <returns>The name of the given value</returns>
        private static string GetName<T>(T value) where T : Enum { return Enum.GetName(typeof(T), value); }
    }
}