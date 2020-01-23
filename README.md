# ButHowDoItComputer

- [ButHowDoItComputer](#buthowdoitcomputer)
  - [But How Do It Know? by J. Clark Scott](#but-how-do-it-know-by-j-clark-scott)
    - [Notes](#notes)
      - [Introduction](#introduction)
      - [Just the facts ma'am](#just-the-facts-maam)
      - [Speed](#speed)
      - [Language](#language)
      - [Just a little bit](#just-a-little-bit)
      - [What the](#what-the)
      - [Simple Variations](#simple-variations)
      - [Diagrams](#diagrams)
      - [Remember When](#remember-when)
      - [What can we do with a bit](#what-can-we-do-with-a-bit)
      - [A rose by any other name](#a-rose-by-any-other-name)
      - [Eight is enough](#eight-is-enough)
      - [Codes](#codes)
      - [Back to the byte](#back-to-the-byte)
      - [The Magic Bus](#the-magic-bus)
      - [Ram](#ram)
      - [The other half of the computer](#the-other-half-of-the-computer)
      - [The Adder](#the-adder)
      - [The first great invention](#the-first-great-invention)
      - [Instructions](#instructions)
        - [The Arithmetic or Logic Instruction](#the-arithmetic-or-logic-instruction)
          - [Operation](#operation)
          - [Registers](#registers)
          - [Instruction Codes](#instruction-codes)

## But How Do It Know? by J. Clark Scott

### Notes

#### Introduction

The title of the book is based off an old joke where someone is showing someone else a thermous. They tell the other person that it keeps hot things hot and cold things cold.

Then the other asks, "But How do it Know?" thinking that the thermous knows if the water is hot or cold.

This links into computers as people often think they are more complicated than they actually are.

The thermous has the priciple motion of heat. The computer has electricity.

All a computer is, is a lot of light bulbs turning on and off.

#### Just the facts ma'am

#### Speed

The main thing to note is that computers don't do anything to crazy. They do a few simple things And they can only do those things one at a time.

What makes them seem like they do a lot more is they do them FAST.

This is because they run on electricity, and electricity is fast!.

#### Language

Computers have their own lingo :P

#### Just a little bit

- A computer is made up of only bits
- A bit is something that has two states. on/off, true/false, yes/no

#### What the

- The way to make more complex stuff and perform operations is to combine bits.
- This is done through the use of gates
- one of those gates is the and gate.

#### Simple Variations

- There are other types of gates
  - not
  - and
  - nand

#### Diagrams

- This book uses standard circuit diagrams

#### Remember When

Computers can only use bits everywhere. So how do we make memory?. With bits of course

1. i: 0 s: 0 o: 0
2. i: 1 s: 1 o: 1
3. i: 0 s: 0 o: 1
4. i: 0 s: 1 o: 0

Basically o is whatever i is so long as s = 1 otherwise it remains in it's previous state.

Look at the chapter for a better diagram.

#### What can we do with a bit

A bit can never mean more or less than the presence of electricity. It's the meaning that we assosiate with than on off state that means something, a 'code'. e.g. a red light is a code. If the red light bit is on it means that you need to stop, if it is off you can go.

#### A rose by any other name

#### Eight is enough

A byte is 8 bits. Which means we have 256 configurations.

#### Codes

- Bytes don't inherently mean something. But you can represent things with them.
  - For example ASCII uses one byte to representa char.

#### Back to the byte

- A register is made of two parts
- The byte memory gate and an enabler
- This means that a byte can be stored and then parsed on to others if need be.

#### The Magic Bus

- A bus is traditionaly 8 Wires that can hold the value of one byte.
- Allows for comunication between registers.
- Registers input is connected to the bus
- This allows you to transfer data between one register and another
  - e.g. to move between register 1 and 4
    - Set register 1 enable pin to true
    - Set register 4's set pin to true
    - The value of register 1 will now be in 4

#### Ram

- Made up of something that selects which register to read/write
- Has i/o bus attached to read/write to that register.

#### The other half of the computer

- The CPU
  - Apprently "nothing but NAND gates"
  - Does something with a to bytes in ram
  - Big loop connected with different parts connected
    - loop stats/ends with memorary address register and IO of ram
    - CPU has 4 registers for temporary storage

#### The Adder

- The basics of arithmatic in binary is easier than in base 10
- There are only 4 possible results

    | a | b | o  |
    | 0 | 0 | 0  |
    | 0 | 1 | 1  |
    | 1 | 0 | 1  |
    | 1 | 1 | 10 |
- This can be done with an XOR Gate and a And as Carry
- Carry is whether or not we need to carry a 1 e.g. when a = b = 1
- Adding the next column along gets a little harder as it is like adding three numbers
  - a
  - b
  - carry
- This time you will have a carry in (see book for diagram)

#### The first great invention

Instructions are lined up in ram in the order they will be executed. This will make up a "program"

- Each stage of the stepper does one step of the CPU cycle which will execute one instruction.
  - Set the MAR to what is in the IAR (Instruction Address Registry) this is where in ram the next instruction is.
    - also during this stage the IAR is incremented by one as this is where the next instruction will be next cycle.
  - Move the byte in the selected ram address into the IR (Instruction Register), this is the instruction that will be executed in step 4, 5, 6
  - Instruction moved from ACC to IAR

#### Instructions

As we are designing our own CPU we get to make our own codes up.
Depending on the codes that we choose will depend on how we wire up the CPU.
There is a posibility of 256 instructions
We are only going to create 9.

##### The Arithmetic or Logic Instruction

- Uses the ALU
- Most veristile instruction 128 posibilities

| ALU Instruction | Operation | Register A | Register B |
| --------------- | --------- | ---------- | ---------- |
| 1               | 000       | 00         | 00         |

###### Operation

- 000 Add
- 001 Shift Left
- 010 Shift Right
- 011 Not
- 100 And
- 101 Or
- 110 Xor
- 111 Cmp

###### Registers

- 00 Register 0
- 01 Register 1
- 10 Register 2
- 11 Register 3

e.g. to add register 2 to register 3 and store in register 3 you would do

1 000 10 11

With one byte operations e.g. Shift Left you can choose where the anwser goes

either 2 to 3 or 2 to 2 etc.

###### Instruction Codes

- ADD RA,RB Add RA and RB and put the answer in RB
- SHR RA,RB Shift RA Right and put the answer in RB
- SHL RA,RB Shift RA Left and put the answer in RB
- NOT RA,RB Not RA and put the answer in RB
- AND RA,RB And RA and RB and put the answer in RB
- OR RA,RB Or RA and RB and put the answer in RB
- XOR RA,RB XOR RA and RB and put the answer in RB
- CMP RA,RB Compare RA and RB
