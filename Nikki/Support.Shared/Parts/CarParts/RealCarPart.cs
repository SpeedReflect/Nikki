using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A unit CarPart attribute of <see cref="DBModelPart"/>.
	/// </summary>
	public abstract class RealCarPart : SubPart
	{
		/// <summary>
		/// Name of this <see cref="RealCarPart"/>.
		/// </summary>
		public abstract string PartName { get; set; }

		/// <summary>
		/// Collection of <see cref="CPAttribute"/> of this <see cref="RealCarPart"/>.
		/// </summary>
		public abstract List<CPAttribute> Attributes { get; set; }

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
		public abstract CPAttribute GetAttribute(uint key);

		/// <summary>
		/// Gets <see cref="CPAttribute"/> with the label provided.
		/// </summary>
		/// <param name="label">Label of a <see cref="CPAttribute"/> to find.</param>
		/// <returns>A <see cref="CPAttribute"/> with label provided.</returns>
		public abstract CPAttribute GetAttribute(string label);

		/// <summary>
		/// Gets <see cref="CPAttribute"/> at index specified.
		/// </summary>
		/// <param name="index">Index in the list of <see cref="CPAttribute"/>.</param>
		/// <returns>A <see cref="CPAttribute"/> at index specified.</returns>
		public abstract CPAttribute GetAttribute(int index);

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
		/// Adds <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the new <see cref="CPAttribute"/>.</param>
		public abstract void AddAttribute(uint key);

		/// <summary>
		/// Adds <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the new <see cref="CPAttribute"/>.</param>
		public abstract void AddAttribute(string label);

		/// <summary>
		/// Removes <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="CPAttribute"/> to remove.</param>
		public abstract void RemoveAttribute(uint key);

		/// <summary>
		/// Removes <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="label">Label of the <see cref="CPAttribute"/> to remove.</param>
		public abstract void RemoveAttribute(string label);

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		public abstract void CloneAttribute(uint newkey, uint copykey);

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		public abstract void CloneAttribute(uint newkey, string copylabel);

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		public abstract void CloneAttribute(string newlabel, uint copykey);

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		public abstract void CloneAttribute(string newlabel, string copylabel);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy() { return null; }
	}
}
