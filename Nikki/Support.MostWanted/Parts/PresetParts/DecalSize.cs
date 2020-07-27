using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Parts.PresetParts
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
			this.DecalFrontWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DecalRearWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DecalLeftDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DecalRightDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DecalLeftQuarter = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DecalRightQuarter = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.DecalFrontWindow.BinHash());
			bw.Write(this.DecalRearWindow.BinHash());
			bw.Write(this.DecalLeftDoor.BinHash());
			bw.Write(this.DecalRightDoor.BinHash());
			bw.Write(this.DecalLeftQuarter.BinHash());
			bw.Write(this.DecalRightQuarter.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.DecalFrontWindow);
			bw.WriteNullTermUTF8(this.DecalRearWindow);
			bw.WriteNullTermUTF8(this.DecalLeftDoor);
			bw.WriteNullTermUTF8(this.DecalRightDoor);
			bw.WriteNullTermUTF8(this.DecalLeftQuarter);
			bw.WriteNullTermUTF8(this.DecalRightQuarter);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.DecalFrontWindow = br.ReadNullTermUTF8();
			this.DecalRearWindow = br.ReadNullTermUTF8();
			this.DecalLeftDoor = br.ReadNullTermUTF8();
			this.DecalRightDoor = br.ReadNullTermUTF8();
			this.DecalLeftQuarter = br.ReadNullTermUTF8();
			this.DecalRightQuarter = br.ReadNullTermUTF8();
		}
	}
}
