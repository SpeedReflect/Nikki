using System.IO;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.SunParts
{
	/// <summary>
	/// A unit <see cref="SunLayer"/> used in sun info collections.
	/// </summary>
	public class SunLayer : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public eSunTexture SunTextureType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public eSunAlpha SunAlphaType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float IntensityScale { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Size { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float OffsetX { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float OffsetY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public byte ColorAlpha { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public byte ColorRed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public byte ColorGreen { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public byte ColorBlue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int Angle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SweepAngleAmount { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new SunLayer();
			result.CloneValues(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.SunTextureType = br.ReadEnum<eSunTexture>();
			this.SunAlphaType = br.ReadEnum<eSunAlpha>();
			this.IntensityScale = br.ReadSingle();
			this.Size = br.ReadSingle();
			this.OffsetX = br.ReadSingle();
			this.OffsetY = br.ReadSingle();
			this.ColorAlpha = br.ReadByte();
			this.ColorRed = br.ReadByte();
			this.ColorGreen = br.ReadByte();
			this.ColorBlue = br.ReadByte();
			this.Angle = br.ReadInt32();
			this.SweepAngleAmount = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(this.SunTextureType);
			bw.WriteEnum(this.SunAlphaType);
			bw.Write(this.IntensityScale);
			bw.Write(this.Size);
			bw.Write(this.OffsetX);
			bw.Write(this.OffsetY);
			bw.Write(this.ColorAlpha);
			bw.Write(this.ColorRed);
			bw.Write(this.ColorGreen);
			bw.Write(this.ColorBlue);
			bw.Write(this.Angle);
			bw.Write(this.SweepAngleAmount);
		}
	}
}
