namespace Nikki.Reflection.Exception
{
    /// <summary>
    /// <see cref="System.Exception"/> that occurs when argument length 
    /// exceed maximum allowed value.
    /// </summary>
    public class ArgumentLengthException : System.Exception
    {
        /// <summary>
        /// Initializes new instance of <see cref="ArgumentLengthException"/> 
        /// with default message.
        /// </summary>
        public ArgumentLengthException()
            : base("Length of the passed argument exceeds the maximum allowed value.") { }

        /// <summary>
        /// Initializes new instance of <see cref="ArgumentLengthException"/> 
        /// with custom message passed.
        /// </summary>
        /// <param name="message">Custom message.</param>
        public ArgumentLengthException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes new instance of <see cref="ArgumentLengthException"/> 
        /// with default message specifying maximum length allowed.
        /// </summary>
        /// <param name="maxlength">Maximum length allowed.</param>
        public ArgumentLengthException(int maxlength)
            : base($"Length of the passed argument should not exceed {maxlength} characters.") { }
    }
}
