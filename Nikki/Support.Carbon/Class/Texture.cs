using System;
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



namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="Texture"/> is a collection of dds image data used by the game.
    /// </summary>
    public class Texture : Shared.Class.Texture
    {
        #region Fields

        private string _collection_name;
        private byte _compression = EAComp.RGBA_08;
        private uint _class = 0x001A93CF;
        private byte _apply_alpha_sort = 0;
        private byte _alpha_usage_type = 2;
        private byte _alpha_blend_type = 1;
        private int _area = 0;
        private short _num_palettes = 0;
        private byte _pal_comp = 0;
        private uint _cube_environment = 0;
        private byte _bias_level = 0;
        private byte _rendering_order = 5;
        private byte _used_flag = 0;
        private byte _flags = 0;
        private byte _padding = 0;
        private short _offsetS = 0;
        private short _offsetT = 0x100;
        private short _scaleS = 0x100;
        private short _scaleT = 0;
        private byte _scroll_type = 0;
        private short _scroll_timestep = 0;
        private short _scroll_speedS = 0;
        private short _scroll_speedT = 0;
        private int _unknown1 = 0;
        private int _unknown2 = 0;
        private int _unknown3 = 0;

        private int _located_at = 0;
        private int _size_of_block = 0;
        private string _parent_TPK;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.Carbon;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Carbon.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Carbon Database { get; set; }

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
                    throw new CollectionExistenceException();
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
        /// DDS data of the texture.
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Compression type value of the texture.
        /// </summary>
        public override string Compression => Comp.GetString(this._compression);

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
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public Texture(string CName, string _TPK, Database.Carbon db)
        {
            this.Database = db;
            this._collection_name = CName;
            this._parent_TPK = _TPK;
            this.BinKey = CName.BinHash();
            this.PaletteOffset = -1;
            this._padding = 0;
        }
        
        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="_TPK"><see cref="TPKBlock"/> to which this texture belongs to.</param>
        /// <param name="filename">Filename of the texture to import.</param>
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public Texture(string CName, string _TPK, string filename, Database.Carbon db)
        {
            this.Database = db;
            this._collection_name = CName;
            this._parent_TPK = _TPK;
            this.BinKey = CName.BinHash();
            this.PaletteOffset = -1;
            this._padding = 0;
            this.Initialize(filename);
        }

        /// <summary>
        /// Initializes new instance of <see cref="Texture"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="_TPK"><see cref="TPKBlock"/> to which this texture belongs to.</param>
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public Texture(BinaryReader br, string _TPK, Database.Carbon db)
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
            int a1 = (this._collection_name.Length > 0x22) ? 0x22 : this._collection_name.Length;
            int a2 = 0x5D + a1 - ((1 + a1) % 4); // size of the texture header

            // Write all settings
            bw.Write(this._cube_environment);
            bw.Write(this.BinKey);
            bw.Write(this._class);
            bw.Write((uint)this.Offset);
            bw.Write((uint)this.PaletteOffset);
            bw.Write(this.Size);
            bw.Write(this.PaletteSize);
            bw.Write(this._area);
            bw.Write(this.Width);
            bw.Write(this.Height);
            bw.Write(this.Log_2_Width);
            bw.Write(this.Log_2_Height);
            bw.Write(this._compression);
            bw.Write(this._pal_comp);
            bw.Write(this._num_palettes);
            bw.Write(this.Mipmaps);
            bw.Write(this.TileableUV);
            bw.Write(this._bias_level);
            bw.Write(this._rendering_order);
            bw.Write(this._scroll_type);
            bw.Write(this._used_flag);
            bw.Write(this._apply_alpha_sort);
            bw.Write(this._alpha_usage_type);
            bw.Write(this._alpha_blend_type);
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
            bw.Write((byte)(a2 - 0x59));

            // Write CollectionName
            for (int a3 = 0; a3 < a1; ++a3)
                bw.Write((byte)this.CollectionName[a3]);
        }

        /// <summary>
        /// Disassembles array into <see cref="Texture"/> header properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Texture"/> header with.</param>
        public override void Disassemble(BinaryReader br)
        {
            this._cube_environment = br.ReadUInt32();
            this.BinKey = br.ReadUInt32();
            this._class = br.ReadUInt32();
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
            this._bias_level = br.ReadByte();
            this._rendering_order = br.ReadByte();
            this._scroll_type = br.ReadByte();
            this._used_flag = br.ReadByte();
            this._apply_alpha_sort = br.ReadByte();
            this._alpha_usage_type = br.ReadByte();
            this._alpha_blend_type = br.ReadByte();
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

            // Get texture name
            this._collection_name = br.ReadNullTermUTF8(br.ReadByte());
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
                DDSHeader.dwFlags += (uint)DDS_HEADER_FLAGS.PITCH; // add pitch for uncompressed
            else
                DDSHeader.dwFlags += (uint)DDS_HEADER_FLAGS.LINEARSIZE; // add linearsize for compressed

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
            for (int a1 = 0; a1 < 11; ++a1)
                bw.Write(DDSHeader.dwReserved1[a1]);
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
                this._compression = EAComp.RGBA_08;
                this._area = this.Width * this.Height * 4;
            }
            else
            {
                this._compression = Comp.GetByte(br.ReadUInt32());
                this._area = Comp.FlipToBase(this.Size);
            }

            // Default all other values
            this._num_palettes = 0;
            this.TileableUV = 0;
            this._bias_level = 0;
            this._rendering_order = 5;
            this._scroll_type = 0;
            this._used_flag = 0;
            this._apply_alpha_sort = 0;
            this._alpha_usage_type = (byte)eAlphaUsageType.TEXUSAGE_MODULATED;
            this._alpha_blend_type = (byte)eTextureAlphaBlendType.TEXBLEND_BLEND;
            this._flags = 0;
            this.MipmapBiasType = (byte)eTextureMipmapBiasType.TEXBIAS_DEFAULT;
            this._scroll_timestep = 0;
            this._scroll_speedS = 0;
            this._scroll_speedT = 0;
            this._offsetS = 0;
            this._offsetT = 0x100;
            this._scaleS = 0x100;
            this._scaleT = 0;

            // Copy data to the memory
            br.BaseStream.Position = 0x80;
            this.Data = br.ReadBytes(this.Size);
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
                this.Data = br.ReadBytes(total);
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
            var result = new Texture(CName, this._parent_TPK, this.Database)
            {
                _offsetS = this._offsetS,
                _offsetT = this._offsetT,
                _scaleS = this._scaleS,
                _scaleT = this._scaleT,
                _scroll_type = this._scroll_type,
                _scroll_timestep = this._scroll_timestep,
                _scroll_speedS = this._scroll_speedS,
                _scroll_speedT = this._scroll_speedT,
                _area = this._area,
                _num_palettes = this._num_palettes,
                _apply_alpha_sort = this._apply_alpha_sort,
                _alpha_usage_type = this._alpha_usage_type,
                _alpha_blend_type = this._alpha_blend_type,
                _cube_environment = this._cube_environment,
                _bias_level = this._bias_level,
                _rendering_order = this._rendering_order,
                _used_flag = this._used_flag,
                _flags = this._flags,
                _padding = this._padding,
                _unknown1 = this._unknown1,
                _unknown2 = this._unknown2,
                _unknown3 = this._unknown3,
                _class = this._class,
                _compression = this._compression,
                _pal_comp = this._pal_comp,
                Mipmaps = this.Mipmaps,
                MipmapBiasType = this.MipmapBiasType,
                Height = this.Height,
                Width = this.Width,
                TileableUV = this.TileableUV,
                Offset = this.Offset,
                Size = this.Size,
                PaletteOffset = this.PaletteOffset,
                PaletteSize = this.PaletteSize,
                Data = new byte[this.Data.Length]
            };

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