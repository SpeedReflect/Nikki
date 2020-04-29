using GlobalLib.Reflection.ID;
using GlobalLib.Utils.EA;
using System;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Assembles tpk block into a byte array.
        /// </summary>
        /// <returns>Byte array of the tpk block.</returns>
        public override unsafe byte[] Assemble()
        {
            // TPK Check
            this.CheckKeys();
            this.CheckComps();
            this.TextureSort();

            // Partial 1 Block
            var _1_Part1 = this.Get1Part1();
            var _1_Part2 = this.Get1Part2();
            var _1_Part4 = this.Get1Part4();
            var _1_Part5 = this.Get1Part5();

            // Partial 2 Block
            var _2_Part1 = this.Get2Part1();
            var _2_Part2 = this.Get2Part2();

            // Get sizes
            int _1_Size = _1_Part1.Length + _1_Part2.Length + _1_Part4.Length + _1_Part5.Length;
            int _2_Size = _2_Part1.Length + _2_Part2.Length;
            var Padding = Resolve.GetPaddingArray(_1_Size + 0x48, 0x80);
            int PadSize = Padding.Length;

            // All offsets
            int PartialOffset1 = 0x40;
            int PartialOffset2 = 0x48 + _1_Size + PadSize;

            int _1_Offset1 = PartialOffset1 + 8;
            int _1_Offset2 = _1_Offset1 + _1_Part1.Length;
            int _1_Offset4 = _1_Offset2 + _1_Part2.Length;
            int _1_Offset5 = _1_Offset4 + _1_Part4.Length;
            int PaddOffset = _1_Offset5 + _1_Part5.Length;
            int _2_Offset1 = PartialOffset2 + 8;
            int _2_Offset2 = _2_Offset1 + _2_Part1.Length;

            // Initialize .tpk array
            int total = _1_Size + _2_Size + PadSize + 0x50;
            var result = new byte[total];

            // Write everything
            fixed (byte* byteptr_t = &result[0])
            {
                *(uint*)byteptr_t = TPK.MAINID;
                *(int*)(byteptr_t + 4) = total - 8;
                *(int*)(byteptr_t + 12) = 0x30;
                *(uint*)(byteptr_t + PartialOffset1) = TPK.INFO_BLOCKID;
                *(int*)(byteptr_t + PartialOffset1 + 4) = _1_Size;
                *(uint*)(byteptr_t + PartialOffset2) = TPK.DATA_BLOCKID;
                *(int*)(byteptr_t + PartialOffset2 + 4) = _2_Size;
            }
            Buffer.BlockCopy(_1_Part1, 0, result, _1_Offset1, _1_Part1.Length);
            Buffer.BlockCopy(_1_Part2, 0, result, _1_Offset2, _1_Part2.Length);
            Buffer.BlockCopy(_1_Part4, 0, result, _1_Offset4, _1_Part4.Length);
            Buffer.BlockCopy(_1_Part5, 0, result, _1_Offset5, _1_Part5.Length);
            Buffer.BlockCopy(Padding, 0, result, PaddOffset, PadSize);
            Buffer.BlockCopy(_2_Part1, 0, result, _2_Offset1, _2_Part1.Length);
            Buffer.BlockCopy(_2_Part2, 0, result, _2_Offset2, _2_Part2.Length);

            return result;
        }
    }
}