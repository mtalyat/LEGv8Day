namespace LEGv8Day
{
    public class DInstruction : Instruction
    {
        private int DtAddress => _data.GetRange(12, 20);

        private int Op => _data.GetRange(10, 11);

        private int Rn => _data.GetRange(5, 9);

        private int Rt => _data.GetRange(0, 4);

        public DInstruction(CoreInstruction instruction, int opcode, int dtAddress, int op, int rn, int rt) : base(instruction)
        {
            _data = 0;
            _data.SetRange(21, 31, opcode);
            _data.SetRange(12, 20, dtAddress);
            _data.SetRange(10, 11, op);
            _data.SetRange(5, 9, rn);
            _data.SetRange(0, 4, rt);
        }

        public override void Evaluate(Emulation e)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.LDUR:
                    e.SetReg(Rt, e.GetMem((int)e.GetReg(Rn) + DtAddress, sizeof(long)));
                    break;
                case InstructionMnemonic.LDURB:
                    e.SetReg(Rt, e.GetMem((int)e.GetReg(Rn) + DtAddress, sizeof(byte)));
                    break;
                case InstructionMnemonic.LDURH:
                    e.SetReg(Rt, e.GetMem((int)e.GetReg(Rn) + DtAddress, sizeof(short)));
                    break;
                case InstructionMnemonic.LDURSW:
                    e.SetReg(Rt, e.GetMem((int)e.GetReg(Rn) + DtAddress, sizeof(int)));
                    break;

                case InstructionMnemonic.STUR:
                    e.SetMem((int)e.GetReg(Rn) + DtAddress, e.GetReg(Rt), sizeof(long));
                    break;
                case InstructionMnemonic.STURB:
                    e.SetMem((int)e.GetReg(Rn) + DtAddress, e.GetReg(Rt), sizeof(byte));
                    break;
                case InstructionMnemonic.STURH:
                    e.SetMem((int)e.GetReg(Rn) + DtAddress, e.GetReg(Rt), sizeof(short));
                    break;
                case InstructionMnemonic.STURW:
                    e.SetMem((int)e.GetReg(Rn) + DtAddress, e.GetReg(Rt), sizeof(int));
                    break;

                case InstructionMnemonic.LDA:
                    e.SetReg(Rt, e.GetReg(Rn) + DtAddress);
                    break;

                case InstructionMnemonic.DUMPM:
                    e.DumpMemory(e.GetReg(Rt));
                    break;
                case InstructionMnemonic.DUMPMR:
                    long address = e.GetReg(Rt);
                    e.DumpMemoryRange(address, address + DtAddress);
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
