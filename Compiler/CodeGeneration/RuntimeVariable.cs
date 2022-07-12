using static Compiler.CodeGeneration.TriangleAbstractMachine.OpCode;
using static Compiler.CodeGeneration.TriangleAbstractMachine.Register;

namespace Compiler.CodeGeneration
{
    /// <summary>
    /// A target language variable
    /// </summary>
    public class RuntimeVariable : IRuntimeEntity
    {
        /// <summary>
        /// The position of the variable, relative to the bottom of the stack frame
        /// </summary>
        public short Offset { get; }

        /// <summary>
        /// The number of words needed to store the variable
        /// </summary>
        public byte Size { get; }

        /// <summary>
        /// Creates a new variable
        /// </summary>
        /// <param name="offset">The position of the variable, relative to the bottom of the stack frame</param>
        /// <param name="size">The number of words needed to store the variable</param>
        public RuntimeVariable(short offset, byte size)
        {
            Offset = offset;
            Size = size;
        }

        /// <summary>
        /// Creates an instruction that loads the entity's value
        /// </summary>
        /// <returns>An instruction which when executed will load the entity's current value onto the top of the stack</returns>
        public Instruction GenerateInstructionToLoad()
        {
            return new Instruction(LOAD, SB, Size, Offset);
        }

        /// <summary>
        /// Creates an instruction that loads the entity's address
        /// </summary>
        /// <returns>An instruction which when executed will load the entity's address onto the top of the stack</returns>
        public Instruction GenerateInstructionToLoadAddress()
        {
            return new Instruction(LOADA, SB, 0, Offset);
        }

        /// <summary>
        /// Creates an instruction that stores the entity
        /// </summary>
        /// <returns>An instruction which when executed will store the value on the top of the stack in the entity</returns>
        public Instruction GenerateInstructionToStore()
        {
            return new Instruction(STORE, SB, Size, Offset);
        }
    }
}
