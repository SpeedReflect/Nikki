using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Class
{
	/// <summary>
	/// <see cref="Collision"/> is a collection of settings related to a car's bounds.
	/// </summary>
	public abstract class Collision : Collectable, IAssembly
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
        public virtual uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public virtual uint VltKey => this.CollectionName.VltHash();

		#endregion

		#region AccessModifiable Properties

        /// <summary>
        /// Indicates amount of bounds in this <see cref="Collision"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public abstract int NumberOfBounds { get; set; }

        /// <summary>
        /// True if this <see cref="Collision"/> is resolved; false otherwise.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public abstract eBoolean IsResolved { get; set; }

        /// <summary>
        /// Indicates amount of clouds in this <see cref="Collision"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public abstract int NumberOfClouds { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="Collision"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Collision"/> with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="Collision"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Collision"/> with.</param>
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
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
