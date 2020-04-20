using System;



namespace Nikki.Reflection.Attributes
{
	/// <summary>
	/// Indicates what the property/field is used for and its optional boundaries.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	class LabelAttribute : Attribute
	{
		/// <summary>
		/// Parent of the property and/or node.
		/// </summary>
		public string Label { get; set; }

		public LabelAttribute(string Label)
		{
			this.Label = Label;
		}
	}
}