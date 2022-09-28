namespace LEGv8Day
{
    public class EmptyInstruction : Instruction
    {
        public EmptyInstruction() : base(new CoreInstruction())
        {

        }

        public override void Evaluate(Simulation simulation)
        {

        }
    }
}
