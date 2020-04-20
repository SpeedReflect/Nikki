using System.Runtime.InteropServices;



namespace Nikki.Utils.DDS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x20)]
    internal class DDS_PIXELFORMAT
    {
        /* 0x4C - 0x4F */ public uint dwSize;
        /* 0x50 - 0x53 */ public uint dwFlags;
        /* 0x50 - 0x57 */ public uint dwFourCC;
        /* 0x50 - 0x5B */ public uint dwRGBBitCount;
        /* 0x50 - 0x5F */ public uint dwRBitMask;
        /* 0x60 - 0x63 */ public uint dwGBitMask;
        /* 0x60 - 0x67 */ public uint dwBBitMask;
        /* 0x60 - 0x6B */ public uint dwABitMask;
    }
}
