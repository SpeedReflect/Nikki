using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="Damages"/> used in preset rides.
	/// </summary>
	public class Damages : ASubPart, ICopyable<Damages>
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
		public Damages PlainCopy()
		{
			var result = new Damages();
			var ThisType = this.GetType();
			var ResultType = result.GetType();
			foreach (var ThisField in ThisType.GetProperties())
			{
				var ResultField = ResultType.GetProperty(ThisField.Name);
				ResultField.SetValue(result, ThisField.GetValue(this));
			}
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.DamageFront = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DamageRear = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DamageLeft = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DamageRight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DamageTop = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.DamageFront.BinHash());
			bw.Write(this.DamageRear.BinHash());
			bw.Write(this.DamageLeft.BinHash());
			bw.Write(this.DamageRight.BinHash());
			bw.Write(this.DamageTop.BinHash());
		}
	}
}
