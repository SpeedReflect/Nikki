using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="StrokeEffect"/> that is used in <see cref="PathSet"/>.
	/// </summary>
	[DebuggerDisplay("Color: {HexColor}")]
	public abstract class StrokeEffect : SubPart
	{
		/// <summary>
		/// Constant size of one unit class.
		/// </summary>
		[Browsable(false)]
		public abstract int BlockSize { get; }

		/// <summary>
		/// Red color of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Red { get; set; }

		/// <summary>
		/// Green color of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Green { get; set; }

		/// <summary>
		/// Blue color of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Blue { get; set; }

		/// <summary>
		/// Alpha color of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Alpha { get; set; }

		/// <summary>
		/// Thinkness of the stroke outline.
		/// </summary>
		[AccessModifiable()]
		public float Thickness { get; set; }

		/// <summary>
		/// Hexadecimal string representation of the color of the effect.
		/// </summary>
		[Browsable(false)]
		public string HexColor => $"0x{this.Red:X2}{this.Green:X2}{this.Blue:X2}{this.Alpha:X2}";

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public abstract void Read(BinaryReader br);

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public abstract void Write(BinaryWriter bw);

		/// <summary>
		/// Gets color of this effect as an HTML formatted string.
		/// </summary>
		/// <returns>Color as an HTML formatted string.</returns>
		public string GetHTMLColor() => $"#{this.Red:X2}{this.Green:X2}{this.Blue:X2}";

		/// <summary>
		/// Returns name of the class a string value.
		/// </summary>
		/// <returns>Name of the class a string value.</returns>
		public override string ToString() => "StrokeEffect";
	}
}
