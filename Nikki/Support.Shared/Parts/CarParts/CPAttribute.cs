using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="DBModelPart"/> unit attribute.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Key: 0x{Key:X8}")]
	public abstract class CPAttribute : SubPart
	{
		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="CPAttribute"/>.
		/// </summary>
		public abstract CarPartAttribType AttribType { get; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public abstract uint Key { get; set; }

		/// <summary>
		/// Disassembles byte array into <see cref="CPAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public abstract void Disassemble(BinaryReader br, BinaryReader str_reader);

		/// <summary>
		/// Assembles <see cref="CPAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets.</param>
		public abstract void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict);

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public abstract void Serialize(BinaryWriter bw);

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public abstract void Deserialize(BinaryReader br);

		/// <summary>
		/// Converts this <see cref="CPAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public abstract CPAttribute ConvertTo(CarPartAttribType type);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy() { return null; }

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"0x{this.Key:X8}";
	}
}