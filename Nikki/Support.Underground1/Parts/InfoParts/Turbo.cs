using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.InfoParts
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
		public float TurboTorque1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque7 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboTorque9 { get; set; }

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
			this.TurboTorque1 = br.ReadSingle();
			this.TurboTorque2 = br.ReadSingle();
			this.TurboTorque3 = br.ReadSingle();
			this.TurboTorque4 = br.ReadSingle();
			this.TurboTorque5 = br.ReadSingle();
			this.TurboTorque6 = br.ReadSingle();
			this.TurboTorque7 = br.ReadSingle();
			this.TurboTorque8 = br.ReadSingle();
			this.TurboTorque9 = br.ReadSingle();
			br.BaseStream.Position += 0xC;
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.TurboTorque1);
			bw.Write(this.TurboTorque2);
			bw.Write(this.TurboTorque3);
			bw.Write(this.TurboTorque4);
			bw.Write(this.TurboTorque5);
			bw.Write(this.TurboTorque6);
			bw.Write(this.TurboTorque7);
			bw.Write(this.TurboTorque8);
			bw.Write(this.TurboTorque9);
			bw.WriteBytes(0, 0xC);
		}
	}
}
