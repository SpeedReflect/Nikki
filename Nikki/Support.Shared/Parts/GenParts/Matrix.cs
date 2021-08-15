using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.GenParts
{
	/// <summary>
	/// A unit <see cref="Matrix"/> class with 4 (X, Y, Z, W) vectors.
	/// </summary>
	public class Matrix : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value11 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value12 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value13 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value14 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value21 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value22 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value23 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value24 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value31 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value32 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value33 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value34 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value41 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value42 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value43 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value44 { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Matrix();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Value11 = br.ReadSingle();
			this.Value12 = br.ReadSingle();
			this.Value13 = br.ReadSingle();
			this.Value14 = br.ReadSingle();
			this.Value21 = br.ReadSingle();
			this.Value22 = br.ReadSingle();
			this.Value23 = br.ReadSingle();
			this.Value24 = br.ReadSingle();
			this.Value31 = br.ReadSingle();
			this.Value32 = br.ReadSingle();
			this.Value33 = br.ReadSingle();
			this.Value34 = br.ReadSingle();
			this.Value41 = br.ReadSingle();
			this.Value42 = br.ReadSingle();
			this.Value43 = br.ReadSingle();
			this.Value44 = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Value11);
			bw.Write(this.Value12);
			bw.Write(this.Value13);
			bw.Write(this.Value14);
			bw.Write(this.Value21);
			bw.Write(this.Value22);
			bw.Write(this.Value23);
			bw.Write(this.Value24);
			bw.Write(this.Value31);
			bw.Write(this.Value32);
			bw.Write(this.Value33);
			bw.Write(this.Value34);
			bw.Write(this.Value41);
			bw.Write(this.Value42);
			bw.Write(this.Value43);
			bw.Write(this.Value44);
		}
	}
}
