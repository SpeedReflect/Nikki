using System.Collections.Generic;
using Nikki.Reflection.Attributes;



namespace Nikki.Reflection.Interface
{
    /// <summary>
    /// <see cref="IReflective"/> is an interface with class reflection methods.
    /// </summary>
    public interface IReflective
    {
        /// <summary>
        /// Gets <see cref="IEnumerable{T}"/> of all properties with 
        /// <see cref="AccessModifiableAttribute"/> attribute.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of strings.</returns>
        IEnumerable<string> GetAccessibles();

        /// <summary>
        /// Returns the value of a field name provided.
        /// </summary>
        /// <param name="propertyName">Field name to get the value from.</param>
        /// <returns>String value of a field name.</returns>
        string GetValue(string propertyName);

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="propertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <returns>True on success; false otherwise.</returns>
        void SetValue(string propertyName, object value);
    }
}