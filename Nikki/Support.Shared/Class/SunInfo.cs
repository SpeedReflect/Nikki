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
    /// <see cref="SunInfo"/> is a collection of sun and daylight settings.
    /// </summary>
	public abstract class SunInfo : Collectable, IAssembly
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
		/// Version of the sun info.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int Version { get; set; } = 2;

		/// <summary>
		/// Position X of the sun.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float PositionX { get; set; }

		/// <summary>
		/// Position Y of the sun.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float PositionY { get; set; }

		/// <summary>
		/// Position Z of the sun.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float PositionZ { get; set; }

		/// <summary>
		/// Position X of car's shadow.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float CarShadowPositionX { get; set; }

		/// <summary>
		/// Position Y of car's shadow.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float CarShadowPositionY { get; set; }

		/// <summary>
		/// Position Z of car's shadow.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float CarShadowPositionZ { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="SunInfo"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SunInfo"/> with.</param>
		public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="SunInfo"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="SunInfo"/> with.</param>
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
