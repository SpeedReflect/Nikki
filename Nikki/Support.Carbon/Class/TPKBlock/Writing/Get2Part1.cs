using GlobalLib.Reflection.ID;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Assembles partial 2 part1 of the tpk block.
        /// </summary>
        /// <returns>Byte array of the partial 2 part1.</returns>
        protected override unsafe byte[] Get2Part1()
        {
            var result = new byte[0x78];
            fixed (byte* byteptr_t = &result[0])
            {
                *(uint*)byteptr_t = TPK.DATA_PART1_BLOCKID; // write ID
                *(int*)(byteptr_t + 4) = 0x18; // write size
                *(int*)(byteptr_t + 0x10) = 1;
                *(uint*)(byteptr_t + 0x14) = this.FilenameHash;
                *(int*)(byteptr_t + 0x24) = 0x50;
            }
            return result;
        }
    }
}