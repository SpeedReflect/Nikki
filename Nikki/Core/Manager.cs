using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.Management;



namespace Nikki.Core
{
	/// <summary>
	/// A generic <see cref="IManager"/> class that is used to manage and maintain 
	/// <see cref="Collectable"/> classes.
	/// </summary>
	/// <typeparam name="T"><see cref="Type"/> derived from <see cref="Collectable"/> type.</typeparam>
	[DebuggerDisplay("Name = {Name} | Count = {Count}")]
	public abstract class Manager<T> : IManager, IList<T> where T : Collectable, new()
	{
		#region Fields

		private T[] _collections;
		private int _size = 0;
		private T[] _empty = new T[0];
		private int _extender = 1;

		#endregion

		#region Main

		internal Manager(FileBase db) : this() => this.Database = db;

		/// <summary>
		/// Initializes new instance of <see cref="Manager{T}"/> with default initial capacity.
		/// </summary>
		public Manager()
		{
			this._collections = this._empty;
		}

		/// <summary>
		/// Initializes new instance of <see cref="Manager{T}"/> with initial capacity specified.
		/// </summary>
		/// <param name="capacity">The number of elements that the new manager can initially store.</param>
		public Manager(int capacity)
		{
			this._collections = capacity <= 0 ? this._empty : (new T[capacity]);
		}

		/// <summary>
		/// Initializes new instance of <see cref="Manager{T}"/> with initial capacity specified.
		/// </summary>
		/// <param name="capacity">The number of elements that the new manager can initially store.</param>
		/// <param name="extender">Specifies by how much elements capacity should be extended when 
		/// it reaches its limit. If extender is 0 or negative, this <see cref="Manager{T}"/> 
		/// will have a fixed capacity.</param>
		public Manager(int capacity, int extender) : this(capacity)
		{
			this.Extender = extender;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Manager{T}"/> class that contains 
		/// elements copied from the specified collection and has sufficient capacity to 
		/// accommodate the number of elements copied.
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the new list.</param>
		public Manager(IEnumerable<T> collection)
		{
			if (collection == null)
			{

				throw new ArgumentNullException(nameof(collection));

			}
			else
			{

				if (collection is ICollection<T> elements)
				{

					if (elements.Count == 0)
					{

						this._collections = this._empty;
						return;

					}
					else
					{

						this._collections = new T[elements.Count];
						elements.CopyTo(this._collections, 0);
						this._size = elements.Count;

					}

				}
				else
				{

					this._collections = this._empty;

					foreach (var element in collection)
					{

						this.Add(element);

					}

				}

			}

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Manager{T}"/> class that contains 
		/// elements copied from the specified collection and has sufficient capacity to 
		/// accommodate the number of elements copied.
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the new list.</param>
		/// <param name="extender">Specifies by how much elements capacity should be extended when 
		/// it reaches its limit. If extender is 0 or negative, this <see cref="Manager{T}"/> 
		/// will have a fixed capacity.</param>
		public Manager(IEnumerable<T> collection, int extender) : this(collection)
		{
			this.Extender = extender;
		}

		#endregion

		#region This

		/// <summary>
		/// Gets or sets the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the collection to get or set.</param>
		/// <returns>The collection at the specified index.</returns>
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{

					throw new IndexOutOfRangeException();

				}
				else
				{

					return this._collections[index];

				}
			}
			set
			{
				if (index < 0 || index >= this._size)
				{

					throw new IndexOutOfRangeException();

				}
				else
				{

					if (this.Contains(value.CollectionName))
					{

						throw new CollectionExistenceException(value.CollectionName);

					}

					this._collections[index] = value;

				}
			}
		}

