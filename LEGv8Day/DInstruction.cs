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

        public override void Evaluate(Simulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.LDUR:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(long)));
                    break;
                case InstructionMnemonic.LDURB:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(byte)));
                    break;
                case InstructionMnemonic.LDURH:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(short)));
                    break;
                case InstructionMnemonic.LDURSW:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(int)));
                    break;

                case InstructionMnemonic.STUR:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(long));
                    break;
                case InstructionMnemonic.STURB:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(byte));
                    break;
                case InstructionMnemonic.STURH:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(short));
                    break;
                case InstructionMnemonic.STURW:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(int));
                    break;

                case InstructionMnemonic.LDA:
                    simulation.SetReg(Rt, simulation.GetReg(Rn) + DtAddress);
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
