using Compiler.IO;
using System;
using System.Collections.Generic;
using System.Text;
using static Compiler.CodeGeneration.TriangleAbstractMachine;

namespace Compiler.CodeGeneration
{
    /// <summary>
    /// Code in the target language
    /// </summary>
    public class TargetCode
    {
        /// <summary>
        /// The error reporter
        /// </summary>
        public ErrorReporter Reporter { get; }

        /// <summary>
        /// The instructions in the code
        /// </summary>
        private List<Instruction> Instructions { get; } = new List<Instruction>();

        /// <summary>
        /// The address of the first instruction in the code
        /// </summary>
        public Address StartAddress { get; }

        /// <summary>
        /// The address of the next instruction in the code
        /// </summary>
        public Address NextAddress { get { return (short)Instructions.Count; } }

        /// <summary>
        /// Checks whether or not there is space to add more instructions to the code
        /// </summary>
        public bool IsFull { get { return NextAddress + StartAddress == short.MaxValue; } }

        /// <summary>
        /// Creates a new (initially empty) segment of target code
        /// </summary>
        /// <param name="startAddress">The start address of the code</param>
        /// <param name="reporter">The error reporter</param>
        public TargetCode(Address startAddress, ErrorReporter reporter)
        {
            StartAddress = startAddress;
            Reporter = reporter;
        }

        /// <summary>
        /// Adds an instruction to the end of the code
        /// </summary>
        /// <param name="command">The instruction's command</param>
        /// <param name="register">The instruction's register argument</param>
        /// <param name="length">The instruction's length argument</param>
        /// <param name="offsetOrValue">The instruction's offset or value argument</param>
        public void AddInstruction(OpCode command, Register register, byte length, short offsetOrValue)
        {
            AddInstruction(new Instruction(command, register, length, offsetOrValue));
        }

        /// <summary>
        /// Adds an instruction to the end of the code
        /// </summary>
        /// <param name="instruction">The instruction to add</param>
        public void AddInstruction(Instruction instruction)
        {
            if (IsFull)
            {
                // Error - Out of space to store program - added too many instructions
            }
            else
                Instructions.Add(instruction);
        }

        /// <summary>
        /// Updates a jump instruction to jump to the next instruction
        /// </summary>
        /// <param name="address">The address of the instruction to update</param>
        public void PatchInstructionToJumpHere(Address address)
        {
            if (address < 0 || address >= Instructions.Count)
                Debugger.Write("Tried to backpatch an instruction which is at an address that is not in the generated program");
            else
            {
                Instruction original = Instructions[address];
                if (original.OpCode == OpCode.JUMP || original.OpCode == OpCode.JUMPIF)
                    Instructions[address] = new Instruction(original.OpCode, original.Register, original.Length, NextAddress);
                else
                    Debugger.Write("Tried to backpatch an instruction which is not a JUMP or JUMPIF");
            }
        }

        /// <inheritDoc />
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Instructions.Count; i++)
                sb.AppendLine($"{(StartAddress + i).ToString().PadLeft(4)}: {Instructions[i].ToString().PadRight(20)} ({Instructions[i].ToSimpleString()})");
            return sb.ToString();
        }

        /// <summary>
        /// Gets a binary representation of the program
        /// </summary>
        /// <returns>The program in binary format</returns>
        public Byte[] ToBinary()
        {
            List<Byte> output = new List<byte>();
            foreach (Instruction instruction in Instructions)
                output.AddRange(instruction.ToBinary());
            return output.ToArray();
        }
    }
}