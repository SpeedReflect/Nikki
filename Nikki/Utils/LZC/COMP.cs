using System;



namespace Nikki.Utils.LZC
{
    /// <summary>
    /// Collection with COMP decompressor.
    /// </summary>
    public static class COMP
	{
        /// <summary>
        /// Decompresses COMP byte array.
        /// </summary>
        /// <param name="input">Byte array to be decompressed.</param>
        /// <returns>Byte array of decompressed data.</returns>
        public static byte[] Decompress(byte[] input)
		{
            if (input == null || input.Length < 0x10) return null;

            var outsize = BitConverter.ToInt32(input, 8);
            var output = new byte[outsize];

            uint flags = 1;

            int inpos = 0x10; // position in input buffer
            int outpos = 0;   // position in output buffer
            int length = input.Length; // length of the buffer

            if (input[6] == 1 && input[7] == 0) // if there is a decompression flag
            {
                Buffer.BlockCopy(input, 0x10, output, 0, input.Length - 0x10);
                return output;
            }
            else if (inpos < input.Length)
            {
                while (inpos < length)
                {
                    if (flags == 1)
                    {
                        flags = (uint)((input[inpos + 1] << 8) | input[inpos] | 0x10000);
                        inpos += 2;
                    }

                    for (int count = length - 0x20 < inpos ? 1 : 0x10; count > 0; --count)
                    {
                        if ((flags & 1) == 1)
                        {
                            var loop = input[inpos++];
                            var offs = input[inpos++] | ((loop & 0xF0) << 4);

                            var pointer = outpos - offs;

                            for (int a1 = 3 + (loop & 0xF); a1 > 0; --a1)
                                output[outpos++] = output[pointer++];
                        }
                        else
                        {
                            output[outpos++] = input[inpos++];
                        }
                        flags >>= 1;
                    }
                }
            }

            return output;
        }
    }
}
