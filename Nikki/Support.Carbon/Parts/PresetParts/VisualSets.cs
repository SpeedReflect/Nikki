using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="VisualSets"/> used in preset rides.
	/// </summary>
	public class VisualSets : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string BasePaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string BasePaintGroup { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string BasePaintSwatch { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VinylSpecific { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VinylGeneric { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VectorVinyl { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string RimsPaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string RimsPaintGroup { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string RimsPaintSwatch { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VinylColor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalFront { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalRear { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new VisualSets();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.BasePaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.BasePaintGroup = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.BasePaintSwatch = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VinylSpecific = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VinylGeneric = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VectorVinyl = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.RimsPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.RimsPaintGroup = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.RimsPaintSwatch = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VinylColor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DecalFront = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DecalRear = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.BasePaintType.BinHash());
			bw.Write(this.BasePaintGroup.BinHash());
			bw.Write(this.BasePaintSwatch.BinHash());
			bw.Write(this.VinylSpecific.BinHash());
			bw.Write(this.VinylGeneric.BinHash());
			bw.Write(this.VectorVinyl.BinHash());
			bw.Write(this.RimsPaintType.BinHash());
			bw.Write(this.RimsPaintGroup.BinHash());
			bw.Write(this.RimsPaintSwatch.BinHash());
			bw.Write(this.VinylColor.BinHash());
			bw.Write(this.DecalFront.BinHash());
			bw.Write(this.DecalRear.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.BasePaintType);
			bw.WriteNullTermUTF8(this.BasePaintGroup);
			bw.WriteNullTermUTF8(this.BasePaintSwatch);
			bw.WriteNullTermUTF8(this.VinylSpecific);
			bw.WriteNullTermUTF8(this.VinylGeneric);
			bw.WriteNullTermUTF8(this.VectorVinyl);
			bw.WriteNullTermUTF8(this.RimsPaintType);
			bw.WriteNullTermUTF8(this.RimsPaintGroup);
			bw.WriteNullTermUTF8(this.RimsPaintSwatch);
			bw.WriteNullTermUTF8(this.VinylColor);
			bw.WriteNullTermUTF8(this.DecalFront);
			bw.WriteNullTermUTF8(this.DecalRear);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.BasePaintType = br.ReadNullTermUTF8();
			this.BasePaintGroup = br.ReadNullTermUTF8();
			this.BasePaintSwatch = br.ReadNullTermUTF8();
			this.VinylSpecific = br.ReadNullTermUTF8();
			this.VinylGeneric = br.ReadNullTermUTF8();
			this.VectorVinyl = br.ReadNullTermUTF8();
			this.RimsPaintType = br.ReadNullTermUTF8();
			this.RimsPaintGroup = br.ReadNullTermUTF8();
			this.RimsPaintSwatch = br.ReadNullTermUTF8();
			this.VinylColor = br.ReadNullTermUTF8();
			this.DecalFront = br.ReadNullTermUTF8();
			this.DecalRear = br.ReadNullTermUTF8();
		}
	}
}
