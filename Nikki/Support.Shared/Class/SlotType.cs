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
	/// <see cref="SlotType"/> is a collection of settings related to a parts and slot information.
	/// </summary>
	public abstract class SlotType : Collectable, IAssembly
	{
        #region Shared Enums

        /// <summary>
        /// Enum of animation locations.
        /// </summary>
        public enum CarAnimLocation : sbyte
        {
            /// <summary>
            /// No animations
            /// </summary>
            CARANIM_NONE = -1,

            /// <summary>
            /// Hood animations.
            /// </summary>
            CARANIM_HOOD = 0,

            /// <summary>
            /// Trunk animations.
            /// </summary>
            CARANIM_TRUNK = 1,

            /// <summary>
            /// Left door animations.
            /// </summary>
            CARANIM_LEFT_DOOR = 2,

            /// <summary>
            /// Right door animations.
            /// </summary>
            CARANIM_RIGHT_DOOR = 3,

            /// <summary>
            /// Multiple animations.
            /// </summary>
            CARANIM_NUM = 4,
        }

        #endregion

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
        /// Stock override entry of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string SlotStockOverride { get; set; } = String.Empty;

        /// <summary>
        /// Main override entry of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string SlotMainOverride { get; set; } = String.Empty;

        /// <summary>
        /// Animation type of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public CarAnimLocation PrimaryAnimation { get; set; } = CarAnimLocation.CARANIM_NONE;

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="SlotType"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SlotType"/> with.</param>
		public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="SlotType"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="SlotType"/> with.</param>
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
