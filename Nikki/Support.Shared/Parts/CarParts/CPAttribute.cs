using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Class;
using Nikki.Reflection.Interface;
using CoreExtensions.Reflection;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="DBModelPart"/> unit attribute.
	/// </summary>
	public abstract class CPAttribute : ASubPart, ICopyable<CPAttribute>
	{
		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="CPAttribute"/>.
		/// </summary>
		public abstract eCarPartAttribType AttribType { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public abstract uint Key { get; set; }

		/// <summary>
		/// <see cref="RealCarPart"/> to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public RealCarPart BelongsTo { get; set; }

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
		/// Converts this <see cref="CPAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public abstract CPAttribute ConvertTo(eCarPartAttribType type);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public virtual CPAttribute PlainCopy() { return null; }

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"0x{this.Key:X8} | {this.AttribType}";
	}
}