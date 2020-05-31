using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Reflection.Enum;
using Nikki.Support.Carbon.Class;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	internal static class Saver
	{
		#region Private Assemble

		private static void WritePadding(BinaryWriter bw, string mark)
		{
			if (bw.BaseStream.Position == 0) return;

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

		private static void WriteMaterials(BinaryWriter bw, Database.Carbon db)
		{
			foreach (var Class in db.Materials.Collections)
				Class.Assemble(bw);
		}

		private static void WriteTPKBlocks(BinaryWriter bw, Options options, Database.Carbon db)
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

		private static void WriteCarTypeInfos(BinaryWriter bw, Options options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.CarTypeInfos);
			bw.Write(db.CarTypeInfos.Length * CarTypeInfo.BaseClassSize + 8);
			bw.Write(0x1111111111111111);
			foreach (var Class in db.CarTypeInfos.Collections)
				Class.Assemble(bw);
		}

		private static void WritePresetRides(BinaryWriter bw, Options options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.PresetRides);
			bw.Write(db.PresetRides.Length * PresetRide.BaseClassSize);
			foreach (var Class in db.PresetRides.Collections)
				Class.Assemble(bw);
		}

		private static void WritePresetSkins(BinaryWriter bw, Options options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.PresetSkins);
			bw.Write(db.PresetSkins.Length * PresetSkin.BaseClassSize);
			foreach (var Class in db.PresetSkins.Collections)
				Class.Assemble(bw);
		}

		private static void WriteCollisions(BinaryWriter bw, Options options, Database.Carbon db)
		{
			long pos = 0;
			long len = 0;

			// Write Padding
			bw.Write((int)0);
			bw.Write((int)-1);
			pos = bw.BaseStream.Position;
			bw.Write(Global.Nikki);
			bw.Write((int)0);
			bw.WriteNullTermUTF8("Padding Block", 0x20);
			bw.WriteNullTermUTF8(options.Watermark, 0x20);
			bw.FillBuffer(8);
			if (bw.BaseStream.Position % 0x10 == 0) bw.Write((long)0);
			len = bw.BaseStream.Position;
			bw.BaseStream.Position = pos - 4;
			bw.Write((int)(len - pos));
			bw.BaseStream.Position = len;

			// Write Collisions
			bw.Write(Global.Collisions);
			bw.Write(-1); // temp size
			pos = bw.BaseStream.Position;

			foreach (var Class in db.Collisions.Collections)
				Class.Assemble(bw);

			len = bw.BaseStream.Position;
			bw.BaseStream.Position = pos - 4;
			bw.Write((int)(len - pos));
			bw.BaseStream.Position = len;
		}

		private static void WriteSunInfos(BinaryWriter bw, Options options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.SunInfos);
			bw.Write(db.SunInfos.Length * SunInfo.BaseClassSize);
			foreach (var Class in db.SunInfos.Collections)
				Class.Assemble(bw);
		}

		private static void WriteTracks(BinaryWriter bw, Options options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.Tracks);
			bw.Write(db.Tracks.Length * Track.BaseClassSize);
			foreach (var Class in db.Tracks.Collections)
				Class.Assemble(bw);
		}

		private static void WriteCarParts(BinaryWriter bw, Options options, Database.Carbon db) =>
			CarPartManager.Assemble(bw, options.Watermark, db);

		private static void WriteFNGroups(BinaryWriter bw, Options options, Database.Carbon db)
		{
			foreach (var Class in db.FNGroups.Collections)
			{
				WritePadding(bw, options.Watermark);
				Class.Assemble(bw);
			}
		}

		private static void WriteSTRBlocks(BinaryWriter bw, Options options, Database.Carbon db)
		{
			foreach (var Class in db.STRBlocks.Collections)
			{
				WritePadding(bw, options.Watermark);
				Class.Watermark = options.Watermark;
				Class.Assemble(bw);
			}
		}

		#endregion

		public static bool Invoke(Options options, Database.Carbon db)
		{
			if (String.IsNullOrEmpty(options.File)) return false;
			if (options.Flags.HasFlag(eOptFlags.None)) return false;
			if (db.Buffer == null) return false;

			using var msr = new MemoryStream(db.Buffer);
			using var msw = new MemoryStream(db.Buffer.Length);

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
						else
						{
							WritePadding(bw, options.Watermark);
							goto default;
						}

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

					case Global.PresetSkins:
						if (options.Flags.HasFlag(eOptFlags.PresetSkins))
						{
							WritePresetSkins(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.Collisions:
						if (options.Flags.HasFlag(eOptFlags.Collisions))
						{
							WriteCollisions(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.SunInfos:
						if (options.Flags.HasFlag(eOptFlags.SunInfos))
						{
							WriteSunInfos(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.Tracks:
						if (options.Flags.HasFlag(eOptFlags.Tracks))
						{
							WriteTracks(bw, options, db);
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

					case Global.LimitsTable:
					case Global.ELabGlobal:
						WritePadding(bw, options.Watermark);
						goto default;

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

			var buffer = msw.ToArray();
			if (options.Compress) buffer = Interop.Compress(buffer, eLZCompressionType.JDLZ);

			using (var writer = new BinaryWriter(File.Open(options.File, FileMode.Create)))
			{
				writer.Write(buffer);
			}

			return true;
		}
	}
}
