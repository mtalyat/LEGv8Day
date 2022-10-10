namespace LEGv8Day
{
    public class CBInstruction : Instruction
    {
        private int CondBrAddress => _data.GetRange(5, 23);

        private int Rt => _data.GetRange(0, 4);

        public CBInstruction(CoreInstruction instruction, int lineNumber, int opcode, int condBrAddress, int rt) : base(instruction, lineNumber)
        {
            _data = 0;
            _data.SetRange(24, 31, opcode);
            _data.SetRange(5, 23, condBrAddress);
            _data.SetRange(0, 4, rt);
        }

        public override void Evaluate(Emulation emulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.CBZ:
                    if (emulation.GetReg(Rt) == 0)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.CBNZ:
                    if (emulation.GetReg(Rt) != 0)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;

                case InstructionMnemonic.B_EQ:
                    if (emulation.ZeroFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_NE:
                    if (!emulation.ZeroFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_LT:
                case InstructionMnemonic.B_LO:
                    if (!emulation.ZeroFlag && emulation.NegativeFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_LE:
                case InstructionMnemonic.B_LS:
                    if (emulation.ZeroFlag || emulation.NegativeFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_GT:
                case InstructionMnemonic.B_HI:
                    if (!emulation.ZeroFlag && !emulation.NegativeFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_GE:
                case InstructionMnemonic.B_HS:
                    if (emulation.ZeroFlag || !emulation.NegativeFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_MI:
                    if (emulation.NegativeFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_PL:
                    if(!emulation.NegativeFlag)
                        emulation.ExecutionIndex = CondBrAddress;
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
