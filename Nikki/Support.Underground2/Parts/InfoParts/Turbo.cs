using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Turbo"/> used in car performance.
	/// </summary>
	public class Turbo : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboBraking { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboVacuum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboHeatHigh { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboHeatLow { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboHighBoost { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboLowBoost { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboSpool { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboSpoolTimeDown { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboSpoolTimeUp { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Turbo"/>.
		/// </summary>
		public Turbo() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Turbo();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.TurboBraking = br.ReadSingle();
			this.TurboVacuum = br.ReadSingle();
			this.TurboHeatHigh = br.ReadSingle();
			this.TurboHeatLow = br.ReadSingle();
			this.TurboHighBoost = br.ReadSingle();
			this.TurboLowBoost = br.ReadSingle();
			this.TurboSpool = br.ReadSingle();
			this.TurboSpoolTimeDown = br.ReadSingle();
			this.TurboSpoolTimeUp = br.ReadSingle();
			br.BaseStream.Position += 0xC;
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.TurboBraking);
			bw.Write(this.TurboVacuum);
			bw.Write(this.TurboHeatHigh);
			bw.Write(this.TurboHeatLow);
			bw.Write(this.TurboHighBoost);
			bw.Write(this.TurboLowBoost);
			bw.Write(this.TurboSpool);
			bw.Write(this.TurboSpoolTimeDown);
			bw.Write(this.TurboSpoolTimeUp);
			bw.WriteBytes(0, 0xC);
		}
	}
}
