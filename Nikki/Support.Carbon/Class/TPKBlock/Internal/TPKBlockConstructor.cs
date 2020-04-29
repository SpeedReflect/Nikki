using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Shared.Parts.TPKParts;



namespace Nikki.Support.Carbon.Class
{
    public partial class TPKBlock : Shared.Class.TPKBlock
    {
        #region Fields

        private string _collection_name;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.Carbon;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Carbon.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Carbon Database { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                if (value.Length > 0x1B)
                    throw new ArgumentLengthException(0x1B);
                if (this.Database.TPKBlocks.FindCollection(value) != null)
                    throw new CollectionExistenceException();
                this._collection_name = value;
            }
        }

        /// <summary>
        /// Version of this <see cref="TPKBlock"/>.
        /// </summary>
        public override eTPKVersion Version => eTPKVersion.Carbon;

        /// <summary>
        /// Filename used for this <see cref="TPKBlock"/>. It is a default watermark.
        /// </summary>
        public override string Filename => Settings.Watermark;

        /// <summary>
        /// BinKey of the filename.
        /// </summary>
        public override uint FilenameHash => this.Filename.BinHash();

        /// <summary>
        /// If true, indicates that this <see cref="TPKBlock"/> is compressed and 
        /// should be saved as compressed on the output.
        /// </summary>
        public override eBoolean IsCompressed { get; set; }

        /// <summary>
        /// True if CollectionName specified should be used; false if hardcoded CollectionName 
        /// should be used.
        /// </summary>
        public eBoolean UseCurrentName { get; set; }

        /// <summary>
        /// List of <see cref="Texture"/> in this <see cref="TPKBlock"/>.
        /// </summary>
        public List<Texture> Textures { get; set; } = new List<Texture>();

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="TPKBlock"/>.
		/// </summary>
		public TPKBlock() { this.UseCurrentName = eBoolean.True; }

