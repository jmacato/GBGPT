using System.Text;

GameBoyCPU gameBoyCPU = new GameBoyCPU();

byte[] program =
{
    0xC3, 0x0F, 0x00, //Jump to 0x0F  
    0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64, 0x21, //Write "Hello World!"
    0x11, 0x00, 0xC0, //Load 0xC000 into DE
    0x21, 0x03, 0x00, //Load 0x0003 into HL
    0x06, 0x0C, //Load 12 into B
    0x7E, //Load (HL) into A (Load 0x03 into A)
    0x12, //Write the content of A into (DE)
    0x23, //Increment HL (HL = 0x0004)
    0x13, //Increment DE (DE = 0xC001)
    0x05, //Decrement B (B = 11)
    0xC2, 0x17, 0x00, //Jump back to 0x0017 if B is not 0
    // Load zero to A
    0x3E, 0x00,
    0x11, 0x00, 0xC0, //Load 0xC000 into DE
    0x06, 0x0C, //Load 12 into B
    0x12, //Write the content of A into (DE)
    0x13, //Increment DE  
    0x05, //Decrement B 
    0xC2, 0x26, 0x00, //Jump back to 0x27 if B is not 0
    0xC3, 0x00, 0x00 // Jump to 0x0000
};

// Initial state of registers
gameBoyCPU.SP = 0xCFEE;
gameBoyCPU.A = 0;
gameBoyCPU.H = 0;
gameBoyCPU.L = 0;


Array.Copy(program, gameBoyCPU.Memory, program.Length);

bool runningRealTimeClock = false;
bool runningPer100ms = false;

while (true)
{
    Console.Clear();
    DrawScreen();

    Console.WriteLine("Press any key to continue.");
    ConsoleKeyInfo c = Console.ReadKey();

    if (c.KeyChar == 'y')
    {
        runningPer100ms = true;
    }

    
    while (!gameBoyCPU.StopCPU)
    {
        Console.Clear();
        DrawScreen();
        gameBoyCPU.DecodeAndExecuteInstruction(gameBoyCPU.Memory[gameBoyCPU.PC]);
        Thread.Sleep(50);
    }
}

void DrawScreen()
{
   
    Console.Clear();
    Console.WriteLine("CPU Registers/Flags View");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine($"A  = 0x{gameBoyCPU.A:X2}     B  = 0x{gameBoyCPU.B:X2}  C  = 0x{gameBoyCPU.C:X2}  D  = 0x{gameBoyCPU.D:X2}");
    Console.WriteLine($"E  = 0x{gameBoyCPU.E:X2}     H  = 0x{gameBoyCPU.H:X2}  L  = 0x{gameBoyCPU.L:X2}  PC = 0x{gameBoyCPU.PC:X4}");
    Console.WriteLine($"SP = 0x{gameBoyCPU.SP:X4}   OP = 0x{gameBoyCPU.Memory[gameBoyCPU.PC]:X2}");
    Console.WriteLine("----------------------------------------");
    Console.Write($"Z  = {(gameBoyCPU.Zero ? 1 : 0)}");
    Console.Write($"   S = {(gameBoyCPU.Sign ? 1 : 0)}");
    Console.Write($"   P = {(gameBoyCPU.Parity ? 1 : 0)}");
    Console.Write($"   H = {(gameBoyCPU.HalfCarry ? 1 : 0)}");
    Console.Write($"   C = {(gameBoyCPU.Carry ? 1 : 0)} ");
    Console.WriteLine("\n----------------------------------------");
    Console.WriteLine($"StopCPU: {(gameBoyCPU.StopCPU ? "Yes" : "No")}");
    Console.WriteLine($"Memory at 0xC000: {string.Join("", gameBoyCPU.Memory[0xC000..(0xC000 + 19)].Select(x => x == 0 ? ' ' : (char)x))}");

}