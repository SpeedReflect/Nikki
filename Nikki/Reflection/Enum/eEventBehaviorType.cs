namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of event behavior types in Underground 2.
	/// </summary>
	public enum eEventBehaviorType : byte
	{
		/// <summary>
		/// Circuit event.
		/// </summary>
		Circuit = 0,

		/// <summary>
		/// Sprint event.
		/// </summary>
		Sprint = 1,

		/// <summary>
		/// StreetX event.
		/// </summary>
		StreetX = 2,

		/// <summary>
		/// OpenWorld (FreeRoam) event.
		/// </summary>
		OpenWorld = 3,
		
		/// <summary>
		/// Drag event.
		/// </summary>
		Drag = 4,

		/// <summary>
		/// Drift event.
		/// </summary>
		Drift = 5,
	}
}