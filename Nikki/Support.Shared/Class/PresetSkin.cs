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
    /// <see cref="PresetSkin"/> is a collection of settings related to car skins.
    /// </summary>
    public abstract class PresetSkin : Collectable, IAssembly
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
        /// Paint type value of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string PaintType { get; set; } = String.Empty;

        /// <summary>
        /// Gradient color value of the paint of the preset skin. Range: 0-90.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string PaintSwatch { get; set; } = String.Empty;

        /// <summary>
        /// Saturation value of the paint of the preset skin. Range: (float)0-1.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float PaintSaturation { get; set; }

        /// <summary>
        /// Brightness value of the paint of the preset skin. Range: (float)0-1.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float PaintBrightness { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="PresetSkin"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PresetSkin"/> with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="PresetSkin"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="PresetSkin"/> with.</param>
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