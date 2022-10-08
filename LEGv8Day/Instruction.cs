using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    /// <summary>
    /// Holds data for an instruction in LEGv8, that can be executed.
    /// </summary>
    public abstract class Instruction
    {
        /// <summary>
        /// The raw data for the instruction, stored within an int.
        /// </summary>
        protected PackedInt _data;

        /// <summary>
        /// The Core Instruction associated with this Instruction.
        /// </summary>
        protected CoreInstruction _instruction;

        /// <summary>
        /// The raw machine code that this Instruction represents.
        /// </summary>
        public int MachineCode => _data.Int;

        /// <summary>
        /// The line number this instruction is on in the file.
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// Creates a new Instruction using the given Core Instruction.
        /// </summary>
        /// <param name="instruction"></param>
        public Instruction(CoreInstruction instruction, int lineNumber)
        {
            _data = new PackedInt();
            _instruction = instruction;
            LineNumber = lineNumber;
        }

        /// <summary>
        /// Evaluates this Instruction, using the given Simulation.
        /// </summary>
        /// <param name="e"></param>
        public abstract void Evaluate(Emulation e);

        public override string ToString()
        {
            return $"{_instruction.Mnemonic} on line {LineNumber}";
        }

        public string ToStackTraceString(int spacing)
        {
            return $"{LineNumber.ToString().PadLeft(spacing)}: {_instruction.Mnemonic.ToString().PadRight(6)} {_data}";
        }

        /// <summary>
        /// Extends the most signigicant bit in the given value to fill the size of the int.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static int ExtendMSB(int value, int sizeInBits)
        {
            unsafe
            {
                //get the bit
                bool bit = (value >> (sizeInBits - 1)) != 0;

                //set to all remaining bits
                PackedInt packed = new PackedInt(value);
                packed.Set(sizeInBits, bit ? ~0 : 0, sizeof(int) - sizeInBits);

                return packed;
            }
        }
    }
}
