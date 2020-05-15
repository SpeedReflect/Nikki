using System;



namespace Nikki.Core
{
	/// <summary>
	/// Flags that specify what collections to load.
	/// </summary>
	[Flags()]
	public enum eOptFlags : uint
	{
		/// <summary>
		/// Load nothing.
		/// </summary>
		None = 0x1,

		/// <summary>
		/// Load Material classes.
		/// </summary>
		Materials = 0x2,

		/// <summary>
		/// Load TPKBlock classes.
		/// </summary>
		TPKBlocks = 0x4,

		/// <summary>
		/// Load CarTypeInfo classes.
		/// </summary>
		CarTypeInfos = 0x8,
		
		/// <summary>
		/// Load PresetRide classes.
		/// </summary>
		PresetRides = 0x10,

		/// <summary>
		/// Load PresetSkin classes.
		/// </summary>
		PresetSkins = 0x20,
		
		/// <summary>
		/// Load Collision classes.
		/// </summary>
		Collisions = 0x40,

		/// <summary>
		/// Load DBModelPart classes.
		/// </summary>
		DBModelParts = 0x80,

		/// <summary>
		/// Load FNGroup classes.
		/// </summary>
		FNGroups = 0x100,

		/// <summary>
		/// Load STRBlock classes.
		/// </summary>
		STRBlocks = 0x200,

		/// <summary>
		/// Load AcidEffect classes.
		/// </summary>
		AcidEffects = 0x400,

		/// <summary>
		/// Load SunInfo classes.
		/// </summary>
		SunInfos = 0x800,

		/// <summary>
		/// Load Track classes.
		/// </summary>
		Tracks = 0x1000,

		/// <summary>
		/// Load GCareerRace classes.
		/// </summary>
		GCareer = 0x2000,
	}
}
