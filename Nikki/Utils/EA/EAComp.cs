using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils.DDS;
using Nikki.Reflection.ID;



namespace Nikki.Utils.EA
{
    /// <summary>
    /// Collection of functions for EA compressed texture files.
    /// </summary>
    public static class Comp
    {
        private const string DXT1 = "DXT1";
        private const string DXT3 = "DXT3";
        private const string DXT5 = "DXT5";
        private const string RGBA = "RGBA";
        private const string P8   = "P8";

        /// <summary>
        /// Determines if an unsigned integer passed is an EA compression.
        /// </summary>
        /// <param name="value">Unsigned integer to be based on.</param>
        /// <returns>True if the value passed is an EA compression.</returns>
        public static bool IsComp(uint value) => value switch
        {
            EAComp.P8_32 => true,
            EAComp.RGBA_32 => true,
            EAComp.DXT1_32 => true,
            EAComp.DXT3_32 => true,
            EAComp.DXT5_32 => true,
            _ => false
        };

        /// <summary>
        /// Get EA compression byte from EA compression uint.
        /// </summary>
        /// <param name="value">Value from which get the result.</param>
        /// <returns>EA compression as a byte value.</returns>
        public static byte GetByte(uint value) => value switch
        {
            EAComp.DXT1_32 => EAComp.DXT1_08,
            EAComp.DXT3_32 => EAComp.DXT3_08,
            EAComp.DXT5_32 => EAComp.DXT5_08,
            EAComp.P8_32 => EAComp.P8_08,
            _ => EAComp.RGBA_08
        };

        /// <summary>
        /// Get EA compression byte from EA compression string.
        /// </summary>
        /// <param name="value">Value from which get the result.</param>
        /// <returns>EA compression as a byte value.</returns>
        public static byte GetByte(string value) => value switch
        {
            DXT1 => EAComp.DXT1_08,
            DXT3 => EAComp.DXT3_08,
            DXT5 => EAComp.DXT5_08,
            P8 => EAComp.P8_08,
            _ => EAComp.RGBA_08
        };

        /// <summary>
        /// Get EA compression string from EA compression byte.
        /// </summary>
        /// <param name="value">Value from which get the result.</param>
        /// <returns>EA compression as a string value.</returns>
        public static string GetString(byte value) => value switch
        {
            EAComp.DXT1_08 => DXT1,
            EAComp.DXT3_08 => DXT3,
            EAComp.DXT5_08 => DXT5,
            EAComp.P8_08 => P8,
            _ => RGBA
        };

        /// <summary>
        /// Get EA compression string from EA compression uint.
        /// </summary>
        /// <param name="value">Value from which get the result.</param>
        /// <returns>EA compression as a string value.</returns>
        public static string GetString(uint value) => value switch
        {
            EAComp.DXT1_32 => DXT1,
            EAComp.DXT3_32 => DXT3,
            EAComp.DXT5_32 => DXT5,
            EAComp.P8_32 => P8,
            _ => RGBA
        };

        /// <summary>
        /// Get EA compression uint from EA compression string.
        /// </summary>
        /// <param name="value">Value from which get the result.</param>
        /// <returns>EA compression as a uint value.</returns>
        public static uint GetInt(string value) => value switch
        {
            DXT1 => EAComp.DXT1_32,
            DXT3 => EAComp.DXT3_32,
            DXT5 => EAComp.DXT5_32,
            P8 => EAComp.P8_32,
            _ => EAComp.RGBA_32
        };

        /// <summary>
        /// Get EA compression uint from EA compression byte.
        /// </summary>
        /// <param name="value">Value from which get the result.</param>
        /// <returns>EA compression as a uint value.</returns>
        public static uint GetInt(byte value) => value switch
        {
            EAComp.DXT1_08 => EAComp.DXT1_32,
            EAComp.DXT3_08 => EAComp.DXT3_32,
            EAComp.DXT5_08 => EAComp.DXT5_32,
            EAComp.P8_08 => EAComp.P8_32,
            _ => EAComp.RGBA_32
        };

        /// <summary>
        /// Calculates base of 2 image area based on the actual size passed.
        /// </summary>
        /// <param name="size">Size in pixels.</param>
        /// <returns>Base of 2 image size in pixels.</returns>
        public static int FlipToBase(int size)
        {
            uint x = (uint)size;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            x -= x >> 1;
            return (int)x;
        }

