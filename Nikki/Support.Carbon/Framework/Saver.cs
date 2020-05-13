using System;
using System.IO;
using Nikki.Core;
using Nikki.Reflection.ID;
using Nikki.Support.Carbon.Class;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
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
			strw.WriteNullTermUTF8("Padding Block", 0x20);
			strw.WriteNullTermUTF8(mark, 0x20);

			bw.Write(data);
		}

		private static void WriteMaterials(BinaryWriter bw, Database.Carbon db)
		{
			foreach (var Class in db.Materials.Collections)
				Class.Assemble(bw);
		}

		private static void WriteTPKBlocks(BinaryWriter bw, CarbonOptions options, Database.Carbon db)
		{
			Shared.Class.TPKBlock.Watermark = options.Watermark;
			foreach (var Class in db.TPKBlocks.Collections)
			{
				if (Class.BelongsToFile == options.File)
				{
					WritePadding(bw, options.Watermark);
					Class.Assemble(bw);
				}
			}
		}

		private static void WriteCarTypeInfos(BinaryWriter bw, CarbonOptions options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.CarTypeInfo);
			bw.Write(db.CarTypeInfos.Length * CarTypeInfo.BaseClassSize + 8);
			bw.Write(0x1111111111111111);
			foreach (var Class in db.CarTypeInfos.Collections)
				Class.Assemble(bw);
		}

		private static void WritePresetRides(BinaryWriter bw, CarbonOptions options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.PresetRides);
			bw.Write(db.PresetRides.Length * PresetRide.BaseClassSize);
			foreach (var Class in db.PresetRides.Collections)
				Class.Assemble(bw);
		}

		private static void WritePresetSkins(BinaryWriter bw, CarbonOptions options, Database.Carbon db)
		{
			WritePadding(bw, options.Watermark);
			bw.Write(Global.PresetSkins);
			bw.Write(db.PresetSkins.Length * PresetSkin.BaseClassSize);
			foreach (var Class in db.PresetSkins.Collections)
				Class.Assemble(bw);
		}

		private static void WriteCollisions(BinaryWriter bw, Database.Carbon db)
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

		private static void WriteSTRBlocks(BinaryWriter bw, CarbonOptions options, Database.Carbon db)
		{
			Shared.Class.STRBlock.Watermark = options.Watermark;
			foreach (var Class in db.STRBlocks.Collections)
			{
				if (Class.BelongsToFile == options.File)
				{
					WritePadding(bw, options.Watermark);
					Class.Assemble(bw);
				}
			}
		}

		#endregion

		public static bool Invoke(CarbonOptions options, byte[] buffer, Database.Carbon db)
		{
			using var msr = new MemoryStream(buffer);
			using var msw = File.Open(options.File, FileMode.Create);

			using var br = new BinaryReader(msr);
			using var bw = new BinaryWriter(msw);

			while (br.BaseStream.Position < br.BaseStream.Length)
			{
				var ID = br.ReadUInt32();
				var size = br.ReadInt32();

				// Write materials first
				if (options.Materials) WriteMaterials(bw, db);
				if (options.TPKBlocks) WriteTPKBlocks(bw, options, db);
				if (options.STRBlocks) WriteSTRBlocks(bw, options, db);

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
						if (options.Materials)
						{
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.TPKBlocks:
						if (options.TPKBlocks)
						{
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.CarTypeInfo:
						if (options.CarTypeInfos)
						{
							WriteCarTypeInfos(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.PresetRides:
						if (options.PresetRides)
						{
							WritePresetRides(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.PresetSkins:
						if (options.PresetSkins)
						{
							WritePresetSkins(bw, options, db);
							br.BaseStream.Position += size;
							break;
						}
						else goto default;

					case Global.Collisions:
						if (options.Collisions)
						{
							WriteCollisions(bw, db);
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
