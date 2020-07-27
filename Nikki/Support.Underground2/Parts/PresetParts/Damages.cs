using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="Damages"/> used in preset rides.
	/// </summary>
	public class Damages : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageFront { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRear { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageTop { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Damages();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.DamageFront = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRear = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageLeft = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageTop = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.DamageFront.BinHash());
			bw.Write(this.DamageRear.BinHash());
			bw.Write(this.DamageLeft.BinHash());
			bw.Write(this.DamageRight.BinHash());
			bw.Write(this.DamageTop.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.DamageFront);
			bw.WriteNullTermUTF8(this.DamageRear);
			bw.WriteNullTermUTF8(this.DamageLeft);
			bw.WriteNullTermUTF8(this.DamageRight);
			bw.WriteNullTermUTF8(this.DamageTop);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.DamageFront = br.ReadNullTermUTF8();
			this.DamageRear = br.ReadNullTermUTF8();
			this.DamageLeft = br.ReadNullTermUTF8();
			this.DamageRight = br.ReadNullTermUTF8();
			this.DamageTop = br.ReadNullTermUTF8();
		}
	}
}
