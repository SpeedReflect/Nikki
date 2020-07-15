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



namespace Nikki.Support.Carbon.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with 4-byte signed integer value.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | Color: {Alpha}-{Red}-{Green}-{Blue}")]
	public class ColorAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="ColorAttribute"/>.
		/// </summary>
		[Category("Main")]
		public override CarPartAttribType AttribType => CarPartAttribType.Color;

		/// <summary>
		/// Type of this <see cref="ColorAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eAttribColor Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		[Category("Main")]
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribColor)value;
		}

		/// <summary>
		/// Alpha attribute value.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public byte Alpha { get; set; }

		/// <summary>
		/// Blue attribute value.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public byte Blue { get; set; }

		/// <summary>
		/// Green attribute value.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public byte Green { get; set; }

		/// <summary>
		/// Red attribute value.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public byte Red { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="ColorAttribute"/>.
		/// </summary>
		public ColorAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="ColorAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		public ColorAttribute(object value)
		{
			try
			{

				this.Alpha = (byte)value.ReinterpretCast(typeof(byte));
				this.Blue = this.Green = this.Red = 0;

			}
			catch (Exception)
			{

				this.Alpha = this.Blue = this.Green = this.Red = 0;
			
			}
		}

		/// <summary>
		/// Initializes new instance of <see cref="ColorAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public ColorAttribute(BinaryReader br, uint key)
		{
			this.Key = key;
			this.Disassemble(br, null);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="ColorAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
		{
			this.Red = br.ReadByte();
			this.Green = br.ReadByte();
			this.Blue = br.ReadByte();
			this.Alpha = br.ReadByte();
		}

		/// <summary>
		/// Assembles <see cref="ColorAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			bw.Write(this.Key);
			bw.Write(this.Red);
			bw.Write(this.Green);
			bw.Write(this.Blue);
			bw.Write(this.Alpha);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => this.Type.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="ColorAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="ColorAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="ColorAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is ColorAttribute attribute && this == attribute;

		/// <summary>
		/// Returns the hash code for this <see cref="ColorAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() =>
			Tuple.Create(this.Key, this.Alpha, this.Blue, this.Green, this.Red).GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="ColorAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="ColorAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="ColorAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(ColorAttribute at1, ColorAttribute at2)
		{
			if (at1 is null) return at2 is null;
			else if (at2 is null) return false;

			bool result = true;
			result &= at1.Alpha == at2.Alpha;
			result &= at1.Blue == at2.Blue;
			result &= at1.Green == at2.Green;
			result &= at1.Blue == at2.Blue;
			return at1.Key == at2.Key && result;
		}

		/// <summary>
		/// Determines whether two specified <see cref="ColorAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="ColorAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="ColorAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(ColorAttribute at1, ColorAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new ColorAttribute
			{
				Type = this.Type,
				Alpha = this.Alpha,
				Blue = this.Blue,
				Green = this.Green,
				Red = this.Red,
			};

			return result;
		}

		/// <summary>
		/// Converts this <see cref="ColorAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public override CPAttribute ConvertTo(CarPartAttribType type) =>
			type switch
			{
				CarPartAttribType.Boolean => new BoolAttribute(this.Alpha),
				CarPartAttribType.Floating => new FloatAttribute(this.Alpha),
				CarPartAttribType.Integer => new IntAttribute(this.Alpha),
				CarPartAttribType.String => new StringAttribute(this.Alpha),
				CarPartAttribType.TwoString => new TwoStringAttribute(this.Alpha),
				CarPartAttribType.CarPartID => new PartIDAttribute(this.Alpha),
				CarPartAttribType.Key => new KeyAttribute(this.Alpha),
				CarPartAttribType.ModelTable => new ModelTableAttribute(this.Alpha),
				_ => this
			};

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public override void Serialize(BinaryWriter bw)
		{
			bw.Write(this.Key);
			bw.Write(this.Red);
			bw.Write(this.Green);
			bw.Write(this.Blue);
			bw.Write(this.Alpha);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public override void Deserialize(BinaryReader br)
		{
			this.Red = br.ReadByte();
			this.Green = br.ReadByte();
			this.Blue = br.ReadByte();
			this.Alpha = br.ReadByte();
		}
	}
}
