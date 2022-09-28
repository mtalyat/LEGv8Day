using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public abstract class Instruction
    {
        protected PackedInt _data;

        protected CoreInstruction _instruction;

        public int MachineCode => _data.Int;

        public Instruction(CoreInstruction instruction)
        {
            _data = new PackedInt();
            _instruction = instruction;
        }

        public abstract void Evaluate(Simulation simulation);

        public override string ToString()
        {
            //print in binary
            return Convert.ToString(MachineCode, 2).PadLeft(sizeof(int) * 8, '0');
        }

        /// <summary>
        /// Extends the most signigicant bit in the given value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static int ExtendMSB(int value, int sizeInBits)
        {
            unsafe
            {
                //get the bit
                bool bit = (value >> (sizeInBits - 1)) != 0;

                //set to all remaining bits
                PackedInt packed = new PackedInt(value);
                packed.Set(sizeInBits, bit ? ~0 : 0, sizeof(int) - sizeInBits);
                //MessageBox.Show($"Setting at {sizeInBits}, value: {Convert.ToString(bit ? ~0 : 0, 2)}, length: {sizeof(long) - sizeInBits} to existing value {value}");

                return packed;
            }
        }
    }

    public class EmptyInstruction : Instruction
    {
        public EmptyInstruction() : base(new CoreInstruction())
        {

        }

        public override void Evaluate(Simulation simulation)
        {

        }
    }

    public class RInstruction : Instruction
    {
        private int Rm => _data.GetRange(16, 20);

        private int Shamt => _data.GetRange(10, 15);

        private int Rn => _data.GetRange(5, 9);

        private int Rd => _data.GetRange(0, 4);

        public RInstruction(CoreInstruction instruction, int opcode, int rm, int shamt, int rn, int rd) : base(instruction)
        {
            _data = 0;
            _data.SetRange(21, 31, opcode);
            _data.SetRange(16, 20, rm);
            _data.SetRange(10, 15, shamt);
            _data.SetRange(5, 9, rn);
            _data.SetRange(0, 4, rd);
        }

        public override void Evaluate(Simulation simulation)
        {
            long left = simulation.GetReg(Rn);
            long right = simulation.GetReg(Rm);
            long value;

            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.ADD:
                    unchecked { simulation.SetReg(Rd, left + right); }
                    break;
                case InstructionMnemonic.ADDS:
                    unchecked { value = left + right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.SUB:
                    unchecked { simulation.SetReg(Rd, left - right); }
                    break;
                case InstructionMnemonic.SUBS:
                    unchecked { value = left - right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.MUL:
                    unchecked { simulation.SetReg(Rd, left * right); }
                    break;
                case InstructionMnemonic.UDIV:
                    simulation.SetRegR(Rd, simulation.GetRegR<ulong>(Rn) / simulation.GetRegR<ulong>(Rm));
                    break;
                case InstructionMnemonic.SDIV:
                    simulation.SetReg(Rd, left / right);
                    break;
                case InstructionMnemonic.AND:
                    simulation.SetReg(Rd, left & right);
                    break;
                case InstructionMnemonic.ANDS:
                    value = left & right;
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.EOR:
                    simulation.SetReg(Rd, left ^ right);
                    break;
                case InstructionMnemonic.ORR:
                    simulation.SetReg(Rd, left | right);
                    break;
                case InstructionMnemonic.LSL:
                    simulation.SetReg(Rd, left << Shamt);
                    break;
                case InstructionMnemonic.LSR:
                    simulation.SetReg(Rd, left >> Shamt);
                    break;
                case InstructionMnemonic.BR:
                    simulation.ExecutionIndex = (int)simulation.GetReg(Rd);
                    break;
                case InstructionMnemonic.FADDS:
                    simulation.SetRegR(Rd, simulation.GetRegR<float>(Rn) + simulation.GetRegR<float>(Rm));
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }

    public class IInstruction : Instruction
    {
        private int AluImmediate => ExtendMSB(_data.GetRange(10, 21), 11);

        private int Rn => _data.GetRange(5, 9);

        private int Rd => _data.GetRange(0, 4);

        public IInstruction(CoreInstruction instruction, int opcode, int aluImmediate, int rn, int rd) : base(instruction)
        {
            _data = 0;
            _data.SetRange(22, 31, opcode);
            _data.SetRange(10, 21, aluImmediate);
            _data.SetRange(5, 9, rn);
            _data.SetRange(0, 4, rd);
        }

        public override void Evaluate(Simulation simulation)
        {
            long left = simulation.GetReg(Rn);
            long right = AluImmediate;
            long value;

            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.ADDI:
                    unchecked { simulation.SetReg(Rd, left + right); }
                    break;
                case InstructionMnemonic.ADDIS:
                    unchecked { value = left + right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.SUBI:
                    unchecked { simulation.SetReg(Rd, left - right); }
                    break;
                case InstructionMnemonic.SUBIS:
                    unchecked { value = left - right; }
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.ANDI:
                    simulation.SetReg(Rd, left & right);
                    break;
                case InstructionMnemonic.ANDIS:
                    value = left & right;
                    simulation.SetFlags(value, left, right);
                    simulation.SetReg(Rd, value);
                    break;
                case InstructionMnemonic.EORI:
                    simulation.SetReg(Rd, left ^ right);
                    break;
                case InstructionMnemonic.ORRI:
                    simulation.SetReg(Rd, left | right);
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }

    public class DInstruction : Instruction
    {
        private int DtAddress => _data.GetRange(12, 20);

        private int Op => _data.GetRange(10, 11);

        private int Rn => _data.GetRange(5, 9);

        private int Rt => _data.GetRange(0, 4);

        public DInstruction(CoreInstruction instruction, int opcode, int dtAddress, int op, int rn, int rt) : base(instruction)
        {
            _data = 0;
            _data.SetRange(21, 31, opcode);
            _data.SetRange(12, 20, dtAddress);
            _data.SetRange(10, 11, op);
            _data.SetRange(5, 9, rn);
            _data.SetRange(0, 4, rt);
        }

        public override void Evaluate(Simulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.LDUR:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(long)));
                    break;
                case InstructionMnemonic.LDURB:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(byte)));
                    break;
                case InstructionMnemonic.LDURH:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(short)));
                    break;
                case InstructionMnemonic.LDURSW:
                    simulation.SetReg(Rt, simulation.GetMem((int)simulation.GetReg(Rn) + DtAddress, sizeof(int)));
                    break;

                case InstructionMnemonic.STUR:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(long));
                    break;
                case InstructionMnemonic.STURB:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(byte));
                    break;
                case InstructionMnemonic.STURH:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(short));
                    break;
                case InstructionMnemonic.STURW:
                    simulation.SetMem((int)simulation.GetReg(Rn) + DtAddress, simulation.GetReg(Rt), sizeof(int));
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }

    public class BInstruction : Instruction
    {
        private int BrAddress => _data.GetRange(0, 25);

        public BInstruction(CoreInstruction instruction, int opcode, int brAddress) : base(instruction)
        {
            _data = 0;
            _data.SetRange(26, 31, opcode);
            _data.SetRange(0, 25, brAddress);
        }

        public override void Evaluate(Simulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.B:
                    simulation.ExecutionIndex = BrAddress;
                    break;
                case InstructionMnemonic.BL:
                    simulation.SetReg(Simulation.RETURN_ADDRESS_REG, simulation.ExecutionIndex);
                    simulation.ExecutionIndex = BrAddress;
                    break;
                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }

        }
    }

    public class CBInstruction : Instruction
    {
        private int CondBrAddress => _data.GetRange(5, 23);

        private int Rt => _data.GetRange(0, 4);

        public CBInstruction(CoreInstruction instruction, int opcode, int condBrAddress, int rt) : base(instruction)
        {
            _data = 0;
            _data.SetRange(24, 31, opcode);
            _data.SetRange(5, 23, condBrAddress);
            _data.SetRange(0, 4, rt);
        }

        public override void Evaluate(Simulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.CBZ:
                    if (simulation.GetReg(Rt) == 0)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.CBNZ:
                    if (simulation.GetReg(Rt) != 0)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;

                case InstructionMnemonic.B_EQ:
                    if (simulation.ZeroFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_NE:
                    if (!simulation.ZeroFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_LT:
                case InstructionMnemonic.B_LO:
                    if (!simulation.ZeroFlag && simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_LE:
                case InstructionMnemonic.B_LS:
                    if (simulation.ZeroFlag || simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_GT:
                case InstructionMnemonic.B_HI:
                    if (!simulation.ZeroFlag && !simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;
                case InstructionMnemonic.B_GE:
                case InstructionMnemonic.B_HS:
                    if (simulation.ZeroFlag || !simulation.NegativeFlag)
                        simulation.ExecutionIndex = CondBrAddress;
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }

    public class IMInstruction : Instruction
    {
        private int MovImmediate => _data.GetRange(5, 20);

        private int Rd => _data.GetRange(0, 4);

        public IMInstruction(CoreInstruction instruction, int opcode, int movImmediate, int rd) : base(instruction)
        {
            _data = 0;
            _data.SetRange(21, 31, opcode);
            _data.SetRange(5, 20, movImmediate);
            _data.SetRange(0, 4, rd);
        }

        public override void Evaluate(Simulation simulation)
        {
            switch (_instruction.Mnemonic)
            {
                case InstructionMnemonic.MOVK:
                    long r = simulation.GetReg(Rd);
                    simulation.SetReg(Rd, (r & ((long)~0 << 16)) | (long)MovImmediate);
                    break;
                case InstructionMnemonic.MOVZ:
                    simulation.SetReg(Rd, MovImmediate);
                    break;
                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
