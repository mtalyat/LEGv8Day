using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public abstract class Instruction
    {
        protected PackedInt _data;

        protected CoreInstruction _instruction;

        public int MachineCode => _data.Int;

        public Instruction(CoreInstruction instruction)
        {
            _data = new PackedInt();
            _instruction = instruction;
        }

        public abstract void Evaluate(Simulation simulation);

        public override string ToString()
        {
            //print in binary
            return Convert.ToString(MachineCode, 2).PadLeft(sizeof(int) * 8, '0');
        }

        /// <summary>
        /// Extends the most signigicant bit in the given value.
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
