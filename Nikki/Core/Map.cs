using System.Collections.Generic;



namespace Nikki.Core
{
	/// <summary>
	/// Represents all important maps of the library.
	/// </summary>
	public static class Map
	{
		/// <summary>
		/// Map of all Bin keys during runtime of library.
		/// </summary>
		public static Dictionary<uint, string> BinKeys { get; set; } = new Dictionary<uint, string>();

		/// <summary>
		/// Map of all Vlt keys during runtime of library.
		/// </summary>
		public static Dictionary<uint, string> VltKeys { get; set; } = new Dictionary<uint, string>();
	}
}
