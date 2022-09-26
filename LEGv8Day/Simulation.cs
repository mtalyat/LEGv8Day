using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    /// <summary>
    /// Responsibly for running the simulation of the LEGV8 code.
    /// </summary>
    public class Simulation
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

        /// <summary>
        /// Creates a new Simulation that will run using the given instructions.
        /// </summary>
        /// <param name="instructions"></param>
        public Simulation(Instruction[] instructions)
        {
            _registers = new long[REGISTER_COUNT];
            _memory = new byte[MEMORY_SIZE];
            _instructions = instructions;
        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        public void Run()
        {
            Reset();

            //go through and run each instruction
            ExecutionIndex = 0;

            Instruction currentInstruction;

            while (ExecutionIndex < _instructions.Length)
            {
                currentInstruction = _instructions[ExecutionIndex];

                ExecutionIndex++;

                currentInstruction.Evaluate(this);
            }
        }

        /// <summary>
        /// Resets the simulation entirely. Clears all registers and memory.
        /// </summary>
        public void Reset()
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
        public string[] Dump()
        {
            List<string> results = new List<string>();

            results.Add("Registers:");

            //add registers

            PackedLong r;

            for (int i = 0; i < REGISTER_COUNT; i++)
            {
                r = _registers[i];

                //print binary, hex, and normal number form
                results.Add($"X{i}:\t[0b{Convert.ToString(r.Long, 2).PadLeft(sizeof(long) * 8, '0')}] [0x{Convert.ToString(r.Long, 16).PadLeft(sizeof(long), '0')}] [ {string.Join(' ', r.ToCharArray())} ] [{r.Long}]");
            }

            //add memory

            results.Add("Memory:");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < MEMORY_SIZE; i++)
            {
                //size is 3, as 255 is the max for a byte, and is 3 chars long
                sb.Append(_memory[i].ToString().PadLeft(3));

                sb.Append(' ');
            }

            results.Add(sb.ToString());

            //add instructions

            results.Add("Instructions:");

            results.AddRange(_instructions.Select(i => i.ToString()));

            return results.ToArray();
        }

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
                u = *(U*)&t;
            }

            return u;
        }

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
        public T GetReg<T>(int index) where T : unmanaged => Reinterpret<long, T>(GetReg(index));

        /// <summary>
        /// Sets the value for the register with the given index to the given value.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetReg(int index, long value)
        {
            _registers[index] = value;
        }

        /// <summary>
        /// Reinterprets the given value to a long, and then sets the value for the register with the given index to the given value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetReg<T>(int index, T value) where T : unmanaged => SetReg(index, Reinterpret<T, long>(value));

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
        public void SetMem<T>(int index, T value) where T : unmanaged
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
        public T GetMem<T>(int index) where T : unmanaged
        {
            int size;

            unsafe
            {
                size = sizeof(T);
            }

            return Reinterpret<long, T>(GetMem(index, size));
        }

        /// <summary>
        /// Gets all register values.
        /// </summary>
        /// <returns></returns>
        public long[] GetRegisters()
        {
            return _registers;
        }

        /// <summary>
        /// Gets all memory values.
        /// </summary>
        /// <returns></returns>
        public byte[] GetMemory()
        {
            return _memory;
        }
        
        /// <summary>
        /// Gets all instructions.
        /// </summary>
        /// <returns></returns>
        public Instruction[] GetInstructions()
        {
            return _instructions;
        }

        public void SetFlags(long result)
        {
            SetFlags(result < 0, result == 0, false, false);
        }

        public void SetFlags(bool negative, bool zero, bool overflow, bool carry)
        {
            Flags = (byte)((negative ? 1 : 0) << 3 | (zero ? 1 : 0) << 2 | (overflow ? 1 : 0) << 1 | (carry ? 1 : 0));
        }
    }
}
