using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public class Simulation
    {
        public const int REGISTER_COUNT = 32;
        public const int MEMORY_SIZE = 4096;

        public const int StackPointerRegister = REGISTER_COUNT - 4;
        public const int FramePointerRegister = REGISTER_COUNT - 3;
        public const int ReturnAddressRegister = REGISTER_COUNT - 2;
        public const int ConstantValueZeroRegister = REGISTER_COUNT - 1;


        private readonly long[] _registers;
        public long[] Registers => _registers;

        private readonly byte[] _memory;
        public byte[] Memory => _memory;

        private readonly Instruction[] _instructions;
        public Instruction[] Instructions => _instructions;

        public int ExecutionIndex { get; set; } = 0;

        public Simulation(Instruction[] instructions)
        {
            _registers = new long[REGISTER_COUNT];
            _memory = new byte[MEMORY_SIZE];
            _instructions = instructions;
        }

        public void Run()
        {
            Reset();

            //go through and run each instruction
            ExecutionIndex = 0;

            Instruction currentInstruction;

            while(ExecutionIndex < _instructions.Length)
            {
                currentInstruction = _instructions[ExecutionIndex];

                ExecutionIndex++;

                currentInstruction.Evaluate(this);
            }
        }

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
                results.Add($"X{i}:\t[0b{Convert.ToString(r.Long, 2).PadLeft(sizeof(long) * 8, '0')}]\t[0x{Convert.ToString(r.Long, 16).PadLeft(sizeof(long), '0')}]\t[{r.Long}]\t[ {string.Join(' ', r.ToCharArray())} ]");
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

        public void SetMemory(int index, long data, int size)
        {
            PackedLong p = new PackedLong(data);

            for (int i = 0; i < size && i + index < MEMORY_SIZE; i++)
            {
                _memory[index + i] = p[i];
            }
        }

        public long GetMemory(int index, int size)
        {
            PackedLong p = new PackedLong();

            for (int i = 0; i < size && i + index < MEMORY_SIZE; i++)
            {
                p.SetByte(i, _memory[index + i]);
            }

            return p.Long;
        }
    }
}
