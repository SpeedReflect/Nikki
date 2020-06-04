using System.IO;
using Nikki.Reflection.Enum;



namespace Nikki.Database
{
	/// <summary>
	/// A piece of binary data in a file.
	/// </summary>
	public class Block
	{
		/// <summary>
		/// ID of this block.
		/// </summary>
		internal eBlockID BlockID { get; set; }

		/// <summary>
		/// Offset of this <see cref="Block"/> in the buffer.
		/// </summary>
		public long Offset { get; set; }

		/// <summary>
		/// Size of this <see cref="Block"/>.
		/// </summary>
		public long Size { get; set; }

		/// <summary>
		/// Start data offset at which data starts in this <see cref="Block"/>.
		/// </summary>
		public long Start => this.Offset + 8;

		/// <summary>
		/// Final data offset at which data ends in this <see cref="Block"/>.
		/// </summary>
		public long Final => this.Start + this.Size;

		/// <summary>
		/// Next <see cref="Block"/> that comes after this one.
		/// </summary>
		public Block Next { get; set; }
	}
}
