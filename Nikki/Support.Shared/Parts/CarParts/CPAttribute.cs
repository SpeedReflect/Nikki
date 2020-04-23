using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="DBModelPart"/> unit attribute.
	/// </summary>
	public abstract class CPAttribute
	{
		private string _part = String.Empty;
		private uint _key = 0;

		/// <summary>
		/// <see cref="eCarPartAttribType"/> type of this <see cref="CPAttribute"/>.
		/// </summary>
		public abstract eCarPartAttribType AttribType { get; }

		/// <summary>
		/// Name of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public string Part
		{
			get => this._part;
			set
			{
				this._part = value ?? String.Empty;
				this._key = value.BinHash();
			}
		}

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		public uint Key
		{
			get => this._key;
			set
			{
				this._key = value;
				this._part = value.BinString(eLookupReturn.EMPTY);
			}
		}

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
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"{this.Part} | {this.AttribType}";
	}
}