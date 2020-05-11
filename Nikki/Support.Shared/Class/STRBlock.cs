using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Parts.STRParts;
using CoreExtensions.Text;



namespace Nikki.Support.Shared.Class
{
	/// <summary>
	/// <see cref="STRBlock"/> is a collection of language strings, hashes and labels.
	/// </summary>
	public abstract class STRBlock : ACollectable
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
		public abstract int InfoLength { get; }

		internal static string Watermark { get; set; } = String.Empty;

		#endregion

		#region Methods

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
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
		/// Attempts to add <see cref="StringRecord"/> in the <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="key">Key of the new <see cref="StringRecord"/></param>
		/// <param name="label">Label of the new <see cref="StringRecord"/></param>
		/// <param name="text">Text of the new <see cref="StringRecord"/></param>
		/// <returns>True if adding was successful; false otherwise.</returns>
		public abstract bool TryAddRecord(string key, string label, string text);

		/// <summary>
		/// Attempts to add <see cref="StringRecord"/> in the <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="key">Key of the new <see cref="StringRecord"/></param>
		/// <param name="label">Label of the new <see cref="StringRecord"/></param>
		/// <param name="text">Text of the new <see cref="StringRecord"/></param>
		/// <param name="error">Error occured when trying to add the record.</param>
		/// <returns>True if adding was successful; false otherwise.</returns>
		public abstract bool TryAddRecord(string key, string label, string text, out string error);

		/// <summary>
		/// Attempts to remove <see cref="StringRecord"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		/// <returns>True if removing was successful; false otherwise.</returns>
		public abstract bool TryRemoveRecord(uint key);

		/// <summary>
		/// Attempts to remove <see cref="StringRecord"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		/// <returns>True if removing was successful; false otherwise.</returns>
		public abstract bool TryRemoveRecord(string key);

		/// <summary>
		/// Attempts to remove <see cref="StringRecord"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		/// <param name="error">Error occured when trying to remove the record.</param>
		/// <returns>True if removing was successful; false otherwise.</returns>
		public abstract bool TryRemoveRecord(uint key, out string error);

		/// <summary>
		/// Attempts to remove <see cref="StringRecord"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		/// <param name="error">Error occured when trying to remove the record.</param>
		/// <returns>True if removing was successful; false otherwise.</returns>
		public abstract bool TryRemoveRecord(string key, out string error);

		/// <summary>
		/// Retrieves all <see cref="StringRecord"/> that have their texts containing text provided.
		/// </summary>
		/// <param name="text">Text that other <see cref="StringRecord"/> should match.</param>
		/// <returns>Enumerable of records containing text provided.</returns>
		public abstract IEnumerable<StringRecord> FindWithText(string text);

		#endregion
	}
}