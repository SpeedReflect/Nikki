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
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Prostreet.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with null-terminated string value.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | Value: {Value}")]
	public class StringAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="StringAttribute"/>.
		/// </summary>
		[Category("Main")]
		public override CarPartAttribType AttribType => CarPartAttribType.String;

		/// <summary>
		/// Type of this <see cref="StringAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eAttribString Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		[Category("Main")]
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribString)value;
		}

		/// <summary>
		/// Attribute value.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public string Value { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether value exists.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eBoolean ValueExists { get; set; } = eBoolean.False;

		/// <summary>
		/// Initializes new instance of <see cref="StringAttribute"/>.
		/// </summary>
		public StringAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="StringAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		public StringAttribute(object value)
		{
			try
			{

				this.Value = (string)value.ReinterpretCast(typeof(string));

			}
			catch (Exception)
			{

				this.Value = String.Empty;

			}
		}

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
			
			if (position < UInt32.MaxValue)
			{
			
				str_reader.BaseStream.Position = position << 2;
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
			bw.Write(this.Key);

			if (this.ValueExists == eBoolean.False) bw.Write(-1);
			else bw.Write(string_dict[this.Value?.GetHashCode() ?? String.Empty.GetHashCode()]);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => this.Type.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="StringAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="StringAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="StringAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is StringAttribute attribute && this == attribute;

		/// <summary>
		/// Returns the hash code for this <see cref="StringAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Tuple.Create(this.Key, this.Value, this.ValueExists.ToString()).GetHashCode();
		}

		/// <summary>
		/// Determines whether two specified <see cref="StringAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="StringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="StringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(StringAttribute at1, StringAttribute at2)
		{
			if (at1 is null) return at2 is null;
			else if (at2 is null) return false;

			return at1.Key == at2.Key && at1.ValueExists == at2.ValueExists && at1.Value == at2.Value;
		}

		/// <summary>
		/// Determines whether two specified <see cref="StringAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="StringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="StringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(StringAttribute at1, StringAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new StringAttribute
			{
				Type = this.Type,
				Value = this.Value,
				ValueExists = this.ValueExists
			};

			return result;
		}

		/// <summary>
		/// Converts this <see cref="BoolAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public override CPAttribute ConvertTo(CarPartAttribType type) =>
			type switch
			{
				CarPartAttribType.Boolean => new BoolAttribute(this.Value),
				CarPartAttribType.Floating => new FloatAttribute(this.Value),
				CarPartAttribType.Integer => new IntAttribute(this.Value),
				CarPartAttribType.TwoString => new TwoStringAttribute(this.Value),
				CarPartAttribType.Color => new ColorAttribute(this.Value),
				CarPartAttribType.CarPartID => new PartIDAttribute(this.Value),
				CarPartAttribType.Key => new KeyAttribute(this.Value),
				CarPartAttribType.ModelTable => new ModelTableAttribute(this.Value),
				_ => this
			};

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public override void Serialize(BinaryWriter bw)
		{
			bw.Write(this.Key);
			bw.WriteEnum(this.ValueExists);

			if (this.ValueExists == eBoolean.True)
			{

				bw.WriteNullTermUTF8(this.Value);

			}
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public override void Deserialize(BinaryReader br)
		{
			this.ValueExists = br.ReadEnum<eBoolean>();

			if (this.ValueExists == eBoolean.True)
			{

				this.Value = br.ReadNullTermUTF8();

			}
		}
	}
}
