using GlobalLib.Reflection.ID;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Gets amount of textures in the tpk block.
        /// </summary>
        /// <param name="byteptr_t">Pointer to the tpk block array.</param>
        /// <param name="offset">Partial 1 part2 offset in the tpk block array.</param>
        /// <returns>Number of textures in the tpk block.</returns>
        protected override unsafe int GetTextureCount(byte* byteptr_t, int offset)
        {
            if (offset == -1) return 0; // check if Part2 even exists
            uint ReaderID = *(uint*)(byteptr_t + offset);
            int ReaderSize = *(int*)(byteptr_t + offset + 4);
            if (ReaderID != TPK.INFO_PART2_BLOCKID) return 0; // check if ID matches

            return (ReaderSize / 8); // 8 bytes for one texture
        }
    }
}