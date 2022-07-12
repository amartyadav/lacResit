namespace Compiler.CodeGeneration
{
    /// <summary>
    /// A target code variable or constant
    /// </summary>
    public interface IRuntimeEntity
    {
        /// <summary>
        /// Creates an instruction that loads the entity's value
        /// </summary>
        /// <returns>An instruction which when executed will load the entity's current value onto the top of the stack</returns>
        Instruction GenerateInstructionToLoad();

        /// <summary>
        /// Creates an instruction that loads the entity's address
        /// </summary>
        /// <returns>An instruction which when executed will load the entity's address onto the top of the stack</returns>
        Instruction GenerateInstructionToLoadAddress();

        /// <summary>
        /// Creates an instruction that stores the entity
        /// </summary>
        /// <returns>An instruction which when executed will store the value on the top of the stack in the entity</returns>
        Instruction GenerateInstructionToStore();
    }
}