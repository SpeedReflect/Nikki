namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of possible spoiler types.
	/// </summary>
	public enum eSpoiler : uint
	{
		/// <summary>
		/// Regular spoilers.
		/// </summary>
		SPOILER = 0xC93B73FD,
		
		/// <summary>
		/// Spoilers for hatchbacks.
		/// </summary>
		SPOILER_HATCH = 0xE8E9C8A4,

		/// <summary>
		/// Spoilers for cars with porsche-like trunks.
		/// </summary>
		SPOILER_PORSCHES = 0xAE826423,

		/// <summary>
		/// Spoilers for cars with wide trunks.
		/// </summary>
		SPOILER_CARRERA = 0x497F589C,

		/// <summary>
		/// Spoilers for SUV cars.
		/// </summary>
		SPOILER_SUV = 0x21D4AEDA,

		/// <summary>
		/// No spoilers.
		/// </summary>
		SPOILER_NONE = 0x5C67B1EC,
	}
}
