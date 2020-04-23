using System.IO;
using System.Collections.Generic;
using System.Text;
using Nikki.Utils;



namespace Nikki.Support.MostWanted.Parts.CarParts
{
	public class TempPart
	{
		public uint PartNameHash { get; set; }

		public string PartName => this.PartNameHash.BinString(eLookupReturn.EMPTY);

		public byte CarPartID { get; set; }

		public ushort Unknown1 { get; set; }

		public byte Index { get; set; }

		public ushort CarPartOffset { get; set; }

		public ushort AttribOffset { get; set; }

		public ushort StructOffset { get; set; }

		public void Disassemble(BinaryReader br)
		{
			this.PartNameHash = br.ReadUInt32();
			this.CarPartID = br.ReadByte();
			this.Unknown1 = br.ReadUInt16();
			this.Index = br.ReadByte();
			this.CarPartOffset = br.ReadUInt16();
			this.AttribOffset = br.ReadUInt16();
			this.StructOffset = br.ReadUInt16();
		}

		public void Assemble(BinaryWriter br)
		{

		}

		public override string ToString() =>
			$"FE: {this.StructOffset:X4} | Unk: {this.AttribOffset:X4} | ID: {this.Index:X2} | Name: {this.PartName}";
	}
}
