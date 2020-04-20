namespace Nikki.Reflection.ID
{
    /// <summary>
    /// Class of IDs related to CarPart database.
    /// </summary>
    public static class CarParts
    {
        /// <summary>
        /// ID of a unit collision bound.
        /// </summary>
        public const uint CollisionBound = 0x0003B901;

        /// <summary>
        /// 
        /// </summary>
        public const uint MAINID = 0x80034602;

        /// <summary>
        /// ID of the header of CarPart database.
        /// </summary>
        public const uint DBCARPART_HEADER = 0x00034603;

        /// <summary>
        /// ID of the main array of carparts of CarPart database.
        /// </summary>
        public const uint DBCARPART_ARRAY = 0x00034604;

        /// <summary>
        /// ID of the attributes table of CarPart database.
        /// </summary>
        public const uint DBCARPART_ATTRIBS = 0x00034605;

        /// <summary>
        /// ID of the strings block of CarPart database.
        /// </summary>
        public const uint DBCARPART_STRINGS = 0x00034606;

        /// <summary>
        /// ID of the model structs table of CarPart database.
        /// </summary>
        public const uint DBCARPART_STRUCTS = 0x0003460A;

        /// <summary>
        /// ID of the model table of CarPart database.
        /// </summary>
        public const uint DBCARPART_MODELS = 0x0003460B;

        /// <summary>
        /// ID of the attribute offset table of CarPart database.
        /// </summary>
        public const uint DBCARPART_OFFSETS = 0x0003460C;
    }
}