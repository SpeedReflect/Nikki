using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with two null-terminated string values.
	/// </summary>
	public class TwoStringAttribute : CPAttribute
	{
		private const eCarPartAttribType _type = eCarPartAttribType.TwoString;

		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="StringAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		public override eCarPartAttribType AttribType
		{
			get => _type;
			set
			{
				var index = this.BelongsTo.GetIndex(this);
				this.BelongsTo.Attributes[index] = this.ConvertTo(value);
			}
		}

		/// <summary>
		/// Type of this <see cref="BoolAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		public eAttribTwoString Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribTwoString)value;
		}

		/// <summary>
		/// Attribute value 1.
		/// </summary>
		[AccessModifiable()]
		public string Value1 { get; set; } = String.Empty;

		/// <summary>
		/// Attribute value 2.
		/// </summary>
		[AccessModifiable()]
		public string Value2 { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether value 1 exists.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Value1Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// Indicates whether value 2 exists.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Value2Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// Initializes new instance of <see cref="TwoStringAttribute"/>.
		/// </summary>
		public TwoStringAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="TwoStringAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		/// <param name="part"><see cref="RealCarPart"/> to which this part belongs to.</param>
		public TwoStringAttribute(object value, RealCarPart part)
		{
			this.BelongsTo = part;
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
		/// Initializes new instance of <see cref="StringAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="string_dict">Dictionary of offsets and strings.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public TwoStringAttribute(BinaryReader br, Dictionary<int, string> string_dict, uint key)
		{
			this.Key = key;
			this.Disassemble(br, string_dict);
		}

		private void Disassemble(BinaryReader br, Dictionary<int, string> string_dict)
		{
			ushort position;
			position = br.ReadUInt16();
			if (position < 0xFFFF && string_dict.TryGetValue(position, out var value1))
			{
				this.Value1 = value1;
				this.Value1Exists = eBoolean.True;
			}
			position = br.ReadUInt16();
			if (position < 0xFFFF && string_dict.TryGetValue(position, out var value2))
			{
				this.Value2 = value2;
				this.Value2Exists = eBoolean.True;
			}
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
				str_reader.BaseStream.Position = position << 2;
				this.Value1 = str_reader.ReadNullTermUTF8();
				this.Value1Exists = eBoolean.True;
			}
			position = br.ReadUInt16();
			if (position != 0xFFFF)
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
				? 0xFFFF
				: (ushort)string_dict[this.Value1.GetHashCode()];
			var result2 = this.Value2Exists == eBoolean.False
				? 0xFFFF
				: (ushort)string_dict[this.Value2.GetHashCode()];
			bw.Write(this.Key);
			bw.Write((ushort)result1);
			bw.Write((ushort)result2);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"{this.AttribType} -> {this.Type}";

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

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override CPAttribute PlainCopy()
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
		public override CPAttribute ConvertTo(eCarPartAttribType type) =>
			type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(this.Value1, this.BelongsTo),
				eCarPartAttribType.Floating => new FloatAttribute(this.Value1, this.BelongsTo),
				eCarPartAttribType.Integer => new IntAttribute(this.Value1, this.BelongsTo),
				eCarPartAttribType.String => new StringAttribute(this.Value1, this.BelongsTo),
				eCarPartAttribType.CarPartID => new PartIDAttribute(this.Value1, this.BelongsTo),
				eCarPartAttribType.Key => new KeyAttribute(this.Value1, this.BelongsTo),
				eCarPartAttribType.ModelTable => new ModelTableAttribute(this.Value1, this.BelongsTo),
				_ => this
			};
	}
}
