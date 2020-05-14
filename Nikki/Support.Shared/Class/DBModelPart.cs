using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Parts.CarParts;



namespace Nikki.Support.Shared.Class
{
	/// <summary>
	/// <see cref="DBModelPart"/> is a collection of car parts of a specific model.
	/// </summary>
	public abstract class DBModelPart : ACollectable
	{
		#region Main Properties

		/// <summary>
		/// Index of this <see cref="DBModelPart"/> in the database.
		/// </summary>
		public int Index { get; set; }

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
		/// List of <see cref="RealCarPart"/> that this <see cref="DBModelPart"/> has.
		/// </summary>
		public abstract List<RealCarPart> ModelCarParts { get; set; }

		/// <summary>
		/// Total amount of <see cref="RealCarPart"/> in this <see cref="DBModelPart"/>.
		/// </summary>
		public virtual int Length => this.ModelCarParts.Count;

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
		/// Gets <see cref="RealCarPart"/> that has name provided.
		/// </summary>
		/// <param name="name">Name of the <see cref="RealCarPart"/> to get.</param>
		/// <returns><see cref="RealCarPart"/> with name provided.</returns>
		public virtual RealCarPart GetRealPart(string name) =>
			this.ModelCarParts.Find(_ => _.PartName == name);

		/// <summary>
		/// Gets <see cref="RealCarPart"/> at index specified.
		/// </summary>
		/// <param name="index">Index of the <see cref="RealCarPart"/> to get.</param>
		/// <returns><see cref="RealCarPart"/> at index specified.</returns>
		public virtual RealCarPart GetRealPart(int index) =>
			(index >= 0 && index < this.Length) ? this.ModelCarParts[index] : null;

		/// <summary>
		/// Resorts all names based on their indexing.
		/// </summary>
		public abstract void ResortNames();

		/// <summary>
		/// Switches two parts and their indexes.
		/// </summary>
		/// <param name="part1">First <see cref="RealCarPart"/> to switch.</param>
		/// <param name="part2">Second <see cref="RealCarPart"/> to switch.</param>
		/// <returns>True if switching was successful; false otherwise.</returns>
		public abstract bool SwitchParts(string part1, string part2);

		/// <summary>
		/// Switches two parts and their indexes.
		/// </summary>
		/// <param name="part1">First <see cref="RealCarPart"/> to switch.</param>
		/// <param name="part2">Second <see cref="RealCarPart"/> to switch.</param>
		/// <param name="error">Error occured while trying to switch.</param>
		/// <returns>True if switching was successful; false otherwise.</returns>
		public abstract bool SwitchParts(string part1, string part2, out string error);

		/// <summary>
		/// Reverses all parts in this <see cref="DBModelPart"/>.
		/// </summary>
		public abstract void ReverseParts();

		/// <summary>
		/// Sorts all parts by property name provided.
		/// </summary>
		/// <param name="property">Property to sort by.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool SortByProperty(string property);

		/// <summary>
		/// Sorts all parts by property name provided.
		/// </summary>
		/// <param name="property">Property to sort by.</param>
		/// <param name="error">Error occured while trying to switch.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool SortByProperty(string property, out string error);

		/// <summary>
		/// Attemps to add new <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="name">Name of the new <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryAddRealPart(string name);

		/// <summary>
		/// Attemps to add new <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="error">Error occured while trying to add <see cref="RealCarPart"/>.</param>
		/// <param name="name">Name of the new <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryAddRealPart(out string error, string name);

		/// <summary>
		/// Attemps to remove <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="name">Name of the <see cref="RealCarPart"/> to remove.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryRemovePart(string name);

		/// <summary>
		/// Attemps to remove <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="name">Name of the <see cref="RealCarPart"/> to remove.</param>
		/// <param name="error">Error occured while trying to remove <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryRemovePart(string name, out string error);

		/// <summary>
		/// Attemps to clone a <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="newname">Name of the new <see cref="RealCarPart"/>.</param>
		/// <param name="copyname">Name of <see cref="RealCarPart"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryClonePart(string newname, string copyname);

		/// <summary>
		/// Attemps to clone a <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="newname">Name of the new <see cref="RealCarPart"/>.</param>
		/// <param name="copyname">Name of <see cref="RealCarPart"/> to clone.</param>
		/// <param name="error">Error occured while trying to clone <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public abstract bool TryClonePart(string newname, string copyname, out string error);

		#endregion
	}
}
