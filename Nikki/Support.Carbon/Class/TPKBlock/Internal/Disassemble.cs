namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Disassembles tpk block array into separate properties.
        /// </summary>
        /// <param name="byteptr_t">Pointer to the tpk block array.</param>
        protected override unsafe void Disassemble(byte* byteptr_t)
        {
            var PartOffsets = this.FindOffsets(byteptr_t);
            var TextureCount = this.GetTextureCount(byteptr_t, PartOffsets[1]);
            if (TextureCount == 0) return; // if no textures allocated

            this.GetHeaderInfo(byteptr_t, PartOffsets[0]);
            this.GetKeyList(byteptr_t, PartOffsets[1]);
            this.GetOffsetSlots(byteptr_t, PartOffsets[2]);
            this.GetCompressionList(byteptr_t, PartOffsets[4]);
            var TextureList = this.GetTextureHeaders(byteptr_t, PartOffsets[3]);

            if (PartOffsets[2] != -1)
            {
                // Check if number of keys is equal to number of texture offslots, pick the least one
                if (TextureCount != this.offslots.Count)
                    TextureCount = (TextureCount > this.offslots.Count) ? this.offslots.Count : TextureCount;

                for (int i = 0; i < TextureCount; ++i)
                    this.ParseCompTexture(byteptr_t, this.offslots[i]);
            }
            else
            {
                // Check if number of keys is equal to number of texture headers, pick the least one
                if (TextureCount != TextureList.Length / 2)
                    TextureCount = (TextureCount > TextureList.Length) ? TextureList.Length : TextureCount;

                // Add textures to the list
                for (int i = 0; i < TextureCount; ++i)
                {
                    var Read = new Texture(byteptr_t, TextureList[i, 0], TextureList[i, 1], this._collection_name, this.Database);
                    this.Textures.Add(Read);
                }

                // Finally, build all .dds files
                for (int i = 0; i < TextureCount; ++i)
                    this.Textures[i].ReadData(byteptr_t, PartOffsets[6] + 0x80, false);
            }
        }
    }
}