using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Underground2.Gameplay;
using Nikki.Support.Underground2.Framework;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="GCareer"/> is a collection of gameplay classes.
    /// </summary>
    public class GCareer : Shared.Class.GCareer
    {
        #region Fields

        private string _collection_name;
        private List<BankTrigger> _bank_triggers;


        private const long max = 0x7FFFFFFF;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override GameINT GameINT => GameINT.Underground2;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.Underground2.ToString();

        /// <summary>
        /// Manager to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public GCareerManager Manager { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [Category("Main")]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                this.Manager?.CreationCheck(value);
                this._collection_name = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BankTrigger> BankTriggers => this._bank_triggers;

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="GCareer"/>.
        /// </summary>
        public GCareer()
		{

		}

        /// <summary>
        /// Initializes new instance of <see cref="GCareer"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="GCareerManager"/> to which this instance belongs to.</param>
        public GCareer(string CName, GCareerManager manager) : this()
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="GCareer"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="GCareerManager"/> to which this instance belongs to.</param>
        public GCareer(BinaryReader br, GCareerManager manager) : this()
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Assembles <see cref="GCareer"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="TPKBlock"/> with.</param>
        /// <returns>Byte array of the tpk block.</returns>
        public override void Assemble(BinaryWriter bw)
        {

        }

        /// <summary>
        /// Disassembles <see cref="GCareer"/> array into separate properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Disassemble(BinaryReader br)
        {

        }

        /// <summary>
        /// Gets all collections of type <see cref="Collectable"/>.
        /// </summary>
        /// <typeparam name="T">A <see cref="Collectable"/> collections to get.</typeparam>
        /// <returns>Collections of type specified, if type is registered; null otherwise.</returns>
        public override IEnumerable<T> GetCollections<T>()
		{

            return null;
		}

        /// <summary>
        /// Gets collection of with CollectionName specified from a root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to get.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        /// <returns>Collection, if exists; null otherwise.</returns>
        public override Collectable GetCollection(string cname, string root)
		{

            return null;
		}

        /// <summary>
        /// Adds a unit collection at a root provided with CollectionName specified.
        /// </summary>
        /// <param name="cname">CollectionName of a new collection.</param>
        /// <param name="root">Root to which collection should belong to.</param>
        public override void AddCollection(string cname, string root)
		{

		}

        /// <summary>
        /// Removes collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to remove.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public override void RemoveCollection(string cname, string root)
		{

		}

        /// <summary>
        /// Clones collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="newname">CollectionName of a new cloned collection.</param>
        /// <param name="copyname">CollectionName of a collection to clone.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public override void CloneCollection(string newname, string copyname, string root)
		{

		}

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="TPKBlock"/> 
        /// as a string value.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return $"Collection Name: {this.CollectionName} | " +
                   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
        }

        #endregion

        #region Reading Methods

        /// <summary>
        /// Finds offsets of all partials and its parts in the <see cref="GCareer"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCareer"/> with.</param>
        /// <returns>Array of all offsets.</returns>
        protected override long[] FindOffsets(BinaryReader br)
        {



            return null;
        }




        #endregion

        #region Serialization

        /// <summary>
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public override void Serialize(BinaryWriter bw)
        {

        }

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Deserialize(BinaryReader br)
        {

        }

        /// <summary>
        /// Synchronizes all parts of this instance with another instance passed.
        /// </summary>
        /// <param name="other"><see cref="GCareer"/> to synchronize with.</param>
        internal void Synchronize(GCareer other)
        {

        }

        #endregion
    }
}