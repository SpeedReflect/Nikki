using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.TPKParts
{
	/// <summary>
	/// A class that contains settings about Compressed-In-Place texture blocks.
	/// </summary>
	public class MagicHeader
	{
		/// <summary>
		/// Magic ID number 0x55441122.
		/// </summary>
		public uint Magic => (uint)BinBlockID.LZCompressed;

		/// <summary>
		/// Size of decoded/decompressed data.
		/// </summary>
		public int DecodedSize { get; set; }

		/// <summary>
		/// Size of encoded/compressed data.
		/// </summary>
		public int EncodedSize { get; set; }

		/// <summary>
		/// Position of the decoded/decompressed data on loading.
		/// </summary>
		public int DecodedDataPosition { get; set; }

		/// <summary>
		/// Position of the encoded/compressed data on saving.
		/// </summary>
		public int EncodedDataPosition { get; set; }

		/// <summary>
		/// Padding pointer that is used in the game.
		/// </summary>
		public int PaddingPtr => 0;

		/// <summary>
		/// Compressed data that this header contains.
		/// </summary>
		public byte[] Data { get; set; }

		/// <summary>
		/// Length of the compressed data.
		/// </summary>
		public int Length => this.Data?.Length ?? -1;

		/// <summary>
		/// Initializes new instance of <see cref="MagicHeader"/>.
		/// </summary>
		public MagicHeader() { }

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			const int HEADER_SIZE = 0x10;

			// Save position
			var position = br.BaseStream.Position - 4;

			// Read settings
			this.DecodedSize = br.ReadInt32();
			this.EncodedSize = br.ReadInt32();
			this.DecodedDataPosition = br.ReadInt32();
			this.EncodedDataPosition = br.ReadInt32();

			// Skip PaddingPtr, and read compressed data size
			br.BaseStream.Position += 8;
			var version = br.ReadByte();
			br.BaseStream.Position += 7;

			int size = br.ReadInt32();
			size += version == 2 ? 0 : HEADER_SIZE;

			// Return back to texture header
			br.BaseStream.Position -= HEADER_SIZE;

			// Decompress data
			this.Data = Interop.Decompress(br.ReadBytes(size));

			// Advance position in the stream
			br.BaseStream.Position = position + this.EncodedSize;
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			// Write all settings
			bw.Write(this.Magic);
			bw.Write(this.DecodedSize);
			bw.Write(this.EncodedSize);
			bw.Write(this.DecodedDataPosition);
			bw.Write(this.EncodedDataPosition);
			bw.Write(this.PaddingPtr);

			// Write internal byte array
			bw.Write(this.Data);
			bw.FillBuffer(4);
		}
	}
}
