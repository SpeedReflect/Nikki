using System;
using System.IO;
using System.Collections;
using Nikki.Core;
using Nikki.Reflection.Enum;



namespace Nikki.Reflection.Interface
{
	/// <summary>
	/// Interface with methods and properties designed for managing collection types.
	/// </summary>
	public interface IManager : IGameSelectable, IList
	{
		#region Properties

		/// <summary>
		/// Name of this <see cref="IManager"/>.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the total number of elements the internal data structure can 
		/// hold without resizing.
		/// </summary>
		int Capacity { get; set; }

		/// <summary>
		/// Specifies by how much elements capacity should be expanded when it reaches its limit. 
		/// If Extender is 0, this <see cref="IManager"/> will have a fixed capacity and adding 
		/// elements beyond its capacity will not be possible, unless Extender becomes positive.
		/// </summary>
		int Extender { get; set; }

		/// <summary>
		/// If true, manager can export and import non-serialized collection; otherwise, false.
		/// </summary>
		bool AllowsNoSerialization { get; }

		/// <summary>
		/// Indicates required alighment when this <see cref="IManager"/> is being serialized.
		/// </summary>
		Alignment Alignment { get; }

		/// <summary>
		/// Gets a collection and unit element type in this <see cref="IManager"/>.
		/// </summary>
		Type CollectionType { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Adds a new collection to the end of the <see cref="IManager"/> with CollectionName 
		/// provided. Throws exception in case of failure.
		/// </summary>
		/// <param name="cname">CollectionName of a new created collection.</param>
		void Add(string cname);

		/// <summary>
		/// Clones collection and casts all of its memory to a new one.
		/// </summary>
		/// <param name="to">CollectionName of a new created collection.</param>
		/// <param name="from">CollectionName of a collection to copy.</param>
		void Clone(string to, string from);

		/// <summary>
		/// Clones collection and casts all of its memory to a new one.
		/// </summary>
		/// <param name="to">CollectionName of a new created collection.</param>
		/// <param name="from">Object from which to cast all memory.</param>
		void Clone(string to, object from);

		/// <summary>
		/// Copies the elements of the <see cref="IManager"/> to an <see cref="Array"/>.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination 
		/// of the elements copied from this <see cref="IManager"/>.</param>
		void CopyTo(Array array);

		/// <summary>
		/// Exports collection with CollectionName specified to a filename provided.
		/// </summary>
		/// <param name="cname">CollectionName of a collection to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="serialized">True if collection exported should be serialized; 
		/// false otherwise.</param>
		void Export(string cname, BinaryWriter bw, bool serialized = true);

		/// <summary>
		/// Exports object to a filename specified.
		/// </summary>
		/// <param name="value">Object to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="serialized">True if collection exported should be serialized; 
		/// false otherwise.</param>
		void Export(object value, BinaryWriter bw, bool serialized = true);

		/// <summary>
		/// Imports collection from file provided and attempts to add it to the end of 
		/// this <see cref="IManager"/> in case it does not exist.
		/// </summary>
		/// <param name="type">Type of serialization of a collection.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		void Import(SerializeType type, BinaryReader br);

		/// <summary>
		/// Searches for the collection with CollectionName specified and returns the 
		/// zero-based index of the first occurrence within the entire <see cref="IManager"/>.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		/// <returns>The zero-based index of the first occurence of collection within 
		/// the entire <see cref="IManager"/>.</returns>
		int IndexOf(string cname);

		/// <summary>
		/// Searches for a collection that has CollectionName specified, and returns the 
		/// zero-based index of its first occurence within this <see cref="IManager"/>.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		/// <returns>The zero-based index of the collection with CollectionName specified, 
		/// if found; otherwise -1.</returns>
		int FindIndex(string cname);

		/// <summary>
		/// Inserts new collection with CollectionName provided at the index specified in this 
		/// <see cref="IManager"/>.
		/// </summary>
		/// <param name="index">The zero-based index at which collection should be inserted.</param>
		/// <param name="cname">CollectionName of a new collection to insert.</param>
		void Insert(int index, string cname);

		/// <summary>
		/// Removes the first occurence of a collection with CollectionName specified from 
		/// the <see cref="IManager"/>.
		/// </summary>
		/// <param name="cname">CollectionName to match.</param>
		void Remove(string cname);

		/// <summary>
		/// Sets value passed statically through all collections in this <see cref="IManager"/>.
		/// </summary>
		/// <param name="property">Property to be edited.</param>
		/// <param name="value">Value to set.</param>
		void Static(string property, string value);

		/// <summary>
		/// Switches two collections in place using their CollectionNames provided.
		/// </summary>
		/// <param name="cname1">CollectionName of the first collection to switch.</param>
		/// <param name="cname2">CollectionName of the second collection to switch.</param>
		void Switch(string cname1, string cname2);

		/// <summary>
		/// Switches two collections in place using their indexes provided.
		/// </summary>
		/// <param name="index1">Index of the first collection to switch.</param>
		/// <param name="index2">Index of the second collection to switch.</param>
		void Switch(int index1, int index2);

		/// <summary>
		/// Switches two objects in place.
		/// </summary>
		/// <param name="value1">First object to switch.</param>
		/// <param name="value2">Second object to switch.</param>
		void Switch(object value1, object value2);

		/// <summary>
		/// Performance the specified action on each collection of the <see cref="IManager"/>.
		/// </summary>
		/// <param name="action">The <see cref="Action"/> delegate to perform on each 
		/// collection of the <see cref="IManager"/>.</param>
		void ForEach(Action<object> action);

		#endregion
	}
}
