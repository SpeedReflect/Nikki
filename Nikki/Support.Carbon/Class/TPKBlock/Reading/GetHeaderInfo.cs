using GlobalLib.Reflection.ID;
using GlobalLib.Utils;
using GlobalLib.Utils.EA;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Gets tpk header information.
        /// </summary>
        /// <param name="byteptr_t">Pointer to the tpk block array.</param>
        /// <param name="offset">Partial 1 part1 offset in the tpk block array.</param>
        protected override unsafe void GetHeaderInfo(byte* byteptr_t, int offset)
        {
            if (*(uint*)(byteptr_t + offset) != TPK.INFO_PART1_BLOCKID)
                return; // check Part1 ID
            if (*(uint*)(byteptr_t + offset + 4) != 0x7C)
                return; // check header size

            // Get CollectionName
            if (this._use_current_cname)
                this._collection_name = ScriptX.NullTerminatedString(byteptr_t + offset + 0xC, 0x1C);
            else
                this._collection_name = this.Index.ToString() + "_" + Comp.GetTPKName(this.Index, this.GameINT);

            // Get Filename
            this.filename = ScriptX.NullTerminatedString(byteptr_t + offset + 0x28, 0x40);

            // Get the rest of the settings
            this.Version = *(int*)(byteptr_t + offset + 8);
            this.FilenameHash = *(uint*)(byteptr_t + offset + 0x68);
            this.PermBlockByteOffset = *(uint*)(byteptr_t + offset + 0x6C);
            this.PermBlockByteSize = *(uint*)(byteptr_t + offset + 0x70);
            this.EndianSwapped = *(int*)(byteptr_t + offset + 0x74);
            this.TexturePack = *(int*)(byteptr_t + offset + 0x78);
            this.TextureIndexEntryTable = *(int*)(byteptr_t + offset + 0x7C);
            this.TextureStreamEntryTable = *(int*)(byteptr_t + offset + 0x80);
        }
    }
}