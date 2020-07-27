using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Transmission"/> used in car performance.
	/// </summary>
	public class Transmission : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ClutchSlip { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float OptimalShift { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float FinalDriveRatio { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float FinalDriveRatio2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TorqueSplit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float BurnoutStrength { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int NumberOfGears { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearEfficiency { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatioR { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatioN { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio6 { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Transmission"/>.
		/// </summary>
        public Transmission() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Transmission();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.ClutchSlip = br.ReadSingle();
			this.OptimalShift = br.ReadSingle();
			this.FinalDriveRatio = br.ReadSingle();
			this.FinalDriveRatio2 = br.ReadSingle();
			this.TorqueSplit = br.ReadSingle();
			this.BurnoutStrength = br.ReadSingle();
			this.NumberOfGears = br.ReadInt32();
			this.GearEfficiency = br.ReadSingle();
			this.GearRatioR = br.ReadSingle();
			this.GearRatioN = br.ReadSingle();
			this.GearRatio1 = br.ReadSingle();
			this.GearRatio2 = br.ReadSingle();
			this.GearRatio3 = br.ReadSingle();
			this.GearRatio4 = br.ReadSingle();
			this.GearRatio5 = br.ReadSingle();
			this.GearRatio6 = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.ClutchSlip);
			bw.Write(this.OptimalShift);
			bw.Write(this.FinalDriveRatio);
			bw.Write(this.FinalDriveRatio2);
			bw.Write(this.TorqueSplit);
			bw.Write(this.BurnoutStrength);
			bw.Write(this.NumberOfGears);
			bw.Write(this.GearEfficiency);
			bw.Write(this.GearRatioR);
			bw.Write(this.GearRatioN);
			bw.Write(this.GearRatio1);
			bw.Write(this.GearRatio2);
			bw.Write(this.GearRatio3);
			bw.Write(this.GearRatio4);
			bw.Write(this.GearRatio5);
			bw.Write(this.GearRatio6);
		}
	}
}