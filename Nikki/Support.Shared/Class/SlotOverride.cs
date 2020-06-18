using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Class
{
	/// <summary>
	/// <see cref="SlotOverride"/> is a collection of settings related to car's slot overrides.
	/// </summary>
	public abstract class SlotOverride : Collectable, IAssembly
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
        /// Main override entry of this <see cref="SlotOverride"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string InfoMainOverride { get; set; } = String.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="SlotOverride"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SlotOverride"/> with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="SlotOverride"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="SlotOverride"/> with.</param>
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
