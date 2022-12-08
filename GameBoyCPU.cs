// ReSharper disable CommentTypo

namespace GBGPT;

public class GameBoyCpu
{
    public byte A, B, C, D, E, H, L;
    public ushort ProgramCounter, StackPointer;
    public bool Zero, Carry, Sign, Parity, HalfCarry;
    public readonly byte[] Memory = new byte[0x10000];
    public bool Halt;

    private static bool IsParity(byte value)
    {
        var size = sizeof(byte) * 8;
        int i;
        var p = 0;
        for (i = 0; i < size; i++)
        {
            if ((value & (1 << i)) != 0)
                p++;
        }

        return (p % 2 == 0);
    }

    public void DecodeAndExecuteInstruction(byte opcode)
    {
        //decode and execute instruction
        switch (opcode)
        {
            case 0x00: //NOP
                ExecuteNop();
                break;

            case 0x01: //LD BC, nn
                ExecuteLdBCnn();
                break;

            case 0x02: //LD (BC), A
                ExecuteLdBca();
                break;

            case 0x03: //INC BC
                ExecuteIncBc();
                break;

            case 0x04: //INC B
                ExecuteIncB();
                break;

            case 0x05: //DEC B
                ExecuteDecB();
                break;

            case 0x06: //LD B, n
                ExecuteLdBn();
                break;

            case 0x07: //RLCA
                ExecuteRlca();
                break;

            case 0x08: //LD (nn), SP
                ExecuteLdnnSp();
                break;

            case 0x09: //ADD HL, BC
                ExecuteAddHlbc();
                break;

            case 0x0A: //LD A, (BC)
                ExecuteLdAbc();
                break;

            case 0x0B: //DEC BC
                ExecuteDecBc();
                break;

            case 0x0C: //INC C
                ExecuteIncC();
                break;

            case 0x0D: //DEC C
                ExecuteDecC();
                break;

            case 0x0E: //LD C, n
                ExecuteLdCn();
                break;

            case 0x0F: //RRCA
                ExecuteRrca();
                break;

            case 0x10: //STOP
                ExecuteStop();
                break;

            case 0x11: //LD DE, nn
                ExecuteLdDEnn();
                break;

            case 0x12: //LD (DE), A
                ExecuteLdDea();
                break;

            case 0x13: //INC DE
                ExecuteIncDe();
                break;

            case 0x14: //INC D
                ExecuteIncD();
                break;

            case 0x15: //DEC D
                ExecuteDecD();
                break;

            case 0x16: //LD D, n
                ExecuteLdDn();
                break;

            case 0x17: //RLA
                ExecuteRla();
                break;

            case 0x18: //JR n
                ExecuteJrn();
                break;

            case 0x19: //ADD HL, DE
                ExecuteAddHlde();
                break;

            case 0x1A: //LD A, (DE)
                ExecuteLdAde();
                break;

            case 0x1B: //DEC DE
                ExecuteDecDe();
                break;

            case 0x1C: //INC E
                ExecuteIncE();
                break;

            case 0x1D: //DEC E
                ExecuteDecE();
                break;

            case 0x1E: //LD E, n
                ExecuteLdEn();
                break;

            case 0x1F: //RRA
                ExecuteRra();
                break;

            case 0x20: //JR NZ, n
                ExecuteJrNZn();
                break;

            case 0x21: //LD HL, nn
                ExecuteLdHLnn();
                break;

            case 0x22: //LD (HL+), A
                ExecuteLdHLiA();
                break;

            case 0x23: //INC HL
                ExecuteIncHl();
                break;

            case 0x24: //INC H
                ExecuteIncH();
                break;

            case 0x25: //DEC H
                ExecuteDecH();
                break;

            case 0x26: //LD H, n
                ExecuteLdHn();
                break;

            case 0x27: //DAA
                ExecuteDaa();
                break;

            case 0x28: //JR Z, n
                ExecuteJrZn();
                break;

            case 0x29: //ADD HL, HL
                ExecuteAddHlhl();
                break;

            case 0x2A: //LD A, (HL+)
                ExecuteLdAhlPlus();
                break;

            case 0x2B: //DEC HL
                ExecuteDecHl();
                break;

            case 0x2C: //INC L
                ExecuteIncL();
                break;

            case 0x2D: //DEC L
                ExecuteDecL();
                break;

            case 0x2E: //LD L, n
                ExecuteLdLn();
                break;

            case 0x2F: //CPL
                ExecuteCpl();
                break;

            case 0x30: //JR NC, n
                ExecuteJrNcE();
                break;

            case 0x31: //LD SP, nn
                ExecuteLdSpNn();
                break;

            case 0x32: //LD (HL-), A
                ExecuteLdHlMinusA();
                break;

            case 0x33: //INC SP
                ExecuteIncSp();
                break;

            case 0x34: //INC (HL)
                ExecuteIncHl();
                break;

            case 0x35: //DEC (HL)
                ExecuteDecHl();
                break;

            case 0x36: //LD (HL), n
                ExecuteLdHln();
                break;

            case 0x37: //SCF
                ExecuteScf();
                break;

            case 0x38: //JR C, n
                ExecuteJrCe();
                break;

            case 0x39: //ADD HL, SP
                ExecuteAddHlsp();
                break;

            case 0x3A: //LD A, (HL-)
                ExecuteLdAhlMinus();
                break;

            case 0x3B: //DEC SP
                ExecuteDecSp();
                break;

            case 0x3C: //INC A
                ExecuteIncA();
                break;

            case 0x3D: //DEC A
                ExecuteDecA();
                break;

            case 0x3E: //LD A, n
                ExecuteLdAn();
                break;

            case 0x3F: //CCF
                ExecuteCcf();
                break;

            case 0x40: //LD B, B
                ExecuteLdBb();
                break;

            case 0x41: //LD B, C
                ExecuteLdBc();
                break;

            case 0x42: //LD B, D
                ExecuteLdBd();
                break;

            case 0x43: //LD B, E
                ExecuteLdBe();
                break;

            case 0x44: //LD B, H
                ExecuteLdBh();
                break;

            case 0x45: //LD B, L
                ExecuteLdBl();
                break;

            case 0x46: //LD B, (HL)
                ExecuteLdBhl();
                break;

            case 0x47: //LD B, A
                ExecuteLdBa();
                break;

            case 0x48: //LD C, B
                ExecuteLdCb();
                break;

            case 0x49: //LD C, C
                ExecuteLdCc();
                break;

            case 0x4A: //LD C, D
                ExecuteLdCd();
                break;

            case 0x4B: //LD C, E
                ExecuteLdCe();
                break;

            case 0x4C: //LD C, H
                ExecuteLdCh();
                break;

            case 0x4D: //LD C, L
                ExecuteLdCl();
                break;

            case 0x4E: //LD C, (HL)
                ExecuteLdChl();
                break;

            case 0x4F: //LD C, A
                ExecuteLdCa();
                break;

            case 0x50: //LD D, B
                ExecuteLdDb();
                break;

            case 0x51: //LD D, C
                ExecuteLdDc();
                break;

            case 0x52: //LD D, D
                ExecuteLdDd();
                break;

            case 0x53: //LD D, E
                ExecuteLdDe();
                break;

            case 0x54: //LD D, H
                ExecuteLdDh();
                break;

            case 0x55: //LD D, L
                ExecuteLdDl();
                break;

            case 0x56: //LD D, (HL)
                ExecuteLdDhl();
                break;

            case 0x57: //LD D, A
                ExecuteLdDa();
                break;

            case 0x58: //LD E, B
                ExecuteLdEb();
                break;

            case 0x59: //LD E, C
                ExecuteLdEc();
                break;

            case 0x5A: //LD E, D
                ExecuteLdEd();
                break;

            case 0x5B: //LD E, E
                ExecuteLdEe();
                break;

            case 0x5C: //LD E, H
                ExecuteLdEh();
                break;

            case 0x5D: //LD E, L
                ExecuteLdEl();
                break;

            case 0x5E: //LD E, (HL)
                ExecuteLdEhl();
                break;

            case 0x5F: //LD E, A
                ExecuteLdEa();
                break;

            case 0x60: //LD H, B
                ExecuteLdHb();
                break;

            case 0x61: //LD H, C
                ExecuteLdHc();
                break;

            case 0x62: //LD H, D
                ExecuteLdHd();
                break;

            case 0x63: //LD H, E
                ExecuteLdHe();
                break;

            case 0x64: //LD H, H
                ExecuteLdHh();
                break;

            case 0x65: //LD H, L
                ExecuteLdHl();
                break;

            case 0x66: //LD H, (HL)
                ExecuteLdHhl();
                break;

            case 0x67: //LD H, A
                ExecuteLdHa();
                break;

            case 0x68: //LD L, B
                ExecuteLdLb();
                break;

            case 0x69: //LD L, C
                ExecuteLdLc();
                break;

            case 0x6A: //LD L, D
                ExecuteLdLd();
                break;

            case 0x6B: //LD L, E
                ExecuteLdLe();
                break;

            case 0x6C: //LD L, H
                ExecuteLdLh();
                break;

            case 0x6D: //LD L, L
                ExecuteLdLl();
                break;

            case 0x6E: //LD L, (HL)
                ExecuteLdLhl();
                break;

            case 0x6F: //LD L, A
                ExecuteLdLa();
                break;

            case 0x70: //LD (HL), B
                ExecuteLdHlb();
                break;

            case 0x71: //LD (HL), C
                ExecuteLdHlc();
                break;

            case 0x72: //LD (HL), D
                ExecuteLdHld();
                break;

            case 0x73: //LD (HL), E
                ExecuteLdHle();
                break;

            case 0x74: //LD (HL), H
                ExecuteLdHlh();
                break;

            case 0x75: //LD (HL), L
                ExecuteLdHll();
                break;

            case 0x76: //HALT
                ExecuteHalt();
                break;

            case 0x77: //LD (HL), A
                ExecuteLdHla();
                break;

            case 0x78: //LD A, B
                ExecuteLdAb();
                break;

            case 0x79: //LD A, C
                ExecuteLdAc();
                break;

            case 0x7A: //LD A, D
                ExecuteLdAd();
                break;

            case 0x7B: //LD A, E
                ExecuteLdAe();
                break;

            case 0x7C: //LD A, H
                ExecuteLdAh();
                break;


            case 0x7D: //LD A, L
                ExecuteLdAl();
                break;

            case 0x7E: //LD A, (HL)
                ExecuteLdAhl();
                break;

            case 0x7F: //LD A, A
                ExecuteLdAa();
                break;

            case 0x80: //ADD A, B
                ExecuteAddAb();
                break;

            case 0x81: //ADD A, C
                ExecuteAddAc();
                break;

            case 0x82: //ADD A, D
                ExecuteAddAd();
                break;

            case 0x83: //ADD A, E
                ExecuteAddAe();
                break;

            case 0x84: //ADD A, H
                ExecuteAddAh();
                break;

            case 0x85: //ADD A, L
                ExecuteAddAl();
                break;

            case 0x86: //ADD A, (HL)
                ExecuteAddAhl();
                break;

            case 0x87: //ADD A, A
                ExecuteAddAa();
                break;

            case 0x88: //ADC A, B
                ExecuteAdcAb();
                break;

            case 0x89: //ADC A, C
                ExecuteAdcAc();
                break;

            case 0x8A: //ADC A, D
                ExecuteAdcAd();
                break;

            case 0x8B: //ADC A, E
                ExecuteAdcAe();
                break;

            case 0x8C: //ADC A, H
                ExecuteAdcAh();
                break;

            case 0x8D: //ADC A, L
                ExecuteAdcAl();
                break;

            case 0x8E: //ADC A, (HL)
                ExecuteAdcAhl();
                break;

            case 0x8F: //ADC A, A
                ExecuteAdcAa();
                break;

            case 0x90: //SUB A, B
                ExecuteSubB();
                break;

            case 0x91: //SUB A, C
                ExecuteSubC();
                break;

            case 0x92: //SUB A, D
                ExecuteSubD();
                break;

            case 0x93: //SUB A, E
                ExecuteSubE();
                break;

            case 0x94: //SUB A, H
                ExecuteSubH();
                break;

            case 0x95: //SUB A, L
                ExecuteSubL();
                break;

            case 0x96: //SUB A, (HL)
                ExecuteSubHl();
                break;

            case 0x97: //SUB A, A
                ExecuteSubA();
                break;

            case 0x98: //SBC A, B
                ExecuteSbcAb();
                break;

            case 0x99: //SBC A, C
                ExecuteSbcAc();
                break;

            case 0x9A: //SBC A, D
                ExecuteSbcAd();
                break;

            case 0x9B: //SBC A, E
                ExecuteSbcAe();
                break;

            case 0x9C: //SBC A, H
                ExecuteSbcAh();
                break;

            case 0x9D: //SBC A, L
                ExecuteSbcAl();
                break;

            case 0x9E: //SBC A, (HL)
                ExecuteSubAhl();
                break;

            case 0x9F: //SBC A, A
                ExecuteSbcAa();
                break;

            case 0xA0: //AND A, B
                ExecuteAndB();
                break;

            case 0xA1: //AND A, C
                ExecuteAndC();
                break;

            case 0xA2: //AND A, D
                ExecuteAndD();
                break;

            case 0xA3: //AND A, E
                ExecuteAndE();
                break;

            case 0xA4: //AND A, H
                ExecuteAndH();
                break;

            case 0xA5: //AND A, L
                ExecuteAndL();
                break;

            case 0xA6: //AND A, (HL)
                ExecuteAndHl();
                break;

            case 0xA7: //AND A, A
                ExecuteAndA();
                break;

            case 0xA8: //XOR A, B
                ExecuteXorB();
                break;

            case 0xA9: //XOR A, C
                ExecuteXorC();
                break;

            case 0xAA: //XOR A, D
                ExecuteXorD();
                break;

            case 0xAB: //XOR A, E
                ExecuteXorE();
                break;

            case 0xAC: //XOR A, H
                ExecuteXorH();
                break;

            case 0xAD: //XOR A, L
                ExecuteXorL();
                break;

            case 0xAE: //XOR A, (HL)
                ExecuteXorHl();
                break;

            case 0xAF: //XOR A, A
                ExecuteXorA();
                break;

            case 0xB0: //OR A, B
                ExecuteOrB();
                break;

            case 0xB1: //OR A, C
                ExecuteOrC();
                break;

            case 0xB2: //OR A, D
                ExecuteOrD();
                break;

            case 0xB3: //OR A, E
                ExecuteOrE();
                break;

            case 0xB4: //OR A, H
                ExecuteOrH();
                break;

            case 0xB5: //OR A, L
                ExecuteOrL();
                break;

            case 0xB6: //OR A, (HL)
                ExecuteOrHl();
                break;

            case 0xB7: //OR A, A
                ExecuteOrA();
                break;

            case 0xB8: //CP A, B
                ExecuteCpB();
                break;

            case 0xB9: //CP A, C
                ExecuteCpC();
                break;

            case 0xBA: //CP A, D
                ExecuteCpD();
                break;

            case 0xBB: //CP A, E
                ExecuteCpE();
                break;

            case 0xBC: //CP A, H
                ExecuteCpH();
                break;

            case 0xBD: //CP A, L
                ExecuteCpL();
                break;

            case 0xBE: //CP A, (HL)
                ExecuteCpHl();
                break;

            case 0xBF: //CP A, A
                ExecuteCpA();
                break;

            case 0xC0: //RET NZ
                ExecuteRetNz();
                break;

            case 0xC1: //POP BC
                ExecutePopBc();
                break;

            case 0xC2: //JP NZ, nn
                ExecuteJpNZnn();
                break;

            case 0xC3: //JP nn
                ExecuteJpnn();
                break;

            case 0xC4: //CALL NZ, nn
                ExecuteCallNZnn();
                break;

            case 0xC5: //PUSH BC
                ExecutePushBc();
                break;

            case 0xC6: //ADD A, n
                ExecuteAddAn();
                break;

            case 0xC7: //RST 00H
                ExecuteRst0();
                break;

            case 0xC8: //RET Z
                ExecuteRetZ();
                break;

            case 0xC9: //RET
                ExecuteRet();
                break;

            case 0xCA: //JP Z, nn
                ExecuteJpZnn();
                break;

            case 0xCB: //CB Prefix
                ExecutePrefixCb();
                break;

            case 0xCC: //CALL Z, nn
                ExecuteCallZnn();
                break;

            case 0xCD: //CALL nn
                ExecuteCallnn();
                break;

            case 0xCE: //ADC A, n
                ExecuteAdcAn();
                break;

            case 0xCF: //RST 08H
                ExecuteRst8();
                break;

            case 0xD0: //RET NC
                ExecuteRetNc();
                break;

            case 0xD1: //POP DE
                ExecutePopDe();
                break;

            case 0xD2: //JP NC, nn
                ExecuteJpNCnn();
                break;

            case 0xD3: //XX - Could be used for internal debugging.
                ExecuteOutnA();
                break;

            case 0xD4: //CALL NC, nn
                ExecuteCallNCnn();
                break;

            case 0xD5: //PUSH DE
                ExecutePushDe();
                break;

            case 0xD6: //SUB n
                ExecuteSubn();
                break;

            case 0xD7: //RST 10H
                ExecuteRst10();
                break;

            case 0xD8: //RET C
                ExecuteRetC();
                break;

            case 0xD9: //RETI
                ExecuteRetI();
                break;

            case 0xDA: //JP C, nn
                ExecuteJpCnn();
                break;

            case 0xDC: //CALL C, nn
                ExecuteCallCnn();
                break;

            case 0xDD: //XX
                ExecuteNop();
                break;

            case 0xDE: //SBC A, n
                ExecuteSbcAn();
                break;

            case 0xDF: //RST 18H
                ExecuteRst18();
                break;

            case 0xE0: //LDH (a8), A
                ExecuteLdhAn();
                break;

            case 0xE1: //POP HL
                ExecutePopHl();
                break;

            case 0xE2: //LD (C), A
                ExecuteLdCa();
                break;

            case 0xE3: //XX
                ExecuteNop();
                break;

            case 0xE4: //XX
                ExecuteNop();
                break;

            case 0xE5: //PUSH HL
                ExecutePushHl();
                break;

            case 0xE6: //AND n
                ExecuteAndN();
                break;

            case 0xE7: //RST 20H
                ExecuteRst20();
                break;
            case 0xE8: //ADD SP, r8
                ExecuteAddSPn();
                break;

            case 0xE9: //JP (HL)
                ExecuteJpHl();
                break;

            case 0xEA: //LD (nn), A
                ExecuteLdnnA();
                break;

            case 0xEB: // XX
                ExecuteNop();
                break;

            case 0xEC: // XX
                ExecuteNop();
                break;

            case 0xED: // XX
                ExecuteNop();
                break;

            case 0xEE: //XOR A, n
                ExecuteXorn();
                break;

            case 0xEF: //RST 28
                ExecuteRst28();
                break;

            case 0xF0: //LDH (a8), A
                ExecuteLdHAa8();
                break;

            case 0xF1: //POP AF
                ExecutePopAf();
                break;

            case 0xF2: //LD A, (C)
                ExecuteLdAc();
                break;

            case 0xF3: //DI
                ExecuteDi();
                break;

            case 0xF4: // XX
                ExecuteNop();
                break;

            case 0xF5: //PUSH BC
                ExecutePushAf();
                break;

            case 0xF6: //OR A, n
                ExecuteOrn();
                break;

            case 0xF7: //RST 30
                ExecuteRst30();
                break;

            case 0xF8: //LD HL, SP+r8
                ExecuteLdHlsPn();
                break;

            case 0xF9: //LD SP, HL
                ExecuteLdSphl();
                break;

            case 0xFA: //LD A, (nn)
                ExecuteLdAnn();
                break;

            case 0xFB: //EI
                ExecuteEi();
                break;

            case 0xFC: // XX
                ExecuteNop();
                break;

            case 0xFD: // XX
                ExecuteNop();
                break;

            case 0xFE: //CP n
                ExecuteCpn();
                break;

            case 0xFF: //RST 38
                ExecuteRst38();
                break;
        }
    }


