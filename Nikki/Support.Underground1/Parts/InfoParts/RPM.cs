using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="RPM"/> used in car performance.
	/// </summary>
	public class RPM : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float IdleRPMAdd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float RedLineRPMAdd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float MaxRPMAdd { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="RPM"/>.
		/// </summary>
		public RPM() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new RPM()
			{
				IdleRPMAdd = this.IdleRPMAdd,
				MaxRPMAdd = this.MaxRPMAdd,
				RedLineRPMAdd = this.RedLineRPMAdd
			};

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.IdleRPMAdd = br.ReadSingle();
			this.RedLineRPMAdd = br.ReadSingle();
			this.MaxRPMAdd = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.IdleRPMAdd);
			bw.Write(this.RedLineRPMAdd);
			bw.Write(this.MaxRPMAdd);
		}
	}
}
