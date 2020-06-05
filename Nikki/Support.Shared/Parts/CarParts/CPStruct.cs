using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A unit <see cref="RealCarPart"/> struct with geometry part names.
	/// </summary>
	public abstract class CPStruct : SubPart
	{
		/// <summary>
		/// Indicates whether this struct should exist in the database or not.
		/// </summary>
		public abstract eBoolean Exists { get; set; }

		/// <summary>
		/// If true, all names are places in the string block; otherwise, all 
		/// hashes of the names are stored in the table.
		/// </summary>
		public abstract eBoolean Templated { get; set; }

		/// <summary>
		/// Main concatenator string, if exists.
		/// </summary>
		public abstract string Concatenator { get; set; }

		/// <summary>
		/// Disassembles byte array into <see cref="CPStruct"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public abstract void Disassemble(BinaryReader br, BinaryReader str_reader);

		/// <summary>
		/// Assembles <see cref="CPStruct"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary with string HashCodes and their offsets.</param>
		public abstract void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy() { return null; }
	}
}
