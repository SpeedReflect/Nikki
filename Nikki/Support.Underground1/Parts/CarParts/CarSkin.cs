using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.CarParts
{
	/// <summary>
	/// A unit <see cref="CarSkin"/> used in cartypeinfo collections.
	/// </summary>
	public class CarSkin : ASubPart
	{
		/// <summary>
		/// Description of the skin.
		/// </summary>
		[AccessModifiable()]
		public string SkinDescription { get; set; } = String.Empty;

		/// <summary>
		/// Unknown integer value.
		/// </summary>
		[AccessModifiable()]
		public int Unknown { get; set; }

		/// <summary>
		/// Material used in this <see cref="CarSkin"/>.
		/// </summary>
		[AccessModifiable()]
		public string MaterialUsed { get; set; } = String.Empty;

		/// <summary>
		/// Class key of the skin.
		/// </summary>
		[AccessModifiable()]
		public eCarSkinClass SkinClassKey { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override ASubPart PlainCopy()
		{
			var result = new CarSkin()
			{
				SkinDescription = this.SkinDescription,
				Unknown = this.Unknown,
				MaterialUsed = this.MaterialUsed,
				SkinClassKey = this.SkinClassKey
			};

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="id">ID of the car this <see cref="CarSkin"/> belongs to.</param>
		/// <param name="index">Index of the <see cref="CarSkin"/>.</param>
		public void Read(BinaryReader br, out int id, out int index)
		{
			id = br.ReadInt32();
			index = br.ReadInt32();
			this.SkinDescription = br.ReadNullTermUTF8(0x20);
			this.Unknown = br.ReadInt32();
			this.MaterialUsed = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.SkinClassKey = br.ReadEnum<eCarSkinClass>();
			br.BaseStream.Position += 0xC;
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="id">ID of the car this <see cref="CarSkin"/> belongs to.</param>
		/// <param name="index">Index of the <see cref="CarSkin"/>.</param>
		public void Write(BinaryWriter bw, int id, int index)
		{
			bw.Write(id);
			bw.Write(index);
			bw.WriteNullTermUTF8(this.SkinDescription, 0x20);
			bw.Write(this.Unknown);
			bw.Write(this.MaterialUsed.BinHash());
			bw.WriteEnum(this.SkinClassKey);
			bw.Write((int)0);
			bw.Write((long)0);
		}
	}
}
