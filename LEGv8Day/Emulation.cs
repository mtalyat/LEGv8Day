using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private byte Flags;

        public bool NegativeFlag => ((Flags >> 3) & 1) != 0;

        public bool ZeroFlag => ((Flags >> 2) & 1) != 0;

        public bool OverflowFlag => ((Flags >> 1) & 1) != 0;

        public bool CarryFlag => (Flags & 1) != 0;

        #endregion

        private Stopwatch _watch = new Stopwatch();

        public bool IsRunning { get; private set; } = false;

        public bool IsCompleted { get; private set; } = false;

        public float ExecutionTime => _watch.ElapsedMilliseconds;

        private List<string> _output = new List<string>();

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

                    Instruction currentInstruction = _instructions[ExecutionIndex];

                    ExecutionIndex++;

                    currentInstruction.Evaluate(this);
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
            }
        }

        /// <summary>
        /// Resets the simulation entirely. Clears all registers and memory.
        /// </summary>
        public void Reset()
        {
            //ckear data
            Clear();

            _output.Clear();

            //no longer completed
            IsCompleted = false;
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
            //add registers
            DumpAllRegisters();

            //add memory
            DumpAllMemory();
        }

        public void DumpAllRegisters()
        {
            for (int i = 0; i < REGISTER_COUNT; i++)
            {
                //print binary, hex, and normal number form
                DumpRegister(i);
            }
        }

        public void DumpAllMemory()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < MEMORY_SIZE; i++)
            {
                //size is 3, as 255 is the max for a byte, and is 3 chars long
                sb.Append(_memory[i].ToString().PadLeft(3));

                sb.Append(' ');
            }

            _output.Add(sb.ToString());
        }

        public void DumpRegister(int index)
        {
            PackedLong reg = _registers[index];

            _output.Add($"X{index}:\t[0b{Convert.ToString(reg.Long, 2).PadLeft(sizeof(long) * 8, '0')}] " +
                $"[0x{Convert.ToString(reg.Long, 16).PadLeft(sizeof(long) * 2, '0')}] [ {string.Join(' ', reg.ToCharArray())} ] " +
                $"[{reg.Long}/{Reinterpret<long, float>(reg.Long)}/{Reinterpret<long, double>(reg.Long)}]");
        }

        public void DumpMemory(long address)
        {
            byte mem = _memory[address];

            _output.Add($"M{address}:\t[0b{Convert.ToString(mem, 2).PadLeft(sizeof(byte) * 8, '0')}] [ {(char)mem} ] [{mem}]");
        }

        public void DumpMemoryRange(long addressStart, long addressStop)
        {
            //check for invalid arguments
            if(addressStop < addressStart)
            {
                return;
            }

            //adjust start and stop to not go out of range of memory
            addressStart = Math.Clamp(addressStart, 0, MEMORY_SIZE - 1);
            addressStop = Math.Clamp(addressStop, 0, MEMORY_SIZE - 1);

            //dump all memory
            for (int i = 0; i <= addressStop; i++)
            {
                DumpMemory(addressStart + i);
            }
        }

        public void Log(string str)
        {
            _output.Add(str);
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
