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
	/// <see cref="ConvexTranslateShape"/> is a unit vertex for <see cref="Collision"/>.
	/// </summary>
	public class ConvexTranslateShape : SubPart
	{
		/// <summary>
		/// X AABB Half-extent value of this <see cref="ConvexTranslateShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float TranslationX { get; set; }

		/// <summary>
		/// Y AABB Half-extent value of this <see cref="ConvexTranslateShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float TranslationY { get; set; }

		/// <summary>
		/// Z AABB Half-extent value of this <see cref="ConvexTranslateShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float TranslationZ { get; set; }

		/// <summary>
		/// W AABB Half-extent value of this <see cref="ConvexTranslateShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float TranslationW { get; set; }

		/// <summary>
		/// An unknown float value in this <see cref="ConvexTranslateShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float UnknownFloat { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new ConvexTranslateShape()
			{
				TranslationX = this.TranslationX,
				TranslationY = this.TranslationY,
				TranslationZ = this.TranslationZ,
				TranslationW = this.TranslationW,
				UnknownFloat = this.UnknownFloat
			};

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="ConvexTranslateShape"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="ConvexTranslateShape"/> with.</param>
		public void Read(BinaryReader br)
		{
			br.BaseStream.Position += 0x10;
			this.UnknownFloat = br.ReadSingle();
			br.BaseStream.Position += 0x0C;
			this.TranslationX = br.ReadSingle();
			this.TranslationY = br.ReadSingle();
			this.TranslationZ = br.ReadSingle();
			this.TranslationW = br.ReadSingle();
		}

		/// <summary>
		/// Assembles <see cref="ConvexTranslateShape"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="ConvexTranslateShape"/> with.</param>
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
			bw.Write(this.TranslationX);
			bw.Write(this.TranslationY);
			bw.Write(this.TranslationZ);
			bw.Write(this.TranslationW);
		}

		/// <summary>
		/// Returns ConvexTranslateShape string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "ConvexTranslateShape";
	}
}
