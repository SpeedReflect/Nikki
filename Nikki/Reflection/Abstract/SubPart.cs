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
    /// <see cref="SubPart"/> is a class that any <see cref="Collectable"/> may include in itself. 
    /// This class has to have any <see cref="AccessModifiableAttribute"/> properties
    /// so it can be declared modifiable from outside.
    /// </summary>
    public abstract class SubPart : IReflective, ICopyable<SubPart>
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
        /// <param name="propertyName">Field name to get the value from.</param>
        /// <returns>String value of a field name.</returns>
        public string GetValue(string propertyName) =>
            this.GetFastPropertyValue(propertyName)?.ToString() ?? String.Empty;

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="propertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <returns>True on success; false otherwise.</returns>
        public void SetValue(string propertyName, object value)
        {
            var property = this.GetFastProperty(propertyName);

            if (property is null)
            {

                throw new InfoAccessException(propertyName);

            }

            if (!Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
            {

                throw new ArgumentException($"Field or property named {property} is not accessible");

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

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public abstract SubPart PlainCopy();

        /// <summary>
        /// Clones values of another <see cref="SubPart"/>.
        /// </summary>
        /// <param name="other"><see cref="SubPart"/> to clone.</param>
        public virtual void CloneValuesFrom(SubPart other)
		{
            var thistype = this.GetType();
            var othertype = other.GetType();

            if (thistype == othertype)
			{

                foreach (var access in this.GetAccessibles())
				{

                    var property = thistype.GetProperty(access);
                    property.SetValue(this, property.GetValue(other));

				}

			}
		}
    }
}
