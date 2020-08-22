using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Prostreet.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="StrokeEffect"/> that is used in <see cref="PathSet"/>.
	/// </summary>
	[DebuggerDisplay("Color: {HexColor}")]
	public class StrokeEffect : Shared.Parts.VinylParts.StrokeEffect
	{
		/// <summary>
		/// Constant size of one unit class.
		/// </summary>
		[Browsable(false)]
		public override int BlockSize => 0xC;

		/// <summary>
		/// Position value of the effect.
		/// </summary>
		[AccessModifiable()]
		public int Position { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new StrokeEffect();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Read(BinaryReader br)
		{
			this.Red = br.ReadByte();
			this.Green = br.ReadByte();
			this.Blue = br.ReadByte();
			this.Alpha = br.ReadByte();
			this.Thickness = br.ReadSingle();
			this.Position = br.ReadInt32();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Write(BinaryWriter bw)
		{
			bw.Write(this.Red);
			bw.Write(this.Green);
			bw.Write(this.Blue);
			bw.Write(this.Alpha);
			bw.Write(this.Thickness);
			bw.Write(this.Position);
		}

		/// <summary>
		/// Returns name of the class a string value.
		/// </summary>
		/// <returns>Name of the class a string value.</returns>
		public override string ToString() => "StrokeEffect";
	}
}
