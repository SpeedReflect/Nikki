using GlobalLib.Core;
using GlobalLib.Reflection.ID;
using GlobalLib.Utils;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Assembles partial 1 part1 of the tpk block.
        /// </summary>
        /// <returns>Byte array of the partial 1 part1.</returns>
        protected override unsafe byte[] Get1Part1()
        {
            var result = new byte[0x84];
            this.filename = Process.Watermark;
            this.FilenameHash = Bin.Hash(this.filename);
            Map.BinKeys.Remove(this.FilenameHash);
            fixed (byte* byteptr_t = &result[0])
            {
                *(uint*)byteptr_t = TPK.INFO_PART1_BLOCKID; // write ID
                *(uint*)(byteptr_t + 4) = 0x7C; // write size
                *(int*)(byteptr_t + 8) = this.Version;

                // Write CollectionName
                string CName = string.Empty;
                if (this._use_current_cname)
                    CName = this._collection_name;
                else
                    CName = this._collection_name.Substring(2, this._collection_name.Length - 2);
                for (int a1 = 0; a1 < CName.Length; ++a1)
                    *(byteptr_t + 0xC + a1) = (byte)CName[a1];

                // Write Filename
                for (int a1 = 0; a1 < this.filename.Length; ++a1)
                    *(byteptr_t + 0x28 + a1) = (byte)this.filename[a1];

                // Write all other settings
                *(uint*)(byteptr_t + 0x68) = this.FilenameHash;
                *(uint*)(byteptr_t + 0x6C) = this.PermBlockByteOffset;
                *(uint*)(byteptr_t + 0x70) = this.PermBlockByteSize;
                *(int*)(byteptr_t + 0x74) = this.EndianSwapped;
                *(int*)(byteptr_t + 0x78) = this.TexturePack;
                *(int*)(byteptr_t + 0x7C) = this.TextureIndexEntryTable;
                *(int*)(byteptr_t + 0x80) = this.TextureStreamEntryTable;
            }
            return result;
        }
    }
}