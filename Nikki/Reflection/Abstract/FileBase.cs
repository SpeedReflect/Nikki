using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Interface;



namespace Nikki.Reflection.Abstract
{
    /// <summary>
    /// Very base class of any database.
    /// </summary>
	public abstract class FileBase : IGameSelectable
	{
        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public abstract GameINT GameINT { get; }

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public abstract string GameSTR { get; }

        /// <summary>
        /// List of <see cref="IManager"/> types that are used by this <see cref="FileBase"/>.
        /// </summary>
        public List<IManager> Managers { get; }

        /// <summary>
        /// Initializes new instance of <see cref="FileBase"/>.
        /// </summary>
        public FileBase() => this.Managers = new List<IManager>();

        /// <summary>
        /// Loads all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to load data.</param>
        public abstract void Load(Options options);

        /// <summary>
        /// Saves all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to save data.</param>
        public abstract void Save(Options options);

        /// <summary>
        /// Exports collection by writing its data to a <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="manager">Name of <see cref="IManager"/> to which collection belongs to.</param>
        /// <param name="cname">CollectionName of collection to export.</param>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        /// <param name="serialized">True if collection should be serialized; false if plainly 
        /// exported.</param>
        public abstract void Export(string manager, string cname, BinaryWriter bw, bool serialized = true);

        /// <summary>
        /// Imports collection by reading its data from a <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="type"><see cref="SerializeType"/> type of importing collection.</param>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public abstract void Import(SerializeType type, BinaryReader br);

        /// <summary>
        /// Imports collection by reading its data from a <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="type"><see cref="SerializeType"/> type of importing collection.</param>
        /// <param name="manager">Name of <see cref="IManager"/> to invoke for import.</param>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public abstract void Import(SerializeType type, string manager, BinaryReader br);

        /// <summary>
        /// Gets <see cref="IManager"/> with name specified.
        /// </summary>
        /// <param name="name">Name of <see cref="IManager"/> to get.</param>
        /// <returns><see cref="IManager"/>, if exists; null otherwise.</returns>
        public IManager this[string name] => this.Managers.Find(_ => _.Name == name);

        /// <summary>
        /// Gets <see cref="IManager"/> with type specified.
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IManager"/> to get.</param>
        /// <returns><see cref="IManager"/>, if exists; null otherwise.</returns>
        public IManager this[Type type] => this.GetManager(type);

        /// <summary>
        /// Gets <see cref="IManager"/> with name specified.
        /// </summary>
        /// <param name="name">Name of <see cref="IManager"/> to get.</param>
        /// <returns><see cref="IManager"/>, if exists; null otherwise.</returns>
        public IManager GetManager(string name) => this.Managers.Find(_ => _.Name == name);

        /// <summary>
        /// Gets <see cref="IManager"/> with type specified.
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IManager"/> to get.</param>
        /// <returns><see cref="IManager"/>, if exists; null otherwise.</returns>
        public IManager GetManager(Type type)
        {
            foreach (var manager in this.Managers)
            {

                if (manager.GetType() == type) return manager;

            }

            return null;
        }

        /// <summary>
        /// Gets information about <see cref="FileBase"/> database.
        /// </summary>
		/// <returns>Info about this database as a string value.</returns>
        public abstract string GetDatabaseInfo();
    }
}
