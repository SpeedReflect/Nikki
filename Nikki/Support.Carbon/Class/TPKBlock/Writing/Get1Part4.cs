using GlobalLib.Reflection.ID;
using System;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Assembles partial 1 part4 of the tpk block.
        /// </summary>
        /// <returns>Byte array of the partial 1 part4.</returns>
        protected override unsafe byte[] Get1Part4()
        {
            var result = new byte[8 + this.keys.Count * 0x7C]; // later resize
            int offheader = 8; // for calculating header offsets
            int offdata = 0; // for calculating data offsets
            fixed (byte* byteptr_t = &result[0])
            {
                *(uint*)byteptr_t = TPK.INFO_PART4_BLOCKID; // write ID
                for (int a1 = 0; a1 < this.keys.Count; ++a1)
                {
                    this.Textures[a1].Assemble(byteptr_t, ref offheader, ref offdata);
                }
                *(int*)(byteptr_t + 4) = offheader - 8; // write size
            }
            Array.Resize(ref result, offheader);
            return result;
        }
    }
}