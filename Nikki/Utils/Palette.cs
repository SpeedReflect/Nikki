using System.Collections.Generic;



namespace Nikki.Utils
{
    /// <summary>
    /// Collection of P8 compression and decompression functions.
    /// </summary>
    public static class Palette
    {
        /// <summary>
        /// Convert PAL8 .dds format into 32-bit RGBA
        /// </summary>
        /// <param name="data">binary data block to be converted</param>
        /// <param name="length">palette length in bytes</param>
        /// <returns>32-bit RGBA byte array converted based on PAL8 palette</returns>
        public static unsafe byte[] P8toRGBA(byte[] data, int length = 0x400)
        {
            uint[] palette = new uint[length / 4]; // palette size is 0x400 bytes = 0x100 ints
            byte[] result = new byte[(data.Length - length) * 4]; // 8 bpp * 4 bytes per color

            fixed (byte* byteptr_t = &data[0])
            {
                // Load palette into memory for access
                for (int a1 = 0; a1 < length / 4; ++a1)
                    palette[a1] = *(uint*)(byteptr_t + a1 * 4);
            }
            fixed (byte* byteptr_t = &result[0])
            {
                // Write result data based on palette
                for (int a1 = 0; a1 < data.Length - length; ++a1)
                {
                    var color = palette[data[a1 + length]];
                    *(uint*)(byteptr_t + a1 * 4) = color;
                }
            }
            return result;
        }

        /// <summary>
        /// Convert RGBA .dds format to 8-bit PAL8
        /// </summary>
        /// <param name="data">binary data of the .dds file including header</param>
        /// <returns>PAL8 formatted byte array with palette and compressed data</returns>
        public static unsafe byte[] RGBAtoP8(byte[] data)
        {
            int size = 0x400 + (data.Length - 0x80) / 4;
            var result = new byte[size];
            var map = new Dictionary<uint, byte>();
            int used = 0; // num of palette colors used; max = 0x100

            fixed (byte* byteptr_t = &data[0])
            {
                // Marshal through RGBA data starting at 0x80
                for (int offset = 0x80, mapoff = 0, dataoff = 0x400; offset < data.Length; offset += 4)
                {
                    uint color = *(uint*)(byteptr_t + offset);
                    if (map.TryGetValue(color, out byte index))
                        result[dataoff++] = index;
                    else
                    {
                        if (used == 0x100) return null; // return null if palette is over
                        map[color] = (byte)used; // add palette to the dictionary
                        result[mapoff++] = data[offset];
                        result[mapoff++] = data[offset + 1];
                        result[mapoff++] = data[offset + 2];
                        result[mapoff++] = data[offset + 3];
                        result[dataoff++] = (byte)used++;
                    }
                }
            }
            return result;
        }
    }
}