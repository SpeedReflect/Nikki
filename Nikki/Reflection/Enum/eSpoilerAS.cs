namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of possible autosculpt spoiler types.
	/// </summary>
	public enum eSpoilerAS2 : uint
	{
		/// <summary>
		/// Regular autosculpt spoilers.
		/// </summary>
		SPOILER_AS2 = 0x21D461E2,

		/// <summary>
		/// Autosculpt spoilers for hatchbacks.
		/// </summary>
		SPOILER_HATCH_AS2 = 0x645AB209,

		/// <summary>
		/// Autosculpt spoilers for cars with porsche-like trunks.
		/// </summary>
		SPOILER_PORSCHES_AS2 = 0x33C2F508,

		/// <summary>
		/// Autosculpt spoilers for cars with wide trunks.
		/// </summary>
		SPOILER_CARRERA_AS2 = 0x34A77E01,

		/// <summary>
		/// Autosculpt spoilers for SUV cars.
		/// </summary>
		SPOILER_SUV_AS2 = 0x4819C33F,

		/// <summary>
		/// No autosculpt spoilers.
		/// </summary>
		SPOILER_NONE_AS2 = 0xCBBBFF51,
	}
}
