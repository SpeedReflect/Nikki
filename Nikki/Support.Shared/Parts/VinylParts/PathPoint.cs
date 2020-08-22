using System.IO;
using System.Diagnostics;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="PathPoint"/> that is used in <see cref="PathSet"/>.
	/// </summary>
	[DebuggerDisplay("({X}, {Y})")]
	public class PathPoint : SubPart
	{
		/// <summary>
		/// X coordinate of this <see cref="PathPoint"/>.
		/// </summary>
		[AccessModifiable()]
		public ushort X { get; set; }

		/// <summary>
		/// Y coordinate of this <see cref="PathPoint"/>.
		/// </summary>
		[AccessModifiable()]
		public ushort Y { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PathPoint()
			{
				X = this.X,
				Y = this.Y,
			};

			return result;
		}

		/// <summary>
		/// Clones values of another <see cref="SubPart"/>.
		/// </summary>
		/// <param name="other"><see cref="SubPart"/> to clone.</param>
		public override void CloneValuesFrom(SubPart other)
		{
			if (other is PathPoint point)
			{

				this.X = point.X;
				this.Y = point.Y;

			}
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.X = br.ReadUInt16();
			this.Y = br.ReadUInt16();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.X);
			bw.Write(this.Y);
		}

		/// <summary>
		/// Returns name of the class a string value.
		/// </summary>
		/// <returns>Name of the class a string value.</returns>
		public override string ToString() => "PathPoint";
	}
}
