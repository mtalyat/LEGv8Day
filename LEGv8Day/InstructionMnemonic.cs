﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public enum InstructionMnemonic : int
    {
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
        B_cond,
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
    }
}