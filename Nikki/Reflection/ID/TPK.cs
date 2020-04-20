using Nikki.Support.Shared.Class;



namespace Nikki.Reflection.ID
{
    /// <summary>
    /// Class of IDs related to <see cref="TPKBlock"/>.
    /// </summary>
    public static class TPK
    {
        /// <summary>
        /// Main ID
        /// </summary>
        public const uint MAINID = 0xB3300000;

        /// <summary>
        /// Info block ID
        /// </summary>
        public const uint INFO_BLOCKID = 0xB3310000;

        /// <summary>
        /// Data block ID
        /// </summary>
        public const uint DATA_BLOCKID = 0xB3320000;

        /// <summary>
        /// Part 1 ID of Info block
        /// </summary>
        public const uint INFO_PART1_BLOCKID = 0x33310001;
        
        /// <summary>
        /// Part 2 ID of Info block
        /// </summary>
        public const uint INFO_PART2_BLOCKID = 0x33310002;

        /// <summary>
        /// Part 3 ID of Info block
        /// </summary>
        public const uint INFO_PART3_BLOCKID = 0x33310003;

        /// <summary>
        /// Part 4 ID of Info block
        /// </summary>
        public const uint INFO_PART4_BLOCKID = 0x33310004;

        /// <summary>
        /// Part 5 ID of Info block
        /// </summary>
        public const uint INFO_PART5_BLOCKID = 0x33310005;

        /// <summary>
        /// Binary data block ID
        /// </summary>
        public const uint BINDATA_BLOCKID    = 0xB3312000;

        /// <summary>
        /// Anim block ID
        /// </summary>
        public const uint ANIM_BLOCKID = 0xB3312004;

        /// <summary>
        /// Part 1 ID of Anim block
        /// </summary>
        public const uint ANIMPART1_BLOCKID = 0x33312001;

        /// <summary>
        /// Part 2 ID of Anim block
        /// </summary>
        public const uint ANIMPART2_BLOCKID = 0x33312002;

        /// <summary>
        /// Part 1 ID of Data block
        /// </summary>
        public const uint DATA_PART1_BLOCKID = 0x33320001;

        /// <summary>
        /// Part 2 ID of Data block
        /// </summary>
        public const uint DATA_PART2_BLOCKID = 0x33320002;

        /// <summary>
        /// Part 3 ID of Data block
        /// </summary>
        public const uint DATA_PART3_BLOCKID = 0x33320003;

        /// <summary>
        /// Compressed texture identifier.
        /// </summary>
        public const uint COMPRESSED_TEXTURE = 0x55441122;
    }
}
