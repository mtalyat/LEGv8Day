using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    internal static class Parse
    {
        #region Consts

        public const char ESCAPE_CHAR = '\\';

        public const char FORMAT_REG_OPEN = '{';
        public const char FORMAT_REG_CLOSE = '}';

        public const char FORMAT_MEM_OPEN = '[';
        public const char FORMAT_MEM_CLOSE = ']';

        public const char NUMBER_PREFIX = '#';
        public const char REGISTER_PREFIX = 'X';

        #endregion

        public static Dictionary<string, CoreInstruction> CoreInstructions { get; private set; } = new Dictionary<string, CoreInstruction>();

        private static InstructionMnemonic ParseMnemonic(string m)
        {
            return Enum.TryParse(m.Replace('.', '_'), out InstructionMnemonic mnemonic) ? mnemonic : InstructionMnemonic.Empty;
        }

        public static void LoadCoreInstructions()
        {
            CoreInstructions.Clear();

            string[] lines = ProgramData.CoreInstructions.Split(Environment.NewLine);

            string line;

            string[] args;
            string[] ops;

            string mnemonic;

            CoreInstruction c;

            //skip the first line, which is just the headers
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i];

                //ignore blank lines
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                //split up by arguments
                args = line.Split(',');

                mnemonic = args[1];

                if (args.Length == 4)//name, mnemonic, format, opcode
                {
                    ops = args[3].Split('-');

                    c = new CoreInstruction(args[0], ParseMnemonic(mnemonic), Enum.Parse<InstructionFormat>(args[2]), int.Parse(ops[0], System.Globalization.NumberStyles.HexNumber), int.Parse(ops.Length == 1 ? ops[0] : ops[1], System.Globalization.NumberStyles.HexNumber));
                }
                else if (args.Length == 5)//name, mnemonic, format, opcode, shamt
                {
                    ops = args[3].Split('-');

                    c = new CoreInstruction(args[0], ParseMnemonic(mnemonic), Enum.Parse<InstructionFormat>(args[2]), int.Parse(ops[0], System.Globalization.NumberStyles.HexNumber), int.Parse(ops.Length == 1 ? ops[0] : ops[1], System.Globalization.NumberStyles.HexNumber), int.Parse(args[4], System.Globalization.NumberStyles.HexNumber));
                }
                else
                {
                    //ignore this line, it is bad
                    throw new ArgumentException($"The line \"{line}\"({i + 1}) has an invalid number of arguments ({args.Length})!");
                }

                //add core instruction to the dictionary
                CoreInstructions.Add(mnemonic, c);
            }

            //now that they have been loaded, send that to the formatter for later use
            RtfLEGv8Formatter.SetKeywords(CoreInstructions.Keys.ToArray());
        }

        public static Instruction ParseInstruction(Line line, Dictionary<string, int> labels)
        {
            int[] args = line.GetArgs().Select(a => ParseArgument(a, labels)).ToArray();

            int arg0 = args.Length > 0 ? args[0] : 0;
            int arg1 = args.Length > 1 ? args[1] : arg0;
            int arg2 = args.Length > 2 ? args[2] : (args.Length > 1 ? arg1 : arg0);

            //find the core instruction
            if (Parse.CoreInstructions.TryGetValue(line.Label.ToUpper(), out CoreInstruction? ci))
            {
                switch (ci.Format)
                {
                    case InstructionFormat.R:
                        return new RInstruction(ci, line.LineNumber, ci.OpCodeStart, arg2, arg2, arg1, arg0);
                    case InstructionFormat.I:
                        return new IInstruction(ci, line.LineNumber, ci.OpCodeStart, arg2, arg1, arg0);
                    case InstructionFormat.D:
                        return new DInstruction(ci, line.LineNumber, ci.OpCodeStart, arg2, 0, arg1, arg0);
                    case InstructionFormat.B:
                        return new BInstruction(ci, line.LineNumber, ci.OpCodeStart, arg0);
                    case InstructionFormat.CB:
                        return new CBInstruction(ci, line.LineNumber, ci.OpCodeStart, arg1, arg0);
                    case InstructionFormat.IM:
                        return new IMInstruction(ci, line.LineNumber, ci.OpCodeStart, arg1, arg0);
                    case InstructionFormat.Z:
                        return new ZInstruction(ci, line.LineNumber, line.RawArgs);
                }
            }

            return new EmptyInstruction(line);
        }

        public static int ParseRegister(string arg)
        {
            string upperArg = arg.ToUpper();

            if (upperArg[0] == REGISTER_PREFIX)
            {
                if (upperArg == "XZR")
                {
                    return 31;//zero register
                }
                else
                {
                    //another register, parse the number next to the X
                    return int.TryParse(arg.AsSpan(1), out int i) ? i : -1;
                }
            } else
            {
                //check special cases for registers
                switch (upperArg)
                {
                    case "IP0": return 16;
                    case "IP1": return 17;
                    case "SP": return 28;//stack pointer
                    case "FP": return 29;//frame pointer
                    case "LR": return 30;//return address
                }
            }

            //not a register
            return -1;
        }

        public static int ParseNumber(string arg)
        {
            if(arg.StartsWith(NUMBER_PREFIX))
            {
                arg = arg.Substring(1);
            }

            return int.TryParse(arg, out int i) ? i : -1;
        }

        public static int ParseArgument(string arg, Dictionary<string, int>? labels = null)
        {
            string upperArg = arg.ToUpper();

            //determine what to do based on the starting char
            switch (upperArg[0])
            {
                case REGISTER_PREFIX://register
                    {
                        if (upperArg == "XZR")
                        {
                            return 31;//zero register
                        }
                        else
                        {
                            //another register, parse the number next to the X
                            return int.TryParse(arg.Substring(1), out int i) ? i : -1;
                        }
                    }
                case NUMBER_PREFIX://number
                    return int.Parse(arg.Substring(1));
                default:
                    //check special cases for registers
                    switch (upperArg)
                    {
                        case "IP0": return 16;
                        case "IP1": return 17;
                        case "SP": return 28;//stack pointer
                        case "FP": return 29;//frame pointer
                        case "LR": return 30;//return address
                    }

                    int value;
                    if (int.TryParse(arg, out value))//check if just a raw number
                    {
                        //return that number
                        return value;
                    }
                    else if (labels?.TryGetValue(arg, out value) ?? false)//check if a label name
                    {
                        //use the line the header corresponds to
                        return value;
                    }

                    //unrecognized argument
                    return 0;
            }
        }
    }
}
