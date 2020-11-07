using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Engine"/> used in car performance.
	/// </summary>
	public class Engine : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SpeedRefreshRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque7 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque9 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineBraking1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineBraking2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineBraking3 { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Engine"/>.
		/// </summary>
		public Engine() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Engine();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.SpeedRefreshRate = br.ReadSingle();
			this.EngineTorque1 = br.ReadSingle();
			this.EngineTorque2 = br.ReadSingle();
			this.EngineTorque3 = br.ReadSingle();
			this.EngineTorque4 = br.ReadSingle();
			this.EngineTorque5 = br.ReadSingle();
			this.EngineTorque6 = br.ReadSingle();
			this.EngineTorque7 = br.ReadSingle();
			this.EngineTorque8 = br.ReadSingle();
			this.EngineTorque9 = br.ReadSingle();
			this.EngineBraking1 = br.ReadSingle();
			this.EngineBraking2 = br.ReadSingle();
			this.EngineBraking3 = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.SpeedRefreshRate);
			bw.Write(this.EngineTorque1);
			bw.Write(this.EngineTorque2);
			bw.Write(this.EngineTorque3);
			bw.Write(this.EngineTorque4);
			bw.Write(this.EngineTorque5);
			bw.Write(this.EngineTorque6);
			bw.Write(this.EngineTorque7);
			bw.Write(this.EngineTorque8);
			bw.Write(this.EngineTorque9);
			bw.Write(this.EngineBraking1);
			bw.Write(this.EngineBraking2);
			bw.Write(this.EngineBraking3);
		}
	}
}
