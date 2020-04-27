using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with null-terminated string value.
	/// </summary>
	public class StringAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="StringAttribute"/>.
		/// </summary>
		public override eCarPartAttribType AttribType => eCarPartAttribType.String;

		/// <summary>
		/// Attribute value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Indicates whether value exists.
		/// </summary>
		public eBoolean ValueExists { get; set; } = eBoolean.False;

		/// <summary>
		/// Initializes new instance of <see cref="StringAttribute"/>.
		/// </summary>
		public StringAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="StringAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public StringAttribute(BinaryReader br, BinaryReader str_reader, uint key)
		{
			this.Key = key;
			this.Disassemble(br, str_reader);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="StringAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
		{
			var position = br.ReadUInt32();
			if (position < 0xFFFF)
			{
				str_reader.BaseStream.Position = position * 4;
				this.Value = str_reader.ReadNullTermUTF8();
				this.ValueExists = eBoolean.True;
			}
		}

		/// <summary>
		/// Assembles <see cref="StringAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			var result = String.IsNullOrEmpty(this.Value)
				? -1
				: string_dict[this.Value.GetHashCode()];
			bw.Write(this.Key);
			bw.Write(result);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => base.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="StringAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="StringAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="StringAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is StringAttribute && this == (StringAttribute)obj;

		/// <summary>
		/// Returns the hash code for this <see cref="StringAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = Tuple.Create(this.Key, this.Value).GetHashCode();
			return result * this.ValueExists.ToString().GetHashCode();
		}

		/// <summary>
		/// Determines whether two specified <see cref="StringAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="StringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="StringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(StringAttribute at1, StringAttribute at2) =>
			at1 is null ? at2 is null : at2 is null ? false
			: (at1.Key == at2.Key && at1.Value == at2.Value);

		/// <summary>
		/// Determines whether two specified <see cref="StringAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="StringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="StringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(StringAttribute at1, StringAttribute at2) => !(at1 == at2);
	}
}
