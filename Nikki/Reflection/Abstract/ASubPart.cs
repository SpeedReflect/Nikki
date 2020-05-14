using System;
using System.Collections.Generic;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using CoreExtensions.Reflection;
using CoreExtensions.Management;
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
                    yield return property.Name;
            }
        }

        /// <summary>
        /// Returns the value of a field name provided.
        /// </summary>
        /// <param name="PropertyName">Field name to get the value from.</param>
        /// <returns>String value of a field name.</returns>
        public string GetValue(string PropertyName) =>
            this.GetFastPropertyValue(PropertyName).ToString();

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="PropertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool SetValue(string PropertyName, object value)
        {
            try
            {
                var property = this.GetFastProperty(PropertyName);
                if (property == null) return false;
                if (!Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
                    throw new FieldAccessException("This field is either non-modifiable or non-accessible");
                if (property.PropertyType.IsEnum)
                    property.SetValue(this, System.Enum.Parse(property.PropertyType, value.ToString()));
                else
                    property.SetValue(this, value.ReinterpretCast(property.PropertyType));
                return true;
            }
            catch (System.Exception) { return false; }
        }

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="PropertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <param name="error">Error occured when trying to set value.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool SetValue(string PropertyName, object value, out string error)
        {
            error = null;
            try
            {
                var property = this.GetFastProperty(PropertyName);
                if (property == null)
                {
                    error = $"Field named {PropertyName} does not exist.";
                    return false;
                }
                if (!Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
                    throw new FieldAccessException("This field is either non-modifiable or non-accessible");
                if (property.PropertyType.IsEnum)
                    property.SetValue(this, System.Enum.Parse(property.PropertyType, value.ToString()));
                else
                    property.SetValue(this, value.ReinterpretCast(property.PropertyType));
                return true;
            }
            catch (System.Exception e)
            {
                error = e.GetLowestMessage();
                return false;
            }
        }
    }
}
