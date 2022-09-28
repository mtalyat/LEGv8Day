namespace LEGv8Day
{
    public class BInstruction : Instruction
    {
        private int BrAddress => _data.GetRange(0, 25);

        public BInstruction(CoreInstruction instruction, int opcode, int brAddress) : base(instruction)
        {
            _data = 0;
            _data.SetRange(26, 31, opcode);
            _data.SetRange(0, 25, brAddress);
        }

        public override void Evaluate(Simulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.B:
                    simulation.ExecutionIndex = BrAddress;
                    break;
                case InstructionMnemonic.BL:
                    simulation.SetReg(Simulation.RETURN_ADDRESS_REG, simulation.ExecutionIndex);
                    simulation.ExecutionIndex = BrAddress;
                    break;
                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }

        }
    }
}
