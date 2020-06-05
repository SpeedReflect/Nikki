namespace Nikki.Reflection.Interface
{
	/// <summary>
	/// Interface with Invoke function.
	/// </summary>
	public interface IInvokable
	{
		/// <summary>
		/// Invokes loader or saver on a buffer.
		/// </summary>
		/// <returns>True on success.</returns>
		public bool Invoke();
	}
}
