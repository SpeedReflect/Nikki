using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;



namespace Nikki.Database
{
    /// <summary>
    /// Very base class of any database.
    /// </summary>
	public class FileBase : IOperative
	{
        /// <summary>
        /// File buffer.
        /// </summary>
		public virtual byte[] Buffer { get; set; }

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public GameINT GameINT { get; }

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public string GameSTR => this.GameINT.ToString();

        /// <summary>
        /// List of <see cref="IManager"/> types that are used by this <see cref="FileBase"/>.
        /// </summary>
        public List<IManager> Managers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public FileBase(GameINT game)
        {
            this.GameINT = game;
            this.Managers = new List<IManager>();
        }

        /// <summary>
        /// Loads all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to load data.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool Load(Options options)
        {
            if (!File.Exists(options.File)) return false;
            if (options.Flags.HasFlag(eOptFlags.None)) return false;
            this.Buffer = File.ReadAllBytes(options.File);

            IInvokable loader;

            switch (this.GameINT)
            {
                case GameINT.Carbon:
                    loader = new Support.Carbon.Framework.DatabaseLoader(options, this);
                    return loader.Invoke();

                default:
                    return false;

            }
        }

        /// <summary>
        /// Saves all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to save data.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool Save(Options options)
        {
            return false;
        }

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
        /// Gets a <see cref="ACollectable"/> class from CollectionName and root provided.
        /// </summary>
        /// <param name="CName">CollectionName of <see cref="ACollectable"/> to find.</param>
        /// <param name="root">Root collection of the class.</param>
        /// <returns><see cref="ACollectable"/> class.</returns>
        public ACollectable GetCollection(string CName, string root)
        {

            return null;
        }

        /// <summary>
        /// Attempts to get <see cref="ACollectable"/> class from CollectionName and root provided.
        /// </summary>
        /// <param name="CName">CollectionName of <see cref="ACollectable"/> to find.</param>
        /// <param name="root">Root collection of the class.</param>
        /// <param name="collection"><see cref="ACollectable"/> class that is to return.</param>
        /// <returns>True if collection exists and can be returned; false otherwise.</returns>
        public bool TryGetCollection(string CName, string root, out ACollectable collection)
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
        /// Attempts to set value statically in all collections in the root specified.
        /// </summary>
        /// <param name="root">Root collection of the class.</param>
        /// <param name="field">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <returns>True on success; false otherwise.</returns>
        public virtual bool TrySetStaticValue(string root, string field, string value)
        {
            var property = this.GetType().GetProperty(root ?? string.Empty);
            if (property == null) return false;
            try
            {
                return (bool)property.PropertyType
                    .GetMethod("TrySetStaticValue", new Type[] { typeof(string), typeof(string) })
                    .Invoke(property.GetValue(this), new object[] { field, value });
            }
            catch (System.Exception) { return false; }
        }

        /// <summary>
        /// Attempts to set value statically in all collections in the root specified.
        /// </summary>
        /// <param name="root">Root collection of the class.</param>
        /// <param name="field">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <param name="error">Error occured when trying to set value.</param>
        /// <returns>True on success; false otherwise.</returns>
        public virtual bool TrySetStaticValue(string root, string field, string value, out string error)
        {
            error = null;
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null)
            {
                error = $"Root named {root} does not exist in the database.";
                return false;
            }

            try
            {
                var callargs = new object[] { field, value, error };
                bool result = (bool)node.PropertyType
                    .GetMethod("TrySetStaticValue", new Type[] { typeof(string), typeof(string), typeof(string).MakeByRefType() })
                    .Invoke(node.GetValue(this), callargs);
                error = callargs[2]?.ToString();
                return result;
            }
            catch (System.Exception)
            {
                if (error == null) error = $"Unable to statically set value in the root {root}.";
                return false;
            }
        }

        /// <summary>
        /// Attempts to add class specfified to the database.
        /// </summary>
        /// <param name="CName">Collection Name of the new class.</param>
        /// <param name="root">Root of the new class. Range: Materials, CarTypeInfos, PresetRides.</param>
        /// <returns>True if class adding was successful, false otherwise.</returns>
        public virtual bool TryAddCollection(string CName, string root)
        {
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null) return false;

