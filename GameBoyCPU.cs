public class GameBoyCPU
{
    public byte A, B, C, D, E, H, L;
    public ushort PC, SP;
    public bool Zero, Carry, Sign, Parity, HalfCarry;
    public byte[] Memory = new byte[0x10000];
    public bool StopCPU;

    private static bool GetBit(byte value, int position)
    {
        return (value & (1 << position)) != 0;
    }

    private static bool IsParity(byte value)
    {
        int size = sizeof(byte) * 8;
        int i;
        int p = 0;
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
                ExecuteNOP();
                break;

            case 0x01: //LD BC, nn
                ExecuteLdBCnn();
                break;

            case 0x02: //LD (BC), A
                ExecuteLdBCA();
                break;

            case 0x03: //INC BC
                ExecuteIncBC();
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
                ExecuteLdnnSP();
                break;

            case 0x09: //ADD HL, BC
                ExecuteAddHLBC();
                break;

            case 0x0A: //LD A, (BC)
                ExecuteLdABC();
                break;

            case 0x0B: //DEC BC
                ExecuteDecBC();
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
                ExecuteLdDEA();
                break;

            case 0x13: //INC DE
                ExecuteIncDE();
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
                ExecuteAddHLDE();
                break;

            case 0x1A: //LD A, (DE)
                ExecuteLdADE();
                break;

            case 0x1B: //DEC DE
                ExecuteDecDE();
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
                ExecuteIncHL();
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
                ExecuteAddHLHL();
                break;

            case 0x2A: //LD A, (HL+)
                ExecuteLdAHLPlus();
                break;

            case 0x2B: //DEC HL
                ExecuteDecHL();
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
                ExecuteJrCE();
                break;

            case 0x39: //ADD HL, SP
                ExecuteAddHLSP();
                break;

            case 0x3A: //LD A, (HL-)
                ExecuteLdAHLMinus();
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
                ExecuteLdBC();
                break;

            case 0x42: //LD B, D
                ExecuteLdBD();
                break;

            case 0x43: //LD B, E
                ExecuteLdBE();
                break;

            case 0x44: //LD B, H
                ExecuteLdBH();
                break;

            case 0x45: //LD B, L
                ExecuteLdBL();
                break;

            case 0x46: //LD B, (HL)
                ExecuteLdBHL();
                break;

            case 0x47: //LD B, A
                ExecuteLdBA();
                break;

            case 0x48: //LD C, B
                ExecuteLdCB();
                break;

            case 0x49: //LD C, C
                ExecuteLdCC();
                break;

            case 0x4A: //LD C, D
                ExecuteLdCD();
                break;

            case 0x4B: //LD C, E
                ExecuteLdCE();
                break;

            case 0x4C: //LD C, H
                ExecuteLdCH();
                break;

            case 0x4D: //LD C, L
                ExecuteLdCL();
                break;

            case 0x4E: //LD C, (HL)
                ExecuteLdCHL();
                break;

            case 0x4F: //LD C, A
                ExecuteLdCA();
                break;

            case 0x50: //LD D, B
                ExecuteLdDB();
                break;

            case 0x51: //LD D, C
                ExecuteLdDC();
                break;

            case 0x52: //LD D, D
                ExecuteLdDD();
                break;

            case 0x53: //LD D, E
                ExecuteLdDE();
                break;

            case 0x54: //LD D, H
                ExecuteLdDH();
                break;

            case 0x55: //LD D, L
                ExecuteLdDL();
                break;

            case 0x56: //LD D, (HL)
                ExecuteLdDHL();
                break;

            case 0x57: //LD D, A
                ExecuteLdDA();
                break;

            case 0x58: //LD E, B
                ExecuteLdEB();
                break;

            case 0x59: //LD E, C
                ExecuteLdEC();
                break;

            case 0x5A: //LD E, D
                ExecuteLdED();
                break;

            case 0x5B: //LD E, E
                ExecuteLdEE();
                break;

            case 0x5C: //LD E, H
                ExecuteLdEH();
                break;

            case 0x5D: //LD E, L
                ExecuteLdEL();
                break;

            case 0x5E: //LD E, (HL)
                ExecuteLdEHL();
                break;

            case 0x5F: //LD E, A
                ExecuteLdEA();
                break;

            case 0x60: //LD H, B
                ExecuteLdHB();
                break;

            case 0x61: //LD H, C
                ExecuteLdHC();
                break;

            case 0x62: //LD H, D
                ExecuteLdHD();
                break;

            case 0x63: //LD H, E
                ExecuteLdHE();
                break;

            case 0x64: //LD H, H
                ExecuteLdHH();
                break;

            case 0x65: //LD H, L
                ExecuteLdHL();
                break;

            case 0x66: //LD H, (HL)
                ExecuteLdHHL();
                break;

            case 0x67: //LD H, A
                ExecuteLdHA();
                break;

            case 0x68: //LD L, B
                ExecuteLdLb();
                break;

            case 0x69: //LD L, C
                ExecuteLdLC();
                break;

            case 0x6A: //LD L, D
                ExecuteLdLD();
                break;

            case 0x6B: //LD L, E
                ExecuteLdLE();
                break;

            case 0x6C: //LD L, H
                ExecuteLdLH();
                break;

            case 0x6D: //LD L, L
                ExecuteLdLL();
                break;

            case 0x6E: //LD L, (HL)
                ExecuteLdLHL();
                break;

            case 0x6F: //LD L, A
                ExecuteLdLA();
                break;

            case 0x70: //LD (HL), B
                ExecuteLdHLB();
                break;

            case 0x71: //LD (HL), C
                ExecuteLdHLC();
                break;

            case 0x72: //LD (HL), D
                ExecuteLdHLD();
                break;

            case 0x73: //LD (HL), E
                ExecuteLdHLE();
                break;

            case 0x74: //LD (HL), H
                ExecuteLdHLH();
                break;

            case 0x75: //LD (HL), L
                ExecuteLdHLL();
                break;

            case 0x76: //HALT
                ExecuteHalt();
                break;

            case 0x77: //LD (HL), A
                ExecuteLdHLA();
                break;

            case 0x78: //LD A, B
                ExecuteLdAB();
                break;

            case 0x79: //LD A, C
                ExecuteLdAC();
                break;

            case 0x7A: //LD A, D
                ExecuteLdAD();
                break;

            case 0x7B: //LD A, E
                ExecuteLdAE();
                break;

            case 0x7C: //LD A, H
                ExecuteLdAH();
                break;


            case 0x7D: //LD A, L
                ExecuteLdAL();
                break;

            case 0x7E: //LD A, (HL)
                ExecuteLdAHL();
                break;

            case 0x7F: //LD A, A
                ExecuteLdAA();
                break;

            case 0x80: //ADD A, B
                ExecuteAddAB();
                break;

            case 0x81: //ADD A, C
                ExecuteAddAC();
                break;

            case 0x82: //ADD A, D
                ExecuteAddAD();
                break;

            case 0x83: //ADD A, E
                ExecuteAddAE();
                break;

            case 0x84: //ADD A, H
                ExecuteAddAH();
                break;

            case 0x85: //ADD A, L
                ExecuteAddAL();
                break;

            case 0x86: //ADD A, (HL)
                ExecuteAddAHL();
                break;

            case 0x87: //ADD A, A
                ExecuteAddAA();
                break;

            case 0x88: //ADC A, B
                ExecuteAdcAB();
                break;

            case 0x89: //ADC A, C
                ExecuteAdcAC();
                break;

            case 0x8A: //ADC A, D
                ExecuteAdcAD();
                break;

            case 0x8B: //ADC A, E
                ExecuteAdcAE();
                break;

            case 0x8C: //ADC A, H
                ExecuteAdcAH();
                break;

            case 0x8D: //ADC A, L
                ExecuteAdcAL();
                break;

            case 0x8E: //ADC A, (HL)
                ExecuteAdcAHL();
                break;

            case 0x8F: //ADC A, A
                ExecuteAdcAA();
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
                ExecuteSubHL();
                break;

            case 0x97: //SUB A, A
                ExecuteSubA();
                break;

            case 0x98: //SBC A, B
                ExecuteSbcAB();
                break;

            case 0x99: //SBC A, C
                ExecuteSbcAC();
                break;

            case 0x9A: //SBC A, D
                ExecuteSbcAD();
                break;

            case 0x9B: //SBC A, E
                ExecuteSbcAE();
                break;

            case 0x9C: //SBC A, H
                ExecuteSbcAH();
                break;

            case 0x9D: //SBC A, L
                ExecuteSbcAL();
                break;

            case 0x9E: //SBC A, (HL)
                ExecuteSubAHL();
                break;

            case 0x9F: //SBC A, A
                ExecuteSbcAA();
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
                ExecuteAndHL();
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
                ExecuteXorHL();
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
                ExecuteOrHL();
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
                ExecuteCpHL();
                break;

            case 0xBF: //CP A, A
                ExecuteCpA();
                break;

            case 0xC0: //RET NZ
                ExecuteRetNZ();
                break;

            case 0xC1: //POP BC
                ExecutePopBC();
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
                ExecutePushBC();
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
                ExecutePrefixCB();
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
                ExecuteRetNC();
                break;

            case 0xD1: //POP DE
                ExecutePopDE();
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
                ExecutePushDE();
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

            case 0xDB: //XX
                ExecuteNOP();
                break;

            case 0xDC: //CALL C, nn
                ExecuteCallCnn();
                break;

            case 0xDD: //XX
                ExecuteNOP();
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
                ExecutePopHL();
                break;

            case 0xE2: //LD (C), A
                ExecuteLdCA();
                break;

            case 0xE3: //XX
                ExecuteNOP();
                break;

            case 0xE4: //XX
                ExecuteNOP();
                break;

            case 0xE5: //PUSH HL
                ExecutePushHL();
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
                ExecuteJpHL();
                break;

            case 0xEA: //LD (nn), A
                ExecuteLdnnA();
                break;

            case 0xEB: // XX
                ExecuteNOP();
                break;

            case 0xEC: // XX
                ExecuteNOP();
                break;

            case 0xED: // XX
                ExecuteNOP();
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
                ExecutePopAF();
                break;

            case 0xF2: //LD A, (C)
                ExecuteLdAC();
                break;

            case 0xF3: //DI
                ExecuteDI();
                break;

            case 0xF4: // XX
                ExecuteNOP();
                break;

            case 0xF5: //PUSH BC
                ExecutePushAF();
                break;

            case 0xF6: //OR A, n
                ExecuteOrn();
                break;

            case 0xF7: //RST 30
                ExecuteRst30();
                break;

            case 0xF8: //LD HL, SP+r8
                ExecuteLdHLSPn();
                break;

            case 0xF9: //LD SP, HL
                ExecuteLdSPHL();
                break;

            case 0xFA: //LD A, (nn)
                ExecuteLdAnn();
                break;

            case 0xFB: //EI
                ExecuteEI();
                break;

            case 0xFC: // XX
                ExecuteNOP();
                break;

            case 0xFD: // XX
                ExecuteNOP();
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
    private void ExecuteNOP()
    {
        PC++;
    }

    // 0x01 - LD BC, nn - Load 16-bit immediate value into BC.
    private void ExecuteLdBCnn()
    {
        C = Memory[PC + 1];
        B = Memory[PC + 2];
        PC += 3;
    }

    // 0x02 - LD (BC), A - Store A register into address pointed by BC.
    private void ExecuteLdBCA()
    {
        ushort x = (ushort)((B << 8) | C);
        Memory[x] = A;
        PC++;
    }

    // 0x03 - INC BC - Increment BC.
    private void ExecuteIncBC()
    {
        ushort bc = (ushort)((B << 8) | C);
        bc++;
        B = (byte)((bc >> 8) & 0xFF);
        C = (byte)(bc & 0xFF);
        PC++;
    }

    // 0x04 - INC B - Increment B register.
    private void ExecuteIncB()
    {
        B++;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = (B & 0x0F) == 0;
        Parity = (B & 0x01) == 0;
        PC++;
    }

    // 0x05 - DEC B - Decrement B register.
    private void ExecuteDecB()
    {
        B--;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = (B & 0x0F) == 0x0F;
        Parity = (B & 0x01) == 1;
        PC++;
    }

    // 0x06 - LD B, n - Load 8-bit immediate value into B.
    private void ExecuteLdBn()
    {
        B = Memory[PC + 1];
        PC += 2;
    }

    // 0x07 - RLCA - Rotate A register left with carry.
    private void ExecuteRlca()
    {
        byte carryOut = (byte)(A >> 7);
        A = (byte)((A << 1) | carryOut);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = carryOut == 0x01;
        Parity = false;
        PC++;
    }

    // 0x08 - LD (nn), SP - Store stack pointer at address pointed by 16-bit immediate value.
    private void ExecuteLdnnSP()
    {
        ushort address = (ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]);
        Memory[address] = (byte)(SP & 0xFF);
        Memory[address + 1] = (byte)((SP >> 8) & 0xFF);
        PC += 3;
    }

    // 0x09 - ADD HL, BC - Add BC to HL.
    private void ExecuteAddHLBC()
    {
        ushort result = (ushort)((H << 8) | L);
        ushort bc = (ushort)((B << 8) | C);
        result += bc;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (bc & 0xFFF);
        Carry = result < bc;
        Parity = (H & 0x01) == 0;
        PC++;
    }


    // 0x0C - INC C - Increment C register.
    private void ExecuteIncC()
    {
        C++;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = (C & 0x0F) == 0;
        Parity = (C & 0x01) == 0;
        PC++;
    }


    // 0x0F - RRCA - Rotate A register right with carry.
    private void ExecuteRrca()
    {
        byte carryOut = (byte)(A & 0x01);
        A = (byte)((A >> 1) | (carryOut << 7));
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = carryOut == 0x01;
        Parity = false;
        PC++;
    }


    // 0x0A - LD A, (BC) - Load A with value pointed by BC.
    private void ExecuteLdABC()
    {
        A = Memory[(B << 8) | C];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x0B - DEC BC - Decrement BC.
    private void ExecuteDecBC()
    {
        ushort bc = (ushort)((B << 8) | C);
        bc--;
        B = (byte)((bc >> 8) & 0xFF);
        C = (byte)(bc & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (B & 0x01) == 0;
        PC++;
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
        PC++;
    }

    // 0x0E - LD C, n - Load C with 8-bit immediate value.
    private void ExecuteLdCn()
    {
        C = Memory[PC + 1];
        Sign = (C >> 7) == 1;
        Zero = C == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (C & 0x01) == 0;
        PC += 2;
    }

    // 0x0F - RRCA - Rotate A right through carry.
    private void ExecuteRRCA()
    {
        byte carry = (byte)(A >> 7);
        A = (byte)((A << 1) | carry);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = (carry == 1);
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x10 - STOP 0 - Stop CPU until button pressed.
    private void ExecuteStop()
    {
        // Stop CPU until button pressed.
        PC++;
    }

    // 0x11 - LD DE, nn - Load DE with 16-bit immediate value.
    private void ExecuteLdDEnn()
    {
        D = Memory[PC + 2];
        E = Memory[PC + 1];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        PC += 3;
    }

    // 0x12 - LD (DE), A - Load A to address pointed by DE.
    private void ExecuteLdDEA()
    {
        Memory[(D << 8) | E] = A;
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x13 - INC DE - Increment DE.
    private void ExecuteIncDE()
    {
        ushort de = (ushort)((D << 8) | E);
        de++;
        D = (byte)((de >> 8) & 0xFF);
        E = (byte)(de & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        PC++;
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
        PC++;
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
        PC++;
    }

    // 0x16 - LD D, n - Load D with 8-bit immediate value.
    private void ExecuteLdDn()
    {
        D = Memory[PC + 1];
        Sign = (D >> 7) == 1;
        Zero = D == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        PC += 2;
    }

    // 0x17 - RLA - Rotate A left through carry.
    private void ExecuteRla()
    {
        byte carry = (byte)(A >> 7);
        A = (byte)((A << 1) | (Carry ? 1 : 0));
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = (carry == 1);
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x18 - JR n - Relative jump by signed immediate byte.
    private void ExecuteJrn()
    {
        sbyte n = (sbyte)Memory[PC + 1];
        PC += (ushort)(n + 2);
    }

    // 0x19 - ADD HL, DE - Add DE to HL.
    private void ExecuteAddHLDE()
    {
        ushort result = (ushort)((H << 8) | L);
        ushort de = (ushort)((D << 8) | E);
        result += de;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (de & 0xFFF);
        Carry = result < de;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x1A - LD A, (DE) - Load A with value pointed by DE.
    private void ExecuteLdADE()
    {
        A = Memory[(D << 8) | E];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x1B - DEC DE - Decrement DE.
    private void ExecuteDecDE()
    {
        ushort de = (ushort)((D << 8) | E);
        de--;
        D = (byte)((de >> 8) & 0xFF);
        E = (byte)(de & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (D & 0x01) == 0;
        PC++;
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
        PC++;
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
        PC++;
    }

    // 0x1E - LD E, n - Load E with 8-bit immediate value.
    private void ExecuteLdEn()
    {
        E = Memory[PC + 1];
        Sign = (E >> 7) == 1;
        Zero = E == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (E & 0x01) == 0;
        PC += 2;
    }

    // 0x1F - RRA - Rotate A right through carry.
    private void ExecuteRra()
    {
        byte carry = (byte)(A & 0x01);
        A = (byte)((A >> 1) | (Carry ? 0x80 : 0x00));
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = (carry == 1);
        Parity = (A & 0x01) == 0;
        PC++;
    }


    // 0x20 - JR NZ, n - Relative jump by signed immediate byte if Zero flag is not set.
    private void ExecuteJrNZn()
    {
        if (!Zero)
        {
            sbyte n = (sbyte)Memory[PC + 1];
            PC += (ushort)(n + 2);
        }
        else
        {
            PC += 2;
        }
    }

    // 0x21 - LD HL, nn - Load HL with 16-bit immediate value.
    private void ExecuteLdHLnn()
    {
        H = Memory[PC + 2];
        L = Memory[PC + 1];
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        PC += 3;
    }

    // 0x22 - LD (HL+), A - Load A to address pointed by HL, then increment HL.
    private void ExecuteLdHLiA()

    {
        ushort hl = (ushort)((H << 8) | L);
        Memory[hl] = A;
        hl++;
        H = (byte)((hl >> 8) & 0xFF);
        L = (byte)(hl & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x23 - INC HL - Increment HL.
    private void ExecuteIncHL()
    {
        ushort hl = (ushort)((H << 8) | L);
        hl++;
        H = (byte)((hl >> 8) & 0xFF);
        L = (byte)(hl & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        PC++;
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
        PC++;
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
        PC++;
    }

    // 0x26 - LD H, n - Load H with 8-bit immediate value.
    private void ExecuteLdHn()
    {
        H = Memory[PC + 1];
        Sign = (H >> 7) == 1;
        Zero = H == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (H & 0x01) == 0;
        PC += 2;
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
        PC++;
    }

    // 0x28 - JR Z, n - Relative jump by signed immediate byte if Zero flag is set.
    private void ExecuteJrZn()
    {
        if (Zero)
        {
            sbyte n = (sbyte)Memory[PC + 1];
            PC += (ushort)(n + 2);
        }
        else
        {
            PC += 2;
        }
    }


    // 0x29 - ADD HL, HL - Add HL to HL.
    private void ExecuteAddHLHL()
    {
        ushort result = (ushort)((H << 8) | L);
        ushort hl = (ushort)((H << 8) | L);
        result += hl;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (hl & 0xFFF);
        Carry = result < hl;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x2A - LD A, (HL+) - Load A from memory address HL then increment HL.
    private void ExecuteLdAHLPlus()
    {
        ushort address = (ushort)((H << 8) | L);
        A = Memory[address];
        ushort result = (ushort)(address + 1);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        PC++;
    }

    // 0x2B - DEC HL - Decrement HL.
    private void ExecuteDecHL()
    {
        ushort result = (ushort)((H << 8) | L);
        result--;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        PC++;
    }

    // 0x2C - INC L - Increment L.
    private void ExecuteIncL()
    {
        L++;
        Sign = (L & 0x80) != 0;
        Zero = (L == 0);
        HalfCarry = (L & 0x0F) == 0;
        Parity = (L & 0x01) == 0;
        PC++;
    }

    // 0x2D - DEC L - Decrement L.
    private void ExecuteDecL()
    {
        L--;
        Sign = (L & 0x80) != 0;
        Zero = (L == 0);
        HalfCarry = (L & 0x0F) == 0;
        Parity = (L & 0x01) == 0;
        PC++;
    }

    // 0x2E - LD L, n - Load 8-bit immediate into L.
    private void ExecuteLdLn()
    {
        L = Memory[PC + 1];
        Sign = (L & 0x80) != 0;
        Zero = (L == 0);
        HalfCarry = (L & 0x0F) == 0;
        Parity = (L & 0x01) == 0;
        PC += 2;
    }

    // 0x2F - CPL - Complement A register.
    private void ExecuteCpl()
    {
        A = (byte)~A;
        Sign = true;
        HalfCarry = true;
        PC++;
    }

    // 0x30 - JR NC, e - Jump relative if Carry flag is not set.
    private void ExecuteJrNcE()
    {
        if (!Carry)
        {
            sbyte offset = (sbyte)Memory[PC + 1];
            PC += (ushort)(offset + 2);
        }
        else
        {
            PC += 2;
        }
    }

    // 0x31 - LD SP, nn - Load 16-bit immediate into Stack Pointer.
    private void ExecuteLdSpNn()
    {
        ushort value = (ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]);
        SP = value;
        PC += 3;
    }

    // 0x32 - LD (HL-), A - Store A at address HL then decrement HL.
    private void ExecuteLdHlMinusA()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = A;
        ushort result = (ushort)(address - 1);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        PC++;
    }

    // 0x33 - INC SP - Increment Stack Pointer.
    private void ExecuteIncSp()
    {
        SP++;
        PC++;
    }

    // 0x34 - INC (HL) - Increment value at address HL.
    private void ExecuteIncHl()
    {
        ushort address = (ushort)((H << 8) | L);
        byte value = Memory[address];
        value++;
        Memory[address] = value;
        Sign = (value & 0x80) != 0;
        Zero = (value == 0);
        HalfCarry = (value & 0x0F) == 0;
        Parity = (value & 0x01) == 0;
        PC++;
    }

    // 0x35 - DEC (HL) - Decrement value at address HL.
    private void ExecuteDecHl()
    {
        ushort address = (ushort)((H << 8) | L);
        byte value = Memory[address];
        value--;
        Memory[address] = value;
        Sign = (value & 0x80) != 0;
        Zero = (value == 0);
        HalfCarry = (value & 0x0F) == 0;
        Parity = (value & 0x01) == 0;
        PC++;
    }

    // 0x36 - LD (HL), n - Load 8-bit immediate into address HL.
    private void ExecuteLdHln()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = Memory[PC + 1];
        PC += 2;
    }

    // 0x37 - SCF - Set Carry flag.
    private void ExecuteScf()
    {
        Carry = true;
        Sign = false;
        HalfCarry = false;
        PC++;
    }

    // 0x38 - JR C, e - Jump relative if Carry flag is set.
    private void ExecuteJrCE()
    {
        if (Carry)
        {
            sbyte offset = (sbyte)Memory[PC + 1];
            PC += (ushort)(offset + 2);
        }
        else
        {
            PC += 2;
        }
    }

    // 0x39 - ADD HL, SP - Add Stack Pointer to HL.
    private void ExecuteAddHLSP()
    {
        ushort result = (ushort)((H << 8) | L);
        result += SP;
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (result & 0xFFF) < (SP & 0xFFF);
        Carry = result < SP;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x3A - LD A, (HL-) - Load A from memory address HL then decrement HL.
    private void ExecuteLdAHLMinus()
    {
        ushort address = (ushort)((H << 8) | L);
        A = Memory[address];
        ushort result = (ushort)(address - 1);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        PC++;
    }

    // 0x3B - DEC SP - Decrement Stack Pointer.
    private void ExecuteDecSp()
    {
        SP--;
        PC++;
    }

    // 0x3C - INC A - Increment A.
    private void ExecuteIncA()
    {
        A++;
        Sign = (A & 0x80) != 0;
        Zero = (A == 0);
        HalfCarry = (A & 0x0F) == 0;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x3D - DEC A - Decrement A.
    private void ExecuteDecA()
    {
        A--;
        Sign = (A & 0x80) != 0;
        Zero = (A == 0);
        HalfCarry = (A & 0x0F) == 0;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x3E - LD A, n - Load 8-bit immediate into A.
    private void ExecuteLdAn()
    {
        A = Memory[PC + 1];
        Sign = (A & 0x80) != 0;
        Zero = (A == 0);
        HalfCarry = (A & 0x0F) == 0;
        Parity = (A & 0x01) == 0;
        PC += 2;
    }


    // 0x3F - CCF - Complement carry flag.
    private void ExecuteCcf()
    {
        Carry = !Carry;
        Sign = false;
        HalfCarry = false;
        Zero = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x40 - LD B, B - Load B with B.
    private void ExecuteLdBb()
    {
        B = B;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x41 - LD B, C - Load B with C.
    private void ExecuteLdBC()
    {
        B = C;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x42 - LD B, D - Load B with D.
    private void ExecuteLdBD()
    {
        B = D;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x43 - LD B, E - Load B with E.
    private void ExecuteLdBE()
    {
        B = E;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x44 - LD B, H - Load B with H.
    private void ExecuteLdBH()
    {
        B = H;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x45 - LD B, L - Load B with L.
    private void ExecuteLdBL()
    {
        B = L;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x46 - LD B, (HL) - Load B with value pointed by HL.
    private void ExecuteLdBHL()
    {
        ushort hl = (ushort)((H << 8) | L);
        B = Memory[hl];
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x47 - LD B, A - Load B with A.
    private void ExecuteLdBA()
    {
        B = A;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x48 - LD C, B - Load C with B.
    private void ExecuteLdCB()
    {
        C = B;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x49 - LD C, C - Load C with C.
    private void ExecuteLdCC()
    {
        C = C;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x4A - LD C, D - Load C with D.
    private void ExecuteLdCD()
    {
        C = D;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x4B - LD C, E - Load C with E.
    private void ExecuteLdCE()
    {
        C = E;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x4C - LD C, H - Load C with H.
    private void ExecuteLdCH()
    {
        C = H;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x4D - LD C, L - Load C with L.
    private void ExecuteLdCL()
    {
        C = L;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x4E - LD C, (HL) - Load C with value pointed by HL.
    private void ExecuteLdCHL()
    {
        ushort hl = (ushort)((H << 8) | L);
        C = Memory[hl];
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x4F - LD C, A - Load C with A.
    private void ExecuteLdCA()
    {
        C = A;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x50 - LD D, B - Load D with B.
    private void ExecuteLdDB()
    {
        D = B;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x51 - LD D, C - Load D with C.
    private void ExecuteLdDC()
    {
        D = C;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x52 - LD D, D - Load D with D.
    private void ExecuteLdDD()
    {
        D = D;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x53 - LD D, E - Load D with E.
    private void ExecuteLdDE()
    {
        D = E;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x54 - LD D, H - Load D with H.
    private void ExecuteLdDH()
    {
        D = H;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x55 - LD D, L - Load D with L.
    private void ExecuteLdDL()
    {
        D = L;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x56 - LD D, (HL) - Load D with value pointed by HL.
    private void ExecuteLdDHL()
    {
        ushort hl = (ushort)((H << 8) | L);
        D = Memory[hl];
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x57 - LD D, A - Load D with A.
    private void ExecuteLdDA()
    {
        D = A;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x58 - LD E, B - Load E with B.
    private void ExecuteLdEB()
    {
        E = B;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x59 - LD E, C - Load E with C.
    private void ExecuteLdEC()
    {
        E = C;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x5A - LD E, D - Load E with D.
    private void ExecuteLdED()
    {
        E = D;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x5B - LD E, E - Load E with E.
    private void ExecuteLdEE()
    {
        E = E;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x5C - LD E, H - Load E with H.
    private void ExecuteLdEH()
    {
        E = H;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x5D - LD E, L - Load E with L.
    private void ExecuteLdEL()
    {
        E = L;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x5E - LD E, (HL) - Load E with value pointed by HL.
    private void ExecuteLdEHL()
    {
        ushort hl = (ushort)((H << 8) | L);
        E = Memory[hl];
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }


    // 0x5F - LD E, A - Load A into E
    private void ExecuteLdEA()
    {
        E = A;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x60 - LD H, B - Load B into H
    private void ExecuteLdHB()
    {
        H = B;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (B & 0x01) == 0;
        PC++;
    }

    // 0x61 - LD H, C - Load C into H
    private void ExecuteLdHC()
    {
        H = C;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (C & 0x01) == 0;
        PC++;
    }

    // 0x62 - LD H, D - Load D into H
    private void ExecuteLdHD()
    {
        H = D;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (D & 0x01) == 0;
        PC++;
    }

    // 0x63 - LD H, E - Load E into H
    private void ExecuteLdHE()
    {
        H = E;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (E & 0x01) == 0;
        PC++;
    }

    // 0x64 - LD H, H - Load H into H
    private void ExecuteLdHH()
    {
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x65 - LD H, L - Load L into H
    private void ExecuteLdHL()
    {
        H = L;
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        PC++;
    }

    // 0x66 - LD H, (HL) - Load (HL) into H
    private void ExecuteLdHHL()
    {
        ushort address = (ushort)((H << 8) | L);
        H = Memory[address];
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x67 - LD H, A - Load A into H
    private void ExecuteLdHA()
    {
        H = A;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x68 - LD L, B - Load B into L
    private void ExecuteLdLb()
    {
        L = B;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (B & 0x01) == 0;
        PC++;
    }

    // 0x69 - LD L, C - Load C into L
    private void ExecuteLdLC()
    {
        L = C;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (C & 0x01) == 0;
        PC++;
    }

    // 0x6A - LD L, D - Load D into L
    private void ExecuteLdLD()
    {
        L = D;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (D & 0x01) == 0;
        PC++;
    }

    // 0x6B - LD L, E - Load E into L
    private void ExecuteLdLE()
    {
        L = E;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (E & 0x01) == 0;
        PC++;
    }

    // 0x6C - LD L, H - Load H into L
    private void ExecuteLdLH()
    {
        L = H;
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x6D - LD L, L - Load L into L
    private void ExecuteLdLL()
    {
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        PC++;
    }

    // 0x6E - LD L, (HL) - Load (HL) into L
    private void ExecuteLdLHL()
    {
        ushort address = (ushort)((H << 8) | L);
        L = Memory[address];
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        PC++;
    }

    // 0x6F - LD L, A - Load A into L
    private void ExecuteLdLA()
    {
        L = A;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }


    // 0x70 - LD (HL), B - Load B into (HL)
    private void ExecuteLdHLB()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = B;
        PC++;
    }

    // 0x71 - LD (HL), C - Load C into (HL)
    private void ExecuteLdHLC()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = C;
        PC++;
    }

    // 0x72 - LD (HL), D - Load D into (HL)
    private void ExecuteLdHLD()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = D;
        PC++;
    }

    // 0x73 - LD (HL), E - Load E into (HL)
    private void ExecuteLdHLE()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = E;
        PC++;
    }

    // 0x74 - LD (HL), H - Load H into (HL)
    private void ExecuteLdHLH()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = H;
        PC++;
    }

    // 0x75 - LD (HL), L - Load L into (HL)
    private void ExecuteLdHLL()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = L;
        PC++;
    }

    // 0x76 - HALT - Halts the processor until an interrupt occurs.
    private void ExecuteHalt()
    {
        StopCPU = true;
        //Do nothing
        PC++;
    }

    // 0x77 - LD (HL), A - Load A into (HL)
    private void ExecuteLdHLA()
    {
        ushort address = (ushort)((H << 8) | L);
        Memory[address] = A;
        PC++;
    }

    // 0x78 - LD A, B - Load B into A
    private void ExecuteLdAB()
    {
        A = B;
        Sign = (B & 0x80) != 0;
        Zero = B == 0;
        HalfCarry = false;
        Parity = (B & 0x01) == 0;
        PC++;
    }

    // 0x79 - LD A, C - Load C into A
    private void ExecuteLdAC()
    {
        A = C;
        Sign = (C & 0x80) != 0;
        Zero = C == 0;
        HalfCarry = false;
        Parity = (C & 0x01) == 0;
        PC++;
    }

    // 0x7A - LD A, D - Load D into A
    private void ExecuteLdAD()
    {
        A = D;
        Sign = (D & 0x80) != 0;
        Zero = D == 0;
        HalfCarry = false;
        Parity = (D & 0x01) == 0;
        PC++;
    }

    // 0x7B - LD A, E - Load E into A
    private void ExecuteLdAE()
    {
        A = E;
        Sign = (E & 0x80) != 0;
        Zero = E == 0;
        HalfCarry = false;
        Parity = (E & 0x01) == 0;
        PC++;
    }

    // 0x7C - LD A, H - Load H into A
    private void ExecuteLdAH()
    {
        A = H;
        Sign = (H & 0x80) != 0;
        Zero = H == 0;
        HalfCarry = false;
        Parity = (H & 0x01) == 0;
        PC++;
    }

    // 0x7D - LD A, L - Load L into A
    private void ExecuteLdAL()
    {
        A = L;
        Sign = (L & 0x80) != 0;
        Zero = L == 0;
        HalfCarry = false;
        Parity = (L & 0x01) == 0;
        PC++;
    }

    // 0x7E - LD A, (HL) - Load (HL)
    private void ExecuteLdAHL()
    {
        ushort address = (ushort)((H << 8) | L);
        A = Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x7F - LD A, A - Load A into A
    private void ExecuteLdAA()
    {
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x80 - ADD A, B - Add B to A
    private void ExecuteAddAB()
    {
        byte result = (byte)(A + B);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (B & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x81 - ADD A, C - Add C to A
    private void ExecuteAddAC()
    {
        byte result = (byte)(A + C);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (C & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x82 - ADD A, D - Add D to A
    private void ExecuteAddAD()
    {
        byte result = (byte)(A + D);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (D & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x83 - ADD A, E - Add E to A
    private void ExecuteAddAE()
    {
        byte result = (byte)(A + E);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (E & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x84 - ADD A, H - Add H to A
    private void ExecuteAddAH()
    {
        byte result = (byte)(A + H);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (H & 0xF) > 0xF;
        Carry = result < A;
        Parity = (A & 0x01) == 0;
        PC++;
    }


    // 0x85 - ADD A, L - Add L to A.
    private void ExecuteAddAL()
    {
        byte result = (byte)(A + L);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (L & 0xF) > 0xF;
        Carry = (A + L) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x86 - ADD A, (HL) - Add (HL) to A.
    private void ExecuteAddAHL()
    {
        byte result = (byte)(A + Memory[(H << 8) | L]);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[(H << 8) | L] & 0xF) > 0xF;
        Carry = (A + Memory[(H << 8) | L]) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x87 - ADD A, A - Add A to A.
    private void ExecuteAddAA()
    {
        byte result = (byte)(A + A);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (A & 0xF) > 0xF;
        Carry = (A + A) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x88 - ADC A, B - Add B + Carry flag to A.
    private void ExecuteAdcAB()
    {
        byte result = (byte)(A + B + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (B & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + B + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x89 - ADC A, C - Add C + Carry flag to A.
    private void ExecuteAdcAC()
    {
        byte result = (byte)(A + C + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (C & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + C + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x8A - ADC A, D - Add D + Carry flag to A.
    private void ExecuteAdcAD()
    {
        byte result = (byte)(A + D + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (D & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + D + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x8B - ADC A, E - Add E + Carry flag to A.
    private void ExecuteAdcAE()
    {
        byte result = (byte)(A + E + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (E & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + E + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x8C - ADC A, H - Add H + Carry flag to A.
    private void ExecuteAdcAH()
    {
        byte result = (byte)(A + H + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (H & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + H + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x8D - ADC A, L - Add L + Carry flag to A.
    private void ExecuteAdcAL()
    {
        byte result = (byte)(A + L + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (L & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + L + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x8E - ADC A, (HL) - Add (HL) + Carry flag to A.
    private void ExecuteAdcAHL()
    {
        byte result = (byte)(A + Memory[(H << 8) | L] + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[(H << 8) | L] & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + Memory[(H << 8) | L] + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x8F - ADC A, A - Add A + Carry flag to A.
    private void ExecuteAdcAA()
    {
        byte result = (byte)(A + A + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (A & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = (A + A + (Carry ? 1 : 0)) > 0xFF;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x90 - SUB B - Subtract B from A.
    private void ExecuteSubB()
    {
        byte result = (byte)(A - B);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (B & 0xF);
        Carry = A < B;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x91 - SUB C - Subtract C from A.
    private void ExecuteSubC()
    {
        byte result = (byte)(A - C);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (C & 0xF);
        Carry = A < C;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x92 - SUB D - Subtract D from A.
    private void ExecuteSubD()
    {
        byte result = (byte)(A - D);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (D & 0xF);
        Carry = A < D;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x93 - SUB E - Subtract E from A.
    private void ExecuteSubE()
    {
        byte result = (byte)(A - E);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (E & 0xF);
        Carry = A < E;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x94 - SUB H - Subtract H from A.
    private void ExecuteSubH()
    {
        byte result = (byte)(A - H);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (H & 0xF);
        Carry = A < H;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x95 - SUB L - Subtract L from A.
    private void ExecuteSubL()
    {
        byte result = (byte)(A - L);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (L & 0xF);
        Carry = A < L;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x96 - SUB (HL) - Subtract (HL) from A.
    private void ExecuteSubHL()
    {
        byte result = (byte)(A - Memory[(H << 8) | L]);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (Memory[(H << 8) | L] & 0xF);
        Carry = A < Memory[(H << 8) | L];
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x97 - SUB A - Subtract A from A.
    private void ExecuteSubA()
    {
        byte result = (byte)(A - A);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (A & 0xF);
        Carry = A < A;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x98 - SBC A, B - Subtract B + Carry flag from A.
    private void ExecuteSbcAB()
    {
        byte result = (byte)(A - B - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (B & 0xF) - (Carry ? 1 : 0);
        Carry = A < B - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x99 - SBC A, C - Subtract C + Carry flag from A.
    private void ExecuteSbcAC()
    {
        byte result = (byte)(A - C - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (C & 0xF) - (Carry ? 1 : 0);
        Carry = A < C - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x9A - SBC A, D - Subtract D + Carry flag from A.
    private void ExecuteSbcAD()
    {
        byte result = (byte)(A - D - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (D & 0xF) - (Carry ? 1 : 0);
        Carry = A < D - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x9B - SBC A, E - Subtract E + Carry flag from A.
    private void ExecuteSbcAE()
    {
        byte result = (byte)(A - E - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (E & 0xF) - (Carry ? 1 : 0);
        Carry = A < E - (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        PC++;
    }


    // 0x9C - SBC A, H - Subtract H + Carry flag from A.
    private void ExecuteSbcAH()
    {
        byte result = (byte)(A - H - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (H & 0xF) + (Carry ? 1 : 0);
        Carry = (A & 0xFF) < (H & 0xFF) + (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x9D - SBC A, L - Subtract L + Carry flag from A.
    private void ExecuteSbcAL()
    {
        byte result = (byte)(A - L - (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (L & 0xF) + (Carry ? 1 : 0);
        Carry = (A & 0xFF) < (L & 0xFF) + (Carry ? 1 : 0);
        Parity = (A & 0x01) == 0;
        PC++;
    }


    // 0x9E - SUB A, (HL) - Subtract (HL) from A.
    private void ExecuteSubAHL()
    {
        byte val = Memory[(H << 8) | L];
        A = (byte)(A - val);
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = (A & 0x0F) > (val & 0x0F);
        Carry = A < val;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0x9F - SBC A, A - Subtract A and carry flag from A.
    private void ExecuteSbcAA()
    {
        byte val = A;
        A = (byte)(A - val - (Carry ? 1 : 0));
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = (A & 0x0F) > (val & 0x0F);
        Carry = A < val;
        Parity = (A & 0x01) == 0;
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
    }

    // 0xA6 - AND (HL) - Logical AND (HL) with A.
    private void ExecuteAndHL()
    {
        byte address = (byte)((H << 8) | L);
        A &= Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0xA7 - AND A - Logical AND A with A.
    private void ExecuteAndA()
    {
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
    }

    // 0xAE - XOR (HL) - Logical XOR (HL) with A.
    private void ExecuteXorHL()
    {
        byte address = (byte)((H << 8) | L);
        A ^= Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
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
        PC++;
    }

    // 0xB6 - OR (HL) - Logical OR (HL) with A.
    private void ExecuteOrHL()
    {
        byte address = (byte)((H << 8) | L);
        A |= Memory[address];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0xB7 - OR A - Logical OR A with A.
    private void ExecuteOrA()
    {
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC++;
    }

    // 0xB8 - CP B - Compare B with A.
    private void ExecuteCpB()
    {
        byte result = (byte)(A - B);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (B & 0xF);
        Carry = (A & 0xFF) < (B & 0xFF);
        Parity = (result & 0x01) == 0;
        PC++;
    }

    // 0xB9 - CP C - Compare C with A.
    private void ExecuteCpC()
    {
        byte result = (byte)(A - C);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (C & 0xF);
        Carry = (A & 0xFF) < (C & 0xFF);
        Parity = (result & 0x01) == 0;
        PC++;
    }

    // 0xBA - CP D - Compare D with A.
    private void ExecuteCpD()
    {
        byte result = (byte)(A - D);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (D & 0xF);
        Carry = (A & 0xFF) < (D & 0xFF);
        Parity = (result & 0x01) == 0;
        PC++;
    }


    // 0xBB - CP E - Compare E with A.
    private void ExecuteCpE()
    {
        byte result = (byte)(A - E);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (E & 0xF);
        Carry = A < E;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        PC++;
    }

    // 0xBC - CP H - Compare H with A.
    private void ExecuteCpH()
    {
        byte result = (byte)(A - H);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (H & 0xF);
        Carry = A < H;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        PC++;
    }

    // 0xBD - CP L - Compare L with A.
    private void ExecuteCpL()
    {
        byte result = (byte)(A - L);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (L & 0xF);
        Carry = A < L;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        PC++;
    }

    // 0xBE - CP (HL) - Compare (HL) with A.
    private void ExecuteCpHL()
    {
        byte result = (byte)(A - Memory[(H << 8) | L]);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (Memory[(H << 8) | L] & 0xF);
        Carry = A < Memory[(H << 8) | L];
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        PC++;
    }

    // 0xBF - CP A - Compare A with A.
    private void ExecuteCpA()
    {
        byte result = (byte)(A - A);
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) < (A & 0xF);
        Carry = A < A;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        PC++;
    }

    // 0xC0 - RET NZ - Return if Z flag is not set.
    private void ExecuteRetNZ()
    {
        if (!Zero)
        {
            SP += 2;
            PC = (ushort)(Memory[SP] << 8 | Memory[SP + 1]);
        }
        else
        {
            PC++;
        }
    }

    // 0xC1 - POP BC - Pop two bytes off stack into BC.
    private void ExecutePopBC()
    {
        C = Memory[SP + 1];
        B = Memory[SP];
        SP += 2;
        PC++;
    }

    // 0xC2 - JP NZ, nn - Jump to address nn if Z flag is not set.
    private void ExecuteJpNZnn()
    {
        if (!Zero)
        {
            PC = (ushort)(Memory[PC + 2] << 8 | Memory[PC + 1]);
        }
        else
        {
            PC += 3;
        }
    }

    // 0xC3 - JP nn - Jump to address nn.
    private void ExecuteJpnn()
    {
        PC = (ushort)(Memory[PC + 2] << 8 | Memory[PC + 1]);
    }


    // 0xC4 - CALL NZ, nn - Call address nn if Z flag is not set.
    private void ExecuteCallNZnn()
    {
        if (!Zero)
        {
            SP -= 2;
            Memory[SP] = (byte)((PC + 3) >> 8);
            Memory[SP + 1] = (byte)((PC + 3) & 0xFF);
            PC = (ushort)(Memory[PC + 1] << 8 | Memory[PC + 2]);
        }
        else
        {
            PC += 3;
        }
    }

    // 0xC5 - PUSH BC - Push BC onto stack.
    private void ExecutePushBC()
    {
        Memory[SP] = B;
        Memory[SP + 1] = C;
        SP -= 2;
        PC++;
    }

    // 0xC6 - ADD A, n - Add n to A.
    private void ExecuteAddAn()
    {
        byte result = (byte)(A + Memory[PC + 1]);
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[PC + 1] & 0xF) > 0xF;
        Carry = A + Memory[PC + 1] > 0xFF;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        PC += 2;
    }

    // 0xC7 - RST 0 - Reset program counter to address 0x00.
    private void ExecuteRst0()
    {
        SP -= 2;
        Memory[SP] = (byte)(PC >> 8);
        Memory[SP + 1] = (byte)(PC & 0xFF);
        PC = 0x00;
    }

    // 0xC8 - RET Z - Return if Z flag is set.
    private void ExecuteRetZ()
    {
        if (Zero)
        {
            SP += 2;
            PC = (ushort)(Memory[SP] << 8 | Memory[SP + 1]);
        }
        else
        {
            PC++;
        }
    }

    // 0xC9 - RET - Return.
    private void ExecuteRet()
    {
        SP += 2;
        PC = (ushort)(Memory[SP] << 8 | Memory[SP + 1]);
    }

    // 0xCA - JP Z, nn - Jump to address nn if Z flag is set.
    private void ExecuteJpZnn()
    {
        if (Zero)
        {
            PC = (ushort)(Memory[PC + 1] << 8 | Memory[PC + 2]);
        }
        else
        {
            PC += 3;
        }
    }


    // 0xCC - CALL Z, nn - Call address nn if Z flag is set.
    private void ExecuteCallZnn()
    {
        if (Zero)
        {
            SP -= 2;
            Memory[SP] = (byte)((PC + 3) >> 8);
            Memory[SP + 1] = (byte)((PC + 3) & 0xFF);
            PC = (ushort)(Memory[PC + 1] << 8 | Memory[PC + 2]);
        }
        else
        {
            PC += 3;
        }
    }

    // 0xCD - CALL nn - Call address nn.
    private void ExecuteCallnn()
    {
        SP -= 2;
        Memory[SP] = (byte)((PC + 3) >> 8);
        Memory[SP + 1] = (byte)((PC + 3) & 0xFF);
        PC = (ushort)(Memory[PC + 1] << 8 | Memory[PC + 2]);
    }

    // 0xCE - ADC A, n - Add n + Carry flag to A.
    private void ExecuteAdcAn()
    {
        byte result = (byte)(A + Memory[PC + 1] + (Carry ? 1 : 0));
        A = result;
        Sign = (result & 0x80) != 0;
        Zero = result == 0;
        HalfCarry = (A & 0xF) + (Memory[PC + 1] & 0xF) + (Carry ? 1 : 0) > 0xF;
        Carry = A + Memory[PC + 1] + (Carry ? 1 : 0) > 0xFF;
        Parity = (((result & 0x1) ^ ((result & 0x2) >> 1) ^ ((result & 0x4) >> 2) ^ ((result & 0x8) >> 3) ^
                   ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^ ((result & 0x80) >> 7)) &
                  0x1) == 0;
        PC += 2;
    }

    // 0xCF - RST 8 - Push PC onto stack and jump to address 0x08.
    private void ExecuteRst8()
    {
        SP -= 2;
        Memory[SP] = (byte)(PC >> 8);
        Memory[SP + 1] = (byte)(PC & 0xFF);
        PC = 0x08;
    }

    // 0xD0 - RET NC - If last result was not carried, return from subroutine.
    private void ExecuteRetNC()
    {
        if (!Carry)
        {
            PC = (ushort)((Memory[SP] << 8) | Memory[SP + 1]);
            SP += 2;
        }
    }

    // 0xD1 - POP DE - Pop two bytes from stack into DE.
    private void ExecutePopDE()
    {
        D = Memory[SP];
        E = Memory[SP + 1];
        SP += 2;
    }

    // 0xD2 - JP NC, nn - If last result was not carried, jump to address nn.
    private void ExecuteJpNCnn()
    {
        if (!Carry)
        {
            ushort address = (ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]);
            PC = address;
        }
        else
        {
            PC += 2;
        }
    }

    // 0xD3 - OUT (n), A - Output A to port n.
    private void ExecuteOutnA()
    {
        byte port = Memory[PC + 1];
        // Output A to port n in the implementation
        Console.Write((char)port);
        PC += 2;
    }

    // 0xD4 - CALL NC, nn - If last result was not carried, call address nn.
    private void ExecuteCallNCnn()
    {
        if (!Carry)
        {
            ushort address = (ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]);
            SP -= 2;
            Memory[SP] = (byte)(PC >> 8);
            Memory[SP + 1] = (byte)(PC & 0xFF);
            PC = address;
        }
        else
        {
            PC += 2;
        }
    }

    // 0xD5 - PUSH DE - Push DE onto stack.
    private void ExecutePushDE()
    {
        SP -= 2;
        Memory[SP] = D;
        Memory[SP + 1] = E;
    }

    // 0xD6 - SUB n - Subtract n from A.
    private void ExecuteSubn()
    {
        byte n = Memory[PC + 1];
        byte result = (byte)(A - n);
        Zero = result == 0;
        Sign = result > 0x7F;
        HalfCarry = (A & 0xF) < (n & 0xF);
        int x = A;
        x = x - n;
        x = x & 0xff;
        int y = x;
        y = y ^ (x >> 4);
        y = y ^ (y >> 2);
        y = y ^ (y >> 1);
        Carry = A < n;
        Parity = (y & 0x01) != 0;
        A = result;
        PC += 2;
    }

    // 0xD7 - RST 10 - Push PC onto stack and jump to address 0x10.
    private void ExecuteRst10()
    {
        SP -= 2;
        Memory[SP] = (byte)(PC >> 8);
        Memory[SP + 1] = (byte)(PC & 0xFF);
        PC = 0x10;
    }

    // 0xD8 - RET C - If last result carried, return from subroutine.
    private void ExecuteRetC()
    {
        if (Carry)
        {
            PC = (ushort)((Memory[SP] << 8) | Memory[SP + 1]);
            SP += 2;
        }
    }

    // 0xD9 - RETI - Enable interrupts and return from subroutine.
    private void ExecuteRetI()
    {
        PC = (ushort)((Memory[SP] << 8) | Memory[SP + 1]);
        SP += 2;
        // Enable interrupts in the implementation
    }

    // 0xDA - JP C, nn - If last result carried, jump to address nn.
    private void ExecuteJpCnn()
    {
        if (Carry)
        {
            ushort address = (ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]);
            PC = address;
        }
        else
        {
            PC += 2;
        }
    }

    // 0xDB - IN A, (n) - Input A from port n.
    private void ExecuteInAn()
    {
        byte port = Memory[PC + 1];
        // Input A from port n in the implementation
        PC += 2;
    }

    // 0xDC - CALL C, nn - If last result carried, call address nn.
    private void ExecuteCallCnn()
    {
        if (Carry)
        {
            ushort address = (ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]);
            SP -= 2;
            Memory[SP] = (byte)(PC >> 8);
            Memory[SP + 1] = (byte)(PC & 0xFF);
            PC = address;
        }
        else
        {
            PC += 2;
        }
    }

    // 0xDD -
    // 0xDE - SBC A, n - Subtract n + Carry flag from A

    private void ExecuteSbcAn()
    {
        byte n = Memory[PC + 1];
        byte result = (byte)(A - n - (Carry ? 1 : 0));
        A = result;
        Zero = result == 0;
        Sign = result > 0x7F;
        HalfCarry = (A & 0xF) < (n & 0xF) + (Carry ? 1 : 0);
        Carry = A < n + (Carry ? 1 : 0);
        Parity = ((result & 0x01) ^ ((result & 0x02) >> 1) ^ ((result & 0x04) >> 2) ^ ((result & 0x08) >> 3)
                  ^ ((result & 0x10) >> 4) ^ ((result & 0x20) >> 5) ^ ((result & 0x40) >> 6) ^
                  ((result & 0x80) >> 7)) == 0;
        PC += 2;
    }

    // 0xDF - RST 18 - Push PC onto stack and jump to address 0x18.
    private void ExecuteRst18()
    {
        SP -= 2;
        Memory[SP] = (byte)(PC >> 8);
        Memory[SP + 1] = (byte)(PC & 0xFF);
        PC = 0x18;
    }

    // 0xE0 - LDH (n), A - Put A into address 0xFF00 + n.
    private void ExecuteLdhAn()
    {
        ushort address = (ushort)((0xFF00 + Memory[PC + 1]) % 0xFFFF);
        Memory[address] = A;
        PC += 2;
    }

    // 0xE1 - POP HL - Pop two bytes from stack into HL.
    private void ExecutePopHL()
    {
        H = Memory[SP];
        L = Memory[SP + 1];
        SP += 2;
    }


    // 0xE2 - LD (C), A - Load A into (C). 


    // 0xE3 - EX (SP), HL - Exchange the values of the HL and (SP) registers.


    // 0xE4 - CALL PO, nn - Call address n if the sign bit is set.


    // 0xE5 - PUSH HL - Push HL onto the stack.
    private void ExecutePushHL()
    {
        ushort hl = (ushort)((H << 8) | L);
        Memory[SP - 1] = (byte)((hl >> 8) & 0xFF);
        Memory[SP - 2] = (byte)(hl & 0xFF);
        SP -= 2;
        PC++;
    }

    // 0xE6 - AND n - Logically AND n with A, result in A.
    private void ExecuteAndN()
    {
        A &= Memory[PC + 1];
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = true;
        Parity = (A & 0x01) == 0;
        Carry = false;
        PC += 2;
    }

    // 0xE7 - RST 20 - Push present address onto stack and jump to address 0020h.
    private void ExecuteRst20()
    {
        Memory[SP - 1] = (byte)((PC >> 8) & 0xFF);
        Memory[SP - 2] = (byte)(PC & 0xFF);
        SP -= 2;
        PC = 0x0020;
    }

    // 0xE8 - ADD SP, n - Add n to SP.
    private void ExecuteAddSPn()
    {
        sbyte n = (sbyte)Memory[PC + 1];
        ushort result = (ushort)(SP + n);
        Sign = false;
        Zero = false;
        HalfCarry = (SP & 0xF) + (n & 0xF) > 0xF;
        Carry = (SP & 0xFF) + (n & 0xFF) > 0xFF;
        Parity = (result & 0x01) == 0;
        SP = result;
        PC += 2;
    }

    // 0xE9 - JP (HL) - Jump to address contained in HL.
    private void ExecuteJpHL()
    {
        ushort address = (ushort)((H << 8) | L);
        PC = address;
    }

    // 0xEA - LD (nn), A - Put A into address nn.
    private void ExecuteLdnnA()
    {
        ushort address = (ushort)((ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]) % 0x10000);
        Memory[address] = A;
        PC = (ushort)((PC + 3) % 0x10000);
    }

    // 0xEB - 

    // 0xEC -  

    // 0xED -  

    // 0xEE - XOR n - Logical exclusive OR n with A, result in A.
    private void ExecuteXorn()
    {
        byte n = Memory[PC + 1];
        A ^= n;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC += 2;
    }

    // 0xEF - RST 28H - Push present address onto stack. Jump to address 0x0028.
    private void ExecuteRst28()
    {
        SP -= 2;
        Memory[SP] = (byte)((PC >> 8) & 0xFF);
        Memory[SP + 1] = (byte)(PC & 0xFF);
        PC = 0x0028;
    }


    // 0xF0 - LDH A, (n) - Put value at address 0xFF00 + n into A.
    private void ExecuteLdHAa8()
    {
        byte n = Memory[PC + 1];
        A = Memory[0xFF00 + (n % 0xFF)];
        PC += 2;
    }


    // 0xF1 - POP AF - Pop two bytes from stack into AF.
    private void ExecutePopAF()
    {
        A = Memory[SP + 1];
        byte flags = Memory[SP];
        Zero = (flags & 0x80) != 0;
        Sign = (flags & 0x40) != 0;
        HalfCarry = (flags & 0x20) != 0;
        Parity = (flags & 0x04) != 0;
        Carry = (flags & 0x01) != 0;
        SP += 2;
        PC++;
    }

    // 0xF2 - LD A, (C) - Put value at address 0xFF00 + C into A.


    // 0xF3 - DI - Disable interrupts after instruction.
    private void ExecuteDI()
    {
        //TODO: Implement interrupt disabling.
        PC++;
    }

    // 0xF4 -  

    // 0xF5 - PUSH AF - Push AF onto stack.
    private void ExecutePushAF()
    {
        Memory[SP] = (byte)((Zero ? 0x80 : 0x00) | (Sign ? 0x40 : 0x00) | (HalfCarry ? 0x20 : 0x00) |
                            (Parity ? 0x04 : 0x00) | (Carry ? 0x01 : 0x00));
        Memory[SP + 1] = A;
        SP -= 2;
        PC++;
    }

    // 0xF6 - OR n - Logical OR n with A, result in A.
    private void ExecuteOrn()
    {
        byte n = Memory[PC + 1];
        A |= n;
        Sign = (A & 0x80) != 0;
        Zero = A == 0;
        HalfCarry = false;
        Carry = false;
        Parity = (A & 0x01) == 0;
        PC += 2;
    }

    // 0xF7 - RST 30H - Push present address onto stack. Jump to address 0x0030.
    private void ExecuteRst30()
    {
        SP -= 2;
        Memory[SP] = (byte)((PC >> 8) & 0xFF);
        Memory[SP + 1] = (byte)(PC & 0xFF);
        PC = 0x0030;
    }

    // 0xF8 - LD HL, SP+n - Put SP + n into HL.
    private void ExecuteLdHLSPn()
    {
        sbyte n = (sbyte)Memory[PC + 1];
        ushort result = (ushort)(SP + n);
        H = (byte)((result >> 8) & 0xFF);
        L = (byte)(result & 0xFF);
        Sign = false;
        Zero = false;
        HalfCarry = (SP & 0xF) + (n & 0xF) > 0xF;
        Carry = (SP & 0xFF) + (n & 0xFF) > 0xFF;
        Parity = (H & 0x01) == 0;
        PC += 2;
    }

    // 0xF9 - LD SP, HL - Put HL into SP.
    private void ExecuteLdSPHL()
    {
        ushort hl = (ushort)((H << 8) | L);
        SP = hl;
        PC++;
    }

    // 0xFA - LD A, (nn) - Put value at address nn into A.
    private void ExecuteLdAnn()
    {
        ushort address = (ushort)((Memory[PC + 2] << 8) | Memory[PC + 1]);
        A = Memory[address];
        PC += 3;
    }

    // 0xFB - EI - Enable interrupts after instruction.
    private void ExecuteEI()
    {
        //TODO: Implement interrupt enabling.
        PC++;
    }

    // 0xFC - 

    // 0xFD - 

    // 0xFE - CP n - Compare A with n.
    private void ExecuteCpn()
    {
        byte n = Memory[PC + 1];
        Sign = (A & 0x80) != (n & 0x80);
        Zero = A == n;
        HalfCarry = (A & 0xF) < (n & 0xF);
        Carry = A < n;
        Parity = (A & 0x01) == (n & 0x01);
        PC += 2;
    }

    // 0xFF - RST 38H - Push present address onto stack. Jump to address 0x0038.
    private void ExecuteRst38()
    {
        SP -= 2;
        Memory[SP] = (byte)((PC >> 8) & 0xFF);
        Memory[SP + 1] = (byte)(PC & 0xFF);
        PC = 0x0038;
    }


    public void ExecutePrefixCB()
    {
        byte opcode = Memory[PC + 1];
        switch (opcode)
        {
            case 0x00: // RLC B
                byte bit0 = (byte)(B & 0x80);
                B = (byte)((B << 1) | bit0);
                Zero = B == 0;
                Carry = bit0 == 0x80;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x01: // RLC C
                bit0 = (byte)(C & 0x80);
                C = (byte)((C << 1) | bit0);
                Zero = C == 0;
                Carry = bit0 == 0x80;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x02: // RLC D
                bit0 = (byte)(D & 0x80);
                D = (byte)((D << 1) | bit0);
                Zero = D == 0;
                Carry = bit0 == 0x80;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x03: // RLC E
                bit0 = (byte)(E & 0x80);
                E = (byte)((E << 1) | bit0);
                Zero = E == 0;
                Carry = bit0 == 0x80;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x04: // RLC H
                bit0 = (byte)(H & 0x80);
                H = (byte)((H << 1) | bit0);
                Zero = H == 0;
                Carry = bit0 == 0x80;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x05: // RLC L
                bit0 = (byte)(L & 0x80);
                L = (byte)((L << 1) | bit0);
                Zero = L == 0;
                Carry = bit0 == 0x80;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;

            case 0x06: // RLC (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] << 1) | bit0);
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0x07: // RLC A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A << 1) | bit0);
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x08: // RRC B
                bit0 = (byte)(B & 0x01);
                B = (byte)((B >> 1) | (bit0 << 7));
                Zero = B == 0;
                Carry = bit0 == 1;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x09: // RRC C
                bit0 = (byte)(C & 0x01);
                C = (byte)((C >> 1) | (bit0 << 7));
                Zero = C == 0;
                Carry = bit0 == 1;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x0A: // RRC D
                bit0 = (byte)(D & 0x01);
                D = (byte)((D >> 1) | (bit0 << 7));
                Zero = D == 0;
                Carry = bit0 == 1;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x0B: // RRC E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E >> 1) | (bit0 << 7));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;

            case 0x0C: // RRC H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x0D: // RRC L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x0E: // RRC (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0x0F: // RRC A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x10: // RL B
                bit0 = (byte)(B & 0x80);
                B = (byte)((B << 1) | (Carry ? 1 : 0));
                Zero = B == 0;
                Carry = bit0 == 0x80;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x11: // RL C
                bit0 = (byte)(C & 0x80);
                C = (byte)((C << 1) | (Carry ? 1 : 0));
                Zero = C == 0;
                Carry = bit0 == 0x80;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x12: // RL D
                bit0 = (byte)(D & 0x80);
                D = (byte)((D << 1) | (Carry ? 1 : 0));
                Zero = D == 0;
                Carry = bit0 == 0x80;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;

            case 0x13: // RL E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E << 1) | (Carry ? 1 : 0));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x14: // RL H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H << 1) | (Carry ? 1 : 0));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x15: // RL L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L << 1) | (Carry ? 1 : 0));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x16: // RL (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] << 1) | (Carry ? 1 : 0));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0x17: // RL A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A << 1) | (Carry ? 1 : 0));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x18: // RR B
                bit0 = (byte)(B & 0x01);
                B = (byte)((B >> 1) | (Carry ? 0x80 : 0x00));
                Zero = B == 0;
                Carry = bit0 == 1;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x19: // RR C
                bit0 = (byte)(C & 0x01);
                C = (byte)((C >> 1) | (Carry ? 0x80 : 0x00));
                Zero = C == 0;
                Carry = bit0 == 1;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;

            case 0x1A: // RR D
                bit0 = (byte)(D & 0x01);
                D = (byte)((D >> 1) | (bit0 << 7));
                Zero = D == 0;
                Carry = bit0 == 1;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x1B: // RR E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E >> 1) | (bit0 << 7));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x1C: // RR H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x1D: // RR L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x1E: // RR (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0x1F: // RR A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x20: // SLA B
                bit0 = (byte)(B & 0x80);
                B = (byte)(B << 1);
                Zero = B == 0;
                Carry = bit0 == 0x80;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x21: // SLA C
                bit0 = (byte)(C & 0x80);
                C = (byte)(C << 1);
                Zero = C == 0;
                Carry = bit0 == 0x80;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x22: // SLA D
                bit0 = (byte)(D & 0x80);
                D = (byte)(D << 1);
                Zero = D == 0;
                Carry = bit0 == 0x80;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;


            case 0x23: // SLA E
                A = (byte)((E << 1) | (E >> 7));
                Zero = A == 0;
                Carry = (E & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x24: // SLA H
                A = (byte)((H << 1) | (H >> 7));
                Zero = A == 0;
                Carry = (H & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x25: // SLA L
                A = (byte)((L << 1) | (L >> 7));
                Zero = A == 0;
                Carry = (L & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x26: // SLA (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] << 1) | (bit0 >> 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0x27: // SLA A
                A = (byte)((A << 1) | (A >> 7));
                Zero = A == 0;
                Carry = (A & 0x80) == 0x80;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x28: // SRA B
                A = (byte)((B >> 1) | (B << 7));
                Zero = A == 0;
                Carry = (B & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x29: // SRA C
                A = (byte)((C >> 1) | (C << 7));
                Zero = A == 0;
                Carry = (C & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x2A: // SRA D
                A = (byte)((D >> 1) | (D << 7));
                Zero = A == 0;
                Carry = (D & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x2B: // SRA E
                A = (byte)((E >> 1) | (E << 7));
                Zero = A == 0;
                Carry = (E & 0x01) == 0x01;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;

            case 0x2C: // SRA H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x2D: // SRA L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x2E: // SRA (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0x2F: // SRA A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x30: // SWAP B
                B = (byte)((B << 4) | (B >> 4));
                Zero = B == 0;
                Carry = false;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x31: // SWAP C
                C = (byte)((C << 4) | (C >> 4));
                Zero = C == 0;
                Carry = false;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x32: // SWAP D
                D = (byte)((D << 4) | (D >> 4));
                Zero = D == 0;
                Carry = false;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x33: // SWAP E
                E = (byte)((E << 4) | (E >> 4));
                Zero = E == 0;
                Carry = false;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;

            case 0x34: // SWAP H
                A = (byte)((H >> 4) | (H << 4));
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                PC += 2;
                break;
            case 0x35: // SWAP L
                A = (byte)((L >> 4) | (L << 4));
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                PC += 2;
                break;
            case 0x36: // SWAP (HL)
                A = (byte)((Memory[(H << 8) | L] >> 4) | (Memory[(H << 8) | L] << 4));
                Memory[(H << 8) | L] = A;
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                PC += 2;
                break;
            case 0x37: // SWAP A
                A = (byte)((A >> 4) | (A << 4));
                Zero = A == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                Carry = false;
                HalfCarry = false;
                PC += 2;
                break;
            case 0x38: // SRL B
                bit0 = (byte)(B & 0x01);
                B = (byte)((B >> 1) | (bit0 << 7));
                Zero = B == 0;
                Carry = bit0 == 1;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x39: // SRL C
                bit0 = (byte)(C & 0x01);
                C = (byte)((C >> 1) | (bit0 << 7));
                Zero = C == 0;
                Carry = bit0 == 1;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x3A: // SRL D
                bit0 = (byte)(D & 0x01);
                D = (byte)((D >> 1) | (bit0 << 7));
                Zero = D == 0;
                Carry = bit0 == 1;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x3B: // SRL E
                bit0 = (byte)(E & 0x01);
                E = (byte)((E >> 1) | (bit0 << 7));
                Zero = E == 0;
                Carry = bit0 == 1;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;

            case 0x3C: // SRL H
                bit0 = (byte)(H & 0x01);
                H = (byte)((H >> 1) | (bit0 << 7));
                Zero = H == 0;
                Carry = bit0 == 1;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x3D: // SRL L
                bit0 = (byte)(L & 0x01);
                L = (byte)((L >> 1) | (bit0 << 7));
                Zero = L == 0;
                Carry = bit0 == 1;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x3E: // SRL (HL)
                bit0 = (byte)(Memory[(H << 8) | L] & 0x01);
                Memory[(H << 8) | L] = (byte)((Memory[(H << 8) | L] >> 1) | (bit0 << 7));
                Zero = Memory[(H << 8) | L] == 0;
                Carry = bit0 == 1;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0x3F: // SRL A
                bit0 = (byte)(A & 0x01);
                A = (byte)((A >> 1) | (bit0 << 7));
                Zero = A == 0;
                Carry = bit0 == 1;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0x40: // BIT 0,B
                Zero = (B & 0x01) == 0;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x41: // BIT 0,C
                Zero = (C & 0x01) == 0;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x42: // BIT 0,D
                Zero = (D & 0x01) == 0;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x43: // BIT 0,E
                Zero = (E & 0x01) == 0;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x44: // BIT 0,H
                Zero = (H & 0x01) == 0;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = true;
                PC += 2;
                break;

            case 0x45: // BIT 0,L
                Zero = (L & 0x01) == 0;
                Sign = (L & 0x80) > 0;
                Parity = IsParity(L);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x46: // BIT 0,(HL)
                Zero = (Memory[(H << 8) | L] & 0x01) == 0;
                Sign = (Memory[(H << 8) | L] & 0x80) > 0;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x47: // BIT 0,A
                Zero = (A & 0x01) == 0;
                Sign = (A & 0x80) > 0;
                Parity = IsParity(A);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x48: // BIT 1,B
                Zero = (B & 0x02) == 0;
                Sign = (B & 0x80) > 0;
                Parity = IsParity(B);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x49: // BIT 1,C
                Zero = (C & 0x02) == 0;
                Sign = (C & 0x80) > 0;
                Parity = IsParity(C);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x4A: // BIT 1,D
                Zero = (D & 0x02) == 0;
                Sign = (D & 0x80) > 0;
                Parity = IsParity(D);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x4B: // BIT 1,E
                Zero = (E & 0x02) == 0;
                Sign = (E & 0x80) > 0;
                Parity = IsParity(E);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x4C: // BIT 1,H
                Zero = (H & 0x02) == 0;
                Sign = (H & 0x80) > 0;
                Parity = IsParity(H);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x4D: // BIT 1,L
                Zero = (L & 0x02) == 0;
                Sign = (L & 0x80) > 0;
                Parity = IsParity(L);
                HalfCarry = true;
                PC += 2;
                break;
            case 0x4E: // BIT 1,(HL)
                Zero = (Memory[(H << 8) | L] & 0x02) == 0;
                Sign = (Memory[(H << 8) | L] & 0x80) > 0;
                Parity = IsParity(Memory[(H << 8) | L]);
                HalfCarry = true;
                PC += 2;
                break;

            case 0x4F: // BIT 1,A
                Zero = (A & 0x02) == 0;
                Sign = (A & 0x02) > 0;
                Parity = IsParity((byte)(A & 0x02));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x50: // BIT 2,B
                Zero = (B & 0x04) == 0;
                Sign = (B & 0x04) > 0;
                Parity = IsParity((byte)(B & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x51: // BIT 2,C
                Zero = (C & 0x04) == 0;
                Sign = (C & 0x04) > 0;
                Parity = IsParity((byte)(C & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x52: // BIT 2,D
                Zero = (D & 0x04) == 0;
                Sign = (D & 0x04) > 0;
                Parity = IsParity((byte)(D & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x53: // BIT 2,E
                Zero = (E & 0x04) == 0;
                Sign = (E & 0x04) > 0;
                Parity = IsParity((byte)(E & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x54: // BIT 2,H
                Zero = (H & 0x04) == 0;
                Sign = (H & 0x04) > 0;
                Parity = IsParity((byte)(H & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x55: // BIT 2,L
                Zero = (L & 0x04) == 0;
                Sign = (L & 0x04) > 0;
                Parity = IsParity((byte)(L & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x56: // BIT 2,(HL)
                Zero = (Memory[(H << 8) | L] & 0x04) == 0;
                Sign = (Memory[(H << 8) | L] & 0x04) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x57: // BIT 2,A
                Zero = (A & 0x04) == 0;
                Sign = (A & 0x04) > 0;
                Parity = IsParity((byte)(A & 0x04));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x58: // BIT 3,B
                Zero = (B & 0x08) == 0;
                Sign = (B & 0x08) > 0;
                Parity = IsParity((byte)(B & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x59: // BIT 3,C
                Zero = (C & 0x08) == 0;
                Sign = (C & 0x08) > 0;
                Parity = IsParity((byte)(C & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x5A: // BIT 3,D
                Zero = (D & 0x08) == 0;
                Sign = (D & 0x08) > 0;
                Parity = IsParity((byte)(D & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x5B: // BIT 3,E
                Zero = (E & 0x08) == 0;
                Sign = (E & 0x08) > 0;
                Parity = IsParity((byte)(E & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x5C: // BIT 3,H
                Zero = (H & 0x08) == 0;
                Sign = (H & 0x08) > 0;
                Parity = IsParity((byte)(H & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x5D: // BIT 3,L
                Zero = (L & 0x08) == 0;
                Sign = (L & 0x08) > 0;
                Parity = IsParity((byte)(L & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x5E: // BIT 3,(HL)
                Zero = (Memory[(H << 8) | L] & 0x08) == 0;
                Sign = (Memory[(H << 8) | L] & 0x08) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x5F: // BIT 3,A
                Zero = (A & 0x08) == 0;
                Sign = (A & 0x08) > 0;
                Parity = IsParity((byte)(A & 0x08));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x60: // BIT 4,B
                Zero = (B & 0x10) == 0;
                Sign = (B & 0x10) > 0;
                Parity = IsParity((byte)(B & 0x10));
                HalfCarry = true;
                PC += 2;
                break;


            case 0x61: // BIT 4,C
                Zero = (C & 0x10) == 0;
                Sign = (C & 0x10) > 0;
                Parity = IsParity((byte)(C & 0x10));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x62: // BIT 4,D
                Zero = (D & 0x10) == 0;
                Sign = (D & 0x10) > 0;
                Parity = IsParity((byte)(D & 0x10));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x63: // BIT 4,E
                Zero = (E & 0x10) == 0;
                Sign = (E & 0x10) > 0;
                Parity = IsParity((byte)(E & 0x10));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x64: // BIT 4,H
                Zero = (H & 0x10) == 0;
                Sign = (H & 0x10) > 0;
                Parity = IsParity((byte)(H & 0x10));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x65: // BIT 4,L
                Zero = (L & 0x10) == 0;
                Sign = (L & 0x10) > 0;
                Parity = IsParity((byte)(L & 0x10));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x66: // BIT 4,(HL)
                Zero = (Memory[(H << 8) | L] & 0x10) == 0;
                Sign = (Memory[(H << 8) | L] & 0x10) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x10));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x67: // BIT 4,A
                Zero = (A & 0x10) == 0;
                Sign = (A & 0x10) > 0;
                Parity = IsParity((byte)(A & 0x10));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x68: // BIT 5,B
                Zero = (B & 0x20) == 0;
                Sign = (B & 0x20) > 0;
                Parity = IsParity((byte)(B & 0x20));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x69: // BIT 5,C
                Zero = (C & 0x20) == 0;
                Sign = (C & 0x20) > 0;
                Parity = IsParity((byte)(C & 0x20));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x6A: // BIT 5,D
                Zero = (D & 0x20) == 0;
                Sign = (D & 0x20) > 0;
                Parity = IsParity((byte)(D & 0x20));
                HalfCarry = true;
                PC += 2;
                break;

            case 0x6B: // BIT 5,E
                Zero = (E & 0x20) == 0;
                Sign = (E & 0x20) > 0;
                Parity = IsParity((byte)(E & 0x20));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x6C: // BIT 5,H
                Zero = (H & 0x20) == 0;
                Sign = (H & 0x20) > 0;
                Parity = IsParity((byte)(H & 0x20));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x6D: // BIT 5,L
                Zero = (L & 0x20) == 0;
                Sign = (L & 0x20) > 0;
                Parity = IsParity((byte)(L & 0x20));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x6E: // BIT 5,(HL)
                Zero = (Memory[(H << 8) | L] & 0x20) == 0;
                Sign = (Memory[(H << 8) | L] & 0x20) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & 0x20));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x6F: // BIT 5,A
                Zero = (A & 0x20) == 0;
                Sign = (A & 0x20) > 0;
                Parity = IsParity((byte)(A & 0x20));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x70: // BIT 6,B
                Zero = (B & 0x40) == 0;
                Sign = (B & 0x40) > 0;
                Parity = IsParity((byte)(B & 0x40));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x71: // BIT 6,C
                Zero = (C & 0x40) == 0;
                Sign = (C & 0x40) > 0;
                Parity = IsParity((byte)(C & 0x40));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x72: // BIT 6,D
                Zero = (D & 0x40) == 0;
                Sign = (D & 0x40) > 0;
                Parity = IsParity((byte)(D & 0x40));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x73: // BIT 6,E
                Zero = (E & 0x40) == 0;
                Sign = (E & 0x40) > 0;
                Parity = IsParity((byte)(E & 0x40));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x74: // BIT 6,H
                Zero = (H & 0x40) == 0;
                Sign = (H & 0x40) > 0;
                Parity = IsParity((byte)(H & 0x40));
                HalfCarry = true;
                PC += 2;
                break;

            case 0x75: // BIT 6,L
                Zero = (L & (1 << 6)) == 0;
                Sign = (L & (1 << 6)) > 0;
                Parity = IsParity((byte)(L & (1 << 6)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x76: // BIT 6,(HL)
                Zero = (Memory[(H << 8) | L] & (1 << 6)) == 0;
                Sign = (Memory[(H << 8) | L] & (1 << 6)) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & (1 << 6)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x77: // BIT 6,A
                Zero = (A & (1 << 6)) == 0;
                Sign = (A & (1 << 6)) > 0;
                Parity = IsParity((byte)(A & (1 << 6)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x78: // BIT 7,B
                Zero = (B & (1 << 7)) == 0;
                Sign = (B & (1 << 7)) > 0;
                Parity = IsParity((byte)(B & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x79: // BIT 7,C
                Zero = (C & (1 << 7)) == 0;
                Sign = (C & (1 << 7)) > 0;
                Parity = IsParity((byte)(C & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x7A: // BIT 7,D
                Zero = (D & (1 << 7)) == 0;
                Sign = (D & (1 << 7)) > 0;
                Parity = IsParity((byte)(D & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x7B: // BIT 7,E
                Zero = (E & (1 << 7)) == 0;
                Sign = (E & (1 << 7)) > 0;
                Parity = IsParity((byte)(E & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x7C: // BIT 7,H
                Zero = (H & (1 << 7)) == 0;
                Sign = (H & (1 << 7)) > 0;
                Parity = IsParity((byte)(H & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x7D: // BIT 7,L
                Zero = (L & (1 << 7)) == 0;
                Sign = (L & (1 << 7)) > 0;
                Parity = IsParity((byte)(L & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x7E: // BIT 7,(HL)
                Zero = (Memory[(H << 8) | L] & (1 << 7)) == 0;
                Sign = (Memory[(H << 8) | L] & (1 << 7)) > 0;
                Parity = IsParity((byte)(Memory[(H << 8) | L] & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;
            case 0x7F: // BIT 7,A
                Zero = (A & (1 << 7)) == 0;
                Sign = (A & (1 << 7)) > 0;
                Parity = IsParity((byte)(A & (1 << 7)));
                HalfCarry = true;
                PC += 2;
                break;

            case 0x80: // RES 0,B
                B &= 0xFE;
                PC += 2;
                break;
            case 0x81: // RES 0,C
                C &= 0xFE;
                PC += 2;
                break;
            case 0x82: // RES 0,D
                D &= 0xFE;
                PC += 2;
                break;
            case 0x83: // RES 0,E
                E &= 0xFE;
                PC += 2;
                break;
            case 0x84: // RES 0,H
                H &= 0xFE;
                PC += 2;
                break;
            case 0x85: // RES 0,L
                L &= 0xFE;
                PC += 2;
                break;
            case 0x86: // RES 0,(HL)
                Memory[(H << 8) | L] &= 0xFE;
                PC += 2;
                break;
            case 0x87: // RES 0,A
                A &= 0xFE;
                PC += 2;
                break;
            case 0x88: // RES 1,B
                B &= 0xFD;
                PC += 2;
                break;
            case 0x89: // RES 1,C
                C &= 0xFD;
                PC += 2;
                break;
            case 0x8A: // RES 1,D
                D &= 0xFD;
                PC += 2;
                break;
            case 0x8B: // RES 1,E
                E &= 0xFD;
                PC += 2;
                break;

            case 0x8C: // RES 1,H
                H &= 0xFE;
                PC += 2;
                break;
            case 0x8D: // RES 1,L
                L &= 0xFE;
                PC += 2;
                break;
            case 0x8E: // RES 1,(HL)
                Memory[(H << 8) | L] &= 0xFE;
                PC += 2;
                break;
            case 0x8F: // RES 1,A
                A &= 0xFE;
                PC += 2;
                break;
            case 0x90: // RES 2,B
                B &= 0xFD;
                PC += 2;
                break;
            case 0x91: // RES 2,C
                C &= 0xFD;
                PC += 2;
                break;
            case 0x92: // RES 2,D
                D &= 0xFD;
                PC += 2;
                break;
            case 0x93: // RES 2,E
                E &= 0xFD;
                PC += 2;
                break;
            case 0x94: // RES 2,H
                H &= 0xFD;
                PC += 2;
                break;
            case 0x95: // RES 2,L
                L &= 0xFD;
                PC += 2;
                break;
            case 0x96: // RES 2,(HL)
                Memory[(H << 8) | L] &= 0xFD;
                PC += 2;
                break;
            case 0x97: // RES 2,A
                A &= 0xFD;
                PC += 2;
                break;
            case 0x98: // RES 3,B
                B &= 0xFB;
                PC += 2;
                break;
            case 0x99: // RES 3,C
                C &= 0xFB;
                PC += 2;
                break;
            case 0x9A: // RES 3,D
                D &= 0xFB;
                PC += 2;
                break;
            case 0x9B: // RES 3,E
                E &= 0xFB;
                PC += 2;
                break;
            case 0x9C: // RES 3,H
                H &= 0xFB;
                PC += 2;
                break;
            case 0x9D: // RES 3,L
                L &= 0xFB;
                PC += 2;
                break;
            case 0x9E: // RES 3,(HL)
                Memory[(H << 8) | L] &= 0xFB;
                PC += 2;
                break;
            case 0x9F: // RES 3,A
                A &= 0xFB;
                PC += 2;
                break;
            case 0xA0: // RES 4,B
                B &= 0xF7;
                PC += 2;
                break;
            case 0xA1: // RES 4,C
                C &= 0xF7;
                PC += 2;
                break;
            case 0xA2: // RES 4,D
                D &= 0xF7;
                PC += 2;
                break;
            case 0xA3: // RES 4,E
                E &= 0xF7;
                PC += 2;
                break;

            case 0xA4: // RES 4,H
                H &= 0xEF;
                PC += 2;
                break;
            case 0xA5: // RES 4,L
                L &= 0xEF;
                PC += 2;
                break;
            case 0xA6: // RES 4,(HL)
                Memory[(H << 8) | L] &= 0xEF;
                PC += 2;
                break;
            case 0xA7: // RES 4,A
                A &= 0xEF;
                PC += 2;
                break;
            case 0xA8: // RES 5,B
                B &= 0xDF;
                PC += 2;
                break;
            case 0xA9: // RES 5,C
                C &= 0xDF;
                PC += 2;
                break;
            case 0xAA: // RES 5,D
                D &= 0xDF;
                PC += 2;
                break;
            case 0xAB: // RES 5,E
                E &= 0xDF;
                PC += 2;
                break;
            case 0xAC: // RES 5,H
                H &= 0xDF;
                PC += 2;
                break;
            case 0xAD: // RES 5,L
                L &= 0xDF;
                PC += 2;
                break;
            case 0xAE: // RES 5,(HL)
                Memory[(H << 8) | L] &= 0xDF;
                PC += 2;
                break;
            case 0xAF: // RES 5,A
                A &= 0xDF;
                PC += 2;
                break;
            case 0xB0: // RES 6,B
                B &= 0xBF;
                PC += 2;
                break;
            case 0xB1: // RES 6,C
                C &= 0xBF;
                PC += 2;
                break;
            case 0xB2: // RES 6,D
                D &= 0xBF;
                PC += 2;
                break;
            case 0xB3: // RES 6,E
                E &= 0xBF;
                PC += 2;
                break;
            case 0xB4: // RES 6,H
                H &= 0xBF;
                PC += 2;
                break;
            case 0xB5: // RES 6,L
                L &= 0xBF;
                PC += 2;
                break;
            case 0xB6: // RES 6,(HL)
                Memory[(H << 8) | L] &= 0xBF;
                PC += 2;
                break;
            case 0xB7: // RES 6,A
                A &= 0xBF;
                PC += 2;
                break;
            case 0xB8: // RES 7,B
                B &= 0x7F;
                PC += 2;
                break;
            case 0xB9: // RES 7,C
                C &= 0x7F;
                PC += 2;
                break;
            case 0xBA: // RES 7,D
                D &= 0x7F;
                PC += 2;
                break;
            case 0xBB: // RES 7,E
                E &= 0x7F;
                PC += 2;
                break;
            case 0xBC: // RES 7,H
                H &= 0x7F;
                PC += 2;
                break;
            case 0xBD: // RES 7,L
                L &= 0x7F;
                PC += 2;
                break;
            case 0xBE: // RES 7,(HL)
                Memory[(H << 8) | L] &= 0x7F;
                PC += 2;
                break;
            case 0xBF: // RES 7,A
                A &= 0x7F;
                PC += 2;
                break;
            case 0xC0: // SET 0,B
                B |= 0x01;
                PC += 2;
                break;
            case 0xC1: // SET 0,C
                C |= 0x01;
                PC += 2;
                break;
            case 0xC2: // SET 0,D
                D |= 0x01;
                PC += 2;
                break;
            case 0xC3: // SET 0,E
                E |= 0x01;
                PC += 2;
                break;
            case 0xC4: // SET 0,H
                H |= 0x01;
                PC += 2;
                break;
            case 0xC5: // SET 0,L
                L |= 0x01;
                PC += 2;
                break;
            case 0xC6: // SET 0,(HL)
                Memory[(H << 8) | L] |= 0x01;
                PC += 2;
                break;
            case 0xC7: // SET 0,A
                A |= 0x01;
                PC += 2;
                break;
            case 0xC8: // SET 1,B
                B |= 0x02;
                PC += 2;
                break;
            case 0xC9: // SET 1,C
                C |= 0x02;
                PC += 2;
                break;
            case 0xCA: // SET 1,D
                D |= 0x02;
                PC += 2;
                break;
            case 0xCB: // SET 1,E
                E |= 0x02;
                PC += 2;
                break;
            case 0xCC: // SET 1,H
                H |= 0x02;
                PC += 2;
                break;
            case 0xCD: // SET 1,L
                L |= 0x02;
                PC += 2;
                break;
            case 0xCE: // SET 1,(HL)
                Memory[(H << 8) | L] |= 0x02;
                PC += 2;
                break;
            case 0xCF: // SET 1,A
                A |= 0x02;
                PC += 2;
                break;
            case 0xD0: // SET 2,B
                B |= 0x04;
                PC += 2;
                break;
            case 0xD1: // SET 2,C
                C |= 0x04;
                PC += 2;
                break;
            case 0xD2: // SET 2,D
                D |= 0x04;
                PC += 2;
                break;
            case 0xD3: // SET 2,E
                E |= 0x04;
                PC += 2;
                break;
            case 0xD4: // SET 2,H
                H |= 0x04;
                PC += 2;
                break;
            case 0xD5: // SET 2,L
                L |= 0x04;
                PC += 2;
                break;
            case 0xD6: // SET 2,(HL)
                Memory[(H << 8) | L] |= 0x04;
                PC += 2;
                break;
            case 0xD7: // SET 2,A
                A |= 0x04;
                PC += 2;
                break;
            case 0xD8: // SET 3,B
                B |= 0x08;
                PC += 2;
                break;
            case 0xD9: // SET 3,C
                C |= 0x08;
                PC += 2;
                break;
            case 0xDA: // SET 3,D
                D |= 0x08;
                PC += 2;
                break;
            case 0xDB: // SET 3,E
                E |= 0x08;
                PC += 2;
                break;
            case 0xDC: // SET 3,H
                H |= 0x08;
                PC += 2;
                break;
            case 0xDD: // SET 3,L
                L |= 0x08;
                PC += 2;
                break;
            case 0xDE: // SET 3,(HL)
                Memory[(H << 8) | L] |= 0x08;
                PC += 2;
                break;
            case 0xDF: // SET 3,A
                A |= 0x08;
                PC += 2;
                break;
            case 0xE0: // SET 4,B
                B |= 0x10;
                PC += 2;
                break;
            case 0xE1: // SET 4,C
                C |= 0x10;
                PC += 2;
                break;
            case 0xE2: // SET 4,D
                D |= 0x10;
                PC += 2;
                break;
            case 0xE3: // SET 4,E
                E |= 0x10;
                PC += 2;
                break;
            case 0xE4: // SET 4,H
                H |= 0x10;
                PC += 2;
                break;
            case 0xE5: // SET 4,L
                L |= 0x10;
                PC += 2;
                break;
            case 0xE6: // SET 4,(HL)
                Memory[(H << 8) | L] |= 0x10;
                PC += 2;
                break;
            case 0xE7: // SET 4,A
                A |= 0x10;
                PC += 2;
                break;
            case 0xE8: // SET 5,B
                B |= 0x20;
                PC += 2;
                break;
            case 0xE9: // SET 5,C
                C |= 0x20;
                PC += 2;
                break;
            case 0xEA: // SET 5,D
                D |= 0x20;
                PC += 2;
                break;
            case 0xEB: // SET 5,E
                E |= 0x20;
                PC += 2;
                break;
            case 0xEC: // SET 5,H
                H |= 0x20;
                PC += 2;
                break;
            case 0xED: // SET 5,L
                L |= 0x20;
                PC += 2;
                break;

            case 0xEE: // SET 5,(HL)
                Memory[(H << 8) | L] |= 0x20;
                Zero = Memory[(H << 8) | L] == 0;
                Carry = false;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0xEF: // SET 5,A
                A |= 0x20;
                Zero = A == 0;
                Carry = false;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF0: // SET 6,B
                B |= 0x40;
                Zero = B == 0;
                Carry = false;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF1: // SET 6,C
                C |= 0x40;
                Zero = C == 0;
                Carry = false;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF2: // SET 6,D
                D |= 0x40;
                Zero = D == 0;
                Carry = false;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF3: // SET 6,E
                E |= 0x40;
                Zero = E == 0;
                Carry = false;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF4: // SET 6,H
                H |= 0x40;
                Zero = H == 0;
                Carry = false;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF5: // SET 6,L
                L |= 0x40;
                Zero = L == 0;
                Carry = false;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF6: // SET 6,(HL)
                Memory[(H << 8) | L] |= 0x40;
                Zero = Memory[(H << 8) | L] == 0;
                Carry = false;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF7: // SET 6,A
                A |= 0x40;
                Zero = A == 0;
                Carry = false;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF8: // SET 7,B
                B |= 0x80;
                Zero = B == 0;
                Carry = false;
                Sign = B > 0x7F;
                Parity = IsParity(B);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xF9: // SET 7,C
                C |= 0x80;
                Zero = C == 0;
                Carry = false;
                Sign = C > 0x7F;
                Parity = IsParity(C);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xFA: // SET 7,D
                D |= 0x80;
                Zero = D == 0;
                Carry = false;
                Sign = D > 0x7F;
                Parity = IsParity(D);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xFB: // SET 7,E
                E |= 0x80;
                Zero = E == 0;
                Carry = false;
                Sign = E > 0x7F;
                Parity = IsParity(E);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xFC: // SET 7,H
                H |= 0x80;
                Zero = H == 0;
                Carry = false;
                Sign = H > 0x7F;
                Parity = IsParity(H);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xFD: // SET 7,L
                L |= 0x80;
                Zero = L == 0;
                Carry = false;
                Sign = L > 0x7F;
                Parity = IsParity(L);
                HalfCarry = false;
                PC += 2;
                break;
            case 0xFE: // SET 7,(HL)
                Memory[(H << 8) | L] |= 0x80;
                Zero = Memory[(H << 8) | L] == 0;
                Carry = false;
                Sign = Memory[(H << 8) | L] > 0x7F;
                Parity = IsParity((byte)(Memory[(H << 8) | L]));
                HalfCarry = false;
                PC += 2;
                break;
            case 0xFF: // SET 7,A
                A |= 0x80;
                Zero = A == 0;
                Carry = false;
                Sign = A > 0x7F;
                Parity = IsParity(A);
                HalfCarry = false;
                PC += 2;
                break;
        }
    }


    public string DisassembleInstruction(ushort programCounter)
    {
        byte opcode = Memory[programCounter];

        switch (opcode)
        {
            case 0x00: //NOP
                return $"{programCounter:X4}: NOP";
            case 0x01: //LD BC, nn
                return $"{programCounter:X4}: LD BC, {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0x02: //LD (BC), A
                return $"{programCounter:X4}: LD (BC), A";
            case 0x03: //INC BC
                return $"{programCounter:X4}: INC BC";
            case 0x04: //INC B
                return $"{programCounter:X4}: INC B";
            case 0x05: //DEC B
                return $"{programCounter:X4}: DEC B";
            case 0x06: //LD B, n
                return $"{programCounter:X4}: LD B, {Memory[programCounter + 1]:X2}";
            case 0x07: //RLCA
                return $"{programCounter:X4}: RLCA";
            case 0x08: //LD (nn), SP
                return $"{programCounter:X4}: LD ({Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}), SP";
            case 0x09: //ADD HL, BC
                return $"{programCounter:X4}: ADD HL, BC";
            case 0x0A: //LD A, (BC)
                return $"{programCounter:X4}: LD A, (BC)";
            case 0x0B: //DEC BC
                return $"{programCounter:X4}: DEC BC";
            case 0x0C: //INC C
                return $"{programCounter:X4}: INC C";
            case 0x0D: //DEC C
                return $"{programCounter:X4}: DEC C";
            case 0x0E: //LD C, n
                return $"{programCounter:X4}: LD C, {Memory[programCounter + 1]:X2}";
            case 0x0F: //RRCA
                return $"{programCounter:X4}: RRCA";
            case 0x10: //STOP
                return $"{programCounter:X4}: STOP";
            case 0x11: //LD DE, nn
                return $"{programCounter:X4}: LD DE, {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0x12: //LD (DE), A
                return $"{programCounter:X4}: LD (DE), A";
            case 0x13: //INC DE
                return $"{programCounter:X4}: INC DE";
            case 0x14: //INC D
                return $"{programCounter:X4}: INC D";
            case 0x15: //DEC D
                return $"{programCounter:X4}: DEC D";
            case 0x16: //LD D, n
                return $"{programCounter:X4}: LD D, {Memory[programCounter + 1]:X2}";
            case 0x17: //RLA
                return $"{programCounter:X4}: RLA";
            case 0x18: //JR n
                return $"{programCounter:X4}: JR {Memory[programCounter + 1]:X2}";
            case 0x19: //ADD HL, DE
                return $"{programCounter:X4}: ADD HL, DE";
            case 0x1A: //LD A, (DE)
                return $"{programCounter:X4}: LD A, (DE)";
            case 0x1B: //DEC DE
                return $"{programCounter:X4}: DEC DE";
            case 0x1C: //INC E
                return $"{programCounter:X4}: INC E";
            case 0x1D: //DEC E
                return $"{programCounter:X4}: DEC E";
            case 0x1E: //LD E, n
                return $"{programCounter:X4}: LD E, {Memory[programCounter + 1]:X2}";
            case 0x1F: //RRA
                return $"{programCounter:X4}: RRA";
            case 0x20: //JR NZ, n
                return $"{programCounter:X4}: JR NZ, {Memory[programCounter + 1]:X2}";
            case 0x21: //LD HL, nn
                return $"{programCounter:X4}: LD HL, {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0x22: //LD (HL+), A
                return $"{programCounter:X4}: LD (HL+), A";
            case 0x23: //INC HL
                return $"{programCounter:X4}: INC HL";
            case 0x24: //INC H
                return $"{programCounter:X4}: INC H";
            case 0x25: //DEC H
                return $"{programCounter:X4}: DEC H";
            case 0x26: //LD H, n
                return $"{programCounter:X4}: LD H, {Memory[programCounter + 1]:X2}";
            case 0x27: //DAA
                return $"{programCounter:X4}: DAA";
            case 0x28: //JR Z, n
                return $"{programCounter:X4}: JR Z, {Memory[programCounter + 1]:X2}";
            case 0x29: //ADD HL, HL
                return $"{programCounter:X4}: ADD HL, HL";
            case 0x2A: //LD A, (HL+)
                return $"{programCounter:X4}: LD A, (HL+)";
            case 0x2B: //DEC HL
                return $"{programCounter:X4}: DEC HL";
            case 0x2C: //INC L
                return $"{programCounter:X4}: INC L";
            case 0x2D: //DEC L
                return $"{programCounter:X4}: DEC L";
            case 0x2E: //LD L, n
                return $"{programCounter:X4}: LD L, {Memory[programCounter + 1]:X2}";
            case 0x2F: //CPL
                return $"{programCounter:X4}: CPL";
            case 0x30: //JR NC, n
                return $"{programCounter:X4}: JR NC, {Memory[programCounter + 1]:X2}";
            case 0x31: //LD SP, nn
                return $"{programCounter:X4}: LD SP, {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0x32: //LD (HL-), A
                return $"{programCounter:X4}: LD (HL-), A";
            case 0x33: //INC SP
                return $"{programCounter:X4}: INC SP";
            case 0x34: //INC (HL)
                return $"{programCounter:X4}: INC (HL)";
            case 0x35: //DEC (HL)
                return $"{programCounter:X4}: DEC (HL)";
            case 0x36: //LD (HL), n
                return $"{programCounter:X4}: LD (HL), {Memory[programCounter + 1]:X2}";
            case 0x37: //SCF
                return $"{programCounter:X4}: SCF";
            case 0x38: //JR C, n
                return $"{programCounter:X4}: JR C, {Memory[programCounter + 1]:X2}";
            case 0x39: //ADD HL, SP
                return $"{programCounter:X4}: ADD HL, SP";
            case 0x3A: //LD A, (HL-)
                return $"{programCounter:X4}: LD A, (HL-)";
            case 0x3B: //DEC SP
                return $"{programCounter:X4}: DEC SP";
            case 0x3C: //INC A
                return $"{programCounter:X4}: INC A";
            case 0x3D: //DEC A
                return $"{programCounter:X4}: DEC A";
            case 0x3E: //LD A, n
                return $"{programCounter:X4}: LD A, {Memory[programCounter + 1]:X2}";
            case 0x3F: //CCF
                return $"{programCounter:X4}: CCF";
            case 0x40: //LD B, B
                return $"{programCounter:X4}: LD B, B";
            case 0x41: //LD B, C
                return $"{programCounter:X4}: LD B, C";
            case 0x42: //LD B, D
                return $"{programCounter:X4}: LD B, D";
            case 0x43: //LD B, E
                return $"{programCounter:X4}: LD B, E";
            case 0x44: //LD B, H
                return $"{programCounter:X4}: LD B, H";
            case 0x45: //LD B, L
                return $"{programCounter:X4}: LD B, L";
            case 0x46: //LD B, (HL)
                return $"{programCounter:X4}: LD B, (HL)";
            case 0x47: //LD B, A
                return $"{programCounter:X4}: LD B, A";
            case 0x48: //LD C, B
                return $"{programCounter:X4}: LD C, B";
            case 0x49: //LD C, C
                return $"{programCounter:X4}: LD C, C";
            case 0x4A: //LD C, D
                return $"{programCounter:X4}: LD C, D";
            case 0x4B: //LD C, E
                return $"{programCounter:X4}: LD C, E";
            case 0x4C: //LD C, H
                return $"{programCounter:X4}: LD C, H";
            case 0x4D: //LD C, L
                return $"{programCounter:X4}: LD C, L";
            case 0x4E: //LD C, (HL)
                return $"{programCounter:X4}: LD C, (HL)";
            case 0x4F: //LD C, A
                return $"{programCounter:X4}: LD C, A";
            case 0x50: //LD D, B
                return $"{programCounter:X4}: LD D, B";
            case 0x51: //LD D, C
                return $"{programCounter:X4}: LD D, C";
            case 0x52: //LD D, D
                return $"{programCounter:X4}: LD D, D";
            case 0x53: //LD D, E
                return $"{programCounter:X4}: LD D, E";
            case 0x54: //LD D, H
                return $"{programCounter:X4}: LD D, H";
            case 0x55: //LD D, L
                return $"{programCounter:X4}: LD D, L";
            case 0x56: //LD D, (HL)
                return $"{programCounter:X4}: LD D, (HL)";
            case 0x57: //LD D, A
                return $"{programCounter:X4}: LD D, A";
            case 0x58: //LD E, B
                return $"{programCounter:X4}: LD E, B";
            case 0x59: //LD E, C
                return $"{programCounter:X4}: LD E, C";
            case 0x5A: //LD E, D
                return $"{programCounter:X4}: LD E, D";
            case 0x5B: //LD E, E
                return $"{programCounter:X4}: LD E, E";
            case 0x5C: //LD E, H
                return $"{programCounter:X4}: LD E, H";
            case 0x5D: //LD E, L
                return $"{programCounter:X4}: LD E, L";
            case 0x5E: //LD E, (HL)
                return $"{programCounter:X4}: LD E, (HL)";
            case 0x5F: //LD E, A
                return $"{programCounter:X4}: LD E, A";
            case 0x60: //LD H, B
                return $"{programCounter:X4}: LD H, B";
            case 0x61: //LD H, C
                return $"{programCounter:X4}: LD H, C";
            case 0x62: //LD H, D
                return $"{programCounter:X4}: LD H, D";
            case 0x63: //LD H, E
                return $"{programCounter:X4}: LD H, E";
            case 0x64: //LD H, H
                return $"{programCounter:X4}: LD H, H";
            case 0x65: //LD H, L
                return $"{programCounter:X4}: LD H, L";
            case 0x66: //LD H, (HL)
                return $"{programCounter:X4}: LD H, (HL)";
            case 0x67: //LD H, A
                return $"{programCounter:X4}: LD H, A";
            case 0x68: //LD L, B
                return $"{programCounter:X4}: LD L, B";
            case 0x69: //LD L, C
                return $"{programCounter:X4}: LD L, C";
            case 0x6A: //LD L, D
                return $"{programCounter:X4}: LD L, D";
            case 0x6B: //LD L, E
                return $"{programCounter:X4}: LD L, E";
            case 0x6C: //LD L, H
                return $"{programCounter:X4}: LD L, H";
            case 0x6D: //LD L, L
                return $"{programCounter:X4}: LD L, L";
            case 0x6E: //LD L, (HL)
                return $"{programCounter:X4}: LD L, (HL)";
            case 0x6F: //LD L, A
                return $"{programCounter:X4}: LD L, A";
            case 0x70: //LD (HL), B
                return $"{programCounter:X4}: LD (HL), B";
            case 0x71: //LD (HL), C
                return $"{programCounter:X4}: LD (HL), C";
            case 0x72: //LD (HL), D
                return $"{programCounter:X4}: LD (HL), D";
            case 0x73: //LD (HL), E
                return $"{programCounter:X4}: LD (HL), E";
            case 0x74: //LD (HL), H
                return $"{programCounter:X4}: LD (HL), H";
            case 0x75: //LD (HL), L
                return $"{programCounter:X4}: LD (HL), L";
            case 0x76: //HALT
                return $"{programCounter:X4}: HALT";
            case 0x77: //LD (HL), A
                return $"{programCounter:X4}: LD (HL), A";
            case 0x78: //LD A, B
                return $"{programCounter:X4}: LD A, B";
            case 0x79: //LD A, C
                return $"{programCounter:X4}: LD A, C";
            case 0x7A: //LD A, D
                return $"{programCounter:X4}: LD A, D";
            case 0x7B: //LD A, E
                return $"{programCounter:X4}: LD A, E";
            case 0x7C: //LD A, H
                return $"{programCounter:X4}: LD A, H";
            case 0x7D: //LD A, L
                return $"{programCounter:X4}: LD A, L";
            case 0x7E: //LD A, (HL)
                return $"{programCounter:X4}: LD A, (HL)";
            case 0x7F: //LD A, A
                return $"{programCounter:X4}: LD A, A";
            case 0x80: //ADD A, B
                return $"{programCounter:X4}: ADD A, B";
            case 0x81: //ADD A, C
                return $"{programCounter:X4}: ADD A, C";
            case 0x82: //ADD A, D
                return $"{programCounter:X4}: ADD A, D";
            case 0x83: //ADD A, E
                return $"{programCounter:X4}: ADD A, E";
            case 0x84: //ADD A, H
                return $"{programCounter:X4}: ADD A, H";
            case 0x85: //ADD A, L
                return $"{programCounter:X4}: ADD A, L";
            case 0x86: //ADD A, (HL)
                return $"{programCounter:X4}: ADD A, (HL)";
            case 0x87: //ADD A, A
                return $"{programCounter:X4}: ADD A, A";
            case 0x88: //ADC A, B
                return $"{programCounter:X4}: ADC A, B";
            case 0x89: //ADC A, C
                return $"{programCounter:X4}: ADC A, C";
            case 0x8A: //ADC A, D
                return $"{programCounter:X4}: ADC A, D";
            case 0x8B: //ADC A, E
                return $"{programCounter:X4}: ADC A, E";
            case 0x8C: //ADC A, H
                return $"{programCounter:X4}: ADC A, H";
            case 0x8D: //ADC A, L
                return $"{programCounter:X4}: ADC A, L";
            case 0x8E: //ADC A, (HL)
                return $"{programCounter:X4}: ADC A, (HL)";
            case 0x8F: //ADC A, A
                return $"{programCounter:X4}: ADC A, A";
            case 0x90: //SUB B
                return $"{programCounter:X4}: SUB B";
            case 0x91: //SUB C
                return $"{programCounter:X4}: SUB C";
            case 0x92: //SUB D
                return $"{programCounter:X4}: SUB D";
            case 0x93: //SUB E
                return $"{programCounter:X4}: SUB E";
            case 0x94: //SUB H
                return $"{programCounter:X4}: SUB H";
            case 0x95: //SUB L
                return $"{programCounter:X4}: SUB L";
            case 0x96: //SUB (HL)
                return $"{programCounter:X4}: SUB (HL)";
            case 0x97: //SUB A
                return $"{programCounter:X4}: SUB A";
            case 0x98: //SBC A, B
                return $"{programCounter:X4}: SBC A, B";
            case 0x99: //SBC A, C
                return $"{programCounter:X4}: SBC A, C";
            case 0x9A: //SBC A, D
                return $"{programCounter:X4}: SBC A, D";
            case 0x9B: //SBC A, E
                return $"{programCounter:X4}: SBC A, E";
            case 0x9C: //SBC A, H
                return $"{programCounter:X4}: SBC A, H";
            case 0x9D: //SBC A, L
                return $"{programCounter:X4}: SBC A, L";
            case 0x9E: //SBC A, (HL)
                return $"{programCounter:X4}: SBC A, (HL)";
            case 0x9F: //SBC A, A
                return $"{programCounter:X4}: SBC A, A";
            case 0xA0: //AND B
                return $"{programCounter:X4}: AND B";
            case 0xA1: //AND C
                return $"{programCounter:X4}: AND C";
            case 0xA2: //AND D
                return $"{programCounter:X4}: AND D";
            case 0xA3: //AND E
                return $"{programCounter:X4}: AND E";
            case 0xA4: //AND H
                return $"{programCounter:X4}: AND H";
            case 0xA5: //AND L
                return $"{programCounter:X4}: AND L";
            case 0xA6: //AND (HL)
                return $"{programCounter:X4}: AND (HL)";
            case 0xA7: //AND A
                return $"{programCounter:X4}: AND A";
            case 0xA8: //XOR B
                return $"{programCounter:X4}: XOR B";
            case 0xA9: //XOR C
                return $"{programCounter:X4}: XOR C";
            case 0xAA: //XOR D
                return $"{programCounter:X4}: XOR D";
            case 0xAB: //XOR E
                return $"{programCounter:X4}: XOR E";
            case 0xAC: //XOR H
                return $"{programCounter:X4}: XOR H";
            case 0xAD: //XOR L
                return $"{programCounter:X4}: XOR L";
            case 0xAE: //XOR (HL)
                return $"{programCounter:X4}: XOR (HL)";
            case 0xAF: //XOR A
                return $"{programCounter:X4}: XOR A";
            case 0xB0: //OR B
                return $"{programCounter:X4}: OR B";
            case 0xB1: //OR C
                return $"{programCounter:X4}: OR C";
            case 0xB2: //OR D
                return $"{programCounter:X4}: OR D";
            case 0xB3: //OR E
                return $"{programCounter:X4}: OR E";
            case 0xB4: //OR H
                return $"{programCounter:X4}: OR H";
            case 0xB5: //OR L
                return $"{programCounter:X4}: OR L";
            case 0xB6: //OR (HL)
                return $"{programCounter:X4}: OR (HL)";
            case 0xB7: //OR A
                return $"{programCounter:X4}: OR A";
            case 0xB8: //CP B
                return $"{programCounter:X4}: CP B";
            case 0xB9: //CP C
                return $"{programCounter:X4}: CP C";
            case 0xBA: //CP D
                return $"{programCounter:X4}: CP D";
            case 0xBB: //CP E
                return $"{programCounter:X4}: CP E";
            case 0xBC: //CP H
                return $"{programCounter:X4}: CP H";
            case 0xBD: //CP L
                return $"{programCounter:X4}: CP L";
            case 0xBE: //CP (HL)
                return $"{programCounter:X4}: CP (HL)";
            case 0xBF: //CP A
                return $"{programCounter:X4}: CP A";
            case 0xC0: //RET NZ
                return $"{programCounter:X4}: RET NZ";
            case 0xC1: //POP BC
                return $"{programCounter:X4}: POP BC";
            case 0xC2: //JP NZ, nn
                return $"{programCounter:X4}: JP NZ, {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xC3: //JP nn
                return $"{programCounter:X4}: JP {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xC4: //CALL NZ, nn
                return $"{programCounter:X4}: CALL NZ, {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xC5: //PUSH BC
                return $"{programCounter:X4}: PUSH BC";
            case 0xC6: //ADD A, n
                return $"{programCounter:X4}: ADD A, {Memory[programCounter + 1]:X2}";
            case 0xC7: //RST 00H
                return $"{programCounter:X4}: RST 00H";
            case 0xC8: //RET Z
                return $"{programCounter:X4}: RET Z";
            case 0xC9: //RET
                return $"{programCounter:X4}: RET";
            case 0xCA: //JP Z, nn
                return $"{programCounter:X4}: JP Z, {Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xCB: //PREFIX CB
                return $"{programCounter:X4}: PREFIX CB {Memory[programCounter + 1]:X2}";

            case 0xCC:
                return $"{programCounter:X4}: RST 0x08";
            case 0xCD:
                return $"{programCounter:X4}: CALL 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xCE:
                return $"{programCounter:X4}: ADC A, 0x{Memory[programCounter + 1]:X2}";
            case 0xCF:
                return $"{programCounter:X4}: RST 0x08";
            case 0xD0:
                return $"{programCounter:X4}: RET NC";
            case 0xD1:
                return $"{programCounter:X4}: POP DE";
            case 0xD2:
                return $"{programCounter:X4}: JP NC, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";

            case 0xD4:
                return
                    $"{programCounter:X4}: CALL NC, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xD5:
                return $"{programCounter:X4}: PUSH DE";
            case 0xD6:
                return $"{programCounter:X4}: SUB 0x{Memory[programCounter + 1]:X2}";
            case 0xD7:
                return $"{programCounter:X4}: RST 0x10";
            case 0xD8:
                return $"{programCounter:X4}: RET C";
            case 0xD9:
                return $"{programCounter:X4}: EXX";
            case 0xDA:
                return $"{programCounter:X4}: JP C, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xDC:
                return $"{programCounter:X4}: CALL C, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xDE:
                return $"{programCounter:X4}: SBC A, 0x{Memory[programCounter + 1]:X2}";
            case 0xDF:
                return $"{programCounter:X4}: RST 0x18";
            case 0xE0:
                return $"{programCounter:X4}: RET PO";
            case 0xE1:
                return $"{programCounter:X4}: POP HL";
            case 0xE2:
                return $"{programCounter:X4}: JP PO, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xE5:
                return $"{programCounter:X4}: PUSH HL";
            case 0xE6:
                return $"{programCounter:X4}: AND 0x{Memory[programCounter + 1]:X2}";
            case 0xE7:
                return $"{programCounter:X4}: RST 0x20";
            case 0xE8:
                return $"{programCounter:X4}: RET PE";
            case 0xE9:
                return $"{programCounter:X4}: JP (HL)";
            case 0xEA:
                return $"{programCounter:X4}: JP PE, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xEE:
                return $"{programCounter:X4}: XOR 0x{Memory[programCounter + 1]:X2}";
            case 0xEF:
                return $"{programCounter:X4}: RST 0x28";
            case 0xF0:
                return $"{programCounter:X4}: RET P";
            case 0xF1:
                return $"{programCounter:X4}: POP AF";
            case 0xF2:
                return $"{programCounter:X4}: JP P, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xF3:
                return $"{programCounter:X4}: DI";
            case 0xF5:
                return $"{programCounter:X4}: PUSH AF";
            case 0xF6:
                return $"{programCounter:X4}: OR 0x{Memory[programCounter + 1]:X2}";
            case 0xF7:
                return $"{programCounter:X4}: RST 0x30";
            case 0xF8:
                return $"{programCounter:X4}: RET M";
            case 0xF9:
                return $"{programCounter:X4}: LD SP, HL";
            case 0xFA:
                return $"{programCounter:X4}: JP M, 0x{Memory[programCounter + 1]:X2}{Memory[programCounter + 2]:X2}";
            case 0xFB:
                return $"{programCounter:X4}: EI";
            case 0xFE:
                return $"{programCounter:X4}: CP 0x{Memory[programCounter + 1]:X2}";
            case 0xFF:
                return $"{programCounter:X4}: RST 0x38";
            default:
                return $"{programCounter:X4}: Unknown opcode 0x{opcode:X2}";
        }
    }
}