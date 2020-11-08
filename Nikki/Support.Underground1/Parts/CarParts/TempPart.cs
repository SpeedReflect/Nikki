using System.IO;
using Nikki.Reflection.Enum.PartID;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.CarParts
{
	/// <summary>
	/// Represents temporary car part that is used to build <see cref="RealCarPart"/>.
	/// </summary>
	public class TempPart
	{
		/// <summary>
		/// Debug name of the part.
		/// </summary>
		public string DebugName { get; set; }

		/// <summary>
		/// Hash of the car to which this part belongs to.
		/// </summary>
		public uint CarNameHash { get; set; }

		/// <summary>
		/// Hash of the part name.
		/// </summary>
		public uint PartNameHash { get; set; }

		/// <summary>
		/// Hash of the brand of this part.
		/// </summary>
		public uint BrandNameHash { get; set; }

		/// <summary>
		/// Group ID of the part.
		/// </summary>
		public PartUnderground1 CarPartGroupID { get; set; }

		/// <summary>
		/// Upgrade group ID of the part.
		/// </summary>
		public byte UpgradeGroupID { get; set; }

		/// <summary>
		/// Upgrade style of the part.
		/// </summary>
		public byte UpgradeStyle { get; set; }

		/// <summary>
		/// Padding align.
		/// </summary>
		public byte Padding { get; set; }

		/// <summary>
		/// Attribute offset of the part.
		/// </summary>
		public int AttribOffset { get; set; }

		/// <summary>
		/// First attribute to read from the offset.
		/// </summary>
		public int AttribStart { get; set; }

		/// <summary>
		/// Last attribute to read from the offset.
		/// </summary>
		public int AttribEnd { get; set; }

		/// <summary>
		/// Geometry Lod A hash of this part.
		/// </summary>
		public uint LodAHash { get; set; }

		/// <summary>
		/// Geometry Lod B hash of this part.
		/// </summary>
		public uint LodBHash { get; set; }

		/// <summary>
		/// Geometry Lod C hash of this part.
		/// </summary>
		public uint LodCHash { get; set; }

		/// <summary>
		/// Geometry Lod D hash of this part.
		/// </summary>
		public uint LodDHash { get; set; }

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
			var position = br.ReadUInt32();

			if (position < 0xFFFFFFFF)
			{

				str_reader.BaseStream.Position = position;
				this.DebugName = str_reader.ReadNullTermUTF8();

			}

			this.CarNameHash = br.ReadUInt32();
			this.PartNameHash = br.ReadUInt32();
			this.BrandNameHash = br.ReadUInt32();

			this.CarPartGroupID = br.ReadEnum<PartUnderground1>();
			this.UpgradeGroupID = br.ReadByte();
			this.UpgradeStyle = br.ReadByte();
			this.Padding = br.ReadByte();
			this.AttribOffset = br.ReadInt32();
			this.AttribStart = br.ReadInt32();
			this.AttribEnd = br.ReadInt32();

			this.LodAHash = br.ReadUInt32();
			this.LodBHash = br.ReadUInt32();
			this.LodCHash = br.ReadUInt32();
			this.LodDHash = br.ReadUInt32();
		}
	}
}
