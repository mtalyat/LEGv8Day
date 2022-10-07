using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public enum InstructionMnemonic : int
    {
        //      CORE INSTRUCTIONS

        /// <summary>
        /// Does nothing. Not a real instruction, used for internal simulation error handling.
        /// </summary>
        Empty,
        ADD,
        ADDI,
        ADDIS,
        ADDS,
        AND,
        ANDI,
        ANDIS,
        ANDS,
        B,
        B_EQ,
        B_NE,
        B_LT,
        B_LE,
        B_GT,
        B_GE,
        B_HI,
        B_HS,
        B_LO,
        B_LS,
        B_MI,
        B_PL,
        B_VS,
        B_VC,
        BL,
        BR,
        CBNZ,
        CBZ,
        EOR,
        EORI,
        LDUR,
        LDURB,
        LDURH,
        LDURSW,
        LDXR,
        LSL,
        LSR,
        MOVK,
        MOVZ,
        ORR,
        ORRI,
        STUR,
        STURB,
        STURH,
        STURW,
        STXR,
        SUB,
        SUBI,
        SUBIS,
        SUBS,
        FADDS,
        FADDD,
        FCMPS,
        FCMPD,
        FDIVS,
        FDIVD,
        FMULS,
        FMULD,
        FSUBS,
        FSUBD,
        LDURS,
        LDURD,
        MUL,
        SDIV,
        SMULH,
        STURS,
        STURD,
        UDIV,
        UMULH,

        //      PSEUDO INSTRUCTIONS
        CMP,
        CMPI,
        LDA,
        MOV,

        //      DEBUG INSTRUCTIONS
        CLR,
        DUMP,
        DAM,
        DAR,
        DR,
        DM,
        DMR,
        DMRI,
    }
}
