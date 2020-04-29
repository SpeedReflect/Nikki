using GlobalLib.Reflection.ID;
using GlobalLib.Utils.EA;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Gets list of compressions of the textures in the tpk block array.
        /// </summary>
        /// <param name="byteptr_t">Pointer to the tpk block array.</param>
        /// <param name="offset">Partial 1 part5 offset in the tpk block array.</param>
        protected override unsafe void GetCompressionList(byte* byteptr_t, int offset)
        {
            if (offset == -1) return;  // if Part5 does not exist
            if (*(uint*)(byteptr_t + offset) != TPK.INFO_PART5_BLOCKID)
                return; // check Part5 ID

            int ReaderSize = 8 + *(int*)(byteptr_t + offset + 4);
            int current = 0x14;
            while (current < ReaderSize)
            {
                uint comp = *(uint*)(byteptr_t + offset + current);
                if (Comp.IsComp(comp))
                    this.compressions.Add(comp);
                current += 0x18;
            }
        }
    }
}