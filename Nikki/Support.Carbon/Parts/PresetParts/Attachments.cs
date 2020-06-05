using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Carbon.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="Attachments"/> used in preset rides.
	/// </summary>
	public class Attachments : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment0 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment1 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment2 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment3 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment4 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment5 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment6 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment7 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment8 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment9 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment10 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment11 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment12 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment13 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment14 { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Attachment15 { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Attachments();

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
			this.Attachment0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment4 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment5 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment6 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment7 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment8 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment9 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment10 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment11 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment12 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment13 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment14 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Attachment15 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Attachment0.BinHash());
			bw.Write(this.Attachment1.BinHash());
			bw.Write(this.Attachment2.BinHash());
			bw.Write(this.Attachment3.BinHash());
			bw.Write(this.Attachment4.BinHash());
			bw.Write(this.Attachment5.BinHash());
			bw.Write(this.Attachment6.BinHash());
			bw.Write(this.Attachment7.BinHash());
			bw.Write(this.Attachment8.BinHash());
			bw.Write(this.Attachment9.BinHash());
			bw.Write(this.Attachment10.BinHash());
			bw.Write(this.Attachment11.BinHash());
			bw.Write(this.Attachment12.BinHash());
			bw.Write(this.Attachment13.BinHash());
			bw.Write(this.Attachment14.BinHash());
			bw.Write(this.Attachment15.BinHash());
		}
	}
}
