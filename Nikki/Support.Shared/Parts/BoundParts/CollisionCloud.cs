using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;



namespace Nikki.Support.Shared.Parts.BoundParts
{
	/// <summary>
	/// <see cref="CollisionCloud"/> is a unit bound for <see cref="Collision"/>.
	/// </summary>
	public class CollisionCloud : SubPart
	{
		private List<CollisionVertex> _vertices = new List<CollisionVertex>();

		/// <summary>
		/// Indicates amount of vertices in this <see cref="CollisionCloud"/>.
		/// </summary>
		[AccessModifiable()]
		public int NumberOfVertices
		{
			get => this._vertices.Count;
			set => this._vertices.Resize(value);
		}

		/// <summary>
		/// List of <see cref="CollisionVertex"/> in this <see cref="CollisionCloud"/>.
		/// </summary>
		public List<CollisionVertex> Vertices => this._vertices;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new CollisionCloud()
			{
				NumberOfVertices = this.NumberOfVertices
			};

			for (int loop = 0; loop < this.Vertices.Count; ++loop)
			{

				result.Vertices[loop] = (CollisionVertex)this.Vertices[loop].PlainCopy();

			}

			return result;
		}

		/// <summary>
		/// Clones values of another <see cref="SubPart"/>.
		/// </summary>
		/// <param name="other"><see cref="SubPart"/> to clone.</param>
		public override void CloneValuesFrom(SubPart other)
		{
			if (other is CollisionCloud cloud)
			{

				this.NumberOfVertices = cloud.NumberOfVertices;

				for (int loop = 0; loop < cloud.Vertices.Count; ++loop)
				{

					this.Vertices[loop] = (CollisionVertex)cloud.Vertices[loop].PlainCopy();

				}

			}
		}

		/// <summary>
		/// Disassembles array into <see cref="CollisionBound"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="CollisionBound"/> with.</param>
		public void Read(BinaryReader br)
		{
			this.NumberOfVertices = br.ReadInt32();
			br.BaseStream.Position += 12;

			for (int loop = 0; loop < this.NumberOfVertices; ++loop)
			{

				this.Vertices[loop].Read(br);
			
			}
		}

		/// <summary>
		/// Assembles <see cref="CollisionBound"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="CollisionBound"/> with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.NumberOfVertices);
			bw.Write((int)0);
			bw.Write((long)0);

			for (int loop = 0; loop < this.Vertices.Count; ++loop)
			{

				this.Vertices[loop].Write(bw);

			}
		}

		/// <summary>
		/// Returns CollisionCloud string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "CollisionCloud";
	}
}
