using System;
using System.IO;
using Nikki.Reflection;
using Nikki.Reflection.Enum;
using Nikki.Support.Shared.Class;
using CoreExtensions.IO;



namespace Nikki.Utils.DDS
{
	/// <summary>
	/// A class with DDS reading and writing options.
	/// </summary>
	public class Generator
	{
		#region Fields

		private byte _mode; // 1 = write | 2 = read
		private byte[] _stream;
		private bool _no_palette = false;
		private TextureCompressionType _prev;

		#endregion

		#region Properties

		/// <summary>
		/// DDS data of the texture.
		/// </summary>
		public byte[] Buffer { get; private set; }

		/// <summary>
		/// <see cref="TextureCompressionType"/> type of the texture.
		/// </summary>
		public TextureCompressionType Compression { get; private set; }

		/// <summary>
		/// Area of the texture.
		/// </summary>
		public int Area { get; private set; }

		/// <summary>
		/// Height of the texture.
		/// </summary>
		public int Height { get; private set; }

		/// <summary>
		/// Width of the texture.
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// Number of mipmaps in the texture.
		/// </summary>
		public int MipMaps { get; private set; }

		/// <summary>
		/// Size of the texture, in bytes.
		/// </summary>
		public int Size { get; private set; }

		/// <summary>
		/// Palette size of the texture, in bytes.
		/// </summary>
		public int PaletteSize { get; private set; }

		/// <summary>
		/// True if settings can be read and generator is based on DDS data passed; otherwise, false.
		/// </summary>
		public bool CanRead => this._mode == 1;

		/// <summary>
		/// True if DDS data can be written based on <see cref="Texture"/> passed; otherwise, false.
		/// </summary>
		public bool CanWrite => this._mode == 2;

		#endregion

		#region Helpers

		private bool IsCompressed() => this.Compression switch
		{
			TextureCompressionType.TEXCOMP_DXTC1 => true,
			TextureCompressionType.TEXCOMP_DXTC3 => true,
			TextureCompressionType.TEXCOMP_DXTC5 => true,
			_ => false
		};

		private int PitchLinearSize()
		{
			int result;

			switch (this.Compression)
			{
				case TextureCompressionType.TEXCOMP_DXTC1:
					result = (1 > (this.Width + 3) / 4) ? 1 : (this.Width + 3) / 4;
					result *= (1 > (this.Height + 3) / 4) ? 1 : (this.Height + 3) / 4;
					result *= 8;
					break;

				case TextureCompressionType.TEXCOMP_DXTC3:
				case TextureCompressionType.TEXCOMP_DXTC5:
					result = (1 > (this.Width + 3) / 4) ? 1 : (this.Width + 3) / 4;
					result *= (1 > (this.Height + 3) / 4) ? 1 : (this.Height + 3) / 4;
					result *= 16;
					break;

				case TextureCompressionType.TEXCOMP_4BIT:
				case TextureCompressionType.TEXCOMP_4BIT_IA8:
				case TextureCompressionType.TEXCOMP_4BIT_RGB16_A8:
				case TextureCompressionType.TEXCOMP_4BIT_RGB24_A8:
					result = this.Width * 4 + 7;
					result /= 8;
					break;

				case TextureCompressionType.TEXCOMP_8BIT:
				case TextureCompressionType.TEXCOMP_8BIT_16:
				case TextureCompressionType.TEXCOMP_8BIT_64:
				case TextureCompressionType.TEXCOMP_8BIT_IA8:
				case TextureCompressionType.TEXCOMP_8BIT_RGB16_A8:
				case TextureCompressionType.TEXCOMP_8BIT_RGB24_A8:
					result = this.Width * 8 + 7;
					result /= 8;
					break;

				case TextureCompressionType.TEXCOMP_16BIT:
				case TextureCompressionType.TEXCOMP_16BIT_1555:
				case TextureCompressionType.TEXCOMP_16BIT_3555:
				case TextureCompressionType.TEXCOMP_16BIT_565:
					result = this.Width * 16 + 7;
					result /= 8;
					break;

				case TextureCompressionType.TEXCOMP_24BIT:
					result = this.Width * 24 + 7;
					result /= 8;
					break;

				case TextureCompressionType.TEXCOMP_32BIT:
					result = this.Width * 32 + 7;
					result /= 8;
					break;

				default:
					result = this.Width * 32 + 7;
					result /= 8;
					break;

			}

			return result;
		}

