using System;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
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
        public override eTPKVersion Version => eTPKVersion.MostWanted;

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
        /// If true, indicates that this <see cref="TPKBlock"/> is compressed and 
        /// should be saved as compressed on the output.
        /// </summary>
        [AccessModifiable()]
        [Category("Primary")]
        public override eBoolean IsCompressed { get; set; }

        /// <summary>
        /// List of <see cref="Texture"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        [Browsable(false)]
        public List<Texture> Textures { get; }

        /// <summary>
        /// Number of <see cref="Texture"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        [Category("Primary")]
        public override int TextureCount => this.Textures.Count;

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="TPKBlock"/>.
        /// </summary>
        public TPKBlock() => this.Textures = new List<Texture>();

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
            this.Disassemble(br);
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

            if (this.IsCompressed == eBoolean.True) this.AssembleCompressed(bw);
            else this.AssembleDecompressed(bw);

            ForcedX.GCCollect();
        }

        private void AssembleDecompressed(BinaryWriter bw)
        {
            // Write main
            bw.WriteEnum(eBlockID.TPKBlocks);
            bw.Write(-1); // write temp size
            var position_0 = bw.BaseStream.Position;
            bw.Write((int)0);
            bw.Write(0x30);
            bw.WriteBytes(0x30);

            // Partial 1 Block
            bw.WriteEnum(eBlockID.TPK_InfoBlock);
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
            bw.WriteEnum(eBlockID.TPK_DataBlock);
            bw.Write(-1);
            var position_2 = bw.BaseStream.Position;
            this.Get2Part1(bw);
            this.Get2Part2(bw);
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

            bw.WriteEnum(eBlockID.TPKBlocks);
            bw.Write(-1); // write temp size
            var position_0 = bw.BaseStream.Position;
            bw.Write((int)0);
            bw.Write(0x30);
            bw.WriteBytes(0x30);

            // Partial 1 Block
            bw.WriteEnum(eBlockID.TPK_InfoBlock);
            bw.Write(-1);
            var position_1 = bw.BaseStream.Position;
            this.Get1Part1(bw);
            this.Get1Part2(bw);

            // Write temporary Part3
            var position_3 = bw.BaseStream.Position;
            bw.Write((long)0);

            for (int a1 = 0; a1 < this.Textures.Count; ++a1)
            {

                bw.WriteBytes(0x18);

            }

            // Write partial 1 size
            bw.BaseStream.Position = position_1 - 4;
            bw.Write((int)(bw.BaseStream.Length - position_1));
            bw.BaseStream.Position = bw.BaseStream.Length;

            // Write padding
            bw.Write(Comp.GetPaddingArray((int)bw.BaseStream.Position, 0x80));

            // Partial 2 Block
            bw.WriteEnum(eBlockID.TPK_DataBlock);
            bw.Write(-1);
            var position_2 = bw.BaseStream.Position;
            this.Get2Part1(bw);
            var offslots = this.Get2CompressedPart2(bw, start);
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
            uint ID = br.ReadUInt32();
            int size = br.ReadInt32();
            var Final = br.BaseStream.Position + size;

            var PartOffsets = this.FindOffsets(br);

            // Get texture count
            br.BaseStream.Position = PartOffsets[1];
            var TextureCount = this.GetTextureCount(br);
            if (TextureCount == 0) return; // if no textures allocated

            // Get header info
            br.BaseStream.Position = PartOffsets[0];
            this.GetHeaderInfo(br);

            // Get Offslot info
            br.BaseStream.Position = PartOffsets[2];
            var offslot_list = this.GetOffsetSlots(br).ToList();

            // Get texture header info
            br.BaseStream.Position = PartOffsets[3];
            var texture_list = this.GetTextureHeaders(br, TextureCount);

            // Get CompSlot info
            br.BaseStream.Position = PartOffsets[4];
            var compslot_list = this.GetCompressionList(br).ToList();

            if (PartOffsets[2] != max)
            {

                this.IsCompressed = eBoolean.True;

                for (int a1 = 0; a1 < TextureCount; ++a1)
                {
                
                    int count = this.Textures.Count;
                    br.BaseStream.Position = Start;
                    this.ParseCompTexture(br, offslot_list[a1]);
                
                }
            
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
        /// Tries to find <see cref="Texture"/> based on the key passed.
        /// </summary>
        /// <param name="key">Key of the <see cref="Texture"/> Collection Name.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <returns>Texture if it is found; null otherwise.</returns>
        public override Shared.Class.Texture FindTexture(uint key, eKeyType type) =>
            type switch
            {
                eKeyType.BINKEY => this.Textures.Find(_ => _.BinKey == key),
                eKeyType.VLTKEY => this.Textures.Find(_ => _.VltKey == key),
                eKeyType.CUSTOM => throw new NotImplementedException(),
                _ => null
            };

        /// <summary>
        /// Sorts <see cref="Texture"/> by their CollectionNames or BinKeys.
        /// </summary>
        /// <param name="by_name">True if sort by name; false is sort by hash.</param>
        public override void SortTexturesByType(bool by_name)
        {
            if (!by_name) this.Textures.Sort((x, y) => x.BinKey.CompareTo(y.BinKey));
            else this.Textures.Sort((x, y) => x.CollectionName.CompareTo(y.CollectionName));
        }

        /// <summary>
        /// Gets all textures of this <see cref="TPKBlock"/>.
        /// </summary>
        /// <returns>Textures as an object.</returns>
        public override object GetTextures() => this.Textures;

        /// <summary>
        /// Gets index of the <see cref="Texture"/> in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/>.</param>
        /// <param name="type">Key type passed.</param>
        /// <returns>Index number as an integer. If element does not exist, returns -1.</returns>
        public override int GetTextureIndex(uint key, eKeyType type)
        {
            switch (type)
            {
                case eKeyType.BINKEY:
                    for (int loop = 0; loop < this.Textures.Count; ++loop)
                    {
 
                        if (this.Textures[loop].BinKey == key) return loop;
                    
                    }
                    break;

                case eKeyType.VLTKEY:
                    for (int loop = 0; loop < this.Textures.Count; ++loop)
                    {
                    
                        if (this.Textures[loop].VltKey == key) return loop;
                    
                    }
                    break;

                case eKeyType.CUSTOM:
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
        public override void AddTexture(string CName, string filename)
        {
            if (string.IsNullOrWhiteSpace(CName))
            {

                throw new ArgumentNullException($"Collection Name cannot be empty or whitespace");

            }

            if (this.FindTexture(CName.BinHash(), eKeyType.BINKEY) != null)
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
        /// Removes <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type fo the key passed.</param>
        public override void RemoveTexture(uint key, eKeyType type)
        {
            var index = this.GetTextureIndex(key, type);

            if (index == -1)
            {

                throw new InfoAccessException($"0x{key:X8}");

            }

            this.Textures.RemoveAt(index);
        }

        /// <summary>
        /// Clones <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        public override void CloneTexture(string newname, uint key, eKeyType type)
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

                throw new InfoAccessException($"0x{key:X8}");

            }

            var texture = (Texture)copyfrom.MemoryCast(newname);
            this.Textures.Add(texture);
        }

        /// <summary>
        /// Replaces <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        public override void ReplaceTexture(uint key, eKeyType type, string filename)
        {
            var tex = (Texture)this.FindTexture(key, type);

            if (tex == null)
            {

                throw new InfoAccessException($"0x{key:X8}");

            }

            if (!Comp.IsDDSTexture(filename, out string error))
            {

                throw new ArgumentException(error);

            }

            tex.Reload(filename);
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
            var ReaderID = eBlockID.Padding;
            int InfoBlockSize = 0;
            int DataBlockSize = 0;
            long ReaderOffset = 0;

            while (ReaderID != eBlockID.TPK_InfoBlock)
            {
            
                ReaderID = br.ReadEnum<eBlockID>();
                InfoBlockSize = br.ReadInt32();
                
                if (ReaderID != eBlockID.TPK_InfoBlock)
                {
                
                    br.BaseStream.Position += InfoBlockSize;
                
                }
            
            }

            ReaderOffset = br.BaseStream.Position;
            
            while (br.BaseStream.Position < ReaderOffset + InfoBlockSize)
            {
            
                ReaderID = br.ReadEnum<eBlockID>();
                
                switch (ReaderID)
                {
                    case eBlockID.TPK_InfoPart1:
                        offsets[0] = br.BaseStream.Position;
                        goto default;

                    case eBlockID.TPK_InfoPart2:
                        offsets[1] = br.BaseStream.Position;
                        goto default;

                    case eBlockID.TPK_InfoPart3:
                        offsets[2] = br.BaseStream.Position;
                        goto default;

                    case eBlockID.TPK_InfoPart4:
                        offsets[3] = br.BaseStream.Position;
                        goto default;

                    case eBlockID.TPK_InfoPart5:
                        offsets[4] = br.BaseStream.Position;
                        goto default;

                    case eBlockID.TPK_BinData:
                        offsets[8] = br.BaseStream.Position;
                        goto default;

                    default:
                        int size = br.ReadInt32();
                        br.BaseStream.Position += size;
                        break;
                
                }
            
            }

            while (ReaderID != eBlockID.TPK_DataBlock)
            {
                
                ReaderID = br.ReadEnum<eBlockID>();
                DataBlockSize = br.ReadInt32();

                if (ReaderID != eBlockID.TPK_DataBlock)
                {

                    br.BaseStream.Position += DataBlockSize;

                }

            }

            ReaderOffset = br.BaseStream.Position; // relative offset
            
            while (br.BaseStream.Position < ReaderOffset + DataBlockSize)
            {
            
                ReaderID = br.ReadEnum<eBlockID>();
                
                switch (ReaderID)
                {
                    case eBlockID.TPK_DataPart1:
                        offsets[5] = br.BaseStream.Position;
                        goto default;

                    case eBlockID.TPK_DataPart2:
                        offsets[6] = br.BaseStream.Position;
                        goto default;

                    case eBlockID.TPK_DataPart3:
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

                this._collection_name = !this.Manager.Contains(fname)
                    ? fname : !this.Manager.Contains(cname)
                    ? cname : $"TPK{this.Manager.Count}";

            }
            else
            {

                this._collection_name = !this.Manager.Contains(cname)
                    ? cname :
                    $"TPK{this.Manager.Count}";

            }

            // Get the rest of the settings
            br.BaseStream.Position += 4;
            this.PermBlockByteOffset = br.ReadUInt32();
            this.PermBlockByteSize = br.ReadUInt32();
            this.EndianSwapped = br.ReadInt32();
            this.TexturePack = br.ReadInt32();
            this.TextureIndexEntryTable = br.ReadInt32();
            this.TextureStreamEntryTable = br.ReadInt32();
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
        /// Parses compressed texture and returns it on the output.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="TPKBlock"/> with.</param>
        /// <param name="offslot">Offslot of the texture to be parsed</param>
        /// <returns>Decompressed texture valid to the current support.</returns>
        protected override void ParseCompTexture(BinaryReader br, OffSlot offslot)
        {
            const int headersize = 0x7C + 0x20; // texture header size + comp slot size
            br.BaseStream.Position += offslot.AbsoluteOffset;
            var offset = br.BaseStream.Position; // save this position

            // Textures are as one, meaning we read once and it is compressed block itself
            var array = br.ReadBytes(offslot.EncodedSize);
            array = Interop.Decompress(array);

            // Header is always located at the end of data, meaning last MagicHeader
            using var ms = new MemoryStream(array);
            using var texr = new BinaryReader(ms);

            // Texture header is located at the end of data
            int datalength = array.Length - headersize;
            texr.BaseStream.Position = datalength;

            // Create new texture based on header found
            var texture = new Texture(texr, this);

            // Read compression values right after
            texr.BaseStream.Position += 8;
            texture.CompressionValue1 = texr.ReadInt32();
            texture.CompressionValue2 = texr.ReadInt32();
            texture.CompressionValue3 = texr.ReadInt32();

            // Initialize stack for data and copy it
            texture.Data = new byte[datalength];

            // Quick way to copy palette in front and data to the back
            if (texture.PaletteSize == 0 || texture.Offset >= texture.PaletteOffset)
			{

                Array.Copy(array, 0, texture.Data, 0, datalength);

			}
            else
			{

                Array.Copy(array, texture.Size, texture.Data, 0, texture.PaletteSize);
                Array.Copy(array, 0, texture.Data, texture.PaletteSize, texture.Size);

			}

            // Add texture to this TPK
            this.Textures.Add(texture);
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

        #endregion

        #region Writing Methods

        /// <summary>
        /// Assembles partial 1 part1 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part1.</returns>
        protected override void Get1Part1(BinaryWriter bw)
        {
            bw.WriteEnum(eBlockID.TPK_InfoPart1); // write ID
            bw.Write(0x7C); // write size
            bw.WriteEnum(this.Version);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x1C);

            // Write Filename
            bw.WriteNullTermUTF8(this.Filename, 0x40);

            // Write all other settings
            bw.Write(this.FilenameHash);
            bw.Write(this.PermBlockByteOffset);
            bw.Write(this.PermBlockByteSize);
            bw.Write(this.EndianSwapped);
            bw.Write(this.TexturePack);
            bw.Write(this.TextureIndexEntryTable);
            bw.Write(this.TextureStreamEntryTable);
        }

        /// <summary>
        /// Assembles partial 1 part2 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 1 part2.</returns>
        protected override void Get1Part2(BinaryWriter bw)
        {
            bw.WriteEnum(eBlockID.TPK_InfoPart2); // write ID
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
            bw.WriteEnum(eBlockID.TPK_InfoPart3); // write ID
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
            bw.WriteEnum(eBlockID.TPK_InfoPart4); // write ID
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
            bw.WriteEnum(eBlockID.TPK_InfoPart5); // write ID
            bw.Write(this.Textures.Count * 0x20); // write size
            
            for (int loop = 0; loop < this.Textures.Count; ++loop)
            {
            
                bw.Write((long)0);
                bw.Write(this.Textures[loop].CompressionValue1);
                bw.Write(this.Textures[loop].CompressionValue2);
                bw.Write(this.Textures[loop].CompressionValue3);
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
            bw.WriteEnum(eBlockID.TPK_DataPart1); // write ID
            bw.Write(0x18); // write size
            bw.Write((long)0);
            bw.Write(1);
            bw.Write(this.FilenameHash);
            bw.Write((long)0);
            bw.Write(0);
            bw.Write(0x50);
            bw.WriteNullTermUTF8(this.Watermark, 0x20);
            bw.WriteBytes(0x30);
        }

        /// <summary>
        /// Assembles partial 2 part2 of the tpk block.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <returns>Byte array of the partial 2 part2.</returns>
        protected override void Get2Part2(BinaryWriter bw)
        {
            bw.WriteEnum(eBlockID.TPK_DataPart2); // write ID
            bw.Write(-1); // write size
            var position = bw.BaseStream.Position;

            for (int loop = 0; loop < 30; ++loop)
            {

                bw.Write(0x11111111);

            }

            for (int loop = 0; loop < this.Textures.Count; ++loop)
            {

                bw.Write(this.Textures[loop].Data);
                bw.FillBuffer(0x80);
            
            }

            bw.BaseStream.Position = position - 4;
            bw.Write((int)(bw.BaseStream.Length - position));
            bw.BaseStream.Position = bw.BaseStream.Length;
        }

        /// <summary>
        /// Assembles partial 2 part2 as compressed block and return all offslots generated.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="thisOffset">Offset of this TPK in BinaryWriter passed.</param>
        protected override List<OffSlot> Get2CompressedPart2(BinaryWriter bw, int thisOffset)
        {
            // Initialize result offslot list
            var result = new List<OffSlot>(this.Textures.Count);

            // Save position and write ID with temporary size
            bw.WriteEnum(eBlockID.TPK_DataPart2);
            bw.Write(0xFFFFFFFF);
            var start = bw.BaseStream.Position;

            // Write padding alignment
            for (int loop = 0; loop < 30; ++loop)
            {

                bw.Write(0x11111111);

            }

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

            // Action delegate to write texture header and dds info header
            var WriteHeader = new Action<Texture, BinaryWriter>((texture, writer) =>
            {

                texture.Offset = totalTexSize;
                texture.PaletteOffset = totalTexSize + texture.Size;
                var nextPos = writer.BaseStream.Position + 0x7C;
                texture.Assemble(writer);
                writer.BaseStream.Position = nextPos;
                writer.Write((long)0);
                writer.Write(texture.CompressionValue1);
                writer.Write(texture.CompressionValue2);
                writer.Write(texture.CompressionValue3);
                writer.Write(Comp.GetInt(texture.Compression));
                writer.Write((long)0);

            });

            // Iterate through every texture. Each iteration creates an OffSlot class 
            // that is yield returned to IEnumerable output. AbsoluteOffset of each 
            // OffSlot initially is offset from the beginning of part 2 + 0x78 bytes 
            // for padding. Those offsets will later be changed while writing Part 1-3.
            foreach (var texture in this.Textures)
            {

                const int texHeaderSize = 0x7C + 0x20; // size of texture header + dds info header

                // Initialize array of texture data and header, write it
                var array = new byte[texture.Data.Length + texHeaderSize];

                if (texture.HasPalette)
                {

                    Array.Copy(texture.Data, texture.PaletteSize, array, 0, texture.Size);
                    Array.Copy(texture.Data, 0, array, texture.Size, texture.PaletteSize);

                }
                else
                {

                    Array.Copy(texture.Data, 0, array, 0, texture.Data.Length);

                }

                using (var strMemory = new MemoryStream(array))
                using (var strWriter = new BinaryWriter(strMemory))
                {

                    strWriter.BaseStream.Position = texture.Data.Length;
                    WriteHeader(texture, strWriter);
                    CalculateNextOffset(texture.Data.Length);

                }

                // Compress texture data with the best compression
                array = Interop.Compress(array, eLZCompressionType.BEST);

                // Create new Offslot that will be yield returned
                var offslot = new OffSlot()
                {
                    Key = texture.BinKey,
                    AbsoluteOffset = (int)(bw.BaseStream.Position - thisOffset),
                    DecodedSize = texture.Data.Length + texHeaderSize,
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
            bw.WriteBytes(SerializationHeader.ThisSize + 4);

            using (var ms = new MemoryStream(this.Animations.Count * 0x200 + 0x80))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);
                writer.WriteEnum(this.IsCompressed);
                writer.Write(this.Animations.Count);
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

                writer.WriteBytes(0x40);

                array = ms.ToArray();
                array = Interop.Compress(array, eLZCompressionType.BEST);
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
            this.IsCompressed = reader.ReadEnum<eBoolean>();
            int animcount = reader.ReadInt32();
            int textcount = reader.ReadInt32();
            this.Animations.Capacity = animcount;
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

            for (int loop = 0; loop < textcount; ++loop)
            {

                var texture = new Texture()
                {
                    TPK = this
                };

                var header = new SerializationHeader();
                header.Read(br);

                // Check for consistency
                if (header.ID != eBlockID.Nikki) break;
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
            var textures = new List<Texture>(other.Textures);

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

            this.Animations = animations;
            this.Textures = textures;
            this.IsCompressed = other.IsCompressed;
            this.SettingData = other.SettingData;
        }

        #endregion
    }
}