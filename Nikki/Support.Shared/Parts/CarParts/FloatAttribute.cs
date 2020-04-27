using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with 4-byte floating point value.
	/// </summary>
	public class FloatAttribute : CPAttribute, ICopyable<FloatAttribute>
	{
		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="FloatAttribute"/>.
		/// </summary>
		public override eCarPartAttribType AttribType => eCarPartAttribType.Floating;

		/// <summary>
		/// Attribute value.
		/// </summary>
		[AccessModifiable()]
		public float Value { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="CPAttribute"/>.
		/// </summary>
		public FloatAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="FloatAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public FloatAttribute(BinaryReader br, uint key)
		{
			this.Key = key;
			this.Disassemble(br, null);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="FloatAttribute"/> using <see cref="BinaryReader"/>  
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with. 
		/// Since it is a Floating Attribute, this value can be <see langword="null"/>.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
			=> this.Value = br.ReadSingle();

		/// <summary>
		/// Assembles <see cref="FloatAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets. 
		/// Since it is a Floating Attribute, this value can be <see langword="null"/>.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			bw.Write(this.Key);
			bw.Write(this.Value);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => base.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="FloatAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="FloatAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="FloatAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is FloatAttribute && this == (FloatAttribute)obj;

		/// <summary>
		/// Returns the hash code for this <see cref="FloatAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() => Tuple.Create(this.Key, this.Value).GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="FloatAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="FloatAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="FloatAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(FloatAttribute at1, FloatAttribute at2) =>
			at1 is null ? at2 is null : at2 is null ? false
			: (at1.Key == at2.Key && at1.Value == at2.Value);

		/// <summary>
		/// Determines whether two specified <see cref="FloatAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="FloatAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="FloatAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(FloatAttribute at1, FloatAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public FloatAttribute PlainCopy()
		{
			var result = new FloatAttribute
			{
				Part = this.Part,
				Value = this.Value
			};

			return result;
		}
	}
}
