using System;



namespace Nikki.Utils
{
    /// <summary>
    /// Collection with JDLZ compressor and decompressor.
    /// </summary>
    public static class JDLZ
    {
        /// <summary>
        /// Decompresses JDLZ byte array.
        /// </summary>
        /// <param name="block">Byte array to be decompressed.</param>
        /// <returns>Byte array of decompressed data.</returns>
        public static unsafe byte[] Decompress(byte[] block)
        {
            if (block == null || block.Length < 0x10)
                return block;

            int param1 = 1;
            int param2 = 1;
            int offset;
            int length;
            int ReaderPosition = 0x10;
            int WriterPosition = 0;
            byte[] result;

            fixed (byte* byteptr_t = &block[0])
            {
                if (*(uint*)byteptr_t != 0x5A4C444A || *(byteptr_t + 4) != 2)
                    return block;

                result = new byte[*(int*)(byteptr_t + 8)];
            }

            while ((ReaderPosition < block.Length) && (WriterPosition < result.Length))
            {
                if (param1 == 1)
                {
                    param1 = (int)block[ReaderPosition] + 0x100; // range 0x100 - 0x1FF
                    ++ReaderPosition; // advance position
                }

                if (param2 == 1)
                {
                    param2 = (int)block[ReaderPosition] + 0x100; // range 0x100 - 0x1FF
                    ++ReaderPosition; // advance position
                }

                if ((param1 & 1) == 1)
                {
                    if ((param2 & 1) == 1)
                    {
                        length = (block[ReaderPosition + 1] | ((block[ReaderPosition] & 0xF0) << 4)) + 3;
                        offset = (block[ReaderPosition] & 0x0F) + 1;
                    }
                    else
                    {
                        offset = (block[ReaderPosition + 1] | ((block[ReaderPosition] & 0xE0) << 3)) + 17;
                        length = (block[ReaderPosition] & 0x1F) + 3;
                    }

                    ReaderPosition += 2; // advance position

                    for (int i = 0; i < length; ++i)
                    {
                        result[WriterPosition + i] = result[WriterPosition + i - offset];
                    }

                    WriterPosition += length;
                    param2 /= 2;
                }
                else
                {
                    if (WriterPosition < result.Length)
                    {
                        result[WriterPosition] = block[ReaderPosition];
                        ++ReaderPosition;
                        ++WriterPosition;
                    }
                }
                param1 /= 2;
            }
            return result;
        }

        /// <summary>
        /// Compresses byte block into JDLZ-compressed one.
        /// </summary>
        /// <param name="input">Byte buffer to compress.</param>
        /// <param name="hashSize">Speed/Ratio tunable; use powers of 2. Results vary per file.</param>
        /// <param name="maxSearchDepth">Speed/Ratio tunable. Results vary per file.</param>
        /// <returns>JDLZ-compressed byte array.</returns>
        public static unsafe byte[] Compress(byte[] input, int hashSize = 0x2000, int maxSearchDepth = 16)
        {
            if (input == null) return null;

            int inputBytes = input.Length;
            byte[] output = new byte[inputBytes + ((inputBytes + 7) / 8) + 0x10 + 1];
            int[] hashPos = new int[hashSize];
            int[] hashChain = new int[inputBytes];

            int outPos = 0x10;
            int inPos = 0;
            byte flags1bit = 1;
            byte flags2bit = 1;
            byte flags1 = 0;
            byte flags2 = 0;

            fixed (byte* byteptr_t = &output[0])
            {
                *(int*)byteptr_t = 0x5A4C444A;
                *(int*)(byteptr_t + 4) = 0x00001002;
                *(int*)(byteptr_t + 8) = inputBytes;
            }

            int flags1Pos = outPos++;
            int flags2Pos = outPos++;

            flags1bit <<= 1;
            output[outPos++] = input[inPos++];
            inputBytes--;

            while (inputBytes > 0)
            {
                int bestMatchLength = 2;
                int bestMatchDist = 0;

                if (inputBytes >= 3)
                {
                    int hash = (-0x1A1 * (input[inPos] ^ ((input[inPos + 1] ^ (input[inPos + 2] << 4)) << 4))) & (hashSize - 1);
                    int matchPos = hashPos[hash];
                    hashPos[hash] = inPos;
                    hashChain[inPos] = matchPos;
                    int prevMatchPos = inPos;

                    for (int i = 0; i < maxSearchDepth; i++)
                    {
                        int matchDist = inPos - matchPos;
                        if (matchDist > 2064 || matchPos >= prevMatchPos) break;
                        int matchLengthLimit = matchDist <= 16 ? 4098 : 34;
                        int maxMatchLength = inputBytes;
                        if (maxMatchLength > matchLengthLimit) maxMatchLength = matchLengthLimit;
                        if (bestMatchLength >= maxMatchLength) break;

                        int matchLength = 0;
                        while ((matchLength < maxMatchLength) && (input[inPos + matchLength] == input[matchPos + matchLength]))
                            matchLength++;

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
                    inPos += bestMatchLength;
                    inputBytes -= bestMatchLength;
                    bestMatchLength -= 3;

                    if (bestMatchDist < 17)
                    {
                        flags2 |= flags2bit;
                        output[outPos++] = (byte)((bestMatchDist - 1) | ((bestMatchLength >> 4) & 0xF0));
                        output[outPos++] = (byte)bestMatchLength;
                    }
                    else
                    {
                        bestMatchDist -= 17;
                        output[outPos++] = (byte)(bestMatchLength | ((bestMatchDist >> 3) & 0xE0));
                        output[outPos++] = (byte)bestMatchDist;
                    }
                    flags2bit <<= 1;
                }
                else
                {
                    output[outPos++] = input[inPos++];
                    inputBytes--;
                }

                flags1bit <<= 1;

                if (flags1bit == 0)
                {
                    output[flags1Pos] = flags1;
                    flags1 = 0;
                    flags1Pos = outPos++;
                    flags1bit = 1;
                }
                if (flags2bit == 0)
                {
                    output[flags2Pos] = flags2;
                    flags2 = 0;
                    flags2Pos = outPos++;
                    flags2bit = 1;
                }
            }

            if (flags2bit > 1)
            {
                output[flags2Pos] = flags2;
            }
            else if (flags2Pos == outPos - 1)
            {
                outPos = flags2Pos;
            }
            if (flags1bit > 1)
            {
                output[flags1Pos] = flags1;
            }
            else if (flags1Pos == outPos - 1)
            {
                outPos = flags1Pos;
            }

            fixed (byte* byteptr_t = &output[0xC])
            {
                *(int*)byteptr_t = outPos;
            }

            Array.Resize(ref output, outPos);
            return output;
        }
    }
}