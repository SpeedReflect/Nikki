using System.IO;
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
		#region Enums

		/// <summary>
		/// Enum of sun alpha values.
		/// </summary>
		public enum SunAlpha : int
		{
			/// <summary>
			/// Blenging rays.
			/// </summary>
			SUNALPHA_BLEND = 0,

			/// <summary>
			/// Additive rays.
			/// </summary>
			SUNALPHA_ADD = 1,
		}

		/// <summary>
		/// Enum of sun textures that could be used.
		/// </summary>
		public enum SunTexture : int
		{
			/// <summary>
			/// 
			/// </summary>
			SUNTEX_CENTER = 0,

			/// <summary>
			/// 
			/// </summary>
			SUNTEX_HALO = 1,

			/// <summary>
			/// 
			/// </summary>
			SUNTEX_MAJORRAYS = 2,

			/// <summary>
			/// 
			/// </summary>
			SUNTEX_MINORRAYS = 3,

			/// <summary>
			/// 
			/// </summary>
			SUNTEX_RING = 4,

			/// <summary>
			/// 
			/// </summary>
			NUM_SUN_TEXTURES = 5,
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public SunTexture SunTextureType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public SunAlpha SunAlphaType { get; set; }

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
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.SunTextureType = br.ReadEnum<SunTexture>();
			this.SunAlphaType = br.ReadEnum<SunAlpha>();
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