            try
            {
                return (bool)node.PropertyType
                    .GetMethod("TryAddCollection", new Type[] { typeof(string) })
                    .Invoke(node.GetValue(this), new object[] { CName });
            }
            catch (System.Exception) { return false; }
        }

        /// <summary>
        /// Attempts to add class specfified to the database.
        /// </summary>
        /// <param name="CName">Collection Name of the new class.</param>
        /// <param name="root">Root of the new class. Range: Materials, CarTypeInfos, PresetRides.</param>
        /// <param name="error">Error occured while trying to add class.</param>
        /// <returns>True if class adding was successful, false otherwise.</returns>
        public virtual bool TryAddCollection(string CName, string root, out string error)
        {
            error = null;
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null)
            {
                error = $"Root named {root} does not exist in the database.";
                return false;
            }

            try
            {
                var callargs = new object[] { CName, error };
                bool result = (bool)node.PropertyType
                    .GetMethod("TryAddCollection", new Type[] { typeof(string), typeof(string).MakeByRefType() })
                    .Invoke(node.GetValue(this), callargs);
                error = callargs[1]?.ToString();
                return result;
            }
            catch (System.Exception)
            {
                if (error == null) error = $"Unable to add collection to the root {root}.";
                return false;
            }
        }

        /// <summary>
        /// Attempts to remove class specfified in the database.
        /// </summary>
        /// <param name="CName">Collection Name of the class to be deleted.</param>
        /// <param name="root">Root of the class to delete. Range: Materials, CarTypeInfos, PresetRides.</param>
        /// <returns>True if class removing was successful, false otherwise.</returns>
        public virtual bool TryRemoveCollection(string CName, string root)
        {
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null) return false;

            try
            {
                return (bool)node.PropertyType
                    .GetMethod("TryRemoveCollection", new Type[] { typeof(string) })
                    .Invoke(node.GetValue(this), new object[] { CName });
            }
            catch (System.Exception) { return false; }
        }

        /// <summary>
        /// Attempts to remove class specfified in the database.
        /// </summary>
        /// <param name="CName">Collection Name of the class to be deleted.</param>
        /// <param name="root">Root of the class to delete. Range: Materials, CarTypeInfos, PresetRides.</param>
        /// <param name="error">Error occured while trying to remove class.</param>
        /// <returns>True if class removing was successful, false otherwise.</returns>
        public virtual bool TryRemoveCollection(string CName, string root, out string error)
        {
            error = null;
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null)
            {
                error = $"Root named {root} does not exist in the database.";
                return false;
            }

            try
            {
                var callargs = new object[] { CName, error };
                bool result = (bool)node.PropertyType
                    .GetMethod("TryRemoveCollection", new Type[] { typeof(string), typeof(string).MakeByRefType() })
                    .Invoke(node.GetValue(this), callargs);
                error = callargs[1]?.ToString();
                return result;
            }
            catch (System.Exception)
            {
                error = $"Unable to remove collection in root {root}.";
                return false;
            }
        }

        /// <summary>
        /// Attempts to clone class specfified in the database.
        /// </summary>
        /// <param name="newname">Collection Name of the new class.</param>
        /// <param name="copyfrom">Collection Name of the class to clone.</param>
        /// <param name="root">Root of the class to clone. Range: Materials, CarTypeInfos, PresetRides.</param>
        /// <returns>True if class cloning was successful, false otherwise.</returns>
        public virtual bool TryCloneCollection(string newname, string copyfrom, string root)
        {
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null) return false;

            try
            {
                return (bool)node.PropertyType
                    .GetMethod("TryCloneCollection", new Type[] { typeof(string), typeof(string) })
                    .Invoke(node.GetValue(this), new object[] { newname, copyfrom });
            }
            catch (System.Exception) { return false; }
        }

        /// <summary>
        /// Attempts to clone class specfified in the database.
        /// </summary>
        /// <param name="newname">Collection Name of the new class.</param>
        /// <param name="copyfrom">Collection Name of the class to clone.</param>
        /// <param name="root">Root of the class to clone. Range: Materials, CarTypeInfos, PresetRides.</param>
        /// <param name="error">Error occured while trying to copy class.</param>
        /// <returns>True if class cloning was successful, false otherwise.</returns>
        public virtual bool TryCloneCollection(string newname, string copyfrom, string root, out string error)
        {
            error = null;
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null)
            {
                error = $"Root named {root} does not exist in the database.";
                return false;
            }

            try
            {
                var callargs = new object[] { newname, copyfrom, error };
                bool result = (bool)node.PropertyType
                    .GetMethod("TryCloneCollection", new Type[] { typeof(string), typeof(string), typeof(string).MakeByRefType() })
                    .Invoke(node.GetValue(this), callargs);
                error = callargs[2]?.ToString();
                return result;
            }
            catch (System.Exception)
            {
                error = $"Unable to copy collection in root {root}.";
                return false;
            }
        }

        /// <summary>
        /// Imports class data from a file specified.
        /// </summary>
        /// <param name="root">Class type to be imported. Range: Material, CarTypeInfo, PresetRide, PresetSkin.</param>
        /// <param name="filepath">File with data to be imported.</param>
        /// <returns>True if class import was successful, false otherwise.</returns>
        public virtual bool TryImportCollection(string root, string filepath)
        {
            byte[] data;

            try
            {
                data = File.ReadAllBytes(filepath);
            }
            catch (System.Exception)
            {
                return false;
            }

            var node = this.GetType().GetProperty(root);
            return root == null
                ? false
                : (bool)node.PropertyType
                    .GetMethod("TryImportCollection", new Type[] { typeof(byte).MakeArrayType() })
                    .Invoke(node.GetValue(this), new object[] { data });
        }

        /// <summary>
        /// Imports class data from a file specified.
        /// </summary>
        /// <param name="root">Class type to be imported. Range: Material, CarTypeInfo, PresetRide, PresetSkin.</param>
        /// <param name="filepath">File with data to be imported.</param>
        /// <param name="error">Error occured while trying to import class.</param>
        /// <returns>True if class import was successful, false otherwise.</returns>
        public virtual bool TryImportCollection(string root, string filepath, out string error)
        {
            byte[] data;
            error = null;

            try
            {
                data = File.ReadAllBytes(filepath);
            }
            catch (System.Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                error = e.Message;
                return false;
            }

            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null)
            {
                error = $"Root named {root} does not exist in the database.";
                return false;
            }

            try
            {
                var callargs = new object[] { data, error };
                bool result = (bool)node.PropertyType
                    .GetMethod("TryImportCollection", new Type[] { typeof(byte).MakeArrayType(), typeof(string).MakeByRefType() })
                    .Invoke(node.GetValue(this), callargs);
                error = callargs[1]?.ToString();
                return result;
            }
            catch (System.Exception)
            {
                if (error == null) error = $"Unable to import collection to the root {root}.";
                return false;
            }
        }

        /// <summary>
        /// Exports <see cref="ACollectable"/> data from <see cref="Root{TypeID}"/> 
        /// root to a file path specified.
        /// </summary>
        /// <param name="CName">CollectionName of <see cref="ACollectable"/> class.</param>
        /// <param name="root">Name of the <see cref="Root{TypeID}"/> collection.</param>
        /// <param name="filepath">Filepath where data should be exported.</param>
        /// <returns>True if class export was successful, false otherwise.</returns>
        public virtual bool TryExportCollection(string CName, string root, string filepath)
        {
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null) return false;

            try
            {
                return (bool)node.PropertyType
                    .GetMethod("TryExportCollection", new Type[] { typeof(string), typeof(string) })
                    .Invoke(node.GetValue(this), new object[] { CName, filepath });
            }
            catch (System.Exception) { return false; }
        }

        /// <summary>
        /// Exports <see cref="ACollectable"/> data from <see cref="Root{TypeID}"/> 
        /// root to a file path specified.
        /// </summary>
        /// <param name="CName">CollectionName of <see cref="ACollectable"/> class.</param>
        /// <param name="root">Name of the <see cref="Root{TypeID}"/> collection.</param>
        /// <param name="filepath">Filepath where data should be exported.</param>
        /// <param name="error">Error occured while trying to export class.</param>
        /// <returns>True if class export was successful, false otherwise.</returns>
        public virtual bool TryExportCollection(string CName, string root, string filepath, out string error)
        {
            error = null;
            var node = this.GetType().GetProperty(root ?? string.Empty);
            if (node == null)
            {
                error = $"Root named {root} does not exist in the database.";
                return false;
            }

            try
            {
                var callargs = new object[] { CName, filepath, error };
                bool result = (bool)node.PropertyType
                    .GetMethod("TryExportCollection", new Type[] { typeof(string), typeof(string), typeof(string).MakeByRefType() })
                    .Invoke(node.GetValue(this), callargs);
                error = callargs[2]?.ToString();
                return result;
            }
            catch (System.Exception)
            {
                error = $"Unable to export collection in root {root}.";
                return false;
            }
        }

        /// <summary>
        /// Gets information about <see cref="FileBase"/> database.
        /// </summary>
        /// <returns></returns>
        public string GetDatabaseInfo()
        {

            return String.Empty;
        }
    }
}
