using GlobalLib.Reflection.ID;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Finds offsets of all partials and its parts in the tpk block.
        /// </summary>
        /// <param name="byteptr_t">Pointer to the tpk block array.</param>
        /// <returns>Array of all offsets.</returns>
        protected override unsafe int[] FindOffsets(byte* byteptr_t)
        {
            var offsets = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            int ReaderOffset = 8; // start after ID and size
            uint ReaderID = 0;
            int InfoBlockSize = 0;
            int DataBlockSize = 0;

            while (ReaderID != TPK.INFO_BLOCKID)
            {
                ReaderID = *(uint*)(byteptr_t + ReaderOffset);
                InfoBlockSize = *(int*)(byteptr_t + ReaderOffset + 4);
                if (ReaderID != TPK.INFO_BLOCKID) ReaderOffset += InfoBlockSize;
                ReaderOffset += 8;
            }

            InfoBlockSize += ReaderOffset; // relative offset
            while (ReaderOffset < InfoBlockSize)
            {
                ReaderID = *(uint*)(byteptr_t + ReaderOffset);
                switch (ReaderID)
                {
                    case TPK.INFO_PART1_BLOCKID:
                        offsets[0] = ReaderOffset;
                        goto default;

                    case TPK.INFO_PART2_BLOCKID:
                        offsets[1] = ReaderOffset;
                        goto default;

                    case TPK.INFO_PART3_BLOCKID:
                        offsets[2] = ReaderOffset;
                        goto default;

                    case TPK.INFO_PART4_BLOCKID:
                        offsets[3] = ReaderOffset;
                        goto default;

                    case TPK.INFO_PART5_BLOCKID:
                        offsets[4] = ReaderOffset;
                        goto default;

                    default:
                        ReaderOffset += 8 + *(int*)(byteptr_t + ReaderOffset + 4);
                        break;
                }
            }

            while (ReaderID != TPK.DATA_BLOCKID)
            {
                ReaderID = *(uint*)(byteptr_t + ReaderOffset);
                DataBlockSize = *(int*)(byteptr_t + ReaderOffset + 4);
                if (ReaderID != TPK.DATA_BLOCKID) ReaderOffset += DataBlockSize;
                ReaderOffset += 8;
            }

            DataBlockSize += ReaderOffset; // relative offset
            while (ReaderOffset < DataBlockSize)
            {
                ReaderID = *(uint*)(byteptr_t + ReaderOffset);
                switch (ReaderID)
                {
                    case TPK.DATA_PART1_BLOCKID:
                        offsets[5] = ReaderOffset;
                        goto default;

                    case TPK.DATA_PART2_BLOCKID:
                        offsets[6] = ReaderOffset;
                        goto default;

                    case TPK.DATA_PART3_BLOCKID:
                        offsets[7] = ReaderOffset;
                        goto default;

                    default:
                        ReaderOffset += 8 + *(int*)(byteptr_t + ReaderOffset + 4);
                        break;
                }
            }

            return offsets;
        }
    }
}