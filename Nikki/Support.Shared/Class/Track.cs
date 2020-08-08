using System;
using System.IO;
using System.ComponentModel;
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
	public abstract class Track : Collectable, IAssembly
	{
		#region Shared Enums

		/// <summary>
		/// Enum of <see cref="Track"/> drift event types.
		/// </summary>
		public enum TrackDriftType : int
		{
			/// <summary>
			/// Drift versus AI.
			/// </summary>
			VS_AI = 0,

			/// <summary>
			/// Drift downhill.
			/// </summary>
			DOWNHILL = 1,

			/// <summary>
			/// Team-based drift.
			/// </summary>
			TEAM = 2,
		}

		/// <summary>
		/// Enum of <see cref="Track"/> location types.
		/// </summary>
		public enum TrackLocationType : int
		{
			/// <summary>
			/// Beacon Hill.
			/// </summary>
			UPPER_CLASS = 0,

			/// <summary>
			/// City Core.
			/// </summary>
			CITY_CORE = 1,

			/// <summary>
			/// Jackson Heights.
			/// </summary>
			SUBURBAN_HILLS = 2,

			/// <summary>
			/// Coal Harbor.
			/// </summary>
			INDUSTRIAL_PARK = 3,

			/// <summary>
			/// Airport.
			/// </summary>
			AIRPORT = 4,

			/// <summary>
			/// Debug.
			/// </summary>
			MODE_SPECIFIC = 5,
		}

		/// <summary>
		/// Enum of all <see cref="Track"/> gameplay modes.
		/// </summary>
		public enum TrackGameplayMode : int
		{
			/// <summary>
			/// 
			/// </summary>
			NONE = 0,

			/// <summary>
			/// 
			/// </summary>
			DRAG = 2,

			/// <summary>
			/// 
			/// </summary>
			DRIFT = 8,

			/// <summary>
			/// 
			/// </summary>
			OPENWORLD = 16,

			/// <summary>
			/// 
			/// </summary>
			SPRINT = 52,

			/// <summary>
			/// 
			/// </summary>
			OUTRUN = 112,

			/// <summary>
			/// 
			/// </summary>
			CIRCUIT = 113,

			/// <summary>
			/// 
			/// </summary>
			STREETX = 2048,

			/// <summary>
			/// 
			/// </summary>
			DRIFTSTREETX = 2056,

			/// <summary>
			/// 
			/// </summary>
			URL = 8208,

			/// <summary>
			/// 
			/// </summary>
			DRIFTURL = 8216,
		}

		/// <summary>
		/// Enum of <see cref="Track"/> difficulties.
		/// </summary>
		public enum TrackDifficulty : byte
		{
			/// <summary>
			/// Easy difficulty.
			/// </summary>
			TRACK_DIFFICULTY_EASY = 0,

			/// <summary>
			/// Medium difficulty.
			/// </summary>
			TRACK_DIFFICULTY_MEDIUM = 1,

			/// <summary>
			/// Hard difficulty.
			/// </summary>
			TRACK_DIFFICULTY_HARD = 2,
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
		/// Indicates whether race is valid.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public eBoolean IsValid { get; set; }

		/// <summary>
		/// Indicates whether race is looping.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public eBoolean IsLoopingRace { get; set; }

		/// <summary>
		/// Indicates whether reverse version of the race exists.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public eBoolean ReverseVersionExists { get; set; }

		/// <summary>
		/// Represents debug description of the race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string RaceDescription { get; set; } = String.Empty;

		/// <summary>
		/// Represents region in which the track and its values are stored.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string RegionName { get; set; } = String.Empty;

		/// <summary>
		/// Represents directory in which the track is stored.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string TrackDirectory { get; set; } = String.Empty;

		/// <summary>
		/// Represents directory in which track's region is stored.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string RegionDirectory { get; set; } = String.Empty;

		/// <summary>
		/// Represents the race gameplay mode of the track.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public TrackGameplayMode RaceGameplayMode { get; set; }

		/// <summary>
		/// Difficulty of the track when it has a forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public TrackDifficulty DifficultyForward { get; set; }

		/// <summary>
		/// Difficulty of the track when it has a reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public TrackDifficulty DifficultyReverse { get; set; }

		/// <summary>
		/// Represents sun type during race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
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
