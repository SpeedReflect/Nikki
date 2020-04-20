using System;
using Nikki.Reflection.ID;


namespace Nikki.Utils.EA
{
    /// <summary>
    /// Collection of FEng and .fng relevant resolving functions.
    /// </summary>
    public static class SAT
    {
        /// <summary>
        /// Decompresses .fng JDLZ-compressed file.
        /// </summary>
        /// <param name="fng">.fng file as a byte array.</param>
        /// <returns>Decompressed FEng file as a byte array.</returns>
        public static unsafe byte[] Decompress(byte[] fng)
        {
            if (fng[0] == 3) // return if already decompressed
                return fng;

            byte[] InterData = new byte[fng.Length - 12];
            Buffer.BlockCopy(fng, 12, InterData, 0, fng.Length - 12);
            var NewData = JDLZ.Decompress(InterData);

            byte[] result = new byte[8 + NewData.Length];
            fixed (byte* byteptr_t = &result[0])
            {
                *(uint*)(byteptr_t + 0) = Global.FEngFiles;
                *(int*)(byteptr_t + 4) = NewData.Length;
            }

            Buffer.BlockCopy(NewData, 0, result, 8, NewData.Length);
            return result;
        }

        /// <summary>
        /// Converts ARGB representation to the hexadecimal one.
        /// </summary>
        /// <param name="alpha">Alpha value of the color.</param>
        /// <param name="red">Red value of the color.</param>
        /// <param name="green">Green value of the color.</param>
        /// <param name="blue">Blue value of the color.</param>
        /// <returns>String as a hexadecimal representation of the color.</returns>
        public static string ColorToHex(byte alpha, byte red, byte green, byte blue)
        {
            return $"0x{alpha:X2}{red:X2}{green:X2}{blue:X2}";
        }

        /// <summary>
        /// Gets alpha parameter of a hexadecimal color passed.
        /// </summary>
        /// <param name="color">Hexadecimal color passed.</param>
        /// <returns>Alpha parameter as a byte.</returns>
        public static byte GetAlpha(string color)
        {
            try
            {
                return Convert.ToByte(color.Substring(2, 2), 16);
            }
            catch (Exception) { return 0; }
        }

        /// <summary>
        /// Gets red parameter of a hexadecimal color passed.
        /// </summary>
        /// <param name="color">Hexadecimal color passed.</param>
        /// <returns>Red parameter as a byte.</returns>
        public static byte GetRed(string color)
        {
            try
            {
                return Convert.ToByte(color.Substring(4, 2), 16);
            }
            catch (Exception) { return 0; }
        }

        /// <summary>
        /// Gets green parameter of a hexadecimal color passed.
        /// </summary>
        /// <param name="color">Hexadecimal color passed.</param>
        /// <returns>Green parameter as a byte.</returns>
        public static byte GetGreen(string color)
        {
            try
            {
                return Convert.ToByte(color.Substring(6, 2), 16);
            }
            catch (Exception) { return 0; }
        }

        /// <summary>
        /// Gets blue parameter of a hexadecimal color passed.
        /// </summary>
        /// <param name="color">Hexadecimal color passed.</param>
        /// <returns>Blue parameter as a byte.</returns>
        public static byte GetBlue(string color)
        {
            try
            {
                return Convert.ToByte(color.Substring(8, 2), 16);
            }
            catch (Exception) { return 0; }
        }
    }
}