using System.IO;
using Nikki.Reflection.Enum.PartID;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Parts.CarParts
{
	/// <summary>
	/// Represents temporary car part that is used to build <see cref="RealCarPart"/>.
	/// </summary>
	public class TempPart
	{
		/// <summary>
		/// Hash of the part name.
		/// </summary>
		public uint PartNameHash { get; set; }

		/// <summary>
		/// Group ID of the car.
		/// </summary>
		public PartMostWanted CarPartGroupID { get; set; }

		/// <summary>
		/// Unknown yet value.
		/// </summary>
		public ushort UpgradeGroupID { get; set; }

		/// <summary>
		/// Index of the model of the car.
		/// </summary>
		public byte Index { get; set; }

		/// <summary>
		/// Debug name of the part.
		/// </summary>
		public string DebugName { get; set; }

		/// <summary>
		/// Attribute offset of the part.
		/// </summary>
		public ushort AttribOffset { get; set; }

		/// <summary>
		/// Struct offset of the part.
		/// </summary>
		public ushort StructOffset { get; set; }

		/// <summary>
		/// Disassembles array of bytes into <see cref="TempPart"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public void Disassemble(BinaryReader br, BinaryReader str_reader)
		{
			this.PartNameHash = br.ReadUInt32();
			this.CarPartGroupID = br.ReadEnum<PartMostWanted>();
			this.UpgradeGroupID = br.ReadUInt16();
			this.Index = br.ReadByte();

			str_reader.BaseStream.Position = br.ReadUInt16() * 4;
			this.DebugName = str_reader.ReadNullTermUTF8();

			this.AttribOffset = br.ReadUInt16();
			this.StructOffset = br.ReadUInt16();
		}
	}
}
