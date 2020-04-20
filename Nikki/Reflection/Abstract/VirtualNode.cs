using System.Collections.Generic;



namespace Nikki.Reflection.Abstract
{
	/// <summary>
	/// Node that can be used for representing virtual hierarchy of collections in the database.
	/// </summary>
	public class VirtualNode
	{
		/// <summary>
		/// Name of the <see cref="VirtualNode"/> class.
		/// </summary>
		public string NodeName { get; set; }

		/// <summary>
		/// List of child <see cref="VirtualNode"/> classes.
		/// </summary>
		public List<VirtualNode> SubNodes { get; set; }

		/// <summary>
		/// Default constructor: initializes instance of <see cref="VirtualNode"/> class.
		/// </summary>
		/// <param name="NodeName">Name of the <see cref="NodeName"/> property of 
		/// <see cref="VirtualNode"/> class.</param>
		public VirtualNode(string NodeName)
		{
			this.NodeName = NodeName;
			this.SubNodes = new List<VirtualNode>();
		}

		/// <summary>
		/// Gets NodeName and SubNodes count as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"{this.NodeName}: {this.SubNodes.Count} nodes";
		}
	}
}
