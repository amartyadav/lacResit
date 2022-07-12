using static Compiler.CodeGeneration.TriangleAbstractMachine.OpCode;

namespace Compiler.CodeGeneration
{
    /// <summary>
    /// A target language constant with a value that is known at compile time
    /// </summary>
    public class RuntimeKnownConstant : IRuntimeEntity
    {
        /// <summary>
        /// The value of the constant
        /// </summary>
        public short Value { get; }

        /// <summary>
        /// Creates a new runtime constant
        /// </summary>
        /// <param name="value">The value of the constant</param>
        public RuntimeKnownConstant(short value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates an instruction that loads the entity's value
        /// </summary>
        /// <returns>An instruction which when executed will load the entity's current value onto the top of the stack</returns>
        public Instruction GenerateInstructionToLoad()
        {
            return new Instruction(LOADL, 0, 0, Value);
        }

        /// <summary>
        /// Creates an instruction that loads the entity's address
        /// </summary>
        /// <returns>An instruction which when executed will load the entity's address onto the top of the stack</returns>
        public Instruction GenerateInstructionToLoadAddress()
        {
            // Shouldn't do this for a constant
            return null;
        }

        /// <summary>
        /// Creates an instruction that stores the entity
        /// </summary>
        /// <returns>An instruction which when executed will store the value on the top of the stack in the entity</returns>
        public Instruction GenerateInstructionToStore()
        {
            // Shouldn't do this for a constant
            return null;
        }
    }
}
