using System;



namespace Nikki.Reflection.Attributes
{
	/// <summary>
	/// Indicates that property or field can be safely memory casted to object of the same type.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	class MemoryCastableAttribute : Attribute
	{
	}
}
