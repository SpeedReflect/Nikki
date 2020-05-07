namespace Nikki.Support.Underground2.Parts.CarParts
{
    /// <summary>
    /// Static class that stores rigid controls for Underground2 cartypeinfos.
    /// </summary>
	public static class RigidControls
	{
        /// <summary>
        /// Racer rigid controls.
        /// </summary>
        public static ushort[] RigidRacerControls = new ushort[] { 0, 0, 0, 0x40E0, 0x8000, 0x4389, 0, 0xC22C, 0, 0x422C, 0,
                                                                   0, 0xC000, 0x45DA, 0x0040, 0x0040, 0, 0x4254, 0, 0x4396, 0,
                                                                   0x4254, 0x8000, 0x4395, 0, 0, 0, 0x435C, 0, 0, 0, 0x43B4,
                                                                   0x0040, 0x0040, 0x0073, 0, 0x005B, 0, 0, 0 }; // racer const values
        
        /// <summary>
        /// Traffic rigid controls.
        /// </summary>
        public static ushort[] RigidTrafControls  = new ushort[] { 1, 0, 0, 0x42AA, 0, 0x4387, 0, 0xC22C, 0, 0x422C, 0, 0, 0,
                                                                   0x45FA, 0x00AD, 0x003D, 0, 0x4240, 0, 0x4352, 0, 0x4240, 0,
                                                                   0x434C, 0, 0, 0, 0x4348, 0, 0, 0, 0x4396, 0x0052, 0x0046,
                                                                   0x0065, 0x000B, 0x0032, 0x0014, 0, 0 }; // traffic const values
    }
}
