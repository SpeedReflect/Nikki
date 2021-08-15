using System;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.DDS;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="Texture"/> is a collection of dds image data used by the game.
    /// </summary>
    public class Texture : Shared.Class.Texture
    {
        #region Fields

        private string _collection_name;
        private uint _binkey;
        
        [MemoryCastable()]
        private TextureCompressionType _compression = TextureCompressionType.TEXCOMP_32BIT;

        [MemoryCastable()]
        private int _area = 0;

        [MemoryCastable()]
        private short _num_palettes = 0;

        [MemoryCastable()]
        private byte _pal_comp = 0;

        [MemoryCastable()]
        private byte _used_flag = 0;

        [MemoryCastable()]
        private byte _padding = 0;

        [MemoryCastable()]
        private int _unknown0 = 0;

        [MemoryCastable()]
        private int _unknown1 = 0;

        [MemoryCastable()]
        private int _unknown2 = 0;

        [MemoryCastable()]
        private int _unknown3 = 0;

        [MemoryCastable()]
        private int _unknown4 = 0;

        [MemoryCastable()]
        private int _unknown5 = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override GameINT GameINT => GameINT.Underground2;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.Underground2.ToString();

        /// <summary>
        /// <see cref="TPKBlock"/> to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public TPKBlock TPK { get; set; }

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
                if (String.IsNullOrWhiteSpace(value))
                {

                    throw new ArgumentNullException("This value cannot be left empty.");

                }

                if (value.Contains(" "))
                {

                    throw new Exception("CollectionName cannot contain whitespace.");


                }

                var key = value.BinHash();
                var type = KeyType.BINKEY;

                if (this.TPK?.GetTextureIndex(key, type) != -1)
                {

                    throw new CollectionExistenceException(value);

                }

                this._collection_name = value;
                this._binkey = key;
            }
        }

        /// <summary>
        /// Binary memory hash of the collection name.
        /// </summary>
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public override uint BinKey => this._binkey;

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public override uint VltKey => this._collection_name.VltHash();

        /// <summary>
        /// Compression type value of the texture.
        /// </summary>
        [Category("Primary")]
        public override TextureCompressionType Compression => this._compression;

        /// <summary>
        /// Used in TPK compression blocks.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public int CompressionValue1 { get; set; } = 1;

        /// <summary>
        /// Used in TPK compression blocks.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public int CompressionValue2 { get; set; } = 5;

        /// <summary>
        /// Used in TPK compression blocks.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public int CompressionValue3 { get; set; } = 6;

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        public Texture() { }

        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="tpk"><see cref="TPKBlock"/> to which this instance belongs to.</param>
        public Texture(string CName, TPKBlock tpk)
        {
            this.TPK = tpk;
            this._collection_name = CName;
            this._binkey = CName.BinHash();
            this.PaletteOffset = 0;
            this._padding = 1;
        }
        
        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="filename">Filename of the texture to import.</param>
        /// <param name="tpk"><see cref="TPKBlock"/> to which this instance belongs to.</param>
        public Texture(string CName, string filename, TPKBlock tpk)
        {
            this.TPK = tpk;
            this._collection_name = CName;
            this._binkey = CName.BinHash();
            this.PaletteOffset = 0;
            this._padding = 1;
            this.Initialize(filename);
        }

        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="tpk"><see cref="TPKBlock"/> to which this instance belongs to.</param>
        public Texture(BinaryReader br, TPKBlock tpk)
        {
            this.TPK = tpk;
            this.Disassemble(br);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="Texture"/> header into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Texture"/> header with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write padding
            bw.Write((int)0);
            bw.Write((long)0);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x18);

            // Write all settings
            bw.Write(this._binkey);
            bw.Write(this.ClassKey);
            bw.Write(this._unknown0);
            bw.Write(this.Offset);
            bw.Write(this.HasPalette ? this.PaletteOffset : -1);
            bw.Write(this.Size);
            bw.Write(this.PaletteSize);
            bw.Write(this._area);
            bw.Write(this.Width);
            bw.Write(this.Height);
            bw.Write(this.Log_2_Width);
            bw.Write(this.Log_2_Height);
            bw.WriteEnum(this._compression);
            bw.Write(this._pal_comp);
            bw.Write(this._num_palettes);
            bw.Write(this.Mipmaps);
            bw.WriteEnum(this.TileableUV);
            bw.Write(this.BiasLevel);
            bw.Write(this.RenderingOrder);
            bw.WriteEnum(this.ScrollType);
            bw.Write(this._used_flag);
            bw.Write(this.ApplyAlphaSort);
            bw.WriteEnum(this.AlphaUsageType);
            bw.WriteEnum(this.AlphaBlendType);
            bw.Write(this.Flags);
            bw.WriteEnum(this.MipmapBiasType);
            bw.Write(this._padding);
            bw.Write(this.ScrollTimestep);
            bw.Write(this.ScrollSpeedS);
            bw.Write(this.ScrollSpeedT);
            bw.Write(this.OffsetS);
            bw.Write(this.OffsetT);
            bw.Write(this.ScaleS);
            bw.Write(this.ScaleT);
            bw.Write(this._unknown1);
            bw.Write(this._unknown2);
            bw.Write(this._unknown3);
            bw.Write(this._unknown4);
            bw.Write(this._unknown5);
        }

        /// <summary>
        /// Disassembles array into <see cref="Texture"/> header properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Texture"/> header with.</param>
        public override void Disassemble(BinaryReader br)
        {
            // Get texture name
            br.BaseStream.Position += 0xC;
            var cname = br.ReadNullTermUTF8(0x18);

            this._binkey = br.ReadUInt32();
            this.ClassKey = br.ReadUInt32();
            this._unknown0 = br.ReadInt32();
            this.Offset = br.ReadInt32();
            this.PaletteOffset = br.ReadInt32();
            this.Size = br.ReadInt32();
            this.PaletteSize = br.ReadInt32();
            this._area = br.ReadInt32();
            this.Width = br.ReadInt16();
            this.Height = br.ReadInt16();
            br.BaseStream.Position += 2; // skip logs
            this._compression = br.ReadEnum<TextureCompressionType>();
            this._pal_comp = br.ReadByte();
            this._num_palettes = br.ReadInt16();
            this.Mipmaps = br.ReadByte();
            this.TileableUV = br.ReadEnum<TextureTileableType>();
            this.BiasLevel = br.ReadByte();
            this.RenderingOrder = br.ReadByte();
            this.ScrollType = br.ReadEnum<TextureScrollType>();
            this._used_flag = br.ReadByte();
            this.ApplyAlphaSort = br.ReadByte();
            this.AlphaUsageType = br.ReadEnum<TextureAlphaUsageType>();
            this.AlphaBlendType = br.ReadEnum<TextureAlphaBlendType>();
            this.Flags = br.ReadByte();
            this.MipmapBiasType = br.ReadEnum<TextureMipmapBiasType>();
            this._padding = br.ReadByte();
            this.ScrollTimestep = br.ReadInt16();
            this.ScrollSpeedS = br.ReadInt16();
            this.ScrollSpeedT = br.ReadInt16();
            this.OffsetS = br.ReadInt16();
            this.OffsetT = br.ReadInt16();
            this.ScaleS = br.ReadInt16();
            this.ScaleT = br.ReadInt16();
            this._unknown1 = br.ReadInt32();
            this._unknown2 = br.ReadInt32();
            this._unknown3 = br.ReadInt32();
            this._unknown4 = br.ReadInt32();
            this._unknown5 = br.ReadInt32();

            if (!Map.BinKeys.TryGetValue(this._binkey, out this._collection_name))
            {

                this._collection_name = cname;
                this._collection_name.BinHash();

            }
        }

        /// <summary>
        /// Gets .dds data along with the .dds header.
        /// </summary>
        /// <returns>.dds texture as a byte array.</returns>
        /// <param name="make_no_palette">True if palette should be decompressed into 
        /// 32 bpp DDS; false otherwise.</param>
        public override byte[] GetDDSArray(bool make_no_palette)
        {
            var generator = new Generator(this, make_no_palette);
            return generator.GetDDSTexture();
        }

        /// <summary>
        /// Initializes all properties of the new <see cref="Texture"/>.
        /// </summary>
        /// <param name="filename">Filename of the .dds texture passed.</param>
        protected override void Initialize(string filename)
        {
            var data = File.ReadAllBytes(filename);

            var generator = new Generator(data, false);
            generator.GetDDSSettings();

            this.Size = generator.Size;
            this.PaletteSize = generator.PaletteSize;
            this._area = generator.Area;
            this.Width = (short)generator.Width;
            this.Height = (short)generator.Height;
            this.Mipmaps = (byte)generator.MipMaps;
            this._compression = generator.Compression;
            this.Data = generator.Buffer;

            // Default all other values
            this._num_palettes = (short)(this.PaletteSize / 4);
            this.ScrollTimestep = 0;
            this.ScrollSpeedS = 0;
            this.ScrollSpeedT = 0;
            this.OffsetS = 0;
            this.OffsetT = 0x100;
            this.ScaleS = 0x100;
            this.ScaleT = 0;
        }

        /// <summary>
        /// Reads .dds data from the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="forced">If forced, ignores internal offset and reads data 
        /// starting at the pointer passed.</param>
        public override void ReadData(BinaryReader br, bool forced)
        {
            // Initialize data
            int total = this.PaletteSize + this.Size;
            var data = new byte[total];
            
            if (forced)
            {

                data = br.ReadBytes(total);

            }
            else
            {
             
                var offset = br.BaseStream.Position;
                br.BaseStream.Position = offset + this.PaletteOffset;
                Array.Copy(br.ReadBytes(this.PaletteSize), 0, data, 0, this.PaletteSize);
                br.BaseStream.Position = offset + this.Offset;
                Array.Copy(br.ReadBytes(this.Size), 0, data, this.PaletteSize, this.Size);
            
            }

            this.Data = data;
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new Texture(CName, this.TPK);
            base.MemoryCast(this, result);
            CopyMemory(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="Texture"/> 
        /// as a string value.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return $"Collection Name: {this.CollectionName} | " +
                   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
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
            var datalist = new List<byte[]>();

            var size = this.DataLength >> 15;
            var modulo = this.DataLength % 0x8000;

            using (var ms = new MemoryStream(0x100 + this._collection_name.Length))
            using (var writer = new BinaryWriter(ms))
            {

                // Write header info
                writer.WriteNullTermUTF8(this._collection_name);
                writer.Write(this._binkey);
                writer.Write(this.ClassKey);
                writer.Write(this._unknown0);
                writer.Write(this.Size);
                writer.Write(this.PaletteSize);
                writer.Write(this._area);
                writer.Write(this.Width);
                writer.Write(this.Height);
                writer.WriteEnum(this._compression);
                writer.Write(this._pal_comp);
                writer.Write(this._num_palettes);
                writer.Write(this.Mipmaps);
                writer.WriteEnum(this.TileableUV);
                writer.Write(this.BiasLevel);
                writer.Write(this.RenderingOrder);
                writer.WriteEnum(this.ScrollType);
                writer.Write(this._used_flag);
                writer.Write(this.ApplyAlphaSort);
                writer.WriteEnum(this.AlphaUsageType);
                writer.WriteEnum(this.AlphaBlendType);
                writer.Write(this.Flags);
                writer.WriteEnum(this.MipmapBiasType);
                writer.Write(this.ScrollTimestep);
                writer.Write(this.ScrollSpeedS);
                writer.Write(this.ScrollSpeedT);
                writer.Write(this.OffsetS);
                writer.Write(this.OffsetT);
                writer.Write(this.ScaleS);
                writer.Write(this.ScaleT);
                writer.Write(this._unknown1);
                writer.Write(this._unknown2);
                writer.Write(this._unknown3);
                writer.Write(this._unknown4);
                writer.Write(this._unknown5);
                writer.Write(this.CompressionValue1);
                writer.Write(this.CompressionValue2);
                writer.Write(this.CompressionValue3);
                writer.WriteBytes(0, 0x20); // write padding for better compression
                writer.Write(modulo == 0 ? size : size + 1);

                array = Interop.Compress(ms.ToArray(), LZCompressionType.RAWW);
                datalist.Add(array);

            }

            var data = this.Data;

            for (int loop = 0; loop <= size; ++loop)
            {

                var total = loop == size ? modulo : 0x8000;

                if (total == 0) break;

                var temp = new byte[total];
                Array.Copy(data, loop << 15, temp, 0, total);
                array = Interop.Compress(temp, LZCompressionType.BEST);
                datalist.Add(array);

            }

            var sum = datalist.Aggregate(0, (res, arr) => res += arr.Length);
            sum += datalist.Count << 2;
            var header = new SerializationHeader(sum, this.GameINT, "TEXTURE");
            header.Write(bw);
            bw.Write(sum);

            foreach (var arr in datalist)
            {

                bw.Write(arr.Length);
                bw.Write(arr);

            }
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
            this._binkey = reader.ReadUInt32();
            this.ClassKey = reader.ReadUInt32();
            this._unknown0 = reader.ReadInt32();
            this.Size = reader.ReadInt32();
            this.PaletteSize = reader.ReadInt32();
            this._area = reader.ReadInt32();
            this.Width = reader.ReadInt16();
            this.Height = reader.ReadInt16();
            this._compression = reader.ReadEnum<TextureCompressionType>();
            this._pal_comp = reader.ReadByte();
            this._num_palettes = reader.ReadInt16();
            this.Mipmaps = reader.ReadByte();
            this.TileableUV = reader.ReadEnum<TextureTileableType>();
            this.BiasLevel = reader.ReadByte();
            this.RenderingOrder = reader.ReadByte();
            this.ScrollType = reader.ReadEnum<TextureScrollType>();
            this._used_flag = reader.ReadByte();
            this.ApplyAlphaSort = reader.ReadByte();
            this.AlphaUsageType = reader.ReadEnum<TextureAlphaUsageType>();
            this.AlphaBlendType = reader.ReadEnum<TextureAlphaBlendType>();
            this.Flags = reader.ReadByte();
            this.MipmapBiasType = reader.ReadEnum<TextureMipmapBiasType>();
            this.ScrollTimestep = reader.ReadInt16();
            this.ScrollSpeedS = reader.ReadInt16();
            this.ScrollSpeedT = reader.ReadInt16();
            this.OffsetS = reader.ReadInt16();
            this.OffsetT = reader.ReadInt16();
            this.ScaleS = reader.ReadInt16();
            this.ScaleT = reader.ReadInt16();
            this._unknown1 = reader.ReadInt32();
            this._unknown2 = reader.ReadInt32();
            this._unknown3 = reader.ReadInt32();
            this._unknown4 = reader.ReadInt32();
            this._unknown5 = reader.ReadInt32();
            this.CompressionValue1 = reader.ReadInt32();
            this.CompressionValue2 = reader.ReadInt32();
            this.CompressionValue3 = reader.ReadInt32();
            reader.BaseStream.Position += 0x20;
            var count = reader.ReadInt32();

            var data = new byte[this.Size + this.PaletteSize];

            for (int loop = 0; loop < count; ++loop)
            {

                var total = br.ReadInt32();
                var temp = br.ReadBytes(total);
                temp = Interop.Decompress(temp);
                Array.Copy(temp, 0, data, loop << 15, temp.Length);

            }

            this.Data = data;
        }

        #endregion
    }
}