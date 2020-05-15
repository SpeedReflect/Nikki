using System;



namespace Nikki.Core
{
	/// <summary>
	/// A class that specifies loading and saving of files.
	/// </summary>
	public class Options
	{
		/// <summary>
		/// Default options.
		/// </summary>
		public static Options Default = new Options()
		{
			File = String.Empty,
			Watermark = $"Nikki by MaxHwoy | {DateTime.Today:MM/dd/yyyy}",
			MessageShow = false,
			Flags = eOptFlags.None
		};

		/// <summary>
		/// File to load or save.
		/// </summary>
		public string File { get; set; } = String.Empty;

		/// <summary>
		/// Watermark that is put in saved file.
		/// </summary>
		public string Watermark { get; set; } = String.Empty;

		/// <summary>
		/// True if messages should be showed as message boxes; false if output to console.
		/// </summary>
		public bool MessageShow { get; set; }

		/// <summary>
		/// Optional flags that specify what collections to load.
		/// </summary>
		public eOptFlags Flags { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Options"/>.
		/// </summary>
		public Options() { }

		/// <summary>
		/// Initializes new instance of <see cref="Options"/>.
		/// </summary>
		/// <param name="file">File to load.</param>
		public Options(string file) => this.File = file ?? String.Empty;

		/// <summary>
		/// Initializes new instance of <see cref="Options"/>.
		/// </summary>
		/// <param name="file">File to load.</param>
		/// <param name="flags">Flags that specify what collections to load.</param>
		public Options(string file, eOptFlags flags)
		{
			this.File = file ?? String.Empty;
			this.Flags = flags;
		}

		/// <summary>
		/// Initializes new instance of <see cref="Options"/>.
		/// </summary>
		/// <param name="file">File to load.</param>
		/// <param name="flags">Flags that specify what collections to load.</param>
		/// <param name="watermark">Watermark that is put in saved file.</param>
		public Options(string file, eOptFlags flags, string watermark)
		{
			this.File = file ?? String.Empty;
			this.Flags = flags;
			this.Watermark = watermark ?? String.Empty;
		}

		/// <summary>
		/// Initializes new instance of <see cref="Options"/>.
		/// </summary>
		/// <param name="file">File to load.</param>
		/// <param name="flags">Flags that specify what collections to load.</param>
		/// <param name="watermark">Watermark that is put in saved file.</param>
		/// <param name="message_show">True if messages should be showed in message 
		/// boxes; false if they should be printed to console.</param>
		public Options(string file, eOptFlags flags, string watermark, bool message_show)
		{
			this.File = file ?? String.Empty;
			this.Flags = flags;
			this.Watermark = watermark ?? String.Empty;
			this.MessageShow = message_show;
		}
	}
}