		private void WritePixelFormat(BinaryWriter bw)
		{
			DDS_PIXELFORMAT format = new DDS_PIXELFORMAT();

			switch (this.Compression)
			{
				case TextureCompressionType.TEXCOMP_DXTC1:
					DDS_CONST.DDSPF_DXT1(format);
					break;

				case TextureCompressionType.TEXCOMP_DXTC3:
					DDS_CONST.DDSPF_DXT3(format);
					break;

				case TextureCompressionType.TEXCOMP_DXTC5:
					DDS_CONST.DDSPF_DXT5(format);
					break;

				case TextureCompressionType.TEXCOMP_4BIT:
					DDS_CONST.DDSPF_PAL4(format);
					break;

				case TextureCompressionType.TEXCOMP_4BIT_IA8:
				case TextureCompressionType.TEXCOMP_4BIT_RGB16_A8:
				case TextureCompressionType.TEXCOMP_4BIT_RGB24_A8:
					DDS_CONST.DDSPF_PAL4A(format);
					break;

				case TextureCompressionType.TEXCOMP_8BIT:
				case TextureCompressionType.TEXCOMP_8BIT_16:
				case TextureCompressionType.TEXCOMP_8BIT_64:
					DDS_CONST.DDSPF_PAL8(format);
					break;

				case TextureCompressionType.TEXCOMP_8BIT_IA8:
				case TextureCompressionType.TEXCOMP_8BIT_RGB16_A8:
				case TextureCompressionType.TEXCOMP_8BIT_RGB24_A8:
					DDS_CONST.DDSPF_PAL8A(format);
					break;

				case TextureCompressionType.TEXCOMP_32BIT:
					DDS_CONST.DDSPF_A8R8G8B8(format);
					break;

				default:
					DDS_CONST.DDSPF_A8R8G8B8(format);
					break;

			}

			bw.Write(format.dwSize);
			bw.Write(format.dwFlags);
			bw.Write(format.dwFourCC);
			bw.Write(format.dwRGBBitCount);
			bw.Write(format.dwRBitMask);
			bw.Write(format.dwGBitMask);
			bw.Write(format.dwBBitMask);
			bw.Write(format.dwABitMask);
		}

		private void ReadCompression(uint flag, uint code)
		{
			switch (flag)
			{
				case (uint)DDS_TYPE.FOURCC:
					this.Compression = code switch
					{
						EAComp.DXT1_32 => TextureCompressionType.TEXCOMP_DXTC1,
						EAComp.DXT3_32 => TextureCompressionType.TEXCOMP_DXTC3,
						EAComp.DXT5_32 => TextureCompressionType.TEXCOMP_DXTC5,
						_ => throw new NotSupportedException("Not supported texture compression")
					};
					this.PaletteSize = 0;
					return;

				case (uint)DDS_TYPE.PAL4:
					this.Compression = TextureCompressionType.TEXCOMP_4BIT;
					this.PaletteSize = 0x40;
					return;

				case (uint)DDS_TYPE.PAL4A:
					this.Compression = TextureCompressionType.TEXCOMP_4BIT_IA8;
					this.PaletteSize = 0x40;
					return;

				case (uint)DDS_TYPE.PAL8:
					this.Compression = TextureCompressionType.TEXCOMP_8BIT;
					this.PaletteSize = 0x400;
					return;

				case (uint)DDS_TYPE.PAL8A:
					this.Compression = TextureCompressionType.TEXCOMP_8BIT_IA8;
					this.PaletteSize = 0x400;
					return;

				case (uint)DDS_TYPE.RGB:
					this.Compression = TextureCompressionType.TEXCOMP_24BIT;
					this.PaletteSize = 0;
					return;

				case (uint)DDS_TYPE.RGBA:
					this.Compression = TextureCompressionType.TEXCOMP_32BIT;
					this.PaletteSize = 0;
					return;

				default:
					throw new NotSupportedException("Not supported texture compression");

			}
		}

		private void ReadArea()
		{
			this.Area = this.Compression switch
			{
				TextureCompressionType.TEXCOMP_4BIT => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_4BIT_IA8 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_4BIT_RGB16_A8 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_4BIT_RGB24_A8 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_8BIT => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_8BIT_16 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_8BIT_64 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_8BIT_IA8 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_8BIT_RGB16_A8 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_8BIT_RGB24_A8 => this.Width * this.Height,
				TextureCompressionType.TEXCOMP_32BIT => this.Width * this.Height * 4,
				_ => this.FlipToBase(this.Size)
			};
		}

		private int FlipToBase(int size)
		{
			uint x = (uint)size;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x -= x >> 1;
			return (int)x;
		}

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="Generator"/> with ability to read settings of 
		/// DDS data buffer passed.
		/// </summary>
		/// <param name="buffer">DDS data buffer to read.</param>
		/// <param name="comp_to_palette">True if attempt to compress RGBA 32 bit DDS data 
		/// to 8 bit one; </param>
		public Generator(byte[] buffer, bool comp_to_palette)
		{
			this._mode = 1;
			this._no_palette = !comp_to_palette;
			this._stream = buffer;
		}

