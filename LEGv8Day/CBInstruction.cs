namespace LEGv8Day
{
    public class CBInstruction : Instruction
    {
        private int CondBrAddress => _data.GetRange(5, 23);

        private int Rt => _data.GetRange(0, 4);

        public CBInstruction(CoreInstruction instruction, int opcode, int condBrAddress, int rt) : base(instruction)
        {
            _data = 0;
            _data.SetRange(24, 31, opcode);
            _data.SetRange(5, 23, condBrAddress);
            _data.SetRange(0, 4, rt);
        }

        public override void Evaluate(Emulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.CBZ:
                    if (simulation.GetReg(Rt) == 0)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.CBNZ:
                    if (simulation.GetReg(Rt) != 0)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;

                case InstructionMnemonic.B_EQ:
                    if (simulation.ZeroFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_NE:
                    if (!simulation.ZeroFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_LT:
                case InstructionMnemonic.B_LO:
                    if (!simulation.ZeroFlag && simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_LE:
                case InstructionMnemonic.B_LS:
                    if (simulation.ZeroFlag || simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_GT:
                case InstructionMnemonic.B_HI:
                    if (!simulation.ZeroFlag && !simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_GE:
                case InstructionMnemonic.B_HS:
                    if (simulation.ZeroFlag || !simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_MI:
                    if (simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_PL:
                    if(!simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
