using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Support.Undercover.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;
using Nikki.Reflection.Enum;

namespace Nikki.Support.Undercover.Parts.BoundParts
{
	/// <summary>
	/// <see cref="VirtualFixUp"/> is a structure for <see cref="Collision"/>.
	/// </summary>
	public class VirtualFixUp : SubPart
	{
		/// <summary>
		/// Offset of this <see cref="VirtualFixUp"/>.
		/// </summary>
		public int fromOffset { get; set; }

		/// <summary>
		/// Class of this <see cref="VirtualFixUp"/>.
		/// </summary>
		public CollisionClass ClassID { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new VirtualFixUp()
			{
				fromOffset = this.fromOffset,
				ClassID = this.ClassID
			};

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="VirtualFixUp"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="VirtualFixUp"/> with.</param>
		public void Read(BinaryReader br)
		{
			this.fromOffset = br.ReadInt32();
			this.ClassID = (CollisionClass)br.ReadUInt32();
			
		}

		/// <summary>
		/// Assembles <see cref="VirtualFixUp"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="VirtualFixUp"/> with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.fromOffset);
			bw.Write((uint)this.ClassID);
		}

		/// <summary>
		/// Returns VirtualFixUp string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "VirtualFixUp";
	}
}
