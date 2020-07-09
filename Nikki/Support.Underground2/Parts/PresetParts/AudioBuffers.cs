using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="AudioBuffers"/> used in preset rides.
	/// </summary>
	public class AudioBuffers : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompSmall00 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompSmall01 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompMedium02 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompMedium03 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompLarge04 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompLarge05 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompSmall06 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompSmall07 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompSmall08 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompSmall09 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompMedium10 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string AudioCompMedium11 { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new AudioBuffers();

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
			this.AudioCompSmall00 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompSmall01 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompMedium02 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompMedium03 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompLarge04 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompLarge05 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompSmall06 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompSmall07 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompSmall08 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompSmall09 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompMedium10 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AudioCompMedium11 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.AudioCompSmall00.BinHash());
			bw.Write(this.AudioCompSmall01.BinHash());
			bw.Write(this.AudioCompMedium02.BinHash());
			bw.Write(this.AudioCompMedium03.BinHash());
			bw.Write(this.AudioCompLarge04.BinHash());
			bw.Write(this.AudioCompLarge05.BinHash());
			bw.Write(this.AudioCompSmall06.BinHash());
			bw.Write(this.AudioCompSmall07.BinHash());
			bw.Write(this.AudioCompSmall08.BinHash());
			bw.Write(this.AudioCompSmall09.BinHash());
			bw.Write(this.AudioCompMedium10.BinHash());
			bw.Write(this.AudioCompMedium11.BinHash());
		}
	}
}
