using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LEGv8Day
{
    internal class LegFile
    {
        public const string EXTENSION = ".legv8asm";

        public string FileName { get; set; }

        public string Name => string.IsNullOrEmpty(FileName) ? "Unnamed Leg File" : Path.GetFileNameWithoutExtension(FileName);

        public string Text { get; set; }

        public LegFile()
        {
            FileName = "";
            Text = string.Empty;
        }

        public LegFile(string path)
        {
            if(string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                //no path for a file, so make a blank leg file
                FileName = string.Empty;
                Text = string.Empty;
            } else
            {
                FileName = path;
                Text = File.ReadAllText(path);

                //debug
                //MessageBox.Show(string.Join(" ", Text.ToCharArray().Select(c => $"{c}{(byte)c} ")));
            }
        }

        public void Save()
        {
            //do nothing if there is no file path
            if(string.IsNullOrEmpty(FileName))
            {
                throw new Exception("No FileName has been assigned!");
            }

            File.WriteAllText(FileName, Text);
        }
    }
}
