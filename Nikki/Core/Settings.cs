using System;



namespace Nikki.Core
{
	/// <summary>
	/// Static class with settings applied on runtime.
	/// </summary>
	public static class Settings
	{
		/// <summary>
		/// Directory from which files should be loaded.
		/// </summary>
		public static string InputDirectory { get; set; } = String.Empty;

		/// <summary>
		/// Directory to which files should be saved.
		/// </summary>
		public static string OutputDirectory { get; set; } = String.Empty;

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
