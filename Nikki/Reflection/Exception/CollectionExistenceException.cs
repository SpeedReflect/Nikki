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
            : base("Class with the collection name provided already exists.") { }

        /// <summary>
        /// Initializes new instance of <see cref="CollectionExistenceException"/> 
        /// with custom message passed.
        /// </summary>
        /// <param name="message">Custom message.</param>
        public CollectionExistenceException(string message)
            : base(message) { }
    }
}
