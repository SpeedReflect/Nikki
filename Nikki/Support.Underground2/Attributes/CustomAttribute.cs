using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with 4-byte signed integer value.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | Value: {Value}")]
	public class CustomAttribute : CPAttribute, ICustomAttrib
	{
		private CarPartAttribType _type;
		private string _name = "CUSTOM";

		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="CustomAttribute"/>.
		/// </summary>
		[Category("Main")]
		public override CarPartAttribType AttribType => CarPartAttribType.Custom;

		/// <summary>
		/// <see cref="CarPartAttribType"/> of the value stored in this <see cref="CustomAttribute"/>.
		/// </summary>
		[Category("Main")]
		public CarPartAttribType Type
		{
			get => this._type;
			set
			{
				switch (value)
				{

					case CarPartAttribType.CarPartID:
					case CarPartAttribType.Custom:
					case CarPartAttribType.ModelTable:
						throw new Exception("Unsupported type of custom attribute");

					default:
						this._type = value;
						return;

				}
			}
		}

		/// <summary>
		/// Name of this <see cref="CustomAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public string Name
		{
			get => this._name;
			set
			{
				if (String.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
				else this._name = value;
			}
		}

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		[Category("Main")]
		public override uint Key
		{
			get => this.Name.BinHash();
			set => this.Name = value.BinString(LookupReturn.EMPTY);
		}

		#region Values

		/// <summary>
		/// Attribute value of boolean type.
		/// </summary>
		[AccessModifiable()]
		[Category("Boolean")]
		public eBoolean ValueBoolean { get; set; }

		/// <summary>
		/// Attribute value of integer type.
		/// </summary>
		[AccessModifiable()]
		[Category("Integer")]
		public int ValueInteger { get; set; }

		/// <summary>
		/// Attribute value of floating type.
		/// </summary>
		[AccessModifiable()]
		[Category("Floating")]
		public float ValueFloating { get; set; }

		/// <summary>
		/// Attribute value of key type.
		/// </summary>
		[AccessModifiable()]
		[Category("Key")]
		public string ValueKey { get; set; } = String.Empty;

		/// <summary>
		/// Attribute value of string type.
		/// </summary>
		[AccessModifiable()]
		[Category("String")]
		public string ValueString { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether value of string type exists.
		/// </summary>
		[AccessModifiable()]
		[Category("String")]
		public eBoolean ValueStringExists { get; set; }

		/// <summary>
		/// Attribute value 1 of two-string type.
		/// </summary>
		[AccessModifiable()]
		[Category("TwoString")]
		public string ValueString1 { get; set; } = String.Empty;

		/// <summary>
		/// Attribute value of two-string type.
		/// </summary>
		[AccessModifiable()]
		[Category("TwoString")]
		public string ValueString2 { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether value 1 of two-string type exists.
		/// </summary>
		[AccessModifiable()]
		[Category("TwoString")]
		public eBoolean ValueString1Exists { get; set; }

		/// <summary>
		/// Indicates whether value 2 of two-string type exists.
		/// </summary>
		[AccessModifiable()]
		[Category("TwoString")]
		public eBoolean ValueString2Exists { get; set; }

		/// <summary>
		/// Attribute red value of color type.
		/// </summary>
		[AccessModifiable()]
		[Category("Color")]
		public byte ValueColorRed { get; set; }

		/// <summary>
		/// Attribute green value of color type.
		/// </summary>
		[AccessModifiable()]
		[Category("Color")]
		public byte ValueColorGreen { get; set; }

		/// <summary>
		/// Attribute blue value of color type.
		/// </summary>
		[AccessModifiable()]
		[Category("Color")]
		public byte ValueColorBlue { get; set; }

		/// <summary>
		/// Attribute alpha value of color type.
		/// </summary>
		[AccessModifiable()]
		[Category("Color")]
		public byte ValueColorAlpha { get; set; }

		#endregion

		/// <summary>
		/// Initializes new instance of <see cref="CustomAttribute"/>.
		/// </summary>
		public CustomAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="CustomAttribute"/> with value provided.
		/// </summary>
		/// <param name="name">Name of this attribute.</param>
		public CustomAttribute(string name)
		{
			this.Name = name ?? String.Empty;
			this.Type = CarPartAttribType.Integer;
		}

		/// <summary>
		/// Initializes new instance of <see cref="CustomAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		/// <param name="cp"><see cref="CustomCP"/> with attribute type data.</param>
		public CustomAttribute(BinaryReader br, BinaryReader str_reader, CustomCP cp)
		{
			if (cp is null) throw new ArgumentNullException(nameof(cp), "CustomCP was null");
			this.Key = cp.Key;
			this.Name = cp.Name;
			this.Type = cp.AttribType;
			this.Disassemble(br, str_reader);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="CustomAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
		{
			switch (this.Type)
			{

				case CarPartAttribType.Boolean: this.ValueBoolean = br.ReadInt32() == 0 ? eBoolean.False : eBoolean.True; return;
				case CarPartAttribType.Integer: this.ValueInteger = br.ReadInt32(); return;
				case CarPartAttribType.Floating: this.ValueFloating = br.ReadSingle(); return;
				case CarPartAttribType.Key: this.ValueKey = br.ReadUInt32().BinString(LookupReturn.EMPTY); return;
				
				case CarPartAttribType.String:
					var strPtr = br.ReadUInt32();
					if (strPtr < UInt32.MaxValue)
					{

						str_reader.BaseStream.Position = strPtr << 2;
						this.ValueString = str_reader.ReadNullTermUTF8();
						this.ValueStringExists = eBoolean.True;

					}
					return;
				
				case CarPartAttribType.TwoString:
					var twostrPtr1 = br.ReadUInt16();
					var twostrPtr2 = br.ReadUInt16();
					if (twostrPtr1 != UInt16.MaxValue)
					{

						str_reader.BaseStream.Position = twostrPtr1 << 2;
						this.ValueString1 = str_reader.ReadNullTermUTF8();
						this.ValueString1Exists = eBoolean.True;

					}
					if (twostrPtr2 != UInt16.MaxValue)
					{

						str_reader.BaseStream.Position = twostrPtr2 << 2;
						this.ValueString2 = str_reader.ReadNullTermUTF8();
						this.ValueString2Exists = eBoolean.True;

					}
					return;
				
				case CarPartAttribType.Color:
					this.ValueColorRed = br.ReadByte();
					this.ValueColorGreen = br.ReadByte();
					this.ValueColorBlue = br.ReadByte();
					this.ValueColorAlpha = br.ReadByte();
					return;
				
				default: return;

			}
		}

		/// <summary>
		/// Assembles <see cref="CustomAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets. 
		/// Since it is an Integer Attribute, this value can be <see langword="null"/>.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			bw.Write(this.Key);
			
			switch (this.Type)
			{

				case CarPartAttribType.Boolean: bw.Write(this.ValueBoolean == eBoolean.False ? 0 : 1); return;
				case CarPartAttribType.Integer: bw.Write(this.ValueInteger); return;
				case CarPartAttribType.Floating: bw.Write(this.ValueFloating); return;
				case CarPartAttribType.Key: bw.Write(this.ValueKey.BinHash()); return;

				case CarPartAttribType.String:
					if (this.ValueStringExists == eBoolean.False) bw.Write(-1);
					else bw.Write(string_dict[this.ValueString?.GetHashCode() ?? String.Empty.GetHashCode()]);
					return;

				case CarPartAttribType.TwoString:
					if (this.ValueString1Exists == eBoolean.False) bw.Write((short)-1);
					else bw.Write((ushort)string_dict[this.ValueString1?.GetHashCode() ?? String.Empty.GetHashCode()]);
					if (this.ValueString2Exists == eBoolean.False) bw.Write((short)-1);
					else bw.Write((ushort)string_dict[this.ValueString2?.GetHashCode() ?? String.Empty.GetHashCode()]);
					return;

				case CarPartAttribType.Color:
					bw.Write(this.ValueColorRed);
					bw.Write(this.ValueColorGreen);
					bw.Write(this.ValueColorBlue);
					bw.Write(this.ValueColorAlpha);
					return;

				default: bw.Write(0); return;

			}
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => this.Name.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="CustomAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="CustomAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="CustomAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is CustomAttribute attribute && this == attribute;

		/// <summary>
		/// Returns the hash code for this <see cref="CustomAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Type, this.Name);

			switch (this.Type)
			{

				case CarPartAttribType.Boolean: result = HashCode.Combine(result, this.ValueBoolean.ToString()); break;
				case CarPartAttribType.Integer: result = HashCode.Combine(result, this.ValueInteger); break;
				case CarPartAttribType.Floating: result = HashCode.Combine(result, this.ValueFloating); break;
				case CarPartAttribType.Key: result = HashCode.Combine(result, this.ValueKey); break;

				case CarPartAttribType.String:
					result = HashCode.Combine(result, this.ValueString, this.ValueStringExists.ToString());
					break;

				case CarPartAttribType.TwoString:
					result = HashCode.Combine(result, this.ValueString1, this.ValueString1Exists.ToString());
					result = HashCode.Combine(result, this.ValueString2, this.ValueString2Exists.ToString());
					break;

				case CarPartAttribType.Color:
					result = HashCode.Combine(result, this.ValueColorAlpha, this.ValueColorBlue);
					result = HashCode.Combine(result, this.ValueColorGreen, this.ValueColorRed);
					break;

				default: break;
			}

			return result;
		}

		/// <summary>
		/// Determines whether two specified <see cref="CustomAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="CustomAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="CustomAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(CustomAttribute at1, CustomAttribute at2)
		{
			if (at1 is null) return at2 is null;
			else if (at2 is null) return false;

			if (at1.Type != at2.Type) return false;

			bool result = true;
			result &= String.CompareOrdinal(at1.Name, at2.Name) == 0;
			
			switch (at1.Type)
			{

				case CarPartAttribType.Boolean: result &= at1.ValueBoolean == at2.ValueBoolean; break;
				case CarPartAttribType.Integer: result &= at1.ValueInteger == at2.ValueInteger; break;
				case CarPartAttribType.Floating: result &= at1.ValueFloating == at2.ValueFloating; break;
				case CarPartAttribType.Key: result &= String.CompareOrdinal(at1.ValueKey, at2.ValueKey) == 0; break;

				case CarPartAttribType.String:
					result &= at1.ValueStringExists == at2.ValueStringExists;
					result &= String.CompareOrdinal(at1.ValueString, at2.ValueString) == 0;
					break;

				case CarPartAttribType.TwoString:
					result &= at1.ValueString1Exists == at2.ValueString1Exists;
					result &= at1.ValueString2Exists == at2.ValueString2Exists;
					result &= String.CompareOrdinal(at1.ValueString1, at2.ValueString1) == 0;
					result &= String.CompareOrdinal(at1.ValueString2, at2.ValueString2) == 0;
					break;

				case CarPartAttribType.Color:
					result &= at1.ValueColorRed == at2.ValueColorRed;
					result &= at1.ValueColorGreen == at2.ValueColorGreen;
					result &= at1.ValueColorBlue == at2.ValueColorBlue;
					result &= at1.ValueColorAlpha == at2.ValueColorAlpha;
					break;

				default: break;
			}

			return result;
		}

		/// <summary>
		/// Determines whether two specified <see cref="CustomAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="CustomAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="CustomAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(CustomAttribute at1, CustomAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new CustomAttribute
			{
				Type = this.Type,
				Name = this.Name,
				ValueBoolean = this.ValueBoolean,
				ValueInteger = this.ValueInteger,
				ValueFloating = this.ValueFloating,
				ValueKey = this.ValueKey,
				ValueString = this.ValueString,
				ValueString1 = this.ValueString1,
				ValueString2 = this.ValueString2,
				ValueStringExists = this.ValueStringExists,
				ValueString1Exists = this.ValueString1Exists,
				ValueString2Exists = this.ValueString2Exists,
				ValueColorAlpha = this.ValueColorAlpha,
				ValueColorBlue = this.ValueColorBlue,
				ValueColorGreen = this.ValueColorGreen,
				ValueColorRed = this.ValueColorRed
			};

			return result;
		}

		/// <summary>
		/// Returns itself.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>This instance.</returns>
		public override CPAttribute ConvertTo(CarPartAttribType type) => this;

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public override void Serialize(BinaryWriter bw)
		{
			bw.Write(this.Key);
			bw.WriteNullTermUTF8(this.Name);
			bw.WriteEnum(this.Type);

			switch (this.Type)
			{

				case CarPartAttribType.Boolean: bw.WriteEnum(this.ValueBoolean); break;
				case CarPartAttribType.Integer: bw.Write(this.ValueInteger); break;
				case CarPartAttribType.Floating: bw.Write(this.ValueFloating); break;
				case CarPartAttribType.Key: bw.WriteNullTermUTF8(this.ValueKey); break;
				
				case CarPartAttribType.String:
					bw.WriteEnum(this.ValueStringExists);
					if (this.ValueStringExists == eBoolean.True) bw.WriteNullTermUTF8(this.ValueString);
					break;
				
				case CarPartAttribType.TwoString:
					bw.WriteEnum(this.ValueString1Exists);
					bw.WriteEnum(this.ValueString2Exists);
					if (this.ValueString1Exists == eBoolean.True) bw.WriteNullTermUTF8(this.ValueString1);
					if (this.ValueString2Exists == eBoolean.True) bw.WriteNullTermUTF8(this.ValueString2);
					break;

				case CarPartAttribType.Color:
					bw.Write(this.ValueColorAlpha);
					bw.Write(this.ValueColorBlue);
					bw.Write(this.ValueColorGreen);
					bw.Write(this.ValueColorRed);
					break;

				default: break;

			}
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public override void Deserialize(BinaryReader br)
		{
			this.Name = br.ReadNullTermUTF8();
			this.Type = br.ReadEnum<CarPartAttribType>();

			switch (this.Type)
			{

				case CarPartAttribType.Boolean: this.ValueBoolean = br.ReadEnum<eBoolean>(); break;
				case CarPartAttribType.Integer: this.ValueInteger = br.ReadInt32(); break;
				case CarPartAttribType.Floating: this.ValueFloating = br.ReadSingle(); break;
				case CarPartAttribType.Key: this.ValueKey = br.ReadNullTermUTF8(); break;

				case CarPartAttribType.String:
					this.ValueStringExists = br.ReadEnum<eBoolean>();
					if (this.ValueStringExists == eBoolean.True) this.ValueString = br.ReadNullTermUTF8();
					break;

				case CarPartAttribType.TwoString:
					this.ValueString1Exists = br.ReadEnum<eBoolean>();
					this.ValueString2Exists = br.ReadEnum<eBoolean>();
					if (this.ValueString1Exists == eBoolean.True) this.ValueString1 = br.ReadNullTermUTF8();
					if (this.ValueString2Exists == eBoolean.True) this.ValueString2 = br.ReadNullTermUTF8();
					break;

				case CarPartAttribType.Color:
					this.ValueColorAlpha = br.ReadByte();
					this.ValueColorBlue = br.ReadByte();
					this.ValueColorGreen = br.ReadByte();
					this.ValueColorRed = br.ReadByte();
					break;

				default: break;

			}
		}
	}
}
