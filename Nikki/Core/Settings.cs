using System;



namespace Nikki.Core
{
	/// <summary>
	/// Static class with settings applied on runtime.
	/// </summary>
	public static class Settings
	{
		/// <summary>
		/// True if output messages to console; false if output to log file.
		/// </summary>
		public static bool MessageShow { get; set; } = false;

		/// <summary>
		/// Watermark that is written to saved files.
		/// </summary>
		public static string Watermark { get; set; } = String.Empty;
	}
}
