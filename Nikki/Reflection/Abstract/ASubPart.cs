using System;
using System.Collections.Generic;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using CoreExtensions.Reflection;
using CoreExtensions.Conversions;



namespace Nikki.Reflection.Abstract
{
    /// <summary>
    /// <see cref="ASubPart"/> is a class that any <see cref="ACollectable"/> may include in itself. 
    /// This class has to have any <see cref="AccessModifiableAttribute"/> properties
    /// so it can be declared modifiable from outside.
    /// </summary>
    public abstract class ASubPart : IReflective
    {
        /// <summary>
        /// Gets <see cref="IEnumerable{T}"/> of all properties with 
        /// <see cref="AccessModifiableAttribute"/> attribute.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of strings.</returns>
        public IEnumerable<string> GetAccessibles()
        {
            foreach (var property in this.GetType().GetProperties())
            {

                if (Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
                {

                    yield return property.Name;

                }
            
            }
        }

        /// <summary>
        /// Returns the value of a field name provided.
        /// </summary>
        /// <param name="PropertyName">Field name to get the value from.</param>
        /// <returns>String value of a field name.</returns>
        public string GetValue(string PropertyName) =>
            this.GetFastPropertyValue(PropertyName)?.ToString() ?? String.Empty;

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="PropertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <returns>True on success; false otherwise.</returns>
        public void SetValue(string PropertyName, object value)
        {
            var property = this.GetFastProperty(PropertyName);

            if (property == null)
            {

                throw new InfoAccessException(PropertyName);

            }

            if (!Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
            {

                throw new InfoAccessException(PropertyName);

            }

            if (property.PropertyType.IsEnum)
            {

                property.SetValue(this, System.Enum.Parse(property.PropertyType, value.ToString()));

            }
            else
            {

                property.SetValue(this, value.ReinterpretCast(property.PropertyType));

            }
        }
    }
}
