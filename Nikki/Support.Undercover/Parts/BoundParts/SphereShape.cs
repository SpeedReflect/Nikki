using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Support.Undercover.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;
using System.ComponentModel;

namespace Nikki.Support.Undercover.Parts.BoundParts
{
	/// <summary>
	/// <see cref="SphereShape"/> is a unit vertex for <see cref="Collision"/>.
	/// </summary>
	public class SphereShape : SubPart
	{
		/// <summary>
		/// An unknown float value in this <see cref="SphereShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float UnknownFloat { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new SphereShape()
			{
				UnknownFloat = this.UnknownFloat
			};

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="SphereShape"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="SphereShape"/> with.</param>
		public void Read(BinaryReader br)
		{
			br.BaseStream.Position += 0x10;
			this.UnknownFloat = br.ReadSingle();
			br.BaseStream.Position += 0x0C;
		}

		/// <summary>
		/// Assembles <see cref="SphereShape"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SphereShape"/> with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(0);
			bw.Write(0);
			bw.Write(0);
			bw.Write(0);
			bw.Write(this.UnknownFloat);
			bw.Write(0);
			bw.Write(0);
			bw.Write(0);
		}

		/// <summary>
		/// Returns SphereShape string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "SphereShape";
	}
}
