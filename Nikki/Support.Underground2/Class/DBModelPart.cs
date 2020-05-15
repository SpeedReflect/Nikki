using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.Reflection;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Class
{
	/// <summary>
	/// <see cref="DBModelPart"/> is a collection of car parts of a specific model.
	/// </summary>
	public class DBModelPart : Shared.Class.DBModelPart
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground2;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground2.ToString();

		/// <summary>
		/// Database to which the class belongs to.
		/// </summary>
		public Database.Underground2 Database { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				if (String.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("This value cannot be left empty.");
				if (value.Contains(" "))
					throw new Exception("CollectionName cannot contain whitespace.");
				if (this.Database.ModelParts.FindCollection(value) != null)
					throw new CollectionExistenceException();
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// List of <see cref="RealCarPart"/>.
		/// </summary>
		public override List<RealCarPart> ModelCarParts { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="DBModelPart"/>.
		/// </summary>
		public DBModelPart() { }

		/// <summary>
		/// Initializes new instance of <see cref="DBModelPart"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public DBModelPart(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			this.ModelCarParts = new List<RealCarPart>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Resorts all names according to their indexed position.
		/// </summary>
		public override void ResortNames()
		{
			for (int a1 = 0; a1 < this.ModelCarParts.Count; ++a1)
				this.ModelCarParts[a1].PartName = $"{this._collection_name}_PART_{a1}";
		}

		/// <summary>
		/// Switches two parts and their indexes.
		/// </summary>
		/// <param name="part1">First <see cref="RealCarPart"/> to switch.</param>
		/// <param name="part2">Second <see cref="RealCarPart"/> to switch.</param>
		/// <returns>True if switching was successful; false otherwise.</returns>
		public override bool SwitchParts(string part1, string part2)
		{
			var index1 = this.ModelCarParts.FindIndex(_ => _.PartName == part1);
			var index2 = this.ModelCarParts.FindIndex(_ => _.PartName == part2);
			if (index1 == -1 || index2 == -1) return false;
			var temp1 = this.GetRealPart(index1);
			var temp2 = this.GetRealPart(index2);
			this.ModelCarParts[index2] = temp1;
			this.ModelCarParts[index1] = temp2;
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Switches two parts and their indexes.
		/// </summary>
		/// <param name="part1">First <see cref="RealCarPart"/> to switch.</param>
		/// <param name="part2">Second <see cref="RealCarPart"/> to switch.</param>
		/// <param name="error">Error occured while trying to switch.</param>
		/// <returns>True if switching was successful; false otherwise.</returns>
		public override bool SwitchParts(string part1, string part2, out string error)
		{
			error = null;
			var index1 = this.ModelCarParts.FindIndex(_ => _.PartName == part1);
			var index2 = this.ModelCarParts.FindIndex(_ => _.PartName == part2);
			if (index1 == -1)
			{
				error = $"Part named {part1} does not exist.";
				return false;
			}
			if (index2 == -1)
			{
				error = $"Part named {part2} does not exist.";
				return false;
			}
			var temp1 = this.GetRealPart(index1);
			var temp2 = this.GetRealPart(index2);
			this.ModelCarParts[index2] = temp1;
			this.ModelCarParts[index1] = temp2;
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Reverses all parts in this <see cref="DBModelPart"/>.
		/// </summary>
		public override void ReverseParts()
		{
			this.ModelCarParts.Reverse();
			this.ResortNames();
		}

		/// <summary>
		/// Sorts all parts by property name provided.
		/// </summary>
		/// <param name="property">Property to sort by.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool SortByProperty(string property)
		{
			var field = typeof(Parts.CarParts.RealCarPart).GetProperty(property);
			if (field == null) return false;
			this.ModelCarParts.Sort((x, y) =>
			{
				var valueX = x.GetFastPropertyValue(property) as IComparable;
				var valueY = y.GetFastPropertyValue(property) as IComparable;
				return valueX.CompareTo(valueY);
			});
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Sorts all parts by property name provided.
		/// </summary>
		/// <param name="property">Property to sort by.</param>
		/// <param name="error">Error occured while trying to switch.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool SortByProperty(string property, out string error)
		{
			error = null;
			var field = typeof(Parts.CarParts.RealCarPart).GetProperty(property);
			if (field == null)
			{
				error = $"Property named {property} does not exist in car parts.";
				return false;
			}
			this.ModelCarParts.Sort((x, y) =>
			{
				var valueX = x.GetFastPropertyValue(property) as IComparable;
				var valueY = y.GetFastPropertyValue(property) as IComparable;
				return valueX.CompareTo(valueY);
			});
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new DBModelPart(CName, this.Database);
			foreach (var part in this.ModelCarParts)
				result.ModelCarParts.Add(part.PlainCopy());

			return result;
		}

		/// <summary>
		/// Attemps to add new <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="name">Name of the new <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryAddRealPart(string name = null)
		{
			this.ModelCarParts.Add(new Parts.CarParts.RealCarPart(this.Index, this));
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Attemps to add new <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="error">Error occured while trying to add <see cref="RealCarPart"/>.</param>
		/// <param name="name">Name of the new <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryAddRealPart(out string error, string name)
		{
			error = null;
			this.ModelCarParts.Add(new Parts.CarParts.RealCarPart(this.Index, this));
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Attemps to remove <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="name">Name of the <see cref="RealCarPart"/> to remove.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryRemovePart(string name)
		{
			var result = this.ModelCarParts.RemoveWith(_ => _.PartName == name);
			if (result) this.ResortNames();
			return result;
		}

		/// <summary>
		/// Attemps to remove <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="name">Name of the <see cref="RealCarPart"/> to remove.</param>
		/// <param name="error">Error occured while trying to remove <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryRemovePart(string name, out string error)
		{
			error = null;
			var result = this.TryRemovePart(name);
			if (!result) error = $"Part named {name} does not exist.";
			else this.ResortNames();
			return result;
		}

		/// <summary>
		/// Attemps to clone a <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="newname">Name of the new <see cref="RealCarPart"/>.</param>
		/// <param name="copyname">Name of <see cref="RealCarPart"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryClonePart(string newname, string copyname)
		{
			var part = this.GetRealPart(copyname);
			if (part == null) return false;
			this.ModelCarParts.Add(part.PlainCopy());
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Attemps to clone a <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="newname">Name of the new <see cref="RealCarPart"/>.</param>
		/// <param name="copyname">Name of <see cref="RealCarPart"/> to clone.</param>
		/// <param name="error">Error occured while trying to clone <see cref="RealCarPart"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryClonePart(string newname, string copyname, out string error)
		{
			error = null;
			var part = this.GetRealPart(copyname);
			if (part == null)
			{
				error = $"Part named {copyname} does not exist.";
				return false;
			}
			this.ModelCarParts.Add(part.PlainCopy());
			this.ResortNames();
			return true;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="DBModelPart"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}

		#endregion
	}
}
