using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="InnerGlowEffect"/> that is used in <see cref="PathSet"/>.
	/// </summary>
	[DebuggerDisplay("Color: {HexColor}")]
	public class InnerGlowEffect : SubPart
	{
		/// <summary>
		/// Constant size of one unit class.
		/// </summary>
		[Browsable(false)]
		public int BlockSize => 0x10;

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
		/// Edge value of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Edge { get; set; }

		/// <summary>
		/// Unknown value 1 of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown1 { get; set; }

		/// <summary>
		/// Unknown value 2 of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown2 { get; set; }

		/// <summary>
		/// Unknown value 3 of the effect.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown3 { get; set; }

		/// <summary>
		/// Size of the effect.
		/// </summary>
		[AccessModifiable()]
		public float Size { get; set; }

		/// <summary>
		/// Choke value of the effect.
		/// </summary>
		[AccessModifiable()]
		public float Choke { get; set; }

		/// <summary>
		/// Hexadecimal string representation of the color of the effect.
		/// </summary>
		[Browsable(false)]
		public string HexColor => $"0x{this.Red:X2}{this.Green:X2}{this.Blue:X2}{this.Alpha:X2}";

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new InnerGlowEffect();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Red = br.ReadByte();
			this.Green = br.ReadByte();
			this.Blue = br.ReadByte();
			this.Alpha = br.ReadByte();
			this.Edge = br.ReadByte();
			this.Unknown1 = br.ReadByte();
			this.Unknown2 = br.ReadByte();
			this.Unknown3 = br.ReadByte();
			this.Size = br.ReadSingle();
			this.Choke = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Red);
			bw.Write(this.Green);
			bw.Write(this.Blue);
			bw.Write(this.Alpha);
			bw.Write(this.Edge);
			bw.Write(this.Unknown1);
			bw.Write(this.Unknown2);
			bw.Write(this.Unknown3);
			bw.Write(this.Size);
			bw.Write(this.Choke);
		}

		/// <summary>
		/// Returns name of the class a string value.
		/// </summary>
		/// <returns>Name of the class a string value.</returns>
		public override string ToString() => "InnerGlowEffect";
	}
}
