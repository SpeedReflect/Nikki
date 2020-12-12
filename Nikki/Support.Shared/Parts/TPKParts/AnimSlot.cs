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
				this.BinKey = value.BinHash();
			}
		}

		/// <summary>
		/// Binary memory hash of the name.
		/// </summary>
		[Category("Main")]
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		public uint BinKey { get; set; }

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
		public List<FrameEntry> FrameTextures { get; }

		/// <summary>
		/// Initializes new instance of <see cref="AnimSlot"/>.
		/// </summary>
		public AnimSlot() => this.FrameTextures = new List<FrameEntry>();

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new AnimSlot()
			{
				FramesPerSecond = this.FramesPerSecond,
				TimeBase = this.TimeBase,
				Name = this.Name
			};

			foreach (var entry in this.FrameTextures)
			{

				result.FrameTextures.Add(new FrameEntry() { Name = entry.Name });

			}

			return result;
		}

		/// <summary>
		/// Clones values of another <see cref="SubPart"/>.
		/// </summary>
		/// <param name="other"><see cref="SubPart"/> to clone.</param>
		public override void CloneValuesFrom(SubPart other)
		{
			if (other is AnimSlot anim)
			{

				this.FramesPerSecond = anim.FramesPerSecond;
				this.TimeBase = anim.TimeBase;
				this.Name = anim.Name;
				this.FrameTextures.Capacity = anim.FrameTextures.Capacity;

				foreach (var entry in anim.FrameTextures)
				{

					this.FrameTextures.Add(new FrameEntry() { Name = entry.Name });

				}

			}
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			if (br.ReadEnum<BinBlockID>() != BinBlockID.TPK_AnimPart1) return;
			br.BaseStream.Position += 0xC; // I guess we trust size stated?

			this._name = br.ReadNullTermUTF8(0x10);
			this.BinKey = br.ReadUInt32();

			this.FrameTextures.Capacity = br.ReadByte();
			this.FramesPerSecond = br.ReadByte();
			this.TimeBase = br.ReadByte();
			br.BaseStream.Position += 0xD;

			while (br.ReadEnum<BinBlockID>() != BinBlockID.TPK_AnimPart2) { }
			var size = br.ReadInt32() / 0xC;

			for (int loop = 0; loop < size; ++loop)
			{

				var entry = new FrameEntry()
				{
					Name = br.ReadUInt32().BinString(LookupReturn.EMPTY)
				};

				this.FrameTextures.Add(entry);
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
			bw.WriteEnum(BinBlockID.TPK_AnimBlock);
			bw.Write(size);

			// Write header
			bw.WriteEnum(BinBlockID.TPK_AnimPart1);
			bw.Write(0x2C);
			bw.Write((long)0);
			bw.WriteNullTermUTF8(this._name, 0x10);
			bw.Write(this.BinKey);
			bw.Write((byte)this.FrameTextures.Count);
			bw.Write(this.FramesPerSecond);
			bw.Write(this.TimeBase);
			bw.WriteBytes(0, 0xD);

			// Write frames
			bw.WriteEnum(BinBlockID.TPK_AnimPart2);
			bw.Write(this.FrameTextures.Count * 0xC);

			for (int loop = 0; loop < this.FrameTextures.Count; ++loop)
			{

				bw.Write(this.FrameTextures[loop].BinKey);
				bw.Write((long)0);

			}
		}

		/// <summary>
		/// Name of the animation slot.
		/// </summary>
		/// <returns>Name of the slot as a string value.</returns>
		public override string ToString() => this.Name;
	}
}
