using System;
using System.IO;
using Nikki.Core;
using Nikki.Reflection.ID;
using Nikki.Support.MostWanted.Class;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Framework
{
	internal static class Saver
	{
		#region Private Assemble

		private static void WritePadding(BinaryWriter bw, string mark)
		{
			int padding = 0x80 - (((int)bw.BaseStream.Length + 0x50) % 0x80);
			if (padding == 0x80) padding = 0;

			var data = new byte[0x50 + padding];
			using var ms = new MemoryStream(data);
			using var strw = new BinaryWriter(ms);

			strw.BaseStream.Position += 4;
			strw.Write(0x48 + padding);
			strw.Write(Global.Nikki);
			strw.Write((int)0);
			strw.WriteNullTermUTF8("Padding Block", 0x20);
			strw.WriteNullTermUTF8(mark, 0x20);

			bw.Write(data);
		}

		private static void WriteMaterials(BinaryWriter bw, Database.MostWanted db)
		{
			foreach (var Class in db.Materials.Collections)
				Class.Assemble(bw);
		}

		private static void WriteTPKBlocks(BinaryWriter bw, Options options, Database.MostWanted db)
		{
			foreach (var Class in db.TPKBlocks.Collections)
			{
				WritePadding(bw, options.Watermark);
				Class.Watermark = options.Watermark;
				if (Class.SettingData != null)
				{
					bw.Write(Global.TPKSettings);
					bw.Write(Class.SettingData.Length);
					bw.Write(Class.SettingData);
					WritePadding(bw, options.Watermark);
				}
				Class.Assemble(bw);
			}
		}

		private static void WriteCarTypeInfos(BinaryWriter bw, Options options, Database.MostWanted db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.CarTypeInfos);
			bw.Write(db.CarTypeInfos.Length * CarTypeInfo.BaseClassSize + 8);
			bw.Write(0x1111111111111111);
			foreach (var Class in db.CarTypeInfos.Collections)
				Class.Assemble(bw);
		}

		private static void WritePresetRides(BinaryWriter bw, Options options, Database.MostWanted db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.PresetRides);
			bw.Write(db.PresetRides.Length * PresetRide.BaseClassSize);
			foreach (var Class in db.PresetRides.Collections)
				Class.Assemble(bw);
		}

		private static void WriteCollisions(BinaryWriter bw, Database.MostWanted db)
		{
			bw.Write(Global.Collisions);
			bw.Write(-1); // temp size
			var pos = bw.BaseStream.Position;

			foreach (var Class in db.Collisions.Collections)
				Class.Assemble(bw);

			var len = bw.BaseStream.Position;
			bw.BaseStream.Position = pos - 4;
			bw.Write((int)(len - pos));
			bw.BaseStream.Position = len;
		}

		private static void WriteCarParts(BinaryWriter bw, Options options, Database.MostWanted db) =>
			CarPartManager.Assemble(bw, options.Watermark, db);

		private static void WriteFNGroups(BinaryWriter bw, Options options, Database.MostWanted db)
		{
			foreach (var Class in db.FNGroups.Collections)
			{
				WritePadding(bw, options.Watermark);
				Class.Assemble(bw);
			}
		}

		private static void WriteSTRBlocks(BinaryWriter bw, Options options, Database.MostWanted db)
		{
			foreach (var Class in db.STRBlocks.Collections)
			{
				WritePadding(bw, options.Watermark);
				Class.Watermark = options.Watermark;
				Class.Assemble(bw);
			}
		}

		#endregion

		public static bool Invoke(Options options, Database.MostWanted db)
		{
			if (String.IsNullOrEmpty(options.File)) return false;
			if (options.Flags.HasFlag(eOptFlags.None)) return false;
			if (db.Buffer == null) return false;

			using var msr = new MemoryStream(db.Buffer);
			using var msw = File.Open(options.File, FileMode.Create);

			using var br = new BinaryReader(msr);
			using var bw = new BinaryWriter(msw);

			// Write materials first
			if (options.Flags.HasFlag(eOptFlags.Materials)) WriteMaterials(bw, db);
			if (options.Flags.HasFlag(eOptFlags.TPKBlocks)) WriteTPKBlocks(bw, options, db);
			if (options.Flags.HasFlag(eOptFlags.STRBlocks)) WriteSTRBlocks(bw, options, db);
			if (options.Flags.HasFlag(eOptFlags.FNGroups)) WriteFNGroups(bw, options, db);

			while (br.BaseStream.Position < br.BaseStream.Length)
			{
				var ID = br.ReadUInt32();
				var size = br.ReadInt32();

				switch (ID)
				{
					case 0:
						var key = br.ReadUInt32();
						br.BaseStream.Position -= 4;
						if (key == Global.Nikki)
						{
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.Materials:
						if (options.Flags.HasFlag(eOptFlags.Materials))
						{
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.TPKBlocks:
					case Global.TPKSettings:
						if (options.Flags.HasFlag(eOptFlags.TPKBlocks))
						{
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.CarTypeInfos:
						if (options.Flags.HasFlag(eOptFlags.CarTypeInfos))
						{
							WriteCarTypeInfos(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.PresetRides:
						if (options.Flags.HasFlag(eOptFlags.PresetRides))
						{
							WritePresetRides(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.Collisions:
						if (options.Flags.HasFlag(eOptFlags.Collisions))
						{
							WriteCollisions(bw, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.CarParts:
						if (options.Flags.HasFlag(eOptFlags.DBModelParts))
						{
							WriteCarParts(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.FEngFiles:
					case Global.FNGCompress:
						if (options.Flags.HasFlag(eOptFlags.FNGroups))
						{
							br.BaseStream.Position += 4;
							var huff = br.ReadUInt32();
							br.BaseStream.Position -= 8;
							if (huff == 0x46465548)
							{
								goto default;
							}
							else
							{
								br.BaseStream.Position += size;
								break;
							}
						}
						else goto default;

					case Global.STRBlocks:
						if (options.Flags.HasFlag(eOptFlags.STRBlocks))
						{
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					default:
						bw.Write(ID);
						bw.Write(size);
						bw.Write(br.ReadBytes(size));
						break;
				}
			}

			return true;
		}
	}
}
