using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Support.Shared.Class;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.BoundParts
{
	/// <summary>
	/// <see cref="CollisionCloud"/> is a unit bound for <see cref="Collision"/>.
	/// </summary>
	public class CollisionCloud : ASubPart, ICopyable<CollisionCloud>
	{
		/// <summary>
		/// Indicates amount of vertices in this <see cref="CollisionCloud"/>.
		/// </summary>
		[AccessModifiable()]
		public int NumberOfVertices { get; set; }

		/// <summary>
		/// List of <see cref="CollisionVertex"/> in this <see cref="CollisionCloud"/>.
		/// </summary>
		[Listable("Vertices", "VERTEX")]
		public List<CollisionVertex> Vertices { get; set; } = new List<CollisionVertex>();

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public CollisionCloud PlainCopy()
		{
			var result = new CollisionCloud()
			{
				NumberOfVertices = this.NumberOfVertices
			};
			for (int a1 = 0; a1 < this.Vertices.Count; ++a1)
				result.Vertices.Add(this.Vertices[a1].PlainCopy());
			return result;
		}

		/// <summary>
		/// Assembles <see cref="CollisionBound"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="CollisionBound"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			bw.Write(this.NumberOfVertices);
			bw.Write((int)0);
			bw.Write((long)0);
			for (int a1 = 0; a1 < this.Vertices.Count; ++a1)
				this.Vertices[a1].Assemble(bw);
		}

		/// <summary>
		/// Disassembles array into <see cref="CollisionBound"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="CollisionBound"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			this.NumberOfVertices = br.ReadInt32();
			br.BaseStream.Position += 12;
			for (int a1 = 0; a1 < this.NumberOfVertices; ++a1)
			{
				var vertex = new CollisionVertex();
				vertex.Disassemble(br);
				this.Vertices.Add(vertex);
			}
		}
	}
}
