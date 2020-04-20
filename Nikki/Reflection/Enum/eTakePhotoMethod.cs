namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of taking photo methods in Underground 2.
	/// </summary>
	public enum eTakePhotoMethod : byte
	{
		/// <summary>
		/// Take photo by yourself for magazine.
		/// </summary>
		MAGAZINE_YOURSELF = 1,

		/// <summary>
		/// Take photo by yourself for dvd.
		/// </summary>
		DVD_YOURSELF = 2,

		/// <summary>
		/// Take photo in-place for magazine.
		/// </summary>
		MAGAZINE_AUTO = 3,

		/// <summary>
		/// Take photo in-place for dvd.
		/// </summary>
		DVD_AUTO = 4,
	}
}