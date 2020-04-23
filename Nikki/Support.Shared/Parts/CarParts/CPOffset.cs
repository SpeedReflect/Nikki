using System.Collections.Generic;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A <see cref="DBModelPart"/> unit CarPart attribute offset class.
	/// </summary>
	public class CPOffset
	{
		/// <summary>
		/// Offset of this <see cref="CPOffset"/> in the <see cref="DBModelPart"/>.
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// List of offsets in that this <see cref="CPOffset"/> contains.
		/// </summary>
		public List<ushort> AttribOffsets { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="CPOffset"/>.
		/// </summary>
		public CPOffset()
		{
			this.AttribOffsets = new List<ushort>();
		}

		/// <summary>
		/// Initializes new instance of <see cref="CPOffset"/>.
		/// </summary>
		/// <param name="length">Initial capacity of internal offset list.</param>
		public CPOffset(int length)
		{
			this.AttribOffsets = new List<ushort>(length);
		}

		/// <summary>
		/// Initializes new instance of <see cref="CPOffset"/>.
		/// </summary>
		/// <param name="length">Initial capacity of internal offset list.</param>
		/// <param name="offset">Offset of this <see cref="CPOffset"/> in <see cref="DBModelPart"/>.</param>
		public CPOffset(int length, int offset)
		{
			this.AttribOffsets = new List<ushort>(length);
			this.Offset = offset;
		}

		/// <summary>
		/// Returns attribute count as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => $"Offset: {this.Offset:X4} | Count = {this.AttribOffsets?.Count}";
	}
}
