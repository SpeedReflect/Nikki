using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.CarParts
{
	/// <summary>
	/// A unit <see cref="RealCarPart"/> struct with geometry part names.
	/// </summary>
	public class CPStruct : Shared.Parts.CarParts.CPStruct
	{
		/// <summary>
		/// Indicates whether this struct should exist in the database or not.
		/// </summary>
		public override eBoolean Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// If true, all strings are built using string block; otherwise they are stored as keys.
		/// </summary>
		public override eBoolean Templated { get; set; } = eBoolean.False;

		/// <summary>
		/// String that is used to be concatenated in front of geometry names.
		/// </summary>
		public override string Concatenator { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 1 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName1 { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 2 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName2 { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 3 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName3 { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 4 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName4 { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 5 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName5 { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 6 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName6 { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 7 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName7 { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 8 of this <see cref="CPStruct"/>.
		/// </summary>
		public string GeometryName8 { get; set; } = String.Empty;

		/// <summary>
		/// Initialized new instance of <see cref="CPStruct"/>.
		/// </summary>
		public CPStruct() { }

		/// <summary>
		/// Initializes new instance of <see cref="CPStruct"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public CPStruct(BinaryReader br, BinaryReader str_reader)
		{
			this.Exists = eBoolean.True;
			this.Disassemble(br, str_reader);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="CPStruct"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader)
		{
			const uint negative = 0xFFFFFFFF;
			this.Templated = br.ReadInt16() == 0 ? eBoolean.False : eBoolean.True;
			if (this.Templated == eBoolean.True)
			{
				// Read concatenator
				long position = br.ReadUInt16();
				if (position != 0xFFFF)
				{
					str_reader.BaseStream.Position = position << 2;
					this.Concatenator = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 1, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName1 = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 2, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName2 = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 3, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName3 = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 4, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName4 = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 5, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName5 = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 6, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName6 = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 7, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName7 = str_reader.ReadNullTermUTF8();
				}

				// Read geometry name 8, if valid
				position = br.ReadUInt32();
				if (position != negative)
				{
					str_reader.BaseStream.Position = position << 2;
					this.GeometryName8 = str_reader.ReadNullTermUTF8();
				}
			}
			else
			{
				uint key = br.ReadUInt16(); // skip concatenator
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName1 = key.BinString(eLookupReturn.EMPTY);
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName2 = key.BinString(eLookupReturn.EMPTY);
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName3 = key.BinString(eLookupReturn.EMPTY);
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName4 = key.BinString(eLookupReturn.EMPTY);
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName5 = key.BinString(eLookupReturn.EMPTY);
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName6 = key.BinString(eLookupReturn.EMPTY);
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName7 = key.BinString(eLookupReturn.EMPTY);
				key = br.ReadUInt32();
				if (key != negative) this.GeometryName8 = key.BinString(eLookupReturn.EMPTY);
			}
		}

		/// <summary>
		/// Assembles <see cref="CPStruct"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary with string HashCodes and their offsets.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			uint negative = 0xFFFFFFFF;
			int negint32 = -1;
			if (this.Templated == eBoolean.True)
			{
				bw.Write((ushort)1);

				bw.Write(String.IsNullOrEmpty(this.Concatenator)
					? (ushort)negint32
					: (ushort)string_dict[this.Concatenator.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName1)
					? negint32
					: string_dict[this.GeometryName1.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName2)
					? negint32
					: string_dict[this.GeometryName2.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName3)
					? negint32
					: string_dict[this.GeometryName3.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName4)
					? negint32
					: string_dict[this.GeometryName4.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName5)
					? negint32
					: string_dict[this.GeometryName5.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName6)
					? negint32
					: string_dict[this.GeometryName6.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName7)
					? negint32
					: string_dict[this.GeometryName7.GetHashCode()]);

				bw.Write(String.IsNullOrEmpty(this.GeometryName8)
					? negint32
					: string_dict[this.GeometryName8.GetHashCode()]);
			}
			else
			{
				bw.Write(0xFFFF0000);

				bw.Write(String.IsNullOrEmpty(this.GeometryName1)
					? negative
					: this.GeometryName1.BinHash());

				bw.Write(String.IsNullOrEmpty(this.GeometryName2)
					? negative 
					: this.GeometryName2.BinHash());

				bw.Write(String.IsNullOrEmpty(this.GeometryName3)
					? negative
					: this.GeometryName3.BinHash());
				
				bw.Write(String.IsNullOrEmpty(this.GeometryName4)
					? negative
					: this.GeometryName4.BinHash());
				
				bw.Write(String.IsNullOrEmpty(this.GeometryName5)
					? negative
					: this.GeometryName5.BinHash());
				
				bw.Write(String.IsNullOrEmpty(this.GeometryName6)
					? negative
					: this.GeometryName6.BinHash());
				
				bw.Write(String.IsNullOrEmpty(this.GeometryName7)
					? negative
					: this.GeometryName7.BinHash());
				
				bw.Write(String.IsNullOrEmpty(this.GeometryName8)
					? negative
					: this.GeometryName8.BinHash());
			}
		}

		/// <summary>
		/// Returns templated value and first geometry name as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"Templated: {this.Templated} | GeometryName1: {this.GeometryName1}";

		private bool ValueEquals(CPStruct other)
		{
			bool result = true;
			result &= this.Templated == other.Templated;
			result &= this.Concatenator == other.Concatenator;
			result &= this.GeometryName1 == other.GeometryName1;
			result &= this.GeometryName2 == other.GeometryName2;
			result &= this.GeometryName3 == other.GeometryName3;
			result &= this.GeometryName4 == other.GeometryName4;
			result &= this.GeometryName5 == other.GeometryName5;
			result &= this.GeometryName6 == other.GeometryName6;
			result &= this.GeometryName7 == other.GeometryName7;
			result &= this.GeometryName8 == other.GeometryName8;
			return result;
		}

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="CPStruct"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="CPStruct"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="CPStruct"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) => obj is CPStruct && this == (CPStruct)obj;

		/// <summary>
		/// Returns the hash code for this <see cref="CPStruct"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = (this.Templated == eBoolean.True) ? 87 : -87;
			int empty = String.Empty.GetHashCode();
			result *= this.Concatenator?.GetHashCode() ?? empty;
			result *= this.GeometryName1?.GetHashCode() ?? empty;
			result ^= this.GeometryName2?.GetHashCode() ?? empty;
			result *= this.GeometryName3?.GetHashCode() ?? empty;
			result ^= this.GeometryName4?.GetHashCode() ?? empty;
			result *= this.GeometryName5?.GetHashCode() ?? empty;
			result ^= this.GeometryName6?.GetHashCode() ?? empty;
			result *= this.GeometryName7?.GetHashCode() ?? empty;
			result ^= this.GeometryName8?.GetHashCode() ?? empty;
			return result;
		}

		/// <summary>
		/// Determines whether two specified <see cref="CPStruct"/> have the same value.
		/// </summary>
		/// <param name="cp1">The first <see cref="CPStruct"/> to compare, or null.</param>
		/// <param name="cp2">The second <see cref="CPStruct"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(CPStruct cp1, CPStruct cp2) =>
			cp1 is null ? cp2 is null : cp2 is null ? false : cp1.ValueEquals(cp2);

		/// <summary>
		/// Determines whether two specified <see cref="CPStruct"/> have different values.
		/// </summary>
		/// <param name="cp1">The first <see cref="CPStruct"/> to compare, or null.</param>
		/// <param name="cp2">The second <see cref="CPStruct"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(CPStruct cp1, CPStruct cp2) => !(cp1 == cp2);
	}
}
