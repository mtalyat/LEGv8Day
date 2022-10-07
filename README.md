README WIP. The following information may be incomplete.

Table of Contents
======
1. [General Information](#general-information)
2. [Features](#features)
3. [Examples](#examples)
4. [References](#references)

General Information
======

## About

This is a LEGv8 assembly emulator for Windows. The program is able to input a text file or a LEGv8 Assembly file, parse and run the file, in addition to providing an editor for the files. The editor uses a Rich Text Format to nicely display the file data for the user. All labels, instruction mnemonics, numbers, comments and registers will have a color assigned to them. The colors can be edited as well using a theme (check out the Features > Editor > View section below). 

This emulator also has support for using alternative names for registers. For example, register 31 (`X31`) can be written as `XZR` or `X31`. Numbers can be written as `23` or `#23`. Negative numbers are supported. The emulation will begin executing instructions at the first line of the program that contains an instruction.

*Note: This program does not yet handle incorrect syntax or other similar issues. The program may crash if the syntax for the open file is incorrect.*

## Motivation

This was created to be able to create an run LEGv8 Assembly code. The main factors behind motivation was to learn how LEGv8/Assembly communications with the hardware better, and to create a program so that I did not have to use Linux. This emulator is written on and for Windows.

Features
======

Instructions
------

The following are the instructions for this emulator. They will indicate their status of implementation below. For further explaination, check out the cheat sheet in the [references](#references) below. Any instructions not listed below may not be added in the future. However, instructions that are listed below will be added at some point.

### Core Instructions

These are instructions that are part of the LEGv8 assembly code. This does not include all LEGv8 instructions. For a list of all instructions, check out the cheat sheet in [references](#references).

| Implemented? | Name | Mnemonic | Format | Definition | Example |
| --- | ---- | -------- | - | ---------- | ------- |
| YES | ADD | ADD | R | Adds two registers together. | `ADD X0, X1, X2 // adds X1 and X2, stores result in X0` |
| YES | ADD and Set flags | ADDS | R | Adds two registers together and sets flags. | `ADDS X0, X1, X2 // adds X1 and X2, stores result in X0, sets flags` |
| YES | ADD Immediate | ADDI | I | Adds a register to an immediate value. | `ADDI X0, X1, 5 // adds 5 to X1, stores result in X0` |
| YES | ADD Immediate and Set flags | ADDIS | I | Adds a register to an immediate value, and sets flags. | `ADDIS X0, X1, 5 // adds 5 to X1, stores result in X0, sets flags` |
| YES | AND | AND | R | Bitwise AND using two registers. | `AND X0, X1, X2 // stores result of X1 AND X2 in X0` |
| YES | AND and Set flags | ANDS | R | Bitwise AND using two registers, and set flags. | `ANDS X0, X1, X2 // stores result of X1 AND X2 in X0, sets flags` |
| YES | AND Immediate | ANDI | I | Bitwise AND using a register and an immediate value. | `ANDI X0, X1, 5 // stores result of X1 AND 5 in X0` |
| YES | AND Immedaite and Set flags | ANDIS | I | Bitwise AND using a register and an immediate value, and sets flags. | `ANDIS X0, X1, 5 // stores result of X1 AND 5 in X0, sets flags` |
| YES | Branch unconditionally | B | B | Starts executing instructions at the given label. | `B loop // branches to label "loop:"` |
| YES | Branch conditionally (EQual) | B.EQ | CB | Branches to the given label, if the zero flag is enabled. | `B.EQ loop // branches to label "loop:" if last flags call had equal values` |
| YES | Branch conditionally (Not Equal) | B.NE | CB | Branches to the given label, if the zero flag is not enabled. | `B.NE loop // branches to the label "loop:" if the last flags call had not equal values` |
| YES | Branch conditionally (Less Than) | B.LT | CB | ... | ... |
| YES | Branch conditionally (Less than or Equal) | B.LE | CB | ... | ... |
| YES | Branch conditionally (Greater Than) | B.GT | CB | ... | ... |
| YES | Branch conditionally (Greater than or Equal) | B.GE | CB | ... | ... |
| YES | Branch conditionally (HIgher) | B.HI | CB | ... | ... |
| YES | Branch conditionally (Higher or Same) | B.HS | CB | ... | ... |
| YES | Branch conditionally (LOwer) | B.LO | CB | ... | ... |
| YES | Branch conditionally (Lower or Same) | B.LS | CB | ... | ... |
| YES | Branch conditionally (on MInus) | B.MI | CB | ... | ... |
| YES | Branch conditionally (on PLus) | B.PL | CB | ... | ... |
| NO  | Branch conditionally (on oVerflow Set) | B.VS | CB | ... | ... |
| NO  | Branch conditionally (on oVerflow Clear) | B.VC | CB | ... | ... |
| YES | Branch with Link | BL | B | Branches to the given label, and sets the return register (`LR`/`X30`) to the current execution index. | `BL loop // branches to label "loop:", saves execution index for return later` |
| YES | Branch to Register | BR | R | Branches to the execution index given within the register. | `BR X30 // branches to the execution index stored in X30 (could be any register)` |
| YES | Compare and Branch if Not Zero | CBNZ | CB | Branches to the given label, if the given register is not zero. | `CBNZ X0, loop // branches to label "loop:" if the given register is not zero` |
| YES | Compare and Branch if Zero | CBNZ | CB | Branches to the given label, if the given register is zero. | `CBZ X0, loop // branches to label "loop:" if the given register is zero` |
| YES | Exclusive OR | EOR | R | Exclusive OR using two registers. | `EOR X0, X1, X2 // stores result of X1 XOR X2 in X0` |
| YES | Exclusive OR Immediate | EORI | I | Exclusive OR using a register and an immediate value. | `EOR X0, X1, 5 // stores the result of X1 XOR 5 in X0` |
| YES | LoaD Register Unscaled offset | LDUR | D | Retrieves data from memory and stores it in a register. | `LDUR X0, [X1, 0] // stores the memory at pointer location X1 + offset (0) in register X0` |
| YES | LoaD Byte Unscaled offset | LDURB | D | ... | ... | 
| YES | LoaD Half Unscaled offset | LDURH | D | ... | ... | 
| YES | LoaD Signed Word Unscaled offset | LDURSW | D | ... | ... |
| YES | Logical Shift Left | LSL | R | Shifts the register to the left (<<) using the immediate value. | `LSL X0, X1, 5 // shifts the register X1 5 times to the left, stores result in X0` |
| YES | Logical Shift Right | LSR | R | Shifts the register to the right (>>) using the immediate value. | `LSR X0, X1, 5 // shifts the register X1 5 times to the right, stores result in X0` |
| YES | MOVe wide with Keep | MOVK | IM | Stores the immediate value in the register, but does not change the other bits in the register. | `MOVK X0, 5 // sets register to 5, but does not change existing bits` |
| YES | MOV wide with Zero | MOVZ | IM | Stores the immediate value in the register, and sets the other bits in the register to zero. | `MOVZ X0, 5 // sets register to 5, and changes existing bits to 0` |
| YES | Inclusive OR | ORR | R | Bitwise OR using two registers. | `ORR X0, X1, X2 // stores result of X1 OR X2 in X0` |
| YES | Inclusive OR Immediate | ORRI | I | Bitwise OR using a register and an immediate value. | `ORRI X0, X1, 5 // stores result of X1 OR 5 in X0` |
| YES | STore Register Unscaled offset | STUR | D | Sets data in memory to the register value. | `STUR X0, [X1, 0] // stores the value in register X0 to memory at location X1 + offset (0)` |
| YES | STore Byte Unscaled offset | STURB | D | ... | ... |
| YES | STore Half Unscaled offset | STURH | D | ... | ... |
| YES | STore Signed Word Unscaled offset | STURSW | D | ... | ... |
| YES | SUBtract | SUB | R | Subtracts one register from another. | `SUB X0, X1, X2 // subtracts X2 from X1, stores result in X0` |
| YES | SUBtract and Set flags | SUBS | R | Subtracts one register from another, and sets flags. | `SUB X0, X1, X2 // subtracts X2 from X1, stores result in X0, sets flags` |
| YES | SUBtract Immediate | SUBI | I | Subtracts an immediate value from a register. | `SUBI X0, X1, 5 // subtracts 5 from X1, stores result in X0` |
| YES | SUBtract Immediate and Set flags | SUBIS | I | Subtracts an immediate value from a register, and sets flags. | `SUB X0, X1, 5 // subtracts 5 from X1, stores result in X0, sets flags` |
| YES | MULtiply | MUL | R | Multiplies two registers together. | `MUL X0, X1, X2 // multiplies X1 and X2 together, stores result in X0` |
| YES | Signed DIVide | SDIV | R | Divides two registers together. | `SDIV X0, X1, X2 // divides X2 into X1, stores result in X0` |
| NO  | Signed MULtiply High | SMULH | R | ... | ... |
| YES | Unsigned DIVide | UDIV | R | Divides two unsigned registers together. | `UDIV X0, X1, X2 // divides X2 into X1, stores result in X0` |
| NO  | Unsigned MULtiply High | UMULH | R | ... | ... |

### Pseudoinstructions

These are instructions that are short for another instruction. They can be written in the program and still function as they should. When being assembled, they would be swapped out with their equivalents.

| Implemented? | Name | Mnemonic | Equivalent | Definition | Example |
| --- | ---- | -------- | - | ---------- | ------- |
| NO  | CoMPare | CMP | `SUBS XZR, m, n` where `m` and `n` are registers | Compares the two given registers by setting the flags. | `CMP X0, X1 // compares X0 and X1` |
| NO  | CoMPare Immediate | CMPI | `SUBIS XZR, m, n` where `m` is a register, and `n` is an immediate value | Compares the given register and the given value by setting the flags. | `CMPI X0, 5 //compares X0 and 5` |
| NO  | LoaD Address | LDA | ... | Loads the return address. | `LDA X0, loop // loads the address of the label "loop" into register X0` |
| NO  | MOVe | MOV | `ORR m, XZR, n` where `m` and `n` are registers | Moves a register value to another register. | `MOV X0, X1 // sets X0 to X1` |

### Debug

These are instructions that are specific to this emulator. Their sole purpose is to aid in the debugging process.

| Implemented? | Name | Mnemonic | Definition | Example |
| --- | ---- | -------- | ---------- | ------- |
| NO  | CLeaR | CLR | Clears all registers and memory in the emulator. | `CLR // reset all registers and memory to 0` |
| NO  | Dump | D | Displays all registers and memory in the output. | `D // show all register values and memory at this point in time` |
| NO  | Dump All Memory | DAM | Displays all memory in the output. | `DAM // show all memory at this point in time` |
| NO  | Dump All Registers | DAR | Displays all registers in the output. | `DAR // show all register values at this point in time` |
| NO  | Dump Register | DR | Displays a given register in the output. | `DR X0 // shows the value of X0 at this point in time` |
| NO  | Dump Memory | DM | Displays a given byte in memory in the output. | `DM X0 // shows the value of the data in the memory at pointer X0 at this point in time` |
| NO  | Dump Memory in Range | DMR | Displays a range of memory in the output. | `DMR X0, X1 // shows the values of the data in the memory from pointer X0 to X1 at this point in time` |
| NO  | Dump Memory in Range Immediate | DMRI | Displays a range of memory in the output. | `DMRI X0, 5 // shows the values of the data in the memory from pointer X0 to X0 + 5 at this point in time` |

Editor
------

### File

The editor has the ability to create new files, open existing files on the disc, or to save/save as files to the disc.

### Edit

The editor has the ability to undo and redo.

### View

The editor has the ability to change the colors of the theme. The default theme cannot be edited or deleted. New themes can use any color for the background or code editor colors.

### Run

The editor will allow the user to run the currently open file. Once the program is complete, the registers and memory will be dumped and displayed to the screen. If the program takes a significant amount of time to complete, the user will be given the option to cancel the process.

Examples
======

Examples to come soon.

References
======

The following are resources that were used to help create this project.

* [LEGv8 Reference Data Cheat Sheet](https://www.usna.edu/Users/cs/lmcdowel/courses/ic220/S20/resources/ARM-v8-Quick-Reference-Guide.pdf)
* [Computer Organization and Design: The Hardware/ Software Interface, ARM® Edition](https://www.amazon.com/Computer-Organization-Design-ARM-Architecture-ebook/dp/B01H1DCRRC)
