﻿using System.Diagnostics;
using System.Text;
using System.Timers;

//
// var cbops = new string[]
// {
//     "RLC B", "RLC C", "RLC D", "RLC E", "RLC H", "RLC L", "RLC (HL)", "RLC A", "RRC B", "RRC C", "RRC D", "RRC E",
//     "RRC H", "RRC L", "RRC (HL)", "RRC A", "RL B", "RL C", "RL D", "RL E", "RL H", "RL L", "RL (HL)", "RL A", "RR B",
//     "RR C", "RR D", "RR E", "RR H", "RR L", "RR (HL)", "RR A", "SLA B", "SLA C", "SLA D", "SLA E", "SLA H", "SLA L",
//     "SLA (HL)", "SLA A", "SRA B", "SRA C", "SRA D", "SRA E", "SRA H", "SRA L", "SRA (HL)", "SRA A", "SWAP B", "SWAP C",
//     "SWAP D", "SWAP E", "SWAP H", "SWAP L", "SWAP (HL)", "SWAP A", "SRL B", "SRL C", "SRL D", "SRL E", "SRL H", "SRL L",
//     "SRL (HL)", "SRL A", "BIT 0,B", "BIT 0,C", "BIT 0,D", "BIT 0,E", "BIT 0,H", "BIT 0,L", "BIT 0,(HL)", "BIT 0,A",
//     "BIT 1,B", "BIT 1,C", "BIT 1,D", "BIT 1,E", "BIT 1,H", "BIT 1,L", "BIT 1,(HL)", "BIT 1,A", "BIT 2,B", "BIT 2,C",
//     "BIT 2,D", "BIT 2,E", "BIT 2,H", "BIT 2,L", "BIT 2,(HL)", "BIT 2,A", "BIT 3,B", "BIT 3,C", "BIT 3,D", "BIT 3,E",
//     "BIT 3,H", "BIT 3,L", "BIT 3,(HL)", "BIT 3,A", "BIT 4,B", "BIT 4,C", "BIT 4,D", "BIT 4,E", "BIT 4,H", "BIT 4,L",
//     "BIT 4,(HL)", "BIT 4,A", "BIT 5,B", "BIT 5,C", "BIT 5,D", "BIT 5,E", "BIT 5,H", "BIT 5,L", "BIT 5,(HL)", "BIT 5,A",
//     "BIT 6,B", "BIT 6,C", "BIT 6,D", "BIT 6,E", "BIT 6,H", "BIT 6,L", "BIT 6,(HL)", "BIT 6,A", "BIT 7,B", "BIT 7,C",
//     "BIT 7,D", "BIT 7,E", "BIT 7,H", "BIT 7,L", "BIT 7,(HL)", "BIT 7,A", "RES 0,B", "RES 0,C", "RES 0,D", "RES 0,E",
//     "RES 0,H", "RES 0,L", "RES 0,(HL)", "RES 0,A", "RES 1,B", "RES 1,C", "RES 1,D", "RES 1,E", "RES 1,H", "RES 1,L",
//     "RES 1,(HL)", "RES 1,A", "RES 2,B", "RES 2,C", "RES 2,D", "RES 2,E", "RES 2,H", "RES 2,L", "RES 2,(HL)", "RES 2,A",
//     "RES 3,B", "RES 3,C", "RES 3,D", "RES 3,E", "RES 3,H", "RES 3,L", "RES 3,(HL)", "RES 3,A", "RES 4,B", "RES 4,C",
//     "RES 4,D", "RES 4,E", "RES 4,H", "RES 4,L", "RES 4,(HL)", "RES 4,A", "RES 5,B", "RES 5,C", "RES 5,D", "RES 5,E",
//     "RES 5,H", "RES 5,L", "RES 5,(HL)", "RES 5,A", "RES 6,B", "RES 6,C", "RES 6,D", "RES 6,E", "RES 6,H", "RES 6,L",
//     "RES 6,(HL)", "RES 6,A", "RES 7,B", "RES 7,C", "RES 7,D", "RES 7,E", "RES 7,H", "RES 7,L", "RES 7,(HL)", "RES 7,A",
//     "SET 0,B", "SET 0,C", "SET 0,D", "SET 0,E", "SET 0,H", "SET 0,L", "SET 0,(HL)", "SET 0,A", "SET 1,B", "SET 1,C",
//     "SET 1,D", "SET 1,E", "SET 1,H", "SET 1,L", "SET 1,(HL)", "SET 1,A", "SET 2,B", "SET 2,C", "SET 2,D", "SET 2,E",
//     "SET 2,H", "SET 2,L", "SET 2,(HL)", "SET 2,A", "SET 3,B", "SET 3,C", "SET 3,D", "SET 3,E", "SET 3,H", "SET 3,L",
//     "SET 3,(HL)", "SET 3,A", "SET 4,B", "SET 4,C", "SET 4,D", "SET 4,E", "SET 4,H", "SET 4,L", "SET 4,(HL)", "SET 4,A",
//     "SET 5,B", "SET 5,C", "SET 5,D", "SET 5,E", "SET 5,H", "SET 5,L", "SET 5,(HL)", "SET 5,A", "SET 6,B", "SET 6,C",
//     "SET 6,D", "SET 6,E", "SET 6,H", "SET 6,L", "SET 6,(HL)", "SET 6,A", "SET 7,B", "SET 7,C", "SET 7,D", "SET 7,E",
//     "SET 7,H", "SET 7,L", "SET 7,(HL)", "SET 7,A"
// };
//
// for(int i = 0; i < cbops.Length; i++)
//
// Console.WriteLine($"case 0x{i:X2}: // {cbops[i]}");


