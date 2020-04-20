using System;



namespace Nikki.Reflection.Interface
{
    /// <summary>
    /// Interface with a method of casting memory of one object to another of the same type.
    /// </summary>
    /// <typeparam name="TypeID"><see cref="Type"/> of the class.</typeparam>
    public interface ICastable<TypeID>
    {
        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        TypeID MemoryCast(string CName);
    }
}