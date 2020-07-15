using System;
using Nikki.Reflection;



namespace Nikki.Utils
{
	/// <summary>
	/// Type of return string in case of resolving key = 0.
	/// </summary>
	public enum LookupReturn : int
	{
		/// <summary>
		/// Returns <see langword="null"/>.
		/// </summary>
		NULLREF = 1,

		/// <summary>
		/// Returns <see cref="String.Empty"/>.
		/// </summary>
		EMPTY = 2,

		/// <summary>
		/// Returns <see cref="BaseArguments.NULL"/>.
		/// </summary>
		NULLARG = 3,
	}
}
