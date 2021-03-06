﻿namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public interface ICodeExtract
    {
        int extract_A(int codepoint);

        int extract_C(int codepoint);

        int extract_B(int codepoint);

        int extract_Bx(int codepoint);

        int extract_sBx(int codepoint);

        int extract_op(int codepoint);
    }
}
