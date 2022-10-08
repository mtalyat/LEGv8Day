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
                case InstructionMnemonic.CLR:
                    e.Clear();
                    break;

                case InstructionMnemonic.DUMP:
                    e.Dump();
                    break;
                case InstructionMnemonic.DUMPAM:
                    e.DumpAllMemory();
                    break;
                case InstructionMnemonic.DUMPAR:
                    e.DumpAllRegisters();
                    break;

                case InstructionMnemonic.LOG:
                    e.Log(_contents);
                    break;
            }
        }
    }
}
