namespace LEGv8Day
{
    public class EmptyInstruction : Instruction
    {
        private readonly Line _line;

        public EmptyInstruction(Line line) : base(new CoreInstruction(), line.LineNumber)
        {
            _line = line;
        }

        public override void Evaluate(Emulation emulation)
        {
            emulation.PrintRaw($"ERROR: \"{_line}\" on line {LineNumber}, no instruction executed");
            emulation.Dump();
        }

        public override string ToString()
        {
            return $"ERROR: \"{_line}\" on line {LineNumber}, no instruction executed";
        }
    }
}
