using GlobalLib.Reflection.ID;
using GlobalLib.Support.Shared.Parts.TPKParts;
using GlobalLib.Utils;

namespace GlobalLib.Support.Carbon.Class
{
	public partial class TPKBlock
	{
		/// <summary>
		/// Parses compressed texture and returns it on the output.
		/// </summary>
		/// <param name="byteptr_t">Pointer to the tpk block array.</param>
		/// <param name="offslot">Offslot of the texture to be parsed</param>
		/// <returns>Decompressed texture valid to the current support.</returns>
		protected override unsafe void ParseCompTexture(byte* byteptr_t, OffSlot offslot)
		{
			byteptr_t += offslot.AbsoluteOffset;
			if (*(uint*)byteptr_t != TPK.COMPRESSED_TEXTURE)
				return; // if not a compressed texture

			// Decompress all data excluding 0x18 byte header
			var data = new byte[offslot.CompressedSize - 0x18];
			for (int a1 = 0; a1 < data.Length; ++a1)
				data[a1] = *(byteptr_t + 0x18 + a1);
			data = JDLZ.Decompress(data);

			// In compressed textures, their header lies right in the end (0x7C + 0x18 bytes)
			fixed (byte* dataptr_t = &data[0])
			{
				int offset = data.Length - 0x7C - 0x18;
				var Read = new Texture(dataptr_t, offset, 0x7C, this._collection_name, this.Database);
				Read.ReadData(dataptr_t, 0, true);
				uint compression = *(uint*)(dataptr_t + offset + 0x7C + 0xC);
				this.compressions.Add(compression);
				this.Textures.Add(Read);
			}
		}
	}
}