using System;
using System.Runtime.InteropServices;



namespace Nikki.Utils.LZC
{
	/// <summary>
	/// Collection with HUFF decompressor.
	/// </summary>
	public static class HUFF
	{
		[DllImport("LZCompressLib.dll", EntryPoint = "BlockDecompress", CallingConvention = CallingConvention.Cdecl)]
		private static extern int PrivateDecode([In] byte[] input, int insize, [Out] byte[] output, int outsize);

		/// <summary>
		/// Decompresses HUFF byte array.
		/// </summary>
		/// <param name="input">Byte array to be decompressed.</param>
		/// <returns>Byte array of decompressed data.</returns>
		public static byte[] Decompress(byte[] input)
		{
			if (input == null || input.Length < 0x10) return null;

			var insize = input.Length;
			var outsize = BitConverter.ToInt32(input, 8);
			var output = new byte[outsize];
			int result = PrivateDecode(input, insize, output, outsize);
			return output;
		}
	}
}
