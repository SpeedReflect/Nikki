using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A class that holds parameters for a custom attribute type.
	/// </summary>
	public class CustomCP
	{
		/// <summary>
		/// Name of the custom attribute.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// BinKey of the custom attribute.
		/// </summary>
		public uint Key { get; set; }

		/// <summary>
		/// Attribute type of the custom attribute.
		/// </summary>
		public CarPartAttribType AttribType { get; set; }

		/// <summary>
		/// Constructor for <see cref="CustomCP"/>.
		/// </summary>
		public CustomCP() { }

		/// <summary>
		/// Constructor for <see cref="CustomCP"/>.
		/// </summary>
		/// <param name="key">Key of this <see cref="CustomCP"/>.</param>
		public CustomCP(uint key)
		{
			this.Key = key;
			this.Name = key.BinString(LookupReturn.EMPTY);
			this.AttribType = CarPartAttribType.Integer;
		}

		/// <summary>
		/// Reads data from stream into this <see cref="CustomCP"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		public void Read(BinaryReader br)
		{
			this.Name = br.ReadNullTermUTF8();
			this.Key = br.ReadUInt32();
			this.AttribType = br.ReadEnum<CarPartAttribType>();
		}

		/// <summary>
		/// Writes data to stream using this <see cref="CustomCP"/>.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.Name);
			bw.Write(this.Key);
			bw.WriteEnum(this.AttribType);
		}

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is CustomCP cp && this == cp;

		/// <inheritdoc/>
		public override int GetHashCode() => (int)this.Key;

		/// <inheritdoc/>
		public override string ToString() => this.Name;

		/// <summary>
		/// Compares two instances of <see cref="CustomCP"/> and returns true if they are equal.
		/// </summary>
		/// <param name="cp1">First <see cref="CustomCP"/> to compare.</param>
		/// <param name="cp2">Second <see cref="CustomCP"/> to compare.</param>
		/// <returns>True if two instances are equal; false otherwise.</returns>
		public static bool operator ==(CustomCP cp1, CustomCP cp2)
		{
			if (cp1 is null) return cp2 is null;
			else if (cp2 is null) return false;

			bool result = true;
			result &= cp1.Key == cp2.Key;
			result &= cp1.AttribType == cp2.AttribType;
			result &= String.CompareOrdinal(cp1.Name, cp2.Name) == 0;
			return result;
		}

		/// <summary>
		/// Compares two instances of <see cref="CustomCP"/> and returns true if they are not equal.
		/// </summary>
		/// <param name="cp1">First <see cref="CustomCP"/> to compare.</param>
		/// <param name="cp2">Second <see cref="CustomCP"/> to compare.</param>
		/// <returns>True if two instances are not equal; false otherwise.</returns>
		public static bool operator !=(CustomCP cp1, CustomCP cp2) => !(cp1 == cp2);
	}
}