		/// <summary>
		/// Initializes new instance of <see cref="Generator"/> with ability to write DDS data 
		/// buffer based on <see cref="Texture"/> passed.
		/// </summary>
		/// <param name="texture"><see cref="Texture"/> to write data of.</param>
		/// <param name="make_no_palette">True if palette should be decompressed into 
		/// 32 bpp DDS; false otherwise.</param>
		public Generator(Texture texture, bool make_no_palette)
		{
			this._mode = 2;
			this._no_palette = make_no_palette;
			this.Buffer = texture.Data;
			this.Height = texture.Height;
			this.Width = texture.Width;
			this.MipMaps = texture.Mipmaps;

			if (make_no_palette)
			{

				this._no_palette = texture.HasPalette;
				this._prev = texture.Compression;
				this.Compression = texture.HasPalette
					? TextureCompressionType.TEXCOMP_32BIT
					: texture.Compression;

			}
			else
			{

				this.Compression = texture.Compression;

			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets DDS data buffer for a texture passed.
		/// </summary>
		/// <returns>DDS data buffer.</returns>
		public byte[] GetDDSTexture()
		{
			if (!this.CanWrite)
			{

				throw new InvalidOperationException("Generator is not initialized for reading data");

			}

			return this._no_palette ? this.GetDDSTexWithLimits() : this.GetDDSTexWithoutLimits();
		}
	
		/// <summary>
		/// Gets settings from DDS data buffer passed.
		/// </summary>
		public void GetDDSSettings()
		{
			if (!this.CanRead)
			{

				throw new InvalidOperationException("Generator is not initialized for writing data");

			}

			using (var ms = new MemoryStream(this._stream))
			using (var br = new BinaryReader(ms))
			{

				br.BaseStream.Position += 0xC; // skip ID, size and flags
				this.Height = br.ReadInt32();
				this.Width = br.ReadInt32();
				br.BaseStream.Position += 0x8; // skip pitch/linear size and depth
				this.MipMaps = br.ReadInt32();
				if (this.MipMaps == 0) this.MipMaps = 1; // in case it is mipmap-less texture

				br.BaseStream.Position += 0x30; // skips padding

				var flag = br.ReadUInt32();
				var code = br.ReadUInt32();
				this.ReadCompression(flag, code);

			}

			if (this._no_palette) this.GetDDSSetWithoutLimits();
			else this.GetDDSSetWithLimits();

			this.ReadArea();
		}

		private byte[] GetDDSTexWithoutLimits()
		{
			var flags = DDS_HEADER_FLAGS.TEXTURE | DDS_HEADER_FLAGS.MIPMAP;
			flags |= this.IsCompressed() ? DDS_HEADER_FLAGS.LINEARSIZE : DDS_HEADER_FLAGS.PITCH;

			var array = new byte[this.Buffer.Length + 0x80];

			using (var ms = new MemoryStream(array))
			using (var bw = new BinaryWriter(ms))
			{

				bw.Write(DDS_MAIN.MAGIC);
				bw.Write(0x7C); // size = 0x7C
				bw.WriteEnum(flags);
				bw.Write(this.Height);
				bw.Write(this.Width);
				bw.Write(this.PitchLinearSize());
				bw.Write(1); // depth = 1
				bw.Write(this.MipMaps);

				bw.WriteBytes(0, 0x2C);
				this.WritePixelFormat(bw);

				bw.WriteEnum(DDS_SURFACE.SURFACE_FLAGS_ALL);
				bw.WriteBytes(0, 0x10);

			}

			Array.Copy(this.Buffer, 0, array, 0x80, this.Buffer.Length);
			return array;
		}

		private byte[] GetDDSTexWithLimits()
		{
			switch (this._prev)
			{
				case TextureCompressionType.TEXCOMP_4BIT:
				case TextureCompressionType.TEXCOMP_4BIT_IA8:
				case TextureCompressionType.TEXCOMP_4BIT_RGB16_A8:
				case TextureCompressionType.TEXCOMP_4BIT_RGB24_A8:
					this.Buffer = Palette.P4toRGBA(this.Buffer);
					break;

				case TextureCompressionType.TEXCOMP_8BIT:
				case TextureCompressionType.TEXCOMP_8BIT_16:
				case TextureCompressionType.TEXCOMP_8BIT_64:
				case TextureCompressionType.TEXCOMP_8BIT_IA8:
				case TextureCompressionType.TEXCOMP_8BIT_RGB16_A8:
				case TextureCompressionType.TEXCOMP_8BIT_RGB24_A8:
					this.Buffer = Palette.P8toRGBA(this.Buffer);
					break;

				default:
					throw new NotSupportedException("Unable to convert palette to RGBA format");

			}

			return this.GetDDSTexWithoutLimits();
		}

		private void GetDDSSetWithoutLimits()
		{
			var length = this._stream.Length - 0x80;
			this.Buffer = new byte[length];
			Array.Copy(this._stream, 0x80, this.Buffer, 0, length);
			this.Size = length - this.PaletteSize;
		}

		private void GetDDSSetWithLimits()
		{
			this.GetDDSSetWithoutLimits();

			if (this.Compression == TextureCompressionType.TEXCOMP_32BIT)
			{

				var array = Palette.RGBAtoP8(this.Buffer);
				if (array == null) return;

				this.Buffer = array;
				this.Compression = TextureCompressionType.TEXCOMP_8BIT;
				this.PaletteSize = 0x400;
				this.Size = array.Length - 0x400;

			}
		}

		#endregion
	}
}
