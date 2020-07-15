namespace Nikki.Reflection.Enum
{
	/// <summary>
	/// Represents serialization type for collections and managers.
	/// </summary>
	public enum SerializeType : int
	{
		/// <summary>
		/// If imported collection does not exist, adds it; else ignores and skips.
		/// </summary>
		Negate = 0,

		/// <summary>
		/// If imported collection does not exist, adds it; else, depending on collection type, 
		/// either replaces existing one or synchronizes properties of both new and existing 
		/// collections, and then replaces existing collection with newly syncrhonized one.
		/// </summary>
		Synchronize = 1,
				
		/// <summary>
		/// If imported collection does not exist, adds it; else replaces existing one.
		/// </summary>
		Override = 2,
	}
}
