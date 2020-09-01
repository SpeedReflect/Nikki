using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground1.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with 4-byte floating point value.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | Value: {Value}")]
	public class FloatAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="FloatAttribute"/>.
		/// </summary>
		[Category("Main")]
		public override CarPartAttribType AttribType => CarPartAttribType.Floating;

		/// <summary>
		/// Type of this <see cref="FloatAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eAttribFloat Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		[Category("Main")]
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribFloat)value;
		}

		/// <summary>
		/// Attribute value.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public float Value { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="CPAttribute"/>.
		/// </summary>
		public FloatAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="FloatAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		public FloatAttribute(object value)
		{
			try
			{

				this.Value = (float)value.ReinterpretCast(typeof(float));

			}
			catch (Exception)
			{

				this.Value = 0;

			}
		}

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
		public override string ToString() => this.Type.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="FloatAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="FloatAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="FloatAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is FloatAttribute attribute && this == attribute;

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
		public static bool operator ==(FloatAttribute at1, FloatAttribute at2)
		{
			if (at1 is null) return at2 is null;
			else if (at2 is null) return false;

			return at1.Key == at2.Key && at1.Value == at2.Value;
		}

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
		public override SubPart PlainCopy()
		{
			var result = new FloatAttribute
			{
				Type = this.Type,
				Value = this.Value
			};

			return result;
		}

		/// <summary>
		/// Converts this <see cref="FloatAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public override CPAttribute ConvertTo(CarPartAttribType type) =>
			type switch
			{
				CarPartAttribType.Boolean => new BoolAttribute(this.Value),
				CarPartAttribType.Integer => new IntAttribute(this.Value),
				CarPartAttribType.String => new StringAttribute(this.Value),
				CarPartAttribType.Key => new KeyAttribute(this.Value),
				_ => this
			};

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public override void Serialize(BinaryWriter bw)
		{
			bw.Write(this.Key);
			bw.Write(this.Value);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public override void Deserialize(BinaryReader br) =>
			this.Value = br.ReadSingle();
	}
}
