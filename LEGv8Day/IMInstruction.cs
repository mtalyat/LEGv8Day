namespace LEGv8Day
{
    public class IMInstruction : Instruction
    {
        private int MovImmediate => _data.GetRange(5, 20);

        private int Rd => _data.GetRange(0, 4);

        public IMInstruction(CoreInstruction instruction, int opcode, int movImmediate, int rd) : base(instruction)
        {
            _data = 0;
            _data.SetRange(21, 31, opcode);
            _data.SetRange(5, 20, movImmediate);
            _data.SetRange(0, 4, rd);
        }

        public override void Evaluate(Emulation e)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.MOVK:
                    long r = e.GetReg(Rd);
                    e.SetReg(Rd, (r & ((long)~0 << 16)) | (long)MovImmediate);
                    break;
                case InstructionMnemonic.MOVZ:
                    e.SetReg(Rd, MovImmediate);
                    break;
                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
