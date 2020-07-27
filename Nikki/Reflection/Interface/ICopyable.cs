using System;



namespace Nikki.Reflection.Interface
{
    /// <summary>
    /// Interface with a method of generating plane copies of an object.
    /// </summary>
    /// <typeparam name="TypeID"><see cref="Type"/> of the class.</typeparam>
    public interface ICopyable<TypeID>
    {
        /// <summary>
        /// Creates a plain copy of the object that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        TypeID PlainCopy();

        /// <summary>
        /// Clones values of another <see cref="ICopyable{TypeID}"/>.
        /// </summary>
        /// <param name="other"><see cref="ICopyable{TypeID}"/> to clone.</param>
        void CloneValuesFrom(TypeID other);
    }
}