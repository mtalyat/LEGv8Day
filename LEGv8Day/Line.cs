using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    internal class Line
    {
        public string Label { get; private set; }

        public string RawArgs { get; private set; }

        public Line(string line)
        {
            int spaceIndex = line.IndexOf(' ');

            if(spaceIndex == -1)
            {
                //if no space index, then there are no arguments
                Label = line;
                RawArgs = string.Empty;
            } else
            {
                //space index, so get the header, the rest are args
                Label = line.Substring(0, spaceIndex);
                RawArgs = line.Substring(spaceIndex + 1, line.Length - 1 - spaceIndex);
            }
        }

        public Line(string label, string args)
        {
            Label = label;
            RawArgs = args;
        }

        public string[] GetArgs()
        {
            return RawArgs
                .Split(Constants.ARG_SEPARATOR)
                .Select(a => a.Replace("[", "")
                .Replace("]", "").Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();
        }

        public bool IsInstruction()
        {
            //this line is an instruction if it has arguments, or if it is a mnemonic
            return RawArgs.Length > 0 || Enum.TryParse<InstructionMnemonic>(Label, out _);
        }

        public override string ToString()
        {
            return $"{Label} {string.Join(Constants.ARG_SEPARATOR, RawArgs)}";
        }
    }
}
