namespace LEGv8Day
{
    public class IInstruction : Instruction
    {
        private int AluImmediate => ExtendMSB(_data.GetRange(10, 21), 11);

        private int Rn => _data.GetRange(5, 9);

        private int Rd => _data.GetRange(0, 4);

        public IInstruction(CoreInstruction instruction, int opcode, int aluImmediate, int rn, int rd) : base(instruction)
        {
            _data = 0;
            _data.SetRange(22, 31, opcode);
            _data.SetRange(10, 21, aluImmediate);
            _data.SetRange(5, 9, rn);
            _data.SetRange(0, 4, rd);
        }

        public override void Evaluate(Emulation simulation)
        {
            long left = simulation.GetReg(Rn);
            long right = AluImmediate;
            long value;

            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.ADDI:
                    unchecked { value = left + right; }
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.ADDIS:
                    unchecked { value = left + right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.SUBI:
                    unchecked { value = left - right; }
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.SUBIS:
                    unchecked { value = left - right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.ANDI:
                    simulation.SetReg(Rd, left & right);
                    break;
                case InstructionMnemonic.ANDIS:
                    value = left & right;
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.EORI:
                    simulation.SetReg(Rd, left ^ right);
                    break;
                case InstructionMnemonic.ORRI:
                    simulation.SetReg(Rd, left | right);
                    break;

                case InstructionMnemonic.CMPI:
                    unchecked { value = left - right; }
                    simulation.SetFlags(value, left, right);
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
