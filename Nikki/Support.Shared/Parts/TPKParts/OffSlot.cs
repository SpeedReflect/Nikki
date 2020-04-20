using System.Runtime.InteropServices;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.TPKParts
{
	/// <summary>
	/// Represents collection of <see cref="Texture"/> in compressed <see cref="TPKBlock"/>.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x18)]
	public struct OffSlot
	{
		/// <summary>
		/// Key of the <see cref="Texture"/>.
		/// </summary>
		public uint Key { get; set; }

		/// <summary>
		/// Data offset relative to beginning of the <see cref="TPKBlock"/>.
		/// </summary>
		public int AbsoluteOffset { get; set; }

		/// <summary>
		/// Compressed data size.
		/// </summary>
		public int CompressedSize { get; set; }

		/// <summary>
		/// Decompressed data size.
		/// </summary>
		public int ActualSize { get; set; }

		/// <summary>
		/// Data offset relative to this offslot.
		/// </summary>
		public int ToHeaderOffset { get; set; }

		/// <summary>
		/// Unknown 4-byte signed integer value.
		/// </summary>
		public int UnknownInt32 { get; set; }
	}
}