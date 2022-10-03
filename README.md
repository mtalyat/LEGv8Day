README WIP. The following information may be incomplete.

General Information
======

## About

This is a LEGv8 Simulator for Windows. The program is able to input a text file or a LEGv8 Assembly file, parse and run the file, in addition to providing an editor for the files. The editor uses a Rich Text Format to nicely display the file data for the user. All labels, instruction mnemonics, numbers, comments and registers will have a color assigned to them. The colors can be edited as well using a theme (check out the Features > Editor > View section below). 

This simulator also has support for using alternative names for registers. For example, register 31 (`X31`) can be written as `XZR` or `X31`. Numbers can be written as `23` or `#23`. Negative numbers are supported. The simulation will begin executing instructions at the first line of the program that contains an instruction.

## Motivation

This was created to be able to create an run LEGv8 Assembly code. The main factors behind motivation was to learn how LEGv8/Assembly communications with the hardware better, and to create a program so that I did not have to use Linux. This simulator is written on and for Windows.

Features
======

Instructions
------

The following are the implemented instructions into this simulator. For further explaination, check out the cheat sheet in the references below.

| Name | Mnemonic | Format | Definition | Example |
| ---- | -------- | - | ---------- | ------- |
| ADD | ADD | R | Adds two registers together. | `ADD X0, X1, X2 // adds X1 and X2, stores result in X0` |
| ADD and Set flags | ADDS | R | Adds two registers together and sets flags. | `ADDS X0, X1, X2 // adds X1 and X2, stores result in X0, sets flags` |
| ADD Immediate | ADDI | I | Adds a register to an immediate value. | `ADDI X0, X1, 5 // adds 5 to X1, stores result in X0` |
| ADD Immediate and Set flags | ADDIS | I | Adds a register to an immediate value, and sets flags. | `ADDIS X0, X1, 5 // adds 5 to X1, stores result in X0, sets flags` |
| AND | AND | R | Bitwise AND using two registers. | `AND X0, X1, X2 // stores result of X1 AND X2 in X0` |
| AND and Set flags | ANDS | R | Bitwise AND using two registers, and set flags. | `ANDS X0, X1, X2 // stores result of X1 AND X2 in X0, sets flags` |
| AND Immediate | ANDI | I | Bitwise AND using a register and an immediate value. | `ANDI X0, X1, 5 // stores result of X1 AND 5 in X0` |
| AND Immedaite and Set flags | ANDIS | I | Bitwise AND using a register and an immediate value, and sets flags. | `ANDIS X0, X1, 5 // stores result of X1 AND 5 in X0, sets flags` |
| Branch unconditionally | B | B | Starts executing instructions at the given label. | `B loop // branches to label "loop:"` |
| Branch conditionally (EQual) | B.EQ | CB | Branches to the given label, if the zero flag is enabled. | `B.EQ loop // branches to label "loop:" if last flags call had equal values"` |
| Branch conditionally (Not Equal) | B.NE | CB | Branches to the given label, if the zero flag is not enabled. | `B.NE loop // branches to the label "loop:" if the last flags call had not equal vlaues"` |
| Branch conditionally (Less Than) | B.LT | CB | ... | ... |
| Branch conditionally (Less than or Equal) | B.LE | CB | ... | ... |
| Branch conditionally (Greater Than) | B.GT | CB | ... | ... |
| Branch conditionally (Greater than or Equal) | B.GE | CB | ... | ... |
| Branch conditionally (HIgher) | B.HI | CB | ... | ... |
| Branch conditionally (Higher or Same) | B.HS | CB | ... | ... |
| Branch conditionally (LOwer) | B.LO | CB | ... | ... |
| Branch conditionally (Lower or Same) | B.LS | CB | ... | ... |
| Branch conditionally (on MInus) | B.MI | CB | ... | ... |
| Branch conditionally (on PLus) | B.PL | CB | ... | ... |
| Branch conditionally (on oVerflow Set) | B.VS | CB | ... | ... |
| Branch conditionally (on oVerflow Clear) | B.VC | CB | ... | ... |
| Branch with Link | BL | B | Branches to the given label, and sets the return register (`LR`/`X30`) to the current execution index. | `BL loop // branches to label "loop:", saves execution index for return later` |
| Branch to Register | BR | R | Branches to the execution index given within the register. | `BR X30 // branches to the execution index stored in X30 (could be any register)` |
| Compare and Branch if Not Zero | CBNZ | CB | Branches to the given label, if the given register is not zero. | `CBNZ X0, loop // branches to label "loop:" if the given register is not zero` |
| Compare and Branch if Zero | CBNZ | CB | Branches to the given label, if the given register is zero. | `CBZ X0, loop // branches to label "loop:" if the given register is zero` |
| Exclusive OR | EOR | R | Exclusive OR using two registers. | `EOR X0, X1, X2 // stores result of X1 XOR X2 in X0` |

Editor
------

### File

The editor has the ability to create new files, open existing files on the disc, or to save/save as files to the disc.

### Edit

The editor has the ability to undo and redo.

### View

The editor has the ability to change the colors of the theme. The default theme cannot be edited or deleted. New themes can use any color for the background or code editor colors.

### Run

References
======

The following are resources that were used to help create this project.

* [LEGv8 Reference Data Cheat Sheet](https://www.usna.edu/Users/cs/lmcdowel/courses/ic220/S20/resources/ARM-v8-Quick-Reference-Guide.pdf)
