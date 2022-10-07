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
        /// Creates a new Instruction using the given Core Instruction.
        /// </summary>
        /// <param name="instruction"></param>
        public Instruction(CoreInstruction instruction)
        {
            _data = new PackedInt();
            _instruction = instruction;
        }

        /// <summary>
        /// Evaluates this Instruction, using the given Simulation.
        /// </summary>
        /// <param name="e"></param>
        public abstract void Evaluate(Emulation e);

        public override string ToString()
        {
            //print in binary
            return Convert.ToString(MachineCode, 2).PadLeft(sizeof(int) * 8, '0');
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
                //MessageBox.Show($"Setting at {sizeInBits}, value: {Convert.ToString(bit ? ~0 : 0, 2)}, length: {sizeof(long) - sizeInBits} to existing value {value}");

                return packed;
            }
        }
    }
}
