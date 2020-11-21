namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Enum of car part attribute types.
	/// </summary>
	public enum CarPartAttribType : int
	{
		/// <summary>
		/// Attribute with a <see langword="bool"/> stored.
		/// </summary>
		Boolean = 1,

		/// <summary>
		/// Attribute with an <see langword="int"/> stored.
		/// </summary>
		Integer = 2,

		/// <summary>
		/// Attribute with a <see langword="float"/> stored.
		/// </summary>
		Floating = 3,

		/// <summary>
		/// Attribute with a <see langword="string"/> stored.
		/// </summary>
		String = 4,

		/// <summary>
		/// Attribute with two <see langword="string"/> stored.
		/// </summary>
		TwoString = 5,

		/// <summary>
		/// Attribute with Color.
		/// </summary>
		Color = 6,

		/// <summary>
		/// Attribute with CarPartID.
		/// </summary>
		CarPartID = 7,

		/// <summary>
		/// Attribute with BIN key stored that is represented as <see langword="string"/>.
		/// </summary>
		Key = 8,

		/// <summary>
		/// Attribute with in-built CPStruct in it.
		/// </summary>
		ModelTable = 9,

		/// <summary>
		/// Attribute with custom type and value.
		/// </summary>
		Custom = 10,
	}
}
