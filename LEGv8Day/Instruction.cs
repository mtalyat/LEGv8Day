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
            return $"{_instruction.Format}[{Convert.ToString(MachineCode, 2)}]";
        }

        protected static T Reinterpret<T, U>(U u) where T : unmanaged where U : unmanaged
        {
            T t;

            unsafe
            {
                t = *(T*)&u;
            }

            return t;
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
            _data = (opcode << 21) | (rm << 16) | (shamt << 10) | (rn << 5) | rd;
        }

        public override void Evaluate(Simulation simulation)
        {
            switch(_instruction.Mnemonic)
            {
                case InstructionMnemonic.ADD:
                    simulation.Registers[Rd] = simulation.Registers[Rn] + simulation.Registers[Rm];
                    break;
                case InstructionMnemonic.SUB:
                    simulation.Registers[Rd] = simulation.Registers[Rn] - simulation.Registers[Rm];
                    break;
                case InstructionMnemonic.MUL:
                    simulation.Registers[Rd] = simulation.Registers[Rn] * simulation.Registers[Rm];
                    break;
                case InstructionMnemonic.UDIV:
                    simulation.Registers[Rd] = Reinterpret<long, ulong>(Reinterpret<ulong, long>(simulation.Registers[Rn]) / Reinterpret<ulong, long>(simulation.Registers[Rm]));
                    break;
                case InstructionMnemonic.SDIV:
                    simulation.Registers[Rd] = simulation.Registers[Rn] / simulation.Registers[Rm];
                    break;
                case InstructionMnemonic.AND:
                    simulation.Registers[Rd] = simulation.Registers[Rn] & simulation.Registers[Rm];
                    break;
                case InstructionMnemonic.EOR:
                    simulation.Registers[Rd] = simulation.Registers[Rn] ^ simulation.Registers[Rm];
                    break;
                case InstructionMnemonic.ORR:
                    simulation.Registers[Rd] = simulation.Registers[Rn] | simulation.Registers[Rm];
                    break;
                case InstructionMnemonic.LSL:
                    simulation.Registers[Rd] = simulation.Registers[Rn] << Shamt;
                    break;
                case InstructionMnemonic.LSR:
                    simulation.Registers[Rd] = simulation.Registers[Rn] >> Shamt;
                    break;
                case InstructionMnemonic.BR:
                    simulation.ExecutionIndex = (int)simulation.Registers[Rd];
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }

    public class IInstruction : Instruction
    {
        private int AluImmediate => _data.GetRange(10, 21);

        private int Rn => _data.GetRange(5, 9);

        private int Rd => _data.GetRange(0, 4);

        public IInstruction(CoreInstruction instruction, int opcode, int aluImmediate, int rn, int rd) : base(instruction)
        {
            _data = (opcode << 22) | (aluImmediate << 10) | (rn << 5) | rd;
        }

        public override void Evaluate(Simulation simulation)
        {
            switch(_instruction.Mnemonic)
            {
                case InstructionMnemonic.ADDI:
                    simulation.Registers[Rd] = simulation.Registers[Rn] + AluImmediate;
                    break;
                case InstructionMnemonic.SUBI:
                    simulation.Registers[Rd] = simulation.Registers[Rn] - AluImmediate;
                    break;
                case InstructionMnemonic.ANDI:
                    simulation.Registers[Rd] = simulation.Registers[Rn] & AluImmediate;
                    break;
                case InstructionMnemonic.EORI:
                    simulation.Registers[Rd] = simulation.Registers[Rn] ^ AluImmediate;
                    break;
                case InstructionMnemonic.ORRI:
                    simulation.Registers[Rd] = simulation.Registers[Rn] | (long)AluImmediate;
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
            _data = (opcode << 21) | (dtAddress << 12) | (op << 10) | (rn << 5) | rt;
        }

        public override void Evaluate(Simulation sim)
        {
            switch(_instruction.Mnemonic)
            {
                case InstructionMnemonic.LDUR:
                    sim.Registers[Rt] = sim.GetMemory((int)sim.Registers[Rn] + DtAddress, sizeof(long));
                    break;
                case InstructionMnemonic.LDURB:
                    sim.Registers[Rt] = sim.GetMemory((int)sim.Registers[Rn] + DtAddress, sizeof(byte));
                    break;
                case InstructionMnemonic.LDURH:
                    sim.Registers[Rt] = sim.GetMemory((int)sim.Registers[Rn] + DtAddress, sizeof(short));
                    break;
                case InstructionMnemonic.LDURSW:
                    sim.Registers[Rt] = sim.GetMemory((int)sim.Registers[Rn] + DtAddress, sizeof(int));
                    break;

                case InstructionMnemonic.STUR:
                    sim.SetMemory((int)sim.Registers[Rn] + DtAddress, sim.Registers[Rt], sizeof(long));
                    break;
                case InstructionMnemonic.STURB:
                    sim.SetMemory((int)sim.Registers[Rn] + DtAddress, sim.Registers[Rt], sizeof(byte));
                    break;
                case InstructionMnemonic.STURH:
                    sim.SetMemory((int)sim.Registers[Rn] + DtAddress, sim.Registers[Rt], sizeof(short));
                    break;
                case InstructionMnemonic.STURW:
                    sim.SetMemory((int)sim.Registers[Rn] + DtAddress, sim.Registers[Rt], sizeof(int));
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
            _data = (opcode << 26) | brAddress;
        }

        public override void Evaluate(Simulation simulation)
        {
            switch(_instruction.Mnemonic)
            {
                case InstructionMnemonic.B:
                    simulation.ExecutionIndex = BrAddress;
                    break;
                case InstructionMnemonic.BL:
                    simulation.Registers[Simulation.ReturnAddressRegister] = simulation.ExecutionIndex;
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
            _data = (opcode << 24) | (condBrAddress << 5) | rt;
        }

        public override void Evaluate(Simulation simulation)
        {
            switch(_instruction.Mnemonic)
            {
                case InstructionMnemonic.CBZ:
                    if(simulation.Registers[Rt] == 0)
                    {
                        simulation.ExecutionIndex = CondBrAddress;
                    }
                    break;
                case InstructionMnemonic.CBNZ:
                    if(simulation.Registers[Rt] != 0)
                    {
                        simulation.ExecutionIndex = CondBrAddress;
                    }
                    break;

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }

    public class IMInstruction : Instruction
    {
        public IMInstruction(CoreInstruction instruction, int opcode, int movImmediate, int rd) : base(instruction)
        {
            _data = (opcode << 21) | (movImmediate << 5) | rd;
        }

        public override void Evaluate(Simulation simulation)
        {
            switch(_instruction.Mnemonic)
            {

                default:
                    throw new NotImplementedException($"The instruction {_instruction.Mnemonic} has not been implemented yet.");
            }
        }
    }
}
