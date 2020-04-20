namespace Nikki.Reflection.Enum
{
    /// <summary>
    /// Enum of part unlock requirements in Underground 2.
    /// </summary>
	public enum ePartUnlockReq : byte
	{
        /// <summary>
        /// No requirements.
        /// </summary>
        NO_REQUIREMENTS = 0,

        /// <summary>
        /// Need to find specific shop.
        /// </summary>
        SPECIFIC_SHOP_FOUND = 1,

        /// <summary>
        /// Need to win specific amount of Regular races.
        /// </summary>
        REQUIRED_REG_RACES_WON = 2,

        /// <summary>
        /// Need to win specific amount of URL races.
        /// </summary>
        REQUIRED_URL_RACES_WON = 3,

        /// <summary>
        /// Need to win specific amount of Sponsor races.
        /// </summary>
        REQUIRED_SPON_RACES_WON = 4,

        /// <summary>
        /// Initially unlocked.
        /// </summary>
        INITIALLY_UNLOCKED = 6,
    }
}
