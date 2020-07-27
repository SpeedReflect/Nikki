using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Parts.CarParts
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
		public string GeometryLodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 2 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string GeometryLodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 3 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string GeometryLodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 4 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string GeometryLodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry name 5 of this <see cref="CPStruct"/>.
		/// </summary>
		[AccessModifiable()]
		public string GeometryLodE { get; set; } = String.Empty;

		/// <summary>
		/// True if concatenator string exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean ConcatenatorExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 1 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean GeometryLodAExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 2 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean GeometryLodBExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 3 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean GeometryLodCExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 4 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean GeometryLodDExists { get; set; } = eBoolean.False;

		/// <summary>
		/// True if geometry 5 exists; false othewise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean GeometryLodEExists { get; set; } = eBoolean.False;

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

				// Read geometry lod A, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.GeometryLodA = str_reader.ReadNullTermUTF8();
					this.GeometryLodAExists = eBoolean.True;
				
				}

				// Read geometry lod B, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.GeometryLodB = str_reader.ReadNullTermUTF8();
					this.GeometryLodBExists = eBoolean.True;
				
				}

				// Read geometry lod C, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.GeometryLodC = str_reader.ReadNullTermUTF8();
					this.GeometryLodCExists = eBoolean.True;
				
				}

				// Read geometry lod D, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.GeometryLodD = str_reader.ReadNullTermUTF8();
					this.GeometryLodDExists = eBoolean.True;
				
				}

				// Read geometry lod E, if valid
				position = br.ReadUInt32();
				
				if (position != negative)
				{
				
					str_reader.BaseStream.Position = position << 2;
					this.GeometryLodE = str_reader.ReadNullTermUTF8();
					this.GeometryLodEExists = eBoolean.True;
				
				}

			}
			else
			{

				uint key = br.ReadUInt16(); // skip concatenator
				
				// Read geometry lod A, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.GeometryLodA = key.BinString(LookupReturn.EMPTY);
					this.GeometryLodAExists = eBoolean.True;
				
				}

				// Read geometry lod B, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.GeometryLodB = key.BinString(LookupReturn.EMPTY);
					this.GeometryLodBExists = eBoolean.True;
				
				}

				// Read geometry lod C, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.GeometryLodC = key.BinString(LookupReturn.EMPTY);
					this.GeometryLodCExists = eBoolean.True;
				
				}

				// Read geometry lod D, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.GeometryLodD = key.BinString(LookupReturn.EMPTY);
					this.GeometryLodDExists = eBoolean.True;
				
				}

				// Read geometry lod E, if valid
				key = br.ReadUInt32();
				
				if (key != negative)
				{
				
					this.GeometryLodE = key.BinString(LookupReturn.EMPTY);
					this.GeometryLodEExists = eBoolean.True;
				
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

				bw.Write(this.GeometryLodAExists == eBoolean.False
					? negint32
					: string_dict[this.GeometryLodA?.GetHashCode() ?? empty]);

				bw.Write(this.GeometryLodBExists == eBoolean.False
					? negint32
					: string_dict[this.GeometryLodB?.GetHashCode() ?? empty]);

				bw.Write(this.GeometryLodCExists == eBoolean.False
					? negint32
					: string_dict[this.GeometryLodC?.GetHashCode() ?? empty]);

				bw.Write(this.GeometryLodDExists == eBoolean.False
					? negint32
					: string_dict[this.GeometryLodD?.GetHashCode() ?? empty]);

				bw.Write(this.GeometryLodEExists == eBoolean.False
					? negint32
					: string_dict[this.GeometryLodE?.GetHashCode() ?? empty]);
			
			}
			else
			{

				bw.Write(0xFFFF0000);

				bw.Write(this.GeometryLodAExists == eBoolean.False
					? negative
					: this.GeometryLodA.BinHash());

				bw.Write(this.GeometryLodBExists == eBoolean.False
					? negative 
					: this.GeometryLodB.BinHash());

				bw.Write(this.GeometryLodCExists == eBoolean.False
					? negative
					: this.GeometryLodC.BinHash());
				
				bw.Write(this.GeometryLodDExists == eBoolean.False
					? negative
					: this.GeometryLodD.BinHash());
				
				bw.Write(this.GeometryLodEExists == eBoolean.False
					? negative
					: this.GeometryLodE.BinHash());
			
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

				result &= this.GeometryLodA == other.GeometryLodA;
				result &= this.GeometryLodB == other.GeometryLodB;
				result &= this.GeometryLodC == other.GeometryLodC;
				result &= this.GeometryLodD == other.GeometryLodD;
				result &= this.GeometryLodE == other.GeometryLodE;
				result &= this.GeometryLodAExists == other.GeometryLodAExists;
				result &= this.GeometryLodBExists == other.GeometryLodBExists;
				result &= this.GeometryLodCExists == other.GeometryLodCExists;
				result &= this.GeometryLodDExists == other.GeometryLodDExists;
				result &= this.GeometryLodEExists == other.GeometryLodEExists;
				return result;

			}
			else
			{

				bool result = true;
				result &= this.GeometryLodA.BinHash() == other.GeometryLodA.BinHash();
				result &= this.GeometryLodB.BinHash() == other.GeometryLodB.BinHash();
				result &= this.GeometryLodC.BinHash() == other.GeometryLodC.BinHash();
				result &= this.GeometryLodD.BinHash() == other.GeometryLodD.BinHash();
				result &= this.GeometryLodE.BinHash() == other.GeometryLodE.BinHash();
				result &= this.GeometryLodAExists == other.GeometryLodAExists;
				result &= this.GeometryLodBExists == other.GeometryLodBExists;
				result &= this.GeometryLodCExists == other.GeometryLodCExists;
				result &= this.GeometryLodDExists == other.GeometryLodDExists;
				result &= this.GeometryLodEExists == other.GeometryLodEExists;
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
				result = HashCode.Combine(result, this.GeometryLodA);
				result = HashCode.Combine(result, this.GeometryLodB);
				result = HashCode.Combine(result, this.GeometryLodC);
				result = HashCode.Combine(result, this.GeometryLodD);
				result = HashCode.Combine(result, this.GeometryLodE);

			}
			else
			{

				result = HashCode.Combine(result, this.GeometryLodA.BinHash());
				result = HashCode.Combine(result, this.GeometryLodB.BinHash());
				result = HashCode.Combine(result, this.GeometryLodC.BinHash());
				result = HashCode.Combine(result, this.GeometryLodD.BinHash());
				result = HashCode.Combine(result, this.GeometryLodE.BinHash());

			}

			string str = String.Empty;
			str += ((int)this.GeometryLodAExists).ToString();
			str += ((int)this.GeometryLodBExists).ToString();
			str += ((int)this.GeometryLodCExists).ToString();
			str += ((int)this.GeometryLodDExists).ToString();
			str += ((int)this.GeometryLodEExists).ToString();

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

			bw.WriteEnum(this.GeometryLodAExists);
			bw.WriteEnum(this.GeometryLodBExists);
			bw.WriteEnum(this.GeometryLodCExists);
			bw.WriteEnum(this.GeometryLodDExists);
			bw.WriteEnum(this.GeometryLodEExists);

			if (this.GeometryLodAExists == eBoolean.True) bw.WriteNullTermUTF8(this.GeometryLodA);
			if (this.GeometryLodBExists == eBoolean.True) bw.WriteNullTermUTF8(this.GeometryLodB);
			if (this.GeometryLodCExists == eBoolean.True) bw.WriteNullTermUTF8(this.GeometryLodC);
			if (this.GeometryLodDExists == eBoolean.True) bw.WriteNullTermUTF8(this.GeometryLodD);
			if (this.GeometryLodEExists == eBoolean.True) bw.WriteNullTermUTF8(this.GeometryLodE);
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

			this.GeometryLodAExists = br.ReadEnum<eBoolean>();
			this.GeometryLodBExists = br.ReadEnum<eBoolean>();
			this.GeometryLodCExists = br.ReadEnum<eBoolean>();
			this.GeometryLodDExists = br.ReadEnum<eBoolean>();
			this.GeometryLodEExists = br.ReadEnum<eBoolean>();

			if (this.GeometryLodAExists == eBoolean.True) this.GeometryLodA = br.ReadNullTermUTF8();
			if (this.GeometryLodBExists == eBoolean.True) this.GeometryLodB = br.ReadNullTermUTF8();
			if (this.GeometryLodCExists == eBoolean.True) this.GeometryLodC = br.ReadNullTermUTF8();
			if (this.GeometryLodDExists == eBoolean.True) this.GeometryLodD = br.ReadNullTermUTF8();
			if (this.GeometryLodEExists == eBoolean.True) this.GeometryLodE = br.ReadNullTermUTF8();
		}
	}
}
