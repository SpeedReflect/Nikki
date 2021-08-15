using System;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Attributes;
using Nikki.Support.MostWanted.Framework;
using Nikki.Support.Shared.Parts.TPKParts;
using CoreExtensions.IO;
using CoreExtensions.Management;



namespace Nikki.Support.MostWanted.Class
{
	/// <summary>
	/// <see cref="TPKBlock"/> is a collection of <see cref="Texture"/>.
	/// </summary>
	public class TPKBlock : Shared.Class.TPKBlock
    {
        #region Fields

        private string _collection_name;
        private List<AnimSlot> _animations;
        private List<TexturePage> _texturePages;
        private List<Shared.Class.Texture> _textures;
        private const long max = 0x7FFFFFFF;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override GameINT GameINT => GameINT.MostWanted;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.MostWanted.ToString();

        /// <summary>
        /// Manager to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public TPKBlockManager Manager { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
        [Category("Main")]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                this.Manager?.CreationCheck(value);
                this._collection_name = value;
            }
        }

		/// <summary>
		/// Version of this <see cref="TPKBlock"/>.
		/// </summary>
		[Browsable(false)]
        public override TPKVersion Version => TPKVersion.MostWanted;

		/// <summary>
		/// Filename used for this <see cref="TPKBlock"/>. It is a default watermark.
		/// </summary>
		[Browsable(false)]
        public override string Filename => $"{this.CollectionName}.tpk";

        /// <summary>
        /// BinKey of the filename.
        /// </summary>
        [Browsable(false)]
        public override uint FilenameHash => this.Filename.BinHash();

		/// <summary>
		/// Represents all <see cref="AnimSlot"/> of this <see cref="TPKBlock"/>.
		/// </summary>
		[Category("Primary")]
        public override List<AnimSlot> Animations => this._animations;

        /// <summary>
        /// Represents all <see cref="TexturePage"/> of this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public override List<TexturePage> TexturePages => this._texturePages;

        /// <summary>
        /// List of <see cref="Texture"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        [Browsable(false)]
        public override List<Shared.Class.Texture> Textures => this._textures;

		/// <summary>
		/// Number of <see cref="Texture"/> in this <see cref="TPKBlock"/>.
		/// </summary>
		[Category("Primary")]
        public override int TextureCount => this.Textures.Count;

        /// <summary>
        /// Indicates size of compressed texture header and compression block struct.
        /// </summary>
        protected override int CompTexHeaderSize => 0x9C;

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="TPKBlock"/>.
		/// </summary>
		public TPKBlock()
		{
            this._animations = new List<AnimSlot>();
            this._texturePages = new List<TexturePage>();
            this._textures = new List<Shared.Class.Texture>();
		}

		/// <summary>
		/// Initializes new instance of <see cref="TPKBlock"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="TPKBlockManager"/> to which this instance belongs to.</param>
		public TPKBlock(string CName, TPKBlockManager manager) : this()
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

		/// <summary>
		/// Initializes new instance of <see cref="TPKBlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="TPKBlockManager"/> to which this instance belongs to.</param>
		public TPKBlock(BinaryReader br, TPKBlockManager manager) : this()
        {
            this.Manager = manager;
            var id = br.ReadEnum<BinBlockID>();

            if (id == BinBlockID.CompTPKBlock)
            {

                var size = br.ReadInt32();
                var array = br.ReadBytes(size);
                array = Interop.Decompress(array);

                using var ms = new MemoryStream(array);
                using var reader = new BinaryReader(ms);

                this.Disassemble(reader);
                this.CompressionType = TPKCompressionType.CompressedMiniMap;

            }
            else
            {

                br.BaseStream.Position -= 4;
                this.Disassemble(br);

            }
        }

		#endregion

		#region Internal Methods

		/// <summary>
		/// Assembles <see cref="TPKBlock"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="TPKBlock"/> with.</param>
		/// <returns>Byte array of the tpk block.</returns>
		public override void Assemble(BinaryWriter bw)
        {
            // TPK Sort
            this.SortTexturesByType(false);

            if (this.CompressionType == TPKCompressionType.CompressedMiniMap)
            {

                using var ms = new MemoryStream(20000);
                using var writer = new BinaryWriter(ms);
                this.AssembleDecompressed(writer);
                var array = ms.ToArray();
                array = Interop.Compress(array, LZCompressionType.BEST);
                int size = array.Length;
                var dif = 4 - (array.Length % 4);
                if (dif != 4) size += dif;
                
                bw.WriteEnum(BinBlockID.CompTPKBlock);
                bw.Write(size);
                bw.Write(array);
                bw.FillBuffer(4);

            }
            else if (this.CompressionType == TPKCompressionType.RawDecompressed)
            {

                this.AssembleDecompressed(bw);

            }
            else
            {

                this.AssembleCompressed(bw);

            }

            ForcedX.GCCollect();
        }

        private void AssembleDecompressed(BinaryWriter bw)
        {
            // Write main
            bw.WriteEnum(BinBlockID.TPKBlocks);
            bw.Write(-1); // write temp size
            var position_0 = bw.BaseStream.Position;
            bw.Write((int)0);
            bw.Write(0x30);
            bw.WriteBytes(0, 0x30);

            // Partial 1 Block
            bw.WriteEnum(BinBlockID.TPK_InfoBlock);
            bw.Write(-1);
            var position_1 = bw.BaseStream.Position;
            this.Get1Part1(bw);
            this.Get1Part2(bw);
            this.Get1Part4(bw);
            this.Get1Part5(bw);
            this.Get1PartAnim(bw);
            bw.BaseStream.Position = position_1 - 4;
            bw.Write((int)(bw.BaseStream.Length - position_1));
            bw.BaseStream.Position = bw.BaseStream.Length;

            // Write padding
            bw.Write(Comp.GetPaddingArray((int)bw.BaseStream.Position, 0x80));

            // Partial 2 Block
            bw.WriteEnum(BinBlockID.TPK_DataBlock);
            bw.Write(-1);
            var position_2 = bw.BaseStream.Position;
            this.Get2Part1(bw);
            this.Get2DecodedPart2(bw);
            bw.BaseStream.Position = position_2 - 4;
            bw.Write((int)(bw.BaseStream.Length - position_2));

            // Write final size
            bw.BaseStream.Position = position_0 - 4;
            bw.Write((int)(bw.BaseStream.Length - position_0));
            bw.BaseStream.Position = bw.BaseStream.Length;
        }

        private void AssembleCompressed(BinaryWriter bw)
        {
            var start = (int)bw.BaseStream.Position;

            bw.WriteEnum(BinBlockID.TPKBlocks);
            bw.Write(-1); // write temp size
            var position_0 = bw.BaseStream.Position;
            bw.Write((int)0);
            bw.Write(0x30);
            bw.WriteBytes(0, 0x30);

            // Partial 1 Block
            bw.WriteEnum(BinBlockID.TPK_InfoBlock);
            bw.Write(-1);
            var position_1 = bw.BaseStream.Position;
            this.Get1Part1(bw);
            this.Get1Part2(bw);

            // Write temporary Part3
            var position_3 = bw.BaseStream.Position;
            bw.Write((long)0);

            for (int a1 = 0; a1 < this.Textures.Count; ++a1) bw.WriteBytes(0, 0x18);

            // Write partial 1 size
            bw.BaseStream.Position = position_1 - 4;
            bw.Write((int)(bw.BaseStream.Length - position_1));
            bw.BaseStream.Position = bw.BaseStream.Length;

            // Write padding
            bw.Write(Comp.GetPaddingArray((int)bw.BaseStream.Position, 0x80));

            // Partial 2 Block
            bw.WriteEnum(BinBlockID.TPK_DataBlock);
            bw.Write(-1);
            var position_2 = bw.BaseStream.Position;
            this.Get2Part1(bw);
            var offslots = this.Get2EncodedPart2(bw, start);
            bw.BaseStream.Position = position_2 - 4;
            bw.Write((int)(bw.BaseStream.Length - position_2));

            // Write offslots
            bw.BaseStream.Position = position_3;
            this.Get1Part3(bw, offslots);

            // Write final size
            bw.BaseStream.Position = position_0 - 4;
            bw.Write((int)(bw.BaseStream.Length - position_0));
            bw.BaseStream.Position = bw.BaseStream.Length;
        }

        /// <summary>
        /// Disassembles tpk block array into separate properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Disassemble(BinaryReader br)
        {
            var Start = br.BaseStream.Position;
            var ID = br.ReadEnum<BinBlockID>();
            int size = br.ReadInt32();
            var Final = br.BaseStream.Position + size;

            var PartOffsets = this.FindOffsets(br);

            // Get texture count
            br.BaseStream.Position = PartOffsets[1];
            var TextureCount = this.GetTextureCount(br);

            // Get header info
            br.BaseStream.Position = PartOffsets[0];
            this.GetHeaderInfo(br);

            // Get texture header info
            br.BaseStream.Position = PartOffsets[3];
            var texture_list = this.GetTextureHeaders(br, TextureCount);

            // Get CompSlot info
            br.BaseStream.Position = PartOffsets[4];
            var compslot_list = this.GetCompressionList(br).ToList();

            // Get Offslot info
            br.BaseStream.Position = PartOffsets[2];
            var offslot_list = this.GetOffsetSlots(br).ToList();

            if (PartOffsets[2] != max)
            {

                br.BaseStream.Position = Start;
                this.ParseCompTextures(br, offslot_list);
            
            }
            else
            {
            
                // Add textures to the list
                for (int a1 = 0; a1 < TextureCount; ++a1)
                {
                
                    br.BaseStream.Position = texture_list[a1];

                    var tex = new Texture(br, this)
                    {
                        CompressionValue1 = compslot_list[a1].Var1,
                        CompressionValue2 = compslot_list[a1].Var2,
                        CompressionValue3 = compslot_list[a1].Var3
                    };
                    
                    this.Textures.Add(tex);
                
                }

                // Finally, build all .dds files
                for (int a1 = 0; a1 < TextureCount; ++a1)
                {
                
                    br.BaseStream.Position = PartOffsets[6] + 0x7C;
                    this.Textures[a1].ReadData(br, false);
                
                }
            
            }

            if (PartOffsets[8] != max)
			{

                br.BaseStream.Position = PartOffsets[8];
                this.GetAnimations(br);

			}

            br.BaseStream.Position = Final;
        }

		/// <summary>
		/// Adds <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
		/// </summary>
		/// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
		/// <param name="filename">Path of the texture to be imported.</param>
		public override void AddTexture(string CName, string filename)
        {
            if (string.IsNullOrWhiteSpace(CName))
            {

                throw new ArgumentNullException($"Collection Name cannot be empty or whitespace");

            }

            if (this.FindTexture(CName.BinHash(), KeyType.BINKEY) != null)
            {

                throw new Exception($"Texture named {CName} already exists");

            }

            if (!Comp.IsDDSTexture(filename, out string error))
            {

                throw new ArgumentException(error);

            }

            var texture = new Texture(CName, filename, this);
            this.Textures.Add(texture);
        }

		/// <summary>
		/// Clones <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
		/// </summary>
		/// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
		/// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
		/// <param name="type">Type of the key passed.</param>
		public override void CloneTexture(string newname, uint key, KeyType type)
        {
            if (string.IsNullOrWhiteSpace(newname))
            {

                throw new ArgumentNullException($"Collection Name cannot be empty or whitespace");

            }

            if (this.FindTexture(newname.BinHash(), type) != null)
            {

                throw new Exception($"Texture named {newname} already exists");

            }

            var copyfrom = (Texture)this.FindTexture(key, type);

            if (copyfrom == null)
            {

                throw new Exception($"Texture with key 0x{key:X8} does not exist");

            }

            var texture = (Texture)copyfrom.MemoryCast(newname);
            this.Textures.Add(texture);
        }

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="TPKBlock"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
        {
            return $"Collection Name: {this.CollectionName} | " +
                   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
        }

		#endregion

		#region Reading Methods

		/// <summary>
		/// Finds offsets of all partials and its parts in the <see cref="TPKBlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
		/// <returns>Array of all offsets.</returns>
		protected override long[] FindOffsets(BinaryReader br)
        {
            var offsets = new long[9] { max, max, max, max, max, max, max, max, max };
            var ReaderID = BinBlockID.Padding;
            int InfoBlockSize = 0;
            int DataBlockSize = 0;
            long ReaderOffset = 0;

            while (ReaderID != BinBlockID.TPK_InfoBlock)
            {
            
                ReaderID = br.ReadEnum<BinBlockID>();
                InfoBlockSize = br.ReadInt32();
                
                if (ReaderID != BinBlockID.TPK_InfoBlock)
                {
                
                    br.BaseStream.Position += InfoBlockSize;
                
                }
            
            }

            ReaderOffset = br.BaseStream.Position;
            
            while (br.BaseStream.Position < ReaderOffset + InfoBlockSize)
            {
            
                ReaderID = br.ReadEnum<BinBlockID>();
                
                switch (ReaderID)
                {
                    case BinBlockID.TPK_InfoPart1:
                        offsets[0] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.TPK_InfoPart2:
                        offsets[1] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.TPK_InfoPart3:
                        offsets[2] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.TPK_InfoPart4:
                        offsets[3] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.TPK_InfoPart5:
                        offsets[4] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.TPK_BinData:
                        offsets[8] = br.BaseStream.Position;
                        goto default;

                    default:
                        int size = br.ReadInt32();
                        br.BaseStream.Position += size;
                        break;
                
                }
            
            }

            while (ReaderID != BinBlockID.TPK_DataBlock)
            {
                
                ReaderID = br.ReadEnum<BinBlockID>();
                DataBlockSize = br.ReadInt32();

                if (ReaderID != BinBlockID.TPK_DataBlock)
                {

                    br.BaseStream.Position += DataBlockSize;

                }

            }

            ReaderOffset = br.BaseStream.Position; // relative offset
            
            while (br.BaseStream.Position < ReaderOffset + DataBlockSize)
            {
            
                ReaderID = br.ReadEnum<BinBlockID>();
                
                switch (ReaderID)
                {
                    case BinBlockID.TPK_DataPart1:
                        offsets[5] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.TPK_DataPart2:
                        offsets[6] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.TPK_DataPart3:
                        offsets[7] = br.BaseStream.Position;
                        goto default;

                    default:
                        int size = br.ReadInt32();
                        br.BaseStream.Position += size;
                        break;
                
                }
            
            }

            return offsets;
        }

		/// <summary>
		/// Gets amount of textures in the <see cref="TPKBlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
		/// <returns>Number of textures in the tpk block.</returns>
		protected override int GetTextureCount(BinaryReader br)
        {
            if (br.BaseStream.Position == max) return 0; // check if Part2 even exists
            return br.ReadInt32() / 8; // 8 bytes for one texture
        }

		/// <summary>
		/// Gets <see cref="TPKBlock"/> header information.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
		protected override void GetHeaderInfo(BinaryReader br)
        {
            if (br.BaseStream.Position == max) return; // check if Part1 even exists

            if (br.ReadInt32() != 0x7C) return; // check header size

            // Check TPK version
            br.BaseStream.Position += 4; // assuming user knows what he/she is doing

            // Get CollectionName
            var cname = br.ReadNullTermUTF8(0x1C);
            var fname = br.ReadNullTermUTF8(0x40);

            if (fname.EndsWith(".tpk") || fname.EndsWith(".TPK"))
            {

                fname = Path.GetFileNameWithoutExtension(fname).ToUpper();

                if (this.Manager is null)
                {

                    this._collection_name = fname;

                }
                else
                {

                    this._collection_name = !this.Manager.Contains(fname)
                        ? fname : !this.Manager.Contains(cname)
                        ? cname : $"TPK{this.Manager.Count}";
                }

            }
            else
            {

                if (this.Manager is null)
                {

                    this._collection_name = cname;

                }
                else
                {
                    this._collection_name = !this.Manager.Contains(cname)
                        ? cname :
                        $"TPK{this.Manager.Count}";
                }

            }

            // Get the rest of the settings
            br.BaseStream.Position += 0x1C;
        }

		/// <summary>
		/// Gets list of offset slots of the textures in the <see cref="TPKBlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
		protected override IEnumerable<OffSlot> GetOffsetSlots(BinaryReader br)
        {
            if (br.BaseStream.Position == max) yield break;  // if Part3 does not exist

            int ReaderSize = br.ReadInt32();
            var ReaderOffset = br.BaseStream.Position;
            
            while (br.BaseStream.Position < ReaderOffset + ReaderSize)
            {
            
                yield return new OffSlot
                {
                    Key = br.ReadUInt32(),
                    AbsoluteOffset = br.ReadInt32(),
                    EncodedSize = br.ReadInt32(),
                    DecodedSize = br.ReadInt32(),
                    UserFlags = br.ReadByte(),
                    Flags = br.ReadByte(),
                    RefCount = br.ReadInt16(),
                    UnknownInt32 = br.ReadInt32()
                };
            
            }
        }

		/// <summary>
		/// Gets list of offsets and sizes of the texture headers in the <see cref="TPKBlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
		/// <param name="count">Number of textures to read.</param>
		/// <returns>Array of offsets and sizes of texture headers.</returns>
		protected override long[] GetTextureHeaders(BinaryReader br, int count)
        {
            if (br.BaseStream.Position == max) return null;  // if Part4 does not exist

            int ReaderSize = br.ReadInt32();
            var ReaderOffset = br.BaseStream.Position;
            var result = new long[count];

            int len = 0;
            
            while (len < count && br.BaseStream.Position < ReaderOffset + ReaderSize)
            {
            
                result[len++] = br.BaseStream.Position; // add offset
                br.BaseStream.Position += 0x7C;
            
            }

            return result;
        }

		/// <summary>
		/// Gets list of compressions of the textures in the tpk block array.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
		protected override IEnumerable<CompSlot> GetCompressionList(BinaryReader br)
        {
            if (br.BaseStream.Position == max) yield break;  // if Part5 does not exist

            int ReaderSize = br.ReadInt32();
            var ReaderOffset = br.BaseStream.Position;
            
            while (br.BaseStream.Position < ReaderOffset + ReaderSize)
            {
            
                br.BaseStream.Position += 8;
                
                yield return new CompSlot
                {
                    Var1 = br.ReadInt32(),
                    Var2 = br.ReadInt32(),
                    Var3 = br.ReadInt32(),
                    Comp = br.ReadUInt32(),
                };
                
                br.BaseStream.Position += 8;
            
            }
        }

		/// <summary>
		/// Creates new texture header and reads compression data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <returns>A <see cref="Texture"/> got from read data.</returns>
		protected override Shared.Class.Texture CreateNewTexture(BinaryReader br)
		{
            var texture = new Texture(br, this);
            br.BaseStream.Position += 0x8;
            texture.CompressionValue1 = br.ReadInt32();
            texture.CompressionValue2 = br.ReadInt32();
            texture.CompressionValue3 = br.ReadInt32();
            br.BaseStream.Position += 0xC;
            return texture;
		}

        #endregion

        #region Writing Methods

        /// <summary>
        /// Assembles partial 1 part1 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part1.</returns>
        protected override void Get1Part1(BinaryWriter bw)
        {
            bw.WriteEnum(BinBlockID.TPK_InfoPart1); // write ID
            bw.Write(0x7C); // write size
            bw.WriteEnum(this.Version);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x1C);

            // Write Filename
            bw.WriteNullTermUTF8(this.Filename, 0x40);

            // Write all other settings
            bw.Write(this.FilenameHash);
            bw.WriteBytes(0, 0x18);
        }

        /// <summary>
        /// Assembles partial 1 part2 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part2.</returns>
        protected override void Get1Part2(BinaryWriter bw)
        {
            bw.WriteEnum(BinBlockID.TPK_InfoPart2); // write ID
            bw.Write(this.Textures.Count * 8); // write size
            
            for (int loop = 0; loop < this.Textures.Count; ++loop)
            {
            
                bw.Write(this.Textures[loop].BinKey);
                bw.Write((int)0);
            
            }
        }

        /// <summary>
        /// Assembles partial 1 part3 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="offslots">List of <see cref="OffSlot"/> to write.</param>
        protected void Get1Part3(BinaryWriter bw, List<OffSlot> offslots)
        {
            bw.WriteEnum(BinBlockID.TPK_InfoPart3); // write ID
            bw.Write(this.Textures.Count * 0x18); // write size

            foreach (var offslot in offslots)
            {

                bw.Write(offslot.Key);
                bw.Write(offslot.AbsoluteOffset);
                bw.Write(offslot.EncodedSize);
                bw.Write(offslot.DecodedSize);
                bw.Write(offslot.UserFlags);
                bw.Write(offslot.Flags);
                bw.Write(offslot.RefCount);
                bw.Write(offslot.UnknownInt32);

            }
        }

        /// <summary>
        /// Assembles partial 1 part4 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part4.</returns>
        protected override void Get1Part4(BinaryWriter bw)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            int length = 0;
            
            foreach (var tex in this.Textures)
            {
            
                tex.PaletteOffset = length;
                tex.Offset = length + tex.PaletteSize;
                tex.Assemble(writer);
                length += tex.PaletteSize + tex.Size;
                var pad = 0x80 - length % 0x80;
                if (pad != 0x80) length += pad;
            
            }

            var data = ms.ToArray();
            bw.WriteEnum(BinBlockID.TPK_InfoPart4); // write ID
            bw.Write(data.Length); // write size
            bw.Write(data);
        }

        /// <summary>
        /// Assembles partial 1 part5 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part5.</returns>
        protected override void Get1Part5(BinaryWriter bw)
        {
            bw.WriteEnum(BinBlockID.TPK_InfoPart5); // write ID
            bw.Write(this.Textures.Count * 0x20); // write size
            
            for (int loop = 0; loop < this.Textures.Count; ++loop)
            {

                var texture = this.Textures[loop] as Texture;
                bw.Write((long)0);
                bw.Write(texture.CompressionValue1);
                bw.Write(texture.CompressionValue2);
                bw.Write(texture.CompressionValue3);
                bw.Write(Comp.GetInt(this.Textures[loop].Compression));
                bw.Write((long)0);
            
            }
        }

        /// <summary>
        /// Assembles partial 2 part1 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 2 part1.</returns>
        protected override void Get2Part1(BinaryWriter bw)
        {
            bw.WriteEnum(BinBlockID.TPK_DataPart1); // write ID
            bw.Write(0x18); // write size
            bw.Write((long)0);
            bw.Write(1);
            bw.Write(this.FilenameHash);
            bw.Write((long)0);
            bw.Write(0);
            bw.Write(0x50);
            bw.WriteNullTermUTF8(this.Watermark, 0x20);
            bw.WriteBytes(0, 0x30);
        }

        /// <summary>
        /// Writes down a <see cref="Texture"/> header and compression slot.
        /// </summary>
        /// <param name="texture"><see cref="Texture"/> to write down.</param>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="totalTexSize">Total texture size written to a buffer.</param>
        protected override void WriteDownTexture(Shared.Class.Texture texture, BinaryWriter bw, int totalTexSize)
		{
            var tex = (Texture)texture;
            texture.Offset = totalTexSize;
            texture.PaletteOffset = totalTexSize + texture.Size;
            var nextPos = bw.BaseStream.Position + 0x7C;
            texture.Assemble(bw);
            bw.BaseStream.Position = nextPos;
            bw.Write((long)0);
            bw.Write(tex.CompressionValue1);
            bw.Write(tex.CompressionValue2);
            bw.Write(tex.CompressionValue3);
            bw.Write(Comp.GetInt(texture.Compression));
            bw.Write((long)0);
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public override void Serialize(BinaryWriter bw)
        {
            byte[] array;

            var start = bw.BaseStream.Position;
            bw.WriteBytes(0, SerializationHeader.ThisSize + 4);

            using (var ms = new MemoryStream(0x8000))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);
                writer.WriteEnum(this.CompressionType);
                writer.Write(this.Animations.Count);
                writer.Write(this.TexturePages.Count);
                writer.Write(this.Textures.Count);

                for (int loop = 0; loop < this.Animations.Count; ++loop)
                {

                    var anim = this.Animations[loop];
                    writer.WriteNullTermUTF8(anim.Name);
                    writer.Write(anim.BinKey);
                    writer.Write(anim.FramesPerSecond);
                    writer.Write(anim.TimeBase);
                    writer.Write((byte)anim.FrameTextures.Count);

                    for (int i = 0; i < anim.FrameTextures.Count; ++i)
                    {

                        writer.WriteNullTermUTF8(anim.FrameTextures[i].Name);

                    }

                }

                for (int loop = 0; loop < this.TexturePages.Count; ++loop)
                {

                    var page = this.TexturePages[loop];
                    writer.WriteNullTermUTF8(page.TextureName);
                    writer.Write(page.U0);
                    writer.Write(page.V0);
                    writer.Write(page.U1);
                    writer.Write(page.V1);
                    writer.Write(page.Flags);

                }

                writer.WriteBytes(0, 0x40);

                array = ms.ToArray();
                array = Interop.Compress(array, LZCompressionType.RAWW);
                bw.Write(array.Length);
                bw.Write(array);

            }

            for (int loop = 0; loop < this.Textures.Count; ++loop)
            {

                this.Textures[loop].Serialize(bw);

            }

            var end = bw.BaseStream.Position;
            bw.BaseStream.Position = start;
            var size = (int)(end - start) - SerializationHeader.ThisSize - 4;
            var header = new SerializationHeader(size, this.GameINT, this.Manager.Name);
            header.Write(bw);
            bw.Write(size);
            bw.BaseStream.Position = end;
        }

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Deserialize(BinaryReader br)
        {
            br.BaseStream.Position += 4;
            int size = br.ReadInt32();
            var array = br.ReadBytes(size);

            array = Interop.Decompress(array);

            using var ms = new MemoryStream(array);
            using var reader = new BinaryReader(ms);

            this._collection_name = reader.ReadNullTermUTF8();
            this.CompressionType = reader.ReadEnum<TPKCompressionType>();
            int animcount = reader.ReadInt32();
            int pageCount = reader.ReadInt32();
            int textcount = reader.ReadInt32();
            this.Animations.Capacity = animcount;
            this.TexturePages.Capacity = pageCount;
            this.Textures.Capacity = textcount;

            for (int loop = 0; loop < animcount; ++loop)
            {

                var anim = new AnimSlot()
                {
                    Name = reader.ReadNullTermUTF8(),
                    BinKey = reader.ReadUInt32(),
                    FramesPerSecond = reader.ReadByte(),
                    TimeBase = reader.ReadByte(),
                };

                var count = reader.ReadByte();
                anim.FrameTextures.Capacity = count;

                for (int i = 0; i < count; ++i)
                {

                    var frame = new FrameEntry()
                    {
                        Name = reader.ReadNullTermUTF8()
                    };

                    anim.FrameTextures.Add(frame);

                }

                this.Animations.Add(anim);

            }

            for (int loop = 0; loop < pageCount; ++loop)
            {

                var page = new TexturePage()
                {
                    TextureName = reader.ReadNullTermUTF8(),
                    U0 = reader.ReadSingle(),
                    V0 = reader.ReadSingle(),
                    U1 = reader.ReadSingle(),
                    V1 = reader.ReadSingle(),
                    Flags = reader.ReadUInt32(),
                };

                this.TexturePages.Add(page);

            }

            for (int loop = 0; loop < textcount; ++loop)
            {

                var texture = new Texture()
                {
                    TPK = this
                };

                var header = new SerializationHeader();
                header.Read(br);

                // Check for consistency
                if (header.ID != BinBlockID.Nikki) break;
                if (header.Game != this.GameINT) break;
                if (header.Name != "TEXTURE") break;

                texture.Deserialize(br);
                this.Textures.Add(texture);

            }
        }

        /// <summary>
        /// Synchronizes all parts of this instance with another instance passed.
        /// </summary>
        /// <param name="other"><see cref="TPKBlock"/> to synchronize with.</param>
        internal void Synchronize(TPKBlock other)
        {
            var animations = new List<AnimSlot>(other.Animations);
            var texturePages = new List<TexturePage>(other.TexturePages);
            var textures = new List<Shared.Class.Texture>(other.Textures);

            // Synchronize animations
            for (int i = 0; i < this.Animations.Count; ++i)
            {

                bool found = false;

                for (int j = 0; j < other.Animations.Count; ++j)
                {

                    if (other.Animations[j].BinKey == this.Animations[i].BinKey)
                    {

                        found = true;
                        break;

                    }

                }

                if (!found) animations.Add(this.Animations[i]);

            }

            // Synchronize texture pages
            for (int i = 0; i < this.TexturePages.Count; ++i)
            {

                bool found = false;

                for (int j = 0; j < other.TexturePages.Count; ++j)
                {

                    if (other.TexturePages[j].TextureKey == this.TexturePages[i].TextureKey)
                    {

                        found = true;
                        break;

                    }

                }

                if (!found) texturePages.Add(this.TexturePages[i]);

            }

            // Synchronize textures
            for (int i = 0; i < this.Textures.Count; ++i)
            {

                bool found = false;

                for (int j = 0; j < other.Textures.Count; ++j)
                {

                    if (other.Textures[j].BinKey == this.Textures[i].BinKey)
                    {

                        found = true;
                        break;

                    }

                }

                if (!found) textures.Add(this.Textures[i]);

            }

            this._animations = animations;
            this._texturePages = texturePages;
            this._textures = textures;
            this.CompressionType = other.CompressionType;
        }

        #endregion
    }
}