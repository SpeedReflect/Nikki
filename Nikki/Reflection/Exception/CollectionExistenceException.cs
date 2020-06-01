namespace Nikki.Reflection.Exception
{
    /// <summary>
    /// <see cref="System.Exception"/> that occurs when collection with CollectionName  
    /// provided already exists in the database.
    /// </summary>
    public class CollectionExistenceException : System.Exception
    {
        /// <summary>
        /// Initializes new instance of <see cref="CollectionExistenceException"/> 
        /// with default message.
        /// </summary>
        public CollectionExistenceException()
            : base("Collection with name provided already exists") { }

        /// <summary>
        /// Initializes new instance of <see cref="CollectionExistenceException"/> 
        /// with custom message passed.
        /// </summary>
        /// <param name="CName">CollectionName that caused exception.</param>
        public CollectionExistenceException(string CName)
            : base($"Collection named {CName} already exists") { }
    }
}
