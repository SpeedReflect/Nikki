using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="Doorlines"/> used in preset rides.
	/// </summary>
	public class Doorlines : ASubPart
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
		public override ASubPart PlainCopy()
		{
			var result = new Doorlines();

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
			this.DoorLeft = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DoorRight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DoorPanelLeft = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DoorPanelRight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DoorSillLeft = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DoorSillRight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
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
	}
}
