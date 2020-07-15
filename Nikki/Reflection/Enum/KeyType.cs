namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Type of the key/hash.
	/// </summary>
	public enum KeyType : int
	{
		/// <summary>
		/// Default system HashCode.
		/// </summary>
		DEFAULT = 0,

		/// <summary>
		/// BinHash key.
		/// </summary>
		BINKEY = 1,

		/// <summary>
		/// VltHash key.
		/// </summary>
		VLTKEY = 2,

		/// <summary>
		/// Custom key.
		/// </summary>
		CUSTOM = 3,
	}
}
