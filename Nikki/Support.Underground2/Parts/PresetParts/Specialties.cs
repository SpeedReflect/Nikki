using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="Specialties"/> used in preset rides.
	/// </summary>
	public class Specialties : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string NeonBody { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string NeonEngine { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string NeonCabin { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string NeonTrunk { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string CabinNeonStyle { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string HeadlightBulb { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DoorStyle { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string Hydraulics { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string NOSPurge { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Specialties();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.NeonBody = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.NeonEngine = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.NeonCabin = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.NeonTrunk = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.CabinNeonStyle = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.HeadlightBulb = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DoorStyle = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.Hydraulics = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.NOSPurge = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.NeonBody.BinHash());
			bw.Write(this.NeonEngine.BinHash());
			bw.Write(this.NeonCabin.BinHash());
			bw.Write(this.NeonTrunk.BinHash());
			bw.Write(this.CabinNeonStyle.BinHash());
			bw.Write(this.HeadlightBulb.BinHash());
			bw.Write(this.DoorStyle.BinHash());
			bw.Write(this.Hydraulics.BinHash());
			bw.Write(this.NOSPurge.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.NeonBody);
			bw.WriteNullTermUTF8(this.NeonEngine);
			bw.WriteNullTermUTF8(this.NeonCabin);
			bw.WriteNullTermUTF8(this.NeonTrunk);
			bw.WriteNullTermUTF8(this.CabinNeonStyle);
			bw.WriteNullTermUTF8(this.HeadlightBulb);
			bw.WriteNullTermUTF8(this.DoorStyle);
			bw.WriteNullTermUTF8(this.Hydraulics);
			bw.WriteNullTermUTF8(this.NOSPurge);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.NeonBody = br.ReadNullTermUTF8();
			this.NeonEngine = br.ReadNullTermUTF8();
			this.NeonCabin = br.ReadNullTermUTF8();
			this.NeonTrunk = br.ReadNullTermUTF8();
			this.CabinNeonStyle = br.ReadNullTermUTF8();
			this.HeadlightBulb = br.ReadNullTermUTF8();
			this.DoorStyle = br.ReadNullTermUTF8();
			this.Hydraulics = br.ReadNullTermUTF8();
			this.NOSPurge = br.ReadNullTermUTF8();
		}
	}
}
