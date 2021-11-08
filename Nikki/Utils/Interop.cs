using System;
using System.Runtime.InteropServices;
using Nikki.Reflection.Enum;



namespace Nikki.Utils
{
	/// <summary>
	/// Collection with HUFF compresssor and decompressor.
	/// </summary>
	public static class Interop
	{
		[DllImport("LZCompressLib.dll", EntryPoint = "BlockDecompress", CallingConvention = CallingConvention.Cdecl)]
		private static extern int PrivateDecode([In] byte[] input, int insize, [Out] byte[] output);

		[DllImport("LZCompressLib.dll", EntryPoint = "BlockCompress", CallingConvention = CallingConvention.Cdecl)]
		private static extern int PrivateEncode([In] byte[] input, int insize, [Out] byte[] output, int comp);

		[DllImport("LZCompressLib.dll", EntryPoint = "BlockDecompress", CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe int PrivateDecode(byte* input, int insize, byte* output);

		[DllImport("LZCompressLib.dll", EntryPoint = "BlockCompress", CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe int PrivateEncode(byte* input, int insize, byte* output, int comp);

		private static readonly object _s_lock = new object();

		/// <summary>
		/// Decompresses buffer based on its header.
		/// </summary>
		/// <param name="input">Array to decompress.</param>
		/// <returns>Decompressed data.</returns>
		public static byte[] Decompress(byte[] input)
		{
			if (input == null || input.Length < 0x10) return null;

			var type = BitConverter.ToInt32(input, 0);
			if (!Enum.IsDefined(typeof(LZCompressionType), type)) return input;

			var outsize = BitConverter.ToInt32(input, 8);
			var output = new byte[outsize];

			lock (Interop._s_lock)
			{
			
				PrivateDecode(input, input.Length, output);
			
			}

			return output;
		}
	
		/// <summary>
		/// Compresses buffer based on compression type passed.
		/// </summary>
		/// <param name="input">Array to compress.</param>
		/// <param name="type">Type of the compression.</param>
		/// <returns>Compressed data.</returns>
		public static byte[] Compress(byte[] input, LZCompressionType type)
		{
			if (input == null) return null;

			var output = new byte[input.Length << 1];
			int outsize = 0;

			lock (Interop._s_lock)
			{
			
				outsize = PrivateEncode(input, input.Length, output, (int)type);
			
			}

			Array.Resize(ref output, outsize);
			return output;
		}

		/// <summary>
		/// Compresses buffer based on compression type passed.
		/// </summary>
		/// <param name="input">Array to compress.</param>
		/// <param name="start">Start index of compression.</param>
		/// <param name="count">Number of bytes to compress.</param>
		/// <param name="type">Type of the compression.</param>
		/// <returns>Compressed data.</returns>
		public static byte[] Compress(byte[] input, int start, int count, LZCompressionType type)
		{
			if (input == null) return null;
			if (start < 0 || count <= 0) return null;
			if (start + count > input.Length) return null;

			var output = new byte[count << 1];
			int outsize = 0;

			unsafe
			{

				fixed (byte* inptr = &input[start])
				{

					fixed (byte* outptr = &output[0])
					{

						lock (Interop._s_lock)
						{

							outsize = PrivateEncode(inptr, count, outptr, (int)type);

						}

					}

				}

			}

			Array.Resize(ref output, outsize);
			return output;
		}
	}
}
