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
	/// <see cref="ConvexVerticesShape"/> is a unit vertex for <see cref="Collision"/>.
	/// </summary>
	public class ConvexVerticesShape : SubPart
	{
		/// <summary>
		/// X Half-extent value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsX { get; set; }

		/// <summary>
		/// Y Half-extent value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsY { get; set; }

		/// <summary>
		/// Z Half-extent value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsZ { get; set; }

		/// <summary>
		/// W Half-extent value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfExtentsW { get; set; }

		/// <summary>
		/// X Center value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float CenterX { get; set; }

		/// <summary>
		/// Y Center value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float CenterY { get; set; }

		/// <summary>
		/// Z Center value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float CenterZ { get; set; }

		/// <summary>
		/// W Center value of this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float CenterW { get; set; }

		/// <summary>
		/// An unknown float value in this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public float UnknownFloat { get; set; }

		/// <summary>
		/// Array definition for the rotated vertices in this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[Browsable(false)]
		public hkArray arrRotatedVertices { get; set; }

		/// <summary>
		/// Number of vertices in this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[AccessModifiable()]
		public int NumVertices { get; set; }

		/// <summary>
		/// Array definition for the plane equations in this <see cref="ConvexVerticesShape"/>.
		/// </summary>
		[Browsable(false)]
		public hkArray arrPlaneEquations { get; set; }

		/// <summary>
		/// List of rotated vertices.
		/// </summary>
		[Category("Secondary")]
		public List<RotatedVertice> RotatedVertices { get; }

		/// <summary>
		/// List of plane equations.
		/// </summary>
		[Category("Secondary")]
		public List<PlaneEquation> PlaneEquations { get; }

		/// <summary>
		/// Number of rotated vertices.
		/// </summary>
		[AccessModifiable()]
		public int NumberOfRotatedVertices
		{
			get => this.RotatedVertices.Count;
			set => this.RotatedVertices.Resize(value);
		}

		/// <summary>
		/// Number of rotated vertices.
		/// </summary>
		[AccessModifiable()]
		public int NumberOfPlaneEquations
		{
			get => this.PlaneEquations.Count;
			set => this.PlaneEquations.Resize(value);
		}

		/// <summary>
		/// Initializes new instance of <see cref="ConvexVerticesShape"/>.
		/// </summary>
		public ConvexVerticesShape()
		{
			this.arrRotatedVertices = new hkArray();
			this.arrPlaneEquations = new hkArray();
			this.RotatedVertices = new List<RotatedVertice>();
			this.PlaneEquations = new List<PlaneEquation>();
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new ConvexVerticesShape()
			{
				HalfExtentsX = this.HalfExtentsX,
				HalfExtentsY = this.HalfExtentsY,
				HalfExtentsZ = this.HalfExtentsZ,
				HalfExtentsW = this.HalfExtentsW,
				CenterX = this.CenterX,
				CenterY = this.CenterY,
				CenterZ = this.CenterZ,
				CenterW = this.CenterW,
				NumVertices = this.NumVertices,
				UnknownFloat = this.UnknownFloat,
				NumberOfRotatedVertices = this.NumberOfRotatedVertices,
				NumberOfPlaneEquations = this.NumberOfPlaneEquations
			};

			for (int loop = 0; loop < NumberOfRotatedVertices; ++loop)
			{

				result.RotatedVertices[loop] = (RotatedVertice)this.RotatedVertices[loop].PlainCopy();

			}

			for (int loop = 0; loop < NumberOfPlaneEquations; ++loop)
			{

				result.PlaneEquations[loop] = (PlaneEquation)this.PlaneEquations[loop].PlainCopy();

			}

			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="ConvexVerticesShape"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="ConvexVerticesShape"/> with.</param>
		public void Read(BinaryReader br)
		{
			br.BaseStream.Position += 0x10;
			this.UnknownFloat = br.ReadSingle();
			br.BaseStream.Position += 0x0C;
			this.HalfExtentsX = br.ReadSingle();
			this.HalfExtentsY = br.ReadSingle();
			this.HalfExtentsZ = br.ReadSingle();
			this.HalfExtentsW = br.ReadSingle();
			this.CenterX = br.ReadSingle();
			this.CenterY = br.ReadSingle();
			this.CenterZ = br.ReadSingle();
			this.CenterW = br.ReadSingle();
			this.arrRotatedVertices.Read(br);
			this.NumVertices = br.ReadInt32();
			this.arrPlaneEquations.Read(br);
			br.BaseStream.Position += 0x4;

			// Get sizes for Rotated Vertices and Plane Equations arrays
			this.NumberOfRotatedVertices = arrRotatedVertices.Size;
			this.NumberOfPlaneEquations = arrPlaneEquations.Size;

			// Get Rotated Vertices
			for (int loop = 0; loop < this.NumberOfRotatedVertices; ++loop)
			{

				this.RotatedVertices[loop].Read(br);

			}

			// Get Plane Equations
			for (int loop = 0; loop < this.NumberOfPlaneEquations; ++loop)
			{

				this.PlaneEquations[loop].Read(br);

			}

		}

		/// <summary>
		/// Assembles <see cref="ConvexVerticesShape"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="ConvexVerticesShape"/> with.</param>
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
			bw.Write(this.CenterX);
			bw.Write(this.CenterY);
			bw.Write(this.CenterZ);
			bw.Write(this.CenterW);
			this.arrRotatedVertices.Size = (short)this.NumberOfRotatedVertices;
			this.arrRotatedVertices.Capacity = (short)this.NumberOfRotatedVertices;
			this.arrRotatedVertices.Write(bw);
			bw.Write(this.NumVertices);
			this.arrPlaneEquations.Size = (short)this.NumberOfPlaneEquations;
			this.arrPlaneEquations.Capacity = (short)this.NumberOfPlaneEquations;
			this.arrPlaneEquations.Write(bw);
			bw.Write(0);

			// Write Rotated Vertices
			for (int loop = 0; loop < this.NumberOfRotatedVertices; ++loop)
			{

				this.RotatedVertices[loop].Write(bw);

			}

			// Write Plane Equations
			for (int loop = 0; loop < this.NumberOfPlaneEquations; ++loop)
			{

				this.PlaneEquations[loop].Write(bw);

			}
		}

		/// <summary>
		/// Returns ConvexVerticesShape string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "ConvexVerticesShape";
	}
}
