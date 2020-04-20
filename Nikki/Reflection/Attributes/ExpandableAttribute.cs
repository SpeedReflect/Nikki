using System;



namespace Nikki.Reflection.Attributes
{
	/// <summary>
	/// Indicates that the property is an expandable class of a node.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	class ExpandableAttribute : Attribute
	{
		/// <summary>
		/// Parent of the property and/or node.
		/// </summary>
		public string Name { get; set; }

		public ExpandableAttribute(string Name)
		{
			this.Name = Name;
		}
	}
}