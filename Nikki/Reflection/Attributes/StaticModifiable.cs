using System;



namespace Nikki.Reflection.Attributes
{
	/// <summary>
	/// Indicates that the field or property can be statically modified through collections.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	class StaticModifiableAttribute : Attribute
	{
	}
}
