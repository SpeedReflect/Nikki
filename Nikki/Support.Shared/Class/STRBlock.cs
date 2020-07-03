using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Support.Shared.Parts.STRParts;
using CoreExtensions.Text;



namespace Nikki.Support.Shared.Class
{
	/// <summary>
	/// <see cref="STRBlock"/> is a collection of language strings, hashes and labels.
	/// </summary>
	public abstract class STRBlock : Collectable, IAssembly
	{
		#region Main Properties

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		public override string CollectionName { get; set; }

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.None;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.None.ToString();

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		public virtual uint BinKey => this.CollectionName.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		public virtual uint VltKey => this.CollectionName.VltHash();

		/// <summary>
		/// Length of the string information array.
		/// </summary>
		public abstract int StringRecordCount { get; }

		/// <summary>
		/// Custom watermark written on assembly.
		/// </summary>
		internal string Watermark { get; set; } = String.Empty;

		#endregion

		#region Methods

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Assembles <see cref="STRBlock"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="STRBlock"/> with.</param>
		public abstract void Assemble(BinaryWriter bw);

		/// <summary>
		/// Disassembles array into <see cref="STRBlock"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="STRBlock"/> with.</param>
		public abstract void Disassemble(BinaryReader br);

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public abstract void Serialize(BinaryWriter bw);

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public abstract void Deserialize(BinaryReader br);

		/// <summary>
		/// Gets the <see cref="StringRecord"/> from the internal list.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to find.</param>
		/// <returns>StringRecord is it exists; otherwise null;</returns>
		public abstract StringRecord GetRecord(uint key);

		/// <summary>
		/// Gets the <see cref="StringRecord"/> from the internal list.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to find.</param>
		/// <returns>StringRecord is it exists; otherwise null;</returns>
		public virtual StringRecord GetRecord(string key)
			=> !key.IsHexString() ? null : this.GetRecord(Convert.ToUInt32(key, 16));

		/// <summary>
		/// Gets all <see cref="StringRecord"/> stored in <see cref="STRBlock"/>.
		/// </summary>
		/// <returns><see cref="IEnumerable{T}"/> of <see cref="StringRecord"/>.</returns>
		public abstract IEnumerable<StringRecord> GetRecords();

		/// <summary>
		/// Gets text from the binary key of a label provided.
		/// </summary>
		/// <param name="key">Key of the string label.</param>
		/// <returns>Text of the label as a string.</returns>
		public virtual string GetText(uint key) => this.GetRecord(key)?.Text;

		/// <summary>
		/// Gets text from the binary key of a label provided.
		/// </summary>
		/// <param name="key">Key of the string label.</param>
		/// <returns>Text of the label as a string.</returns>
		public virtual string GetText(string key) => this.GetRecord(key)?.Text;

		/// <summary>
		/// Adds <see cref="StringRecord"/> in the <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="key">Key of the new <see cref="StringRecord"/></param>
		/// <param name="label">Label of the new <see cref="StringRecord"/></param>
		/// <param name="text">Text of the new <see cref="StringRecord"/></param>
		public abstract void AddRecord(string key, string label, string text);

		/// <summary>
		/// Removes <see cref="StringRecord"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		public abstract void RemoveRecord(uint key);

		/// <summary>
		/// Removes <see cref="StringRecord"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		public abstract void RemoveRecord(string key);

		/// <summary>
		/// Retrieves all <see cref="StringRecord"/> that have their texts containing text provided.
		/// </summary>
		/// <param name="text">Text that other <see cref="StringRecord"/> should match.</param>
		/// <returns>Enumerable of records containing text provided.</returns>
		public abstract IEnumerable<StringRecord> FindWithText(string text);

		/// <summary>
		/// Sorts all <see cref="StringRecord"/> by their BinKey value.
		/// </summary>
		public abstract void SortRecordsByKey();

		/// <summary>
		/// Sorts all <see cref="StringRecord"/> by their Label value.
		/// </summary>
		public abstract void SortRecordsByLabel();

		/// <summary>
		/// Sorts all <see cref="StringRecord"/> by their Text value.
		/// </summary>
		public abstract void SortRecordsByText();

		#endregion
	}
}