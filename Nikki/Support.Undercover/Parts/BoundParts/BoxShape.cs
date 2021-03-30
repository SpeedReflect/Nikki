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
	/// <see cref="BoxShape"/> is a unit vertex for <see cref="Collision"/>.
	/// </summary>
	public class BoxShape : SubPart
	{
		/// <summary>
		/// X Half-extent value of this <see cref="BoxShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsX { get; set; }

		/// <summary>
		/// Y Half-extent value of this <see cref="BoxShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsY { get; set; }

		/// <summary>
		/// Z Half-extent value of this <see cref="BoxShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsZ { get; set; }

		/// <summary>
		/// W Half-extent value of this <see cref="BoxShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsW { get; set; }

		/// <summary>
		/// An unknown float value in this <see cref="BoxShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float UnknownFloat { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new BoxShape()
			{
				HalfExtentsX = this.HalfExtentsX,
				HalfExtentsY = this.HalfExtentsY,
				HalfExtentsZ = this.HalfExtentsZ,
				HalfExtentsW = this.HalfExtentsW,
				UnknownFloat = this.UnknownFloat
			};

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="BoxShape"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="BoxShape"/> with.</param>
		public void Read(BinaryReader br)
		{
			br.BaseStream.Position += 0x10;
			this.UnknownFloat = br.ReadSingle();
			br.BaseStream.Position += 0x0C;
			this.HalfExtentsX = br.ReadSingle();
			this.HalfExtentsY = br.ReadSingle();
			this.HalfExtentsZ = br.ReadSingle();
			this.HalfExtentsW = br.ReadSingle();
		}

		/// <summary>
		/// Assembles <see cref="BoxShape"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="BoxShape"/> with.</param>
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
			bw.Write(this.HalfExtentsX);
			bw.Write(this.HalfExtentsY);
			bw.Write(this.HalfExtentsZ);
			bw.Write(this.HalfExtentsW);
		}

		/// <summary>
		/// Returns BoxShape string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "BoxShape";
	}
}
