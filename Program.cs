using System.Text;

GameBoyCPU gameBoyCPU = new GameBoyCPU();


byte[] program = {
    0x21, 0x00, 0xC0, // LD HL, 0xC000 ; Load 0xC000 into HL
    0x3E, 0x48,       // LD A, 0x48    ; Load 0x48 into A (ASCII 'H')
    0x32, 0x00, 0xC0, // LD (HL), A    ; Store A into memory location 0xC000
    0x3E, 0x65,       // LD A, 0x65    ; Load 0x65 into A (ASCII 'e')
    0x32, 0x00, 0xC1, // LD (HL+1), A  ; Store A into memory location 0xC001
    0x3E, 0x6C,       // LD A, 0x6C    ; Load 0x6C into A (ASCII 'l')
    0x32, 0x00, 0xC2, // LD (HL+2), A  ; Store A into memory location 0xC002
    0x3E, 0x6C,       // LD A, 0x6C    ; Load 0x6C into A (ASCII 'l')
    0x32, 0x00, 0xC3, // LD (HL+3), A  ; Store A into memory location 0xC003
    0x3E, 0x6F,       // LD A, 0x6F    ; Load 0x6F into A (ASCII 'o')
    0x32, 0x00, 0xC4, // LD (HL+4), A  ; Store A into memory location 0xC004
    0x20, 0x20,       // LD A, 0x20    ; Load 0x20 into A (ASCII ' ')
    0x32, 0x00, 0xC5, // LD (HL+5), A  ; Store A into memory location 0xC005
    0x3E, 0x57,       // LD A, 0x57    ; Load 0x57 into A (ASCII 'W')
    0x32, 0x00, 0xC6, // LD (HL+6), A  ; Store A into memory location 0xC006
    0x3E, 0x6F,       // LD A, 0x6F    ; Load 0x6F into A (ASCII 'o')
    0x32, 0x00, 0xC7, // LD (HL+7), A  ; Store A into memory location 0xC007
    0x3E, 0x72,       // LD A, 0x72    ; Load 0x72 into A (ASCII 'r')
    0x32, 0x00, 0xC8, // LD (HL+8), A  ; Store A into memory location 0xC008
    0x3E, 0x6C,       // LD A, 0x6C    ; Load 0x6C into A (ASCII 'l')
    0x32, 0x00, 0xC9, // LD (HL+9), A  ; Store A into memory location 0xC009
    0x3E, 0x64,       // LD A, 0x64    ; Load 0x64 into A (ASCII 'd')
    0x32, 0x00, 0xCA, // LD (HL+10), A ; Store A into memory location 0xC00A
    0x3E, 0x21,       // LD A, 0x21    ; Load 0x21 into A (ASCII '!')
    0x32, 0x00, 0xCB, // LD (HL+11), A ; Store A into memory location 0xC00B
    0xC3, 0x00, 0x00  // JP 0x0000     ; Jump to 0x0000 (loop forever)
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
    DrawScreen();

    void DrawScreen()
    {
        Console.Clear();

        var spacer1 = new string('-', Console.WindowWidth - 1);
        var border = $"{spacer1}\r\n";


        Console.WriteLine(border);
        Console.WriteLine("Registers:");
        Console.WriteLine(
            $"A: {gameBoyCPU.A} B:{gameBoyCPU.B} C:{gameBoyCPU.C} D:{gameBoyCPU.D} E:{gameBoyCPU.E} H:{gameBoyCPU.H} L:{gameBoyCPU.L} SP:{gameBoyCPU.SP} PC:{gameBoyCPU.PC}");
        Console.WriteLine(
            $"Flags: Z:{gameBoyCPU.Zero} Carry:{gameBoyCPU.Carry} Sign:{gameBoyCPU.Sign} Parity:{gameBoyCPU.Parity} HalfCarry:{gameBoyCPU.HalfCarry}");
        Console.WriteLine($"Memory at 0xC000: {Encoding.ASCII.GetString(gameBoyCPU.Memory[0xC000..(0xC000 + 30)])}");
        Console.WriteLine("X - Run realtime clock, Y - Run per 100ms, Space - Pause execution");

        Console.WriteLine(border);
    }

    ConsoleKeyInfo c = Console.ReadKey();

    if (c.KeyChar == 'y')
    {
        runningPer100ms = true;
    }

    Console.Clear();
    DrawScreen();

    while (!gameBoyCPU.StopCPU)
    {
        gameBoyCPU.DecodeAndExecuteInstruction(gameBoyCPU.Memory[gameBoyCPU.PC]);

        if (Console.KeyAvailable) return;
        DrawScreen();
        if (runningPer100ms) Thread.Sleep(100);
    }
}