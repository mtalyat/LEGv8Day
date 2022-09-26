using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public class CoreInstruction
    {
        public string Name { get; private set; }

        public InstructionMnemonic Mnemonic { get; private set; }

        public InstructionFormat Format { get; private set; }

        public int OpCodeStart { get; private set; }
        public int OpCodeStop { get; private set; }

        public int Shamt { get; private set; }

        public CoreInstruction()
            : this("", InstructionMnemonic.Empty, InstructionFormat.Empty, 0, 0, 0) { }

        public CoreInstruction(string name, InstructionMnemonic mnemonic, InstructionFormat format, int opStart, int opStop)
            : this(name, mnemonic, format, opStart, opStop, 0) { }

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
