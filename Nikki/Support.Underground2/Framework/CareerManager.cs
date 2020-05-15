using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.ID;
using Nikki.Reflection.Enum;
using Nikki.Support.Underground2.Gameplay;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Framework
{
	/// <summary>
	/// A static manager to assemble and disassemble GCareer collections.
	/// </summary>
	public static class CareerManager
	{
		private const int max = 0x7FFFFFFF;

		#region Private Assemble

		private static byte[] WriteGCareerRaces(BinaryWriter strw, Database.Underground2 db)
		{
			var result = new byte[8 + db.GCareerRaces.Length * 0x88];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.EVENT_BLOCK); // write ID
			bw.Write(result.Length - 8);      // write size
			foreach (var race in db.GCareerRaces.Collections)
				race.Assemble(bw, strw);
			
			return result;
		}

		private static byte[] WriteWorldShops(BinaryWriter strw, Database.Underground2 db)
		{
			var result = new byte[8 + db.WorldShops.Length * 0xA0];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.SHOP_BLOCK); // write ID
			bw.Write(result.Length - 8);     // write size
			foreach (var shop in db.WorldShops.Collections)
				shop.Assemble(bw, strw);

			return result;
		}

		private static byte[] WriteGCareerBrands(BinaryWriter strw, Database.Underground2 db)
		{
			var result = new byte[8 + db.GCareerBrands.Length * 0x44];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.BRAND_BLOCK); // write ID
			bw.Write(result.Length - 8); // write size
			foreach (var brand in db.GCareerBrands.Collections)
				brand.Assemble(bw, strw);

			return result;
		}

		private static byte[] WritePartPerformances(Database.Underground2 db)
		{
			var result = new byte[0x2C90]; // max size of perf part block
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);

			bw.Write(CareerInfo.PERF_PACK_BLOCK); // write ID
			bw.Write(result.Length - 8); // write size
			for (int a1 = 0; a1 < 10; ++a1)
			{
				for (int a2 = 0; a2 < 3; ++a2)
				{
					// Write PerfIndex and Upgrade level
					int count = 0;
					bw.Write(a1);
					bw.Write(a2 + 1);
					var startpos = bw.BaseStream.Position;
					bw.Write(-1); // temp part count

					// Write perf part 1, if exists
					if (Map.PerfPartTable[a1, a2, 0] != 0)
					{
						uint key = Map.PerfPartTable[a1, a2, 0];
						var cla = db.PartPerformances.FindCollection(key, eKeyType.BINKEY);
						cla.Assemble(bw);
						++count;
					}
					else bw.BaseStream.Position += 0x5C;

					// Write perf part 2, if exists
					if (Map.PerfPartTable[a1, a2, 1] != 0)
					{
						uint key = Map.PerfPartTable[a1, a2, 1];
						var cla = db.PartPerformances.FindCollection(key, eKeyType.BINKEY);
						cla.Assemble(bw);
						++count;
					}
					else bw.BaseStream.Position += 0x5C;

					// Write perf part 3, if exists
					if (Map.PerfPartTable[a1, a2, 2] != 0)
					{
						uint key = Map.PerfPartTable[a1, a2, 2];
						var cla = db.PartPerformances.FindCollection(key, eKeyType.BINKEY);
						cla.Assemble(bw);
						++count;
					}
					else bw.BaseStream.Position += 0x5C;

					// Write perf part 4, if exists
					if (Map.PerfPartTable[a1, a2, 3] != 0)
					{
						uint key = Map.PerfPartTable[a1, a2, 3];
						var cla = db.PartPerformances.FindCollection(key, eKeyType.BINKEY);
						cla.Assemble(bw);
						++count;
					}
					else bw.BaseStream.Position += 0x5C;

					// Write perf part count and return back
					var finalpos = bw.BaseStream.Position;
					bw.BaseStream.Position = startpos;
					bw.Write(count);
					bw.BaseStream.Position = finalpos;
				}
			}

			return result;
		}

		private static byte[] WriteGShowcases(BinaryWriter strw, Database.Underground2 db)
		{
			var result = new byte[8 + db.GShowcases.Length * 0x40];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.SHOWCASE_BLOCK); // write ID
			bw.Write(result.Length - 8);         // write size
			foreach (var showcase in db.GShowcases.Collections)
				showcase.Assemble(bw, strw);

			return result;
		}

		private static byte[] WriteSMSMessages(BinaryWriter strw, Database.Underground2 db)
		{
			var result = new byte[8 + db.SMSMessages.Length * 0x14];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.SMS_MESSAGE_BLOCK); // write ID
			bw.Write(result.Length - 8);            // write size
			foreach (var message in db.SMSMessages.Collections)
				message.Assemble(bw, strw);

			return result;
		}

		private static byte[] WriteSponsors(BinaryWriter strw, Database.Underground2 db)
		{
			var result = new byte[8 + db.Sponsors.Length * 0x10];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.SPONSOR_BLOCK); // write ID
			bw.Write(result.Length - 8);        // write size
			foreach (var sponsor in db.Sponsors.Collections)
				sponsor.Assemble(bw, strw);
			
			return result;
		}

		private static byte[] WriteGCareerStages(Database.Underground2 db)
		{
			var result = new byte[8 + db.GCareerStages.Length * 0x50];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.STAGE_BLOCK); // write ID
			bw.Write(result.Length - 8);      // write size
			foreach (var stage in db.GCareerStages.Collections)
				stage.Assemble(bw);

			return result;
		}

		private static byte[] WritePerfSliderTunings(Database.Underground2 db)
		{
			var result = new byte[8 + db.PerfSliderTunings.Length * 0x18];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.TUNING_PERF_BLOCK); // write ID
			bw.Write(result.Length - 8);            // write size
			foreach (var slider in db.PerfSliderTunings.Collections)
				slider.Assemble(bw);

			return result;
		}

		private static byte[] WriteWorldChallenges(BinaryWriter strw, Database.Underground2 db)
		{
			var result = new byte[8 + db.WorldChallenges.Length * 0x18];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.WORLD_CHAL_BLOCK); // write ID
			bw.Write(result.Length - 8);           // write size
			foreach (var challenge in db.WorldChallenges.Collections)
				challenge.Assemble(bw, strw);

			return result;
		}

		private static byte[] WritePartUnlockables(Database.Underground2 db)
		{
			var result = new byte[8 + db.PartUnlockables.Length * 0x28];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.PART_UNLOCKS_BLOCK); // write ID
			bw.Write(result.Length - 8); // write size
			foreach (var part in db.PartUnlockables.Collections)
				part.Assemble(bw);

			return result;
		}

		private static byte[] WriteBankTriggers(Database.Underground2 db)
		{
			var result = new byte[8 + db.BankTriggers.Length * 0xC];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.BANK_TRIGS_BLOCK); // write ID
			bw.Write(result.Length - 8);           // write size
			foreach (var bank in db.BankTriggers.Collections)
				bank.Assemble(bw);

			return result;
		}

		private static byte[] WriteGCarUnlocks(Database.Underground2 db)
		{
			var result = new byte[8 + db.GCarUnlocks.Length * 0xC];
			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);
			bw.Write(CareerInfo.CAR_UNLOCKS_BLOCK); // write ID
			bw.Write(result.Length - 8);            // write size
			foreach (var carunlock in db.GCarUnlocks.Collections)
				carunlock.Assemble(bw);

			return result;
		}

		#endregion

		#region Private Disassemble

		private static long[] FindOffsets(BinaryReader br, int size)
		{
			var offsets = new long[] { max, max, max, max, max, max, max, 
				max, max, max, max, max, max, max };
			var ReaderOffset = br.BaseStream.Position;

			while (br.BaseStream.Position < ReaderOffset + size)
			{
				switch (br.ReadUInt32())
				{
					case CareerInfo.STRING_BLOCK:
						offsets[0] = br.BaseStream.Position;
						goto default;

					case CareerInfo.EVENT_BLOCK:
						offsets[1] = br.BaseStream.Position;
						goto default;

					case CareerInfo.SHOP_BLOCK:
						offsets[2] = br.BaseStream.Position;
						goto default;

					case CareerInfo.BRAND_BLOCK:
						offsets[3] = br.BaseStream.Position;
						goto default;

					case CareerInfo.PERF_PACK_BLOCK:
						offsets[4] = br.BaseStream.Position;
						goto default;

					case CareerInfo.SHOWCASE_BLOCK:
						offsets[5] = br.BaseStream.Position;
						goto default;

					case CareerInfo.SMS_MESSAGE_BLOCK:
						offsets[6] = br.BaseStream.Position;
						goto default;

					case CareerInfo.SPONSOR_BLOCK:
						offsets[7] = br.BaseStream.Position;
						goto default;

					case CareerInfo.STAGE_BLOCK:
						offsets[8] = br.BaseStream.Position;
						goto default;

					case CareerInfo.TUNING_PERF_BLOCK:
						offsets[9] = br.BaseStream.Position;
						goto default;

					case CareerInfo.WORLD_CHAL_BLOCK:
						offsets[10] = br.BaseStream.Position;
						goto default;

					case CareerInfo.PART_UNLOCKS_BLOCK:
						offsets[11] = br.BaseStream.Position;
						goto default;

					case CareerInfo.BANK_TRIGS_BLOCK:
						offsets[12] = br.BaseStream.Position;
						goto default;

					case CareerInfo.CAR_UNLOCKS_BLOCK:
						offsets[13] = br.BaseStream.Position;
						goto default;

					default:
						int over = br.ReadInt32();
						br.BaseStream.Position += over;
						break;
				}
			}
			return offsets;
		}

		private static void ReadStrings(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			int ReaderSize = br.ReadInt32();
			var ReaderOffset = br.BaseStream.Position;
			while (br.BaseStream.Position < ReaderOffset + ReaderSize)
				br.ReadNullTermUTF8().BinHash();
		}

		private static void ReadGCareerRaces(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x88;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new GCareerRace(br, strr, db);
				db.GCareerRaces.Collections.Add(Class);
			}
		}

		private static void ReadWorldShops(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0xA0;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new WorldShop(br, db);
				db.WorldShops.Collections.Add(Class);
			}
		}

		private static void ReadGCareerBrands(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x44;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new GCareerBrand(br, db);
				db.GCareerBrands.Collections.Add(Class);
			}
		}

		private static void ReadPartPerformances(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x17C;

			for (int a1 = 0; a1 < size; ++a1)
			{
				int index = br.ReadInt32();
				int level = br.ReadInt32() - 1;
				int total = br.ReadInt32();

				for (int a2 = 0; a2 < total; ++a2)
				{
					var Class = new PartPerformance(br, db, index, level, a2);
					db.PartPerformances.Collections.Add(Class);
				}
			}
		}

		private static void ReadGShowcases(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x40;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new GShowcase(br, db);
				db.GShowcases.Collections.Add(Class);
			}
		}

		private static void ReadSMSMessages(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x14;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new SMSMessage(br, strr, db);
				db.SMSMessages.Collections.Add(Class);
			}
		}

		private static void ReadSponsors(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x10;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new Sponsor(br, strr, db);
				db.Sponsors.Collections.Add(Class);
			}
		}

		private static void ReadGCareerStages(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x50;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new GCareerStage(br, db);
				db.GCareerStages.Collections.Add(Class);
			}
		}

		private static void ReadPerfSliderTunings(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x18;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new PerfSliderTuning(br, db);
				db.PerfSliderTunings.Collections.Add(Class);
			}
		}

		private static void ReadWorldChallenges(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x18;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new WorldChallenge(br, strr, db);
				db.WorldChallenges.Collections.Add(Class);
			}
		}

		private static void ReadPartUnlockables(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0x28;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new PartUnlockable(br, db);
				db.PartUnlockables.Collections.Add(Class);
			}
		}

		private static void ReadBankTriggers(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0xC;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new BankTrigger(br, db);
				db.BankTriggers.Collections.Add(Class);
			}
		}

		private static void ReadGCarUnlocks(BinaryReader br, Database.Underground2 db)
		{
			if (br.BaseStream.Position == max) return;
			int size = br.ReadInt32() / 0xC;

			for (int a1 = 0; a1 < size; ++a1)
			{
				var Class = new GCarUnlock(br, db);
				db.GCarUnlocks.Collections.Add(Class);
			}
		}

		#endregion

		/// <summary>
		/// Assembles entire roots of GCareer collections into a byte array and 
		/// writes it with <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> database with roots 
		/// and collections.</param>
		public static void Assemble(BinaryWriter bw, Database.Underground2 db)
		{
			// Initialize string BinaryWriter
			var ms = new MemoryStream();
			var strw = new BinaryWriter(ms);
			strw.WriteNullTermUTF8(Settings.Watermark);

			// Get arrays of all blocks
			var GCareerRacesBlock = WriteGCareerRaces(strw, db);
			var WorldShopBlock = WriteWorldShops(strw, db);
			var GCareerBrandsBlock = WriteGCareerBrands(strw, db);
			var PartPerformancesBlock = WritePartPerformances(db);
			var GShowcasesBlock = WriteGShowcases(strw, db);
			var SMSMessagesBlock = WriteSMSMessages(strw, db);
			var SponsorsBlock = WriteSponsors(strw, db);
			var GCareerStagesBlock = WriteGCareerStages(db);
			var PerfSliderTuningsBlock = WritePerfSliderTunings(db);
			var WorldChallengesBlock = WriteWorldChallenges(strw, db);
			var PartUnlockablesBlock = WritePartUnlockables(db);
			var BankTriggersBlock = WriteBankTriggers(db);
			var GCarUnlocksBlock = WriteGCarUnlocks(db);

			// Compress to the position
			strw.FillBuffer(4);
			var StringBlock = ms.ToArray();

			// Pre-calculate size
			var size = 8 + StringBlock.Length;
			size += GCareerRacesBlock.Length;
			size += WorldShopBlock.Length;
			size += GCareerBrandsBlock.Length;
			size += PartPerformancesBlock.Length;
			size += GShowcasesBlock.Length;
			size += SMSMessagesBlock.Length;
			size += SponsorsBlock.Length;
			size += GCareerStagesBlock.Length;
			size += PerfSliderTuningsBlock.Length;
			size += WorldChallengesBlock.Length;
			size += PartUnlockablesBlock.Length;
			size += BankTriggersBlock.Length;
			size += GCarUnlocksBlock.Length;

			// Pre-calculate padding
			var padding = Comp.GetPaddingArray(size + 0x50, 0x80);
			size += padding.Length;

			// Finally, write entire Career Block
			bw.Write(CareerInfo.MAINID);
			bw.Write(size);
			bw.Write(CareerInfo.STRING_BLOCK);
			bw.Write(StringBlock.Length);
			bw.Write(StringBlock);
			bw.Write(GCareerRacesBlock);
			bw.Write(WorldShopBlock);
			bw.Write(GCareerBrandsBlock);
			bw.Write(PartPerformancesBlock);
			bw.Write(GShowcasesBlock);
			bw.Write(SMSMessagesBlock);
			bw.Write(SponsorsBlock);
			bw.Write(GCareerStagesBlock);
			bw.Write(PerfSliderTuningsBlock);
			bw.Write(WorldChallengesBlock);
			bw.Write(PartUnlockablesBlock);
			bw.Write(BankTriggersBlock);
			bw.Write(GCarUnlocksBlock);
			bw.Write(padding);
		}

		/// <summary>
		/// Disassembles entire GCareer block using <see cref="BinaryReader"/> provided into 
		/// separate collections and stores them in <see cref="Database.Underground2"/> passed.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="size">Size of the GCareer block.</param>
		/// <param name="db"><see cref="Database.Underground2"/> where all collections 
		/// should be stored.</param>
		public static void Disassemble(BinaryReader br, int size, Database.Underground2 db)
		{
			var position = br.BaseStream.Position + size;
			var PartOffsets = FindOffsets(br, size);

			// Read and hash all strings
			br.BaseStream.Position = PartOffsets[0];
			using var ms = new MemoryStream(br.ReadInt32());
			using var strr = new BinaryReader(ms);
			br.BaseStream.Position = PartOffsets[0];
			ReadStrings(br);

			// Read all career races
			br.BaseStream.Position = PartOffsets[1];
			ReadGCareerRaces(br, strr, db);

			// Read all world shops
			br.BaseStream.Position = PartOffsets[2];
			ReadWorldShops(br, db);

			// Read all career brands
			br.BaseStream.Position = PartOffsets[3];
			ReadGCareerBrands(br, db);

			// Read all part performances
			br.BaseStream.Position = PartOffsets[4];
			ReadPartPerformances(br, db);

			// Read all showcases
			br.BaseStream.Position = PartOffsets[5];
			ReadGShowcases(br, db);

			// Read all sms messages
			br.BaseStream.Position = PartOffsets[6];
			ReadSMSMessages(br, strr, db);

			// Read all sponsors
			br.BaseStream.Position = PartOffsets[7];
			ReadSponsors(br, strr, db);

			// Read all career stages
			br.BaseStream.Position = PartOffsets[8];
			ReadGCareerStages(br, db);

			// Read performance sliders
			br.BaseStream.Position = PartOffsets[9];
			ReadPerfSliderTunings(br, db);

			// Read world challenges
			br.BaseStream.Position = PartOffsets[10];
			ReadWorldChallenges(br, strr, db);

			// Read part unlockables
			br.BaseStream.Position = PartOffsets[11];
			ReadPartUnlockables(br, db);

			// Read bank triggers
			br.BaseStream.Position = PartOffsets[12];
			ReadBankTriggers(br, db);

			// Read car unlocks
			br.BaseStream.Position = PartOffsets[13];
			ReadGCarUnlocks(br, db);

			// Return
			br.BaseStream.Position = position;
		}
	}
}
