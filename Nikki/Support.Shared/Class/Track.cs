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
	/// <see cref="Track"/> is a collection of settings related to races and events.
    /// </summary>
	public abstract class Track : ACollectable, IAssemblable
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
		/// Indicates whether race is valid.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean IsValid { get; set; }

		/// <summary>
		/// Indicates whether race is looping.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean IsLoopingRace { get; set; }

		/// <summary>
		/// Indicates whether reverse version of the race exists.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean ReverseVersionExists { get; set; }

		/// <summary>
		/// Represents debug description of the race.
		/// </summary>
		[AccessModifiable()]
		public string RaceDescription { get; set; } = String.Empty;

		/// <summary>
		/// Represents region in which the track and its values are stored.
		/// </summary>
		[AccessModifiable()]
		public string RegionName { get; set; } = String.Empty;

		/// <summary>
		/// Represents directory in which the track is stored.
		/// </summary>
		[AccessModifiable()]
		public string TrackDirectory { get; set; } = String.Empty;

		/// <summary>
		/// Represents directory in which track's region is stored.
		/// </summary>
		[AccessModifiable()]
		public string RegionDirectory { get; set; } = String.Empty;

		/// <summary>
		/// Represents the race gameplay mode of the track.
		/// </summary>
		[AccessModifiable()]
		public eRaceGameplayMode RaceGameplayMode { get; set; }

		/// <summary>
		/// Difficulty of the track when it has a forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eTrackDifficulty DifficultyForward { get; set; }

		/// <summary>
		/// Difficulty of the track when it has a reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eTrackDifficulty DifficultyReverse { get; set; }

		/// <summary>
		/// Represents sun type during race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string SunInfoName { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Track"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Track"/> with.</param>
		public abstract void Assemble(BinaryWriter bw);

        /// <summary>
		/// Disassembles array into <see cref="Track"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Track"/> with.</param>
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
