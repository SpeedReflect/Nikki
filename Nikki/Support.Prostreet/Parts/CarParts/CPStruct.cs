using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Prostreet.Parts.CarParts
{
	/// <summary>
	/// A unit <see cref="RealCarPart"/> struct with geometry part names.
	/// </summary>
	public class CPStruct : Shared.Parts.CarParts.CPStruct
	{
		/// <summary>
		/// Indicates whether this struct should exist in the database or not.
		/// </summary>
		[AccessModifiable()]
		public override eBoolean Exists { get; set; } = eBoolean.False;

		/// <summary>
		/// If true, all strings are built using string block; otherwise they are stored as keys.
		/// </summary>
		[AccessModifiable()]
		public override eBoolean Templated { get; set; } = eBoolean.False;

		/// <summary>
		/// String that is used to be concatenated in front of geometry names.
		/// </summary>
		[AccessModifiable()]
		public override string Concatenator { get; set; } = String.Empty;

		/// <summary>
		/// True if concatenator string exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean ConcatenatorExists { get; set; } = eBoolean.False;

		/// <summary>
		/// Geometry names of the struct.
		/// </summary>
		[AccessModifiable()]
		public string[] GeometryName { get; set; } = new string[StructNamesSize];

		/// <summary>
		/// Indicates existing geometries in the struct.
		/// </summary>
		[AccessModifiable()]
		public eBoolean[] GeometryExists { get; set; } = new eBoolean[StructNamesSize];

		/// <summary>
		/// Amount of geometries stored in the struct.
		/// </summary>
		public const int StructNamesSize = 60;

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
					this.ConcatenatorExists = eBoolean.True;
				}

				// Read all geometries
				for (int a1 = 0; a1 < StructNamesSize; ++a1)
				{
					position = br.ReadUInt32();
					if (position != negative)
					{
						str_reader.BaseStream.Position = position << 2;
						this.GeometryName[a1] = str_reader.ReadNullTermUTF8();
						this.GeometryExists[a1] = eBoolean.True;
					}
				}
			}
			else
			{
				uint key = br.ReadUInt16(); // skip concatenator

				// Read all geometries
				for (int a1 = 0; a1 < StructNamesSize; ++a1)
				{
					key = br.ReadUInt32();
					if (key != negative)
					{
						this.GeometryName[a1] = key.BinString(eLookupReturn.EMPTY);
						this.GeometryExists[a1] = eBoolean.True;
					}
				}
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
			int empty = String.Empty.GetHashCode();

			if (this.Templated == eBoolean.True)
			{
				bw.Write((ushort)1);
				bw.Write(this.ConcatenatorExists == eBoolean.False
					? (ushort)negint32
					: (ushort)string_dict[this.Concatenator?.GetHashCode() ?? empty]);

				for (int a1 = 0; a1 < StructNamesSize; ++a1)
				{
					bw.Write(this.GeometryExists[a1] == eBoolean.False
						? negint32
						: string_dict[this.GeometryName[a1]?.GetHashCode() ?? empty]);
				}
			}
			else
			{
				bw.Write(0xFFFF0000);
				for (int a1 = 0; a1 < StructNamesSize; ++a1)
				{
					bw.Write(this.GeometryExists[a1] == eBoolean.False
						? negative
						: this.GeometryName[a1].BinHash());
				}
			}
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override Shared.Parts.CarParts.CPStruct PlainCopy()
		{
			var result = new CPStruct()
			{
				Exists = this.Exists,
				Concatenator = this.Concatenator,
				ConcatenatorExists = this.ConcatenatorExists,
				Templated = this.Templated
			};

			for (int a1 = 0; a1 < StructNamesSize; ++a1)
			{
				result.GeometryExists[a1] = this.GeometryExists[a1];
				result.GeometryName[a1] = this.GeometryName[a1];
			}

			return result;
		}

		/// <summary>
		/// Returns templated value and first geometry name as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"Templated: {this.Templated} | Concatenator: {this.Concatenator}";

		private bool ValueEquals(CPStruct other)
		{
			bool result = true;
			result &= this.Templated == other.Templated;
			result &= this.Concatenator == other.Concatenator;
			for (int a1 = 0; a1 < StructNamesSize; ++a1)
			{
				result &= this.GeometryExists[a1] == other.GeometryExists[a1];
				result &= this.GeometryName[a1] == other.GeometryName[a1];
			}

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

			string str = String.Empty;
			result *= this.Concatenator?.GetHashCode() ?? empty;
			for (int a1 = 0; a1 < StructNamesSize; a1 += 2)
			{
				result *= this.GeometryName[a1]?.GetHashCode() ?? empty;
				result ^= this.GeometryName[a1 + 1]?.GetHashCode() ?? empty;
				str += ((int)this.GeometryExists[a1]).ToString();
				str += ((int)this.GeometryExists[a1 + 1]).ToString();
			}

			return Tuple.Create(result, str).GetHashCode();
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
