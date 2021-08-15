using System;
using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Utils;



namespace Nikki.Support.Shared.Parts.TPKParts
{
	public class TexturePage : SubPart
	{
		private string _name = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float U0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float V0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float U1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float V1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public uint Flags { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string TextureName
		{
			get => this._name;
			set => this._name = value ?? String.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		public uint TextureKey => this._name.BinHash();

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new TexturePage();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.U0 = br.ReadSingle();
			this.U1 = br.ReadSingle();
			this.V0 = br.ReadSingle();
			this.V1 = br.ReadSingle();
			this.Flags = br.ReadUInt32();
			this._name = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 8;
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.U0);
			bw.Write(this.U1);
			bw.Write(this.V0);
			bw.Write(this.V1);
			bw.Write(this.Flags);
			bw.Write(this._name.BinHash());
			bw.Write((long)0);
		}

		/// <summary>
		/// Name of the texture page.
		/// </summary>
		/// <returns>Name of the texture page as a string value.</returns>
		public override string ToString() => this.TextureName;
	}
}
