using System;
using System.Runtime.InteropServices;



namespace Nikki.Utils
{
	/// <summary>
	/// Collection with HUFF compresssor and decompressor.
	/// </summary>
	public static class Interop
	{
		[DllImport("LZCompressLib.dll", EntryPoint = "BlockDecompress", CallingConvention = CallingConvention.Cdecl)]
		private static extern int PrivateDecode([In] byte[] input, int insize, [Out] byte[] output, int outsize);

		/// <summary>
		/// Decompresses JDLZ or HUFF compression using LZCompressLib library.
		/// </summary>
		/// <param name="input">Array to decompress.</param>
		/// <returns>Decompressed data.</returns>
		public static byte[] Decompress(byte[] input)
		{
			if (input == null || input.Length < 0x10) return null;
			var type = BitConverter.ToUInt32(input, 0);
			if (type != 0x5A4C444A && type != 0x46465548) return null;
			var insize = input.Length;
			var outsize = BitConverter.ToInt32(input, 8);
			var output = new byte[outsize];

			int result = PrivateDecode(input, insize, output, outsize);

			return output;
		}
	}
}
