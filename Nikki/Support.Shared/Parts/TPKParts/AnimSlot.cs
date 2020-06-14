using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Class;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Shared.Parts.TPKParts
{
	/// <summary>
	/// A unit animation block for <see cref="TPKBlock"/>.
	/// </summary>
	public class AnimSlot : SubPart
	{
		private string _name = String.Empty;
		private uint _binkey;

		/// <summary>
		/// Name of this <see cref="AnimSlot"/>.
		/// </summary>
		[Category("Main")]
		public string Name
		{
			get => this._name;
			set
			{
				this._name = value;
				this._binkey = value.BinHash();
			}
		}

		/// <summary>
		/// Binary memory hash of the name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint BinKey => this._binkey;

		/// <summary>
		/// Number of frames shown per second.
		/// </summary>
		[Category("Primary")]
		public byte FramesPerSecond { get; set; }

		/// <summary>
		/// Time base of this <see cref="AnimSlot"/>.
		/// </summary>
		[Category("Primary")]
		public byte TimeBase { get; set; }
		
		/// <summary>
		/// Frame textures of this <see cref="AnimSlot"/>.
		/// </summary>
		[Category("Primary")]
		public List<string> FrameTextures { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="AnimSlot"/>.
		/// </summary>
		public AnimSlot() => this.FrameTextures = new List<string>();

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new AnimSlot();

			foreach (var property in this.GetType().GetProperties())
			{

				property.SetValue(result, property.GetValue(this));

			}

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			if (br.ReadEnum<eBlockID>() != eBlockID.TPK_AnimPart1) return;
			br.BaseStream.Position += 0xC; // I guess we trust size stated?

			this._name = br.ReadNullTermUTF8(0x10);
			this._binkey = br.ReadUInt32();

			this.FrameTextures.Capacity = br.ReadByte();
			this.FramesPerSecond = br.ReadByte();
			this.TimeBase = br.ReadByte();
			br.BaseStream.Position += 0xD;

			while (br.ReadEnum<eBlockID>() != eBlockID.TPK_AnimPart2) { }
			var size = br.ReadInt32() / 0xC;

			for (int loop = 0; loop < size; ++loop)
			{

				this.FrameTextures.Add(br.ReadUInt32().BinString(eLookupReturn.EMPTY));
				br.BaseStream.Position += 8;

			}
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			// Precalculate size
			var size = 0x3C + this.FrameTextures.Count * 0xC;
			bw.WriteEnum(eBlockID.TPK_AnimBlock);
			bw.Write(size);

			// Write header
			bw.WriteEnum(eBlockID.TPK_AnimPart1);
			bw.Write(0x2C);
			bw.Write((long)0);
			bw.WriteNullTermUTF8(this._name, 0x10);
			bw.Write(this._binkey);
			bw.Write((byte)this.FrameTextures.Count);
			bw.Write(this.FramesPerSecond);
			bw.Write(this.TimeBase);
			bw.WriteBytes(0xD);

			// Write frames
			bw.WriteEnum(eBlockID.TPK_AnimPart2);
			bw.Write(this.FrameTextures.Count * 0xC);

			for (int loop = 0; loop < this.FrameTextures.Count; ++loop)
			{

				bw.Write(this.FrameTextures[loop].BinHash());
				bw.Write((long)0);

			}
		}
	}
}
