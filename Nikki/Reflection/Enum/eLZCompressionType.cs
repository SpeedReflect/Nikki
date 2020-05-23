namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// LZC Compression type of the buffer.
	/// </summary>
	public enum eLZCompressionType : uint
	{
		/// <summary>
		/// RAWW compression: requires usage of <see cref="Utils.LZC.RAWW"/>.
		/// </summary>
		RAWW = 0x57574152,

		/// <summary>
		/// JDLZ compression: requires usage of <see cref="Utils.LZC.JDLZ"/>.
		/// </summary>
		JDLZ = 0x5A4C444A,

		/// <summary>
		/// HUFF compression: requires usage of <see cref="Utils.LZC.HUFF"/>.
		/// </summary>
		HUFF = 0x46465548,

		/// <summary>
		/// COMP compression: requires usage of <see cref="Utils.LZC.COMP"/>.
		/// </summary>
		COMP = 0x504D4F43,
	}
}
