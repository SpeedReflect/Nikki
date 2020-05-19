using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.Reflection;
using CoreExtensions.Management;
using CoreExtensions.Conversions;



namespace Nikki.Database
{
	/// <summary>
	/// A root of collections used in Database.
	/// </summary>
	/// <typeparam name="TypeID">An <see cref="ACollectable"/> type.</typeparam>
	public class Root<TypeID> where TypeID : ACollectable, new()
	{
		/// <summary>
		/// List of collections in this <see cref="Root{TypeID}"/>.
		/// </summary>
		public List<TypeID> Collections { get; set; }

		/// <summary>
		/// Name of the root.
		/// </summary>
		public string ThisName { get; set; }

		/// <summary>
		/// Total number of collections.
		/// </summary>
		public int Length => this.Collections.Count;

		/// <summary>
		/// Base size of a unit collection.
		/// </summary>
		public int BaseClassSize { get; }

		/// <summary>
		/// Indicates whether this root can be resized or not.
		/// </summary>
		public bool Resizable { get; }

		/// <summary>
		/// Indicates whether collections in this root can be exported and imported,
		/// </summary>
		private readonly bool importable = false;

		/// <summary>
		/// Database to which this root belongs to.
		/// </summary>
		public ABasicBase Database { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Root{TypeID}"/>.
		/// </summary>
		/// <param name="name">Name of this root.</param>
		/// <param name="basesize">BaseClassSize of a unit collection in this root.</param>
		/// <param name="resizable">True if this root can be resized; false otherwise.</param>
		/// <param name="importable">True if collections in this root can be imported and 
		/// exported; false otherwise.</param>
		/// <param name="db">Database to which this root belongs to.</param>
		public Root(string name, int basesize, bool resizable, bool importable, ABasicBase db)
		{
			this.Collections = new List<TypeID>();
			this.ThisName = name;
			this.BaseClassSize = basesize;
			this.Resizable = resizable;
			this.importable = importable;
			this.Database = db;
		}

		#region Collection Access

		/// <summary>
		/// Accesses collection by its CollectionName.
		/// </summary>
		/// <param name="CName">CollectionName of a class.</param>
		/// <returns>Collection with CollectionName specified.</returns>
		public TypeID this[string CName] => this.FindCollection(CName);
		
		/// <summary>
		/// Accesses collection by its index in the list of collections.
		/// </summary>
		/// <param name="index">Index of the collection in the list.</param>
		/// <returns>Collection by its index.</returns>
		public TypeID this[int index] => index < this.Length ? this.Collections[index] : null;
		
		/// <summary>
		/// Tries to get index of a collection in the internal list by CollectionName provided.
		/// </summary>
		/// <param name="CName">CollectionName to look for.</param>
		/// <param name="index">Index of the collection, if found.</param>
		/// <returns>True if collection with CollectionName provided exists; false otherwise.</returns>
		public bool TryGetCollectionIndex(string CName, out int index)
		{
			for (index = 0; index < this.Length; ++index)
			{
				if (this.Collections[index].CollectionName == CName)
					return true;
			}
			index = -1;
			return false;
		}
		
		/// <summary>
		/// Searches for collection with CollectionName provided.
		/// </summary>
		/// <param name="CName">CollectionName of a class to look for.</param>
		/// <returns>Collection with CollectionName provided.</returns>
		public TypeID FindCollection(string CName)
		{
			return this.Collections.Find(c => c.CollectionName == (CName ?? string.Empty));
		}
		
		/// <summary>
		/// Searches for collection with key and its type provided.
		/// </summary>
		/// <param name="key">Key of the CollectionName.</param>
		/// <param name="type"><see cref="eKeyType"/> of the key provided.</param>
		/// <returns>Collection with key provided.</returns>
		public TypeID FindCollection(uint key, eKeyType type)
		{
			switch (type)
			{
				case eKeyType.BINKEY:
					var bin = this.Collections.Find(c => c.CollectionName.BinHash() == key);
					if (bin != null) return bin;
					goto default;

				case eKeyType.VLTKEY:
					var vlt = this.Collections.Find(c => c.CollectionName.VltHash() == key);
					if (vlt != null) return vlt;
					goto default;

				case eKeyType.CUSTOM:
					throw new NotImplementedException();
				default:
					return null;
			}
		}
		
		/// <summary>
		/// Finds all classes that have exact value specified in a field provided.
		/// </summary>
		/// <param name="field">Field to compare to.</param>
		/// <param name="value">Value to compare to.</param>
		/// <returns><see cref="IEnumerable{T}"/> of all collections with value provided.</returns>
		public IEnumerable<TypeID> FindClassWithValue(string field, object value)
		{
			foreach (var obj in this.Collections)
			{
				if (obj.GetFastPropertyValue(field) == value)
					yield return obj;
			}
		}
		
		/// <summary>
		/// Tries to get collection with CollectionName specified.
		/// </summary>
		/// <param name="CName">CollectionName of a class to search for.</param>
		/// <param name="collection">Collection with CollectionName specified in case it exists.</param>
		/// <returns>True if collection exists; false otherwise.</returns>
		public bool TryGetCollection(string CName, out TypeID collection)
		{
			collection = this.FindCollection(CName);
			return collection != null;
		}

		#endregion

		#region Collection Arrangement 

		/// <summary>
		/// Attempts to switch/swap two collections.
		/// </summary>
		/// <param name="name1">CollectionName of the first collection to switch.</param>
		/// <param name="name2">CollectionName of the second collection to switch.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool SwitchCollections(string name1, string name2)
		{
			if (!this.TryGetCollectionIndex(name1, out int index1)) return false;
			if (!this.TryGetCollectionIndex(name2, out int index2)) return false;
			var temp1 = this[index1];
			var temp2 = this[index2];
			this.Collections[index1] = temp2;
			this.Collections[index2] = temp1;
			return true;
		}

		/// <summary>
		/// Attempts to switch/swap two collections.
		/// </summary>
		/// <param name="name1">CollectionName of the first collection to switch.</param>
		/// <param name="name2">CollectionName of the second collection to switch.</param>
		/// <param name="error">Error occured when trying to switch collection.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool SwitchCollections(string name1, string name2, out string error)
		{
			error = null;
			if (!this.TryGetCollectionIndex(name1, out int index1))
			{
				error = $"Collection named {name1} does not exist.";
				return false;
			}
			if (!this.TryGetCollectionIndex(name2, out int index2))
			{
				error = $"Collection named {name2} does not exist.";
				return false;
			}
			var temp1 = this[index1];
			var temp2 = this[index2];
			this.Collections[index1] = temp2;
			this.Collections[index2] = temp1;
			return true;
		}

		/// <summary>
		/// Attempts to move collection one index up.
		/// </summary>
		/// <param name="name">CollectionName of the collection to move.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool MoveCollectionUp(string name)
		{
			if (!this.TryGetCollectionIndex(name, out int index)) return false;
			var temp1 = this[index];
			var temp2 = this[index + 1];
			if (temp2 == null) return false;
			this.Collections[index + 1] = temp1;
			this.Collections[index] = temp2;
			return true;
		}

		/// <summary>
		/// Attempts to move collection one index up.
		/// </summary>
		/// <param name="name">CollectionName of the collection to move.</param>
		/// <param name="error">Error occured when trying to move collection up.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool MoveCollectionUp(string name, out string error)
		{
			error = null;
			if (!this.TryGetCollectionIndex(name, out int index))
			{
				error = $"Collection named {name} does not exist.";
				return false;
			}
			var temp1 = this[index];
			var temp2 = this[index + 1];
			if (temp2 == null)
			{
				error = $"Unable to move collection up since it has the top collection.";
				return false;
			}
			this.Collections[index + 1] = temp1;
			this.Collections[index] = temp2;
			return true;
		}

		/// <summary>
		/// Attempts to move collection one index down.
		/// </summary>
		/// <param name="name">CollectionName of the collection to move.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool MoveCollectionDown(string name)
		{
			if (!this.TryGetCollectionIndex(name, out int index)) return false;
			var temp1 = this[index];
			var temp2 = this[index - 1];
			if (temp2 == null) return false;
			this.Collections[index - 1] = temp1;
			this.Collections[index] = temp2;
			return true;
		}

		/// <summary>
		/// Attempts to move collection one index down.
		/// </summary>
		/// <param name="name">CollectionName of the collection to move.</param>
		/// <param name="error">Error occured when trying to move collection down.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool MoveCollectionDown(string name, out string error)
		{
			error = null;
			if (!this.TryGetCollectionIndex(name, out int index))
			{
				error = $"Collection named {name} does not exist.";
				return false;
			}
			var temp1 = this[index];
			var temp2 = this[index - 1];
			if (temp2 == null)
			{
				error = $"Unable to move collection up since it has the lowest collection.";
				return false;
			}
			this.Collections[index - 1] = temp1;
			this.Collections[index] = temp2;
			return true;
		}

		/// <summary>
		/// Attempts to rearrange collections using list of CollectionNames passed.
		/// </summary>
		/// <param name="names">CollectionNames to sort based on.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool RearrangeCollections(List<string> names)
		{
			if (names.Count != this.Length) return false;
			if (!names.AllUnique()) return false;
			var list = new List<TypeID>(this.Length);
			
			for (int a1 = 0; a1 < this.Length; ++a1)
			{
				var collection = this[names[a1]];
				if (collection == null) return false;
				list.Add(collection);
			}

			this.Collections = list;
			return true;
		}

		/// <summary>
		/// Attempts to rearrange collections using list of CollectionNames passed.
		/// </summary>
		/// <param name="names">CollectionNames to sort based on.</param>
		/// <param name="error">Error occured when trying to rearrange collections.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool RearrangeCollections(List<string> names, out string error)
		{
			error = null;
			if (names.Count != this.Length)
			{
				error = $"Mismatch between amount of CollectionNames passed. Unable to sort.";
				return false;
			}
			if (!names.AllUnique())
			{
				error = $"Not all CollectionNames are unique in the list passed. Unable to sort.";
				return false;
			}
			var list = new List<TypeID>(this.Length);

			for (int a1 = 0; a1 < this.Length; ++a1)
			{
				var collection = this[names[a1]];
				if (collection == null)
				{
					error = $"Collection named {names[a1]} does not exist. Unable to sort.";
					return false;
				}
				list.Add(collection);
			}

			this.Collections = list;
			return true;
		}

		#endregion

		#region Collection Statics

		/// <summary>
		/// Attempts to set value statically in all collections in this root.
		/// </summary>
		/// <param name="field">Name of the field to be modified.</param>
		/// <param name="value">Value to be set at the field specified.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool TrySetStaticValue(string field, string value)
		{
			// Works only for Collectable and StaticModifiable properties
			var property = typeof(TypeID).GetProperty(field);
			if (property == null) return false;
			if (!Attribute.IsDefined(property, typeof(StaticModifiableAttribute)))
				return false;

			foreach (var collection in this.Collections)
			{
				bool pass = collection.SetValue(field, value);
				if (!pass) return false;
			}
			return true;
		}

		/// <summary>
		/// Attempts to set value statically in all collections in this root.
		/// </summary>
		/// <param name="field">Name of the field to be modified.</param>
		/// <param name="value">Value to be set at the field specified.</param>
		/// <param name="error">Error occured when trying to set value.</param>
		/// <returns>True on success; false otherwise.</returns>
		public bool TrySetStaticValue(string field, string value, out string error)
		{
			error = null;
			var property = typeof(TypeID).GetProperty(field);
			if (property == null)
			{
				error = $"Field named {field} does not exist.";
				return false;
			}
			if (!Attribute.IsDefined(property, typeof(StaticModifiableAttribute)))
			{
				error = $"Field named {field} is not a static-modifiable field.";
				return false;
			}
			foreach (var collection in this.Collections)
			{
				bool pass = collection.SetValue(field, value, out error);
				if (!pass) return false;
			}
			return true;
		}

		#endregion

		#region Collection Methods

		/// <summary>
		/// Attempts to add class specfified to this root.
		/// </summary>
		/// <param name="value">CollectionName of the new class.</param>
		/// <returns>True if class adding was successful, false otherwise.</returns>
		public bool TryAddCollection(string value)
		{
			TypeID instance = null;

			try
			{
				if (!this.Resizable) return false;
				if (string.IsNullOrWhiteSpace(value)) return false;
				if (this.FindCollection(value) != null) return false;
				var ctor = typeof(TypeID).GetConstructor(new Type[] { typeof(string), this.Database.GetType() });
				instance = (TypeID)ctor.Invoke(new object[] { value, this.Database });
				this.Collections.Add(instance);
				return true;
			}
			catch (Exception)
			{
				instance = null;
				return false;
			}
		}

		/// <summary>
		/// Attempts to add class specfified to this root.
		/// </summary>
		/// <param name="value">CollectionName of the new class.</param>
		/// <param name="error">Error occured while trying to add class.</param>
		/// <returns>True if class adding was successful, false otherwise.</returns>
		public bool TryAddCollection(string value, out string error)
		{
			TypeID instance = null;
			error = null;

			try
			{
				if (!this.Resizable)
				{
					error = "Class collection specified is non-resizable.";
					return false;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					error = "CollectionName cannot be empty or whitespace.";
					return false;
				}
				if (this.FindCollection(value) != null)
				{
					error = $"Class with CollectionName {value} already exists.";
					return false;
				}
				var ctor = typeof(TypeID).GetConstructor(new Type[] { typeof(string), this.Database.GetType() });
				instance = (TypeID)ctor.Invoke(new object[] { value, this.Database });
				this.Collections.Add(instance);
				return true;
			}
			catch (Exception e)
			{
				error = e.GetLowestMessage();
				instance = null;
				return false;
			}
		}

		/// <summary>
		/// Attempts to remove class specfified in this root.
		/// </summary>
		/// <param name="value">CollectionName of the class to be deleted.</param>
		/// <returns>True if class removing was successful, false otherwise.</returns>
		public bool TryRemoveCollection(string value)
		{
			return !this.Resizable
				? false
				: string.IsNullOrWhiteSpace(value)
					? false
					: !this.TryGetCollection(value, out var cla)
						? false
						: !cla.Deletable
							? false
							: this.Collections.Remove(cla);
		}

		/// <summary>
		/// Attempts to remove class specfified in this root.
		/// </summary>
		/// <param name="value">CollectionName of the class to be deleted.</param>
		/// <param name="error">Error occured while trying to remove class.</param>
		/// <returns>True if class removing was successful, false otherwise.</returns>
		public bool TryRemoveCollection(string value, out string error)
		{
			error = null;
			if (!this.Resizable)
			{
				error = "Class collection specified is non-resizable.";
				return false;
			}
			if (string.IsNullOrWhiteSpace(value))
			{
				error = "Class with empty or whitespace CollectionName does not exist.";
				return false;
			}
			if (!this.TryGetCollection(value, out var cla))
			{
				error = $"Class with CollectionName {value} does not exist.";
				return false;
			}
			if (!cla.Deletable)
			{
				error = $"This collection cannot be deleted because it is important to the game.";
				return false;
			}
			bool done = this.Collections.Remove(cla);
			if (!done) error = $"Unable to remove class with CollectionName {value}.";
			return done;
		}

		/// <summary>
		/// Attempts to clone class specfified in this root.
		/// </summary>
		/// <param name="value">CollectionName of the new class.</param>
		/// <param name="copyfrom">CollectionName of the class to clone.</param>
		/// <returns>True if class cloning was successful, false otherwise.</returns>
		public bool TryCloneCollection(string value, string copyfrom)
		{
			TypeID instance = null;

			try
			{
				if (!this.Resizable) return false;
				if (string.IsNullOrWhiteSpace(value)) return false;
				if (this.FindCollection(value) != null) return false;
				if (!this.TryGetCollection(copyfrom, out var cla)) return false;

				instance = (TypeID)cla.MemoryCast(value);
				this.Collections.Add(instance);
				return true;
			}
			catch (Exception)
			{
				instance = null;
				return false;
			}
		}

		/// <summary>
		/// Attempts to clone class specfified in this root.
		/// </summary>
		/// <param name="value">CollectionName of the new class.</param>
		/// <param name="copyfrom">CollectionName of the class to clone.</param>
		/// <param name="error">Error occured while trying to copy class.</param>
		/// <returns>True if class cloning was successful, false otherwise.</returns>
		public bool TryCloneCollection(string value, string copyfrom, out string error)
		{
			TypeID instance = null;
			error = null;

			try
			{
				if (!this.Resizable)
				{
					error = "Class collection specified is non-resizable.";
					return false;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					error = "CollectionName cannot be empty or whitespace.";
					return false;
				}
				if (this.FindCollection(value) != null)
				{
					error = $"Class with CollectionName {value} already exists.";
					return false;
				}
				if (!this.TryGetCollection(copyfrom, out var cla))
				{
					error = $"Class with CollectionName {copyfrom} does not exist.";
					return false;
				}

				instance = (TypeID)cla.MemoryCast(value);
				this.Collections.Add(instance);
				return true;
			}
			catch (Exception e)
			{
				error = e.GetLowestMessage();
				instance = null;
				return false;
			}
		}

		/// <summary>
		/// Imports class by reading data provided.
		/// </summary>
		/// <param name="data">Data of the class to import.</param>
		/// <returns>True if class import was successful, false otherwise.</returns>
		public bool TryImportCollection(byte[] data)
		{
			if (!this.importable) return false;
			if (this.BaseClassSize != -1 && data.Length != this.BaseClassSize) return false;

			using var ms = new MemoryStream(data);
			using var br = new BinaryReader(ms);

			var ctor = typeof(TypeID).GetConstructor(new Type[] { typeof(BinaryReader), this.Database.GetType() });
			var instance = (TypeID)ctor.Invoke(new object[] { br, this.Database });

			if (this.FindCollection(instance.CollectionName) != null) return false;
			this.Collections.Add(instance);
			return true;
		}

		/// <summary>
		/// Imports class by reading data provided.
		/// </summary>
		/// <param name="data">Data of the class to import.</param>
		/// <param name="error">Error occured while trying to import class.</param>
		/// <returns>True if class import was successful, false otherwise.</returns>
		public bool TryImportCollection(byte[] data, out string error)
		{
			error = null;
			if (!this.importable)
			{
				error = "Class collection specified is not importable.";
				return false;
			}
			if (this.BaseClassSize != -1 && data.Length != this.BaseClassSize)
			{
				error = $"Size of the class imported is {data.Length} bytes, while should be {this.BaseClassSize} bytes.";
				return false;
			}

			using var ms = new MemoryStream(data);
			using var br = new BinaryReader(ms);

			var ctor = typeof(TypeID).GetConstructor(new Type[] { typeof(BinaryReader), this.Database.GetType() });
			var instance = (TypeID)ctor.Invoke(new object[] { br, this.Database });

			if (this.FindCollection(instance.CollectionName) != null)
			{
				error = $"Class with CollectionName {instance.CollectionName} already exists. Unable to import.";
				return false;
			}
			this.Collections.Add(instance);
			return true;
		}

		/// <summary>
		/// Exports <see cref="ACollectable"/> data to a path specified.
		/// </summary>
		/// <param name="value">CollectionName of <see cref="ACollectable"/> class.</param>
		/// <param name="filepath">Filepath where data should be exported.</param>
		/// <returns>True if class export was successful, false otherwise.</returns>
		public bool TryExportCollection(string value, string filepath)
		{
			if (!this.importable) return false;
			if (string.IsNullOrWhiteSpace(value)) return false;
			if (!this.TryGetCollection(value, out var cla)) return false;
			if (!Directory.Exists(Path.GetDirectoryName(filepath))) return false;

			var arr = (byte[])cla.GetType()
				.GetMethod("Assemble").Invoke(cla, new object[0] { });

			using (var bw = new BinaryWriter(File.Open(filepath, FileMode.Create)))
			{
				bw.Write(arr);
			}
			return true;
		}

		/// <summary>
		/// Exports <see cref="ACollectable"/> data to a path specified.
		/// </summary>
		/// <param name="value">CollectionName of <see cref="ACollectable"/> class.</param>
		/// <param name="filepath">Filepath where data should be exported.</param>
		/// <param name="error">Error occured while trying to export class.</param>
		/// <returns>True if class export was successful, false otherwise.</returns>
		public bool TryExportCollection(string value, string filepath, out string error)
		{
			error = null;
			if (!this.importable)
			{
				error = "Class collection specified is not exportable.";
				return false;
			}
			if (string.IsNullOrWhiteSpace(value))
			{
				error = "CollectionName cannot be empty or whitespace.";
				return false;
			}
			if (!this.TryGetCollection(value, out var cla))
			{
				error = $"Class with CollectionName {value} does not exist.";
				return false;
			}
			if (!Directory.Exists(Path.GetDirectoryName(filepath)))
			{
				error = $"Directory of the file path {filepath} specified does not exist.";
				return false;
			}
			var arr = (byte[])cla.GetType().GetMethod("Assemble").Invoke(cla, new object[0] { });

			using (var bw = new BinaryWriter(File.Open(filepath, FileMode.Create)))
			{
				bw.Write(arr);
			}
			return true;
		}

		#endregion

		#region Collection Reflection

		/// <summary>
		/// Gets all nodes for tree view.
		/// </summary>
		/// <returns>List of <see cref="VirtualNode"/>.</returns>
		public List<VirtualNode> GetAllNodes()
		{
			var list = new List<VirtualNode>(this.Length);
			foreach (var cla in this.Collections)
			{
				var node = new VirtualNode(cla.CollectionName)
				{
					SubNodes = cla.GetAllNodes()
				};
				list.Add(node);
			}
			return list;
		}

		#endregion

		/// <summary>
		/// Returns root name and number of collections in it.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"Root: {this.ThisName} | Count = {this.Length}";
		}
	}
}
