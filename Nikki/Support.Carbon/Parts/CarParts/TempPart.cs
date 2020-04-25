using System.IO;



namespace Nikki.Support.Carbon.Parts.CarParts
{
	/// <summary>
	/// Represents temporary car part that is used to build <see cref="RealCarPart"/>.
	/// </summary>
	public class TempPart
	{
		/// <summary>
		/// Padding byte
		/// </summary>
		public byte Padding { get; set; }

		/// <summary>
		/// Index of the model of the car.
		/// </summary>
		public byte Index { get; set; }

		/// <summary>
		/// Attribute offset of the part.
		/// </summary>
		public ushort AttribOffset { get; set; }

		/// <summary>
		/// Disassembles array of bytes into <see cref="TempPart"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		public void Disassemble(BinaryReader br)
		{
			this.Padding = br.ReadByte();
			this.Index = br.ReadByte();
			this.AttribOffset = br.ReadUInt16();
		}
	}
}
