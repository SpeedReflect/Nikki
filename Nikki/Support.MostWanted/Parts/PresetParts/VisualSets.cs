using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Parts.PresetParts
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
		public string VinylLayer { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string RimsPaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VinylColor0 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VinylColor1 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VinylColor2 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string VinylColor3 { get; set; } = String.Empty;

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
			this.VinylLayer = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.RimsPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VinylColor0 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VinylColor1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VinylColor2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.VinylColor3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.BasePaintType.BinHash());
			bw.Write(this.VinylLayer.BinHash());
			bw.Write(this.RimsPaintType.BinHash());
			bw.Write(this.VinylColor0.BinHash());
			bw.Write(this.VinylColor1.BinHash());
			bw.Write(this.VinylColor2.BinHash());
			bw.Write(this.VinylColor3.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.BasePaintType);
			bw.WriteNullTermUTF8(this.VinylLayer);
			bw.WriteNullTermUTF8(this.RimsPaintType);
			bw.WriteNullTermUTF8(this.VinylColor0);
			bw.WriteNullTermUTF8(this.VinylColor1);
			bw.WriteNullTermUTF8(this.VinylColor2);
			bw.WriteNullTermUTF8(this.VinylColor3);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.BasePaintType = br.ReadNullTermUTF8();
			this.VinylLayer = br.ReadNullTermUTF8();
			this.RimsPaintType = br.ReadNullTermUTF8();
			this.VinylColor0 = br.ReadNullTermUTF8();
			this.VinylColor1 = br.ReadNullTermUTF8();
			this.VinylColor2 = br.ReadNullTermUTF8();
			this.VinylColor3 = br.ReadNullTermUTF8();
		}
	}
}
