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

        public ZInstruction(CoreInstruction instruction, string contents) : base(instruction)
        {
            _data = 0;
            _contents = contents;
        }

        public override void Evaluate(Simulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.CLR:
                    simulation.Clear();
                    break;

                case InstructionMnemonic.DUMP:
                    simulation.Dump();
                    break;
                case InstructionMnemonic.DUMPAM:
                    simulation.DumpAllMemory();
                    break;
                case InstructionMnemonic.DUMPAR:
                    simulation.DumpAllRegisters();
                    break;

                case InstructionMnemonic.LOG:
                    simulation.Log(_contents);
                    break;
            }
        }
    }
}
