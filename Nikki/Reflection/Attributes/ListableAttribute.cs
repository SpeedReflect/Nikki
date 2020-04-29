using System;
using System.Collections.Generic;



namespace Nikki.Reflection.Attributes
{
	/// <summary>
	/// Indicates that the property is an <see cref="IEnumerable{T}"/> and should be
	/// expanded based on its length.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class ListableAttribute : Attribute
	{
		/// <summary>
		/// Parent of the property and/or node.
		/// </summary>
		public string Parent { get; set; }

		/// <summary>
		/// Name that is shared between units in the <see cref="IEnumerable{T}"/> when
		/// it is expanded in the treeview. Each unit will get an index appended to the 
		/// end based on its index in the enumerable itself.
		/// </summary>
		public string BaseName { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="ListableAttribute"/> and applies it 
		/// to the property or field that it is attached to.
		/// </summary>
		/// <param name="parent">Parent node of this property and/or node.</param>
		/// <param name="name">Base name that is shared between all units.</param>
		public ListableAttribute(string parent, string name)
		{
			this.Parent = parent;
			this.BaseName = name;
		}
	}
}