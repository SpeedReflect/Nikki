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
        /// Gets information about <see cref="FileBase"/> database.
        /// </summary>
		/// <returns>Info about this database as a string value.</returns>
        public abstract string GetDatabaseInfo();
    }
}
