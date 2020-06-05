namespace Nikki.Reflection.Enum
{
    /// <summary>
    /// Enum of animation locations.
    /// </summary>
	public enum eCarAnimLocation : sbyte
	{
        /// <summary>
        /// No animations
        /// </summary>
        CARANIM_NONE = -1,

        /// <summary>
        /// Hood animations.
        /// </summary>
        CARANIM_HOOD = 0,

        /// <summary>
        /// Trunk animations.
        /// </summary>
        CARANIM_TRUNK = 1,

        /// <summary>
        /// Left door animations.
        /// </summary>
        CARANIM_LEFT_DOOR = 2,

        /// <summary>
        /// Right door animations.
        /// </summary>
        CARANIM_RIGHT_DOOR = 3,

        /// <summary>
        /// Multiple animations.
        /// </summary>
        CARANIM_NUM = 4,
    }
}
