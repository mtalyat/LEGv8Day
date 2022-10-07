using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public enum InstructionFormat
    {
        /// <summary>
        /// Has no format or values.
        /// </summary>
        Empty,

        /// <summary>
        /// opcode  [31-21]
        /// Rm      [20-16]
        /// shamt   [15-10]
        /// Rn      [9-5]
        /// Rd      [4-0]
        /// </summary>
        R,

        /// <summary>
        /// opcode          [31-22]
        /// ALU_immediate   [21-10]
        /// Rn              [9-5]
        /// Rd              [4-0]
        /// </summary>
        I,

        /// <summary>
        /// opcode      [31-21]
        /// DT_address  [20-12]
        /// op          [11-10]
        /// Rn          [9-5]
        /// Rt          [4-0]
        /// </summary>
        D,

        /// <summary>
        /// opcode      [31-26]
        /// BR_address  [25-0]
        /// </summary>
        B,

        /// <summary>
        /// opcode          [31-24]
        /// COND_BR_address [23-5]
        /// Rt              [4-0]
        /// </summary>
        CB,

        /// <summary>
        /// opcode          [31-21]
        /// MOV_immediate   [20-5]
        /// Rd              [4-0]
        /// </summary>
        IM,

        /// <summary>
        /// DEBUG
        /// </summary>
        Z
    }
}
