using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="RigidControls"/> used in car performance.
	/// </summary>
	public class RigidControls : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown01 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown02 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown03 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown04 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown05 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown06 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown07 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown08 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown09 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown10 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown11 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown12 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown13 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown14 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown15 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown16 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float Unknown17 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown18 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown19 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown20 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown21 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown22 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public short Unknown23 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int Unknown24 { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="RigidControls"/>.
		/// </summary>
		public RigidControls()
		{
			this.Unknown01 = 0;
			this.Unknown02 = 7;
			this.Unknown03 = 275;
			this.Unknown04 = -43;
			this.Unknown05 = 43;
			this.Unknown06 = 0;
			this.Unknown07 = 7000;
			this.Unknown08 = 64;
			this.Unknown09 = 64;
			this.Unknown10 = 53;
			this.Unknown11 = 300;
			this.Unknown12 = 53;
			this.Unknown13 = 299;
			this.Unknown14 = 0;
			this.Unknown15 = 220;
			this.Unknown16 = 0;
			this.Unknown17 = 360;
			this.Unknown18 = 64;
			this.Unknown19 = 64;
			this.Unknown20 = 115;
			this.Unknown21 = 0;
			this.Unknown22 = 91;
			this.Unknown23 = 0;
			this.Unknown24 = 0;
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new RigidControls();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Unknown01 = br.ReadSingle();
			this.Unknown02 = br.ReadSingle();
			this.Unknown03 = br.ReadSingle();
			this.Unknown04 = br.ReadSingle();
			this.Unknown05 = br.ReadSingle();
			this.Unknown06 = br.ReadSingle();
			this.Unknown07 = br.ReadSingle();
			this.Unknown08 = br.ReadInt16();
			this.Unknown09 = br.ReadInt16();
			this.Unknown10 = br.ReadSingle();
			this.Unknown11 = br.ReadSingle();
			this.Unknown12 = br.ReadSingle();
			this.Unknown13 = br.ReadSingle();
			this.Unknown14 = br.ReadSingle();
			this.Unknown15 = br.ReadSingle();
			this.Unknown16 = br.ReadSingle();
			this.Unknown17 = br.ReadSingle();
			this.Unknown18 = br.ReadInt16();
			this.Unknown19 = br.ReadInt16();
			this.Unknown20 = br.ReadInt16();
			this.Unknown21 = br.ReadInt16();
			this.Unknown22 = br.ReadInt16();
			this.Unknown23 = br.ReadInt16();
			this.Unknown24 = br.ReadInt32();

		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.Unknown01);
			bw.Write(this.Unknown02);
			bw.Write(this.Unknown03);
			bw.Write(this.Unknown04);
			bw.Write(this.Unknown05);
			bw.Write(this.Unknown06);
			bw.Write(this.Unknown07);
			bw.Write(this.Unknown08);
			bw.Write(this.Unknown09);
			bw.Write(this.Unknown10);
			bw.Write(this.Unknown11);
			bw.Write(this.Unknown12);
			bw.Write(this.Unknown13);
			bw.Write(this.Unknown14);
			bw.Write(this.Unknown15);
			bw.Write(this.Unknown16);
			bw.Write(this.Unknown17);
			bw.Write(this.Unknown18);
			bw.Write(this.Unknown19);
			bw.Write(this.Unknown20);
			bw.Write(this.Unknown21);
			bw.Write(this.Unknown22);
			bw.Write(this.Unknown23);
			bw.Write(this.Unknown24);
		}
	}
}
