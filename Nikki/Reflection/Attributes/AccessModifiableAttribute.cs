using System;



namespace Nikki.Reflection.Attributes
{
	/// <summary>
	/// Indicates that the field or property can be accessed and modified by user.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	class AccessModifiableAttribute : Attribute
	{
	}
}