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
		public float Value1X { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value1Y { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value1Z { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value1W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value2X { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value2Y { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value2Z { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value2W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value3X { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value3Y { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value3Z { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value3W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value4X { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value4Y { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value4Z { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Value4W { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Matrix();


			foreach (var property in this.GetType().GetProperties())
			{

				property.SetValue(result, property.GetValue(this));

			}

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Value1X = br.ReadSingle();
			this.Value1Y = br.ReadSingle();
			this.Value1Z = br.ReadSingle();
			this.Value1W = br.ReadSingle();
			this.Value2X = br.ReadSingle();
			this.Value2Y = br.ReadSingle();
			this.Value2Z = br.ReadSingle();
			this.Value2W = br.ReadSingle();
			this.Value3X = br.ReadSingle();
			this.Value3Y = br.ReadSingle();
			this.Value3Z = br.ReadSingle();
			this.Value3W = br.ReadSingle();
			this.Value4X = br.ReadSingle();
			this.Value4Y = br.ReadSingle();
			this.Value4Z = br.ReadSingle();
			this.Value4W = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Value1X);
			bw.Write(this.Value1Y);
			bw.Write(this.Value1Z);
			bw.Write(this.Value1W);
			bw.Write(this.Value2X);
			bw.Write(this.Value2Y);
			bw.Write(this.Value2Z);
			bw.Write(this.Value2W);
			bw.Write(this.Value3X);
			bw.Write(this.Value3Y);
			bw.Write(this.Value3Z);
			bw.Write(this.Value3W);
			bw.Write(this.Value4X);
			bw.Write(this.Value4Y);
			bw.Write(this.Value4Z);
			bw.Write(this.Value4W);
		}
	}
}
