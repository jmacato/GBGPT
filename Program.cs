using System.Diagnostics;
using GBGPT;


var gameBoyCpu = new GameBoyCpu();

var testRom = File.ReadAllBytes("/Users/jumarmacato/Downloads/DMG_ROM.bin");

Array.Copy(testRom, 0, gameBoyCpu.Memory, 0, testRom.Length);

// Patch out cartridge calls .
Enumerable.Repeat(byte.MinValue, 12).ToArray().AsMemory().CopyTo(
    gameBoyCpu.Memory.AsMemory(new Range(0x0028, 0x0034)));

// Patch out Video RAM interrupt.
Enumerable.Repeat(byte.MinValue, 12).ToArray().AsMemory().CopyTo(
    gameBoyCpu.Memory.AsMemory(new Range(0x0064, 0x0070)));

// Bypass logo checks.
new byte[] { 0, 0 }.CopyTo(
    gameBoyCpu.Memory.AsMemory(new Range(0x00e9, 0x00e9 + 2)));

new byte[] { 0, 0 }.CopyTo(
    gameBoyCpu.Memory.AsMemory(new Range(0x00ef, 0x00ef + 2)));

new byte[] { 0, 0 }.AsMemory().CopyTo(
    gameBoyCpu.Memory.AsMemory(new Range(0x00f7, 0x00f7 + 2)));

new byte[] { 0, 0 }.AsMemory().CopyTo(
    gameBoyCpu.Memory.AsMemory(new Range(0x00fa, 0x00fa + 2)));

gameBoyCpu.Memory[0x100] = 0xC3;


do
{
    
    DrawScreen();
    Console.ReadKey();
    gameBoyCpu.DecodeAndExecuteInstruction(gameBoyCpu.Memory[gameBoyCpu.ProgramCounter]);

    // if (consoleClock.Elapsed.TotalMilliseconds > 250)
    // {
    //     consoleClock.Restart();
    // }
    //
    // if (gameBoyCPU.PC == 0x0c || gameBoyCPU.PC >= 0x95)
    // {
    //     DrawScreen();
    // }


    // Thread.Sleep(TimeSpan.FromMicroseconds(4.9d));

} while (!gameBoyCpu.Halt);


void DrawScreen()
{
    
    Console.Clear();
    Console.WriteLine("CPU Registers/Flags View");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(
        $"A  = 0x{gameBoyCpu.A:X2}     B  = 0x{gameBoyCpu.B:X2}  C  = 0x{gameBoyCpu.C:X2}  D  = 0x{gameBoyCpu.D:X2}");
    Console.WriteLine(
        $"E  = 0x{gameBoyCpu.E:X2}     H  = 0x{gameBoyCpu.H:X2}  L  = 0x{gameBoyCpu.L:X2}  PC = 0x{gameBoyCpu.ProgramCounter:X4}");
    Console.WriteLine(
        $"SP = 0x{gameBoyCpu.StackPointer:X4}   OP = 0x{gameBoyCpu.Memory[gameBoyCpu.ProgramCounter]:X2}");
    Console.WriteLine("----------------------------------------");
    Console.Write($"Z  = {(gameBoyCpu.Zero ? 1 : 0)}");
    Console.Write($"   S = {(gameBoyCpu.Sign ? 1 : 0)}");
    Console.Write($"   P = {(gameBoyCpu.Parity ? 1 : 0)}");
    Console.Write($"   H = {(gameBoyCpu.HalfCarry ? 1 : 0)}");
    Console.Write($"   C = {(gameBoyCpu.Carry ? 1 : 0)} ");
    Console.WriteLine("\n----------------------------------------");
    Console.WriteLine(
        $"HALT: {(gameBoyCpu.Halt ? "Yes" : "No ")}");

    DisassemblyView(gameBoyCpu);
}


void DisassemblyView(GameBoyCpu cpu)
{
    // Initialize variables
    int pastnextcount = 3;
    int pastbuffer = 3;
    List<ushort> validAddresses = new();

    ushort lastAddr = 0;
    ushort nextInstrCounter = 0;
    do
    {
        // Get the instruction length
        ushort instructionLength = cpu.OpcodeData[cpu.Memory[lastAddr]].Item1;

        validAddresses.Add(lastAddr);

        if (lastAddr == cpu.ProgramCounter) break;

        // Move to the next instruction
        lastAddr += instructionLength;
    } while (true);


    validAddresses = validAddresses.TakeLast(pastnextcount).ToList();
    pastbuffer -= validAddresses.Count;


    lastAddr = (ushort)(cpu.ProgramCounter + cpu.OpcodeData[cpu.Memory[cpu.ProgramCounter]].Item1);
    nextInstrCounter = 0;
    do
    {
        if (lastAddr > cpu.Memory.Length) break;

        // Get the instruction length
        ushort instructionLength = cpu.OpcodeData[cpu.Memory[lastAddr]].Item1;

        validAddresses.Add(lastAddr);

        // Move to the next instruction
        lastAddr += instructionLength;
        nextInstrCounter++;
    } while (nextInstrCounter < pastnextcount);

    Console.Write(string.Join(char.MinValue, Enumerable.Repeat(Environment.NewLine, pastbuffer)));

    foreach (var addr in validAddresses)
    {
        var opc = cpu.Memory[addr];
        string dis;
        if (opc != 0xCB)
        {
            dis = cpu.OpcodeData[opc].Item2.Invoke(cpu);

        }
        else
        {
            dis = cpu.PrefixCBOpNames[cpu.Memory[addr + 1]];
        }
        
        Console.WriteLine(
            $"${addr:X4} - {(addr == cpu.ProgramCounter ? ">>" : "  ")} {dis}");
    }
}