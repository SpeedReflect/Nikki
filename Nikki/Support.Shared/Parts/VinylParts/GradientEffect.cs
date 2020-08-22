using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="GradientEffect"/> that is used in <see cref="PathSet"/>.
	/// </summary>
	[DebuggerDisplay("Color1: {HexColor1} | Color2: {HexColor2}")]
	public class GradientEffect : SubPart
	{
		/// <summary>
		/// Constant size of one unit class.
		/// </summary>
		[Browsable(false)]
		public int BlockSize => 0x28;

		/// <summary>
		/// Angle of the gradient.
		/// </summary>
		[AccessModifiable()]
		public float Angle { get; set; }

		/// <summary>
		/// Main alpha of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte MainAlpha { get; set; }

		/// <summary>
		/// Unknown value 1 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown1 { get; set; }

		/// <summary>
		/// Unknown value 2 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown2 { get; set; }

		/// <summary>
		/// Unknown value 3 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown3 { get; set; }

		/// <summary>
		/// Red color 1 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color1Red { get; set; }

		/// <summary>
		/// Green color 1 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color1Green { get; set; }

		/// <summary>
		/// Blue color 1 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color1Blue { get; set; }

		/// <summary>
		/// Alpha color 1 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color1Alpha { get; set; }

		/// <summary>
		/// Red color 2 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color2Red { get; set; }

		/// <summary>
		/// Green color 2 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color2Green { get; set; }

		/// <summary>
		/// Blue color 2 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color2Blue { get; set; }

		/// <summary>
		/// Alpha color 2 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public byte Color2Alpha { get; set; }

		/// <summary>
		/// Position of color 1 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public float Color1Position { get; set; }

		/// <summary>
		/// Position of color 2 of the gradient.
		/// </summary>
		[AccessModifiable()]
		public float Color2Position { get; set; }

		/// <summary>
		/// Position of alpha 1 color of the gradient.
		/// </summary>
		[AccessModifiable()]
		public float Alpha1Position { get; set; }

		/// <summary>
		/// Position of alpha 2 color of the gradient.
		/// </summary>
		[AccessModifiable()]
		public float Alpha2Position { get; set; }

		/// <summary>
		/// Offset U coordinate of the gradient.
		/// </summary>
		[AccessModifiable()]
		public float OffsetU { get; set; }

		/// <summary>
		/// Offset V coordinate of the gradient.
		/// </summary>
		[AccessModifiable()]
		public float OffsetV { get; set; }

		/// <summary>
		/// Hexadecimal string representation of the color of the effect.
		/// </summary>
		[Browsable(false)]
		public string HexColor1 =>
			$"0x{this.Color1Red:X2}{this.Color1Green:X2}{this.Color1Blue:X2}{this.Color1Alpha:X2}";

		/// <summary>
		/// Hexadecimal string representation of the color of the effect.
		/// </summary>
		[Browsable(false)]
		public string HexColor2 =>
			$"0x{this.Color2Red:X2}{this.Color2Green:X2}{this.Color2Blue:X2}{this.Color2Alpha:X2}";

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new GradientEffect();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Angle = br.ReadSingle();
			this.MainAlpha = br.ReadByte();
			this.Unknown1 = br.ReadByte();
			this.Unknown2 = br.ReadByte();
			this.Unknown3 = br.ReadByte();
			this.Color1Red = br.ReadByte();
			this.Color1Green = br.ReadByte();
			this.Color1Blue = br.ReadByte();
			this.Color1Alpha = br.ReadByte();
			this.Color2Red = br.ReadByte();
			this.Color2Green = br.ReadByte();
			this.Color2Blue = br.ReadByte();
			this.Color2Alpha = br.ReadByte();
			this.Color1Position = br.ReadSingle();
			this.Color2Position = br.ReadSingle();
			this.Alpha1Position = br.ReadSingle();
			this.Alpha2Position = br.ReadSingle();
			this.OffsetU = br.ReadSingle();
			this.OffsetV = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Angle);
			bw.Write(this.MainAlpha);
			bw.Write(this.Unknown1);
			bw.Write(this.Unknown2);
			bw.Write(this.Unknown3);
			bw.Write(this.Color1Red);
			bw.Write(this.Color1Green);
			bw.Write(this.Color1Blue);
			bw.Write(this.Color1Alpha);
			bw.Write(this.Color2Red);
			bw.Write(this.Color2Green);
			bw.Write(this.Color2Blue);
			bw.Write(this.Color2Alpha);
			bw.Write(this.Color1Position);
			bw.Write(this.Color2Position);
			bw.Write(this.Alpha1Position);
			bw.Write(this.Alpha2Position);
			bw.Write(this.OffsetU);
			bw.Write(this.OffsetV);
		}

		/// <summary>
		/// Returns name of the class a string value.
		/// </summary>
		/// <returns>Name of the class a string value.</returns>
		public override string ToString() => "GradientEffect";
	}
}
