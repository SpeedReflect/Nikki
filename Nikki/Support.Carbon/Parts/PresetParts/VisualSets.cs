using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Carbon.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="VisualSets"/> used in preset rides.
	/// </summary>
	public class VisualSets : ASubPart
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
		public override ASubPart PlainCopy()
		{
			var result = new VisualSets();

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
			this.BasePaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.BasePaintGroup = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.BasePaintSwatch = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VinylSpecific = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VinylGeneric = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VectorVinyl = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.RimsPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.RimsPaintGroup = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.RimsPaintSwatch = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VinylColor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalFront = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalRear = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
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
	}
}
