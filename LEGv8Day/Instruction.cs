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
                    simulation.SetReg(Rd, simulation.GetReg(Rn) + simulation.GetReg(Rm));
                    break;
                case InstructionMnemonic.SUB:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) - simulation.GetReg(Rm));
                    break;
                case InstructionMnemonic.MUL:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) * simulation.GetReg(Rm));
                    break;
                case InstructionMnemonic.UDIV:
                    simulation.SetReg(Rd, simulation.GetReg<ulong>(Rn) / simulation.GetReg<ulong>(Rm));
                    break;
                case InstructionMnemonic.SDIV:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) / simulation.GetReg(Rm));
                    break;
                case InstructionMnemonic.AND:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) & simulation.GetReg(Rm));
                    break;
                case InstructionMnemonic.EOR:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) ^ simulation.GetReg(Rm));
                    break;
                case InstructionMnemonic.ORR:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) | simulation.GetReg(Rm));
                    break;
                case InstructionMnemonic.LSL:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) << Shamt);
                    break;
                case InstructionMnemonic.LSR:
                    simulation.SetReg(Rd, simulation.GetReg(Rn) >> Shamt);
                    break;
                case InstructionMnemonic.BR:
                    simulation.ExecutionIndex = (int)simulation.GetReg(Rd);
                    break;
                case InstructionMnemonic.FADDS:
                    simulation.SetReg(Rd, simulation.GetReg<float>(Rn) + simulation.GetReg<float>(Rm));
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
                    simulation.SetReg(Rd,  simulation.GetReg(Rn) + AluImmediate);
                    break;
                case InstructionMnemonic.SUBI:
                    simulation.SetReg(Rd,  simulation.GetReg(Rn) - AluImmediate);
                    break;
                case InstructionMnemonic.ANDI:
                    simulation.SetReg(Rd,  simulation.GetReg(Rn) & AluImmediate);
                    break;
                case InstructionMnemonic.EORI:
                    simulation.SetReg(Rd,  simulation.GetReg(Rn) ^ AluImmediate);
                    break;
                case InstructionMnemonic.ORRI:
                    simulation.SetReg(Rd,  simulation.GetReg(Rn) | (long)AluImmediate);
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

        public override void Evaluate(Simulation simulation)
        {
            switch(_instruction.Mnemonic)
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
            _data = (opcode << 24) | (condBrAddress << 5) | rt;
        }

        public override void Evaluate(Simulation simulation)
        {
            switch(_instruction.Mnemonic)
            {
                case InstructionMnemonic.CBZ:
                    if(simulation.GetReg(Rt) == 0)
                    {
                        simulation.ExecutionIndex = CondBrAddress;
                    }
                    break;
                case InstructionMnemonic.CBNZ:
                    if(simulation.GetReg(Rt) != 0)
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
