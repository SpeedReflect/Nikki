namespace Nikki.Reflection.Exception
{
	/// <summary>
	/// <see cref="System.Exception"/> that occurs when attempting to set or get value 
	/// of a non-existent field or property.
	/// </summary>
	public class InfoAccessException : System.Exception
	{
        /// <summary>
        /// Initializes new instance of <see cref="InfoAccessException"/> 
        /// with default message.
        /// </summary>
        public InfoAccessException()
            : base("Property or field does not exist") { }

        /// <summary>
        /// Initializes new instance of <see cref="InfoAccessException"/> 
        /// with custom message passed.
        /// </summary>
        /// <param name="name">Name of the property/field.</param>
        public InfoAccessException(string name)
            : base($"Property or field named {name} does not exist") { }
    }
}
