using GlobalLib.Reflection.ID;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Assembles partial 1 part5 of the tpk block.
        /// </summary>
        /// <returns>Byte array of the partial 1 part5.</returns>
        protected override unsafe byte[] Get1Part5()
        {
            var result = new byte[8 + this.keys.Count * 0x18];
            fixed (byte* byteptr_t = &result[0x14])
            {
                *(uint*)(byteptr_t - 0x14) = TPK.INFO_PART5_BLOCKID; // write ID
                *(int*)(byteptr_t - 0x10) = this.keys.Count * 0x18; // write size
                for (int a1 = 0; a1 < this.keys.Count; ++a1)
                    *(uint*)(byteptr_t + a1 * 0x18) = this.compressions[a1];
            }
            return result;
        }
    }
}