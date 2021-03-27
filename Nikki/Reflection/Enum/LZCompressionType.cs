namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// LZC Compression type of the buffer.
	/// </summary>
	public enum LZCompressionType : int
	{
		/// <summary>
		/// Use RAWW compression.
		/// </summary>
		RAWW = 0x57574152,

		/// <summary>
		/// Use JDLZ compression.
		/// </summary>
		JDLZ = 0x5A4C444A,

		/// <summary>
		/// Use HUFF compression.
		/// </summary>
		HUFF = 0x46465548,

		/// <summary>
		/// Use COMP compression.
		/// </summary>
		COMP = 0x504D4F43,

		/// <summary>
		/// Use RefPack compression.
		/// </summary>
		RFPK = 0x4B504652,

		/// <summary>
		/// Use BEST compression (system chooses which one depending on the smallest size). 
		/// </summary>
		BEST = 0x54534542,
	}
}