		/// <summary>
		/// Gets or sets the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the collection to get or set.</param>
		/// <returns>The collection at the specified index.</returns>
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is T obj)
				{

					this[index] = obj;

				}
				else
				{

					throw new ArgumentException("Value passed is of invalid type");

				}
			}
		}

		/// <summary>
		/// Gets the collection with CollectionName specified.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		/// <returns>The collection with CollectionName specified, if exists; null otherwise.</returns>
		public T this[string cname] => this.Find(cname);

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public abstract GameINT GameINT { get; }

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public abstract string GameSTR { get; }

		/// <summary>
		/// Name of this <see cref="Manager{T}"/>.
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// If true, manager can export and import non-serialized collection; otherwise, false.
		/// </summary>
		public abstract bool AllowsNoSerialization { get; }

		/// <summary>
		/// Indicates required alighment when this <see cref="IManager"/> is being serialized.
		/// </summary>
		public abstract Alignment Alignment { get; }

		/// <summary>
		/// Gets a collection and unit element type in this <see cref="IManager"/>.
		/// </summary>
		public abstract Type CollectionType { get; }

		/// <summary>
		/// Gets the number of elements contained in the <see cref="Manager{T}"/>.
		/// </summary>
		public int Count => this._size;

		/// <summary>
		/// Gets or sets the total number of elements the internal data structure can 
		/// hold without resizing.
		/// </summary>
		public int Capacity
		{
			get
			{
				return this._collections.Length;
			}
			set
			{
				if (value < this._size || value == this._collections.Length)
				{

					return;

				}
				
				if (value > 0)
				{

					var data = new T[value];

					for (int loop = 0; loop < this._size; ++loop)
					{

						data[loop] = this._collections[loop];

					}

					this._collections = data;
					ForcedX.GCCollect();

				}
				else
				{

					this._collections = this._empty;
					ForcedX.GCCollect();

				}
			}
		}

		/// <summary>
		/// Specifies by how much elements capacity should be expanded when it reaches its limit. 
		/// If Extender is 0, this <see cref="Manager{T}"/> will have a fixed capacity and adding 
		/// elements beyond its capacity will not be possible, unless Extender becomes positive.
		/// </summary>
		public int Extender
		{
			get => this._extender;
			set => this._extender = value <= 0 ? 0 : value;
		}

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public abstract bool IsReadOnly { get; }

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is of fixed size; otherwise, false.
		/// </summary>
		public bool IsFixedSize => this._extender == 0;

		/// <summary>
		/// Gets a value indicating whether access to the <see cref="ICollection"/> is 
		/// synchronized (thread safe).
		/// </summary>
		/// <returns>True if access to the <see cref="ICollection"/> is synchronized 
		/// (thread safe); otherwise, false..</returns>
		public bool IsSynchronized => true;

		/// <summary>
		/// Throws <see cref="NotImplementedException"/>.
		/// </summary>
		public object SyncRoot => throw new NotImplementedException();

		/// <summary>
		/// Database to which this <see cref="Manager{T}"/> belongs to.
		/// </summary>
		public FileBase Database { get; }

		#endregion

		#region Add

		/// <summary>
		/// Adds a new collection to the end of the <see cref="Manager{T}"/> with CollectionName 
		/// provided. Throws exception in case of failure.
		/// </summary>
		/// <param name="cname">CollectionName of a new created collection.</param>
		public void Add(string cname)
		{
			this.ReadOnlyThrow();
			this.CreationCheck(cname);

			if (this._size == this.Capacity)
			{

				if (this.IsFixedSize)
				{

					throw new ArgumentException("Unable to add collection to a non-resizable manager");

				}
				else
				{

					this.Capacity += this.Extender;

				}

			}

			var ctor = typeof(T).GetConstructor(new Type[] { typeof(string), this.GetType() });
			var instance = (T)ctor.Invoke(new object[] { cname, this });
			this._collections[this._size++] = instance;
		}

		/// <summary>
		/// Adds an object to the end of the <see cref="Manager{T}"/>. Throws exception in case 
		/// of failure.
		/// </summary>
		/// <param name="item">The object to be added to the end of the <see cref="Manager{T}"/>.</param>
		public void Add(T item)
		{
			this.ReadOnlyThrow();

			if (this.Contains(item.CollectionName))
			{

				throw new CollectionExistenceException(item.CollectionName);

			}

			if (this._size == this.Capacity)
			{

				if (this.IsFixedSize)
				{

					throw new ArgumentException("Unable to add collection to a non-resizable manager");

				}
				else
				{

					this.Capacity += this.Extender;

				}

			}

			this._collections[this._size++] = item;

		}

		/// <summary>
		/// Adds an item to the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="value">The object to add to the <see cref="Manager{T}"/>.</param>
		/// <returns>The position into which the new collection was inserted.</returns>
		public int Add(object value)
		{
			if (value is T obj)
			{

				this.Add(obj);
				return this._size - 1;

			}
			else
			{

				throw new ArgumentException("Value passed is of invalid type");

			}
		}

		#endregion

		#region Clone

		/// <summary>
		/// Clones collection and casts all of its memory to a new one.
		/// </summary>
		/// <param name="to">CollectionName of a new created collection.</param>
		/// <param name="from">CollectionName of a collection to copy.</param>
		public void Clone(string to, string from)
		{
			this.ReadOnlyThrow();
			var copy = this.Find(from);

			if (copy == null)
			{

				throw new ArgumentException($"Collection named {from} does not exist");

			}

			this.CreationCheck(to);

			if (this._size == this.Capacity)
			{

				if (this.IsFixedSize)
				{

					throw new ArgumentException("Unable to add collection to a non-resizable manager");

				}
				else
				{

					this.Capacity += this.Extender;

				}

			}

			var instance = (T)copy.MemoryCast(to);
			this._collections[this._size++] = instance;
		}

		/// <summary>
		/// Clones collection and casts all of its memory to a new one.
		/// </summary>
		/// <param name="to">CollectionName of a new created collection.</param>
		/// <param name="from">Collection to copy.</param>
		public void Clone(string to, T from) => this.Clone(to, from.CollectionName);

		/// <summary>
		/// Clones collection and casts all of its memory to a new one.
		/// </summary>
		/// <param name="to">CollectionName of a new created collection.</param>
		/// <param name="from">Object from which to cast all memory.</param>
		public void Clone(string to, object from)
		{
			if (from is T obj)
			{

				this.Clone(to, obj.CollectionName);

			}
			else
			{

				throw new ArgumentException("Value passed is of invalid type");

			}
		}

		#endregion

		#region Contains

		/// <summary>
		/// Determines whether a collection with CollectionName specified exists in 
		/// the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="cname">CollectionName to find.</param>
		/// <returns>True if collection with CollectionName specified exists; otherwise, false.</returns>
		public bool Contains(string cname)
		{
			for (int loop = 0; loop < this._size; ++loop)
			{

				if (this._collections[loop].CollectionName == cname)
				{

					return true;

				}

			}

			return false;
		}

		/// <summary>
		/// Determines whether a collection is in the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="item">The collection to locate in this <see cref="Manager{T}"/>.</param>
		/// <returns>True if collection is in this <see cref="Manager{T}"/>; otherwise, false.</returns>
		public bool Contains(T item) => item != null && this.Contains(item.CollectionName);

		/// <summary>
		/// Determines whether an object is in the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="value">The object to locate in this <see cref="Manager{T}"/>.</param>
		/// <returns>True if object is in this <see cref="Manager{T}"/>; otherwise, false.</returns>
		public bool Contains(object value)
		{
			if (value is string cname)
			{

				return this.Contains(cname);

			}
			else if (value is T obj)
			{

				return this.Contains(obj.CollectionName);

			}
			else
			{

				throw new ArgumentException("Value passed is of invalid type");

			}
		}

		#endregion

		#region CopyTo

		/// <summary>
		/// Copies elements of the <see cref="Manager{T}"/> to an <see cref="Array"/>, 
		/// starting at a particular <see cref="Array"/> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination 
		/// of the elements copied from this <see cref="Manager{T}"/>.</param>
		public void CopyTo(T[] array) => this.CopyTo(array, 0);

		/// <summary>
		/// Copies elements of the <see cref="Manager{T}"/> to an <see cref="Array"/>, 
		/// starting at a particular <see cref="Array"/> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination 
		/// of the elements copied from this <see cref="Manager{T}"/>.</param>
		/// <param name="index">The zero-based index in <see cref="Array"/> at which copying begins.</param>
		public void CopyTo(T[] array, int index) =>
			Array.Copy(this._collections, 0, array, index, this._size);

		/// <summary>
		/// Copies the elements of the <see cref="Manager{T}"/> to an <see cref="Array"/>.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination 
		/// of the elements copied from this <see cref="Manager{T}"/>.</param>
		public void CopyTo(Array array) => this.CopyTo(array, 0);

		/// <summary>
		/// Copies the elements of the <see cref="Manager{T}"/> to an <see cref="Array"/>, 
		/// starting at a particular <see cref="Array"/> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination 
		/// of the elements copied from this <see cref="Manager{T}"/>.</param>
		/// <param name="index">The zero-based index in <see cref="Array"/> at which copying begins.</param>
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{

				throw new ArgumentNullException(nameof(array));

			}
			else if (array.Rank != 1)
			{

				throw new ArgumentException("Array passed should be one-dimensional");

			}

			Array.Copy(this._collections, 0, array, index, this._size);
		}

		#endregion

		#region Enumerator

		/// <summary>
		/// Enumerates collections of a <see cref="Manager{T}"/>.
		/// </summary>
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			private Manager<T> _manager;
			private int _index;
			private T _current;

			internal Enumerator(Manager<T> manager)
			{
				this._manager = manager;
				this._index = 0;
				this._current = null;
			}

			/// <summary>
			/// Gets the element at the current position of the enumerator.
			/// </summary>
			public T Current => this._current;

			/// <summary>
			/// Gets the element at the current position of the enumerator.
			/// </summary>
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._manager._size + 1)
					{

						throw new InvalidOperationException();

					}

					return this._current;
				}
			}

			/// <summary>
			/// Releases all resources used by the <see cref="Enumerator"/>.
			/// </summary>
			public void Dispose() { }

			/// <summary>
			/// Advances the enumerator to the next element of the <see cref="Manager{T}"/>.
			/// </summary>
			/// <returns>True if the enumerator was successfully advanced to the next element; 
			/// false if the enumerator has passed the end of the collection.</returns>
			public bool MoveNext()
			{
				if (this._index < this._manager._size)
				{
					
					this._current = this._manager._collections[this._index++];
					return true;

				}

				this._index = this._manager._size + 1;
				this._current = null;
				return false;
			}

			/// <summary>
			/// Sets the enumerator to its initial position, which is before the first element 
			/// in the collection.
			/// </summary>
			public void Reset()
			{
				this._index = 0;
				this._current = null;
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerator{T}"/> that can be used to iterate through 
		/// the collection.</returns>
		public IEnumerator<T> GetEnumerator() => new Enumerator(this);

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/> that can be used to iterate through 
		/// the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

		#endregion

		#region Export

		/// <summary>
		/// Exports collection with CollectionName specified to a filename provided.
		/// </summary>
		/// <param name="cname">CollectionName of a collection to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="serialized">True if collection exported should be serialized; 
		/// false otherwise.</param>
		public abstract void Export(string cname, BinaryWriter bw, bool serialized = true);

		/// <summary>
		/// Exports object to a filename specified.
		/// </summary>
		/// <param name="value">Object to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="serialized">True if collection exported should be serialized; 
		/// false otherwise.</param>
		public void Export(object value, BinaryWriter bw, bool serialized = true)
		{
			if (value is T obj)
			{

				this.Export(obj.CollectionName, bw);

			}
			else
			{

				throw new ArgumentException("Value passed is of invalid type");

			}
		}

		#endregion

		#region Import

		/// <summary>
		/// Imports collection from file provided and attempts to add it to the end of 
		/// this <see cref="Manager{T}"/> in case it does not exist.
		/// </summary>
		/// <param name="type">Type of serialization of a collection.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public abstract void Import(SerializeType type, BinaryReader br);

		#endregion

		#region IndexOf

		/// <summary>
		/// Searches for the collection with CollectionName specified and returns the 
		/// zero-based index of the first occurrence within the entire <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		/// <returns>The zero-based index of the first occurence of collection within 
		/// the entire <see cref="Manager{T}"/>.</returns>
		public int IndexOf(string cname)
		{
			for (int loop = 0; loop < this._size; ++loop)
			{

				if (this._collections[loop].CollectionName == cname) return loop;

			}

			return -1;
		}

		/// <summary>
		/// Searches for the specified collection and returns the zero-based index of the 
		/// first occurrence within the entire <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="item">The collection to locate in the <see cref="Manager{T}"/>.</param>
		/// <returns>The zero-based index of the first occurence of collection within 
		/// the entire <see cref="Manager{T}"/>.</returns>
		public int IndexOf(T item) => item == null ? -1 : this.IndexOf(item.CollectionName);

		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the first 
		/// occurrence within the entire <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="value">The object to locate in the <see cref="Manager{T}"/>.</param>
		/// <returns>The zero-based index of the first occurrence of object within 
		/// the entire <see cref="Manager{T}"/>.</returns>
		public int IndexOf(object value)
		{
			if (value is string cname)
			{

				return this.IndexOf(cname);

			}
			else if (value is T obj)
			{

				return this.IndexOf(obj.CollectionName);

			}
			else
			{

				throw new ArgumentException("Value passed is of invalid type");

			}
		}

		#endregion

		#region Find

		/// <summary>
		/// Searches for a collection that has CollectionName specified within the entire 
		/// <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		/// <returns>Collection with CollectionName specified, if found; null otherwise.</returns>
		public T Find(string cname)
		{
			for (int loop = 0; loop < this._size; ++loop)
			{

				if (this._collections[loop].CollectionName == cname)
				{

					return this._collections[loop];

				}

			}

			return null;
		}

		/// <summary>
		/// Searches for a collection that matches the conditions defined by the specified 
		/// predicate, and returns the first occurrence within the entire <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="predicate">The <see cref="Predicate{T}"/> delegate that defines 
		/// the conditions of the collection to search for.</param>
		/// <returns>The first collection that matches the conditions defined by the specified 
		/// predicate, if found; null otherwise.</returns>
		public T Find(Predicate<T> predicate)
		{
			if (predicate == null)
			{

				throw new ArgumentNullException(nameof(predicate));

			}
			else
			{

				for (int loop = 0; loop < this._size; ++loop)
				{

					if (predicate(this._collections[loop]))
					{

						return this._collections[loop];

					}

				}

			}

			return null;
		}

		/// <summary>
		/// Searches for a collection that has CollectionName specified, and returns the 
		/// zero-based index of its first occurence within this <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		/// <returns>The zero-based index of the collection with CollectionName specified, 
		/// if found; otherwise -1.</returns>
		public int FindIndex(string cname)
		{
			for (int loop = 0; loop < this._size; ++loop)
			{

				if (this._collections[loop].CollectionName == cname) return loop;

			}

			return -1;
		}

		/// <summary>
		/// Searches for a collection that matches the conditions defined by the specified 
		/// predicate, and returns the zero-based index of its first occurrence within 
		/// this <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="predicate">The <see cref="Predicate{T}"/> delegate that defines 
		/// the conditions of the collection to search for.</param>
		/// <returns>The zero-based index of the first occurrence of an element that 
		/// matches the conditions defined by <see cref="Predicate{T}"/>, 
		/// if found; otherwise, -1.</returns>
		public int FindIndex(Predicate<T> predicate)
		{
			if (predicate == null)
			{

				throw new ArgumentNullException(nameof(predicate));

			}
			else
			{

				for (int loop = 0; loop < this._size; ++loop)
				{

					if (predicate(this._collections[loop])) return loop;

				}

			}

			return -1;
		}

		#endregion

		#region Insert

		/// <summary>
		/// Inserts new collection with CollectionName provided at the index specified in this 
		/// <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="index">The zero-based index at which collection should be inserted.</param>
		/// <param name="cname">CollectionName of a new collection to insert.</param>
		public void Insert(int index, string cname)
		{
			this.ReadOnlyThrow();

			if (index < 0 || index > this._size)
			{

				throw new ArgumentOutOfRangeException(nameof(index));

			}

			this.CreationCheck(cname);

			if (this._size == this.Capacity)
			{

				if (this.IsFixedSize)
				{

					throw new ArgumentException("Unable to add collection to a non-resizable manager");

				}
				else
				{

					this.Capacity += this.Extender;

				}

			}

			if (index < this._size)
			{

				Array.Copy(this._collections, index, this._collections, index + 1, this._size - index);

			}

			var ctor = typeof(T).GetConstructor(new Type[] { typeof(string), this.GetType() });
			var instance = (T)ctor.Invoke(new object[] { cname, this });
			this._collections[index] = instance;
			++this._size;
		}

		/// <summary>
		/// Inserts a collection into the <see cref="Manager{T}"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which collection should be inserted.</param>
		/// <param name="item">The collection to insert.</param>
		public void Insert(int index, T item)
		{
			this.ReadOnlyThrow();

			if (index < 0 || index > this._size)
			{

				throw new ArgumentOutOfRangeException(nameof(index));

			}

			if (this.Contains(item.CollectionName))
			{

				throw new CollectionExistenceException(item.CollectionName);

			}

			if (this._size == this.Capacity)
			{

				if (this.IsFixedSize)
				{

					throw new ArgumentException("Unable to add collection to a non-resizable manager");

				}
				else
				{

					this.Capacity += this.Extender;

				}

			}

			if (index < this._size)
			{

				Array.Copy(this._collections, index, this._collections, index + 1, this._size - index);

			}

			this._collections[index] = item;
			++this._size;
		}

		/// <summary>
		/// Inserts an element into the <see cref="Manager{T}"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which element should be inserted.</param>
		/// <param name="value">The object to insert.</param>
		public void Insert(int index, object value)
		{
			if (value is T obj)
			{

				this.Insert(index, obj);

			}
			else
			{

				throw new ArgumentException("Value passed is of invalid type");

			}
		}

		#endregion

		#region Remove

		/// <summary>
		/// Removes the first occurence of a collection with CollectionName specified from 
		/// the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		public void Remove(string cname)
		{
			this.ReadOnlyThrow();

			for (int loop = 0; loop < this._size; ++loop)
			{

				if (this._collections[loop].CollectionName == cname)
				{

					this.RemoveAt(loop);
					return;

				}

			}

			throw new ArgumentException($"Collection named {cname} does not exist");
		}

		/// <summary>
		/// Removes the first occurrence of a specific collection from the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="item">The collection to remove from the <see cref="Manager{T}"/>.</param>
		/// <returns>True is successfully removed; otherwise, false.</returns>
		public bool Remove(T item)
		{
			this.ReadOnlyThrow();
			int index = this.IndexOf(item);
			
			if (index != -1)
			{

				this.RemoveAt(index);
				return true;

			}

			return false;
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="value">The object to remove from the <see cref="Manager{T}"/>.</param>
		public void Remove(object value)
		{
			if (value is T obj)
			{

				this.Remove(obj.CollectionName);

			}
			else
			{

				throw new ArgumentException("Value passed is of invalid type");

			}
		}

		#endregion

		#region Static

		/// <summary>
		/// Sets value passed statically through all collections in this <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="property">Property to be edited.</param>
		/// <param name="value">Value to set.</param>
		public void Static(string property, string value)
		{
			var proper = typeof(T).GetProperty(property);

			if (proper == null)
			{

				throw new ArgumentException($"Property named {property} does not exist");

			}

			if (!Attribute.IsDefined(proper, typeof(StaticModifiableAttribute)))
			{

				throw new ArgumentException($"Property named {property} is not statically modifiable");

			}

			for (int loop = 0; loop < this._size; ++loop)
			{

				this._collections[loop].SetValue(property, value);

			}
		}

		#endregion

		#region Switch

		/// <summary>
		/// Switches two collections in place using their CollectionNames provided.
		/// </summary>
		/// <param name="cname1">CollectionName of the first collection to switch.</param>
		/// <param name="cname2">CollectionName of the second collection to switch.</param>
		public void Switch(string cname1, string cname2)
		{
			var index1 = this.IndexOf(cname1);
			var index2 = this.IndexOf(cname2);

			if (index1 == -1)
			{

				throw new ArgumentException($"Collection named {cname1} does not exist");

			}

			if (index2 == -1)
			{

				throw new ArgumentException($"Collection named {cname2} does not exist");

			}

			var temp = this._collections[index1];
			this._collections[index1] = this._collections[index2];
			this._collections[index2] = temp;
		}

		/// <summary>
		/// Switches two collections in place using their indexes provided.
		/// </summary>
		/// <param name="index1">Index of the first collection to switch.</param>
		/// <param name="index2">Index of the second collection to switch.</param>
		public void Switch(int index1, int index2)
		{
			if (index1 < 0 || index1 >= this._size)
			{

				throw new ArgumentOutOfRangeException(nameof(index1));

			}

			if (index2 < 0 || index2 >= this._size)
			{

				throw new ArgumentOutOfRangeException(nameof(index2));
			
			}

			var temp = this._collections[index1];
			this._collections[index1] = this._collections[index2];
			this._collections[index2] = temp;
		}

		/// <summary>
		/// Switches two collections in place.
		/// </summary>
		/// <param name="item1">First collection to switch.</param>
		/// <param name="item2">Second collection to switch.</param>
		public void Switch(T item1, T item2) =>
			this.Switch(item1.CollectionName, item2.CollectionName);

		/// <summary>
		/// Switches two objects in place.
		/// </summary>
		/// <param name="value1">First object to switch.</param>
		/// <param name="value2">Second object to switch.</param>
		public void Switch(object value1, object value2)
		{
			var index1 = this.IndexOf(value1);
			var index2 = this.IndexOf(value2);

			if (index1 == -1)
			{

				throw new ArgumentException($"Object {nameof(value1)} does not exist");

			}

			if (index2 == -1)
			{

				throw new ArgumentException($"Object {nameof(value2)} does not exist");

			}

			var temp = this._collections[index1];
			this._collections[index1] = this._collections[index2];
			this._collections[index2] = temp;
		}

		#endregion

		#region Misc

		/// <summary>
		/// Removes the element at the specified index of the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		public void RemoveAt(int index)
		{
			this.ReadOnlyThrow();

			if (index >= this._size || index < 0)
			{

				throw new ArgumentOutOfRangeException(nameof(index));

			}

			--this._size;

			if (index < this._size)
			{

				Array.Copy(this._collections, index + 1, this._collections, index, this._size - index);

			}

			this._collections[this._size] = null;
		}

		/// <summary>
		/// Removes all elements from the <see cref="Manager{T}"/>.
		/// </summary>
		public void Clear()
		{
			this.ReadOnlyThrow();

			if (this._size > 0)
			{

				Array.Clear(this._collections, 0, this._size);
				this._size = 0;

			}
		}

		/// <summary>
		/// Performs the specified action on each collection of the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="action">The <see cref="Action"/> delegate to perform on each 
		/// collection of the <see cref="Manager{T}"/>.</param>
		public void ForEach(Action<T> action)
		{
			if (action == null)
			{

				throw new ArgumentNullException(nameof(action));

			}
			else
			{

				for (int loop = 0; loop < this._size; ++loop)
				{

					action(this._collections[loop]);

				}

			}
		}

		/// <summary>
		/// Performance the specified action on each collection of the <see cref="Manager{T}"/>.
		/// </summary>
		/// <param name="action">The <see cref="Action"/> delegate to perform on each 
		/// collection of the <see cref="Manager{T}"/>.</param>
		public void ForEach(Action<object> action)
		{
			if (action == null)
			{

				throw new ArgumentNullException(nameof(action));

			}
			else
			{

				for (int loop = 0; loop < this._size; ++loop)
				{

					action(this._collections[loop]);

				}

			}
		}

		/// <summary>
		/// Replaces collection with instance passed at index specified.
		/// </summary>
		/// <param name="collection">New collection to replace with.</param>
		/// <param name="index">Position of replacement.</param>
		internal void Replace(T collection, int index)
		{
			if (index < 0 || index >= this._size)
			{

				throw new IndexOutOfRangeException(nameof(index));

			}

			this._collections[index] = collection;
		}

		/// <summary>
		/// Sorts all collections by their BinKey value.
		/// </summary>
		internal void SortByKey()
		{
			for (int i = 0; i < this._size - 1; ++i)
			{

				for (int k = 0; k < this._size - i - 1; ++k)
				{

					if (this._collections[k].CollectionName.BinHash() > this._collections[k + 1].CollectionName.BinHash())
					{

						var temp = this._collections[k];
						this._collections[k] = this._collections[k + 1];
						this._collections[k + 1] = temp;

					}

				}

			}
		}

		#endregion

		#region Assembly

		/// <summary>
		/// Throws exception if this <see cref="Manager{T}"/> is read-only.
		/// </summary>
		private void ReadOnlyThrow()
		{
			if (this.IsReadOnly)
			{

				throw new Exception("Unable to add collection because manager is read-only");

			}
		}

		/// <summary>
		/// Assembles collection data into byte buffers.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark to put in the padding blocks.</param>
		internal abstract void Assemble(BinaryWriter bw, string mark);

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="IManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal abstract void Disassemble(BinaryReader br, Block block);

		/// <summary>
		/// Checks whether CollectionName provided allows creation of a new collection.
		/// </summary>
		/// <param name="cname">CollectionName to check.</param>
		internal abstract void CreationCheck(string cname);

		#endregion
	}
}
