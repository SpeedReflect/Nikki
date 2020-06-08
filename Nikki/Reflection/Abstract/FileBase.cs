using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Reflection.Interface;



namespace Nikki.Reflection.Abstract
{
    /// <summary>
    /// Very base class of any database.
    /// </summary>
	public abstract class FileBase : IGameSelectable
	{
        /// <summary>
        /// File buffer.
        /// </summary>
		public byte[] Buffer { get; set; }

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
        public List<IManager> Managers { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="FileBase"/>.
        /// </summary>
        public FileBase() => this.Managers = new List<IManager>();

        /// <summary>
        /// Loads all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to load data.</param>
        /// <returns>True on success; false otherwise.</returns>
        public abstract bool Load(Options options);

        /// <summary>
        /// Saves all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to save data.</param>
        /// <returns>True on success; false otherwise.</returns>
        public abstract bool Save(Options options);

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
        /// Gets a <see cref="Collectable"/> class from CollectionName and root provided.
        /// </summary>
        /// <param name="CName">CollectionName of <see cref="Collectable"/> to find.</param>
        /// <param name="root">Root collection of the class.</param>
        /// <returns><see cref="Collectable"/> class.</returns>
        public Collectable GetCollection(string CName, string root)
        {

            return null;
        }

        /// <summary>
        /// Attempts to get <see cref="Collectable"/> class from CollectionName and root provided.
        /// </summary>
        /// <param name="CName">CollectionName of <see cref="Collectable"/> to find.</param>
        /// <param name="root">Root collection of the class.</param>
        /// <param name="collection"><see cref="Collectable"/> class that is to return.</param>
        /// <returns>True if collection exists and can be returned; false otherwise.</returns>
        public bool TryGetCollection(string CName, string root, out Collectable collection)
        {
            collection = null;
            try
            {
                collection = this.GetCollection(CName, root);
                return collection != null;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a <see cref="IReflective"/> class from the path provided.
        /// </summary>
        /// <param name="path">Path of the <see cref="IReflective"/> class to find.</param>
        /// <returns><see cref="IReflective"/> class.</returns>
        public virtual IReflective GetPrimitive(params string[] path)
        {
            switch (path.Length)
            {
                case 2:
                    return this.GetCollection(path[1], path[0]);

                case 4:
                    var collection = this.GetCollection(path[1], path[0]);
                    return collection.GetSubPart(path[3], path[2]);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets information about <see cref="FileBase"/> database.
        /// </summary>
		/// <returns>Info about this database as a string value.</returns>
        public abstract string GetDatabaseInfo();
    }
}
