using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Parts.TPKParts;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="TPKBlock"/> is a collection of <see cref="Texture"/>.
    /// </summary>
    public abstract class TPKBlock : ACollectable
    {
        #region Primary Properties

        /// <summary>
        /// Version of this <see cref="TPKBlock"/>.
        /// </summary>
        public abstract eTPKVersion Version { get; }

        /// <summary>
        /// Filename of the <see cref="TPKBlock"/> which was assembled. Has no effect. 0x40 bytes.
        /// </summary>
        public abstract string Filename { get; }

        /// <summary>
        /// BinHash of the Filename property.
        /// </summary>
        public abstract uint FilenameHash { get; }

        /// <summary>
        /// 
        /// </summary>
        public uint PermBlockByteOffset { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public uint PermBlockByteSize { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int EndianSwapped { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int TexturePack { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int TextureIndexEntryTable { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int TextureStreamEntryTable { get; set; } = 0;

        /// <summary>
        /// True if <see cref="TPKBlock"/> is compressed and should be saved 
        /// as compressed; false otherwise.
        /// </summary>
        public abstract eBoolean IsCompressed { get; set; }

        #endregion

        #region Main Properties

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        public override string CollectionName { get; set; }

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.None;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.None.ToString();

        /// <summary>
        /// Binary memory hash of the collection name.
        /// </summary>
        public uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public uint VltKey => this.CollectionName.VltHash();

        /// <summary>
        /// Index of the <see cref="TPKBlock"/> in the Global data.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Filename to which <see cref="TPKBlock"/> belong to.
        /// </summary>
        public string BelongsToFile { get; set; } = String.Empty;

        internal static string Watermark { get; set; } = String.Empty;

        #endregion

        #region Internal Methods

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all textures of this <see cref="TPKBlock"/>.
        /// </summary>
        /// <returns>Textures as an object.</returns>
        public abstract object GetTextures();

        /// <summary>
        /// Sorts <see cref="Texture"/> by their CollectionNames or BinKeys.
        /// </summary>
        /// <param name="by_name">True if sort by name; false is sort by hash.</param>
        public abstract void SortTexturesByType(bool by_name);

        /// <summary>
        /// Assembles <see cref="TPKBlock"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="TPKBlock"/> with.</param>
        /// <returns>Byte array of the tpk block.</returns>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles <see cref="TPKBlock"/> array into separate properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        public abstract void Disassemble(BinaryReader br);

        /// <summary>
        /// Tries to find <see cref="Texture"/> based on the key passed.
        /// </summary>
        /// <param name="key">Key of the <see cref="Texture"/> Collection Name.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <returns>Texture if it is found; null otherwise.</returns>
        public abstract Texture FindTexture(uint key, eKeyType type);

        /// <summary>
        /// Gets index of the <see cref="Texture"/> in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/>.</param>
        /// <param name="type">Key type passed.</param>
        /// <returns>Index number as an integer. If element does not exist, returns -1.</returns>
        public abstract int GetTextureIndex(uint key, eKeyType type);

        /// <summary>
        /// Attempts to add <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="filename">Path of the texture to be imported.</param>
        /// <returns>True if texture adding was successful, false otherwise.</returns>
        public abstract bool TryAddTexture(string CName, string filename);

        /// <summary>
        /// Attempts to add <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="filename">Path of the texture to be imported.</param>
        /// <param name="error">Error occured when trying to add a texture.</param>
        /// <returns>True if texture adding was successful, false otherwise.</returns>
        public abstract bool TryAddTexture(string CName, string filename, out string error);

        /// <summary>
        /// Attempts to remove <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type fo the key passed.</param>
        /// <returns>True if texture removing was successful, false otherwise.</returns>
        public abstract bool TryRemoveTexture(uint key, eKeyType type);

        /// <summary>
        /// Attempts to remove <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="error">Error occured when trying to remove a texture.</param>
        /// <returns>True if texture removing was successful, false otherwise.</returns>
        public abstract bool TryRemoveTexture(uint key, eKeyType type, out string error);

        /// <summary>
        /// Attempts to clone <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <returns>True if texture cloning was successful, false otherwise.</returns>
        public abstract bool TryCloneTexture(string newname, uint key, eKeyType type);

        /// <summary>
        /// Attempts to clone <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="error">Error occured when trying to clone a texture.</param>
        /// <returns>True if texture cloning was successful, false otherwise.</returns>
        public abstract bool TryCloneTexture(string newname, uint key, eKeyType type, out string error);

        /// <summary>
        /// Attemps to replace <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        /// <returns>True if texture replacing was successful, false otherwise.</returns>
        public abstract bool TryReplaceTexture(uint key, eKeyType type, string filename);

        /// <summary>
        /// Attemps to replace <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        /// <param name="error">Error occured when trying to replace a texture.</param>
        /// <returns>True if texture replacing was successful, false otherwise.</returns>
        public abstract bool TryReplaceTexture(uint key, eKeyType type, string filename, out string error);

        #endregion

        #region Reading Methods

        /// <summary>
        /// Finds offsets of all partials and its parts in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        /// <returns>Array of all offsets.</returns>
        protected abstract long[] FindOffsets(BinaryReader br);

        /// <summary>
        /// Gets amount of textures in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        /// <returns>Number of textures in the tpk block.</returns>
        protected abstract int GetTextureCount(BinaryReader br);

        /// <summary>
        /// Gets <see cref="TPKBlock"/> header information.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        protected abstract void GetHeaderInfo(BinaryReader br);

        /// <summary>
        /// Gets list of offset slots of the textures in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        protected abstract IEnumerable<OffSlot> GetOffsetSlots(BinaryReader br);

        /// <summary>
        /// Gets list of offsets and sizes of the texture headers in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        /// <param name="count">Number of textures to read.</param>
        /// <returns>Array of offsets and sizes of texture headers.</returns>
        protected abstract long[] GetTextureHeaders(BinaryReader br, int count);

        /// <summary>
        /// Gets list of compressions of the textures in the tpk block array.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        protected abstract IEnumerable<CompSlot> GetCompressionList(BinaryReader br);

        /// <summary>
        /// Parses compressed texture and returns it on the output.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        /// <param name="offslot">Offslot of the texture to be parsed</param>
        /// <returns>Decompressed texture valid to the current support.</returns>
        protected abstract void ParseCompTexture(BinaryReader br, OffSlot offslot);

        #endregion

        #region Writing Methods

        /// <summary>
        /// Assembles partial 1 part1 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part1.</returns>
        protected abstract void Get1Part1(BinaryWriter bw);

        /// <summary>
        /// Assembles partial 1 part2 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part2.</returns>
        protected abstract void Get1Part2(BinaryWriter bw);

        /// <summary>
        /// Assembles partial 1 part4 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part4.</returns>
        protected abstract void Get1Part4(BinaryWriter bw);

        /// <summary>
        /// Assembles partial 1 part5 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part5.</returns>
        protected abstract void Get1Part5(BinaryWriter bw);

        /// <summary>
        /// Assembles partial 2 part1 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 2 part1.</returns>
        protected abstract void Get2Part1(BinaryWriter bw);

        /// <summary>
        /// Assembles partial 2 part2 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 2 part2.</returns>
        protected abstract void Get2Part2(BinaryWriter bw);

        #endregion
    }
}