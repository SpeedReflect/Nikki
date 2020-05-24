using System;
using System.Collections.Generic;



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
            var insize = BitConverter.ToInt32(input, 12);
            var output = new byte[outsize];

            while ((inpos < insize) && (outpos < outsize))
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
                    if (outpos < outsize) output[outpos++] = input[inpos++];
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

            var insize = input.Length;
            var output = new byte[insize + (insize + 7) / 8 + 16];

            unsafe
            {
                fixed (byte* ptr = &input[0])
                {
                    var outsize = BlockCompress(ptr, insize, output);
                    Array.Resize(ref output, outsize);
                }
            }

            return output;
        }

        /// <summary>
        /// Compresses byte block into JDLZ-compressed one.
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

            var output = new byte[count + (count + 7) / 8 + 16];

            unsafe
            {
                fixed (byte* ptr = &input[start])
                {
                    var outsize = BlockCompress(ptr, count, output);
                    Array.Resize(ref output, outsize);
                }
            }

            return output;
        }

        /// <summary>
        /// Special thanks to heyitsleo for making this JDLZ compressor.
        /// It is based on the real compressor in the NFS executables.
        /// Compressed data output size if almost identical to original
        /// compressed files with about 1-5% difference.
        /// </summary>
        private static unsafe int BlockCompress(byte* src, int srcSize, Span<byte> dst)
        {
            var newSize = 0;
            var afterHeaderWriteIndex = 16;
            var dataIndex = 18;
            var offset = 0;

            var hashPool = new JLZHashPool();

            uint flags1 = 0xFF00;
            uint flags2 = 0xFF00;
            var indexFlags2 = 17;
            var dwSize = srcSize;

            if (dwSize >= 0)
                while (true)
                {
                    var bytesRemaining = Math.Min(dwSize, 4098);
                    var hashPoolHeadIndex = hashPool.GetHashOffsetBase(src + offset) & 0x1FFF;
                    var byteRepeatCount = 2;
                    var currentHash = hashPool.HashHead[hashPoolHeadIndex];
                    var savedCurrentHash = currentHash;
                    var doShortUpdate = currentHash == null;

                    if (currentHash != null)
                    {
                        do
                        {
                            if (byteRepeatCount >= 4098) break;

                            var compareIndex = 0;

                            if (bytesRemaining > 3)
                            {
                                var hashCheckBlock = src + offset;

                                while (compareIndex < bytesRemaining)
                                {
                                    var value1 = *(int*)hashCheckBlock;
                                    var value2 = *(int*)(src + currentHash.Offset + compareIndex);

                                    if (value1 != value2) break;

                                    hashCheckBlock += 4;
                                    compareIndex += 4;
                                }
                            }

                            while (compareIndex < bytesRemaining)
                            {
                                if (src[currentHash.Offset + compareIndex] != src[offset + compareIndex]) break;

                                compareIndex++;
                            }

                            if (compareIndex > byteRepeatCount &&
                                (compareIndex <= 34 || offset - currentHash.Offset < 16 ||
                                 byteRepeatCount <= 34))
                            {
                                byteRepeatCount = compareIndex;
                                savedCurrentHash = currentHash;
                            }

                            currentHash = currentHash.Next;
                        } while (currentHash != null);

                        if (byteRepeatCount > 2)
                        {
                            flags1 >>= 1;
                            var backtrack = offset - savedCurrentHash.Offset - 1;
                            byte backtrackCode;
                            if (backtrack < 16)
                            {
                                flags2 >>= 1;
                                backtrackCode = (byte)(backtrack | (((byteRepeatCount - 3) >> 4) & 0xF0));
                                dst[dataIndex + 1] = (byte)(byteRepeatCount - 3);
                            }
                            else
                            {
                                byteRepeatCount = Math.Min(byteRepeatCount, 34);
                                flags2 = (flags2 >> 1) & 0x7F7F;
                                backtrackCode = (byte)((byteRepeatCount - 3) |
                                                        (((offset - savedCurrentHash.Offset - 17) >> 3) & 0xE0));
                                dst[dataIndex + 1] = (byte)(offset - (savedCurrentHash.Offset & 0xFF) - 17);
                            }

                            dst[dataIndex] = backtrackCode;
                            dataIndex += 2;
                            dwSize -= byteRepeatCount;

                            do
                            {
                                hashPool.Update(src + offset, offset);
                                --byteRepeatCount;
                                offset++;
                            } while (byteRepeatCount != 0);

                            newSize = dwSize;
                        }
                        else
                        {
                            doShortUpdate = true;
                        }
                    }

                    if (doShortUpdate)
                    {
                        hashPool.Update(src + offset, offset);
                        dst[dataIndex++] = src[offset];
                        offset++;
                        newSize = dwSize - 1;
                        flags1 = (flags1 >> 1) & 0x7F7F;
                        --dwSize;
                    }

                    var sf1 = unchecked((ushort)flags1);
                    var sf2 = unchecked((ushort)flags2);

                    if (sf1 < 0x100)
                    {
                        dst[afterHeaderWriteIndex] = (byte)sf1;
                        afterHeaderWriteIndex = dataIndex++;
                        flags1 = 0xFF00;
                    }

                    if (sf2 < 0x100)
                    {
                        dst[indexFlags2] = (byte)sf2;
                        indexFlags2 = dataIndex++;
                        flags2 = 0xFF00;
                    }

                    if (newSize < 0)
                        break;
                }

            for (; (flags1 & 0xFF00) != 0; flags1 >>= 1)
            {
            }

            for (dst[afterHeaderWriteIndex] = (byte)flags1; (flags2 & 0xFF00) != 0; flags2 >>= 1)
            {
            }

            dst[indexFlags2] = (byte)flags2;

            dst[0] = 0x4a;
            dst[1] = 0x44;
            dst[2] = 0x4c;
            dst[3] = 0x5a;
            dst[4] = 0x2;
            dst[5] = 0x10;
            dst[8] = (byte)(srcSize & 0xff);
            dst[9] = (byte)((srcSize >> 8) & 0xff);
            dst[10] = (byte)((srcSize >> 16) & 0xff);
            dst[11] = (byte)((srcSize >> 24) & 0xff);
            dst[12] = (byte)(dataIndex & 0xff);
            dst[13] = (byte)((dataIndex >> 8) & 0xff);
            dst[14] = (byte)((dataIndex >> 16) & 0xff);
            dst[15] = (byte)((dataIndex >> 24) & 0xff);

            return dataIndex;
        }

        private class JLZHash
        {
            public bool IsComplete { get; set; }
            public int ComputedHash { get; set; }
            public int Offset { get; set; }

            public JLZHash Next { get; set; }
            public JLZHash Previous { get; set; }
        }

        private class JLZHashPool
        {
            private readonly int _poolSize;

            public JLZHashPool(int poolSize = 0x810)
            {
                this._poolSize = poolSize;
                for (var i = 0; i < poolSize; i++)
                {
                    var jlzHash = new JLZHash();
                    this.HashPool.Add(jlzHash);
                }

                for (var i = 0; i < 8192; i++) this.HashHead.Add(null);
            }

            public List<JLZHash> HashPool { get; } = new List<JLZHash>();
            public List<JLZHash> HashHead { get; } = new List<JLZHash>();

            public unsafe short GetHashOffsetBase(byte* data)
            {
                return (short)(-417 * (data[0] ^ (16 * (data[1] ^ (16 * data[2])))));
            }

            public unsafe JLZHash Update(byte* data, int offset)
            {
                if (offset < 0)
                    throw new ArgumentException("value < 0", nameof(offset));

                var hashOffset = offset % this._poolSize;
                var hash = this.HashPool[hashOffset];

                if (hash.IsComplete)
                {
                    // if this hash has already been defined, move everything around
                    var nextHash = hash.Next;

                    if (nextHash != null) nextHash.Previous = hash.Previous;

                    var previousHash = hash.Previous;

                    if (previousHash != null) previousHash.Next = hash.Next;

                    var hashIndex = hash.ComputedHash;
                    if (ReferenceEquals(this.HashHead[hashIndex], hash)) this.HashHead[hashIndex] = hash.Next;
                }

                var alignedNewOffset = this.GetHashOffsetBase(data) & 0x1FFF;

                hash.Offset = offset;
                hash.Previous = null;
                hash.Next = this.HashHead[alignedNewOffset];
                hash.ComputedHash = alignedNewOffset;
                hash.IsComplete = true;

                this.HashHead[alignedNewOffset] = hash;

                if (hash.Next != null) hash.Next.Previous = hash;

                return hash.Next;
            }
        }
    }
}