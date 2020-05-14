using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Interface;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A unit CarPart attribute of <see cref="DBModelPart"/>.
	/// </summary>
	public abstract class RealCarPart : ICopyable<RealCarPart>
	{
		/// <summary>
		/// Name of this <see cref="RealCarPart"/>.
		/// </summary>
		public abstract string PartName { get; set; }

		/// <summary>
		/// Collection of <see cref="CPAttribute"/> of this <see cref="RealCarPart"/>.
		/// </summary>
		public virtual List<CPAttribute> Attributes { get; set; }

		/// <summary>
		/// Total amount of <see cref="CPAttribute"/> in this <see cref="RealCarPart"/>.
		/// </summary>
		public virtual int Length => this.Attributes.Count;

		/// <summary>
		/// Index of <see cref="DBModelPart"/> to which this part belongs to.
		/// </summary>
		public abstract int Index { get; set; }

		/// <summary>
		/// Gets <see cref="CPAttribute"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of a <see cref="CPAttribute"/> to find.</param>
		/// <returns>A <see cref="CPAttribute"/> with key provided.</returns>
		public virtual CPAttribute GetAttribute(uint key) => this.Attributes.Find(_ => _.Key == key);

		/// <summary>
		/// Gets <see cref="CPAttribute"/> with the label provided.
		/// </summary>
		/// <param name="label">Label of a <see cref="CPAttribute"/> to find.</param>
		/// <returns>A <see cref="CPAttribute"/> with label provided.</returns>
		public virtual CPAttribute GetAttribute(string label) => this.GetAttribute(label.BinHash());

		/// <summary>
		/// Gets <see cref="CPAttribute"/> at index specified.
		/// </summary>
		/// <param name="index">Index in the list of <see cref="CPAttribute"/>.</param>
		/// <returns>A <see cref="CPAttribute"/> at index specified.</returns>
		public virtual CPAttribute GetAttribute(int index) =>
			(index >= 0 && index < this.Length) ? this.Attributes[index] : null;

		/// <summary>
		/// Gets index of an attribute that has key provided.
		/// </summary>
		/// <param name="key">Key of an attribute.</param>
		/// <returns>Index of an attribute.</returns>
		public virtual int GetIndex(uint key) => this.Attributes.FindIndex(_ => _.Key == key);

		/// <summary>
		/// Gets index of an attribute that has label provided.
		/// </summary>
		/// <param name="label">Label of an attribute.</param>
		/// <returns>Index of an attribute.</returns>
		public virtual int GetIndex(string label) => this.GetIndex(label.BinHash());

		/// <summary>
		/// Gets index of an attribute provided.
		/// </summary>
		/// <param name="attrib"><see cref="CPAttribute"/> to find index of.</param>
		/// <returns>Index of an attribute provided.</returns>
		public virtual int GetIndex(CPAttribute attrib) => this.Attributes.IndexOf(attrib);

		/// <summary>
		/// Attempts to add <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryAddAttribute(uint key);

		/// <summary>
		/// Attempts to add <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryAddAttribute(string label);

		/// <summary>
		/// Attempts to add <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="error">Error occured when trying to add new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryAddAttribute(uint key, out string error);

		/// <summary>
		/// Attempts to add <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="error">Error occured when trying to add new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryAddAttribute(string label, out string error);

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="CPAttribute"/> to remove.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryRemoveAttribute(uint key);

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="label">Label of the <see cref="CPAttribute"/> to remove.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryRemoveAttribute(string label);

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="CPAttribute"/> to remove.</param>
		/// <param name="error">Error occured when trying to remove <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryRemoveAttribute(uint key, out string error);

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the <see cref="CPAttribute"/> to remove.</param>
		/// <param name="error">Error occured when trying to remove <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryRemoveAttribute(string label, out string error);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(uint newkey, uint copykey);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(uint newkey, string copylabel);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(string newlabel, uint copykey);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(string newlabel, string copylabel);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(uint newkey, uint copykey, out string error);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(uint newkey, string copylabel, out string error);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(string newlabel, uint copykey, out string error);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryCloneAttribute(string newlabel, string copylabel, out string error);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public virtual RealCarPart PlainCopy() { return null; }
	}
}
