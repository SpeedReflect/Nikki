using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="ZeroDamage"/> used in preset rides.
	/// </summary>
	public class ZeroDamage : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageFront { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageFrontLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageFrontRight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageRear { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageRearLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageRearRight { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new ZeroDamage();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.ZeroDamageFront = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.ZeroDamageFrontLeft = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.ZeroDamageFrontRight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.ZeroDamageRear = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.ZeroDamageRearLeft = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.ZeroDamageRearRight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.ZeroDamageFront.BinHash());
			bw.Write(this.ZeroDamageFrontLeft.BinHash());
			bw.Write(this.ZeroDamageFrontRight.BinHash());
			bw.Write(this.ZeroDamageRear.BinHash());
			bw.Write(this.ZeroDamageRearLeft.BinHash());
			bw.Write(this.ZeroDamageRearRight.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.ZeroDamageFront);
			bw.WriteNullTermUTF8(this.ZeroDamageFrontLeft);
			bw.WriteNullTermUTF8(this.ZeroDamageFrontRight);
			bw.WriteNullTermUTF8(this.ZeroDamageRear);
			bw.WriteNullTermUTF8(this.ZeroDamageRearLeft);
			bw.WriteNullTermUTF8(this.ZeroDamageRearRight);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.ZeroDamageFront = br.ReadNullTermUTF8();
			this.ZeroDamageFrontLeft = br.ReadNullTermUTF8();
			this.ZeroDamageFrontRight = br.ReadNullTermUTF8();
			this.ZeroDamageRear = br.ReadNullTermUTF8();
			this.ZeroDamageRearLeft = br.ReadNullTermUTF8();
			this.ZeroDamageRearRight = br.ReadNullTermUTF8();
		}
	}
}