var gameBoyCPU = new GameBoyCPU();

var test_rom = File.ReadAllBytes("DMG_ROM.bin");

Array.Copy(test_rom, 0, gameBoyCPU.Memory, 0, test_rom.Length);

// Patch out cartridge calls .
     Enumerable.Repeat(byte.MinValue, 12).ToArray().AsMemory().CopyTo(
gameBoyCPU.Memory.AsMemory(new Range(0x0028,0x0034)));

// Patch out VRAM interrupt.
Enumerable.Repeat(byte.MinValue, 12).ToArray().AsMemory().CopyTo(
    gameBoyCPU.Memory.AsMemory(new Range(0x0064,0x0070)));

// Bypass logo checks.
Enumerable.Repeat(byte.MinValue, 2).ToArray().AsMemory().CopyTo(
    gameBoyCPU.Memory.AsMemory(new Range(0x00e9, 0x00e9 + 2)));

Enumerable.Repeat(byte.MinValue, 2).ToArray().AsMemory().CopyTo(
    gameBoyCPU.Memory.AsMemory(new Range(0x00ef, 0x00ef + 2)));

Enumerable.Repeat(byte.MinValue, 2).ToArray().AsMemory().CopyTo(
    gameBoyCPU.Memory.AsMemory(new Range(0x00f7, 0x00f7 + 2)));

Enumerable.Repeat(byte.MinValue, 2).ToArray().AsMemory().CopyTo(
    gameBoyCPU.Memory.AsMemory(new Range(0x00fa, 0x00fa + 2)));

var consoleClock = new Stopwatch();
var cpuClock = new Stopwatch();
cpuClock.Start();
consoleClock.Start();
Console.Clear();
do
{
    gameBoyCPU.DecodeAndExecuteInstruction(gameBoyCPU.Memory[gameBoyCPU.PC]);

    // if (consoleClock.Elapsed.TotalMilliseconds > 250)
    // {
    //     consoleClock.Restart();
    // }
    //
    // if (gameBoyCPU.PC == 0x0c || gameBoyCPU.PC >= 0x95)
    // {
    //     DrawScreen();
    //     Console.ReadKey();
    // }
    
    Thread.Sleep(TimeSpan.FromMicroseconds(4.9d * 10000d));
    DrawScreen();
} while (!gameBoyCPU.StopCPU);


void DrawScreen()
{
    Console.SetCursorPosition(0,0);
    Console.WriteLine("CPU Registers/Flags View");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(
        $"A  = 0x{gameBoyCPU.A:X2}     B  = 0x{gameBoyCPU.B:X2}  C  = 0x{gameBoyCPU.C:X2}  D  = 0x{gameBoyCPU.D:X2}");
    Console.WriteLine(
        $"E  = 0x{gameBoyCPU.E:X2}     H  = 0x{gameBoyCPU.H:X2}  L  = 0x{gameBoyCPU.L:X2}  PC = 0x{gameBoyCPU.PC:X4}");
    Console.WriteLine($"SP = 0x{gameBoyCPU.SP:X4}   OP = 0x{gameBoyCPU.Memory[gameBoyCPU.PC]:X2}");
    Console.WriteLine("----------------------------------------");
    Console.Write($"Z  = {(gameBoyCPU.Zero ? 1 : 0)}");
    Console.Write($"   S = {(gameBoyCPU.Sign ? 1 : 0)}");
    Console.Write($"   P = {(gameBoyCPU.Parity ? 1 : 0)}");
    Console.Write($"   H = {(gameBoyCPU.HalfCarry ? 1 : 0)}");
    Console.Write($"   C = {(gameBoyCPU.Carry ? 1 : 0)} ");
    Console.WriteLine("\n----------------------------------------");
    Console.WriteLine(
        $"HALT: {(gameBoyCPU.StopCPU ? "Yes" : "No ")}");
    Console.WriteLine(
        $"> {gameBoyCPU.DisassembleInstruction(gameBoyCPU.PC)}                           ");
}