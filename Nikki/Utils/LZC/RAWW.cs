using System;



namespace Nikki.Utils.LZC
{
	/// <summary>
	/// Collection with RAWW decompressor.
	/// </summary>
	public static class RAWW
	{
		/// <summary>
		/// Decompresses RAWW byte array.
		/// </summary>
		/// <param name="input">Byte array to be decompressed.</param>
		/// <returns>Byte array of decompressed data.</returns>
		public static byte[] Decompress(byte[] input)
		{
			if (input == null || input.Length < 0x10) return null;

			var outsize = BitConverter.ToInt32(input, 8);
			var output = new byte[outsize];
			Buffer.BlockCopy(input, 0x10, output, 0, outsize);
			return output;
		}

		/// <summary>
		/// Compresses byte block into RAWW-compressed one.
		/// </summary>
		/// <param name="input">Byte buffer to compress.</param>
		/// <returns>JDLZ-compressed byte array.</returns>
		public static byte[] Compress(byte[] input)
		{
			if (input == null) return null;

			var insize = input.Length;
			var outsize = 0x10 + input.Length;
			var output = new byte[0x10 + input.Length];
			output[0x00] = 0x52; // 'R'
			output[0x01] = 0x41; // 'A'
			output[0x02] = 0x57; // 'W'
			output[0x03] = 0x57; // 'W'
			output[0x04] = 0x01;
			output[0x05] = 0x10;
			output[0x06] = 0x00;
			output[0x07] = 0x00;
			output[0x08] = (byte)insize;
			output[0x09] = (byte)(insize >> 8);
			output[0x0A] = (byte)(insize >> 16);
			output[0x0B] = (byte)(insize >> 24);
			output[0x0C] = (byte)(outsize);
			output[0x0D] = (byte)(outsize >> 8);
			output[0x0E] = (byte)(outsize >> 16);
			output[0x0F] = (byte)(outsize >> 24);

			Array.Copy(input, 0, output, 0x10, input.Length);

			return output;
		}

		/// <summary>
		/// Compresses byte block into RAWW-compressed one.
		/// </summary>
		/// <param name="input">Byte buffer to compress.</param>
		/// <param name="start">Start index of compression.</param>
		/// <param name="count">Number of bytes to compress.</param>
		/// <returns>JDLZ-compressed byte array.</returns>
		public static byte[] Compress(byte[] input, int start, int count)
		{
			if (input == null) return null;
			if (start < 0 || count <= 0) return null;
			if (start + count > input.Length) return null;

			var insize = count;
			var outsize = 0x10 + count;
			var output = new byte[0x10 + count];
			output[0x00] = 0x52; // 'R'
			output[0x01] = 0x41; // 'A'
			output[0x02] = 0x57; // 'W'
			output[0x03] = 0x57; // 'W'
			output[0x04] = 0x01;
			output[0x05] = 0x10;
			output[0x06] = 0x00;
			output[0x07] = 0x00;
			output[0x08] = (byte)insize;
			output[0x09] = (byte)(insize >> 8);
			output[0x0A] = (byte)(insize >> 16);
			output[0x0B] = (byte)(insize >> 24);
			output[0x0C] = (byte)(outsize);
			output[0x0D] = (byte)(outsize >> 8);
			output[0x0E] = (byte)(outsize >> 16);
			output[0x0F] = (byte)(outsize >> 24);

			Array.Copy(input, start, output, 0x10, count);

			return output;
		}
	}
}
