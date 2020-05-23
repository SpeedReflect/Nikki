using System;
using Nikki.Utils.LZC;
using Nikki.Reflection.Enum;



namespace Nikki.Utils
{
	/// <summary>
	/// Collection with HUFF compresssor and decompressor.
	/// </summary>
	public static class Interop
	{
		/// <summary>
		/// Decompresses buffer based on its header.
		/// </summary>
		/// <param name="input">Array to decompress.</param>
		/// <returns>Decompressed data.</returns>
		public static byte[] Decompress(byte[] input)
		{
			if (input == null || input.Length < 0x10) return null;
			var type = (eLZCompressionType)BitConverter.ToUInt32(input, 0);

			return type switch
			{
				eLZCompressionType.RAWW => RAWW.Decompress(input),
				eLZCompressionType.JDLZ => JDLZ.Decompress(input),
				eLZCompressionType.HUFF => HUFF.Decompress(input),
				eLZCompressionType.COMP => COMP.Decompress(input),
				_ => input,
			};
		}
	
		/// <summary>
		/// Compresses buffer based on compression type passed.
		/// </summary>
		/// <param name="input">Array to compress.</param>
		/// <param name="type"><see cref="eLZCompressionType"/> of the compression.</param>
		/// <returns>Compressed data.</returns>
		public static byte[] Compress(byte[] input, eLZCompressionType type)
		{
			return input == null
				? null
				: (type switch
			{
				eLZCompressionType.RAWW => RAWW.Compress(input),
				eLZCompressionType.JDLZ => JDLZ.Compress(input),
				_ => null,
			});
		}
	}
}
