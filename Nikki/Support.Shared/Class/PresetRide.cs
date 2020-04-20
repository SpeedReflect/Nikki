using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.PresetParts;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="PresetRide"/> is a collection of specific settings of a ride.
    /// </summary>
    public abstract class PresetRide : ACollectable
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

        /// <summary>
        /// Represents model of the preset ride.
        /// </summary>
        [AccessModifiable()]
        public abstract string MODEL { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="PresetRide"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PresetRide"/> with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="PresetRide"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="PresetRide"/> with.</param>
        public abstract void Disassemble(BinaryReader br);

        /// <summary>
        /// Converts all preset parts into binary memory keys.
        /// </summary>
        /// <param name="parts"><see cref="Concatenator"/> class of all preset ride's parts.</param>
        /// <returns>Sorted array of all preset parts hashes.</returns>
        protected abstract uint[] StringToKey(Concatenator parts);

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}