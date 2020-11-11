using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Unknown"/> used in car performance.
	/// </summary>
	public class Unknown : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown4 { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Unknown"/>.
		/// </summary>
		public Unknown() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Unknown()
			{
				Unknown1 = this.Unknown1,
				Unknown2 = this.Unknown2,
				Unknown3 = this.Unknown3,
				Unknown4 = this.Unknown4
			};

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Unknown1 = br.ReadSingle();
			this.Unknown2 = br.ReadSingle();
			this.Unknown3 = br.ReadSingle();
			this.Unknown4 = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Unknown1);
			bw.Write(this.Unknown2);
			bw.Write(this.Unknown3);
			bw.Write(this.Unknown4);
		}
	}
}
