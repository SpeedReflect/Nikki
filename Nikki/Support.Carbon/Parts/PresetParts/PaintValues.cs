using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="PaintValues"/> used in preset rides.
	/// </summary>
	public class PaintValues : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public eBoolean IsCarbonStyle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string PaintGroup { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string PaintSwatch { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Saturation { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Brightness { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PaintValues()
			{
				IsCarbonStyle = this.IsCarbonStyle,
				PaintGroup = this.PaintGroup,
				PaintSwatch = this.PaintSwatch,
				Saturation = this.Saturation,
				Brightness = this.Brightness
			};

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.IsCarbonStyle = br.ReadInt32() == 0 ? eBoolean.False : eBoolean.True;
			this.PaintGroup = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PaintSwatch = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.Saturation = br.ReadSingle();
			this.Brightness = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.IsCarbonStyle == eBoolean.True ? (int)1 : (int)0);
			bw.Write(this.PaintGroup.BinHash());
			bw.Write(this.PaintSwatch.BinHash());
			bw.Write(this.Saturation);
			bw.Write(this.Brightness);
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteEnum(this.IsCarbonStyle);
			bw.WriteNullTermUTF8(this.PaintGroup);
			bw.WriteNullTermUTF8(this.PaintSwatch);
			bw.Write(this.Saturation);
			bw.Write(this.Brightness);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.IsCarbonStyle = br.ReadEnum<eBoolean>();
			this.PaintGroup = br.ReadNullTermUTF8();
			this.PaintSwatch = br.ReadNullTermUTF8();
			this.Saturation = br.ReadSingle();
			this.Brightness = br.ReadSingle();
		}
	}
}