        /// <summary>
        /// Calculates .dds pitch or linear size.
        /// </summary>
        /// <param name="compression">.dds compression format as an EA compression byte.</param>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="bpp">Bytes per pixel in the image.</param>
        /// <returns>Pitch or linear size based on compression passed.</returns>
        public static uint PitchLinearSize(byte compression, short width, short height, uint bpp)
        {
            int result;
            switch (compression)
            {
                case EAComp.DXT1_08:
                    result = (1 > (width + 3) / 4) ? 1 : (width + 3) / 4;
                    result *= (1 > (height + 3) / 4) ? 1 : (height + 3) / 4;
                    result *= 8;
                    break;

                case EAComp.P8_08:
                case EAComp.RGBA_08:
                    result = (int)width * (int)bpp + 7;
                    result /= 8;
                    break;

                default:
                    result = (1 > (width + 3) / 4) ? 1 : (width + 3) / 4;
                    result *= (1 > (height + 3) / 4) ? 1 : (height + 3) / 4;
                    result *= 16;
                    break;
            }
            return (uint)result;
        }

        /// <summary>
        /// Edits <see cref="DDS_PIXELFORMAT"/> based on compression passed.
        /// </summary>
        /// <param name="PIXELFORMAT"><see cref="DDS_PIXELFORMAT"/> of the .dds header passed as a reference type.</param>
        /// <param name="compression">EA compression byte of the image.</param>
        public static void GetPixelFormat(DDS_PIXELFORMAT PIXELFORMAT, byte compression)
        {
            switch (compression)
            {
                case EAComp.DXT1_08:
                    DDS_CONST.DDSPF_DXT1(PIXELFORMAT);
                    break;

                case EAComp.DXT3_08:
                    DDS_CONST.DDSPF_DXT3(PIXELFORMAT);
                    break;

                case EAComp.DXT5_08:
                    DDS_CONST.DDSPF_DXT5(PIXELFORMAT);
                    break;

                default: // set to be RGB in case of mismatch
                    DDS_CONST.DDSPF_A8R8G8B8(PIXELFORMAT);
                    break;
            }
        }

        /// <summary>
        /// Determines if the file is a .dds texture.
        /// </summary>
        /// <param name="filename">Filename to be evaluated.</param>
        /// <returns>True if the texture is a .dds texture.</returns>
        public static bool IsDDSTexture(string filename)
        {
            using (var OpenReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read)))
            {
                string name = Path.GetFileNameWithoutExtension(filename);
                if (OpenReader.BaseStream.Length < 0x80) return false;
                if (OpenReader.ReadUInt32() != DDS_MAIN.MAGIC) return false;
                OpenReader.BaseStream.Position = 0x50;
                uint num1 = OpenReader.ReadUInt32();
                uint num2 = OpenReader.ReadUInt32();
                if (!IsComp(num2) && (num1 != (uint)DDS_TYPE.RGBA)) return false;
            }
            return true;
        }

        /// <summary>
        /// Determines if the file is a .dds texture.
        /// </summary>
        /// <param name="filename">Filename to be evaluated.</param>
        /// <param name="error">Specifies failure reason in case file is not a .dds texture.</param>
        /// <returns>True if the texture is a .dds texture.</returns>
        public static bool IsDDSTexture(string filename, ref string error)
        {
            using (var OpenReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read)))
            {
                string name = Path.GetFileNameWithoutExtension(filename);
                if (OpenReader.BaseStream.Length < 0x80)
                {
                    error = "Texture " + name + " has invalid header type.";
                    return false;
                }
                if (OpenReader.ReadUInt32() != DDS_MAIN.MAGIC)
                {
                    error = "Texture " + name + " has invalid header type.";
                    return false;
                }
                OpenReader.BaseStream.Position = 0x50;
                uint num1 = OpenReader.ReadUInt32();
                uint num2 = OpenReader.ReadUInt32();
                if (!IsComp(num2) && (num1 != (uint)DDS_TYPE.RGBA))
                {
                    error = name + ": invalid DDS compression type";
                    return false;
                }
            }
            return true;
        }
    
        /// <summary>
        /// Gets the default .tpk name by index passed.
        /// </summary>
        /// <param name="index">Index of the .tpk in the array.</param>
        /// <param name="game"><see cref="GameINT"/> of the game.</param>
        /// <returns>Collection Name of the .tpk</returns>
        public static string GetTPKName(int index, GameINT game)
        {
            return game switch
            {
                GameINT.Carbon => index switch
                {
                    0 => "GLOBALMESSAGETEXTURES",
                    1 => "GLOBALTEXTURES",
                    2 => "FLARETEXTURES",
                    3 => "GLOBALTEXTURESPC",
                    4 => "EMITTER_SYSTEM_TEXTURE_PAGE",
                    5 => "EMITTER_SYSTEM_NORMALMAPS_P",
                    6 => "FLARE_TEXTURE_PAGE",
                    _ => String.Empty
                },
                GameINT.MostWanted => index switch
                {
                    0 => "GLOBALMESSAGE",
                    1 => "GLOBAL",
                    2 => "GLOBAL",
                    3 => "GLOBAL",
                    _ => String.Empty
                },
                GameINT.Underground2 => index switch
                {
                    0 => "GLOBALMESSAGE",
                    1 => "GLOBAL",
                    2 => "GLOBAL",
                    _ => String.Empty
                },
                _ => null,
            };
        }
    }
}