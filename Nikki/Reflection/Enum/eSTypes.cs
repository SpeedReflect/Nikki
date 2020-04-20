namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of possible spoilers for preset rides.
	/// </summary>
	public enum eSTypes : int
	{
		/// <summary>
		/// Nullified spoiler.
		/// </summary>
		NULL = -1,

		/// <summary>
		/// Stock spoiler.
		/// </summary>
		STOCK = 0,

		/// <summary>
		/// Regular spoiler.
		/// </summary>
		BASE = 1,
		
		/// <summary>
		/// Hatchback-type spoiler.
		/// </summary>
		_HATCH = 2,

		/// <summary>
		/// Porsche-type spoiler.
		/// </summary>
		_PORSCHES = 3,

		/// <summary>
		/// Carrera-type spoiler.
		/// </summary>
		_CARRERA = 4,

		/// <summary>
		/// SUV-type spoiler.
		/// </summary>
		_SUV = 5,
	}
}
