using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with two null-terminated string values.
	/// </summary>
	public class TwoStringAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="StringAttribute"/>.
		/// </summary>
		public override eCarPartAttribType AttribType => eCarPartAttribType.TwoString;

		/// <summary>
		/// Attribute value 1.
		/// </summary>
		public string Value1 { get; set; }

		/// <summary>
		/// Attribute value 2.
		/// </summary>
		public string Value2 { get; set; }

		/// <summary>
		/// Indicates whether value 1 exists.
		/// </summary>
		public eBoolean Value1Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// Indicates whether value 2 exists.
		/// </summary>
		public eBoolean Value2Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// Initializes new instance of <see cref="TwoStringAttribute"/>.
		/// </summary>
		public TwoStringAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="TwoStringAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public TwoStringAttribute(BinaryReader br, BinaryReader str_reader, uint key)
		{
			this.Key = key;
			this.Disassemble(br, str_reader);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="TwoStringAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
		{
			ushort position;
			position = br.ReadUInt16();
			if (position != 0xFFFF)
			{
				str_reader.BaseStream.Position = position * 4;
				this.Value1 = str_reader.ReadNullTermUTF8();
				this.Value1Exists = eBoolean.True;
			}
			position = br.ReadUInt16();
			if (position != 0xFFFF)
			{
				str_reader.BaseStream.Position = position * 4;
				this.Value2 = str_reader.ReadNullTermUTF8();
				this.Value2Exists = eBoolean.True;
			}
		}

		/// <summary>
		/// Assembles <see cref="TwoStringAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			var result1 = this.Value1Exists == eBoolean.False
				? 0xFFFF
				: (ushort)string_dict[this.Value1.GetHashCode()];
			var result2 = this.Value2Exists == eBoolean.False
				? 0xFFFF
				: (ushort)string_dict[this.Value2.GetHashCode()];
			bw.Write(this.Key);
			bw.Write(result1);
			bw.Write(result2);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => base.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="TwoStringAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="TwoStringAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="TwoStringAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is TwoStringAttribute && this == (TwoStringAttribute)obj;

		/// <summary>
		/// Returns the hash code for this <see cref="TwoStringAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = Tuple.Create(this.Key, this.Value1, this.Value2).GetHashCode();
			return result * $"{this.Value1Exists}{this.Value2Exists}".GetHashCode();
		}

		/// <summary>
		/// Determines whether two specified <see cref="TwoStringAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(TwoStringAttribute at1, TwoStringAttribute at2) =>
			at1 is null ? at2 is null : at2 is null ? false
			: (at1.Key == at2.Key && at1.Value1 == at2.Value1 && at1.Value2 == at2.Value2);

		/// <summary>
		/// Determines whether two specified <see cref="TwoStringAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(TwoStringAttribute at1, TwoStringAttribute at2) => !(at1 == at2);
	}
}
