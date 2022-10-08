namespace LEGv8Day
{
    public class BInstruction : Instruction
    {
        private int BrAddress => _data.GetRange(0, 25);

        public BInstruction(CoreInstruction instruction, int lineNumber, int opcode, int brAddress) : base(instruction, lineNumber)
        {
            _data = 0;
            _data.SetRange(26, 31, opcode);
            _data.SetRange(0, 25, brAddress);
        }

        public override void Evaluate(Emulation e)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.B:
                    e.ExecutionIndex = BrAddress;
                    break;
                case InstructionMnemonic.BL:
                    e.SetReg(Emulation.RETURN_ADDRESS_REG, e.ExecutionIndex);
                    e.ExecutionIndex = BrAddress;
                    break;
                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }

        }
    }
}
