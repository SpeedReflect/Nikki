using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using CoreExtensions.Conversions;
using System.Collections.Generic;

namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="GCareer"/> is a collection of gameplay classes.
    /// </summary>
    public abstract class GCareer : Collectable, IAssembly
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
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public uint VltKey => this.CollectionName.VltHash();

        /// <summary>
        /// Represents array of <see cref="IList"/> of <see cref="Collectable"/> collections.
        /// </summary>
        [Browsable(false)]
        public abstract IList[] AllCollections { get; }

        /// <summary>
        /// Represents array of all root names in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public abstract string[] AllRootNames { get; }

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
        /// Assembles <see cref="GCareer"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="GCareer"/> with.</param>
        /// <returns>Byte array of the tpk block.</returns>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles <see cref="GCareer"/> array into separate properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCareer"/> with.</param>
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
        /// Returns an <see cref="IList"/> root with name specified.
        /// </summary>
        /// <param name="root">Name of a root to get.</param>
        /// <returns>Root with name specified as an <see cref="IList"/>.</returns>
        public abstract IList GetRoot(string root);

        /// <summary>
        /// Gets collection of with CollectionName specified from a root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to get.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        /// <returns>Collection, if exists; null otherwise.</returns>
        public abstract Collectable GetCollection(string cname, string root);

        /// <summary>
        /// Adds a unit collection at a root provided with CollectionName specified.
        /// </summary>
        /// <param name="cname">CollectionName of a new collection.</param>
        /// <param name="root">Root to which collection should belong to.</param>
        public abstract void AddCollection(string cname, string root);

        /// <summary>
        /// Removes collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to remove.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public abstract void RemoveCollection(string cname, string root);

        /// <summary>
        /// Clones collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="newname">CollectionName of a new cloned collection.</param>
        /// <param name="copyname">CollectionName of a collection to clone.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public abstract void CloneCollection(string newname, string copyname, string root);

        /// <summary>
        /// Merges two lists of <see cref="Collectable"/> into one.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Collectable"/>.</typeparam>
        /// <param name="main">Main list that takes merging priority in case of duplicates.</param>
        /// <param name="merger">List that main list is being merged with.</param>
        /// <returns>List with merged collections.</returns>
        protected List<T> MergeCollectionLists<T>(List<T> main, List<T> merger) where T : Collectable
		{
            var result = new List<T>(main);

            foreach (var collection in merger)
			{

                var match = main.Find(_ => _.CollectionName == collection.CollectionName);
                if (match is null) result.Add(collection);
                else continue;

			}

            return result;
		}

		#endregion
	}
}