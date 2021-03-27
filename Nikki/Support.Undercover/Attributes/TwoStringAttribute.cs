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



namespace Nikki.Support.Undercover.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with two null-terminated string values.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | Value1: {Value1} | Value2: {Value2}")]
	public class TwoStringAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="TwoStringAttribute"/>.
		/// </summary>
		[Category("Main")]
		public override CarPartAttribType AttribType => CarPartAttribType.TwoString;

		/// <summary>
		/// Type of this <see cref="TwoStringAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eAttribTwoString Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		[Category("Main")]
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribTwoString)value;
		}

		/// <summary>
		/// Attribute value 1.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public string Value1 { get; set; } = String.Empty;

		/// <summary>
		/// Attribute value 2.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public string Value2 { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether value 1 exists.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eBoolean Value1Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// Indicates whether value 2 exists.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eBoolean Value2Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// Initializes new instance of <see cref="TwoStringAttribute"/>.
		/// </summary>
		public TwoStringAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="TwoStringAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		public TwoStringAttribute(object value)
		{
			try
			{

				this.Value1 = (string)value.ReinterpretCast(typeof(string));
				this.Value2 = String.Empty;
				this.Value1Exists = eBoolean.True;
				this.Value2Exists = eBoolean.False;

			}
			catch (Exception)
			{

				this.Value1 = String.Empty;
				this.Value2 = String.Empty;
				this.Value1Exists = eBoolean.False;
				this.Value2Exists = eBoolean.False;

			}
		}

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
			
			if (position != UInt16.MaxValue)
			{
			
				str_reader.BaseStream.Position = position << 2;
				this.Value1 = str_reader.ReadNullTermUTF8();
				this.Value1Exists = eBoolean.True;
			
			}
			
			position = br.ReadUInt16();
			
			if (position != UInt16.MaxValue)
			{
			
				str_reader.BaseStream.Position = position << 2;
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
				? UInt16.MaxValue
				: (ushort)string_dict[this.Value1.GetHashCode()];
			
			var result2 = this.Value2Exists == eBoolean.False
				? UInt16.MaxValue
				: (ushort)string_dict[this.Value2.GetHashCode()];
			
			bw.Write(this.Key);
			bw.Write((ushort)result1);
			bw.Write((ushort)result2);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => this.Type.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="TwoStringAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="TwoStringAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="TwoStringAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is TwoStringAttribute attribute && this == attribute;

		/// <summary>
		/// Returns the hash code for this <see cref="TwoStringAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int code1 = Tuple.Create(this.Value1, this.Value1Exists.ToString()).GetHashCode();
			int code2 = Tuple.Create(this.Value2, this.Value2Exists.ToString()).GetHashCode();
			return HashCode.Combine(this.Key, code1, code2);
		}

		/// <summary>
		/// Determines whether two specified <see cref="TwoStringAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(TwoStringAttribute at1, TwoStringAttribute at2)
		{
			if (at1 is null) return at2 is null;
			else if (at2 is null) return false;

			var res1 = at1.Value1Exists == at2.Value1Exists && at1.Value1 == at2.Value1;
			var res2 = at1.Value2Exists == at2.Value2Exists && at1.Value2 == at2.Value2;
			return at1.Key == at2.Key && res1 && res2;
		}

		/// <summary>
		/// Determines whether two specified <see cref="TwoStringAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="TwoStringAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(TwoStringAttribute at1, TwoStringAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new TwoStringAttribute
			{
				Type = this.Type,
				Value1 = this.Value1,
				Value2 = this.Value2,
				Value1Exists = this.Value1Exists,
				Value2Exists = this.Value2Exists
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
				CarPartAttribType.Boolean => new BoolAttribute(this.Value1),
				CarPartAttribType.Floating => new FloatAttribute(this.Value1),
				CarPartAttribType.Integer => new IntAttribute(this.Value1),
				CarPartAttribType.String => new StringAttribute(this.Value1),
				CarPartAttribType.Color => new ColorAttribute(this.Value1),
				CarPartAttribType.CarPartID => new PartIDAttribute(this.Value1),
				CarPartAttribType.Key => new KeyAttribute(this.Value1),
				CarPartAttribType.ModelTable => new ModelTableAttribute(this.Value1),
				_ => this
			};

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public override void Serialize(BinaryWriter bw)
		{
			bw.Write(this.Key);
			bw.WriteEnum(this.Value1Exists);
			bw.WriteEnum(this.Value2Exists);

			if (this.Value1Exists == eBoolean.True)
			{

				bw.WriteNullTermUTF8(this.Value1);

			}

			if (this.Value2Exists == eBoolean.True)
			{

				bw.WriteNullTermUTF8(this.Value2);

			}
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public override void Deserialize(BinaryReader br)
		{
			this.Value1Exists = br.ReadEnum<eBoolean>();
			this.Value2Exists = br.ReadEnum<eBoolean>();

			if (this.Value1Exists == eBoolean.True)
			{

				this.Value1 = br.ReadNullTermUTF8();

			}

			if (this.Value2Exists == eBoolean.True)
			{

				this.Value2 = br.ReadNullTermUTF8();

			}
		}
	}
}
