using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.TPKParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="TPKBlock"/> is a collection of <see cref="Texture"/>.
    /// </summary>
    public abstract class TPKBlock : Collectable, IAssembly
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
        [MemoryCastable()]
        [Browsable(false)]
        public uint PermBlockByteOffset { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public uint PermBlockByteSize { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int EndianSwapped { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int TexturePack { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int TextureIndexEntryTable { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int TextureStreamEntryTable { get; set; } = 0;

        /// <summary>
        /// True if <see cref="TPKBlock"/> is compressed and should be saved 
        /// as compressed; false otherwise.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public abstract eBoolean IsCompressed { get; set; }

        /// <summary>
        /// Settings data related to this <seealso cref="TPKBlock"/>.
        /// </summary>
        [Browsable(false)]
        public byte[] SettingData { get; set; }

        /// <summary>
        /// Represents all <see cref="AnimSlot"/> of this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public abstract List<AnimSlot> Animations { get; }

        /// <summary>
        /// List of <see cref="Texture"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        [Browsable(false)]
        public abstract List<Texture> Textures { get; }

        /// <summary>
        /// Number of <see cref="Texture"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public abstract int TextureCount { get; }

        /// <summary>
        /// Number of <see cref="AnimSlot"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public int AnimationCount => this.Animations.Count;

        /// <summary>
        /// Custom watermark written on assembly.
        /// </summary>
        internal string Watermark { get; set; }

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
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public uint VltKey => this.CollectionName.VltHash();

        #endregion

        #region Internal Methods

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all textures of this <see cref="TPKBlock"/>.
        /// </summary>
        /// <returns>Textures as an object.</returns>
        public virtual IEnumerable<Texture> GetTextures() => this.Textures;

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
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public abstract void Serialize(BinaryWriter bw);

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public abstract void Deserialize(BinaryReader br);

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
        /// Adds <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="filename">Path of the texture to be imported.</param>
        public abstract void AddTexture(string CName, string filename);

        /// <summary>
        /// Removes <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type fo the key passed.</param>
        public abstract void RemoveTexture(uint key, eKeyType type);

        /// <summary>
        /// Clones <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        public abstract void CloneTexture(string newname, uint key, eKeyType type);

        /// <summary>
        /// Replaces <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        public abstract void ReplaceTexture(uint key, eKeyType type, string filename);

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

        /// <summary>
        /// Gets list of all animations in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        protected virtual void GetAnimations(BinaryReader br)
		{
            var size = br.ReadInt32();
            var offset = br.BaseStream.Position;

            while (br.BaseStream.Position < offset + size)
			{

                var id = br.ReadEnum<eBlockID>();
                var to = br.ReadInt32();

                if (id != eBlockID.TPK_AnimBlock)
				{

                    br.BaseStream.Position += to;
                    continue;

				}
                else
				{

                    var anim = new AnimSlot();
                    anim.Read(br);
                    this.Animations.Add(anim);

				}

			}
		}

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

        /// <summary>
        /// Assembles partial 2 part2 as compressed block and return all offslots generated.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="thisOffset">Offset of this TPK in BinaryWriter passed.</param>
        protected abstract List<OffSlot> Get2CompressedPart2(BinaryWriter bw, int thisOffset);

        /// <summary>
        /// Assembles partial 1 animation block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        protected virtual void Get1PartAnim(BinaryWriter bw)
		{

            if (this.Animations.Count == 0) return;
            bw.WriteEnum(eBlockID.TPK_BinData);
            bw.Write(-1);
            var start = bw.BaseStream.Position;

            foreach (var anim in this.Animations)
			{

                anim.Write(bw);

			}

            var end = bw.BaseStream.Position;
            bw.BaseStream.Position = start - 4;
            bw.Write((int)(end - start));
            bw.BaseStream.Position = end;
		}

        #endregion
    }
}