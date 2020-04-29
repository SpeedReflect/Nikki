using GlobalLib.Reflection.ID;
using GlobalLib.Support.Shared.Parts.TPKParts;

namespace GlobalLib.Support.Carbon.Class
{
	public partial class TPKBlock
	{
		/// <summary>
		/// Gets list of offset slots of the textures in the tpk block array.
		/// </summary>
		/// <param name="byteptr_t">Pointer to the tpk block array.</param>
		/// <param name="offset">Partial 1 part3 offset in the tpk block array.</param>
		protected override unsafe void GetOffsetSlots(byte* byteptr_t, int offset)
		{
			if (offset == -1) return;  // if Part3 does not exist
			if (*(uint*)(byteptr_t + offset) != TPK.INFO_PART3_BLOCKID)
				return; // check Part3 ID

			int ReaderSize = 8 + *(int*)(byteptr_t + offset + 4);
			int current = 8;
			while (current < ReaderSize)
			{
				var offslot = new OffSlot();
				offslot.Key = *(uint*)(byteptr_t + offset + current);
				offslot.AbsoluteOffset = *(int*)(byteptr_t + offset + current + 4);
				offslot.CompressedSize = *(int*)(byteptr_t + offset + current + 8);
				offslot.ActualSize = *(int*)(byteptr_t + offset + current + 0xC);
				offslot.ToHeaderOffset = *(int*)(byteptr_t + offset + current + 0x10);
				offslot.UnknownInt32 = *(int*)(byteptr_t + offset + current + 0x14);
				this.offslots.Add(offslot);
				current += 0x18;
			}
		}
	}
}