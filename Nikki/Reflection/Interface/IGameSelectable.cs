using Nikki.Core;



namespace Nikki.Reflection.Interface
{
	/// <summary>
	/// Interface that points to what game object belongs to.
	/// </summary>
	public interface IGameSelectable
	{
		/// <summary>
		/// Specifies game to which this <see cref="IGameSelectable"/> belongs to as an enum.
		/// </summary>
		public GameINT GameINT { get; }

		/// <summary>
		/// Specifies game to which this <see cref="IGameSelectable"/> belongs to a string.
		/// </summary>
		public string GameSTR { get; }
	}
}
