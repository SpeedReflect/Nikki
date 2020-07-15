using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils.DDS;
using Nikki.Reflection;
using Nikki.Reflection.Enum;



namespace Nikki.Utils.EA
{
    /// <summary>
    /// Collection of functions for EA compressed texture files.
    /// </summary>
    public static class Comp
    {
        /// <summary>
        /// Determines if an unsigned integer passed is an EA compression.
        /// </summary>
        /// <param name="value">Unsigned integer to be based on.</param>
        /// <returns>True if the value passed is an EA compression.</returns>
        public static bool IsDXTEncode(uint value) => value switch
        {
            EAComp.DXT1_32 => true,
            EAComp.DXT3_32 => true,
            EAComp.DXT5_32 => true,
            _ => false
        };

        /// <summary>
        /// Get EA compression uint from EA compression byte.
        /// </summary>
        /// <param name="value">Value from which get the result.</param>
        /// <returns>EA compression as a uint value.</returns>
        public static uint GetInt(TextureCompressionType value) => value switch
        {
            TextureCompressionType.TEXCOMP_DXTC1 => EAComp.DXT1_32,
            TextureCompressionType.TEXCOMP_DXTC3 => EAComp.DXT3_32,
            TextureCompressionType.TEXCOMP_DXTC5 => EAComp.DXT5_32,
            TextureCompressionType.TEXCOMP_8BIT => EAComp.P8_32,
            TextureCompressionType.TEXCOMP_8BIT_16 => EAComp.P8_32,
            TextureCompressionType.TEXCOMP_8BIT_64 => EAComp.P8_32,
            _ => EAComp.RGBA_32
        };

        /// <summary>
        /// Determines if the file is a .dds texture.
        /// </summary>
        /// <param name="filename">Filename to be evaluated.</param>
        /// <returns>True if the texture is a .dds texture.</returns>
        public static bool IsDDSTexture(string filename)
        {
            if (!File.Exists(filename))
			{

                throw new FileNotFoundException($"File with path {filename} does not exist");

			}

			using var OpenReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read));
			if (OpenReader.BaseStream.Length < 0x80) return false;
			if (OpenReader.ReadUInt32() != DDS_MAIN.MAGIC) return false;
			OpenReader.BaseStream.Position = 0x50;
			uint flag = OpenReader.ReadUInt32();
			uint code = OpenReader.ReadUInt32();

			switch (flag)
			{
				case (uint)DDS_TYPE.FOURCC:
					return IsDXTEncode(code);

				case (uint)DDS_TYPE.PAL8:
				case (uint)DDS_TYPE.PAL8A:
				case (uint)DDS_TYPE.RGBA:
					return true;

				default:
					return false;

			}
		}

        /// <summary>
        /// Determines if the file is a .dds texture.
        /// </summary>
        /// <param name="filename">Filename to be evaluated.</param>
        /// <param name="error">Specifies failure reason in case file is not a .dds texture.</param>
        /// <returns>True if the texture is a .dds texture.</returns>
        public static bool IsDDSTexture(string filename, out string error)
        {
            if (!File.Exists(filename))
            {

                throw new FileNotFoundException($"File with path {filename} does not exist");

            }

            using var OpenReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read));
			
            if (OpenReader.BaseStream.Length < 0x80)
			{

				error = "Texture has invalid header type.";
				return false;

			}

			if (OpenReader.ReadUInt32() != DDS_MAIN.MAGIC)
			{

				error = "Texture has invalid header type.";
				return false;

			}

			OpenReader.BaseStream.Position = 0x50;
			uint flag = OpenReader.ReadUInt32();
			uint code = OpenReader.ReadUInt32();
            error = null;

            switch (flag)
            {
                case (uint)DDS_TYPE.FOURCC:
                    if (IsDXTEncode(code)) return true;
                    else goto default;

                case (uint)DDS_TYPE.PAL8:
                case (uint)DDS_TYPE.PAL8A:
                case (uint)DDS_TYPE.RGBA:
                    return true;

                default:
                    error = "Texture has not supported compression type";
                    return false;

            }
        }

        /// <summary>
        /// Returns byte array of padding bytes required to start at offset % start_at = 0
        /// </summary>
        /// <param name="length">Length of the current stream to be added to.</param>
        /// <param name="start_at">Offset at which padding ends.</param>
        /// <returns>Byte array of padding bytes.</returns>
        public static byte[] GetPaddingArray(int length, byte start_at)
        {
            byte[] result;
            int difference = start_at - (length % start_at);
            if (difference == start_at) difference = -1;
            
            switch (difference)
            {
                case -1:
                    result = new byte[0];
                    return result;

                case 4:
                    result = new byte[4 + start_at];
                    result[4] = (byte)(start_at - 4);
                    return result;
                
                case 8:
                    result = new byte[8];
                    return result;
                
                default:
                    result = new byte[difference];
                    result[4] = (byte)(difference - 8);
                    return result;
            }
        }
    }
}