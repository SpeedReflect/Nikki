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

		public virtual RealCarPart GetRealPart(string name) =>
			this.ModelCarParts.Find(_ => _.PartName == name);

		public virtual RealCarPart GetRealPart(int index) =>
			(index >= 0 && index < this.Length) ? this.ModelCarParts[index] : null;

		public abstract void ResortNames();

		#endregion
	}
}
