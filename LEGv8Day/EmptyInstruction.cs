namespace LEGv8Day
{
    public class EmptyInstruction : Instruction
    {
        public EmptyInstruction(int lineNumber) : base(new CoreInstruction(), lineNumber)
        {

        }

        public override void Evaluate(Emulation simulation)
        {

        }
    }
}
