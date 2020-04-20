namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of all mirror types in Underground 2.
	/// </summary>
	public enum eMirrorTypes : uint
	{
		/// <summary>
		/// Regular mirrors.
		/// </summary>
		MIRRORS = 0xEB45326D,

		/// <summary>
		/// Mirrors for cars with wide body.
		/// </summary>
		MIRRORS_BODY = 0x6573609A,

		/// <summary>
		/// Special mirrors.
		/// </summary>
		MIRRORS_POST = 0x657B0FD2,

		/// <summary>
		/// Murrors for SUV cars.
		/// </summary>
		MIRRORS_SUV = 0x9678254A,

		/// <summary>
		/// Mirrors for hummer cars.
		/// </summary>
		MIRRORS_HUMMER = 0x9E3E3B7A,

		/// <summary>
		/// No mirrors.
		/// </summary>
		MIRRORS_NONE = 0x6579F65C,
	}
}
