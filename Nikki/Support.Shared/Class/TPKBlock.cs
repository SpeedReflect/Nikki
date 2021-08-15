using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
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
        #region Shared Enums

        /// <summary>
        /// Enum of <see cref="TPKBlock"/> version types.
        /// </summary>
        public enum TPKVersion : int
        {
            /// <summary>
            /// 
            /// </summary>
            Underground1 = 4,

            /// <summary>
            /// 
            /// </summary>
            Underground2 = 5,

            /// <summary>
            /// 
            /// </summary>
            MostWanted = 5,

            /// <summary>
            /// 
            /// </summary>
            Carbon = 8,

            /// <summary>
            /// 
            /// </summary>
            Prostreet = 8,

            /// <summary>
            /// 
            /// </summary>
            Undercover = 9,
        }

        /// <summary>
        /// Compression type of a <see cref="TPKBlock"/>.
        /// </summary>
        public enum TPKCompressionType : int
        {
            /// <summary>
            /// All TPK textures data are stored raw decompressed.
            /// </summary>
            RawDecompressed = 0,

            /// <summary>
            /// All TPK textures data are stored decompressed using stream rules.
            /// </summary>
            StreamDecompressed = 1,

            /// <summary>
            /// All TPK textures data are compressed fully without splitting in parts.
            /// </summary>
            CompressedFullData = 2,

            /// <summary>
            /// All TPK textures data are compressed fully by splitting and compressing them in parts.
            /// </summary>
            CompressedByParts = 3,

            /// <summary>
            /// MiniMap compression type, where TPK is compressed and put inside 0x0003A100 block.
            /// </summary>
            CompressedMiniMap = 4,
        }

        #endregion

        #region Primary Properties

        /// <summary>
        /// Version of this <see cref="TPKBlock"/>.
        /// </summary>
        public abstract TPKVersion Version { get; }

        /// <summary>
        /// Filename of the <see cref="TPKBlock"/> which was assembled. Has no effect. 0x40 bytes.
        /// </summary>
        public abstract string Filename { get; }

        /// <summary>
        /// BinHash of the Filename property.
        /// </summary>
        public abstract uint FilenameHash { get; }

        /// <summary>
        /// <see cref="TPKCompressionType"/> of this <see cref="TPKBlock"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public virtual TPKCompressionType CompressionType { get; set; }

        /// <summary>
        /// Represents all <see cref="AnimSlot"/> of this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public abstract List<AnimSlot> Animations { get; }

        /// <summary>
        /// Represents all <see cref="TexturePage"/> of this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public abstract List<TexturePage> TexturePages { get; }

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
        /// Number of <see cref="TexturePage"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public int TexturePageCount => this.TexturePages.Count;

        /// <summary>
        /// Custom watermark written on assembly.
        /// </summary>
        internal string Watermark { get; set; }

        /// <summary>
        /// Indicates size of compressed texture header and compression block struct.
        /// </summary>
        protected abstract int CompTexHeaderSize { get; }

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
        /// Gets all textures of this <see cref="TPKBlock"/>.
        /// </summary>
        /// <returns>Textures as an object.</returns>
        public virtual IEnumerable<Texture> GetTextures() => this.Textures;

        /// <summary>
        /// Sorts <see cref="Texture"/> by their CollectionNames or BinKeys.
        /// </summary>
        /// <param name="by_name">True if sort by name; false is sort by hash.</param>
        public void SortTexturesByType(bool by_name)
        {
            if (!by_name) this.Textures.Sort((x, y) => x.BinKey.CompareTo(y.BinKey));
            else this.Textures.Sort((x, y) => x.CollectionName.CompareTo(y.CollectionName));
        }

        /// <summary>
        /// Tries to find <see cref="Texture"/> based on the key passed.
        /// </summary>
        /// <param name="key">Key of the <see cref="Texture"/> Collection Name.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <returns>Texture if it is found; null otherwise.</returns>
        public Texture FindTexture(uint key, KeyType type) =>
            type switch
            {
                KeyType.BINKEY => this.Textures.Find(_ => _.BinKey == key),
                KeyType.VLTKEY => this.Textures.Find(_ => _.VltKey == key),
                KeyType.CUSTOM => throw new NotImplementedException(),
                _ => null
            };

        /// <summary>
        /// Gets index of the <see cref="Texture"/> in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/>.</param>
        /// <param name="type">Key type passed.</param>
        /// <returns>Index number as an integer. If element does not exist, returns -1.</returns>
        public int GetTextureIndex(uint key, KeyType type)
		{
            switch (type)
            {

                case KeyType.BINKEY:
                    for (int loop = 0; loop < this.Textures.Count; ++loop)
                    {

                        if (this.Textures[loop].BinKey == key) return loop;

                    }
                    break;

                case KeyType.VLTKEY:
                    for (int loop = 0; loop < this.Textures.Count; ++loop)
                    {

                        if (this.Textures[loop].VltKey == key) return loop;

                    }
                    break;

                case KeyType.CUSTOM:
                    throw new NotImplementedException();

                default:
                    break;
            }

            return -1;
        }

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
        public void RemoveTexture(uint key, KeyType type)
		{
            var index = this.GetTextureIndex(key, type);

            if (index == -1)
            {

                throw new Exception($"Texture with key 0x{key:X8} does not exist");

            }

            this.Textures.RemoveAt(index);
        }

        /// <summary>
        /// Clones <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        public abstract void CloneTexture(string newname, uint key, KeyType type);

        /// <summary>
        /// Replaces <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        public void ReplaceTexture(uint key, KeyType type, string filename)
		{
            var tex = this.FindTexture(key, type);

            if (tex == null)
            {

                throw new Exception($"Texture with key 0x{key:X8} does not exist");

            }

            if (!Comp.IsDDSTexture(filename, out string error))
            {

                throw new ArgumentException(error);

            }

            tex.Reload(filename);
        }

        /// <summary>
        /// Reads all <see cref="TexturePage"/> using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read with.</param>
        public void ReadTexturePages(BinaryReader br)
		{
            var id = br.ReadEnum<BinBlockID>();
            var size = br.ReadInt32();

            if (id != BinBlockID.EmitterTexturePage) return;

            var current = br.BaseStream.Position;

            br.AlignReaderPow2(0x10);

            size -= (int)(br.BaseStream.Position - current);

            for (int i = 0; i < size >> 5; ++i)
			{

                var texturePage = new TexturePage();
                texturePage.Read(br);
                this.TexturePages.Add(texturePage);
			
            }
		}

        /// <summary>
        /// Writes all <see cref="TexturePage"/> using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
        public void WriteTexturePages(BinaryWriter bw)
		{
            bw.WriteEnum(BinBlockID.EmitterTexturePage);
            bw.Write(-1);

            var start = bw.BaseStream.Position;

            bw.AlignWriterPow2(0x10);

            for (int i = 0; i < this.TexturePageCount; ++i)
			{

                this.TexturePages[i].Write(bw);

			}

            var end = bw.BaseStream.Position;

            bw.BaseStream.Position = start - 4;
            bw.Write((int)(end - start));
            bw.BaseStream.Position = end;
		}

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
        /// Gets list of all animations in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        protected void GetAnimations(BinaryReader br)
		{
            var size = br.ReadInt32();
            var offset = br.BaseStream.Position;

            while (br.BaseStream.Position < offset + size)
			{

                var id = br.ReadEnum<BinBlockID>();
                var to = br.ReadInt32();

                if (id != BinBlockID.TPK_AnimBlock)
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

        /// <summary>
        /// Parses all compressed textures using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="offslots">An enumeration of texture <see cref="OffSlot"/>.</param>
        protected virtual void ParseCompTextures(BinaryReader br, IEnumerable<OffSlot> offslots)
		{
            var start = br.BaseStream.Position;

            foreach (var offslot in offslots)
            {

                Texture texture;

                if (offslot.Flags == 0 || offslot.Flags == 1)
                {

                    // Set position of the reader
                    br.BaseStream.Position = start + offslot.AbsoluteOffset;

                    // Textures are as one, meaning we read once and it is compressed block itself
                    var array = br.ReadBytes(offslot.EncodedSize);
                    if (offslot.Flags == 1) array = Interop.Decompress(array);

                    using var ms = new MemoryStream(array);
                    using var texr = new BinaryReader(ms);

                    // Texture header is located at the end of data
                    int datalength = array.Length - this.CompTexHeaderSize;
                    texr.BaseStream.Position = datalength;

                    // Create new texture based on header found
                    texture = this.CreateNewTexture(texr);

                    // Initialize stack for data and copy it
                    var textureData = new byte[texture.Size + texture.PaletteSize];

                    // Calculate offsets of data and palette
                    int datoff = 0;
                    int paloff = 0;
                    var before = texture.PaletteOffset > texture.PaletteSize;

                    if (before) paloff = texture.PaletteOffset - texture.Offset;
                    else datoff = texture.Offset - texture.PaletteOffset;

                    // Quick way to copy palette in front and data to the back
                    if (texture.PaletteSize == 0)
                    {

                        Array.Copy(array, 0, textureData, 0, texture.Size);

                    }
                    else
                    {

                        Array.Copy(array, paloff, textureData, 0, texture.PaletteSize);
                        Array.Copy(array, datoff, textureData, texture.PaletteSize, texture.Size);

                    }

                    // Assign data and add texture
                    texture.Data = textureData;
                    this.Textures.Add(texture);

                }
                else if (offslot.Flags == 2)
                {

                    // Set position of the reader
                    br.BaseStream.Position = start + offslot.AbsoluteOffset;
                    var offset = br.BaseStream.Position;

                    // Magic list that contains MagicHeaders
                    int total = 0;
                    var magiclist = new List<MagicHeader>(offslot.EncodedSize / 0x4000 + 1);

                    // Read while position in the stream is less than encoded size specified
                    while (br.BaseStream.Position < offset + offslot.EncodedSize)
                    {

                        // We read till we find magic compressed block number
                        if (br.ReadEnum<BinBlockID>() != BinBlockID.LZCompressed) continue;

                        var magic = new MagicHeader();
                        magic.Read(br);

                        total += magic.Length;
                        magiclist.Add(magic);

                    }

                    // If no data was read, we return; else if magic count is more than 1, sort by positions
                    if (magiclist.Count == 0) continue;
                    else if (magiclist.Count == 1) { }
                    else magiclist.Sort((x, y) => x.DecodedDataPosition.CompareTo(y.DecodedDataPosition));

                    // Combine all magic headers into one array
                    var array = new byte[total];

                    for (int i = 0, off = 0; i < magiclist.Count; ++i)
					{

                        var magic = magiclist[i];
                        Array.Copy(magic.Data, 0, array, off, magic.Length);
                        off += magic.Length;

					}

                    // Header is always located at the end of data, meaning last MagicHeader
                    var header = magiclist[^1];
                    using (var ms = new MemoryStream(header.Data))
                    using (var texr = new BinaryReader(ms))
                    {
                        // Texture header is located at the end of data
                        texr.BaseStream.Position = header.Length - this.CompTexHeaderSize;
                        texture = this.CreateNewTexture(texr);
                    }

                    // Calculate offsets of data and palette
                    int datoff = 0;
                    int paloff = 0;
                    var before = texture.PaletteOffset > texture.Offset;

                    if (before) paloff = texture.PaletteOffset - texture.Offset;
                    else datoff = texture.Offset - texture.PaletteOffset;

                    // Initialize stack for data
                    var textureData = new byte[texture.Size + texture.PaletteSize];

                    // Quick way to copy palette in front and data to the back
                    if (texture.PaletteSize == 0)
                    {

                        Array.Copy(array, 0, textureData, 0, texture.Size);

                    }
                    else
                    {

                        Array.Copy(array, paloff, textureData, 0, texture.PaletteSize);
                        Array.Copy(array, datoff, textureData, texture.PaletteSize, texture.Size);

                    }

                    // Assign data and add texture
                    texture.Data = textureData;
                    this.Textures.Add(texture);

                }
                else continue;

                this.CompressionType = offslot.Flags switch
                {
                    1 => TPKCompressionType.CompressedFullData,
                    2 => TPKCompressionType.CompressedByParts,
                    _ => TPKCompressionType.StreamDecompressed,
                };

            }

        }

        /// <summary>
        /// Creates new texture header and reads compression data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <returns>A <see cref="Texture"/> got from read data.</returns>
        protected abstract Texture CreateNewTexture(BinaryReader br);

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
        /// Assembles partial 1 animation block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        protected void Get1PartAnim(BinaryWriter bw)
		{

            if (this.Animations.Count == 0) return;
            bw.WriteEnum(BinBlockID.TPK_BinData);
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

        /// <summary>
        /// Assembles partial 2 part2 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 2 part2.</returns>
        protected void Get2DecodedPart2(BinaryWriter bw)
		{
            bw.WriteEnum(BinBlockID.TPK_DataPart2); // write ID
            bw.Write(-1); // write size
            var position = bw.BaseStream.Position;

            for (int loop = 0; loop < 30; ++loop)
            {

                bw.Write(0x11111111);

            }

            for (int loop = 0; loop < this.Textures.Count; ++loop)
            {

                var data = this.Textures[loop].Data;
                bw.Write(data);
                bw.FillBuffer(0x80);

            }

            bw.BaseStream.Position = position - 4;
            bw.Write((int)(bw.BaseStream.Length - position));
            bw.BaseStream.Position = bw.BaseStream.Length;
        }

        /// <summary>
        /// Gets list of <see cref="OffSlot"/> by compressing all texture data by parts.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="thisOffset">Offset of this <see cref="TPKBlock"/> in the buffer.</param>
        /// <returns><see cref="List{T}"/> of <see cref="OffSlot"/> from textures written.</returns>
        protected List<OffSlot> Get2EncodedPart2(BinaryWriter bw, int thisOffset)
		{
            return this.CompressionType switch
            {
                TPKCompressionType.StreamDecompressed => this.GetStreamDecompressed(bw, thisOffset),
                TPKCompressionType.CompressedFullData => this.GetCompressedFullData(bw, thisOffset),
                TPKCompressionType.CompressedByParts => this.GetCompressedByParts(bw, thisOffset),
                _ => null
            };
		}

        /// <summary>
        /// Gets list of <see cref="OffSlot"/> by writing texture data using decompressed stream rules.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="thisOffset">Offset of this <see cref="TPKBlock"/> in the buffer.</param>
        /// <returns><see cref="List{T}"/> of <see cref="OffSlot"/> from textures written.</returns>
        protected List<OffSlot> GetStreamDecompressed(BinaryWriter bw, int thisOffset)
        {
            // Initialize result offslot list
            var result = new List<OffSlot>(this.Textures.Count);

            // Save position and write ID with temporary size
            bw.WriteEnum(BinBlockID.TPK_DataPart2);
            bw.Write(0xFFFFFFFF);
            var start = bw.BaseStream.Position;

            // Write padding alignment
            for (int loop = 0; loop < 30; ++loop) bw.Write(0x11111111);

            // Precalculate initial capacity for the stream
            int capacity = this.Textures.Count << 16; // count * 0x8000
            int totalTexSize = 0; // to keep track of total texture data length

            // Action delegate to calculate next texture offset
            var CalculateNextOffset = new Action<int>((texlen) =>
            {

                totalTexSize += texlen;
                var dif = 0x80 - totalTexSize % 0x80;
                if (dif != 0x80) totalTexSize += dif;

            });

            // Iterate through every texture. Each iteration creates an OffSlot class 
            // that is yield returned to IEnumerable output. AbsoluteOffset of each 
            // OffSlot initially is offset from the beginning of this TPKBlock in buffer.
            foreach (var texture in this.Textures)
            {

                // Initialize array of texture data and header, write it
                var array = new byte[texture.DataLength + this.CompTexHeaderSize];

                var textureData = texture.Data;

                if (texture.HasPalette)
                {

                    Array.Copy(textureData, texture.PaletteSize, array, 0, texture.Size);
                    Array.Copy(textureData, 0, array, texture.Size, texture.PaletteSize);

                }
                else
                {

                    Array.Copy(textureData, 0, array, 0, textureData.Length);

                }

                using (var strMemory = new MemoryStream(array))
                using (var strWriter = new BinaryWriter(strMemory))
                {

                    strWriter.BaseStream.Position = textureData.Length;
                    this.WriteDownTexture(texture, strWriter, totalTexSize);
                    CalculateNextOffset(textureData.Length);

                }

                // Create new Offslot that will be yield returned
                var offslot = new OffSlot()
                {
                    Key = texture.BinKey,
                    AbsoluteOffset = (int)(bw.BaseStream.Position - thisOffset),
                    DecodedSize = textureData.Length + this.CompTexHeaderSize,
                    EncodedSize = array.Length,
                    UserFlags = 0,
                    Flags = 0,
                    RefCount = 0,
                    UnknownInt32 = 0,
                };

                // Write compressed data
                bw.Write(array);

                // Fill buffer till offset % 0x40
                bw.FillBuffer(0x40);

                // Yield return OffSlot made
                result.Add(offslot);

            }

            // Finally, fix size at the beginning of the block
            var final = bw.BaseStream.Position;
            bw.BaseStream.Position = start - 4;
            bw.Write((int)(final - start));
            bw.BaseStream.Position = final;

            // Return result list
            return result;
        }

        /// <summary>
        /// Gets list of <see cref="OffSlot"/> by fully compressing all texture data.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="thisOffset">Offset of this <see cref="TPKBlock"/> in the buffer.</param>
        /// <returns><see cref="List{T}"/> of <see cref="OffSlot"/> from textures written.</returns>
        protected List<OffSlot> GetCompressedFullData(BinaryWriter bw, int thisOffset)
		{
            // Initialize result offslot list
            var result = new List<OffSlot>(this.Textures.Count);

            // Save position and write ID with temporary size
            bw.WriteEnum(BinBlockID.TPK_DataPart2);
            bw.Write(0xFFFFFFFF);
            var start = bw.BaseStream.Position;

            // Write padding alignment
            for (int loop = 0; loop < 30; ++loop) bw.Write(0x11111111);

            // Precalculate initial capacity for the stream
            int capacity = this.Textures.Count << 16; // count * 0x8000
            int totalTexSize = 0; // to keep track of total texture data length

            // Action delegate to calculate next texture offset
            var CalculateNextOffset = new Action<int>((texlen) =>
            {

                totalTexSize += texlen;
                var dif = 0x80 - totalTexSize % 0x80;
                if (dif != 0x80) totalTexSize += dif;

            });

            // Iterate through every texture. Each iteration creates an OffSlot class 
            // that is yield returned to IEnumerable output. AbsoluteOffset of each 
            // OffSlot initially is offset from the beginning of this TPKBlock in buffer.
            foreach (var texture in this.Textures)
            {

                // Initialize array of texture data and header, write it
                var array = new byte[texture.DataLength + this.CompTexHeaderSize];

                var textureData = texture.Data;

                if (texture.HasPalette)
                {

                    Array.Copy(textureData, texture.PaletteSize, array, 0, texture.Size);
                    Array.Copy(textureData, 0, array, texture.Size, texture.PaletteSize);

                }
                else
                {

                    Array.Copy(textureData, 0, array, 0, textureData.Length);

                }

                using (var strMemory = new MemoryStream(array))
                using (var strWriter = new BinaryWriter(strMemory))
                {

                    strWriter.BaseStream.Position = textureData.Length;
                    this.WriteDownTexture(texture, strWriter, totalTexSize);
                    CalculateNextOffset(textureData.Length);

                }

                // Compress texture data with the best compression
                array = Interop.Compress(array, LZCompressionType.BEST);

                // Create new Offslot that will be yield returned
                var offslot = new OffSlot()
                {
                    Key = texture.BinKey,
                    AbsoluteOffset = (int)(bw.BaseStream.Position - thisOffset),
                    DecodedSize = textureData.Length + this.CompTexHeaderSize,
                    EncodedSize = array.Length,
                    UserFlags = 0,
                    Flags = 1,
                    RefCount = 0,
                    UnknownInt32 = 0,
                };

                // Write compressed data
                bw.Write(array);

                // Fill buffer till offset % 0x40
                bw.FillBuffer(0x40);

                // Yield return OffSlot made
                result.Add(offslot);

            }

            // Finally, fix size at the beginning of the block
            var final = bw.BaseStream.Position;
            bw.BaseStream.Position = start - 4;
            bw.Write((int)(final - start));
            bw.BaseStream.Position = final;

            // Return result list
            return result;
        }

        /// <summary>
        /// Gets list of <see cref="OffSlot"/> by compressing all texture data by parts.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="thisOffset">Offset of this <see cref="TPKBlock"/> in the buffer.</param>
        /// <returns><see cref="List{T}"/> of <see cref="OffSlot"/> from textures written.</returns>
        protected virtual List<OffSlot> GetCompressedByParts(BinaryWriter bw, int thisOffset)
		{
            // Initialize result offslot list
            var result = new List<OffSlot>(this.Textures.Count);

            // Save position and write ID with temporary size
            bw.WriteEnum(BinBlockID.TPK_DataPart2);
            bw.Write(0xFFFFFFFF);
            var start = bw.BaseStream.Position;

            // Write padding alignment
            for (int loop = 0; loop < 30; ++loop) bw.Write(0x11111111);

            // Precalculate initial capacity for the stream
            int capacity = this.Textures.Count << 16; // count * 0x8000
            int totalTexSize = 0; // to keep track of total texture data length

            // Action delegate to calculate next texture offset
            var CalculateNextOffset = new Action<int>((texlen) =>
            {

                totalTexSize += texlen;
                var dif = 0x80 - totalTexSize % 0x80;
                if (dif != 0x80) totalTexSize += dif;

            });

            // Iterate through every texture. Each iteration creates an OffSlot class 
            // that is yield returned to IEnumerable output. AbsoluteOffset of each 
            // OffSlot initially is offset from the beginning of this TPKBlock in buffer.
            foreach (var texture in this.Textures)
            {

                int texOffset = 0; // to keep track of offset in dds data of the texture

                const int headerSize = 0x18; // header size is constant for all compressions
                const int maxBlockSize = 0x8000; // maximum block size of data

                var textureData = texture.Data;

                // If has palette, swap it with actual data
                if (texture.HasPalette)
                {

                    var tempbuf = new byte[texture.PaletteSize];
                    Array.Copy(textureData, tempbuf, texture.PaletteSize);
                    Array.Copy(textureData, texture.PaletteSize, textureData, 0, texture.Size);
                    Array.Copy(tempbuf, 0, textureData, texture.Size, texture.PaletteSize);

                }

                // Calculate header length. Header consists of leftover dds data got by 
                // dividing it in blocks of 0x8000 bytes + size of texture header + 
                // size of dds info header
                var numParts = textureData.Length / maxBlockSize + 1;
                var magiclist = new List<MagicHeader>(numParts);

                for (int loop = 0; loop < numParts; ++loop)
                {

                    var magic = new MagicHeader();

                    // If we are at the leftover/last part
                    if (loop == numParts - 1)
                    {

                        var difference = textureData.Length - texOffset;
                        var head = new byte[difference + this.CompTexHeaderSize];

                        if (difference != 0)
                        {

                            Array.Copy(textureData, texOffset, head, 0, difference);

                        }

                        texOffset = textureData.Length;

                        // Initialize new stream over header and set position at the end
                        using var strMemory = new MemoryStream(head);
                        using var strWriter = new BinaryWriter(strMemory);
                        strWriter.BaseStream.Position = strWriter.BaseStream.Length - this.CompTexHeaderSize;

                        // Write header data and calculate next offset
                        this.WriteDownTexture(texture, strWriter, totalTexSize);
                        CalculateNextOffset(textureData.Length);

                        // Save compressed data to MagicHeader
                        magic.Data = Interop.Compress(head, LZCompressionType.BEST);
                        magic.DecodedSize = difference + this.CompTexHeaderSize;

                    }

                    // Else compress data and save as MagicHeader
                    else
                    {

                        // Use compression type passed
                        magic.Data = Interop.Compress(textureData, texOffset, maxBlockSize, LZCompressionType.BEST);
                        texOffset += maxBlockSize;
                        magic.DecodedSize = maxBlockSize;

                    }

                    magiclist.Add(magic);
                }

                // Create new Offslot that will be yield returned
                var offslot = new OffSlot()
                {
                    Key = texture.BinKey,
                    AbsoluteOffset = (int)(bw.BaseStream.Position - thisOffset),
                    DecodedSize = 0,
                    EncodedSize = 0,
                    UserFlags = 0,
                    Flags = 2,
                    RefCount = 0,
                    UnknownInt32 = 0,
                };

                // Make new offsets to keep track of positions
                //int decodeOffset = 0;
                int decodeOffset = magiclist[0].DecodedSize;
                int encodeOffset = 0;

                // If there are more than 1 subparts
                if (magiclist.Count > 1)
                {

                    // Iterate through every part starting with index 1
                    for (int loop = 1; loop < magiclist.Count; ++loop)
                    {

                        var magic = magiclist[loop];
                        var size = magic.Length + headerSize;
                        var difference = 4 - size % 4;
                        if (difference != 4) size += difference;

                        // Manage settings about decoded data
                        magic.DecodedDataPosition = decodeOffset;
                        decodeOffset += magic.DecodedSize;
                        offslot.DecodedSize += magic.DecodedSize;

                        // Manage settings about encoded data
                        magic.EncodedSize = size;
                        magic.EncodedDataPosition = encodeOffset;
                        encodeOffset += size;
                        offslot.EncodedSize += size;

                        magic.Write(bw);

                    }

                }

                // Write very first subpart at the end
                {

                    var magic = magiclist[0];
                    var size = magic.Length + headerSize;
                    var difference = 4 - size % 4;
                    if (difference != 4) size += difference;

                    // Manage settings about decoded data
                    offslot.DecodedSize += magic.DecodedSize;

                    // Manage settings about encoded data
                    magic.EncodedSize = size;
                    magic.EncodedDataPosition = encodeOffset;
                    offslot.EncodedSize += size;

                    magic.Write(bw);

                }

                // Fill buffer till offset % 0x40
                bw.FillBuffer(0x40);

                // Yield return OffSlot made
                result.Add(offslot);

            }

            // Finally, fix size at the beginning of the block
            var final = bw.BaseStream.Position;
            bw.BaseStream.Position = start - 4;
            bw.Write((int)(final - start));
            bw.BaseStream.Position = final;

            // Return result list
            return result;
        }

        /// <summary>
        /// Writes down a <see cref="Texture"/> header and compression slot.
        /// </summary>
        /// <param name="texture"><see cref="Texture"/> to write down.</param>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="totalTexSize">Total texture size written to a buffer.</param>
        protected abstract void WriteDownTexture(Texture texture, BinaryWriter bw, int totalTexSize);

        #endregion
    }
}