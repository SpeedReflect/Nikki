using GlobalLib.Reflection.ID;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Assembles partial 1 part2 of the tpk block.
        /// </summary>
        /// <returns>Byte array of the partial 1 part2.</returns>
        protected override unsafe byte[] Get1Part2()
        {
            var result = new byte[8 + this.keys.Count * 8];
            fixed (byte* byteptr_t = &result[8])
            {
                *(uint*)(byteptr_t - 8) = TPK.INFO_PART2_BLOCKID; // write ID
                *(int*)(byteptr_t - 4) = this.keys.Count * 8; // write size
                for (int a1 = 0; a1 < this.keys.Count; ++a1)
                    *(uint*)(byteptr_t + a1 * 8) = this.keys[a1];
            }
            return result;
        }
    }
}