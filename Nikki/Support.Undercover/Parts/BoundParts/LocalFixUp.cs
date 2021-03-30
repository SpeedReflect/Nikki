using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Support.Undercover.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;



namespace Nikki.Support.Undercover.Parts.BoundParts
{
	/// <summary>
	/// <see cref="LocalFixUp"/> is a structure for <see cref="Collision"/>.
	/// </summary>
	public class LocalFixUp : SubPart
	{
		/// <summary>
		/// Offset of this <see cref="LocalFixUp"/>.
		/// </summary>
		public int fromOffset { get; set; }

		/// <summary>
		/// 2nd offset of this <see cref="LocalFixUp"/>.
		/// </summary>
		public int toOffset { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new LocalFixUp()
			{
				fromOffset = this.fromOffset,
				toOffset = this.toOffset
			};

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="LocalFixUp"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="LocalFixUp"/> with.</param>
		public void Read(BinaryReader br)
		{
			this.fromOffset = br.ReadInt32();
			this.toOffset = br.ReadInt32();

		}

		/// <summary>
		/// Assembles <see cref="LocalFixUp"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="LocalFixUp"/> with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.fromOffset);
			bw.Write(this.toOffset);
		}

		/// <summary>
		/// Returns LocalFixUp string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "LocalFixUp";
	}
}
