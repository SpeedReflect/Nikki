using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Support.Undercover.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;

namespace Nikki.Support.Undercover.Parts.BoundParts
{
	/// <summary>
	/// <see cref="hkArray"/> is a structure for <see cref="Collision"/>.
	/// </summary>
	public class hkArray : SubPart
    {
        /// <summary>
		/// Size of this <see cref="hkArray"/>.
		/// </summary>
        [AccessModifiable()]
        public short Size;

        /// <summary>
		/// Capacity of this <see cref="hkArray"/>.
		/// </summary>
        [AccessModifiable()]
        public short Capacity;

        /// <summary>
		/// Flags of this <see cref="hkArray"/>.
		/// </summary>
        [AccessModifiable()]
        public byte Flags = 0xC0;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new hkArray()
			{
				Size = this.Size,
				Capacity= this.Capacity,
				Flags = this.Flags
			};

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="hkArray"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="hkArray"/> with.</param>
		public void Read(BinaryReader br)
		{
			br.BaseStream.Position += 0x4;
			this.Size = br.ReadInt16();
			br.BaseStream.Position += 0x2;
			this.Capacity = br.ReadInt16();
			br.BaseStream.Position += 0x1;
			this.Flags = br.ReadByte();
		}

		/// <summary>
		/// Assembles <see cref="hkArray"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="hkArray"/> with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(0);
			bw.Write((int)this.Size);
			bw.Write(this.Capacity);
			bw.Write((byte)0);
			bw.Write(this.Flags);
		}

		/// <summary>
		/// Returns hkArray string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "hkArray";
	}
}
