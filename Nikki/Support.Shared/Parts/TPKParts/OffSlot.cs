using System.Runtime.InteropServices;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.TPKParts
{
	/// <summary>
	/// Represents collection of <see cref="Texture"/> in compressed <see cref="TPKBlock"/>.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x18)]
	public class OffSlot
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
		public int EncodedSize { get; set; }

		/// <summary>
		/// Decompressed data size.
		/// </summary>
		public int DecodedSize { get; set; }

		/// <summary>
		/// User flags defined.
		/// </summary>
		public byte UserFlags { get; set; }

		/// <summary>
		/// Flags that define compression. Set to 2 since CompressedInPlace.
		/// </summary>
		public byte Flags { get; set; }

		/// <summary>
		/// Reference count.
		/// </summary>
		public short RefCount { get; set; }

		/// <summary>
		/// Unknown 4-byte signed integer value.
		/// </summary>
		public int UnknownInt32 { get; set; }
	}
}