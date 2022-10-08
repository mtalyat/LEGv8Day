using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public class ZInstruction : Instruction
    {
        private string _contents;

        public ZInstruction(CoreInstruction instruction, int lineNumber, string contents) : base(instruction, lineNumber)
        {
            _data = 0;
            _contents = contents;
        }

        public override void Evaluate(Emulation e)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.DUMP:
                    e.Dump();
                    break;
                case InstructionMnemonic.PRNT:
                    e.Print(_contents);
                    break;
                case InstructionMnemonic.PRNL:
                    e.Print("");
                    break;
            }
        }
    }
}
