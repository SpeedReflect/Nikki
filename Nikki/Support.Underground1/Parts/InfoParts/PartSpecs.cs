using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="PartSpecs"/> used in car configuration.
	/// </summary>
	public class PartSpecs : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x00 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x02 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x04 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x06 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x08 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x0A { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x0C { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x0E { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x10 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x14 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x16 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x18 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x1A { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x1C { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x1E { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x20 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x24 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x28 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x2A { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x2C { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x2E { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x30 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x32 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x34 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x38 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x3C { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x3E { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x40 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x44 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x48 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value0x4C { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x50 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Value0x52 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int Value0x54 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int Value0x58 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int Value0x5C { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int Value0x60 { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="PartSpecs"/>.
		/// </summary>
		public PartSpecs() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PartSpecs();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Value0x00 = br.ReadInt16();
			this.Value0x02 = br.ReadInt16();
			this.Value0x04 = br.ReadInt16();
			this.Value0x06 = br.ReadInt16();
			this.Value0x08 = br.ReadInt16();
			this.Value0x0A = br.ReadInt16();
			this.Value0x0C = br.ReadInt16();
			this.Value0x0E = br.ReadInt16();
			this.Value0x10 = br.ReadSingle();
			this.Value0x14 = br.ReadInt16();
			this.Value0x16 = br.ReadInt16();
			this.Value0x18 = br.ReadInt16();
			this.Value0x1A = br.ReadInt16();
			this.Value0x1C = br.ReadInt16();
			this.Value0x1E = br.ReadInt16();
			this.Value0x20 = br.ReadSingle();
			this.Value0x24 = br.ReadSingle();
			this.Value0x28 = br.ReadInt16();
			this.Value0x2A = br.ReadInt16();
			this.Value0x2C = br.ReadInt16();
			this.Value0x2E = br.ReadInt16();
			this.Value0x30 = br.ReadInt16();
			this.Value0x32 = br.ReadInt16();
			this.Value0x34 = br.ReadSingle();
			this.Value0x38 = br.ReadSingle();
			this.Value0x3C = br.ReadInt16();
			this.Value0x3E = br.ReadInt16();
			this.Value0x40 = br.ReadSingle();
			this.Value0x44 = br.ReadSingle();
			this.Value0x48 = br.ReadSingle();
			this.Value0x4C = br.ReadSingle();
			this.Value0x50 = br.ReadInt16();
			this.Value0x52 = br.ReadInt16();
			this.Value0x54 = br.ReadInt32();
			this.Value0x58 = br.ReadInt32();
			this.Value0x5C = br.ReadInt32();
			this.Value0x60 = br.ReadInt32();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Value0x00);
			bw.Write(this.Value0x02);
			bw.Write(this.Value0x04);
			bw.Write(this.Value0x06);
			bw.Write(this.Value0x08);
			bw.Write(this.Value0x0A);
			bw.Write(this.Value0x0C);
			bw.Write(this.Value0x0E);
			bw.Write(this.Value0x10);
			bw.Write(this.Value0x14);
			bw.Write(this.Value0x16);
			bw.Write(this.Value0x18);
			bw.Write(this.Value0x1A);
			bw.Write(this.Value0x1C);
			bw.Write(this.Value0x1E);
			bw.Write(this.Value0x20);
			bw.Write(this.Value0x24);
			bw.Write(this.Value0x28);
			bw.Write(this.Value0x2A);
			bw.Write(this.Value0x2C);
			bw.Write(this.Value0x2E);
			bw.Write(this.Value0x30);
			bw.Write(this.Value0x32);
			bw.Write(this.Value0x34);
			bw.Write(this.Value0x38);
			bw.Write(this.Value0x3C);
			bw.Write(this.Value0x3E);
			bw.Write(this.Value0x40);
			bw.Write(this.Value0x44);
			bw.Write(this.Value0x48);
			bw.Write(this.Value0x4C);
			bw.Write(this.Value0x50);
			bw.Write(this.Value0x52);
			bw.Write(this.Value0x54);
			bw.Write(this.Value0x58);
			bw.Write(this.Value0x5C);
			bw.Write(this.Value0x60);
		}
	}
}
