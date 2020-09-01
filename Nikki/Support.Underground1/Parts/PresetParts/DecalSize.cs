using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="DecalSize"/> used in preset rides.
	/// </summary>
	public class DecalSize : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalHood { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalFrontWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalRearWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalLeftDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalRightDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalLeftQuarter { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalRightQuarter { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideLeftDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideRightDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideLeftQuarter { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideRightQuarter { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new DecalSize();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			br.BaseStream.Position += 4;
			this.DecalHood = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalFrontWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalRearWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalLeftDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalRightDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalLeftQuarter = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalRightQuarter = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalWideLeftDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalWideRightDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalWideLeftQuarter = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.DecalWideRightQuarter = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write((int)0x13);
			bw.Write(this.DecalHood.BinHash());
			bw.Write((int)0x14);
			bw.Write(this.DecalFrontWindow.BinHash());
			bw.Write((int)0x15);
			bw.Write(this.DecalRearWindow.BinHash());
			bw.Write((int)0x16);
			bw.Write(this.DecalLeftDoor.BinHash());
			bw.Write((int)0x17);
			bw.Write(this.DecalRightDoor.BinHash());
			bw.Write((int)0x18);
			bw.Write(this.DecalLeftQuarter.BinHash());
			bw.Write((int)0x19);
			bw.Write(this.DecalRightQuarter.BinHash());
			bw.Write((int)0x1A);
			bw.Write(this.DecalWideLeftDoor.BinHash());
			bw.Write((int)0x1B);
			bw.Write(this.DecalWideRightDoor.BinHash());
			bw.Write((int)0x1C);
			bw.Write(this.DecalWideLeftQuarter.BinHash());
			bw.Write((int)0x1D);
			bw.Write(this.DecalWideRightQuarter.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.DecalHood);
			bw.WriteNullTermUTF8(this.DecalFrontWindow);
			bw.WriteNullTermUTF8(this.DecalRearWindow);
			bw.WriteNullTermUTF8(this.DecalLeftDoor);
			bw.WriteNullTermUTF8(this.DecalRightDoor);
			bw.WriteNullTermUTF8(this.DecalLeftQuarter);
			bw.WriteNullTermUTF8(this.DecalRightQuarter);
			bw.WriteNullTermUTF8(this.DecalWideLeftDoor);
			bw.WriteNullTermUTF8(this.DecalWideRightDoor);
			bw.WriteNullTermUTF8(this.DecalWideLeftQuarter);
			bw.WriteNullTermUTF8(this.DecalWideRightQuarter);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.DecalHood = br.ReadNullTermUTF8();
			this.DecalFrontWindow = br.ReadNullTermUTF8();
			this.DecalRearWindow = br.ReadNullTermUTF8();
			this.DecalLeftDoor = br.ReadNullTermUTF8();
			this.DecalRightDoor = br.ReadNullTermUTF8();
			this.DecalLeftQuarter = br.ReadNullTermUTF8();
			this.DecalRightQuarter = br.ReadNullTermUTF8();
			this.DecalWideLeftDoor = br.ReadNullTermUTF8();
			this.DecalWideRightDoor = br.ReadNullTermUTF8();
			this.DecalWideLeftQuarter = br.ReadNullTermUTF8();
			this.DecalWideRightQuarter = br.ReadNullTermUTF8();
		}
	}
}
