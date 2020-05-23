using System;



namespace Nikki.Utils.LZC
{
    /// <summary>
    /// Collection with JDLZ compressor and decompressor.
    /// </summary>
    public static class JDLZ
    {
        /// <summary>
        /// Decompresses JDLZ byte array.
        /// </summary>
        /// <param name="input">Byte array to be decompressed.</param>
        /// <returns>Byte array of decompressed data.</returns>
        public static byte[] Decompress(byte[] input)
        {
            if (input == null || input.Length < 0x10) return null;

            int flags1 = 1;
            int flags2 = 1;
            int t = 0;
            int length = 0;
            int inpos = 0x10;
            int outpos = 0;

            var outsize = BitConverter.ToInt32(input, 8);
            var output = new byte[outsize];

            while ((inpos < input.Length) && (outpos < output.Length))
            {
                if (flags1 == 1) flags1 = input[inpos++] | 0x100;
                if (flags2 == 1) flags2 = input[inpos++] | 0x100;

                if ((flags1 & 1) == 1)
                {
                    if ((flags2 & 1) == 1)
                    {
                        length = (input[inpos + 1] | ((input[inpos] & 0xF0) << 4)) + 3;
                        t = (input[inpos] & 0x0F) + 1;
                    }
                    else 
                    {
                        t = (input[inpos + 1] | ((input[inpos] & 0xE0) << 3)) + 17;
                        length = (input[inpos] & 0x1F) + 3;
                    }

                    inpos += 2;

                    for (int i = 0; i < length; ++i)
                        output[outpos + i] = output[outpos + i - t];

                    outpos += length;
                    flags2 >>= 1;
                }
                else
                {
                    if (outpos < output.Length) output[outpos++] = input[inpos++];
                }
                flags1 >>= 1;
            }

            return output;
        }

        /// <summary>
        /// Compresses byte block into JDLZ-compressed one.
        /// </summary>
        /// <param name="input">Byte buffer to compress.</param>
        /// <returns>JDLZ-compressed byte array.</returns>
        public static byte[] Compress(byte[] input)
        {
            if (input == null) return null;

            int inputBytes = input.Length;
            byte[] output = new byte[inputBytes + ((inputBytes + 7) / 8) + 0x10 + 1];
            int[] hashPos = new int[0x2000];
            int[] hashChain = new int[inputBytes];

            int outpos = 0;
            int inpos = 0;
            byte flags1bit = 1;
            byte flags2bit = 1;
            byte flags1 = 0;
            byte flags2 = 0;

            output[outpos++] = 0x4A; // 'J'
            output[outpos++] = 0x44; // 'D'
            output[outpos++] = 0x4C; // 'L'
            output[outpos++] = 0x5A; // 'Z'
            output[outpos++] = 0x02;
            output[outpos++] = 0x10;
            output[outpos++] = 0x00;
            output[outpos++] = 0x00;
            output[outpos++] = (byte)inputBytes;
            output[outpos++] = (byte)(inputBytes >> 8);
            output[outpos++] = (byte)(inputBytes >> 16);
            output[outpos++] = (byte)(inputBytes >> 24);
            outpos += 4;

            int flags1Pos = outpos++;
            int flags2Pos = outpos++;

            flags1bit <<= 1;
            output[outpos++] = input[inpos++];
            inputBytes--;

            while (inputBytes > 0)
            {
                int bestMatchLength = 2;
                int bestMatchDist = 0;

                if (inputBytes >= 3)
                {
                    int hash = (-0x1A1 * (input[inpos] ^ ((input[inpos + 1] ^ (input[inpos + 2] << 4)) << 4))) & 0x1FFF;
                    int matchPos = hashPos[hash];
                    hashPos[hash] = inpos;
                    hashChain[inpos] = matchPos;
                    int prevMatchPos = inpos;

                    for (int i = 0; i < 0x10; i++)
                    {
                        int matchDist = inpos - matchPos;

                        if (matchDist > 2064 || matchPos >= prevMatchPos)
                        {
                            break;
                        }

                        int matchLengthLimit = matchDist <= 16 ? 4098 : 34;
                        int maxMatchLength = inputBytes;

                        if (maxMatchLength > matchLengthLimit)
                        {
                            maxMatchLength = matchLengthLimit;
                        }
                        if (bestMatchLength >= maxMatchLength)
                        {
                            break;
                        }

                        int matchLength = 0;
                        while ((matchLength < maxMatchLength) && (input[inpos + matchLength] == input[matchPos + matchLength]))
                        {
                            matchLength++;
                        }

                        if (matchLength > bestMatchLength)
                        {
                            bestMatchLength = matchLength;
                            bestMatchDist = matchDist;
                        }

                        prevMatchPos = matchPos;
                        matchPos = hashChain[matchPos];
                    }
                }

                if (bestMatchLength >= 3)
                {
                    flags1 |= flags1bit;
                    inpos += bestMatchLength;
                    inputBytes -= bestMatchLength;
                    bestMatchLength -= 3;

                    if (bestMatchDist < 17)
                    {
                        flags2 |= flags2bit;
                        output[outpos++] = (byte)((bestMatchDist - 1) | ((bestMatchLength >> 4) & 0xF0));
                        output[outpos++] = (byte)bestMatchLength;
                    }
                    else
                    {
                        bestMatchDist -= 17;
                        output[outpos++] = (byte)(bestMatchLength | ((bestMatchDist >> 3) & 0xE0));
                        output[outpos++] = (byte)bestMatchDist;
                    }

                    flags2bit <<= 1;
                }
                else
                {
                    output[outpos++] = input[inpos++];
                    inputBytes--;
                }

                flags1bit <<= 1;

                if (flags1bit == 0)
                {
                    output[flags1Pos] = flags1;
                    flags1 = 0;
                    flags1Pos = outpos++;
                    flags1bit = 1;
                }

                if (flags2bit == 0)
                {
                    output[flags2Pos] = flags2;
                    flags2 = 0;
                    flags2Pos = outpos++;
                    flags2bit = 1;
                }
            }

            if (flags2bit > 1)
            {
                output[flags2Pos] = flags2;
            }
            else if (flags2Pos == outpos - 1)
            {
                outpos = flags2Pos;
            }

            if (flags1bit > 1)
            {
                output[flags1Pos] = flags1;
            }
            else if (flags1Pos == outpos - 1)
            {
                outpos = flags1Pos;
            }

            output[0x0C] = (byte)outpos;
            output[0x0D] = (byte)(outpos >> 8);
            output[0x0E] = (byte)(outpos >> 16);
            output[0x0F] = (byte)(outpos >> 24);

            Array.Resize(ref output, outpos);
            return output;
        }
    }
}