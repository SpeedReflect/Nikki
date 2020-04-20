namespace Nikki.Reflection.ID
{
    public static class EAComp
    {
        /// <summary>
        /// PAL8 Compression = 0x8
        /// </summary>
        public const byte P8_08   = (byte)Enum.eTextureCompressionType.TEXCOMP_8BIT;

        /// <summary>
        /// RGBA Compression = 0x20
        /// </summary>
        public const byte RGBA_08 = (byte)Enum.eTextureCompressionType.TEXCOMP_32BIT;

        /// <summary>
        /// DXT1 Compression = 0x22
        /// </summary>
        public const byte DXT1_08 = (byte)Enum.eTextureCompressionType.TEXCOMP_DXTC1;

        /// <summary>
        /// DXT3 Compression = 0x24
        /// </summary>
        public const byte DXT3_08 = (byte)Enum.eTextureCompressionType.TEXCOMP_DXTC3;

        /// <summary>
        /// DXT5 Compression = 0x26
        /// </summary>
        public const byte DXT5_08 = (byte)Enum.eTextureCompressionType.TEXCOMP_DXTC5;

        /// <summary>
        /// PAL8-64 Compression = 0x81
        /// </summary>
        public const byte SECRET  = (byte)Enum.eTextureCompressionType.TEXCOMP_8BIT_64;


        public const uint P8_32   = 0x00000029;
        public const uint RGBA_32 = 0x00000015;
        public const uint DXT1_32 = 0x31545844;
        public const uint DXT3_32 = 0x33545844;
        public const uint DXT5_32 = 0x35545844;
    }
}
