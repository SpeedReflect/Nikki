using System.Collections.Generic;
using Nikki.Reflection.Enum;



namespace Nikki.Core
{
	/// <summary>
	/// A piece of binary data in a file.
	/// </summary>
	internal class Block
	{
		/// <summary>
		/// ID of this block.
		/// </summary>
		public BinBlockID BlockID { get; set; }

		/// <summary>
		/// Offset of this <see cref="Block"/> in the buffer.
		/// </summary>
		public List<long> Offsets { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Block"/>.
		/// </summary>
		/// <param name="id"><see cref="BinBlockID"/> of this block.</param>
		public Block(BinBlockID id)
		{
			this.BlockID = id;
			this.Offsets = new List<long>();
		}

		/// <summary>
		/// Combines offsets of <see cref="Block"/> passed with this <see cref="Block"/>.
		/// </summary>
		/// <param name="other"><see cref="Block"/> to combine with.</param>
		public void Combine(Block other) => this.Offsets.AddRange(other.Offsets);

		/// <summary>
		/// Determines whether <see cref="Block"/> is null or its offset count is empty.
		/// </summary>
		/// <param name="block"><see cref="Block"/> to check.</param>
		/// <returns>True if <see cref="Block"/> is null or its offset count is 0; 
		/// false otherwise.</returns>
		public static bool IsNullOrEmpty(Block block) =>
			block == null || block.Offsets.Count == 0;

		/// <summary>
		/// Returns <see cref="BinBlockID"/> and offset count as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"ID: {this.BlockID} | Count: {this.Offsets.Count}";
	}
}
