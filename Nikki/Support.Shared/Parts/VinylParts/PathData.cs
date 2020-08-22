using System.IO;
using System.Diagnostics;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="PathData"/> that is used in <see cref="PathSet"/>.
	/// </summary>
	[DebuggerDisplay("({StartIndex} - {NumCurves})")]
	public class PathData : SubPart
	{
		/// <summary>
		/// Index of the starting point of this <see cref="PathData"/>.
		/// </summary>
		[AccessModifiable()]
		public ushort StartIndex { get; set; }

		/// <summary>
		/// Number of curves in this <see cref="PathData"/>.
		/// </summary>
		[AccessModifiable()]
		public ushort NumCurves { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PathData()
			{
				StartIndex = this.StartIndex,
				NumCurves = this.NumCurves,
			};

			return result;
		}

		/// <summary>
		/// Clones values of another <see cref="SubPart"/>.
		/// </summary>
		/// <param name="other"><see cref="SubPart"/> to clone.</param>
		public override void CloneValuesFrom(SubPart other)
		{
			if (other is PathData data)
			{

				this.StartIndex = data.StartIndex;
				this.NumCurves = data.NumCurves;

			}
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.StartIndex = br.ReadUInt16();
			this.NumCurves = br.ReadUInt16();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.StartIndex);
			bw.Write(this.NumCurves);
		}

		/// <summary>
		/// Returns name of the class a string value.
		/// </summary>
		/// <returns>Name of the class a string value.</returns>
		public override string ToString() => "PathData";
	}
}
