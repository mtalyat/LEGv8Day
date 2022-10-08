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

This was created to be able to create an run LEGv8 Assembly code. The main factors behind motivation was to learn how LEGv8/Assembly communications with the hardware better. This emulator is written on and for Windows.

Features
======

Instructions
------

Most basic instructions have been added to the emulator. The full list of them can be found on the [instructions page](https://github.com/mtalyat/LEGv8Day/wiki/Instructions) on the wiki. Most instructions are part of the core instruction set, but there are pseudo instructions as well. In addition to those, there have also been some instructions added for debugging, including a way to dump the register, as well as other things.

Editor
------

### File

The editor has the ability to create new files, open existing files on the disc, or to save/save as files to the disc. When the application starts, it will load the last file loaded/saved, as long as it still exists at the last known file location.

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
