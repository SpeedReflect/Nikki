using Nikki.Support.Shared.Class;



namespace Nikki.Reflection
{
    /// <summary>
    /// Class of IDs related to <see cref="Texture"/> compressions.
    /// </summary>
    public static class EAComp
    {
        /// <summary>
        /// PAL4 Compression = 0x4
        /// </summary>
        public const byte P4_08 = (byte)Enum.TextureCompressionType.TEXCOMP_4BIT;

        /// <summary>
        /// PAL8 Compression = 0x8
        /// </summary>
        public const byte P8_08 = (byte)Enum.TextureCompressionType.TEXCOMP_8BIT;

        /// <summary>
        /// RGBA Compression = 0x20
        /// </summary>
        public const byte RGBA_08 = (byte)Enum.TextureCompressionType.TEXCOMP_32BIT;

        /// <summary>
        /// DXT1 Compression = 0x22
        /// </summary>
        public const byte DXT1_08 = (byte)Enum.TextureCompressionType.TEXCOMP_DXTC1;

        /// <summary>
        /// DXT3 Compression = 0x24
        /// </summary>
        public const byte DXT3_08 = (byte)Enum.TextureCompressionType.TEXCOMP_DXTC3;

        /// <summary>
        /// DXT5 Compression = 0x26
        /// </summary>
        public const byte DXT5_08 = (byte)Enum.TextureCompressionType.TEXCOMP_DXTC5;

        /// <summary>
        /// PAL8-64 Compression = 0x81
        /// </summary>
        public const byte SECRET = (byte)Enum.TextureCompressionType.TEXCOMP_8BIT_64;

        /// <summary>
        /// PAL8 Compression identifier in Part 5 of <see cref="TPKBlock"/>.
        /// </summary>
        public const uint P8_32   = 0x00000029;

        /// <summary>
        /// RGBA Compression identifier in Part 5 of <see cref="TPKBlock"/>.
        /// </summary>
        public const uint RGBA_32 = 0x00000015;

        /// <summary>
        /// DXT1 Compression identifier in Part 5 of <see cref="TPKBlock"/>.
        /// </summary>
        public const uint DXT1_32 = 0x31545844;

        /// <summary>
        /// DXT3 Compression identifier in Part 5 of <see cref="TPKBlock"/>.
        /// </summary>
        public const uint DXT3_32 = 0x33545844;

        /// <summary>
        /// DXT5 Compression identifier in Part 5 of <see cref="TPKBlock"/>.
        /// </summary>
        public const uint DXT5_32 = 0x35545844;
    }
}
