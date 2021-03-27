using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Support.Undercover.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;

namespace Nikki.Support.Undercover.Parts.BoundParts
{
    /// <summary>
	/// <see cref="PlaneEquation"/> is a structure for <see cref="ConvexVerticesShape"/>.
	/// </summary>
    public class PlaneEquation : SubPart
    {
		/// <summary>
		/// X value of this <see cref="PlaneEquation"/>.
		/// </summary>
		[AccessModifiable()]
		public float X { get; set; }

		/// <summary>
		/// Y value of this <see cref="PlaneEquation"/>.
		/// </summary>
		[AccessModifiable()]
		public float Y { get; set; }

		/// <summary>
		/// Z value of this <see cref="PlaneEquation"/>.
		/// </summary>
		[AccessModifiable()]
		public float Z { get; set; }

		/// <summary>
		/// W value of this <see cref="PlaneEquation"/>.
		/// </summary>
		[AccessModifiable()]
		public float W { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PlaneEquation()
			{
				X = this.X,
				Y = this.Y,
				Z = this.Z,
				W = this.W,
			};

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="PlaneEquation"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="PlaneEquation"/> with.</param>
		public void Read(BinaryReader br)
		{
			this.X = br.ReadSingle();
			this.Y = br.ReadSingle();
			this.Z = br.ReadSingle();
			this.W = br.ReadSingle();
		}

		/// <summary>
		/// Assembles <see cref="PlaneEquation"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PlaneEquation"/> with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.X);
			bw.Write(this.Y);
			bw.Write(this.Z);
			bw.Write(this.W);
		}

		/// <summary>
		/// Returns PlaneEquation string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "PlaneEquation";
	}
}