    // 0x00 - NOP - No Operation.
    private void ExecuteNop()
    {
        ProgramCounter++;
    }

    // 0x01 - LD BC, nn - Load 16-bit immediate value into BC.
    private void ExecuteLdBCnn()
    {
        C = Memory[ProgramCounter + 1];
        B = Memory[ProgramCounter + 2];
        ProgramCounter += 3;
    }

    // 0x02 - LD (BC), A - Store A register into address pointed by BC.
    private void ExecuteLdBca()
    {
        var x = (ushort)((B << 8) | C);
        Memory[x] = A;
        ProgramCounter++;
    }

    // 0x03 - INC BC - Increment BC.
    private void ExecuteIncBc()
    {
        var bc = (ushort)((B << 8) | C);
        bc++;
        B = (byte)((bc >> 8) & 0xFF);
        C = (byte)(bc & 0xFF);
        ProgramCounter++;
    }

    // 0x04 - INC B - Increment B register.
    private void ExecuteIncB()
    {
        B++;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = (B & 0x0F) == 0;
        Parity = (B & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x05 - DEC B - Decrement B register.
    private void ExecuteDecB()
    {
        B--;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = (B & 0x0F) == 0x0F;
        Parity = (B & 0x01) == 1;
        ProgramCounter++;
    }

    // 0x06 - LD B, n - Load 8-bit immediate value into B.
    private void ExecuteLdBn()
    {
        B = Memory[ProgramCounter + 1];
        ProgramCounter += 2;
    }

    // 0x07 - RLCA - Rotate A register left with carry.
    private void ExecuteRlca()
    {
        var carryOut = (byte)(A >> 7);
        A = (byte)((A << 1) | carryOut);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = carryOut == 0x01;
        Parity = false;
        ProgramCounter++;
    }

    // 0x08 - LD (nn), SP - Store stack pointer at address pointed by 16-bit immediate value.
    private void ExecuteLdnnSp()
    {
        var address = (ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]);
        Memory[address] = (byte)(StackPointer & 0xFF);
        Memory[address + 1] = (byte)((StackPointer >> 8) & 0xFF);
        ProgramCounter += 3;
    }

    // 0x09 - ADD HL, BC - Add BC to HL.
    private void ExecuteAddHlbc()
    {
        var result = (ushort)((H << 8) | L);
        var bc = (ushort)((B << 8) | C);
        result += bc;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (bc & 0xFFF);
        Carry = result < bc;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x0C - INC C - Increment C register.
    private void ExecuteIncC()
    {
        C++;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = (C & 0x0F) == 0;
        Parity = (C & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x0F - RRCA - Rotate A register right with carry.
    private void ExecuteRrca()
    {
        var carryOut = (byte)(A & 0x01);
        A = (byte)((A >> 1) | (carryOut << 7));
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = carryOut == 0x01;
        Parity = false;
        ProgramCounter++;
    }


    // 0x0A - LD A, (BC) - Load A with value pointed by BC.
    private void ExecuteLdAbc()
    {
        A = Memory[(B << 8) | C];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x0B - DEC BC - Decrement BC.
    private void ExecuteDecBc()
    {
        var bc = (ushort)((B << 8) | C);
        bc--;
        B = (byte)((bc >> 8) & 0xFF);
        C = (byte)(bc & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (B & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x0D - DEC C - Decrement C.
    private void ExecuteDecC()
    {
        C--;
        Sign = (C >> 7) == 1;
        Zero = C == 0;
        HalfCarry = (C & 0xF) == 0;
        Carry = false;
        Parity = (C & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x0E - LD C, n - Load C with 8-bit immediate value.
    private void ExecuteLdCn()
    {
        C = Memory[ProgramCounter + 1];
        Sign = (C >> 7) == 1;
        Zero = C == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (C & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0x10 - STOP 0 - Stop CPU until button pressed.
    private void ExecuteStop()
    {
        // Stop CPU until button pressed.
        ProgramCounter++;
    }

    // 0x11 - LD DE, nn - Load DE with 16-bit immediate value.
    private void ExecuteLdDEnn()
    {
        D = Memory[ProgramCounter + 2];
        E = Memory[ProgramCounter + 1];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter += 3;
    }

    // 0x12 - LD (DE), A - Load A to address pointed by DE.
    private void ExecuteLdDea()
    {
        Memory[(D << 8) | E] = A;
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x13 - INC DE - Increment DE.
    private void ExecuteIncDe()
    {
        var de = (ushort)((D << 8) | E);
        de++;
        D = (byte)((de >> 8) & 0xFF);
        E = (byte)(de & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x14 - INC D - Increment D.
    private void ExecuteIncD()
    {
        D++;
        Sign = (D >> 7) == 1;
        Zero = D == 0;
        HalfCarry = (D & 0xF) == 0;
        Carry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x15 - DEC D - Decrement D.
    private void ExecuteDecD()
    {
        D--;
        Sign = (D >> 7) == 1;
        Zero = D == 0;
        HalfCarry = (D & 0xF) == 0;
        Carry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x16 - LD D, n - Load D with 8-bit immediate value.
    private void ExecuteLdDn()
    {
        D = Memory[ProgramCounter + 1];
        Sign = (D >> 7) == 1;
        Zero = D == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0x17 - RLA - Rotate A left through carry.
    private void ExecuteRla()
    {
        var carry = (byte)(A >> 7);
        A = (byte)((A << 1) | (Carry ? 1 : 0));
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = (carry == 1);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x18 - JR n - Relative jump by signed immediate byte.
    private void ExecuteJrn()
    {
        var n = (sbyte)Memory[ProgramCounter + 1];
        ProgramCounter += (ushort)(n + 2);
    }

    // 0x19 - ADD HL, DE - Add DE to HL.
    private void ExecuteAddHlde()
    {
        var result = (ushort)((H << 8) | L);
        var de = (ushort)((D << 8) | E);
        result += de;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (de & 0xFFF);
        Carry = result < de;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x1A - LD A, (DE) - Load A with value pointed by DE.
    private void ExecuteLdAde()
    {
        A = Memory[(D << 8) | E];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x1B - DEC DE - Decrement DE.
    private void ExecuteDecDe()
    {
        var de = (ushort)((D << 8) | E);
        de--;
        D = (byte)((de >> 8) & 0xFF);
        E = (byte)(de & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x1C - INC E - Increment E.
    private void ExecuteIncE()
    {
        E++;
        Sign = (E >> 7) == 1;
        Zero = E == 0;
        HalfCarry = (E & 0xF) == 0;
        Carry = false;
        Parity = (E & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x1D - DEC E - Decrement E.
    private void ExecuteDecE()
    {
        E--;
        Sign = (E >> 7) == 1;
        Zero = E == 0;
        HalfCarry = (E & 0xF) == 0;
        Carry = false;
        Parity = (E & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x1E - LD E, n - Load E with 8-bit immediate value.
    private void ExecuteLdEn()
    {
        E = Memory[ProgramCounter + 1];
        Sign = (E >> 7) == 1;
        Zero = E == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (E & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0x1F - RRA - Rotate A right through carry.
    private void ExecuteRra()
    {
        var carry = (byte)(A & 0x01);
        A = (byte)((A >> 1) | (Carry ? 0x80 : 0x00));
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = (carry == 1);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x20 - JR NZ, n - Relative jump by signed immediate byte if Zero flag is not set.
    private void ExecuteJrNZn()
    {
        if (!Zero)
        {
            var n = (sbyte)Memory[ProgramCounter + 1];
            ProgramCounter += (ushort)(n + 2);
        }
        else
        {
            ProgramCounter += 2;
        }
    }

    // 0x21 - LD HL, nn - Load HL with 16-bit immediate value.
    private void ExecuteLdHLnn()
    {
        H = Memory[ProgramCounter + 2];
        L = Memory[ProgramCounter + 1];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter += 3;
    }

    // 0x22 - LD (HL+), A - Load A to address pointed by HL, then increment HL.
    private void ExecuteLdHLiA()

    {
        var hl = (ushort)((H << 8) | L);
        Memory[hl] = A;
        hl++;
        H = (byte)((hl >> 8) & 0xFF);
        L = (byte)(hl & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x23 - INC HL - Increment HL.
    private void ExecuteIncHl()
    {
        var hl = (ushort)((H << 8) | L);
        hl++;
        H = (byte)((hl >> 8) & 0xFF);
        L = (byte)(hl & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x24 - INC H - Increment H.
    private void ExecuteIncH()
    {
        H++;
        Sign = (H >> 7) == 1;
        Zero = H == 0;
        HalfCarry = (H & 0xF) == 0;
        Carry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x25 - DEC H - Decrement H.
    private void ExecuteDecH()
    {
        H--;
        Sign = (H >> 7) == 1;
        Zero = H == 0;
        HalfCarry = (H & 0xF) == 0;
        Carry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x26 - LD H, n - Load H with 8-bit immediate value.
    private void ExecuteLdHn()
    {
        H = Memory[ProgramCounter + 1];
        Sign = (H >> 7) == 1;
        Zero = H == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0x27 - DAA - Decimal adjust register A.
    private void ExecuteDaa()
    {
        if ((A & 0xF) > 9 || HalfCarry)
        {
            A += 0x06;
            HalfCarry = true;
        }

        if (A > 0x9F || Carry)
        {
            A += 0x60;
            Carry = true;
        }

        Sign = (A >> 7) == 1;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x28 - JR Z, n - Relative jump by signed immediate byte if Zero flag is set.
    private void ExecuteJrZn()
    {
        if (Zero)
        {
            var n = (sbyte)Memory[ProgramCounter + 1];
            ProgramCounter += (ushort)(n + 2);
        }
        else
        {
            ProgramCounter += 2;
        }
    }


    // 0x29 - ADD HL, HL - Add HL to HL.
    private void ExecuteAddHlhl()
    {
        var result = (ushort)((H << 8) | L);
        var hl = (ushort)((H << 8) | L);
        result += hl;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (hl & 0xFFF);
        Carry = result < hl;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x2A - LD A, (HL+) - Load A from memory address HL then increment HL.
    private void ExecuteLdAhlPlus()
    {
        var address = (ushort)((H << 8) | L);
        A = Memory[address];
        var result = (ushort)(address + 1);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        ProgramCounter++;
    }

    // 0x2B - DEC HL - Decrement HL.
    private void ExecuteDecHl()
    {
        var result = (ushort)((H << 8) | L);
        result--;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        ProgramCounter++;
    }

    // 0x2C - INC L - Increment L.
    private void ExecuteIncL()
    {
        L++;
        Sign = (L & 0x80) != 0;
        Zero = (L == 0);
        HalfCarry = (L & 0x0F) == 0;
        Parity = (L & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x2D - DEC L - Decrement L.
    private void ExecuteDecL()
    {
        L--;
        Sign = (L & 0x80) != 0;
        Zero = (L == 0);
        HalfCarry = (L & 0x0F) == 0;
        Parity = (L & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x2E - LD L, n - Load 8-bit immediate into L.
    private void ExecuteLdLn()
    {
        L = Memory[ProgramCounter + 1];
        Sign = (L & 0x80) != 0;
        Zero = (L == 0);
        HalfCarry = (L & 0x0F) == 0;
        Parity = (L & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0x2F - CPL - Complement A register.
    private void ExecuteCpl()
    {
        A = (byte)~A;
        Sign = true;
        HalfCarry = true;
        ProgramCounter++;
    }

    // 0x30 - JR NC, e - Jump relative if Carry flag is not set.
    private void ExecuteJrNcE()
    {
        if (!Carry)
        {
            var offset = (sbyte)Memory[ProgramCounter + 1];
            ProgramCounter += (ushort)(offset + 2);
        }
        else
        {
            ProgramCounter += 2;
        }
    }

    // 0x31 - LD SP, nn - Load 16-bit immediate into Stack Pointer.
    private void ExecuteLdSpNn()
    {
        var value = (ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]);
        StackPointer = value;
        ProgramCounter += 3;
    }

    // 0x32 - LD (HL-), A - Store A at address HL then decrement HL.
    private void ExecuteLdHlMinusA()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = A;
        var result = (ushort)(address - 1);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        ProgramCounter++;
    }

    // 0x33 - INC SP - Increment Stack Pointer.
    private void ExecuteIncSp()
    {
        StackPointer++;
        ProgramCounter++;
    }


    // 0x36 - LD (HL), n - Load 8-bit immediate into address HL.
    private void ExecuteLdHln()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = Memory[ProgramCounter + 1];
        ProgramCounter += 2;
    }

    // 0x37 - SCF - Set Carry flag.
    private void ExecuteScf()
    {
        Carry = true;
        Sign = false;
        HalfCarry = false;
        ProgramCounter++;
    }

    // 0x38 - JR C, e - Jump relative if Carry flag is set.
    private void ExecuteJrCe()
    {
        if (Carry)
        {
            var offset = (sbyte)Memory[ProgramCounter + 1];
            ProgramCounter += (ushort)(offset + 2);
        }
        else
        {
            ProgramCounter += 2;
        }
    }

    // 0x39 - ADD HL, SP - Add Stack Pointer to HL.
    private void ExecuteAddHlsp()
    {
        var result = (ushort)((H << 8) | L);
        result += StackPointer;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (StackPointer & 0xFFF);
        Carry = result < StackPointer;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x3A - LD A, (HL-) - Load A from memory address HL then decrement HL.
    private void ExecuteLdAhlMinus()
    {
        var address = (ushort)((H << 8) | L);
        A = Memory[address];
        var result = (ushort)(address - 1);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        ProgramCounter++;
    }

    // 0x3B - DEC SP - Decrement Stack Pointer.
    private void ExecuteDecSp()
    {
        StackPointer--;
        ProgramCounter++;
    }

    // 0x3C - INC A - Increment A.
    private void ExecuteIncA()
    {
        A++;
        Sign = (A & 0x80) != 0;
        Zero = (A == 0);
        HalfCarry = (A & 0x0F) == 0;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x3D - DEC A - Decrement A.
    private void ExecuteDecA()
    {
        A--;
        Sign = (A & 0x80) != 0;
        Zero = (A == 0);
        HalfCarry = (A & 0x0F) == 0;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x3E - LD A, n - Load 8-bit immediate into A.
    private void ExecuteLdAn()
    {
        A = Memory[ProgramCounter + 1];
        Sign = (A & 0x80) != 0;
        Zero = (A == 0);
        HalfCarry = (A & 0x0F) == 0;
        Parity = (A & 0x01) == 0;
        ProgramCounter += 2;
    }


    // 0x3F - CCF - Complement carry flag.
    private void ExecuteCcf()
    {
        Carry = !Carry;
        Sign = false;
        HalfCarry = false;
        Zero = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x40 - LD B, B - Load B with B.
    private void ExecuteLdBb()
    {
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x41 - LD B, C - Load B with C.
    private void ExecuteLdBc()
    {
        B = C;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x42 - LD B, D - Load B with D.
    private void ExecuteLdBd()
    {
        B = D;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x43 - LD B, E - Load B with E.
    private void ExecuteLdBe()
    {
        B = E;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x44 - LD B, H - Load B with H.
    private void ExecuteLdBh()
    {
        B = H;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x45 - LD B, L - Load B with L.
    private void ExecuteLdBl()
    {
        B = L;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x46 - LD B, (HL) - Load B with value pointed by HL.
    private void ExecuteLdBhl()
    {
        var hl = (ushort)((H << 8) | L);
        B = Memory[hl];
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x47 - LD B, A - Load B with A.
    private void ExecuteLdBa()
    {
        B = A;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x48 - LD C, B - Load C with B.
    private void ExecuteLdCb()
    {
        C = B;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x49 - LD C, C - Load C with C.
    private void ExecuteLdCc()
    {
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x4A - LD C, D - Load C with D.
    private void ExecuteLdCd()
    {
        C = D;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x4B - LD C, E - Load C with E.
    private void ExecuteLdCe()
    {
        C = E;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x4C - LD C, H - Load C with H.
    private void ExecuteLdCh()
    {
        C = H;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x4D - LD C, L - Load C with L.
    private void ExecuteLdCl()
    {
        C = L;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x4E - LD C, (HL) - Load C with value pointed by HL.
    private void ExecuteLdChl()
    {
        var hl = (ushort)((H << 8) | L);
        C = Memory[hl];
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x4F - LD C, A - Load C with A.
    private void ExecuteLdCa()
    {
        C = A;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x50 - LD D, B - Load D with B.
    private void ExecuteLdDb()
    {
        D = B;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x51 - LD D, C - Load D with C.
    private void ExecuteLdDc()
    {
        D = C;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x52 - LD D, D - Load D with D.
    private void ExecuteLdDd()
    {
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x53 - LD D, E - Load D with E.
    private void ExecuteLdDe()
    {
        D = E;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x54 - LD D, H - Load D with H.
    private void ExecuteLdDh()
    {
        D = H;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x55 - LD D, L - Load D with L.
    private void ExecuteLdDl()
    {
        D = L;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x56 - LD D, (HL) - Load D with value pointed by HL.
    private void ExecuteLdDhl()
    {
        var hl = (ushort)((H << 8) | L);
        D = Memory[hl];
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x57 - LD D, A - Load D with A.
    private void ExecuteLdDa()
    {
        D = A;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x58 - LD E, B - Load E with B.
    private void ExecuteLdEb()
    {
        E = B;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x59 - LD E, C - Load E with C.
    private void ExecuteLdEc()
    {
        E = C;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x5A - LD E, D - Load E with D.
    private void ExecuteLdEd()
    {
        E = D;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x5B - LD E, E - Load E with E.
    private void ExecuteLdEe()
    {
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x5C - LD E, H - Load E with H.
    private void ExecuteLdEh()
    {
        E = H;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x5D - LD E, L - Load E with L.
    private void ExecuteLdEl()
    {
        E = L;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x5E - LD E, (HL) - Load E with value pointed by HL.
    private void ExecuteLdEhl()
    {
        var hl = (ushort)((H << 8) | L);
        E = Memory[hl];
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x5F - LD E, A - Load A into E
    private void ExecuteLdEa()
    {
        E = A;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x60 - LD H, B - Load B into H
    private void ExecuteLdHb()
    {
        H = B;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (B & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x61 - LD H, C - Load C into H
    private void ExecuteLdHc()
    {
        H = C;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (C & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x62 - LD H, D - Load D into H
    private void ExecuteLdHd()
    {
        H = D;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x63 - LD H, E - Load E into H
    private void ExecuteLdHe()
    {
        H = E;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (E & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x64 - LD H, H - Load H into H
    private void ExecuteLdHh()
    {
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x65 - LD H, L - Load L into H
    private void ExecuteLdHl()
    {
        H = L;
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x66 - LD H, (HL) - Load (HL) into H
    private void ExecuteLdHhl()
    {
        var address = (ushort)((H << 8) | L);
        H = Memory[address];
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x67 - LD H, A - Load A into H
    private void ExecuteLdHa()
    {
        H = A;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x68 - LD L, B - Load B into L
    private void ExecuteLdLb()
    {
        L = B;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (B & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x69 - LD L, C - Load C into L
    private void ExecuteLdLc()
    {
        L = C;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (C & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x6A - LD L, D - Load D into L
    private void ExecuteLdLd()
    {
        L = D;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x6B - LD L, E - Load E into L
    private void ExecuteLdLe()
    {
        L = E;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (E & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x6C - LD L, H - Load H into L
    private void ExecuteLdLh()
    {
        L = H;
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x6D - LD L, L - Load L into L
    private void ExecuteLdLl()
    {
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x6E - LD L, (HL) - Load (HL) into L
    private void ExecuteLdLhl()
    {
        var address = (ushort)((H << 8) | L);
        L = Memory[address];
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x6F - LD L, A - Load A into L
    private void ExecuteLdLa()
    {
        L = A;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x70 - LD (HL), B - Load B into (HL)
    private void ExecuteLdHlb()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = B;
        ProgramCounter++;
    }

    // 0x71 - LD (HL), C - Load C into (HL)
    private void ExecuteLdHlc()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = C;
        ProgramCounter++;
    }

    // 0x72 - LD (HL), D - Load D into (HL)
    private void ExecuteLdHld()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = D;
        ProgramCounter++;
    }

    // 0x73 - LD (HL), E - Load E into (HL)
    private void ExecuteLdHle()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = E;
        ProgramCounter++;
    }

    // 0x74 - LD (HL), H - Load H into (HL)
    private void ExecuteLdHlh()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = H;
        ProgramCounter++;
    }

    // 0x75 - LD (HL), L - Load L into (HL)
    private void ExecuteLdHll()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = L;
        ProgramCounter++;
    }

    // 0x76 - HALT - Halts the processor until an interrupt occurs.
    private void ExecuteHalt()
    {
        Halt = true;
        //Do nothing
        ProgramCounter++;
    }

    // 0x77 - LD (HL), A - Load A into (HL)
    private void ExecuteLdHla()
    {
        var address = (ushort)((H << 8) | L);
        Memory[address] = A;
        ProgramCounter++;
    }

    // 0x78 - LD A, B - Load B into A
    private void ExecuteLdAb()
    {
        A = B;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (B & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x79 - LD A, C - Load C into A
    private void ExecuteLdAc()
    {
        A = C;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (C & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x7A - LD A, D - Load D into A
    private void ExecuteLdAd()
    {
        A = D;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (D & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x7B - LD A, E - Load E into A
    private void ExecuteLdAe()
    {
        A = E;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (E & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x7C - LD A, H - Load H into A
    private void ExecuteLdAh()
    {
        A = H;
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x7D - LD A, L - Load L into A
    private void ExecuteLdAl()
    {
        A = L;
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x7E - LD A, (HL) - Load (HL)
    private void ExecuteLdAhl()
    {
        var address = (ushort)((H << 8) | L);
        A = Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x7F - LD A, A - Load A into A
    private void ExecuteLdAa()
    {
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x80 - ADD A, B - Add B to A
    private void ExecuteAddAb()
    {
        var result = (byte)(A + B);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (B & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x81 - ADD A, C - Add C to A
    private void ExecuteAddAc()
    {
        var result = (byte)(A + C);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (C & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x82 - ADD A, D - Add D to A
    private void ExecuteAddAd()
    {
        var result = (byte)(A + D);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (D & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x83 - ADD A, E - Add E to A
    private void ExecuteAddAe()
    {
        var result = (byte)(A + E);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (E & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x84 - ADD A, H - Add H to A
    private void ExecuteAddAh()
    {
        var result = (byte)(A + H);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (H & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x85 - ADD A, L - Add L to A.
    private void ExecuteAddAl()
    {
        var result = (byte)(A + L);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (L & 0xF) > 0xF;
        Carry = (A + L) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x86 - ADD A, (HL) - Add (HL) to A.
    private void ExecuteAddAhl()
    {
        var result = (byte)(A + Memory[(H << 8) | L]);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[(H << 8) | L] & 0xF) > 0xF;
        Carry = (A + Memory[(H << 8) | L]) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x87 - ADD A, A - Add A to A.
    private void ExecuteAddAa()
    {
        var result = (byte)(A + A);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (A & 0xF) > 0xF;
        Carry = (A + A) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x88 - ADC A, B - Add B + Carry flag to A.
    private void ExecuteAdcAb()
    {
        var result = (byte)(A + B + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (B & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + B + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x89 - ADC A, C - Add C + Carry flag to A.
    private void ExecuteAdcAc()
    {
        var result = (byte)(A + C + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (C & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + C + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x8A - ADC A, D - Add D + Carry flag to A.
    private void ExecuteAdcAd()
    {
        var result = (byte)(A + D + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (D & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + D + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x8B - ADC A, E - Add E + Carry flag to A.
    private void ExecuteAdcAe()
    {
        var result = (byte)(A + E + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (E & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + E + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x8C - ADC A, H - Add H + Carry flag to A.
    private void ExecuteAdcAh()
    {
        var result = (byte)(A + H + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (H & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + H + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x8D - ADC A, L - Add L + Carry flag to A.
    private void ExecuteAdcAl()
    {
        var result = (byte)(A + L + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (L & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + L + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x8E - ADC A, (HL) - Add (HL) + Carry flag to A.
    private void ExecuteAdcAhl()
    {
        var result = (byte)(A + Memory[(H << 8) | L] + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[(H << 8) | L] & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + Memory[(H << 8) | L] + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x8F - ADC A, A - Add A + Carry flag to A.
    private void ExecuteAdcAa()
    {
        var result = (byte)(A + A + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (A & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + A + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x90 - SUB B - Subtract B from A.
    private void ExecuteSubB()
    {
        var result = (byte)(A - B);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (B & 0xF);
        Carry = A < B;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x91 - SUB C - Subtract C from A.
    private void ExecuteSubC()
    {
        var result = (byte)(A - C);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (C & 0xF);
        Carry = A < C;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x92 - SUB D - Subtract D from A.
    private void ExecuteSubD()
    {
        var result = (byte)(A - D);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (D & 0xF);
        Carry = A < D;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x93 - SUB E - Subtract E from A.
    private void ExecuteSubE()
    {
        var result = (byte)(A - E);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (E & 0xF);
        Carry = A < E;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x94 - SUB H - Subtract H from A.
    private void ExecuteSubH()
    {
        var result = (byte)(A - H);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (H & 0xF);
        Carry = A < H;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x95 - SUB L - Subtract L from A.
    private void ExecuteSubL()
    {
        var result = (byte)(A - L);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (L & 0xF);
        Carry = A < L;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x96 - SUB (HL) - Subtract (HL) from A.
    private void ExecuteSubHl()
    {
        var result = (byte)(A - Memory[(H << 8) | L]);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (Memory[(H << 8) | L] & 0xF);
        Carry = A < Memory[(H << 8) | L];
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x97 - SUB A - Subtract A from A.
    private void ExecuteSubA()
    {
        var result = (byte)(A - A);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x98 - SBC A, B - Subtract B + Carry flag from A.
    private void ExecuteSbcAb()
    {
        var result = (byte)(A - B - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (B & 0xF) - (Carry ? 1 : 0);
        Carry = A < B - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x99 - SBC A, C - Subtract C + Carry flag from A.
    private void ExecuteSbcAc()
    {
        var result = (byte)(A - C - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (C & 0xF) - (Carry ? 1 : 0);
        Carry = A < C - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x9A - SBC A, D - Subtract D + Carry flag from A.
    private void ExecuteSbcAd()
    {
        var result = (byte)(A - D - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (D & 0xF) - (Carry ? 1 : 0);
        Carry = A < D - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x9B - SBC A, E - Subtract E + Carry flag from A.
    private void ExecuteSbcAe()
    {
        var result = (byte)(A - E - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (E & 0xF) - (Carry ? 1 : 0);
        Carry = A < E - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x9C - SBC A, H - Subtract H + Carry flag from A.
    private void ExecuteSbcAh()
    {
        var result = (byte)(A - H - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (H & 0xF) + (Carry ? 1 : 0);
        Carry = (A & 0xFF) < (H & 0xFF) + (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x9D - SBC A, L - Subtract L + Carry flag from A.
    private void ExecuteSbcAl()
    {
        var result = (byte)(A - L - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (L & 0xF) + (Carry ? 1 : 0);
        Carry = (A & 0xFF) < (L & 0xFF) + (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }


    // 0x9E - SUB A, (HL) - Subtract (HL) from A.
    private void ExecuteSubAhl()
    {
        var val = Memory[(H << 8) | L];
        A = (byte)(A - val);
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = (A & 0x0F) > (val & 0x0F);
        Carry = A < val;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0x9F - SBC A, A - Subtract A and carry flag from A.
    private void ExecuteSbcAa()
    {
        var val = A;
        A = (byte)(A - val - (Carry ? 1 : 0));
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = (A & 0x0F) > (val & 0x0F);
        Carry = A < val;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }


    // 0xA0 - AND B - Logical AND B with A.
    private void ExecuteAndB()
    {
        A &= B;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA1 - AND C - Logical AND C with A.
    private void ExecuteAndC()
    {
        A &= C;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA2 - AND D - Logical AND D with A.
    private void ExecuteAndD()
    {
        A &= D;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA3 - AND E - Logical AND E with A.
    private void ExecuteAndE()
    {
        A &= E;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA4 - AND H - Logical AND H with A.
    private void ExecuteAndH()
    {
        A &= H;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA5 - AND L - Logical AND L with A.
    private void ExecuteAndL()
    {
        A &= L;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA6 - AND (HL) - Logical AND (HL) with A.
    private void ExecuteAndHl()
    {
        var address = (byte)((H << 8) | L);
        A &= Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA7 - AND A - Logical AND A with A.
    private void ExecuteAndA()
    {
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA8 - XOR B - Logical XOR B with A.
    private void ExecuteXorB()
    {
        A ^= B;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xA9 - XOR C - Logical XOR C with A.
    private void ExecuteXorC()
    {
        A ^= C;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xAA - XOR D - Logical XOR D with A.
    private void ExecuteXorD()
    {
        A ^= D;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xAB - XOR E - Logical XOR E with A.
    private void ExecuteXorE()
    {
        A ^= E;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xAC - XOR H - Logical XOR H with A.
    private void ExecuteXorH()
    {
        A ^= H;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xAD - XOR L - Logical XOR L with A.
    private void ExecuteXorL()
    {
        A ^= L;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xAE - XOR (HL) - Logical XOR (HL) with A.
    private void ExecuteXorHl()
    {
        var address = (byte)((H << 8) | L);
        A ^= Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xAF - XOR A - Logical XOR A with A.
    private void ExecuteXorA()
    {
        A = 0;
        Sign = false;
        Zero = true;
        HalfCarry = false;
        Carry = false;
        Parity = true;
        ProgramCounter++;
    }

    // 0xB0 - OR B - Logical OR B with A.
    private void ExecuteOrB()
    {
        A |= B;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB1 - OR C - Logical OR C with A.
    private void ExecuteOrC()
    {
        A |= C;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB2 - OR D - Logical OR D with A.
    private void ExecuteOrD()
    {
        A |= D;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB3 - OR E - Logical OR E with A.
    private void ExecuteOrE()
    {
        A |= E;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB4 - OR H - Logical OR H with A.
    private void ExecuteOrH()
    {
        A |= H;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB5 - OR L - Logical OR L with A.
    private void ExecuteOrL()
    {
        A |= L;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB6 - OR (HL) - Logical OR (HL) with A.
    private void ExecuteOrHl()
    {
        var address = (byte)((H << 8) | L);
        A |= Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB7 - OR A - Logical OR A with A.
    private void ExecuteOrA()
    {
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB8 - CP B - Compare B with A.
    private void ExecuteCpB()
    {
        var result = (byte)(A - B);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (B & 0xF);
        Carry = (A & 0xFF) < (B & 0xFF);
        Parity = (result & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xB9 - CP C - Compare C with A.
    private void ExecuteCpC()
    {
        var result = (byte)(A - C);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (C & 0xF);
        Carry = (A & 0xFF) < (C & 0xFF);
        Parity = (result & 0x01) == 0;
        ProgramCounter++;
    }

    // 0xBA - CP D - Compare D with A.
    private void ExecuteCpD()
    {
        var result = (byte)(A - D);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (D & 0xF);
        Carry = (A & 0xFF) < (D & 0xFF);
        Parity = (result & 0x01) == 0;
        ProgramCounter++;
    }


    // 0xBB - CP E - Compare E with A.
    private void ExecuteCpE()
    {
        var result = (byte)(A - E);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (E & 0xF);
        Carry = A < E;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        ProgramCounter++;
    }

    // 0xBC - CP H - Compare H with A.
    private void ExecuteCpH()
    {
        var result = (byte)(A - H);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (H & 0xF);
        Carry = A < H;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        ProgramCounter++;
    }

    // 0xBD - CP L - Compare L with A.
    private void ExecuteCpL()
    {
        var result = (byte)(A - L);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (L & 0xF);
        Carry = A < L;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        ProgramCounter++;
    }

    // 0xBE - CP (HL) - Compare (HL) with A.
    private void ExecuteCpHl()
    {
        var result = (byte)(A - Memory[(H << 8) | L]);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (Memory[(H << 8) | L] & 0xF);
        Carry = A < Memory[(H << 8) | L];
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        ProgramCounter++;
    }

    // 0xBF - CP A - Compare A with A.
    private void ExecuteCpA()
    {
        var result = (byte)(A - A);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        ProgramCounter++;
    }

    // 0xC0 - RET NZ - Return if Z flag is not set.
    private void ExecuteRetNz()
    {
        if (!Zero)
        {
            StackPointer += 2;
            ProgramCounter = (ushort)(Memory[StackPointer] << 8 | Memory[StackPointer + 1]);
        }
        else
        {
            ProgramCounter++;
        }
    }

    // 0xC1 - POP BC - Pop two bytes off stack into BC.
    private void ExecutePopBc()
    {
        C = Memory[StackPointer + 1];
        B = Memory[StackPointer];
        StackPointer += 2;
        ProgramCounter++;
    }

    // 0xC2 - JP NZ, nn - Jump to address nn if Z flag is not set.
    private void ExecuteJpNZnn()
    {
        if (!Zero)
        {
            ProgramCounter = (ushort)(Memory[ProgramCounter + 2] << 8 | Memory[ProgramCounter + 1]);
        }
        else
        {
            ProgramCounter += 3;
        }
    }

    // 0xC3 - JP nn - Jump to address nn.
    private void ExecuteJpnn()
    {
        ProgramCounter = (ushort)(Memory[ProgramCounter + 2] << 8 | Memory[ProgramCounter + 1]);
    }


    // 0xC4 - CALL NZ, nn - Call address nn if Z flag is not set.
    private void ExecuteCallNZnn()
    {
        if (!Zero)
        {
            StackPointer -= 2;
            Memory[StackPointer] = (byte)((ProgramCounter + 3) >> 8);
            Memory[StackPointer + 1] = (byte)((ProgramCounter + 3) & 0xFF);
            ProgramCounter = (ushort)(Memory[ProgramCounter + 1] << 8 | Memory[ProgramCounter + 2]);
        }
        else
        {
            ProgramCounter += 3;
        }
    }

    // 0xC5 - PUSH BC - Push BC onto stack.
    private void ExecutePushBc()
    {
        Memory[StackPointer] = B;
        Memory[StackPointer + 1] = C;
        StackPointer -= 2;
        ProgramCounter++;
    }

    // 0xC6 - ADD A, n - Add n to A.
    private void ExecuteAddAn()
    {
        var result = (byte)(A + Memory[ProgramCounter + 1]);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[ProgramCounter + 1] & 0xF) > 0xF;
        Carry = A + Memory[ProgramCounter + 1] > 0xFF;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        ProgramCounter += 2;
    }

    // 0xC7 - RST 0 - Reset program counter to address 0x00.
    private void ExecuteRst0()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)(ProgramCounter >> 8);
        Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
        ProgramCounter = 0x00;
    }

    // 0xC8 - RET Z - Return if Z flag is set.
    private void ExecuteRetZ()
    {
        if (Zero)
        {
            StackPointer += 2;
            ProgramCounter = (ushort)(Memory[StackPointer] << 8 | Memory[StackPointer + 1]);
        }
        else
        {
            ProgramCounter++;
        }
    }

    // 0xC9 - RET - Return.
    private void ExecuteRet()
    {
        StackPointer += 2;
        ProgramCounter = (ushort)(Memory[StackPointer] << 8 | Memory[StackPointer + 1]);
    }

    // 0xCA - JP Z, nn - Jump to address nn if Z flag is set.
    private void ExecuteJpZnn()
    {
        if (Zero)
        {
            ProgramCounter = (ushort)(Memory[ProgramCounter + 1] << 8 | Memory[ProgramCounter + 2]);
        }
        else
        {
            ProgramCounter += 3;
        }
    }


    // 0xCC - CALL Z, nn - Call address nn if Z flag is set.
    private void ExecuteCallZnn()
    {
        if (Zero)
        {
            StackPointer -= 2;
            Memory[StackPointer] = (byte)((ProgramCounter + 3) >> 8);
            Memory[StackPointer + 1] = (byte)((ProgramCounter + 3) & 0xFF);
            ProgramCounter = (ushort)(Memory[ProgramCounter + 1] << 8 | Memory[ProgramCounter + 2]);
        }
        else
        {
            ProgramCounter += 3;
        }
    }

    // 0xCD - CALL nn - Call address nn.
    private void ExecuteCallnn()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)((ProgramCounter + 3) >> 8);
        Memory[StackPointer + 1] = (byte)((ProgramCounter + 3) & 0xFF);
        ProgramCounter = (ushort)(Memory[ProgramCounter + 1] << 8 | Memory[ProgramCounter + 2]);
    }

    // 0xCE - ADC A, n - Add n + Carry flag to A.
    private void ExecuteAdcAn()
    {
        var result = (byte)(A + Memory[ProgramCounter + 1] + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[ProgramCounter + 1] & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = A + Memory[ProgramCounter + 1] + (Carry ? 1 : 0) > 0xFF;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        ProgramCounter += 2;
    }

    // 0xCF - RST 8 - Push PC onto stack and jump to address 0x08.
    private void ExecuteRst8()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)(ProgramCounter >> 8);
        Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
        ProgramCounter = 0x08;
    }

    // 0xD0 - RET NC - If last result was not carried, return from subroutine.
    private void ExecuteRetNc()
    {
        if (!Carry)
        {
            ProgramCounter = (ushort)((Memory[StackPointer] << 8) | Memory[StackPointer + 1]);
            StackPointer += 2;
        }
    }

    // 0xD1 - POP DE - Pop two bytes from stack into DE.
    private void ExecutePopDe()
    {
        D = Memory[StackPointer];
        E = Memory[StackPointer + 1];
        StackPointer += 2;
    }

    // 0xD2 - JP NC, nn - If last result was not carried, jump to address nn.
    private void ExecuteJpNCnn()
    {
        if (!Carry)
        {
            var address = (ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]);
            ProgramCounter = address;
        }
        else
        {
            ProgramCounter += 2;
        }
    }

    // 0xD3 - OUT (n), A - Output A to port n.
    private void ExecuteOutnA()
    {
        var port = Memory[ProgramCounter + 1];
        // Output A to port n in the implementation
        Console.Write((char)port);
        ProgramCounter += 2;
    }

    // 0xD4 - CALL NC, nn - If last result was not carried, call address nn.
    private void ExecuteCallNCnn()
    {
        if (!Carry)
        {
            var address = (ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]);
            StackPointer -= 2;
            Memory[StackPointer] = (byte)(ProgramCounter >> 8);
            Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
            ProgramCounter = address;
        }
        else
        {
            ProgramCounter += 2;
        }
    }

    // 0xD5 - PUSH DE - Push DE onto stack.
    private void ExecutePushDe()
    {
        StackPointer -= 2;
        Memory[StackPointer] = D;
        Memory[StackPointer + 1] = E;
    }

    // 0xD6 - SUB n - Subtract n from A.
    private void ExecuteSubn()
    {
        var n = Memory[ProgramCounter + 1];
        var result = (byte)(A - n);
        Zero = result == 0;
        Sign = result > 0x7F;
        HalfCarry = (A & 0xF) < (n & 0xF);
        int x = A;
        x = x - n;
        x = x & 0xff;
        var y = x;
        y = y ^ (x >> 4);
        y = y ^ (y >> 2);
        y = y ^ (y >> 1);
        Carry = A < n;
        Parity = (y & 0x01) != 0;
        A = result;
        ProgramCounter += 2;
    }

    // 0xD7 - RST 10 - Push PC onto stack and jump to address 0x10.
    private void ExecuteRst10()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)(ProgramCounter >> 8);
        Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
        ProgramCounter = 0x10;
    }

    // 0xD8 - RET C - If last result carried, return from subroutine.
    private void ExecuteRetC()
    {
        if (Carry)
        {
            ProgramCounter = (ushort)((Memory[StackPointer] << 8) | Memory[StackPointer + 1]);
            StackPointer += 2;
        }
    }

    // 0xD9 - RETI - Enable interrupts and return from subroutine.
    private void ExecuteRetI()
    {
        ProgramCounter = (ushort)((Memory[StackPointer] << 8) | Memory[StackPointer + 1]);
        StackPointer += 2;
        // Enable interrupts in the implementation
    }

    // 0xDA - JP C, nn - If last result carried, jump to address nn.
    private void ExecuteJpCnn()
    {
        if (Carry)
        {
            var address = (ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]);
            ProgramCounter = address;
        }
        else
        {
            ProgramCounter += 2;
        }
    }

    // 0xDC - CALL C, nn - If last result carried, call address nn.
    private void ExecuteCallCnn()
    {
        if (Carry)
        {
            var address = (ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]);
            StackPointer -= 2;
            Memory[StackPointer] = (byte)(ProgramCounter >> 8);
            Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
            ProgramCounter = address;
        }
        else
        {
            ProgramCounter += 2;
        }
    }

    // 0xDD -
    // 0xDE - SBC A, n - Subtract n + Carry flag from A

    private void ExecuteSbcAn()
    {
        var n = Memory[ProgramCounter + 1];
        var result = (byte)(A - n - (Carry ? 1 : 0));
        A = result;
        Zero = result == 0;
        Sign = result > 0x7F;
        HalfCarry = (A & 0xF) < (n & 0xF) + (Carry ? 1 : 0);
        Carry = A < n + (Carry ? 1 : 0);
        Parity = ((result & 0x01) ^ ((result & 0x02) >> 1) ^ ((result & 0x04) >> 2) ^ ((result & 0x08) >> 3)
                  ^ ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^
                  ((result & 0x80) >> 7)) == 0;
        ProgramCounter += 2;
    }

    // 0xDF - RST 18 - Push PC onto stack and jump to address 0x18.
    private void ExecuteRst18()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)(ProgramCounter >> 8);
        Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
        ProgramCounter = 0x18;
    }

    // 0xE0 - LDH (n), A - Put A into address 0xFF00 + n.
    private void ExecuteLdhAn()
    {
        var address = (ushort)((0xFF00 + Memory[ProgramCounter + 1]) % 0xFFFF);
        Memory[address] = A;
        ProgramCounter += 2;
    }

    // 0xE1 - POP HL - Pop two bytes from stack into HL.
    private void ExecutePopHl()
    {
        H = Memory[StackPointer];
        L = Memory[StackPointer + 1];
        StackPointer += 2;
    }


    // 0xE2 - LD (C), A - Load A into (C). 


    // 0xE3 - EX (SP), HL - Exchange the values of the HL and (SP) registers.


    // 0xE4 - CALL PO, nn - Call address n if the sign bit is set.


    // 0xE5 - PUSH HL - Push HL onto the stack.
    private void ExecutePushHl()
    {
        var hl = (ushort)((H << 8) | L);
        Memory[StackPointer - 1] = (byte)((hl >> 8) & 0xFF);
        Memory[StackPointer - 2] = (byte)(hl & 0xFF);
        StackPointer -= 2;
        ProgramCounter++;
    }

    // 0xE6 - AND n - Logically AND n with A, result in A.
    private void ExecuteAndN()
    {
        A &= Memory[ProgramCounter + 1];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Parity = (A & 0x01) == 0;
        Carry = false;
        ProgramCounter += 2;
    }

    // 0xE7 - RST 20 - Push present address onto stack and jump to address 0020h.
    private void ExecuteRst20()
    {
        Memory[StackPointer - 1] = (byte)((ProgramCounter >> 8) & 0xFF);
        Memory[StackPointer - 2] = (byte)(ProgramCounter & 0xFF);
        StackPointer -= 2;
        ProgramCounter = 0x0020;
    }

    // 0xE8 - ADD SP, n - Add n to SP.
    private void ExecuteAddSPn()
    {
        var n = (sbyte)Memory[ProgramCounter + 1];
        var result = (ushort)(StackPointer + n);
        Sign = false;
        Zero = false;
        HalfCarry = (StackPointer & 0xF) + (n & 0xF) > 0xF;
        Carry = (StackPointer & 0xFF) + (n & 0xFF) > 0xFF;
        Parity = (result & 0x01) == 0;
        StackPointer = result;
        ProgramCounter += 2;
    }

    // 0xE9 - JP (HL) - Jump to address contained in HL.
    private void ExecuteJpHl()
    {
        var address = (ushort)((H << 8) | L);
        ProgramCounter = address;
    }

    // 0xEA - LD (nn), A - Put A into address nn.
    private void ExecuteLdnnA()
    {
        var address = (ushort)((ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]) % 0x10000);
        Memory[address] = A;
        ProgramCounter = (ushort)((ProgramCounter + 3) % 0x10000);
    }

    // 0xEB - 

    // 0xEC -  

    // 0xED -  

    // 0xEE - XOR n - Logical exclusive OR n with A, result in A.
    private void ExecuteXorn()
    {
        var n = Memory[ProgramCounter + 1];
        A ^= n;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0xEF - RST 28H - Push present address onto stack. Jump to address 0x0028.
    private void ExecuteRst28()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)((ProgramCounter >> 8) & 0xFF);
        Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
        ProgramCounter = 0x0028;
    }


    // 0xF0 - LDH A, (n) - Put value at address 0xFF00 + n into A.
    private void ExecuteLdHAa8()
    {
        var n = Memory[ProgramCounter + 1];
        A = Memory[0xFF00 + (n % 0xFF)];
        ProgramCounter += 2;
    }


    // 0xF1 - POP AF - Pop two bytes from stack into AF.
    private void ExecutePopAf()
    {
        A = Memory[StackPointer + 1];
        var flags = Memory[StackPointer];
        Zero = (flags & 0x80) != 0;
        Sign = (flags & 0x40) != 0;
        HalfCarry = (flags & 0x20) != 0;
        Parity = (flags & 0x04) != 0;
        Carry = (flags & 0x01) != 0;
        StackPointer += 2;
        ProgramCounter++;
    }

    // 0xF2 - LD A, (C) - Put value at address 0xFF00 + C into A.


    // 0xF3 - DI - Disable interrupts after instruction.
    private void ExecuteDi()
    {
        //TODO: Implement interrupt disabling.
        ProgramCounter++;
    }

    // 0xF4 -  

    // 0xF5 - PUSH AF - Push AF onto stack.
    private void ExecutePushAf()
    {
        Memory[StackPointer] = (byte)((Zero ? 0x80 : 0x00) | (Sign ? 0x40 : 0x00) | (HalfCarry ? 0x20 : 0x00) |
                                      (Parity ? 0x04 : 0x00) | (Carry ? 0x01 : 0x00));
        Memory[StackPointer + 1] = A;
        StackPointer -= 2;
        ProgramCounter++;
    }

    // 0xF6 - OR n - Logical OR n with A, result in A.
    private void ExecuteOrn()
    {
        var n = Memory[ProgramCounter + 1];
        A |= n;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0xF7 - RST 30H - Push present address onto stack. Jump to address 0x0030.
    private void ExecuteRst30()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)((ProgramCounter >> 8) & 0xFF);
        Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
        ProgramCounter = 0x0030;
    }

    // 0xF8 - LD HL, SP+n - Put SP + n into HL.
    private void ExecuteLdHlsPn()
    {
        var n = (sbyte)Memory[ProgramCounter + 1];
        var result = (ushort)(StackPointer + n);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (StackPointer & 0xF) + (n & 0xF) > 0xF;
        Carry = (StackPointer & 0xFF) + (n & 0xFF) > 0xFF;
        Parity = (H & 0x01) == 0;
        ProgramCounter += 2;
    }

    // 0xF9 - LD SP, HL - Put HL into SP.
    private void ExecuteLdSphl()
    {
        var hl = (ushort)((H << 8) | L);
        StackPointer = hl;
        ProgramCounter++;
    }

    // 0xFA - LD A, (nn) - Put value at address nn into A.
    private void ExecuteLdAnn()
    {
        var address = (ushort)((Memory[ProgramCounter + 2] << 8) | Memory[ProgramCounter + 1]);
        A = Memory[address];
        ProgramCounter += 3;
    }

    // 0xFB - EI - Enable interrupts after instruction.
    private void ExecuteEi()
    {
        //TODO: Implement interrupt enabling.
        ProgramCounter++;
    }

    // 0xFC - 

    // 0xFD - 

    // 0xFE - CP n - Compare A with n.
    private void ExecuteCpn()
    {
        var n = Memory[ProgramCounter + 1];
        Sign = (A & 0x80) != (n & 0x80);
        Zero = A == n;
        HalfCarry = (A & 0xF) < (n & 0xF);
        Carry = A < n;
        Parity = (A & 0x01) == (n & 0x01);
        ProgramCounter += 2;
    }

    // 0xFF - RST 38H - Push present address onto stack. Jump to address 0x0038.
    private void ExecuteRst38()
    {
        StackPointer -= 2;
        Memory[StackPointer] = (byte)((ProgramCounter >> 8) & 0xFF);
        Memory[StackPointer + 1] = (byte)(ProgramCounter & 0xFF);
        ProgramCounter = 0x0038;
    }


    public void ExecutePrefixCb()
    {
        var opcode = Memory[ProgramCounter + 1];
        switch (opcode)
        {
            case 0x00: // RLC B
                var bit0 = (byte)(B & 0x80);
                B = (byte)((B << 1) | bit0);
                Zero = B == 0;
                Carry = bit0 == 0x80;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x01: // RLC C
                bit0 = (byte)(C & 0x80);
                C = (byte)((C << 1) | bit0);
                Zero = C == 0;
                Carry = bit0 == 0x80;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x02: // RLC D
                bit0 = (byte)(D & 0x80);
                D = (byte)((D << 1) | bit0);
                Zero = D == 0;
                Carry = bit0 == 0x80;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x03: // RLC E
                bit0 = (byte)(E & 0x80);
                E = (byte)((E << 1) | bit0);
                Zero = E == 0;
                Carry = bit0 == 0x80;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x04: // RLC H
                bit0 = (byte)(H & 0x80);
                H = (byte)((H << 1) | bit0);
                Zero = H == 0;
                Carry = bit0 == 0x80;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x05: // RLC L
                bit0 = (byte)(L & 0x80);
                L = (byte)((L << 1) | bit0);
                Zero = L == 0;
                Carry = bit0 == 0x80;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;

            case 0x06: // RLC (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] << 1) | bit0);
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x07: // RLC A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A << 1) | bit0);
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x08: // RRC B
                bit0 = (byte)(B & 0x01);
                B = (byte)((B >> 1) | (bit0 << 7));
                Zero = B == 0;
                Carry = bit0 == 1;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x09: // RRC C
                bit0 = (byte)(C & 0x01);
                C = (byte)((C >> 1) | (bit0 << 7));
                Zero = C == 0;
                Carry = bit0 == 1;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x0A: // RRC D
                bit0 = (byte)(D & 0x01);
                D = (byte)((D >> 1) | (bit0 << 7));
                Zero = D == 0;
                Carry = bit0 == 1;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x0B: // RRC E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E >> 1) | (bit0 << 7));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;

            case 0x0C: // RRC H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x0D: // RRC L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x0E: // RRC (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x0F: // RRC A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x10: // RL B
                bit0 = (byte)(B & 0x80);
                B = (byte)((B << 1) | (Carry ? 1 : 0));
                Zero = B == 0;
                Carry = bit0 == 0x80;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x11: // RL C
                bit0 = (byte)(C & 0x80);
                C = (byte)((C << 1) | (Carry ? 1 : 0));
                Zero = C == 0;
                Carry = bit0 == 0x80;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x12: // RL D
                bit0 = (byte)(D & 0x80);
                D = (byte)((D << 1) | (Carry ? 1 : 0));
                Zero = D == 0;
                Carry = bit0 == 0x80;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;

            case 0x13: // RL E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E << 1) | (Carry ? 1 : 0));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x14: // RL H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H << 1) | (Carry ? 1 : 0));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x15: // RL L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L << 1) | (Carry ? 1 : 0));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x16: // RL (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] << 1) | (Carry ? 1 : 0));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x17: // RL A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A << 1) | (Carry ? 1 : 0));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x18: // RR B
                bit0 = (byte)(B & 0x01);
                B = (byte)((B >> 1) | (Carry ? 0x80 : 0x00));
                Zero = B == 0;
                Carry = bit0 == 1;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x19: // RR C
                bit0 = (byte)(C & 0x01);
                C = (byte)((C >> 1) | (Carry ? 0x80 : 0x00));
                Zero = C == 0;
                Carry = bit0 == 1;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;

            case 0x1A: // RR D
                bit0 = (byte)(D & 0x01);
                D = (byte)((D >> 1) | (bit0 << 7));
                Zero = D == 0;
                Carry = bit0 == 1;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x1B: // RR E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E >> 1) | (bit0 << 7));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x1C: // RR H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x1D: // RR L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x1E: // RR (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x1F: // RR A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x20: // SLA B
                bit0 = (byte)(B & 0x80);
                B = (byte)(B << 1);
                Zero = B == 0;
                Carry = bit0 == 0x80;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x21: // SLA C
                bit0 = (byte)(C & 0x80);
                C = (byte)(C << 1);
                Zero = C == 0;
                Carry = bit0 == 0x80;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x22: // SLA D
                bit0 = (byte)(D & 0x80);
                D = (byte)(D << 1);
                Zero = D == 0;
                Carry = bit0 == 0x80;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;


            case 0x23: // SLA E
                A = (byte)((E << 1) | (E >> 7));
                Zero = A == 0;
                Carry = (E & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x24: // SLA H
                A = (byte)((H << 1) | (H >> 7));
                Zero = A == 0;
                Carry = (H & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x25: // SLA L
                A = (byte)((L << 1) | (L >> 7));
                Zero = A == 0;
                Carry = (L & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x26: // SLA (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] << 1) | (bit0 >> 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x27: // SLA A
                A = (byte)((A << 1) | (A >> 7));
                Zero = A == 0;
                Carry = (A & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x28: // SRA B
                A = (byte)((B >> 1) | (B << 7));
                Zero = A == 0;
                Carry = (B & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x29: // SRA C
                A = (byte)((C >> 1) | (C << 7));
                Zero = A == 0;
                Carry = (C & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x2A: // SRA D
                A = (byte)((D >> 1) | (D << 7));
                Zero = A == 0;
                Carry = (D & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x2B: // SRA E
                A = (byte)((E >> 1) | (E << 7));
                Zero = A == 0;
                Carry = (E & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;

            case 0x2C: // SRA H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x2D: // SRA L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x2E: // SRA (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x2F: // SRA A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x30: // SWAP B
                B = (byte)((B << 4) | (B >> 4));
                Zero = B == 0;
                Carry = false;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x31: // SWAP C
                C = (byte)((C << 4) | (C >> 4));
                Zero = C == 0;
                Carry = false;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x32: // SWAP D
                D = (byte)((D << 4) | (D >> 4));
                Zero = D == 0;
                Carry = false;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x33: // SWAP E
                E = (byte)((E << 4) | (E >> 4));
                Zero = E == 0;
                Carry = false;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;

            case 0x34: // SWAP H
                A = (byte)((H >> 4) | (H << 4));
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x35: // SWAP L
                A = (byte)((L >> 4) | (L << 4));
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x36: // SWAP (HL)
                A = (byte)((Memory[(H << 8) | L] >> 4) | (Memory[(H << 8) | L] << 4));
                Memory[(H << 8) | L] = A;
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x37: // SWAP A
                A = (byte)((A >> 4) | (A << 4));
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x38: // SRL B
                bit0 = (byte)(B & 0x01);
                B = (byte)((B >> 1) | (bit0 << 7));
                Zero = B == 0;
                Carry = bit0 == 1;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x39: // SRL C
                bit0 = (byte)(C & 0x01);
                C = (byte)((C >> 1) | (bit0 << 7));
                Zero = C == 0;
                Carry = bit0 == 1;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x3A: // SRL D
                bit0 = (byte)(D & 0x01);
                D = (byte)((D >> 1) | (bit0 << 7));
                Zero = D == 0;
                Carry = bit0 == 1;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x3B: // SRL E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E >> 1) | (bit0 << 7));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;

            case 0x3C: // SRL H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x3D: // SRL L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x3E: // SRL (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x3F: // SRL A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0x40: // BIT 0,B
                Zero = (B & 0x01) == 0;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x41: // BIT 0,C
                Zero = (C & 0x01) == 0;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x42: // BIT 0,D
                Zero = (D & 0x01) == 0;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x43: // BIT 0,E
                Zero = (E & 0x01) == 0;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x44: // BIT 0,H
                Zero = (H & 0x01) == 0;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = true;
                ProgramCounter += 2;
                break;

            case 0x45: // BIT 0,L
                Zero = (L & 0x01) == 0;
                Sign = (L & 0x80) > 0;
                Parity = IsParity(L);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x46: // BIT 0,(HL)
                Zero = (Memory[(H << 8) | L] & 0x01) == 0;
                Sign = (Memory[(H << 8) | L] & 0x80) > 0;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x47: // BIT 0,A
                Zero = (A & 0x01) == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x48: // BIT 1,B
                Zero = (B & 0x02) == 0;
                Sign = (B & 0x80) > 0;
                Parity = IsParity(B);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x49: // BIT 1,C
                Zero = (C & 0x02) == 0;
                Sign = (C & 0x80) > 0;
                Parity = IsParity(C);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x4A: // BIT 1,D
                Zero = (D & 0x02) == 0;
                Sign = (D & 0x80) > 0;
                Parity = IsParity(D);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x4B: // BIT 1,E
                Zero = (E & 0x02) == 0;
                Sign = (E & 0x80) > 0;
                Parity = IsParity(E);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x4C: // BIT 1,H
                Zero = (H & 0x02) == 0;
                Sign = (H & 0x80) > 0;
                Parity = IsParity(H);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x4D: // BIT 1,L
                Zero = (L & 0x02) == 0;
                Sign = (L & 0x80) > 0;
                Parity = IsParity(L);
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x4E: // BIT 1,(HL)
                Zero = (Memory[(H << 8) | L] & 0x02) == 0;
                Sign = (Memory[(H << 8) | L] & 0x80) > 0;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = true;
                ProgramCounter += 2;
                break;

            case 0x4F: // BIT 1,A
                Zero = (A & 0x02) == 0;
                Sign = (A & 0x02) > 0;
                Parity = IsParity((byte)(A & 0x02));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x50: // BIT 2,B
                Zero = (B & 0x04) == 0;
                Sign = (B & 0x04) > 0;
                Parity = IsParity((byte)(B & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x51: // BIT 2,C
                Zero = (C & 0x04) == 0;
                Sign = (C & 0x04) > 0;
                Parity = IsParity((byte)(C & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x52: // BIT 2,D
                Zero = (D & 0x04) == 0;
                Sign = (D & 0x04) > 0;
                Parity = IsParity((byte)(D & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x53: // BIT 2,E
                Zero = (E & 0x04) == 0;
                Sign = (E & 0x04) > 0;
                Parity = IsParity((byte)(E & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x54: // BIT 2,H
                Zero = (H & 0x04) == 0;
                Sign = (H & 0x04) > 0;
                Parity = IsParity((byte)(H & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x55: // BIT 2,L
                Zero = (L & 0x04) == 0;
                Sign = (L & 0x04) > 0;
                Parity = IsParity((byte)(L & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x56: // BIT 2,(HL)
                Zero = (Memory[(H << 8) | L] & 0x04) == 0;
                Sign = (Memory[(H << 8) | L] & 0x04) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x57: // BIT 2,A
                Zero = (A & 0x04) == 0;
                Sign = (A & 0x04) > 0;
                Parity = IsParity((byte)(A & 0x04));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x58: // BIT 3,B
                Zero = (B & 0x08) == 0;
                Sign = (B & 0x08) > 0;
                Parity = IsParity((byte)(B & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x59: // BIT 3,C
                Zero = (C & 0x08) == 0;
                Sign = (C & 0x08) > 0;
                Parity = IsParity((byte)(C & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x5A: // BIT 3,D
                Zero = (D & 0x08) == 0;
                Sign = (D & 0x08) > 0;
                Parity = IsParity((byte)(D & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x5B: // BIT 3,E
                Zero = (E & 0x08) == 0;
                Sign = (E & 0x08) > 0;
                Parity = IsParity((byte)(E & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x5C: // BIT 3,H
                Zero = (H & 0x08) == 0;
                Sign = (H & 0x08) > 0;
                Parity = IsParity((byte)(H & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x5D: // BIT 3,L
                Zero = (L & 0x08) == 0;
                Sign = (L & 0x08) > 0;
                Parity = IsParity((byte)(L & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x5E: // BIT 3,(HL)
                Zero = (Memory[(H << 8) | L] & 0x08) == 0;
                Sign = (Memory[(H << 8) | L] & 0x08) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x5F: // BIT 3,A
                Zero = (A & 0x08) == 0;
                Sign = (A & 0x08) > 0;
                Parity = IsParity((byte)(A & 0x08));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x60: // BIT 4,B
                Zero = (B & 0x10) == 0;
                Sign = (B & 0x10) > 0;
                Parity = IsParity((byte)(B & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;


            case 0x61: // BIT 4,C
                Zero = (C & 0x10) == 0;
                Sign = (C & 0x10) > 0;
                Parity = IsParity((byte)(C & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x62: // BIT 4,D
                Zero = (D & 0x10) == 0;
                Sign = (D & 0x10) > 0;
                Parity = IsParity((byte)(D & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x63: // BIT 4,E
                Zero = (E & 0x10) == 0;
                Sign = (E & 0x10) > 0;
                Parity = IsParity((byte)(E & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x64: // BIT 4,H
                Zero = (H & 0x10) == 0;
                Sign = (H & 0x10) > 0;
                Parity = IsParity((byte)(H & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x65: // BIT 4,L
                Zero = (L & 0x10) == 0;
                Sign = (L & 0x10) > 0;
                Parity = IsParity((byte)(L & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x66: // BIT 4,(HL)
                Zero = (Memory[(H << 8) | L] & 0x10) == 0;
                Sign = (Memory[(H << 8) | L] & 0x10) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x67: // BIT 4,A
                Zero = (A & 0x10) == 0;
                Sign = (A & 0x10) > 0;
                Parity = IsParity((byte)(A & 0x10));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x68: // BIT 5,B
                Zero = (B & 0x20) == 0;
                Sign = (B & 0x20) > 0;
                Parity = IsParity((byte)(B & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x69: // BIT 5,C
                Zero = (C & 0x20) == 0;
                Sign = (C & 0x20) > 0;
                Parity = IsParity((byte)(C & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x6A: // BIT 5,D
                Zero = (D & 0x20) == 0;
                Sign = (D & 0x20) > 0;
                Parity = IsParity((byte)(D & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;

            case 0x6B: // BIT 5,E
                Zero = (E & 0x20) == 0;
                Sign = (E & 0x20) > 0;
                Parity = IsParity((byte)(E & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x6C: // BIT 5,H
                Zero = (H & 0x20) == 0;
                Sign = (H & 0x20) > 0;
                Parity = IsParity((byte)(H & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x6D: // BIT 5,L
                Zero = (L & 0x20) == 0;
                Sign = (L & 0x20) > 0;
                Parity = IsParity((byte)(L & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x6E: // BIT 5,(HL)
                Zero = (Memory[(H << 8) | L] & 0x20) == 0;
                Sign = (Memory[(H << 8) | L] & 0x20) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x6F: // BIT 5,A
                Zero = (A & 0x20) == 0;
                Sign = (A & 0x20) > 0;
                Parity = IsParity((byte)(A & 0x20));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x70: // BIT 6,B
                Zero = (B & 0x40) == 0;
                Sign = (B & 0x40) > 0;
                Parity = IsParity((byte)(B & 0x40));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x71: // BIT 6,C
                Zero = (C & 0x40) == 0;
                Sign = (C & 0x40) > 0;
                Parity = IsParity((byte)(C & 0x40));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x72: // BIT 6,D
                Zero = (D & 0x40) == 0;
                Sign = (D & 0x40) > 0;
                Parity = IsParity((byte)(D & 0x40));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x73: // BIT 6,E
                Zero = (E & 0x40) == 0;
                Sign = (E & 0x40) > 0;
                Parity = IsParity((byte)(E & 0x40));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x74: // BIT 6,H
                Zero = (H & 0x40) == 0;
                Sign = (H & 0x40) > 0;
                Parity = IsParity((byte)(H & 0x40));
                HalfCarry = true;
                ProgramCounter += 2;
                break;

            case 0x75: // BIT 6,L
                Zero = (L & (1 << 6)) == 0;
                Sign = (L & (1 << 6)) > 0;
                Parity = IsParity((byte)(L & (1 << 6)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x76: // BIT 6,(HL)
                Zero = (Memory[(H << 8) | L] & (1 << 6)) == 0;
                Sign = (Memory[(H << 8) | L] & (1 << 6)) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & (1 << 6)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x77: // BIT 6,A
                Zero = (A & (1 << 6)) == 0;
                Sign = (A & (1 << 6)) > 0;
                Parity = IsParity((byte)(A & (1 << 6)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x78: // BIT 7,B
                Zero = (B & (1 << 7)) == 0;
                Sign = (B & (1 << 7)) > 0;
                Parity = IsParity((byte)(B & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x79: // BIT 7,C
                Zero = (C & (1 << 7)) == 0;
                Sign = (C & (1 << 7)) > 0;
                Parity = IsParity((byte)(C & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x7A: // BIT 7,D
                Zero = (D & (1 << 7)) == 0;
                Sign = (D & (1 << 7)) > 0;
                Parity = IsParity((byte)(D & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x7B: // BIT 7,E
                Zero = (E & (1 << 7)) == 0;
                Sign = (E & (1 << 7)) > 0;
                Parity = IsParity((byte)(E & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x7C: // BIT 7,H
                Zero = (H & (1 << 7)) == 0;
                Sign = (H & (1 << 7)) > 0;
                Parity = IsParity((byte)(H & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x7D: // BIT 7,L
                Zero = (L & (1 << 7)) == 0;
                Sign = (L & (1 << 7)) > 0;
                Parity = IsParity((byte)(L & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x7E: // BIT 7,(HL)
                Zero = (Memory[(H << 8) | L] & (1 << 7)) == 0;
                Sign = (Memory[(H << 8) | L] & (1 << 7)) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;
            case 0x7F: // BIT 7,A
                Zero = (A & (1 << 7)) == 0;
                Sign = (A & (1 << 7)) > 0;
                Parity = IsParity((byte)(A & (1 << 7)));
                HalfCarry = true;
                ProgramCounter += 2;
                break;

            case 0x80: // RES 0,B
                B &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x81: // RES 0,C
                C &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x82: // RES 0,D
                D &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x83: // RES 0,E
                E &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x84: // RES 0,H
                H &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x85: // RES 0,L
                L &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x86: // RES 0,(HL)
                Memory[(H << 8) | L] &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x87: // RES 0,A
                A &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x88: // RES 1,B
                B &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x89: // RES 1,C
                C &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x8A: // RES 1,D
                D &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x8B: // RES 1,E
                E &= 0xFD;
                ProgramCounter += 2;
                break;

            case 0x8C: // RES 1,H
                H &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x8D: // RES 1,L
                L &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x8E: // RES 1,(HL)
                Memory[(H << 8) | L] &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x8F: // RES 1,A
                A &= 0xFE;
                ProgramCounter += 2;
                break;
            case 0x90: // RES 2,B
                B &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x91: // RES 2,C
                C &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x92: // RES 2,D
                D &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x93: // RES 2,E
                E &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x94: // RES 2,H
                H &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x95: // RES 2,L
                L &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x96: // RES 2,(HL)
                Memory[(H << 8) | L] &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x97: // RES 2,A
                A &= 0xFD;
                ProgramCounter += 2;
                break;
            case 0x98: // RES 3,B
                B &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0x99: // RES 3,C
                C &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0x9A: // RES 3,D
                D &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0x9B: // RES 3,E
                E &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0x9C: // RES 3,H
                H &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0x9D: // RES 3,L
                L &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0x9E: // RES 3,(HL)
                Memory[(H << 8) | L] &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0x9F: // RES 3,A
                A &= 0xFB;
                ProgramCounter += 2;
                break;
            case 0xA0: // RES 4,B
                B &= 0xF7;
                ProgramCounter += 2;
                break;
            case 0xA1: // RES 4,C
                C &= 0xF7;
                ProgramCounter += 2;
                break;
            case 0xA2: // RES 4,D
                D &= 0xF7;
                ProgramCounter += 2;
                break;
            case 0xA3: // RES 4,E
                E &= 0xF7;
                ProgramCounter += 2;
                break;

            case 0xA4: // RES 4,H
                H &= 0xEF;
                ProgramCounter += 2;
                break;
            case 0xA5: // RES 4,L
                L &= 0xEF;
                ProgramCounter += 2;
                break;
            case 0xA6: // RES 4,(HL)
                Memory[(H << 8) | L] &= 0xEF;
                ProgramCounter += 2;
                break;
            case 0xA7: // RES 4,A
                A &= 0xEF;
                ProgramCounter += 2;
                break;
            case 0xA8: // RES 5,B
                B &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xA9: // RES 5,C
                C &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xAA: // RES 5,D
                D &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xAB: // RES 5,E
                E &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xAC: // RES 5,H
                H &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xAD: // RES 5,L
                L &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xAE: // RES 5,(HL)
                Memory[(H << 8) | L] &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xAF: // RES 5,A
                A &= 0xDF;
                ProgramCounter += 2;
                break;
            case 0xB0: // RES 6,B
                B &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB1: // RES 6,C
                C &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB2: // RES 6,D
                D &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB3: // RES 6,E
                E &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB4: // RES 6,H
                H &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB5: // RES 6,L
                L &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB6: // RES 6,(HL)
                Memory[(H << 8) | L] &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB7: // RES 6,A
                A &= 0xBF;
                ProgramCounter += 2;
                break;
            case 0xB8: // RES 7,B
                B &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xB9: // RES 7,C
                C &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xBA: // RES 7,D
                D &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xBB: // RES 7,E
                E &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xBC: // RES 7,H
                H &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xBD: // RES 7,L
                L &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xBE: // RES 7,(HL)
                Memory[(H << 8) | L] &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xBF: // RES 7,A
                A &= 0x7F;
                ProgramCounter += 2;
                break;
            case 0xC0: // SET 0,B
                B |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC1: // SET 0,C
                C |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC2: // SET 0,D
                D |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC3: // SET 0,E
                E |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC4: // SET 0,H
                H |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC5: // SET 0,L
                L |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC6: // SET 0,(HL)
                Memory[(H << 8) | L] |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC7: // SET 0,A
                A |= 0x01;
                ProgramCounter += 2;
                break;
            case 0xC8: // SET 1,B
                B |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xC9: // SET 1,C
                C |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xCA: // SET 1,D
                D |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xCB: // SET 1,E
                E |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xCC: // SET 1,H
                H |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xCD: // SET 1,L
                L |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xCE: // SET 1,(HL)
                Memory[(H << 8) | L] |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xCF: // SET 1,A
                A |= 0x02;
                ProgramCounter += 2;
                break;
            case 0xD0: // SET 2,B
                B |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD1: // SET 2,C
                C |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD2: // SET 2,D
                D |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD3: // SET 2,E
                E |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD4: // SET 2,H
                H |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD5: // SET 2,L
                L |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD6: // SET 2,(HL)
                Memory[(H << 8) | L] |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD7: // SET 2,A
                A |= 0x04;
                ProgramCounter += 2;
                break;
            case 0xD8: // SET 3,B
                B |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xD9: // SET 3,C
                C |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xDA: // SET 3,D
                D |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xDB: // SET 3,E
                E |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xDC: // SET 3,H
                H |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xDD: // SET 3,L
                L |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xDE: // SET 3,(HL)
                Memory[(H << 8) | L] |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xDF: // SET 3,A
                A |= 0x08;
                ProgramCounter += 2;
                break;
            case 0xE0: // SET 4,B
                B |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE1: // SET 4,C
                C |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE2: // SET 4,D
                D |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE3: // SET 4,E
                E |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE4: // SET 4,H
                H |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE5: // SET 4,L
                L |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE6: // SET 4,(HL)
                Memory[(H << 8) | L] |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE7: // SET 4,A
                A |= 0x10;
                ProgramCounter += 2;
                break;
            case 0xE8: // SET 5,B
                B |= 0x20;
                ProgramCounter += 2;
                break;
            case 0xE9: // SET 5,C
                C |= 0x20;
                ProgramCounter += 2;
                break;
            case 0xEA: // SET 5,D
                D |= 0x20;
                ProgramCounter += 2;
                break;
            case 0xEB: // SET 5,E
                E |= 0x20;
                ProgramCounter += 2;
                break;
            case 0xEC: // SET 5,H
                H |= 0x20;
                ProgramCounter += 2;
                break;
            case 0xED: // SET 5,L
                L |= 0x20;
                ProgramCounter += 2;
                break;

            case 0xEE: // SET 5,(HL)
                Memory[(H << 8) | L] |= 0x20;
                Zero = Memory[(H << 8) | L] == 0;
                Carry = false;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xEF: // SET 5,A
                A |= 0x20;
                Zero = A == 0;
                Carry = false;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF0: // SET 6,B
                B |= 0x40;
                Zero = B == 0;
                Carry = false;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF1: // SET 6,C
                C |= 0x40;
                Zero = C == 0;
                Carry = false;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF2: // SET 6,D
                D |= 0x40;
                Zero = D == 0;
                Carry = false;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF3: // SET 6,E
                E |= 0x40;
                Zero = E == 0;
                Carry = false;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF4: // SET 6,H
                H |= 0x40;
                Zero = H == 0;
                Carry = false;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF5: // SET 6,L
                L |= 0x40;
                Zero = L == 0;
                Carry = false;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF6: // SET 6,(HL)
                Memory[(H << 8) | L] |= 0x40;
                Zero = Memory[(H << 8) | L] == 0;
                Carry = false;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF7: // SET 6,A
                A |= 0x40;
                Zero = A == 0;
                Carry = false;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF8: // SET 7,B
                B |= 0x80;
                Zero = B == 0;
                Carry = false;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xF9: // SET 7,C
                C |= 0x80;
                Zero = C == 0;
                Carry = false;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xFA: // SET 7,D
                D |= 0x80;
                Zero = D == 0;
                Carry = false;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xFB: // SET 7,E
                E |= 0x80;
                Zero = E == 0;
                Carry = false;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xFC: // SET 7,H
                H |= 0x80;
                Zero = H == 0;
                Carry = false;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xFD: // SET 7,L
                L |= 0x80;
                Zero = L == 0;
                Carry = false;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xFE: // SET 7,(HL)
                Memory[(H << 8) | L] |= 0x80;
                Zero = Memory[(H << 8) | L] == 0;
                Carry = false;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
            case 0xFF: // SET 7,A
                A |= 0x80;
                Zero = A == 0;
                Carry = false;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                ProgramCounter += 2;
                break;
        }
    }


    public Dictionary<byte, (ushort, Func<GameBoyCpu, string>)> OpcodeData = new()
    {
        { 0x00, (1, _ => "NOP") },
        {
            0x01, (3, cpu => $"LD BC, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0x02, (1, _ => "LD (BC), A") },
        { 0x03, (1, _ => "INC BC") },
        { 0x04, (1, _ => "INC B") },
        { 0x05, (1, _ => "DEC B") },
        { 0x06, (2, cpu => $"LD B, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x07, (1, _ => "RLCA") },
        {
            0x08,
            (3, cpu => $"LD (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}), SP")
        },
        { 0x09, (1, _ => "ADD HL, BC") },
        { 0x0A, (1, _ => "LD A, (BC)") },
        { 0x0B, (1, _ => "DEC BC") },
        { 0x0C, (1, _ => "INC C") },
        { 0x0D, (1, _ => "DEC C") },
        { 0x0E, (2, cpu => $"LD C, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x0F, (1, _ => "RRCA") },
        { 0x10, (2, _ => "STOP 0") },
        {
            0x11, (3, cpu => $"LD DE, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0x12, (1, _ => "LD (DE), A") },
        { 0x13, (1, _ => "INC DE") },
        { 0x14, (1, _ => "INC D") },
        { 0x15, (1, _ => "DEC D") },
        { 0x16, (2, cpu => $"LD D, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x17, (1, _ => "RLA") },
        { 0x18, (2, cpu => $"JR 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x19, (1, _ => "ADD HL, DE") },
        { 0x1A, (1, _ => "LD A, (DE)") },
        { 0x1B, (1, _ => "DEC DE") },
        { 0x1C, (1, _ => "INC E") },
        { 0x1D, (1, _ => "DEC E") },
        { 0x1E, (2, cpu => $"LD E, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x1F, (1, _ => "RRA") },
        { 0x20, (2, cpu => $"JR NZ, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        {
            0x21, (3, cpu => $"LD HL, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0x22, (1, _ => "LD (HL+), A") },
        { 0x23, (1, _ => "INC HL") },
        { 0x24, (1, _ => "INC H") },
        { 0x25, (1, _ => "DEC H") },
        { 0x26, (2, cpu => $"LD H, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x27, (1, _ => "DAA") },
        { 0x28, (2, cpu => $"JR Z, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x29, (1, _ => "ADD HL, HL") },
        { 0x2A, (1, _ => "LD A, (HL+)") },
        { 0x2B, (1, _ => "DEC HL") },
        { 0x2C, (1, _ => "INC L") },
        { 0x2D, (1, _ => "DEC L") },
        { 0x2E, (2, cpu => $"LD L, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x2F, (1, _ => "CPL") },
        { 0x30, (2, cpu => $"JR NC, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        {
            0x31, (3, cpu => $"LD SP, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0x32, (1, _ => "LD (HL-), A") },
        { 0x33, (1, _ => "INC SP") },
        { 0x34, (1, _ => "INC (HL)") },
        { 0x35, (1, _ => "DEC (HL)") },
        { 0x36, (2, cpu => $"LD (HL), 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x37, (1, _ => "SCF") },
        { 0x38, (2, cpu => $"JR C, ${cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x39, (1, _ => "ADD HL, SP") },
        { 0x3A, (1, _ => "LD A, (HL-)") },
        { 0x3B, (1, _ => "DEC SP") },
        { 0x3C, (1, _ => "INC A") },
        { 0x3D, (1, _ => "DEC A") },
        { 0x3E, (2, cpu => $"LD A, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0x3F, (1, _ => "CCF") },
        { 0x40, (1, _ => "LD B, B") },
        { 0x41, (1, _ => "LD B, C") },
        { 0x42, (1, _ => "LD B, D") },
        { 0x43, (1, _ => "LD B, E") },
        { 0x44, (1, _ => "LD B, H") },
        { 0x45, (1, _ => "LD B, L") },
        { 0x46, (1, _ => "LD B, (HL)") },
        { 0x47, (1, _ => "LD B, A") },
        { 0x48, (1, _ => "LD C, B") },
        { 0x49, (1, _ => "LD C, C") },
        { 0x4A, (1, _ => "LD C, D") },
        { 0x4B, (1, _ => "LD C, E") },
        { 0x4C, (1, _ => "LD C, H") },
        { 0x4D, (1, _ => "LD C, L") },
        { 0x4E, (1, _ => "LD C, (HL)") },
        { 0x4F, (1, _ => "LD C, A") },
        { 0x50, (1, _ => "LD D, B") },
        { 0x51, (1, _ => "LD D, C") },
        { 0x52, (1, _ => "LD D, D") },
        { 0x53, (1, _ => "LD D, E") },
        { 0x54, (1, _ => "LD D, H") },
        { 0x55, (1, _ => "LD D, L") },
        { 0x56, (1, _ => "LD D, (HL)") },
        { 0x57, (1, _ => "LD D, A") },
        { 0x58, (1, _ => "LD E, B") },
        { 0x59, (1, _ => "LD E, C") },
        { 0x5A, (1, _ => "LD E, D") },
        { 0x5B, (1, _ => "LD E, E") },
        { 0x5C, (1, _ => "LD E, H") },
        { 0x5D, (1, _ => "LD E, L") },
        { 0x5E, (1, _ => "LD E, (HL)") },
        { 0x5F, (1, _ => "LD E, A") },
        { 0x60, (1, _ => "LD H, B") },
        { 0x61, (1, _ => "LD H, C") },
        { 0x62, (1, _ => "LD H, D") },
        { 0x63, (1, _ => "LD H, E") },
        { 0x64, (1, _ => "LD H, H") },
        { 0x65, (1, _ => "LD H, L") },
        { 0x66, (1, _ => "LD H, (HL)") },
        { 0x67, (1, _ => "LD H, A") },
        { 0x68, (1, _ => "LD L, B") },
        { 0x69, (1, _ => "LD L, C") },
        { 0x6A, (1, _ => "LD L, D") },
        { 0x6B, (1, _ => "LD L, E") },
        { 0x6C, (1, _ => "LD L, H") },
        { 0x6D, (1, _ => "LD L, L") },
        { 0x6E, (1, _ => "LD L, (HL)") },
        { 0x6F, (1, _ => "LD L, A") },
        { 0x70, (1, _ => "LD (HL), B") },
        { 0x71, (1, _ => "LD (HL), C") },
        { 0x72, (1, _ => "LD (HL), D") },
        { 0x73, (1, _ => "LD (HL), E") },
        { 0x74, (1, _ => "LD (HL), H") },
        { 0x75, (1, _ => "LD (HL), L") },
        { 0x76, (1, _ => "HALT") },
        { 0x77, (1, _ => "LD (HL), A") },
        { 0x78, (1, _ => "LD A, B") },
        { 0x79, (1, _ => "LD A, C") },
        { 0x7A, (1, _ => "LD A, D") },
        { 0x7B, (1, _ => "LD A, E") },
        { 0x7C, (1, _ => "LD A, H") },
        { 0x7D, (1, _ => "LD A, L") },
        {
            0x7E, (1, cpu => $"LD A, (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})")
        },
        { 0x7F, (1, _ => "LD A, A") },
        { 0x80, (1, _ => "ADD A, B") },
        { 0x81, (1, _ => "ADD A, C") },
        { 0x82, (1, _ => "ADD A, D") },
        { 0x83, (1, _ => "ADD A, E") },
        { 0x84, (1, _ => "ADD A, H") },
        { 0x85, (1, _ => "ADD A, L") },
        {
            0x86,
            (1, cpu => $"ADD A, (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})")
        },
        { 0x87, (1, _ => "ADD A, A") },
        { 0x88, (1, _ => "ADC A, B") },
        { 0x89, (1, _ => "ADC A, C") },
        { 0x8A, (1, _ => "ADC A, D") },
        { 0x8B, (1, _ => "ADC A, E") },
        { 0x8C, (1, _ => "ADC A, H") },
        { 0x8D, (1, _ => "ADC A, L") },
        {
            0x8E,
            (1, cpu => $"ADC A, (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})")
        },
        { 0x8F, (1, _ => "ADC A, A") },
        { 0x90, (1, _ => "SUB B") },
        { 0x91, (1, _ => "SUB C") },
        { 0x92, (1, _ => "SUB D") },
        { 0x93, (1, _ => "SUB E") },
        { 0x94, (1, _ => "SUB H") },
        { 0x95, (1, _ => "SUB L") },
        { 0x96, (1, cpu => $"SUB (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})") },
        { 0x97, (1, _ => "SUB A") },
        { 0x98, (1, _ => "SBC A, B") },
        { 0x99, (1, _ => "SBC A, C") },
        { 0x9A, (1, _ => "SBC A, D") },
        { 0x9B, (1, _ => "SBC A, E") },
        { 0x9C, (1, _ => "SBC A, H") },
        { 0x9D, (1, _ => "SBC A, L") },
        {
            0x9E,
            (1, cpu => $"SBC A, (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})")
        },
        { 0x9F, (1, _ => "SBC A, A") },
        { 0xA0, (1, _ => "AND B") },
        { 0xA1, (1, _ => "AND C") },
        { 0xA2, (1, _ => "AND D") },
        { 0xA3, (1, _ => "AND E") },
        { 0xA4, (1, _ => "AND H") },
        { 0xA5, (1, _ => "AND L") },
        { 0xA6, (1, cpu => $"AND (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})") },
        { 0xA7, (1, _ => "AND A") },
        { 0xA8, (1, _ => "XOR B") },
        { 0xA9, (1, _ => "XOR C") },
        { 0xAA, (1, _ => "XOR D") },
        { 0xAB, (1, _ => "XOR E") },
        { 0xAC, (1, _ => "XOR H") },
        { 0xAD, (1, _ => "XOR L") },
        { 0xAE, (1, cpu => $"XOR (${cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})") },
        { 0xAF, (1, _ => "XOR A") },
        { 0xB0, (1, _ => "OR B") },
        { 0xB1, (1, _ => "OR C") },
        { 0xB2, (1, _ => "OR D") },
        { 0xB3, (1, _ => "OR E") },
        { 0xB4, (1, _ => "OR H") },

        { 0xB5, (1, _ => "OR L") },
        { 0xB6, (1, _ => "OR (HL)") },
        { 0xB7, (1, _ => "OR A") },
        { 0xB8, (1, _ => "CP B") },
        { 0xB9, (1, _ => "CP C") },
        { 0xBA, (1, _ => "CP D") },
        { 0xBB, (1, _ => "CP E") },
        { 0xBC, (1, _ => "CP H") },
        { 0xBD, (1, _ => "CP L") },
        { 0xBE, (1, _ => "CP (HL)") },
        { 0xBF, (1, _ => "CP A") },
        { 0xC0, (1, _ => "RET NZ") },
        { 0xC1, (1, _ => "POP BC") },
        {
            0xC2, (3, cpu => $"JP NZ, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0xC3, (3, cpu => $"JP 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        {
            0xC4,
            (3, cpu => $"CALL NZ, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0xC5, (1, _ => "PUSH BC") },
        { 0xC6, (2, cpu => $"ADD A, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xC7, (1, _ => "RST 00h") },
        { 0xC8, (1, _ => "RET Z") },
        { 0xC9, (1, _ => "RET") },
        { 0xCA, (3, cpu => $"JP Z, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xCB, (1, _ => "PREFIX CB") },
        {
            0xCC,
            (3, cpu => $"CALL Z, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0xCD, (3, cpu => $"CALL 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xCE, (2, cpu => $"ADC A, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xCF, (1, _ => "RST 08h") },
        { 0xD0, (1, _ => "RET NC") },
        { 0xD1, (1, _ => "POP DE") },
        {
            0xD2, (3, cpu => $"JP NC, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        {
            0xD4,
            (3, cpu => $"CALL NC, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0xD5, (1, _ => "PUSH DE") },
        { 0xD6, (2, cpu => $"SUB 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xD7, (1, _ => "RST 10h") },
        { 0xD8, (1, _ => "RET C") },
        { 0xD9, (1, _ => "RETI") },
        { 0xDA, (3, cpu => $"JP C, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        {
            0xDC,
            (3, cpu => $"CALL C, 0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}")
        },
        { 0xDE, (2, cpu => $"SBC A, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xDF, (1, _ => "RST 18h") },
        { 0xE0, (2, cpu => $"LDH (0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}), A") },
        { 0xE1, (1, _ => "POP HL") },
        { 0xE2, (2, _ => "LD (C), A") },
        { 0xE5, (1, _ => "PUSH HL") },
        { 0xE6, (2, cpu => $"AND 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xE7, (1, _ => "RST 20h") },
        { 0xE8, (2, cpu => $"ADD SP, 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xE9, (1, _ => "JP (HL)") },
        {
            0xEA,
            (3, cpu => $"LD (0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2}), A")
        },
        { 0xEE, (2, cpu => $"XOR 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xEF, (1, _ => "RST 28h") },
        { 0xF0, (2, cpu => $"LDH A, (0x{cpu.Memory[cpu.ProgramCounter + 1]:X2})") },
        { 0xF1, (1, _ => "POP AF") },
        { 0xF2, (2, _ => "LD A, (C)") },
        { 0xF3, (1, _ => "DI") },
        { 0xF5, (1, _ => "PUSH AF") },
        { 0xF6, (2, cpu => $"OR 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xF7, (1, _ => "RST 30h") },
        { 0xF8, (2, cpu => $"LD HL, SP+0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xF9, (1, _ => "LD SP, HL") },
        {
            0xFA,
            (3, cpu => $"LD A, (0x{cpu.Memory[cpu.ProgramCounter + 2]:X2}{cpu.Memory[cpu.ProgramCounter + 1]:X2})")
        },
        { 0xFB, (1, _ => "EI") },
        { 0xFE, (2, cpu => $"CP 0x{cpu.Memory[cpu.ProgramCounter + 1]:X2}") },
        { 0xFF, (1, _ => "RST 38h") }
    };

    public string[] PrefixCBOpNames = 
    {
        "RLC B", "RLC C", "RLC D", "RLC E", "RLC H", "RLC L", "RLC (HL)", "RLC A", "RRC B", "RRC C", "RRC D", "RRC E",
        "RRC H", "RRC L", "RRC (HL)", "RRC A", "RL B", "RL C", "RL D", "RL E", "RL H", "RL L", "RL (HL)", "RL A",
        "RR B",
        "RR C", "RR D", "RR E", "RR H", "RR L", "RR (HL)", "RR A", "SLA B", "SLA C", "SLA D", "SLA E", "SLA H", "SLA L",
        "SLA (HL)", "SLA A", "SRA B", "SRA C", "SRA D", "SRA E", "SRA H", "SRA L", "SRA (HL)", "SRA A", "SWAP B",
        "SWAP C",
        "SWAP D", "SWAP E", "SWAP H", "SWAP L", "SWAP (HL)", "SWAP A", "SRL B", "SRL C", "SRL D", "SRL E", "SRL H",
        "SRL L",
        "SRL (HL)", "SRL A", "BIT 0,B", "BIT 0,C", "BIT 0,D", "BIT 0,E", "BIT 0,H", "BIT 0,L", "BIT 0,(HL)", "BIT 0,A",
        "BIT 1,B", "BIT 1,C", "BIT 1,D", "BIT 1,E", "BIT 1,H", "BIT 1,L", "BIT 1,(HL)", "BIT 1,A", "BIT 2,B", "BIT 2,C",
        "BIT 2,D", "BIT 2,E", "BIT 2,H", "BIT 2,L", "BIT 2,(HL)", "BIT 2,A", "BIT 3,B", "BIT 3,C", "BIT 3,D", "BIT 3,E",
        "BIT 3,H", "BIT 3,L", "BIT 3,(HL)", "BIT 3,A", "BIT 4,B", "BIT 4,C", "BIT 4,D", "BIT 4,E", "BIT 4,H", "BIT 4,L",
        "BIT 4,(HL)", "BIT 4,A", "BIT 5,B", "BIT 5,C", "BIT 5,D", "BIT 5,E", "BIT 5,H", "BIT 5,L", "BIT 5,(HL)",
        "BIT 5,A",
        "BIT 6,B", "BIT 6,C", "BIT 6,D", "BIT 6,E", "BIT 6,H", "BIT 6,L", "BIT 6,(HL)", "BIT 6,A", "BIT 7,B", "BIT 7,C",
        "BIT 7,D", "BIT 7,E", "BIT 7,H", "BIT 7,L", "BIT 7,(HL)", "BIT 7,A", "RES 0,B", "RES 0,C", "RES 0,D", "RES 0,E",
        "RES 0,H", "RES 0,L", "RES 0,(HL)", "RES 0,A", "RES 1,B", "RES 1,C", "RES 1,D", "RES 1,E", "RES 1,H", "RES 1,L",
        "RES 1,(HL)", "RES 1,A", "RES 2,B", "RES 2,C", "RES 2,D", "RES 2,E", "RES 2,H", "RES 2,L", "RES 2,(HL)",
        "RES 2,A",
        "RES 3,B", "RES 3,C", "RES 3,D", "RES 3,E", "RES 3,H", "RES 3,L", "RES 3,(HL)", "RES 3,A", "RES 4,B", "RES 4,C",
        "RES 4,D", "RES 4,E", "RES 4,H", "RES 4,L", "RES 4,(HL)", "RES 4,A", "RES 5,B", "RES 5,C", "RES 5,D", "RES 5,E",
        "RES 5,H", "RES 5,L", "RES 5,(HL)", "RES 5,A", "RES 6,B", "RES 6,C", "RES 6,D", "RES 6,E", "RES 6,H", "RES 6,L",
        "RES 6,(HL)", "RES 6,A", "RES 7,B", "RES 7,C", "RES 7,D", "RES 7,E", "RES 7,H", "RES 7,L", "RES 7,(HL)",
        "RES 7,A",
        "SET 0,B", "SET 0,C", "SET 0,D", "SET 0,E", "SET 0,H", "SET 0,L", "SET 0,(HL)", "SET 0,A", "SET 1,B", "SET 1,C",
        "SET 1,D", "SET 1,E", "SET 1,H", "SET 1,L", "SET 1,(HL)", "SET 1,A", "SET 2,B", "SET 2,C", "SET 2,D", "SET 2,E",
        "SET 2,H", "SET 2,L", "SET 2,(HL)", "SET 2,A", "SET 3,B", "SET 3,C", "SET 3,D", "SET 3,E", "SET 3,H", "SET 3,L",
        "SET 3,(HL)", "SET 3,A", "SET 4,B", "SET 4,C", "SET 4,D", "SET 4,E", "SET 4,H", "SET 4,L", "SET 4,(HL)",
        "SET 4,A",
        "SET 5,B", "SET 5,C", "SET 5,D", "SET 5,E", "SET 5,H", "SET 5,L", "SET 5,(HL)", "SET 5,A", "SET 6,B", "SET 6,C",
        "SET 6,D", "SET 6,E", "SET 6,H", "SET 6,L", "SET 6,(HL)", "SET 6,A", "SET 7,B", "SET 7,C", "SET 7,D", "SET 7,E",
        "SET 7,H", "SET 7,L", "SET 7,(HL)", "SET 7,A"
    };
}