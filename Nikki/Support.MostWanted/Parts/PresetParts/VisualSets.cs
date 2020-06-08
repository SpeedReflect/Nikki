using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



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
			this.VinylLayer = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.RimsPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VinylColor0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VinylColor1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VinylColor2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.VinylColor3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
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
	}
}
