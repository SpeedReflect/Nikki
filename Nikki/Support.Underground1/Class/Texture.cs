﻿using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Utils.DDS;
using Nikki.Reflection.ID;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Class
{
    /// <summary>
    /// <see cref="Texture"/> is a collection of dds image data used by the game.
    /// </summary>
    public class Texture : Shared.Class.Texture
    {
        #region Fields

        private string _collection_name;

        [MemoryCastable()]
        private byte _compression = EAComp.RGBA_08;

        [MemoryCastable()]
        private bool _secretp8 = false; // true if _compression = 0x81 at disassembly

        [MemoryCastable()]
        private int _area = 0;

        [MemoryCastable()]
        private short _num_palettes = 0;

        [MemoryCastable()]
        private byte _pal_comp = 0;

        [MemoryCastable()]
        private byte _used_flag = 0;

        [MemoryCastable()]
        private byte _flags = 0;

        [MemoryCastable()]
        private byte _padding = 0;

        [MemoryCastable()]
        private short _offsetS = 0;

        [MemoryCastable()]
        private short _offsetT = 0x100;

        [MemoryCastable()]
        private short _scaleS = 0x100;

        [MemoryCastable()]
        private short _scaleT = 0;

        [MemoryCastable()]
        private short _scroll_timestep = 0;

        [MemoryCastable()]
        private short _scroll_speedS = 0;

        [MemoryCastable()]
        private short _scroll_speedT = 0;

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

        private string _parent_TPK;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.Underground1;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Underground1.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Underground1 Database { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                var tpk = this.Database.TPKBlocks.FindCollection(this._parent_TPK);
                var key = value.BinHash();
                var type = eKeyType.BINKEY;
                if (tpk.GetTextureIndex(key, type) != -1)
                    throw new CollectionExistenceException(value);
                this._collection_name = value;
                this.BinKey = key;
            }
        }

        /// <summary>
        /// Binary memory hash of the collection name.
        /// </summary>
        public override uint BinKey { get; set; }

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public override uint VltKey => this._collection_name.VltHash();

        /// <summary>
        /// Compression type value of the texture.
        /// </summary>
        public override string Compression => Comp.GetString(this._compression);

        /// <summary>
        /// Used in TPK compression blocks.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public int CompressionValue1 { get; set; } = 1;

        /// <summary>
        /// Used in TPK compression blocks.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public int CompressionValue2 { get; set; } = 5;

        /// <summary>
        /// Used in TPK compression blocks.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
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
        /// <param name="_TPK"><see cref="TPKBlock"/> to which this texture belongs to.</param>
        /// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
        public Texture(string CName, string _TPK, Database.Underground1 db)
        {
            this.Database = db;
            this._collection_name = CName;
            this._parent_TPK = _TPK;
            this.BinKey = CName.BinHash();
            this.PaletteOffset = 0;
            this._padding = 1;
        }

        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="_TPK"><see cref="TPKBlock"/> to which this texture belongs to.</param>
        /// <param name="filename">Filename of the texture to import.</param>
        /// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
        public Texture(string CName, string _TPK, string filename, Database.Underground1 db)
        {
            this.Database = db;
            this._collection_name = CName;
            this._parent_TPK = _TPK;
            this.BinKey = CName.BinHash();
            this.PaletteOffset = 0;
            this._padding = 1;
            this.Initialize(filename);
        }

        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="_TPK"><see cref="TPKBlock"/> to which this texture belongs to.</param>
        /// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
        public Texture(BinaryReader br, string _TPK, Database.Underground1 db)
        {
            this.Database = db;
            this._parent_TPK = _TPK;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~Texture() { }

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
            bw.Write(this.BinKey);
            bw.Write(this.ClassKey);
            bw.Write(this._unknown0);
            bw.Write(this.Offset);
            bw.Write(this._compression == EAComp.P8_08 ? this.PaletteOffset : -1);
            bw.Write(this.Size);
            bw.Write(this.PaletteSize);
            bw.Write(this._area);
            bw.Write(this.Width);
            bw.Write(this.Height);
            bw.Write(this.Log_2_Width);
            bw.Write(this.Log_2_Height);
            bw.Write(this._secretp8 ? EAComp.SECRET : this._compression);
            bw.Write(this._pal_comp);
            bw.Write(this._num_palettes);
            bw.Write(this.Mipmaps);
            bw.Write(this.TileableUV);
            bw.Write(this.BiasLevel);
            bw.Write(this.RenderingOrder);
            bw.WriteEnum(this.ScrollType);
            bw.Write(this._used_flag);
            bw.Write(this.ApplyAlphaSort);
            bw.WriteEnum(this.AlphaUsageType);
            bw.WriteEnum(this.AlphaBlendType);
            bw.Write(this._flags);
            bw.WriteEnum(this.MipmapBiasType);
            bw.Write(this._padding);
            bw.Write(this._scroll_timestep);
            bw.Write(this._scroll_speedS);
            bw.Write(this._scroll_speedT);
            bw.Write(this._offsetS);
            bw.Write(this._offsetT);
            bw.Write(this._scaleS);
            bw.Write(this._scaleT);
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
            this._collection_name = br.ReadNullTermUTF8(0x18);

            this.BinKey = br.ReadUInt32();
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
            this._compression = br.ReadByte();
            this._pal_comp = br.ReadByte();
            this._num_palettes = br.ReadInt16();
            this.Mipmaps = br.ReadByte();
            this.TileableUV = br.ReadByte();
            this.BiasLevel = br.ReadByte();
            this.RenderingOrder = br.ReadByte();
            this.ScrollType = br.ReadEnum<eTextureScrollType>();
            this._used_flag = br.ReadByte();
            this.ApplyAlphaSort = br.ReadByte();
            this.AlphaUsageType = br.ReadEnum<eTextureAlphaUsageType>();
            this.AlphaBlendType = br.ReadEnum<eTextureAlphaBlendType>();
            this._flags = br.ReadByte();
            this.MipmapBiasType = br.ReadEnum<eTextureMipmapBiasType>();
            this._padding = br.ReadByte();
            this._scroll_timestep = br.ReadInt16();
            this._scroll_speedS = br.ReadInt16();
            this._scroll_speedT = br.ReadInt16();
            this._offsetS = br.ReadInt16();
            this._offsetT = br.ReadInt16();
            this._scaleS = br.ReadInt16();
            this._scaleT = br.ReadInt16();
            this._unknown1 = br.ReadInt32();
            this._unknown2 = br.ReadInt32();
            this._unknown3 = br.ReadInt32();
            this._unknown4 = br.ReadInt32();
            this._unknown5 = br.ReadInt32();

            if (this._compression == EAComp.SECRET)
            {

                this._compression = EAComp.P8_08;
                this._secretp8 = true;

            }
        }

        /// <summary>
        /// Gets .dds data along with the .dds header.
        /// </summary>
        /// <returns>.dds texture as a byte array.</returns>
        public override byte[] GetDDSArray()
        {
            byte[] data;

            if (this._compression == EAComp.P8_08)
            {

                data = new byte[this.Size * 4 + 0x80];
                var copy = Palette.P8toRGBA(this.Data);
                Buffer.BlockCopy(copy, 0, data, 0x80, copy.Length);

            }
            else
            {

                data = new byte[this.Data.Length + 0x80];
                Buffer.BlockCopy(this.Data, 0, data, 0x80, this.Data.Length);

            }

            // Initialize header first
            var DDSHeader = new DDS_HEADER
            {
                dwHeight = (uint)this.Height,
                dwWidth = (uint)this.Width,
                dwDepth = 1,
                dwMipMapCount = (uint)this.Mipmaps,
                dwFlags = (uint)DDS_HEADER_FLAGS.TEXTURE
            };

            DDSHeader.dwFlags += (uint)DDS_HEADER_FLAGS.MIPMAP;

            if (this._compression == EAComp.RGBA_08 || this._compression == EAComp.P8_08)
            {

                DDSHeader.dwFlags += (uint)DDS_HEADER_FLAGS.PITCH; // add pitch for uncompressed

            }
            else
            {

                DDSHeader.dwFlags += (uint)DDS_HEADER_FLAGS.LINEARSIZE; // add linearsize for compressed

            }

            Comp.GetPixelFormat(DDSHeader.ddspf, this._compression);
            DDSHeader.dwCaps = (uint)DDS_SURFACE.SURFACE_FLAGS_TEXTURE; // by default is a texture
            DDSHeader.dwCaps += (uint)DDS_SURFACE.SURFACE_FLAGS_MIPMAP; // mipmaps should be included
            DDSHeader.dwPitchOrLinearSize = Comp.PitchLinearSize(this._compression, this.Width,
                this.Height, DDSHeader.ddspf.dwRGBBitCount);

            using var ms = new MemoryStream(data);
            using var bw = new BinaryWriter(ms);

            bw.Write(DDS_MAIN.MAGIC);
            bw.Write(DDSHeader.dwSize);
            bw.Write(DDSHeader.dwFlags);
            bw.Write(DDSHeader.dwHeight);
            bw.Write(DDSHeader.dwWidth);
            bw.Write(DDSHeader.dwPitchOrLinearSize);
            bw.Write(DDSHeader.dwDepth);
            bw.Write(DDSHeader.dwMipMapCount);

            for (int loop = 0; loop < 11; ++loop)
            {

                bw.Write(DDSHeader.dwReserved1[loop]);

            }

            bw.Write(DDSHeader.ddspf.dwSize);
            bw.Write(DDSHeader.ddspf.dwFlags);
            bw.Write(DDSHeader.ddspf.dwFourCC);
            bw.Write(DDSHeader.ddspf.dwRGBBitCount);
            bw.Write(DDSHeader.ddspf.dwRBitMask);
            bw.Write(DDSHeader.ddspf.dwGBitMask);
            bw.Write(DDSHeader.ddspf.dwBBitMask);
            bw.Write(DDSHeader.ddspf.dwABitMask);
            bw.Write(DDSHeader.dwCaps);
            bw.Write(DDSHeader.dwCaps2);
            bw.Write(DDSHeader.dwCaps3);
            bw.Write(DDSHeader.dwCaps4);
            bw.Write(DDSHeader.dwReserved2);

            return data;
        }

        /// <summary>
        /// Initializes all properties of the new <see cref="Texture"/>.
        /// </summary>
        /// <param name="filename">Filename of the .dds texture passed.</param>
        protected override void Initialize(string filename)
        {
            var data = File.ReadAllBytes(filename);

            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);

            this.Size = data.Length - 0x80;
            br.BaseStream.Position = 0xC;
            this.Height = (short)br.ReadInt32();
            this.Width = (short)br.ReadInt32();
            br.BaseStream.Position = 0x1C;
            this.Mipmaps = (byte)br.ReadInt32();
            br.BaseStream.Position = 0x50;

            if (br.ReadUInt32() == (uint)DDS_TYPE.RGBA)
            {

                var cdata = Palette.RGBAtoP8(data);

                if (cdata == null)
                {

                    this._compression = EAComp.RGBA_08;
                    this._area = this.Width * this.Height * 4;
                    this.PaletteSize = 0;
                    this.Data = new byte[this.Size];
                    Buffer.BlockCopy(data, 0x80, this.Data, 0, this.Size);

                }
                else
                {

                    this._compression = EAComp.P8_08;
                    this._area = this.Width * this.Height * 4;
                    this.Size = (data.Length - 0x80) / 4;
                    this.PaletteSize = 0x400;
                    this.Data = new byte[cdata.Length];
                    Buffer.BlockCopy(cdata, 0, this.Data, 0, cdata.Length);

                }

            }
            else
            {

                this._compression = Comp.GetByte(br.ReadUInt32());
                this._area = Comp.FlipToBase(this.Size);
                br.BaseStream.Position = 0x80;
                this.Data = br.ReadBytes(this.Size);

            }

            // Default all other values
            this._num_palettes = (short)(this.PaletteSize / 4);
            this.TileableUV = 0;
            this._used_flag = 0;
            this._flags = 0;
            this._scroll_timestep = 0;
            this._scroll_speedS = 0;
            this._scroll_speedT = 0;
            this._offsetS = 0;
            this._offsetT = 0x100;
            this._scaleS = 0x100;
            this._scaleT = 0;
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
            this.Data = new byte[total];

            if (forced)
            {

                this.Data = br.ReadBytes(total);

            }
            else
            {

                var offset = br.BaseStream.Position;
                br.BaseStream.Position = offset + this.PaletteOffset;
                Buffer.BlockCopy(br.ReadBytes(this.PaletteSize), 0, this.Data, 0, this.PaletteSize);
                br.BaseStream.Position = offset + this.Offset;
                Buffer.BlockCopy(br.ReadBytes(this.Size), 0, this.Data, this.PaletteSize, this.Size);

            }
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            var result = new Texture(CName, this._parent_TPK, this.Database);
            base.MemoryCast(this, result);
            result.Data = new byte[this.Data.Length];
            Buffer.BlockCopy(this.Data, 0, result.Data, 0, this.Data.Length);
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
                   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
        }

        #endregion
    }
}