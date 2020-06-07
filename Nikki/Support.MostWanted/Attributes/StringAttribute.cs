using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.MostWanted.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with null-terminated string value.
	/// </summary>
	public class StringAttribute : CPAttribute
	{
		private const eCarPartAttribType _type = eCarPartAttribType.String;

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
		public eAttribString Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribString)value;
		}

		/// <summary>
		/// Attribute value.
		/// </summary>
		[AccessModifiable()]
		public string Value { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether value exists.
		/// </summary>
		[AccessModifiable()]
		public eBoolean ValueExists { get; set; } = eBoolean.False;

		/// <summary>
		/// Initializes new instance of <see cref="StringAttribute"/>.
		/// </summary>
		public StringAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="StringAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		/// <param name="part"><see cref="RealCarPart"/> to which this part belongs to.</param>
		public StringAttribute(object value, RealCarPart part)
		{
			this.BelongsTo = part;
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
		/// Initializes new instance of <see cref="StringAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="string_dict">Dictionary of offsets and strings.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public StringAttribute(BinaryReader br, Dictionary<int, string> string_dict, uint key)
		{
			this.Key = key;
			this.Disassemble(br, string_dict);
		}

		private void Disassemble(BinaryReader br, Dictionary<int, string> string_dict)
		{
			var position = br.ReadUInt32();

			if (position < 0xFFFFFFFF && string_dict.TryGetValue((int)position, out var value))
			{
			
				this.Value = value;
				this.ValueExists = eBoolean.True;
			
			}
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
		public override string ToString() => $"Attribute: {this.AttribType} | Type: {this.Type} | Value: {this.Value}";

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
		public override CPAttribute ConvertTo(eCarPartAttribType type) =>
			type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.Floating => new FloatAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.Integer => new IntAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.TwoString => new TwoStringAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.CarPartID => new PartIDAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.Key => new KeyAttribute(this.Value, this.BelongsTo),
				_ => this
			};
	}
}
