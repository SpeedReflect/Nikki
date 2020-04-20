namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of event unlock conditions in Underground 2.
	/// </summary>
	public enum eUnlockCondition : byte
	{
		/// <summary>
		/// Event requires specific race won.
		/// </summary>
		SPECIFIC_RACE_WON = 0,
		
		/// <summary>
		/// Event unlocks at the beginning of the stage to which it belongs to.
		/// </summary>
		AT_STAGE_START = 1,
		
		/// <summary>
		/// Event requires specific sponsor chosen.
		/// </summary>
		SPONSOR_CHOSEN = 2,
		
		/// <summary>
		/// Event requires specific amount of races won.
		/// </summary>
		REQUIRED_RACES_WON = 3,
		
		/// <summary>
		/// Event requires specific amount of URL won.
		/// </summary>
		REQUIRED_URL_WON = 4,
	}
}
