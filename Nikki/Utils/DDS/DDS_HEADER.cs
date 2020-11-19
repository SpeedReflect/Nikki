using System.Runtime.InteropServices;



namespace Nikki.Utils.DDS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x80)]
    internal class DDS_HEADER
    {
        /* 0x04 - 0x07 */ public uint dwSize;
        /* 0x08 - 0x0B */ public uint dwFlags;
        /* 0x0C - 0x0F */ public uint dwHeight;
        /* 0x10 - 0x13 */ public uint dwWidth;
        /* 0x14 - 0x17 */ public uint dwPitchOrLinearSize;
        /* 0x17 - 0x1B */ public uint dwDepth; // only if DDS_HEADER_FLAGS_VOLUME is set in dwFlags
        /* 0x1C - 0x1F */ public uint dwMipMapCount;
        /* 0x20 - 0x4B */ public uint[] dwReserved1 = new uint[11];
        /* 0x4C - 0x6B */ public DDS_PIXELFORMAT ddspf = new DDS_PIXELFORMAT();
        /* 0x6C - 0x6F */ public uint dwCaps;
        /* 0x70 - 0x73 */ public uint dwCaps2;
        /* 0x74 - 0x77 */ public uint dwCaps3;
        /* 0x78 - 0x7B */ public uint dwCaps4;
        /* 0x7C - 0x7F */ public uint dwReserved2;
        
        public DDS_HEADER()
        {
            for (int loop = 0; loop < 11; ++loop) this.dwReserved1[loop] = 0; // unused
            this.dwCaps2 = 0; // usually it is not a 3D texture
            this.dwCaps3 = 0; // unused
            this.dwCaps4 = 0; // unused
            this.dwReserved2 = 0; // unused
            this.dwSize = 0x7C; // always const, unless stated otherwise
        }
    }
}
