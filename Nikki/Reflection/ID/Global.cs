using Nikki.Support.Shared.Class;



namespace Nikki.Reflection.ID
{
    /// <summary>
    /// Class of IDs related to all global blocks.
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Special Nikki block ID
        /// </summary>
        public const uint Nikki = 0x42704D67;

        /// <summary>
        /// ID of <see cref="STRBlock"/> block.
        /// </summary>
        public const uint STRBlocks = 0x00039000;

        /// <summary>
        /// ID of <see cref="Material"/> block.
        /// </summary>
        public const uint Materials = 0x00135200;

        /// <summary>
        /// ID of <see cref="TPKBlock"/> block.
        /// </summary>
        public const uint TPKBlocks = 0xB3300000;

        /// <summary>
        /// ID of <see cref="CarTypeInfo"/> block.
        /// </summary>
        public const uint CarTypeInfos = 0x00034600;

        /// <summary>
        /// ID of SlotTypes block.
        /// </summary>
        public const uint SlotTypes = 0x00034607;

        /// <summary>
        /// ID of <see cref="DBModelPart"/> block.
        /// </summary>
        public const uint CarParts = 0x80034602;

        /// <summary>
        /// ID of <see cref="SunInfo"/> block.
        /// </summary>
        public const uint SunInfos = 0x00034202;

        /// <summary>
        /// ID of Tracks block.
        /// </summary>
        public const uint Tracks = 0x00034201;

        /// <summary>
        /// ID of <see cref="PresetRide"/> block.
        /// </summary>
        public const uint PresetRides = 0x00030220;

        /// <summary>
        /// ID of <see cref="PresetSkin"/> block.
        /// </summary>
        public const uint PresetSkins = 0x00030250;

        /// <summary>
        /// ID of <see cref="Collision"/> database block.
        /// </summary>
        public const uint Collisions = 0x8003B900;

        /// <summary>
        /// ID of Underground 2 GCareer block.
        /// </summary>
        public const uint CareerInfo = 0x80034A10;

        /// <summary>
        /// ID of Underground 2 CarSkins block.
        /// </summary>
        public const uint CarSkins = 0x00034601;

        /// <summary>
        /// ID of compressed <see cref="FNGroup"/> block.
        /// </summary>
        public const uint FNGCompress = 0x00030210;

        /// <summary>
        /// ID of decompressed <see cref="FNGroup"/> block.
        /// </summary>
        public const uint FEngFiles = 0x00030203;

        /// <summary>
        /// ID of ELabGlobal block.
        /// </summary>
        public const uint ELabGlobal = 0x80134000;

        /// <summary>
        /// ID of AcidEmmiters block.
        /// </summary>
        public const uint AcidEmmiters = 0x00035021;

        /// <summary>
        /// ID of <see cref="AcidEffect"/> block.
        /// </summary>
        public const uint AcidEffects = 0x00035020;

        /// <summary>
        /// ID of some unknown float chunk.
        /// </summary>
        public const uint FloatChunk = 0x00E34009;
        
        /// <summary>
        /// ID of Carbon limits table.
        /// </summary>
        public const uint LimitsTable = 0x8003CE0D;

        /// <summary>
        /// ID of TPK settings.
        /// </summary>
        public const uint TPKSettings = 0x0003BD00;
    }
}
