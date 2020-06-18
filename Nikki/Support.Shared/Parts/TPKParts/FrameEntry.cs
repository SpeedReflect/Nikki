using System.ComponentModel;
using Nikki.Utils;
using CoreExtensions.Conversions;



namespace Nikki.Support.Shared.Parts.TPKParts
{
	/// <summary>
	/// A unit frame that is used in <see cref="AnimSlot"/>.
	/// </summary>
	public class FrameEntry
	{
		/// <summary>
		/// Name of the frame.
		/// </summary>
		[Category("Main")]
		public string Name { get; set; }

		/// <summary>
		/// Bin hash of the frame.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint BinKey => this.Name.BinHash();

		/// <summary>
		/// Name of the entry.
		/// </summary>
		/// <returns>Name of the entry as a string value.</returns>
		public override string ToString() => this.Name;
	}
}