        /// <summary>
        /// Initializes new instance of <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public TPKBlock(string CName, Database.Carbon db)
        {
            this.Database = db;
            this.CollectionName = CName;
            this.UseCurrentName = eBoolean.True;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="index">Index of the instance in the database.</param>
		/// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public TPKBlock(int index, Database.Carbon db)
        {
            if (index < 0) this.UseCurrentName = eBoolean.True;
            this.Database = db;
            this.Index = index;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to find <see cref="Texture"/> based on the key passed.
        /// </summary>
        /// <param name="key">Key of the <see cref="Texture"/> Collection Name.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <returns>Texture if it is found; null otherwise.</returns>
        public override Shared.Class.Texture FindTexture(uint key, eKeyType type) =>
            type switch
            {
                eKeyType.BINKEY => this.Textures.Find(_ => _.BinKey == key),
                eKeyType.VLTKEY => this.Textures.Find(_ => _.VltKey == key),
                eKeyType.CUSTOM => throw new NotImplementedException(),
                _ => null
            };

        /// <summary>
        /// Sorts <see cref="Texture"/> by their CollectionNames or BinKeys.
        /// </summary>
        /// <param name="by_name">True if sort by name; false is sort by hash.</param>
        public override void SortTexturesByType(bool by_name)
        {
            if (!by_name) this.Textures.Sort((x, y) => x.BinKey.CompareTo(y.BinKey));
            else this.Textures.Sort((x, y) => x.CollectionName.CompareTo(y.CollectionName));
        }

        /// <summary>
        /// Gets all textures of this <see cref="TPKBlock"/>.
        /// </summary>
        /// <returns>Textures as an object.</returns>
        public override object GetTextures() => this.Textures;

        /// <summary>
        /// Gets index of the <see cref="Texture"/> in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/>.</param>
        /// <param name="type">Key type passed.</param>
        /// <returns>Index number as an integer. If element does not exist, returns -1.</returns>
        public override int GetTextureIndex(uint key, eKeyType type)
        {
            switch (type)
            {
                case eKeyType.BINKEY:
                    for (int a1 = 0; a1 < this.Textures.Count; ++a1)
                    {
                        if (this.Textures[a1].BinKey == key) return a1;
                    }
                    break;

                case eKeyType.VLTKEY:
                    for (int a1 = 0; a1 < this.Textures.Count; ++a1)
                    {
                        if (this.Textures[a1].VltKey == key) return a1;
                    }
                    break;

                case eKeyType.CUSTOM:
                    throw new NotImplementedException();

                default:
                    break;
            }
            return -1;
        }

        /// <summary>
        /// Attempts to add <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="filename">Path of the texture to be imported.</param>
        /// <returns>True if texture adding was successful, false otherwise.</returns>
        public override bool TryAddTexture(string CName, string filename)
        {
            if (string.IsNullOrWhiteSpace(CName)) return false;

            if (this.FindTexture(CName.BinHash(), eKeyType.BINKEY) != null)
                return false;

            if (!Comp.IsDDSTexture(filename))
                return false;

            var texture = new Texture(CName, this.CollectionName, filename, this.Database);
            this.Textures.Add(texture);
            return true;
        }

        /// <summary>
        /// Attempts to add <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="filename">Path of the texture to be imported.</param>
        /// <param name="error">Error occured when trying to add a texture.</param>
        /// <returns>True if texture adding was successful, false otherwise.</returns>
        public override bool TryAddTexture(string CName, string filename, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(CName))
            {
                error = $"Collection Name cannot be empty or whitespace.";
                return false;
            }

            if (this.FindTexture(CName.BinHash(), eKeyType.BINKEY) != null)
            {
                error = $"Texture named {CName} already exists.";
                return false;
            }

            if (!Comp.IsDDSTexture(filename))
            {
                error = $"Texture passed is not a DDS texture.";
                return false;
            }

            var texture = new Texture(CName, this.CollectionName, filename, this.Database);
            this.Textures.Add(texture);
            return true;
        }

        /// <summary>
        /// Attempts to remove <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type fo the key passed.</param>
        /// <returns>True if texture removing was successful, false otherwise.</returns>
        public override bool TryRemoveTexture(uint key, eKeyType type)
        {
            var index = this.GetTextureIndex(key, type);
            if (index == -1) return false;
            this.Textures.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Attempts to remove <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="error">Error occured when trying to remove a texture.</param>
        /// <returns>True if texture removing was successful, false otherwise.</returns>
        public override bool TryRemoveTexture(uint key, eKeyType type, out string error)
        {
            error = null;
            var index = this.GetTextureIndex(key, type);
            if (index == -1)
            {
                error = $"Texture with key 0x{key:X8} does not exist.";
                return false;
            }
            this.Textures.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Attempts to clone <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <returns>True if texture cloning was successful, false otherwise.</returns>
        public override bool TryCloneTexture(string newname, uint key, eKeyType type)
        {
            if (string.IsNullOrWhiteSpace(newname)) return false;

            if (this.FindTexture(newname.BinHash(), type) != null)
                return false;

            var copyfrom = (Texture)this.FindTexture(key, type);
            if (copyfrom == null) return false;

            var texture = (Texture)copyfrom.MemoryCast(newname);
            this.Textures.Add(texture);
            return true;
        }

        /// <summary>
        /// Attempts to clone <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="error">Error occured when trying to clone a texture.</param>
        /// <returns>True if texture cloning was successful, false otherwise.</returns>
        public override bool TryCloneTexture(string newname, uint key, eKeyType type, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(newname))
            {
                error = $"CollectionName cannot be empty or whitespace.";
                return false;
            }

            if (this.FindTexture(newname.BinHash(), type) != null)
            {
                error = $"Texture with CollectionName {newname} already exists.";
                return false;
            }

            var copyfrom = (Texture)this.FindTexture(key, type);
            if (copyfrom == null)
            {
                error = $"Texture with key 0x{key:X8} does not exist.";
                return false;
            }

            var texture = (Texture)copyfrom.MemoryCast(newname);
            this.Textures.Add(texture);
            return true;
        }

        /// <summary>
        /// Attemps to replace <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        /// <returns>True if texture replacing was successful, false otherwise.</returns>
        public override bool TryReplaceTexture(uint key, eKeyType type, string filename)
        {
            var tex = (Texture)this.FindTexture(key, type);
            if (tex == null) return false;
            if (!Comp.IsDDSTexture(filename)) return false;
            tex.Reload(filename);
            return true;
        }

        /// <summary>
        /// Attemps to replace <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        /// <param name="error">Error occured when trying to replace a texture.</param>
        /// <returns>True if texture replacing was successful, false otherwise.</returns>
        public override bool TryReplaceTexture(uint key, eKeyType type, string filename, out string error)
        {
            error = null;
            var tex = (Texture)this.FindTexture(key, type);
            if (tex == null)
            {
                error = $"Texture with key 0x{key:X8} does not exist.";
                return false;
            }

            if (!Comp.IsDDSTexture(filename))
            {
                error = $"File {filename} is not a valid DDS texture.";
                return false;
            }

            tex.Reload(filename);
            return true;
        }

        #endregion
    }
}