using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.ID;
using Nikki.Reflection.Enum;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Shared.Parts.TPKParts;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	public static class TextureManager
	{
        #region Assemble

        private static void Get1Part1(BinaryWriter bw, TPKBlock tpk)
        {
            bw.Write(TPK.INFO_PART1_BLOCKID); // write ID
            bw.Write(0x7C); // write size
            bw.WriteEnum(tpk.Version);

            // Write CollectionName
            string CName = string.Empty;
            CName = tpk.UseCurrentName == eBoolean.True
                ? tpk.CollectionName
                : tpk.CollectionName[2..];
            bw.WriteNullTermUTF8(CName, 0x1C);

            // Write Filename
            bw.WriteNullTermUTF8(tpk.Filename, 0x40);

            // Write all other settings
            bw.Write(tpk.FilenameHash);
            bw.Write(tpk.PermBlockByteOffset);
            bw.Write(tpk.PermBlockByteSize);
            bw.Write(tpk.EndianSwapped);
            bw.Write(tpk.TexturePack);
            bw.Write(tpk.TextureIndexEntryTable);
            bw.Write(tpk.TextureStreamEntryTable);
        }

        private static void Get1Part2(BinaryWriter bw, TPKBlock tpk)
        {
            bw.Write(TPK.INFO_PART2_BLOCKID); // write ID
            bw.Write(tpk.Textures.Count * 8); // write size
            for (int a1 = 0; a1 < tpk.Textures.Count; ++a1)
            {
                bw.Write(tpk.Textures[a1].BinKey);
                bw.Write((int)0);
            }
        }

        private static void Get1Part4(BinaryWriter bw, TPKBlock tpk)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            int length = 0;
            foreach (var tex in tpk.Textures)
            {
                tex.PaletteOffset = length;
                tex.Offset = length + tex.PaletteSize;
                tex.Assemble(writer);
                length += tex.PaletteSize + tex.Size;
                var pad = 0x80 - length % 0x80;
                if (pad != 0x80) length += pad;
            }

            var data = ms.ToArray();
            bw.Write(TPK.INFO_PART4_BLOCKID); // write ID
            bw.Write(data.Length); // write size
            bw.Write(data);
        }

        private static void Get1Part5(BinaryWriter bw, TPKBlock tpk)
        {
            bw.Write(TPK.INFO_PART5_BLOCKID); // write ID
            bw.Write(tpk.Textures.Count * 0x18); // write size
            for (int a1 = 0; a1 < tpk.Textures.Count; ++a1)
            {
                bw.Write((int)0);
                bw.Write((long)0);
                bw.Write(Comp.GetInt(tpk.Textures[a1].Compression));
                bw.Write((long)0);
            }
        }

        private static void Get2Part1(BinaryWriter bw, TPKBlock tpk)
        {
            bw.Write(TPK.DATA_PART1_BLOCKID); // write ID
            bw.Write(0x18); // write size
            bw.Write(1);
            bw.Write(tpk.FilenameHash);
            bw.Write(0x50);
        }

        private static void Get2Part2(BinaryWriter bw, TPKBlock tpk)
        {
            int size = this.Textures[this.Textures.Count - 1].Offset;
            size += this.Textures[this.Textures.Count - 1].Size;
            int difference = 0x80 - (size % 0x80);
            if (difference != 0x80) // last padding
                size += difference;

            var result = new byte[size + 0x80];

            // Copy all data to the array
            for (int a1 = 0; a1 < this.keys.Count; ++a1)
            {
                if (this.Textures[a1].Compression == Comp.GetString(EAComp.P8_08))
                    Buffer.BlockCopy(this.Textures[a1].Data, 0, result, this.Textures[a1].PaletteOffset + 0x80, this.Textures[a1].PaletteSize + this.Textures[a1].Size);
                else
                    Buffer.BlockCopy(this.Textures[a1].Data, 0, result, this.Textures[a1].Offset + 0x80, this.Textures[a1].Size);
            }

            bw.Write(TPK.DATA_PART2_BLOCKID); // write ID
            bw.Write(size + 0x78); // write size
            for (int a1 = 0; a1 < 30; ++a1)
                bw.Write(0x11111111);

        }

        #endregion

        #region Disassemble

        private static long[] FindOffsets(BinaryReader br)
        {
            var offsets = new long[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            long ReaderOffset = 0;
            uint ReaderID = 0;
            int InfoBlockSize = 0;
            int DataBlockSize = 0;

            while (ReaderID != TPK.INFO_BLOCKID)
            {
                ReaderID = br.ReadUInt32();
                InfoBlockSize = br.ReadInt32();
                if (ReaderID != TPK.INFO_BLOCKID)
                    br.BaseStream.Position += InfoBlockSize;
            }

            ReaderOffset = br.BaseStream.Position;
            while (br.BaseStream.Position < ReaderOffset + InfoBlockSize)
            {
                ReaderID = br.ReadUInt32();
                switch (ReaderID)
                {
                    case TPK.INFO_PART1_BLOCKID:
                        offsets[0] = br.BaseStream.Position;
                        goto default;

                    case TPK.INFO_PART2_BLOCKID:
                        offsets[1] = br.BaseStream.Position;
                        goto default;

                    case TPK.INFO_PART3_BLOCKID:
                        offsets[2] = br.BaseStream.Position;
                        goto default;

                    case TPK.INFO_PART4_BLOCKID:
                        offsets[3] = br.BaseStream.Position;
                        goto default;

                    case TPK.INFO_PART5_BLOCKID:
                        offsets[4] = br.BaseStream.Position;
                        goto default;

                    default:
                        br.BaseStream.Position += br.ReadInt32();
                        break;
                }
            }

            while (ReaderID != TPK.DATA_BLOCKID)
            {
                ReaderID = br.ReadUInt32();
                DataBlockSize = br.ReadInt32();
                if (ReaderID != TPK.DATA_BLOCKID)
                    br.BaseStream.Position += DataBlockSize;
            }

            ReaderOffset += br.BaseStream.Position; // relative offset
            while (ReaderOffset < ReaderOffset + DataBlockSize)
            {
                ReaderID = br.ReadUInt32();
                switch (ReaderID)
                {
                    case TPK.DATA_PART1_BLOCKID:
                        offsets[5] = br.BaseStream.Position;
                        goto default;

                    case TPK.DATA_PART2_BLOCKID:
                        offsets[6] = br.BaseStream.Position;
                        goto default;

                    case TPK.DATA_PART3_BLOCKID:
                        offsets[7] = br.BaseStream.Position;
                        goto default;

                    default:
                        ReaderOffset += br.ReadInt32();
                        break;
                }
            }

            return offsets;
        }

        private static int GetTextureCount(BinaryReader br)
        {
            if (br.BaseStream.Position == -1) return 0; // check if Part2 even exists
            return br.ReadInt32() / 8; // 8 bytes for one texture
        }

        private static void GetHeaderInfo(BinaryReader br, TPKBlock tpk)
        {
            if (br.ReadInt32() != 0x7C) return; // check header size

            // Check TPK version
            if (br.ReadInt32() != (int)tpk.Version) return; // return if versions does not match

            // Get CollectionName
            tpk.CollectionName = tpk.UseCurrentName == eBoolean.True
                ? br.ReadNullTermUTF8(0x1C)
                : tpk.Index.ToString() + "_" + Comp.GetTPKName(tpk.Index, tpk.GameINT);

            // Get the rest of the settings
            br.BaseStream.Position += 0x44;
            tpk.PermBlockByteOffset = br.ReadUInt32();
            tpk.PermBlockByteSize = br.ReadUInt32();
            tpk.EndianSwapped = br.ReadInt32();
            tpk.TexturePack = br.ReadInt32();
            tpk.TextureIndexEntryTable = br.ReadInt32();
            tpk.TextureStreamEntryTable = br.ReadInt32();
        }

        private static IEnumerable<OffSlot> GetOffsetSlots(BinaryReader br)
        {
            if (br.BaseStream.Position == -1) yield break;  // if Part3 does not exist

            int ReaderSize = br.ReadInt32();
            var ReaderOffset = br.BaseStream.Position;
            while (br.BaseStream.Position < ReaderOffset + ReaderSize)
            {
                yield return new OffSlot
                {
                    Key = br.ReadUInt32(),
                    AbsoluteOffset = br.ReadInt32(),
                    CompressedSize = br.ReadInt32(),
                    ActualSize = br.ReadInt32(),
                    ToHeaderOffset = br.ReadInt32(),
                    UnknownInt32 = br.ReadInt32()
                };
            }
        }

        private static int[,] GetTextureHeaders(BinaryReader br)
        {
            int ReaderSize = br.ReadInt32();
            var ReaderOffset = br.BaseStream.Position;
            var offsets = new List<long>();
            var sizes = new List<long>();

            while (br.BaseStream.Position < ReaderOffset + ReaderSize)
            {
                offsets.Add(br.BaseStream.Position); // add offset
                var temp = br.BaseStream.Position;
                br.BaseStream.Position += 0x58; // advance to the name of the texture
                br.BaseStream.Position += br.ReadByte(); // skip texture name
                sizes.Add(br.BaseStream.Position - temp); // add size
            }

            var result = new int[offsets.Count, 2];
            for (int a1 = 0; a1 < offsets.Count; ++a1)
            {
                result[a1, 0] = (int)offsets[a1];
                result[a1, 1] = (int)sizes[a1];
            }
            return result;
        }

        private static void ParseCompTexture(BinaryReader br, OffSlot offslot, TPKBlock tpk,
            Database.Carbon db)
        {
            br.BaseStream.Position += offslot.AbsoluteOffset;
            if (br.ReadUInt32() != TPK.COMPRESSED_TEXTURE) return; // if not a compressed texture

            // Decompress all data excluding 0x18 byte header
            br.BaseStream.Position += 0x14;
            var data = br.ReadBytes(offslot.CompressedSize - 0x18);
            data = JDLZ.Decompress(data);

            using var ms = new MemoryStream(data);
            using var reader = new BinaryReader(ms);

            // In compressed textures, their header lies right in the end (0x7C + 0x18 bytes)
            reader.BaseStream.Position = reader.BaseStream.Length - 0x7C - 0x18;
            var tex = new Texture(reader, tpk.CollectionName, db);
            reader.BaseStream.Position = 0;
            tex.ReadData(reader, true);
            tpk.Textures.Add(tex);
        }

        #endregion




        /// <summary>
        /// Disassembles tpk block array into separate properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="for_db">If true, CollectionName of the <see cref="TPKBlock"/> will 
        /// be hardcoded; otherwise, it will as it is in the read data.</param>
		/// <param name="db"><see cref="Database.Carbon"/> database with roots 
		/// and collections.</param>
        public static void Disassemble(BinaryReader br, bool for_db, Database.Carbon db)
        {
            var Start = br.BaseStream.Position;
            uint ID = br.ReadUInt32();
            int size = br.ReadInt32();
            var Final = br.BaseStream.Position + size;

            var PartOffsets = FindOffsets(br);

            // Get texture count
            br.BaseStream.Position = PartOffsets[1];
            var TextureCount = GetTextureCount(br);
            if (TextureCount == 0) return; // if no textures allocated

            // Create new TPKBlock
            var tpk = for_db
                ? new TPKBlock(db.TPKBlocks.Count, db)
                : new TPKBlock(String.Empty, db);

            // Get header info
            br.BaseStream.Position = PartOffsets[0];
            GetHeaderInfo(br, tpk);

            // Get Offslot info
            br.BaseStream.Position = PartOffsets[2];
            var offslot_list = GetOffsetSlots(br).ToList();

            // Get texture header info
            br.BaseStream.Position = PartOffsets[3];
            var texture_list = GetTextureHeaders(br);

            if (PartOffsets[2] != -1)
            {
                for (int a1 = 0; a1 < TextureCount; ++a1)
                {
                    br.BaseStream.Position = Start;
                    ParseCompTexture(br, offslot_list[a1], tpk, db);
                }
            }
            else
            {
                // Add textures to the list
                for (int a1 = 0; a1 < TextureCount; ++a1)
                {
                    br.BaseStream.Position = texture_list[a1, 0];
                    var tex = new Texture(br, tpk.CollectionName, db);
                    tpk.Textures.Add(tex);
                }

                // Finally, build all .dds files
                for (int a1 = 0; a1 < TextureCount; ++a1)
                {
                    br.BaseStream.Position = PartOffsets[6] + 0x80;
                    tpk.Textures[a1].ReadData(br, false);
                }
            }

            br.BaseStream.Position = Final;
        }
    }
}
