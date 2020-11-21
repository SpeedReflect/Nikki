using Nikki.Reflection.Enum;



namespace Nikki.Reflection.Interface
{
	/// <summary>
	/// Interface that declares a custom attribute type for car parts.
	/// </summary>
	public interface ICustomAttrib
	{
		/// <summary>
		/// Name of this <see cref="ICustomAttrib"/>.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// <see cref="CarPartAttribType"/> of the value stored in this <see cref="ICustomAttrib"/>.
		/// </summary>
		public CarPartAttribType Type { get; set; }
	}
}
