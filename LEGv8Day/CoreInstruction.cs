using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    /// <summary>
    /// Core Instructions hold the data regarding instructions in LEGv8.
    /// Core Instructions will be loaded from a file, as their data is static during runtime.
    /// </summary>
    public class CoreInstruction
    {
        /// <summary>
        /// The name of the Core Instruction.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The mnemonic of the Core Instruction, which is the shortened version of the name. This is what users will write.
        /// </summary>
        public InstructionMnemonic Mnemonic { get; private set; }

        /// <summary>
        /// The format of this Core Instruction.
        /// </summary>
        public InstructionFormat Format { get; private set; }

        /// <summary>
        /// The smallest possible opcode for this Core Instruction.
        /// </summary>
        public int OpCodeStart { get; private set; }

        /// <summary>
        /// The largest possible opcode for this Core Instruction.
        /// </summary>
        public int OpCodeStop { get; private set; }

        /// <summary>
        /// The shift ammount for this Core Instruction.
        /// </summary>
        public int Shamt { get; private set; }

        /// <summary>
        /// Creates an empty Core Instruction.
        /// </summary>
        public CoreInstruction()
            : this("", InstructionMnemonic.Empty, InstructionFormat.Empty, 0, 0, 0) { }
        
        /// <summary>
        /// Creates a new Core Instruction using the given data. The shamt is defaulted to 0.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mnemonic"></param>
        /// <param name="format"></param>
        /// <param name="opStart"></param>
        /// <param name="opStop"></param>
        public CoreInstruction(string name, InstructionMnemonic mnemonic, InstructionFormat format, int opStart, int opStop)
            : this(name, mnemonic, format, opStart, opStop, 0) { }

        /// <summary>
        /// Creates a new Core Instruction using the given data.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mnemonic"></param>
        /// <param name="format"></param>
        /// <param name="opStart"></param>
        /// <param name="opStop"></param>
        /// <param name="shamt"></param>
        public CoreInstruction(string name, InstructionMnemonic mnemonic, InstructionFormat format, int opStart, int opStop, int shamt)
        {
            Name = name;
            Mnemonic = mnemonic;
            Format = format;
            OpCodeStart = opStart;
            OpCodeStop = opStop;
            Shamt = shamt;
        }

        public override string ToString()
        {
            return $"{Mnemonic}: \"{Name}\"\t{Format}\t{(OpCodeStart == OpCodeStop ? OpCodeStart : $"{OpCodeStart}-{OpCodeStop}")}\t{Shamt}";
        }
    }
}
