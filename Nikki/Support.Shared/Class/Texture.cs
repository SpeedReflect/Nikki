using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="Texture"/> is a collection of dds image data used by the game.
    /// </summary>
    public abstract class Texture : Collectable, IAssembly
    {
        #region Private Fields

        private byte[] _data;
        private int _decodedSize;

		#endregion

		#region Shared Enums

		/// <summary>
		/// Enum of alpha usage types for textures.
		/// </summary>
		public enum TextureAlphaUsageType : byte
        {
            /// <summary>
            /// 
            /// </summary>
            TEXUSAGE_NONE = 0,

            /// <summary>
            /// 
            /// </summary>
            TEXUSAGE_PUNCHTHRU = 1,

            /// <summary>
            /// 
            /// </summary>
            TEXUSAGE_MODULATED = 2,
        }

        /// <summary>
        /// Enum of alpha blend types for textures.
        /// </summary>
        public enum TextureAlphaBlendType : sbyte
        {
            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_DEFAULT = -1,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_SRCCOPY = 0,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_BLEND = 1,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_ADDATIVE = 2,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_SUBTRACTIVE = 3,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_OVERBRIGHT = 4,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_DEST_BLEND = 5,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_DEST_ADDATIVE = 6,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_DEST_SUBTRACTIVE = 7,

            /// <summary>
            /// 
            /// </summary>
            TEXBLEND_DEST_OVERBRIGHT = 8,
        }

        /// <summary>
        /// Enum of possible mipmap bias types.
        /// </summary>
        public enum TextureMipmapBiasType : byte
        {
            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_DEFAULT = 0,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_ADS = 1,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_ARC = 2,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_OBJ = 3,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_ORG = 4,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_SGN = 5,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_TRN = 6,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_PARTICLES = 7,

            /// <summary>
            /// 
            /// </summary>
            TEXBIAS_NUM = 8,
        }

        /// <summary>
        /// Enum of texture scroll types.
        /// </summary>
        public enum TextureScrollType : byte
        {
            /// <summary>
            /// 
            /// </summary>
            TEXSCROLL_NONE = 0,

            /// <summary>
            /// 
            /// </summary>
            TEXSCROLL_SMOOTH = 1,

            /// <summary>
            /// 
            /// </summary>
            TEXSCROLL_SNAP = 2,

            /// <summary>
            /// 
            /// </summary>
            TEXSCROLL_OFFSETSCALE = 3,
        }

        /// <summary>
        /// Type of tileable UV.
        /// </summary>
        public enum TextureTileableType : byte
        {
            /// <summary>
            /// 
            /// </summary>
            NONE = 0,

            /// <summary>
            /// 
            /// </summary>
            HORIZONTAL = 1,

            /// <summary>
            /// 
            /// </summary>
            VERTICAL = 2,

            /// <summary>
            /// 
            /// </summary>
            ALL_SIDES = 3,
        }

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
        public virtual uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public virtual uint VltKey => this.CollectionName.VltHash();

        /// <summary>
        /// Represents data offset of the block in Global data.
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Represents data size of the block in Global data.
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int Size { get; protected set; } = 0;

        /// <summary>
        /// Represents palette offset of the block in Global data.
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int PaletteOffset { get; set; } = 0;

        /// <summary>
        /// Represents palette size of the block in Global data.
        /// </summary>
        [MemoryCastable()]
        [Browsable(false)]
        public int PaletteSize { get; protected set; } = 0;

        /// <summary>
        /// Compression type of the texture.
        /// </summary>
        [Category("Primary")]
        public abstract TextureCompressionType Compression { get; }

        /// <summary>
        /// Determines whether this <see cref="Texture"/> has palette.
        /// </summary>
        [Category("Primary")]
        public bool HasPalette => this.Compression switch
        {
            TextureCompressionType.TEXCOMP_4BIT => true,
            TextureCompressionType.TEXCOMP_4BIT_IA8 => true,
            TextureCompressionType.TEXCOMP_4BIT_RGB16_A8 => true,
            TextureCompressionType.TEXCOMP_4BIT_RGB24_A8 => true,
            TextureCompressionType.TEXCOMP_8BIT => true,
            TextureCompressionType.TEXCOMP_8BIT_16 => true,
            TextureCompressionType.TEXCOMP_8BIT_64 => true,
            TextureCompressionType.TEXCOMP_8BIT_IA8 => true,
            TextureCompressionType.TEXCOMP_8BIT_RGB16_A8 => true,
            TextureCompressionType.TEXCOMP_8BIT_RGB24_A8 => true,
            TextureCompressionType.TEXCOMP_16BIT => true,
            TextureCompressionType.TEXCOMP_16BIT_1555 => true,
            TextureCompressionType.TEXCOMP_16BIT_3555 => true,
            TextureCompressionType.TEXCOMP_16BIT_565 => true,
            _ => false
        };

        #endregion

        #region Public Properties

        /// <summary>
        /// Represents height in pixels of the texture.
        /// </summary>
        [MemoryCastable()]
        [Category("Primary")]
        public short Width { get; protected set; }

        /// <summary>
        /// Represents height in pixels of the texture.
        /// </summary>
        [MemoryCastable()]
        [Category("Primary")]
        public short Height { get; protected set; }

        /// <summary>
        /// Represents base 2 value of the width of the texture.
        /// </summary>
        [Browsable(false)]
        public byte Log_2_Width => (byte)Math.Log(this.Width, 2);

        /// <summary>
        /// Represents base 2 value of the height of the texture.
        /// </summary>
        [Browsable(false)]
        public byte Log_2_Height => (byte)Math.Log(this.Height, 2);

        /// <summary>
        /// Represents number of mipmaps in the texture.
        /// </summary>
        [MemoryCastable()]
        [Category("Primary")]
        public byte Mipmaps { get; protected set; }

        /// <summary>
        /// Represents class key of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public uint ClassKey { get; set; } = 0x001A93CF;

        /// <summary>
        /// Represents class name of the texture. Directly linked to <see cref="ClassKey"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string ClassName
		{
            get => this.ClassKey.BinString(LookupReturn.EMPTY);
            set => this.ClassKey = value.BinHash();
		}

        /// <summary>
        /// Represents mipmap bias type of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public TextureMipmapBiasType MipmapBiasType { get; set; }

        /// <summary>
        /// Represents mipmap bias type of the texture as a <see cref="Byte"/>. Directly linked to <see cref="MipmapBiasType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte MipmapBiasInt
		{
            get => (byte)this.MipmapBiasType;
            set => this.MipmapBiasType = (TextureMipmapBiasType)value;
		}

        /// <summary>
        /// Represents bias level of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte BiasLevel { get; set; }

        /// <summary>
        /// Represents alpha usage type of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public TextureAlphaUsageType AlphaUsageType { get; set; } = TextureAlphaUsageType.TEXUSAGE_MODULATED;

        /// <summary>
        /// Represents alpha blend type of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public TextureAlphaBlendType AlphaBlendType { get; set; } = TextureAlphaBlendType.TEXBLEND_BLEND;

        /// <summary>
        /// Represents type of applying alpha sort of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte ApplyAlphaSort { get; set; }

        /// <summary>
        /// Represents scroll type of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public TextureScrollType ScrollType { get; set; }

        /// <summary>
        /// Represents rendering order of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte RenderingOrder { get; set; } = 5;

        /// <summary>
        /// Represents tileable level of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public TextureTileableType TileableUV { get; set; }

        /// <summary>
        /// Represents offset S of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short OffsetS { get; set; }

        /// <summary>
        /// Represents offset T of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short OffsetT { get; set; }

        /// <summary>
        /// Represents scale S of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short ScaleS { get; set; }

        /// <summary>
        /// Represents scale T of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short ScaleT { get; set; }

        /// <summary>
        /// Represents scroll time step of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short ScrollTimestep { get; set; }

        /// <summary>
        /// Represents scroll speed S of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short ScrollSpeedS { get; set; }

        /// <summary>
        /// Represents scroll speed T of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short ScrollSpeedT { get; set; }

        /// <summary>
        /// Represents flags of the texture.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte Flags { get; set; }

        /// <summary>
        /// DDS data of this <see cref="Texture"/>.
        /// </summary>
        [Browsable(false)]
        public byte[] Data
		{
            get
			{
                if (this._decodedSize == 0) return this._data;
                return LZF.Decompress(this._data, this._decodedSize);
			}
            set
			{
                if (value is null || value.Length == 0)
				{

                    this._decodedSize = 0;
                    this._data = value;

				}
                else
				{

                    this._decodedSize = value.Length;
                    this._data = LZF.Compress(value);

				}
			}
		}

        /// <summary>
        /// Length of decoded DDS data of this <see cref="Texture"/>.
        /// </summary>
        [Browsable(false)]
        public int DataLength => this._decodedSize;

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="Texture"/> header into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Texture"/> header with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="Texture"/> header properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Texture"/> header with.</param>
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
        /// Gets .dds data along with the .dds header.
        /// </summary>
        /// <returns>.dds texture as a byte array.</returns>
        /// <param name="make_no_palette">True if palette should be decompressed into 
        /// 32 bpp DDS; false otherwise.</param>
        public abstract byte[] GetDDSArray(bool make_no_palette);

        /// <summary>
        /// Initializes all properties of the new <see cref="Texture"/>.
        /// </summary>
        /// <param name="filename">Filename of the .dds texture passed.</param>
        protected abstract void Initialize(string filename);

        /// <summary>
        /// Reads .dds data from the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="forced">If forced, ignores internal offset and reads data 
        /// starting at the pointer passed.</param>
        public abstract void ReadData(BinaryReader br, bool forced);

        /// <summary>
        /// Reloads <see cref="Texture"/> properties based on the new file passed.
        /// </summary>
        /// <param name="filename">Filename of .dds texture passed.</param>
        public virtual void Reload(string filename) => this.Initialize(filename);

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
        /// Returns LZF compressed buffer of this <see cref="Texture"/>.
        /// </summary>
        /// <returns>LZF compressed data buffer.</returns>
        protected byte[] GetCompressedBuffer()
		{
            return this._data;
		}

        /// <summary>
        /// Copies data buffer from one texture to another.
        /// </summary>
        /// <param name="from">Texture to copy data from.</param>
        /// <param name="to">Texture to copy data into.</param>
        protected static void CopyMemory(Texture from, Texture to)
		{
            to._decodedSize = from._decodedSize;
            to._data = new byte[from._data.Length];
            Array.Copy(from._data, to._data, to._data.Length);
		}

        #endregion
    }
}
