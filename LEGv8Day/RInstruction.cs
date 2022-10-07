namespace LEGv8Day
{
    public class RInstruction : Instruction
    {
        private int Rm => _data.GetRange(16, 20);

        private int Shamt => _data.GetRange(10, 15);

        private int Rn => _data.GetRange(5, 9);

        private int Rd => _data.GetRange(0, 4);

        public RInstruction(CoreInstruction instruction, int opcode, int rm, int shamt, int rn, int rd) : base(instruction)
        {
            _data = 0;
            _data.SetRange(21, 31, opcode);
            _data.SetRange(16, 20, rm);
            _data.SetRange(10, 15, shamt);
            _data.SetRange(5, 9, rn);
            _data.SetRange(0, 4, rd);
        }

        public override void Evaluate(Simulation simulation)
        {
            long left = simulation.GetReg(Rn);
            long right = simulation.GetReg(Rm);
            long value;

            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.ADD:
                    unchecked { simulation.SetReg(Rd, left + right); }
                    break;
                case InstructionMnemonic.ADDS:
                    unchecked { value = left + right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.SUB:
                    unchecked { simulation.SetReg(Rd, left - right); }
                    break;
                case InstructionMnemonic.SUBS:
                    unchecked { value = left - right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.MUL:
                    unchecked { simulation.SetReg(Rd, left * right); }
                    break;
                case InstructionMnemonic.UDIV:
                    simulation.SetRegR(Rd, simulation.GetRegR<ulong>(Rn) / simulation.GetRegR<ulong>(Rm));
                    break;
                case InstructionMnemonic.SDIV:
                    simulation.SetReg(Rd, left / right);
                    break;
                case InstructionMnemonic.AND:
                    simulation.SetReg(Rd, left & right);
                    break;
                case InstructionMnemonic.ANDS:
                    value = left & right;
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.EOR:
                    simulation.SetReg(Rd, left ^ right);
                    break;
                case InstructionMnemonic.ORR:
                    simulation.SetReg(Rd, left | right);
                    break;
                case InstructionMnemonic.LSL:
                    simulation.SetReg(Rd, left << Shamt);
                    break;
                case InstructionMnemonic.LSR:
                    simulation.SetReg(Rd, left >> Shamt);
                    break;
                case InstructionMnemonic.BR:
                    simulation.ExecutionIndex = (int)simulation.GetReg(Rd);
                    break;
                case InstructionMnemonic.FADDS:
                    simulation.SetRegR(Rd, simulation.GetRegR<float>(Rn) + simulation.GetRegR<float>(Rm));
                    break;

                case InstructionMnemonic.CMP:
                    unchecked { value = left + right; }
                    simulation.SetFlags(value, left, right);
                    break;
                case InstructionMnemonic.MOV:
                    simulation.SetReg(Rd, left);
                    break;

                case InstructionMnemonic.CLR:
                    simulation.Clear();
                    break;

                case InstructionMnemonic.DUMP:
                    simulation.Dump();
                    break;
                case InstructionMnemonic.DAM:
                    simulation.DumpAllMemory();
                    break;
                case InstructionMnemonic.DAR:
                    simulation.DumpAllRegisters();
                    break;
                case InstructionMnemonic.DR:
                    simulation.DumpRegister(Rd);
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
