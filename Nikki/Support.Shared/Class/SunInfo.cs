using System;
using System.IO;
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
	public abstract class SunInfo : ACollectable, IAssemblable
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
		public int Version { get; set; } = 2;

		/// <summary>
		/// Position X of the sun.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float PositionX { get; set; }

		/// <summary>
		/// Position Y of the sun.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float PositionY { get; set; }

		/// <summary>
		/// Position Z of the sun.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float PositionZ { get; set; }

		/// <summary>
		/// Position X of car's shadow.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float CarShadowPositionX { get; set; }

		/// <summary>
		/// Position Y of car's shadow.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float CarShadowPositionY { get; set; }

		/// <summary>
		/// Position Z of car's shadow.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
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
