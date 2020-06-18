using System.IO;



namespace Nikki.Reflection.Interface
{
	/// <summary>
	/// Interface with methods of assembling and disassembling classes and structs using 
	/// binary streams.
	/// </summary>
	public interface IAssembly
	{
		/// <summary>
		/// Assembles class or struct into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Assemble(BinaryWriter bw);

		/// <summary>
		/// Disassembles array into class or struct properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Disassemble(BinaryReader br);

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Serialize(BinaryWriter bw);

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br);
	}
}
