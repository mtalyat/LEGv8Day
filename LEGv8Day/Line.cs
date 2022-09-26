using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    internal class Line
    {
        public string Header { get; private set; }

        public string[] Args { get; private set; }

        public Line(string line)
        {
            int spaceIndex = line.IndexOf(' ');

            if(spaceIndex == -1)
            {
                //if no space index, then there are no arguments
                Header = line;
                Args = Array.Empty<string>();
            } else
            {
                //space index, so get the header, the rest are args
                Header = line.Substring(0, spaceIndex);
                Args = line.Substring(spaceIndex + 1, line.Length - 1 - spaceIndex).Split(Constants.ARG_SEPARATOR).Select(a => a.Replace("[", "").Replace("]", "").Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            }
        }

        public Line(string header, params string[] args)
        {
            Header = header;
            Args = args.Select(a => a.Trim()).ToArray();
        }

        public override string ToString()
        {
            return $"{Header} {string.Join(Constants.ARG_SEPARATOR, Args)}";
        }
    }
}
