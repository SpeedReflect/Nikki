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



namespace Nikki.Support.MostWanted.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with 4-byte signed integer value.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | Value: {Value}")]
	public class IntAttribute : CPAttribute
	{
		private const eCarPartAttribType _type = eCarPartAttribType.Integer;

		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="IntAttribute"/>.
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
		public eAttribInt Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[Browsable(false)]
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribInt)value;
		}

		/// <summary>
		/// Attribute value.
		/// </summary>
		[AccessModifiable()]
		public uint Value { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="IntAttribute"/>.
		/// </summary>
		public IntAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="IntAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		/// <param name="part"><see cref="RealCarPart"/> to which this part belongs to.</param>
		public IntAttribute(object value, RealCarPart part)
		{
			this.BelongsTo = part;
			try
			{
				this.Value = (uint)value.ReinterpretCast(typeof(uint));
			}
			catch (Exception)
			{
				this.Value = 0;
			}
		}

		/// <summary>
		/// Initializes new instance of <see cref="IntAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public IntAttribute(BinaryReader br, uint key)
		{
			this.Key = key;
			this.Disassemble(br, null);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="IntAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
			=> this.Value = br.ReadUInt32();

		/// <summary>
		/// Assembles <see cref="IntAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
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
		/// <see cref="IntAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="IntAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="IntAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is IntAttribute attribute && this == attribute;

		/// <summary>
		/// Returns the hash code for this <see cref="IntAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() => Tuple.Create(this.Key, this.Value).GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="IntAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="IntAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="IntAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(IntAttribute at1, IntAttribute at2)
		{
			bool v = at2 is null;
			return at1 is null ? at2 is null : !v && at1.Key == at2.Key && at1.Value == at2.Value;
		}

		/// <summary>
		/// Determines whether two specified <see cref="IntAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="IntAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="IntAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(IntAttribute at1, IntAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new IntAttribute
			{
				Type = this.Type,
				Value = this.Value
			};

			return result;
		}

		/// <summary>
		/// Converts this <see cref="IntAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public override CPAttribute ConvertTo(eCarPartAttribType type) =>
			type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.Floating => new FloatAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.String => new StringAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.TwoString => new TwoStringAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.CarPartID => new PartIDAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.Key => new KeyAttribute(this.Value, this.BelongsTo),
				_ => this
			};
	}
}
