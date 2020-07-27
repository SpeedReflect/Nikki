using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.CarParts
{
	/// <summary>
	/// A unit <see cref="RealCarPart"/> struct with geometry part names.
	/// </summary>
	[DebuggerDisplay("Templated: {Templated} | Concatenator: {Concatenator}")]
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
		/// Geometry name 1 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry0LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 2 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry0LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 3 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry0LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 4 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry0LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 1 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry1LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 2 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry1LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 3 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry1LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 4 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string Geometry1LodD { get; set; } = String.Empty;

		/// <summary>
		/// True if concatenator string exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean ConcatenatorExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 1 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry0LodAExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 2 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry0LodBExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 3 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry0LodCExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 4 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry0LodDExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 1 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry1LodAExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 2 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry1LodBExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 3 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry1LodCExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 4 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean Geometry1LodDExists { get; set; } = eBoolean.False;

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

				// Read geometry 0 lod A, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.Geometry0LodA = str_reader.ReadNullTermUTF8();
					this.Geometry0LodAExists = eBoolean.True;
				
				}

				// Read geometry 0 lod B, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.Geometry0LodB = str_reader.ReadNullTermUTF8();
					this.Geometry0LodBExists = eBoolean.True;
				
				}

				// Read geometry 0 lod C, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.Geometry0LodC = str_reader.ReadNullTermUTF8();
					this.Geometry0LodCExists = eBoolean.True;
				
				}

				// Read geometry 0 lod D, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.Geometry0LodD = str_reader.ReadNullTermUTF8();
					this.Geometry0LodDExists = eBoolean.True;
				
				}

				// Read geometry 1 lod A, if valid
				position = br.ReadUInt32();

				if (position != negative)
				{

					str_reader.BaseStream.Position = position << 2;
					this.Geometry1LodA = str_reader.ReadNullTermUTF8();
					this.Geometry1LodAExists = eBoolean.True;

				}

				// Read geometry 1 lod B, if valid
				position = br.ReadUInt32();

				if (position != negative)
				{

					str_reader.BaseStream.Position = position << 2;
					this.Geometry1LodB = str_reader.ReadNullTermUTF8();
					this.Geometry1LodBExists = eBoolean.True;

				}

				// Read geometry 1 lod C, if valid
				position = br.ReadUInt32();

				if (position != negative)
				{

					str_reader.BaseStream.Position = position << 2;
					this.Geometry1LodC = str_reader.ReadNullTermUTF8();
					this.Geometry1LodCExists = eBoolean.True;

				}

				// Read geometry 1 lod D, if valid
				position = br.ReadUInt32();

				if (position != negative)
				{

					str_reader.BaseStream.Position = position << 2;
					this.Geometry1LodD = str_reader.ReadNullTermUTF8();
					this.Geometry1LodDExists = eBoolean.True;

				}

			}
			else
			{

				uint key = br.ReadUInt16(); // skip concatenator
				
				// Read geometry 0 lod A, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.Geometry0LodA = key.BinString(LookupReturn.EMPTY);
					this.Geometry0LodAExists = eBoolean.True;
				
				}

				// Read geometry 0 lod B, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.Geometry0LodB = key.BinString(LookupReturn.EMPTY);
					this.Geometry0LodBExists = eBoolean.True;
				
				}

				// Read geometry 0 lod C, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.Geometry0LodC = key.BinString(LookupReturn.EMPTY);
					this.Geometry0LodCExists = eBoolean.True;
				
				}

				// Read geometry 0 lod D, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.Geometry0LodD = key.BinString(LookupReturn.EMPTY);
					this.Geometry0LodDExists = eBoolean.True;
				
				}

				// Read geometry 1 lod A, if valid
				key = br.ReadUInt32();

				if (key != negative)
				{

					this.Geometry1LodA = key.BinString(LookupReturn.EMPTY);
					this.Geometry1LodAExists = eBoolean.True;

				}

				// Read geometry 1 lod B, if valid
				key = br.ReadUInt32();

				if (key != negative)
				{

					this.Geometry1LodB = key.BinString(LookupReturn.EMPTY);
					this.Geometry1LodBExists = eBoolean.True;

				}

				// Read geometry 1 lod C, if valid
				key = br.ReadUInt32();

				if (key != negative)
				{

					this.Geometry1LodC = key.BinString(LookupReturn.EMPTY);
					this.Geometry1LodCExists = eBoolean.True;

				}

				// Read geometry 1 lod D, if valid
				key = br.ReadUInt32();

				if (key != negative)
				{

					this.Geometry1LodD = key.BinString(LookupReturn.EMPTY);
					this.Geometry1LodDExists = eBoolean.True;

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

				bw.Write(this.Geometry0LodAExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry0LodA?.GetHashCode() ?? empty]);

				bw.Write(this.Geometry0LodBExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry0LodB?.GetHashCode() ?? empty]);

				bw.Write(this.Geometry0LodCExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry0LodC?.GetHashCode() ?? empty]);

				bw.Write(this.Geometry0LodDExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry0LodD?.GetHashCode() ?? empty]);

				bw.Write(this.Geometry1LodAExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry1LodA?.GetHashCode() ?? empty]);

				bw.Write(this.Geometry1LodBExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry1LodB?.GetHashCode() ?? empty]);

				bw.Write(this.Geometry1LodCExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry1LodC?.GetHashCode() ?? empty]);

				bw.Write(this.Geometry1LodDExists == eBoolean.False
					? negint32
					: string_dict[this.Geometry1LodD?.GetHashCode() ?? empty]);

			}
			else
			{

				bw.Write(0xFFFF0000);

				bw.Write(this.Geometry0LodAExists == eBoolean.False
					? negative
					: this.Geometry0LodA.BinHash());

				bw.Write(this.Geometry0LodBExists == eBoolean.False
					? negative 
					: this.Geometry0LodB.BinHash());

				bw.Write(this.Geometry0LodCExists == eBoolean.False
					? negative
					: this.Geometry0LodC.BinHash());
				
				bw.Write(this.Geometry0LodDExists == eBoolean.False
					? negative
					: this.Geometry0LodD.BinHash());

				bw.Write(this.Geometry1LodAExists == eBoolean.False
					? negative
					: this.Geometry1LodA.BinHash());

				bw.Write(this.Geometry1LodBExists == eBoolean.False
					? negative
					: this.Geometry1LodB.BinHash());

				bw.Write(this.Geometry1LodCExists == eBoolean.False
					? negative
					: this.Geometry1LodC.BinHash());

				bw.Write(this.Geometry1LodDExists == eBoolean.False
					? negative
					: this.Geometry1LodD.BinHash());

			}
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new CPStruct();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Returns templated value and first geometry name as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"LodStruct";

		private bool ValueEquals(CPStruct other)
		{
			if (this.Templated != other.Templated) return false;

			if (this.Templated == eBoolean.True)
			{

				bool result = true;
				result &= this.Concatenator == other.Concatenator;
				result &= this.ConcatenatorExists == other.ConcatenatorExists;
				if (!result) return false;

				result &= this.Geometry0LodA == other.Geometry0LodA;
				result &= this.Geometry0LodB == other.Geometry0LodB;
				result &= this.Geometry0LodC == other.Geometry0LodC;
				result &= this.Geometry0LodD == other.Geometry0LodD;
				result &= this.Geometry1LodA == other.Geometry1LodA;
				result &= this.Geometry1LodB == other.Geometry1LodB;
				result &= this.Geometry1LodC == other.Geometry1LodC;
				result &= this.Geometry1LodD == other.Geometry1LodD;
				result &= this.Geometry0LodAExists == other.Geometry0LodAExists;
				result &= this.Geometry0LodBExists == other.Geometry0LodBExists;
				result &= this.Geometry0LodCExists == other.Geometry0LodCExists;
				result &= this.Geometry0LodDExists == other.Geometry0LodDExists;
				result &= this.Geometry1LodAExists == other.Geometry1LodAExists;
				result &= this.Geometry1LodBExists == other.Geometry1LodBExists;
				result &= this.Geometry1LodCExists == other.Geometry1LodCExists;
				result &= this.Geometry1LodDExists == other.Geometry1LodDExists;
				return result;

			}
			else
			{

				bool result = true;
				result &= this.Geometry0LodA.BinHash() == other.Geometry0LodA.BinHash();
				result &= this.Geometry0LodB.BinHash() == other.Geometry0LodB.BinHash();
				result &= this.Geometry0LodC.BinHash() == other.Geometry0LodC.BinHash();
				result &= this.Geometry0LodD.BinHash() == other.Geometry0LodD.BinHash();
				result &= this.Geometry1LodA.BinHash() == other.Geometry1LodA.BinHash();
				result &= this.Geometry1LodB.BinHash() == other.Geometry1LodB.BinHash();
				result &= this.Geometry1LodC.BinHash() == other.Geometry1LodC.BinHash();
				result &= this.Geometry1LodD.BinHash() == other.Geometry1LodD.BinHash();
				result &= this.Geometry0LodAExists == other.Geometry0LodAExists;
				result &= this.Geometry0LodBExists == other.Geometry0LodBExists;
				result &= this.Geometry0LodCExists == other.Geometry0LodCExists;
				result &= this.Geometry0LodDExists == other.Geometry0LodDExists;
				result &= this.Geometry1LodAExists == other.Geometry1LodAExists;
				result &= this.Geometry1LodBExists == other.Geometry1LodBExists;
				result &= this.Geometry1LodCExists == other.Geometry1LodCExists;
				result &= this.Geometry1LodDExists == other.Geometry1LodDExists;
				return result;

			}
		}

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="CPStruct"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="CPStruct"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="CPStruct"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) => obj is CPStruct @struct && this == @struct;

		/// <summary>
		/// Returns the hash code for this <see cref="CPStruct"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = (this.Templated == eBoolean.True) ? 87 : -87;

			if (this.Templated == eBoolean.True)
			{

				result = HashCode.Combine(result, this.Concatenator);
				result = HashCode.Combine(result, this.ConcatenatorExists);
				result = HashCode.Combine(result, this.Geometry0LodA);
				result = HashCode.Combine(result, this.Geometry0LodB);
				result = HashCode.Combine(result, this.Geometry0LodC);
				result = HashCode.Combine(result, this.Geometry0LodD);
				result = HashCode.Combine(result, this.Geometry1LodA);
				result = HashCode.Combine(result, this.Geometry1LodB);
				result = HashCode.Combine(result, this.Geometry1LodC);
				result = HashCode.Combine(result, this.Geometry1LodD);

			}
			else
			{

				result = HashCode.Combine(result, this.Geometry0LodA.BinHash());
				result = HashCode.Combine(result, this.Geometry0LodB.BinHash());
				result = HashCode.Combine(result, this.Geometry0LodC.BinHash());
				result = HashCode.Combine(result, this.Geometry0LodD.BinHash());
				result = HashCode.Combine(result, this.Geometry1LodA.BinHash());
				result = HashCode.Combine(result, this.Geometry1LodB.BinHash());
				result = HashCode.Combine(result, this.Geometry1LodC.BinHash());
				result = HashCode.Combine(result, this.Geometry1LodD.BinHash());

			}

			string str = String.Empty;
			str += ((int)this.Geometry0LodAExists).ToString();
			str += ((int)this.Geometry0LodBExists).ToString();
			str += ((int)this.Geometry0LodCExists).ToString();
			str += ((int)this.Geometry0LodDExists).ToString();
			str += ((int)this.Geometry1LodAExists).ToString();
			str += ((int)this.Geometry1LodBExists).ToString();
			str += ((int)this.Geometry1LodCExists).ToString();
			str += ((int)this.Geometry1LodDExists).ToString();

			return Tuple.Create(result, str).GetHashCode();
		}

		/// <summary>
		/// Determines whether two specified <see cref="CPStruct"/> have the same value.
		/// </summary>
		/// <param name="cp1">The first <see cref="CPStruct"/> to compare, or null.</param>
		/// <param name="cp2">The second <see cref="CPStruct"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(CPStruct cp1, CPStruct cp2)
		{
			if (cp1 is null) return cp2 is null;
			else if (cp2 is null) return false;

			return cp1.ValueEquals(cp2);
		}

		/// <summary>
		/// Determines whether two specified <see cref="CPStruct"/> have different values.
		/// </summary>
		/// <param name="cp1">The first <see cref="CPStruct"/> to compare, or null.</param>
		/// <param name="cp2">The second <see cref="CPStruct"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(CPStruct cp1, CPStruct cp2) => !(cp1 == cp2);

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteEnum(this.Exists);
			if (this.Exists == eBoolean.False) return;

			bw.WriteEnum(this.Templated);
			bw.WriteEnum(this.ConcatenatorExists);
			if (this.ConcatenatorExists == eBoolean.True) bw.WriteNullTermUTF8(this.Concatenator);

			bw.WriteEnum(this.Geometry0LodAExists);
			bw.WriteEnum(this.Geometry0LodBExists);
			bw.WriteEnum(this.Geometry0LodCExists);
			bw.WriteEnum(this.Geometry0LodDExists);
			bw.WriteEnum(this.Geometry1LodAExists);
			bw.WriteEnum(this.Geometry1LodBExists);
			bw.WriteEnum(this.Geometry1LodCExists);
			bw.WriteEnum(this.Geometry1LodDExists);

			if (this.Geometry0LodAExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry0LodA);
			if (this.Geometry0LodBExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry0LodB);
			if (this.Geometry0LodCExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry0LodC);
			if (this.Geometry0LodDExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry0LodD);
			if (this.Geometry1LodAExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry1LodA);
			if (this.Geometry1LodBExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry1LodB);
			if (this.Geometry1LodCExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry1LodC);
			if (this.Geometry1LodDExists == eBoolean.True) bw.WriteNullTermUTF8(this.Geometry1LodD);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.Exists = br.ReadEnum<eBoolean>();
			if (this.Exists == eBoolean.False) return;

			this.Templated = br.ReadEnum<eBoolean>();
			this.ConcatenatorExists = br.ReadEnum<eBoolean>();
			if (this.ConcatenatorExists == eBoolean.True) this.Concatenator = br.ReadNullTermUTF8();

			this.Geometry0LodAExists = br.ReadEnum<eBoolean>();
			this.Geometry0LodBExists = br.ReadEnum<eBoolean>();
			this.Geometry0LodCExists = br.ReadEnum<eBoolean>();
			this.Geometry0LodDExists = br.ReadEnum<eBoolean>();
			this.Geometry1LodAExists = br.ReadEnum<eBoolean>();
			this.Geometry1LodBExists = br.ReadEnum<eBoolean>();
			this.Geometry1LodCExists = br.ReadEnum<eBoolean>();
			this.Geometry1LodDExists = br.ReadEnum<eBoolean>();

			if (this.Geometry0LodAExists == eBoolean.True) this.Geometry0LodA = br.ReadNullTermUTF8();
			if (this.Geometry0LodBExists == eBoolean.True) this.Geometry0LodB = br.ReadNullTermUTF8();
			if (this.Geometry0LodCExists == eBoolean.True) this.Geometry0LodC = br.ReadNullTermUTF8();
			if (this.Geometry0LodDExists == eBoolean.True) this.Geometry0LodD = br.ReadNullTermUTF8();
			if (this.Geometry1LodAExists == eBoolean.True) this.Geometry1LodA = br.ReadNullTermUTF8();
			if (this.Geometry1LodBExists == eBoolean.True) this.Geometry1LodB = br.ReadNullTermUTF8();
			if (this.Geometry1LodCExists == eBoolean.True) this.Geometry1LodC = br.ReadNullTermUTF8();
			if (this.Geometry1LodDExists == eBoolean.True) this.Geometry1LodD = br.ReadNullTermUTF8();
		}
	}
}
