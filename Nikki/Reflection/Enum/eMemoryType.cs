namespace Nikki.Reflection.Enum
{
    /// <summary>
    /// Enum of car memory types.
    /// </summary>
    public enum eMemoryType : uint
    {
        /// <summary>
        /// Player Memory Type.
        /// </summary>
        Player = 0x757C0CEC,

        /// <summary>
        /// Racing Memory Type.
        /// </summary>
        Racing = 0x79602673,

        /// <summary>
        /// Cop Memory Type.
        /// </summary>
        Cop = 0x00009F61,

        /// <summary>
        /// Traffic Memory Type.
        /// </summary>
        Traffic = 0x66DB297E,

        /// <summary>
        /// BigTraffic Memory Type.
        /// </summary>
        BigTraffic = 0xAE00B390,
        
        /// <summary>
        /// HugeTraffic Memory Type.
        /// </summary>
        HugeTraffic = 0x911B6387,
    }
}