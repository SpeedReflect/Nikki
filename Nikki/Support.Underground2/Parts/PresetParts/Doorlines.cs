using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="Doorlines"/> used in preset rides.
	/// </summary>
	public class Doorlines : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DoorLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DoorRight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DoorPanelLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DoorPanelRight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DoorSillLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DoorSillRight { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Doorlines();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.DoorLeft = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DoorRight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DoorPanelLeft = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DoorPanelRight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DoorSillLeft = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DoorSillRight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.DoorLeft.BinHash());
			bw.Write(this.DoorRight.BinHash());
			bw.Write(this.DoorPanelLeft.BinHash());
			bw.Write(this.DoorPanelRight.BinHash());
			bw.Write(this.DoorSillLeft.BinHash());
			bw.Write(this.DoorSillRight.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.DoorLeft);
			bw.WriteNullTermUTF8(this.DoorRight);
			bw.WriteNullTermUTF8(this.DoorPanelLeft);
			bw.WriteNullTermUTF8(this.DoorPanelRight);
			bw.WriteNullTermUTF8(this.DoorSillLeft);
			bw.WriteNullTermUTF8(this.DoorSillRight);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.DoorLeft = br.ReadNullTermUTF8();
			this.DoorRight = br.ReadNullTermUTF8();
			this.DoorPanelLeft = br.ReadNullTermUTF8();
			this.DoorPanelRight = br.ReadNullTermUTF8();
			this.DoorSillLeft = br.ReadNullTermUTF8();
			this.DoorSillRight = br.ReadNullTermUTF8();
		}
	}
}
