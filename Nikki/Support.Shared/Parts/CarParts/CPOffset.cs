using System;
using System.Collections.Generic;
using Nikki.Support.Shared.Class;
using CoreExtensions.IO;



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
		public List<ushort> AttribOffsets { get; }

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
		public override bool Equals(object obj) => obj is CPOffset offset && this == offset;

		/// <summary>
		/// Returns the hash code for this <see cref="CPOffset"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = 0x17;

			var sortlist = new List<ushort>(this.AttribOffsets);
			sortlist.Sort();

			for (int loop = 0; loop < sortlist.Count; ++loop)
			{

				result = HashCode.Combine(result, sortlist[loop], sortlist[loop].ToString());
			
			}

			return result;
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
			else if (off2 is null) return false;

			if (off1.AttribOffsets.Count != off2.AttribOffsets.Count) return false;

			var list1 = new List<ushort>(off1.AttribOffsets);
			var list2 = new List<ushort>(off2.AttribOffsets);

			list1.Sort();
			list2.Sort();

			for (int loop = 0; loop < list1.Count; ++loop)
			{

				if (list1[loop] != list2[loop]) return false;
			
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

		/// <summary>
		/// Writes current information stored to <see cref="Logger"/> provided.
		/// </summary>
		/// <param name="logger"><see cref="Logger"/> to write information with.</param>
		public void WriteToLog(Logger logger)
		{
			logger.WriteLine($"Offset: 0x{this.Offset:X4}");

			int code = 0x17;
			var line = String.Empty;
			var sort = new List<ushort>(this.AttribOffsets);

			sort.Sort();

			foreach (var offset in sort)
			{

				line += $"0x{offset:X4} ";
			
			}

			logger.WriteLine($"Attrib: {line}");
			logger.WriteLine($"Computations (initial timestep [0x{code:X8}]):");

			for (int i = 0; i < sort.Count; ++i)
			{

				code = HashCode.Combine(code, sort[i], sort[i].ToString());
				logger.WriteLine($"Iteration {i + 1} -> HashCode [0x{code:X8}]");

			}

			logger.WriteLine($"Final HashCode [0x{code:X8}]");
		}
	}
}
