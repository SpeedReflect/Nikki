﻿using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with 4-byte signed integer value.
	/// </summary>
	public class KeyAttribute : CPAttribute
	{
		private const eCarPartAttribType _type = eCarPartAttribType.Key;

		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="KeyAttribute"/>.
		/// </summary>
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
		public eAttribKey Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribKey)value;
		}

		/// <summary>
		/// Attribute value.
		/// </summary>
		[AccessModifiable()]
		public string Value { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="KeyAttribute"/>.
		/// </summary>
		public KeyAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="KeyAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		/// <param name="part"><see cref="RealCarPart"/> to which this part belongs to.</param>
		public KeyAttribute(object value, RealCarPart part)
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
		/// Initializes new instance of <see cref="KeyAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public KeyAttribute(BinaryReader br, uint key)
		{
			this.Key = key;
			this.Disassemble(br, null);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="KeyAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
			=> this.Value = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

		/// <summary>
		/// Assembles <see cref="KeyAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			bw.Write(this.Key);
			bw.Write(this.Value.BinHash());
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"{this.AttribType} -> {this.Type}";

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="KeyAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="KeyAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="KeyAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is KeyAttribute && this == (KeyAttribute)obj;

		/// <summary>
		/// Returns the hash code for this <see cref="KeyAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() => Tuple.Create(this.Key, this.Value).GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="KeyAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="KeyAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="KeyAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(KeyAttribute at1, KeyAttribute at2) =>
			at1 is null ? at2 is null : at2 is null ? false
			: (at1.Key == at2.Key && at1.Value == at2.Value);

		/// <summary>
		/// Determines whether two specified <see cref="KeyAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="KeyAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="KeyAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(KeyAttribute at1, KeyAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override CPAttribute PlainCopy()
		{
			var result = new KeyAttribute
			{
				Type = this.Type,
				Value = this.Value
			};

			return result;
		}

		/// <summary>
		/// Converts this <see cref="KeyAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public override CPAttribute ConvertTo(eCarPartAttribType type) =>
			type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.Floating => new FloatAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.Integer => new IntAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.String => new StringAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.TwoString => new TwoStringAttribute(this.Value, this.BelongsTo),
				eCarPartAttribType.CarPartID => new PartIDAttribute(this.Value, this.BelongsTo),
				_ => this
			};
	}
}
