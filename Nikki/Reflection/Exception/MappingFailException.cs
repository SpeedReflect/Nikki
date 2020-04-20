namespace Nikki.Reflection.Exception
{
    /// <summary>
    /// <see cref="System.Exception"/> that occurs when key or hash were unable to be resolved.
    /// </summary>
    public class MappingFailException : System.Exception
    {
        /// <summary>
        /// Initializes new instance of <see cref="ArgumentLengthException"/> 
        /// with default message.
        /// </summary>
        public MappingFailException()
            : base("Specified argument passed could not be found in the map data.") { }

        /// <summary>
        /// Initializes new instance of <see cref="ArgumentLengthException"/> 
        /// with custom message passed.
        /// </summary>
        /// <param name="message">Custom message.</param>
        public MappingFailException(string message)
            : base(message) { }
    }
}
