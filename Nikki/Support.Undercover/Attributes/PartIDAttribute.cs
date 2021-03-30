using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Reflection.Enum.PartID;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Undercover.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with upgrade level and part ID values.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | ID: {ID} | Level: {Level}")]
	public class PartIDAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="PartIDAttribute"/>.
		/// </summary>
		[Category("Main")]
		public override CarPartAttribType AttribType => CarPartAttribType.CarPartID;

		/// <summary>
		/// Type of this <see cref="PartIDAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eAttribPartID Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		[Category("Main")]
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribPartID)value;
		}

		/// <summary>
		/// Unknown byte value.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public byte Level { get; set; }

		/// <summary>
		/// Part ID of this <see cref="PartIDAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public PartUndercover ID { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="PartIDAttribute"/>.
		/// </summary>
		public PartIDAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="PartIDAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		public PartIDAttribute(object value)
		{
			try
			{

				this.Level = (byte)value.ReinterpretCast(typeof(byte));
				this.ID = PartUndercover.INVALID;

			}
			catch (Exception)
			{

				this.ID = PartUndercover.INVALID;
				this.Level = 0;

			}
		}

		/// <summary>
		/// Initializes new instance of <see cref="PartIDAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public PartIDAttribute(BinaryReader br, uint key)
		{
			this.Key = key;
			this.Disassemble(br, null);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="PartIDAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
		{
			this.Level = br.ReadByte();
			this.ID = br.ReadEnum<PartUndercover>();
			br.BaseStream.Position += 2;
		}

		/// <summary>
		/// Assembles <see cref="PartIDAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			bw.Write(this.Key);
			bw.Write(this.Level);
			bw.WriteEnum(this.ID);
			bw.Write((ushort)0);
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => this.Type.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="PartIDAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="PartIDAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="PartIDAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is PartIDAttribute attribute && this == attribute;

		/// <summary>
		/// Returns the hash code for this <see cref="PartIDAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() =>
			Tuple.Create(this.Key, this.ID.ToString(), this.Level.ToString()).GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="PartIDAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="PartIDAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="PartIDAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(PartIDAttribute at1, PartIDAttribute at2)
		{
			if (at1 is null) return at2 is null;
			else if (at2 is null) return false;

			return at1.Key == at2.Key && at1.ID == at2.ID && at1.Level == at2.Level;
		}

		/// <summary>
		/// Determines whether two specified <see cref="PartIDAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="PartIDAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="PartIDAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(PartIDAttribute at1, PartIDAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PartIDAttribute
			{
				Type = this.Type,
				ID = this.ID,
				Level = this.Level
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
				CarPartAttribType.Boolean => new BoolAttribute(this.ID),
				CarPartAttribType.Floating => new FloatAttribute(this.ID),
				CarPartAttribType.Integer => new IntAttribute(this.ID),
				CarPartAttribType.String => new StringAttribute(this.ID),
				CarPartAttribType.TwoString => new TwoStringAttribute(this.ID),
				CarPartAttribType.Color => new ColorAttribute(this.ID),
				CarPartAttribType.Key => new KeyAttribute(this.ID),
				CarPartAttribType.ModelTable => new ModelTableAttribute(this.ID),
				_ => this
			};

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public override void Serialize(BinaryWriter bw)
		{
			bw.Write(this.Key);
			bw.Write(this.Level);
			bw.WriteEnum(this.ID);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public override void Deserialize(BinaryReader br)
		{
			this.Level = br.ReadByte();
			this.ID = br.ReadEnum<PartUndercover>();
		}
	}
}