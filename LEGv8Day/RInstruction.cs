namespace LEGv8Day
{
    public class RInstruction : Instruction
    {
        private int Rm => _data.GetRange(16, 20);

        private int Shamt => _data.GetRange(10, 15);

        private int Rn => _data.GetRange(5, 9);

        private int Rd => _data.GetRange(0, 4);

        public RInstruction(CoreInstruction instruction, int lineNumber, int opcode, int rm, int shamt, int rn, int rd) : base(instruction, lineNumber)
        {
            _data = 0;
            _data.SetRange(21, 31, opcode);
            _data.SetRange(16, 20, rm);
            _data.SetRange(10, 15, shamt);
            _data.SetRange(5, 9, rn);
            _data.SetRange(0, 4, rd);
        }

        public override void Evaluate(Emulation e)
        {
            long left = e.GetReg(Rn);
            long right = e.GetReg(Rm);
            long value;

            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.ADD:
                    unchecked { e.SetReg(Rd, left + right); }
                    break;
                case InstructionMnemonic.ADDS:
                    unchecked { value = left + right; }
                    e.SetFlags(value, left, right);
                    e.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.SUB:
                    unchecked { e.SetReg(Rd, left - right); }
                    break;
                case InstructionMnemonic.SUBS:
                    unchecked { value = left - right; }
                    e.SetFlags(value, left, right);
                    e.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.MUL:
                    unchecked { e.SetReg(Rd, left * right); }
                    break;
                case InstructionMnemonic.UDIV:
                    e.SetRegR(Rd, e.GetRegR<ulong>(Rn) / e.GetRegR<ulong>(Rm));
                    break;
                case InstructionMnemonic.SDIV:
                    e.SetReg(Rd, left / right);
                    break;
                case InstructionMnemonic.AND:
                    e.SetReg(Rd, left & right);
                    break;
                case InstructionMnemonic.ANDS:
                    value = left & right;
                    e.SetFlags(value, left, right);
                    e.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.EOR:
                    e.SetReg(Rd, left ^ right);
                    break;
                case InstructionMnemonic.ORR:
                    e.SetReg(Rd, left | right);
                    break;
                case InstructionMnemonic.LSL:
                    e.SetReg(Rd, left << Shamt);
                    break;
                case InstructionMnemonic.LSR:
                    e.SetReg(Rd, left >> Shamt);
                    break;
                case InstructionMnemonic.BR:
                    e.ExecutionIndex = (int)e.GetReg(Rd);
                    break;
                case InstructionMnemonic.FADDS:
                    e.SetRegR(Rd, e.GetRegR<float>(Rn) + e.GetRegR<float>(Rm));
                    break;

                case InstructionMnemonic.CMP:
                    unchecked { value = left + right; }
                    e.SetFlags(value, left, right);
                    break;
                case InstructionMnemonic.MOV:
                    e.SetReg(Rd, left);
                    break;

                case InstructionMnemonic.DUMPR:
                    e.DumpRegister(Rd);
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
