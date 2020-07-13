using Nikki.Support.Shared.Class;



namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Compression type of a <see cref="TPKBlock"/>.
	/// </summary>
	public enum eTPKCompressionType : int
	{
		/// <summary>
		/// All TPK textures data are stored raw decompressed.
		/// </summary>
		RawDecompressed = 0,

		/// <summary>
		/// All TPK textures data are stored decompressed using stream rules.
		/// </summary>
		StreamDecompressed = 1,

		/// <summary>
		/// All TPK textures data are compressed fully without splitting in parts.
		/// </summary>
		CompressedFullData = 2,

		/// <summary>
		/// All TPK textures data are compressed fully by splitting and compressing them in parts.
		/// </summary>
		CompressedByParts = 3,
	}
}
