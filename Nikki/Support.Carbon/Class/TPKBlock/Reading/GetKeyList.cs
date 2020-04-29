namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Gets list of keys of the textures in the tpk block array.
        /// </summary>
        /// <param name="byteptr_t">Pointer to the tpk block array.</param>
        /// <param name="offset">Partial 1 part2 offset in the tpk block array.</param>
        protected override unsafe void GetKeyList(byte* byteptr_t, int offset)
        {
            int ReaderSize = 8 + *(int*)(byteptr_t + offset + 4);
            int current = 8;
            while (current < ReaderSize)
            {
                this.keys.Add(*(uint*)(byteptr_t + offset + current));
                current += 8;
            }
        }
    }
}