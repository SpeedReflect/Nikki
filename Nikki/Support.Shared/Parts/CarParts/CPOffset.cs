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

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="CPOffset"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="CPOffset"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="CPOffset"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) => obj is CPOffset && this == (CPOffset)obj;

		/// <summary>
		/// Returns the hash code for this <see cref="CPOffset"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = 0x17;
			for (int a1 = 0; a1 < this.AttribOffsets.Count; ++a1)
				result = result * 37 + this.AttribOffsets[a1] + 1;
			return result.GetHashCode();
		}

		/// <summary>
		/// Determines whether two specified <see cref="CPOffset"/> have the same value.
		/// </summary>
		/// <param name="off1">The first <see cref="CPOffset"/> to compare, or null.</param>
		/// <param name="off2">The second <see cref="CPOffset"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(CPOffset off1, CPOffset off2)
		{
			if (off1 is null) return off2 is null;
			if (off2 is null) return false;

			if (off1.AttribOffsets.Count != off2.AttribOffsets.Count) return false;
			for (int a1 = 0; a1 < off1.AttribOffsets.Count; ++a1)
			{
				if (off1.AttribOffsets[a1] != off2.AttribOffsets[a1])
					return false;
			}
			return true;
		}

		/// <summary>
		/// Determines whether two specified <see cref="CPOffset"/> have different values.
		/// </summary>
		/// <param name="off1">The first <see cref="CPOffset"/> to compare, or null.</param>
		/// <param name="off2">The second <see cref="CPOffset"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(CPOffset off1, CPOffset off2) => !(off1 == off2);
	}
}
