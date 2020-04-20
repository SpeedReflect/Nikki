using System.IO;



namespace Nikki.Utils
{
    /// <summary>
    /// Collection of methods of unlocking EA games' memory files.
    /// </summary>
    public static class MemoryUnlock
    {
        /// <summary>
        /// Creates a 16-bytes unlocked memory file.
        /// </summary>
        /// <param name="filename">Filename to be created/replaced/overwritten.</param>
        public static void FastUnlock(string filename)
        {
            if (!File.Exists(filename)) return;
            using var MemoryWriter = new BinaryWriter(File.Open(filename, FileMode.Create));
            MemoryWriter.Write(0x00000000);
            MemoryWriter.Write(0x00000000);
            MemoryWriter.Write(0x53219999);
            MemoryWriter.Write(0x00000000);
        }

        /// <summary>
        /// Creates a file of assembly generated info.
        /// </summary>
        /// <param name="filename">Filename to be created/replaced/overwritten.</param>
        public static void LongUnlock(string filename)
        {
            if (!File.Exists(filename)) return;
            using var MemoryWriter = new BinaryWriter(File.Open(filename, FileMode.Create));
            MemoryWriter.Write(0x00000000);
            MemoryWriter.Write(0x00000000);
            MemoryWriter.Write(0x53219999);
            MemoryWriter.Write(0x00000001);


            MemoryWriter.Write(0x97E7DAA1);
            MemoryWriter.Write(0x00000024);
            MemoryWriter.Write(0x0000014A);
            MemoryWriter.Write(0x0000014A);


            MemoryWriter.Write(0x0000016E);
            MemoryWriter.Write(0x0A0D2F2F);
            MemoryWriter.Write(0x42202F2F);
            MemoryWriter.Write(0x646C6975);


            MemoryWriter.Write(0x73726556);
            MemoryWriter.Write(0x2E6E6F69);
            MemoryWriter.Write(0x206F6F6D);
            MemoryWriter.Write(0x6547202D);


            MemoryWriter.Write(0x6172656E);
            MemoryWriter.Write(0x20646574);
            MemoryWriter.Write(0x6D6F7266);
            MemoryWriter.Write(0x6B614D20);


            MemoryWriter.Write(0x6C696665);
            MemoryWriter.Write(0x70632E65);
            MemoryWriter.Write(0x2F0A0D70);
            MemoryWriter.Write(0x0D0A0D2F);


            MemoryWriter.Write(0x63614D0A);
            MemoryWriter.Write(0x656E6968);
            MemoryWriter.Write(0x0909093A);
            MemoryWriter.Write(0x31442209);


            MemoryWriter.Write(0x35353130);
            MemoryWriter.Write(0x09223637);
            MemoryWriter.Write(0x202F2F09);
            MemoryWriter.Write(0x656D614E);


            MemoryWriter.Write(0x20666F20);
            MemoryWriter.Write(0x706D6F63);
            MemoryWriter.Write(0x72657475);
            MemoryWriter.Write(0x61687420);


            MemoryWriter.Write(0x75622074);
            MemoryWriter.Write(0x20746C69);
            MemoryWriter.Write(0x73696874);
            MemoryWriter.Write(0x72657620);


            MemoryWriter.Write(0x6E6F6973);
            MemoryWriter.Write(0x68430A0D);
            MemoryWriter.Write(0x65676E61);
            MemoryWriter.Write(0x7473696C);


            MemoryWriter.Write(0x656D614E);
            MemoryWriter.Write(0x0909093A);
            MemoryWriter.Write(0x6B6E5522);
            MemoryWriter.Write(0x6E776F6E);


            MemoryWriter.Write(0x2F090922);
            MemoryWriter.Write(0x6550202F);
            MemoryWriter.Write(0x726F6672);
            MemoryWriter.Write(0x63206563);


            MemoryWriter.Write(0x676E6168);
            MemoryWriter.Write(0x73696C65);
            MemoryWriter.Write(0x65722074);
            MemoryWriter.Write(0x65697274);


            MemoryWriter.Write(0x20646576);
            MemoryWriter.Write(0x68746977);
            MemoryWriter.Write(0x34702720);
            MemoryWriter.Write(0x61686320);


            MemoryWriter.Write(0x7365676E);
            MemoryWriter.Write(0x430A0D27);
            MemoryWriter.Write(0x676E6168);
            MemoryWriter.Write(0x73696C65);


            MemoryWriter.Write(0x6D754E74);
            MemoryWriter.Write(0x3A726562);
            MemoryWriter.Write(0x09300909);
            MemoryWriter.Write(0x2F2F0909);


            MemoryWriter.Write(0x61684320);
            MemoryWriter.Write(0x6C65676E);
            MemoryWriter.Write(0x20747369);
            MemoryWriter.Write(0x78652023);


            MemoryWriter.Write(0x63617274);
            MemoryWriter.Write(0x20646574);
            MemoryWriter.Write(0x6D6F7266);
            MemoryWriter.Write(0x6F626120);


            MemoryWriter.Write(0x73206576);
            MemoryWriter.Write(0x6E697274);
            MemoryWriter.Write(0x420A0D67);
            MemoryWriter.Write(0x646C6975);


            MemoryWriter.Write(0x73726556);
            MemoryWriter.Write(0x4E6E6F69);
            MemoryWriter.Write(0x3A656D61);
            MemoryWriter.Write(0x22220909);


            MemoryWriter.Write(0x75420A0D);
            MemoryWriter.Write(0x56646C69);
            MemoryWriter.Write(0x69737265);
            MemoryWriter.Write(0x75466E6F);


            MemoryWriter.Write(0x614E6C6C);
            MemoryWriter.Write(0x093A656D);
            MemoryWriter.Write(0x0A0D2222);
            MemoryWriter.Write((short)0x0A0D);
        }
    }
}