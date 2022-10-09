using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    /// <summary>
    /// Responsibly for running the simulation of the LEGV8 code.
    /// </summary>
    public class Emulation
    {
        #region Configuration

        /// <summary>
        /// The number of registers within this simulation.
        /// </summary>
        public const int REGISTER_COUNT = 32;

        /// <summary>
        /// The size of memory, in bytes, within this simulation.
        /// </summary>
        public const int MEMORY_SIZE = 4096;

        /// <summary>
        /// The maximum number of lines the stack trace will remember.
        /// </summary>
        private const int STACK_TRACE_MAX_LENGTH = 256;

        #endregion

        #region Registers

        /// <summary>
        /// The index of the stack pointer register.
        /// </summary>
        public const int STACK_POINTER_REG = REGISTER_COUNT - 4;

        /// <summary>
        /// The index of the frame pointer register.
        /// </summary>
        public const int FRAME_POINTER_REG = REGISTER_COUNT - 3;

        /// <summary>
        /// The index of the return address register.
        /// </summary>
        public const int RETURN_ADDRESS_REG = REGISTER_COUNT - 2;

        /// <summary>
        /// The index of the constant zero register.
        /// </summary>
        public const int ZERO_REG = REGISTER_COUNT - 1;

        #endregion

        #region Data

        /// <summary>
        /// The registers within this simulation.
        /// </summary>
        private readonly long[] _registers;

        /// <summary>
        /// The memory within this simulation.
        /// </summary>
        private readonly byte[] _memory;

        /// <summary>
        /// The instructions that are used to run this simulation.
        /// </summary>
        private readonly Instruction[] _instructions;

        /// <summary>
        /// The current index of the instruction that is being executed.
        /// </summary>
        public int ExecutionIndex { get; set; } = 0;

        /// <summary>
        /// The line number of the last Instruction to be executed.
        /// </summary>
        public int ExecutionLine => _executionInstruction?.LineNumber ?? -1;

        /// <summary>
        /// The last instruction to be executed.
        /// </summary>
        private Instruction? _executionInstruction = null;

        private byte Flags;

        public bool NegativeFlag => ((Flags >> 3) & 1) != 0;

        public bool ZeroFlag => ((Flags >> 2) & 1) != 0;

        public bool OverflowFlag => ((Flags >> 1) & 1) != 0;

        public bool CarryFlag => (Flags & 1) != 0;

        #endregion

        private Stopwatch _watch = new Stopwatch();

        public float ExecutionTime => _watch.ElapsedMilliseconds;

        public bool IsRunning { get; private set; } = false;

        public bool IsCompleted { get; private set; } = false;

        public bool IsDumped { get; private set; } = false;

        public bool IsCanceled { get; private set; } = false;

        private List<string> _output = new List<string>();

        private Queue<int> _stackTrace = new Queue<int>();
        private int _maxExecutionIndex = -1;

        /// <summary>
        /// Creates a new Simulation that will run using the given instructions.
        /// </summary>
        /// <param name="instructions"></param>
        public Emulation(Instruction[] instructions)
        {
            _registers = new long[REGISTER_COUNT];
            _memory = new byte[MEMORY_SIZE];
            _instructions = instructions;
        }

        #region Running

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        public void Run()
        {
            Reset();

            //go through and run each instruction
            ExecutionIndex = 0;

            Instruction currentInstruction;

            _watch.Start();

            IsRunning = true;

            while (ExecutionIndex < _instructions.Length)
            {
                currentInstruction = _instructions[ExecutionIndex];

                ExecutionIndex++;

                currentInstruction.Evaluate(this);
            }

            IsRunning = false;

            _watch.Stop();
        }

        public void Start()
        {
            if(!IsRunning)
            {
                Reset();

                IsRunning = true;

                ExecutionIndex = 0;
                _executionInstruction = null;

                _watch.Restart();
            }
        }

        public void Step()
        {
            if(IsRunning)
            {
                if (ExecutionIndex < _instructions.Length)
                {
                    //perform step

                    _executionInstruction = _instructions[ExecutionIndex];

                    _stackTrace.Enqueue(ExecutionIndex);
                    if(_stackTrace.Count > STACK_TRACE_MAX_LENGTH)
                    {
                        _stackTrace.Dequeue();
                    }

                    _maxExecutionIndex = Math.Max(_maxExecutionIndex, ExecutionIndex);

                    ExecutionIndex++;

                    _executionInstruction.Evaluate(this);
                }
                else
                {
                    //done here
                    IsCompleted = true;
                    Stop();
                }
            }
        }

        public void Stop()
        {
            if(IsRunning)
            {
                IsRunning = false;

                _watch.Stop();

                ExecutionIndex = -1;
                _executionInstruction = null;
            }
        }

        public void Cancel()
        {
            //stop
            Stop();

            IsCanceled = true;
        }

        /// <summary>
        /// Resets the simulation entirely. Clears all registers and memory.
        /// </summary>
        public void Reset()
        {
            //ckear data
            Clear();

            _output.Clear();
            _stackTrace.Clear();
            _maxExecutionIndex = -1;

            //no longer completed or dumped
            IsCompleted = false;
            IsDumped = false;
            IsCanceled = false;

            //get rid of lines
            ExecutionIndex = 0;
            _executionInstruction = null;
        }

        public string[] GetOutput()
        {
            return _output.ToArray();
        }

        #endregion

        #region Debugging

        public void Clear()
        {
            //clear registers
            for (int i = 0; i < REGISTER_COUNT; i++)
            {
                _registers[i] = 0;
            }

            //clear memory
            for (int i = 0; i < MEMORY_SIZE; i++)
            {
                _memory[i] = 0;
            }
        }

        /// <summary>
        /// Provides a string array including all data from registers, memory, and instructions.
        /// </summary>
        /// <returns></returns>
        public void Dump()
        {
            //print dump
            if(_executionInstruction != null)
            {
                Print($"DUMP: {_executionInstruction}");
            } else
            {
                Print("DUMP:");
            }

            //add registers
            DumpAllRegisters();

            //add memory
            DumpAllMemory();

            //stop program
            Stop();

            IsDumped = true;
        }

        private void DumpAllRegisters()
        {
            for (int i = 0; i < REGISTER_COUNT; i++)
            {
                //print binary, hex, and normal number form
                PrintRegister(i);
            }
        }

        private void DumpAllMemory()
        {
            StringBuilder sb = new StringBuilder();

            byte b;

            for (int i = 0; i < MEMORY_SIZE; i++)
            {
                b = _memory[i];
                //size is 3, as 255 is the max for a byte, and is 3 chars long
                sb.Append(b > 0 ? b.ToString().PadLeft(3, '.'): "...");

                sb.Append(' ');
            }

            _output.Add(sb.ToString());
        }

        public void PrintRegister(int index)
        {
            PackedLong reg = _registers[index];

            _output.Add($"X{index}:\t[0b{Convert.ToString(reg.Long, 2).PadLeft(sizeof(long) * 8, '0')}] " +
                $"[0x{Convert.ToString(reg.Long, 16).PadLeft(sizeof(long) * 2, '0')}] [ {string.Join(' ', reg.ToCharArray())} ] " +
                $"[{reg.Long}]");
        }

        public void PrintMemory(long address)
        {
            byte mem = _memory[address];

            _output.Add($"M{address}:\t[0b{Convert.ToString(mem, 2).PadLeft(sizeof(byte) * 8, '0')}] [ {(char)mem} ] [{mem}]");
        }

        public void Print(string str)
        {
            //check for empty
            if(string.IsNullOrWhiteSpace(str))
            {
                _output.Add(string.Empty);
            }

            //format string
            _output.Add(FormatString(str));
        }

        private int FindInFormatString(string str, int i, char c)
        {
            char d;

            for (int j = i + 1; j < str.Length; j++)
            {
                d = str[j];

                if (d == Parse.ESCAPE_CHAR)
                {
                    //skip this and next char
                    j++;
                    continue;
                }

                //if a closing...
                if (d == c)
                {
                    return j;
                }
            }

            //not found
            return str.Length;
        }

        private string FormatString(string str)
        {
            if(string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            //if it is just one word, and a register, just print that
            int index = Parse.ParseRegister(str);

            if (index >= 0)
            {
                return GetReg(index).ToString();
            }

            int i, j;
            char c, d;
            string inside;

            StringBuilder sb = new StringBuilder();

            //not just a register, so format it
            for (i = 0; i < str.Length; i++)
            {
                c = str[i];

                if(c == Parse.ESCAPE_CHAR)
                {
                    //skip this and next char
                    i++;
                    continue;
                }

                //if an opening...
                if(c == Parse.FORMAT_REG_OPEN || c == Parse.FORMAT_MEM_OPEN)
                {
                    //find closing
                    j = FindInFormatString(str, i, c == Parse.FORMAT_REG_OPEN ? Parse.FORMAT_REG_CLOSE : Parse.FORMAT_MEM_CLOSE);

                    //check if not found or
                    //if the next one, ignore, there is no inside
                    if (j >= str.Length || j == i + 1)
                    {
                        sb.Append(c);
                        continue;
                    }

                    //get insides
                    inside = str.Substring(i + 1, j - i - 1);

                    //parse
                    index = Parse.ParseRegister(inside);

                    //if valid...
                    if (index >= 0)
                    {
                        //insert value
                        if (c == Parse.FORMAT_REG_OPEN)
                        {
                            //register value
                            sb.Append(GetReg(index));
                        }
                        else
                        {
                            //memory value at the given register
                            sb.Append(GetMem((int)GetReg(index)));
                        }
                    }
                    else
                    {
                        //if not valid, just insert the whole thing
                        sb.Append(c);
                        sb.Append(inside);
                        sb.Append(str[j]);
                    }

                    //advance i to the closing
                    i = j;
                } else
                {
                    //not an opening, so just add
                    sb.Append(c);
                }
            }

            //return new string
            return sb.ToString();

            ////split into words
            //string[] words = str.Split(' ');

            ////string builder to go back
            //StringBuilder sb = new StringBuilder();

            ////check words
            //foreach(string w in words)
            //{
            //    //if starts with { and ends with }...
            //    if(w.StartsWith(Parse.FORMAT_REG_OPEN) && w.EndsWith(Parse.FORMAT_REG_CLOSE))
            //    {
            //        //format
            //        string inside = w.Substring(1, w.Length - 2);

            //        //get register index
            //        int index = Parse.ParseRegister(inside);

            //        //get value
            //        if(index >= 0)
            //        {
            //            sb.Append(GetReg(index));
            //            sb.Append(' ');

            //            continue;
            //        }
            //    } else if (w.StartsWith(Parse.FORMAT_MEM_OPEN) && w.EndsWith(Parse.FORMAT_MEM_CLOSE))
            //    {
            //        //format
            //        string inside = w.Substring(1, w.Length - 2);

            //        //get register index
            //        int value = Parse.ParseRegister(inside);

            //        if(value < 0)
            //        {
            //            //not a register, get a number
            //            value = Parse.ParseNumber(inside);
            //        } else
            //        {
            //            //register, get value from register
            //            value = (int)GetReg(value);
            //        }

            //        //put value from memory
            //        if(value >= 0)
            //        {
            //            sb.Append(GetMem(value));
            //            sb.Append(' ');

            //            continue;
            //        }
            //    }

            //    //ignore otherwise
            //    sb.Append(w);
            //    sb.Append(' ');
            //}

            ////remove string at end
            //return sb.ToString().TrimEnd();
        }

        public string[] GetStackTrace()
        {
            int spacing = _maxExecutionIndex.ToString().Length;
            List<string> lines = _stackTrace.Select(i => _instructions[i].ToStackTraceString(spacing)).ToList();

            //add reason for completion
            if(IsCompleted)
            {
                lines.Add("Program completed.");
            }
            else if (IsCanceled)
            {
                lines.Add("Program canceled.");
            }
            else if (IsDumped)
            {
                lines.Add("Program dumped.");
            }

            //reverse
            lines.Reverse();

            return lines.ToArray();
        }

        #endregion

        #region Getting and Setting

        /// <summary>
        /// Reinterprets the given T t as a U.
        /// Equivalent to the C++ reinterpret_cast.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="U">The destination type.</typeparam>
        /// <param name="t">The original value.</param>
        /// <returns>A U, which has the same exact bits as the given T t, but is a type of U.</returns>
        private static U Reinterpret<T, U>(T t) where T : unmanaged where U : unmanaged
        {
            U u;

            unsafe
            {
                //turn into a pointer, cast to a new pointer of type U
                u = *(U*)&t;
            }

            return u;
        }

        #region Registers

        /// <summary>
        /// Gets the value from the register with the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public long GetReg(int index)
        {
            return _registers[index];
        }

        /// <summary>
        /// Gets the value from the register with the given index, and reinterprets it to the given type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetRegR<T>(int index) where T : unmanaged => Reinterpret<long, T>(GetReg(index));

        /// <summary>
        /// Sets the value for the register with the given index to the given value.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetReg(int index, long value)
        {
            //only set if not the zero register
            if (index == ZERO_REG)
            {
                return;
            }

            _registers[index] = value;
        }

        /// <summary>
        /// Reinterprets the given value to a long, and then sets the value for the register with the given index to the given value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetRegR<T>(int index, T value) where T : unmanaged => SetReg(index, Reinterpret<T, long>(value));

        /// <summary>
        /// Gets all register values.
        /// </summary>
        /// <returns></returns>
        public long[] GetRegisters()
        {
            return _registers;
        }

        #endregion

        #region Memory

        /// <summary>
        /// Sets the memory at the given index to the given value.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        public void SetMem(int index, long value, int size)
        {
            PackedLong p = new PackedLong(value);

            for (int i = 0; i < size && i + index < MEMORY_SIZE; i++)
            {
                _memory[index + i] = p[i];
            }
        }

        /// <summary>
        /// Reinterprets the given value to a long, then sets the value within the memory. Uses the size in bytes of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetMemR<T>(int index, T value) where T : unmanaged
        {
            int size;

            unsafe
            {
                size = sizeof(T);
            }

            SetMem(index, Reinterpret<T, long>(value), size);
        }

        public long GetMem(int index) => GetMem(index, 1);

        /// <summary>
        /// Gets the memory from the given position and given size as a long.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public long GetMem(int index, int size)
        {
            PackedLong p = new PackedLong();

            for (int i = 0; i < size && i + index < MEMORY_SIZE; i++)
            {
                p.SetByte(i, _memory[index + i]);
            }

            return p.Long;
        }

        /// <summary>
        /// Gets the memory from the given position and reinterprets it as a T. Uses the size in bytes of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetMemR<T>(int index) where T : unmanaged
        {
            int size;

            unsafe
            {
                size = sizeof(T);
            }

            return Reinterpret<long, T>(GetMem(index, size));
        }

        /// <summary>
        /// Gets all memory values.
        /// </summary>
        /// <returns></returns>
        public byte[] GetMemory()
        {
            return _memory;
        }

        #endregion

        /// <summary>
        /// Gets all instructions.
        /// </summary>
        /// <returns></returns>
        public Instruction[] GetInstructions()
        {
            return _instructions;
        }

        #region Flags

        public void SetFlags(long value, long left, long right)
        {
            SetFlags(
                        value < 0,
                        value == 0,
                        (left > 0 && right > 0 && value < 0) || (left < 0 && right < 0 && value > 0),
                        (left > 0 && right > 0 && value < left && value < right) || (left < 0 && right < 0 && value > left && value > right));
        }

        public void SetFlags(bool negative, bool zero, bool overflow, bool carry)
        {
            Flags = (byte)((negative ? 1 : 0) << 3 | (zero ? 1 : 0) << 2 | (overflow ? 1 : 0) << 1 | (carry ? 1 : 0));

            //MessageBox.Show($"Flags: [{Convert.ToString(Flags, 2).PadLeft(4, '0')}]");
        }

        #endregion

        #endregion
    }
}
