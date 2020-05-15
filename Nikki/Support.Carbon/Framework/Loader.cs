﻿using System.IO;
using Nikki.Core;
using Nikki.Reflection.ID;
using Nikki.Support.Carbon.Class;



namespace Nikki.Support.Carbon.Framework
{
	internal static class Loader
	{
		#region Private Reading

		private static void ReadMaterial(BinaryReader br, Database.Carbon db)
		{
			var Class = new Material(br, db);
			db.Materials.Collections.Add(Class);
		}

		private static void ReadTPKBlock(BinaryReader br, Database.Carbon db)
		{
			br.BaseStream.Position -= 8;
			var Class = new TPKBlock(db.TPKBlocks.Length, db);
			Class.Disassemble(br);
			db.TPKBlocks.Collections.Add(Class);
		}

		private static void ReadCarTypeInfos(BinaryReader br, int size, Database.Carbon db)
		{
			br.BaseStream.Position += 8;
			int len = size / CarTypeInfo.BaseClassSize;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new CarTypeInfo(br, db);
				db.CarTypeInfos.Collections.Add(Class);
			}
		}

		private static void ReadCarParts(BinaryReader br, int size, Database.Carbon db) =>
			CarPartManager.Disassemble(br, size, db);

		private static void ReadPresetRides(BinaryReader br, int size, Database.Carbon db)
		{
			int len = size / PresetRide.BaseClassSize;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new PresetRide(br, db);
				db.PresetRides.Collections.Add(Class);
			}
		}

		private static void ReadPresetSkins(BinaryReader br, int size, Database.Carbon db)
		{
			int len = size / PresetSkin.BaseClassSize;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new PresetSkin(br, db);
				db.PresetSkins.Collections.Add(Class);
			}
		}

		private static void ReadCollisions(BinaryReader br, int size, Database.Carbon db)
		{
			var offset = br.BaseStream.Position + size;

			while (br.BaseStream.Position < offset)
			{
				var Class = new Collision(br, db);
				db.Collisions.Collections.Add(Class);
			}
		}

		private static void ReadFNGroup(BinaryReader br, Database.Carbon db)
		{
			br.BaseStream.Position -= 8;
			var Class = new FNGroup(br, db);
			if (Class.Destroy) return;
			db.FNGroups.Collections.Add(Class);
		}

		private static void ReadSTRBlock(BinaryReader br, Database.Carbon db)
		{
			br.BaseStream.Position -= 8;
			var Class = new STRBlock(br, db);
			db.STRBlocks.Collections.Add(Class);
		}

		#endregion

		public static bool Invoke(Options options, Database.Carbon db)
		{
			if (!File.Exists(options.File)) return false;
			if (options.Flags.HasFlag(eOptFlags.None)) return false;
			db.Buffer = File.ReadAllBytes(options.File);

			using var ms = new MemoryStream(db.Buffer);
			using var br = new BinaryReader(ms);

			uint ID = 0;
			int size = 0;

			while (br.BaseStream.Position < br.BaseStream.Length)
			{
				ID = br.ReadUInt32();
				size = br.ReadInt32();

				switch (ID)
				{
					case Global.Materials:
						if (options.Flags.HasFlag(eOptFlags.Materials))
							ReadMaterial(br, db);
						break;

					case Global.TPKBlocks:
						if (options.Flags.HasFlag(eOptFlags.TPKBlocks))
							ReadTPKBlock(br, db);
						break;

					case Global.CarTypeInfo:
						if (options.Flags.HasFlag(eOptFlags.CarTypeInfos))
							ReadCarTypeInfos(br, size, db);
						break;

					case Global.PresetRides:
						if (options.Flags.HasFlag(eOptFlags.PresetRides))
							ReadPresetRides(br, size, db);
						break;

					case Global.PresetSkins:
						if (options.Flags.HasFlag(eOptFlags.PresetSkins))
							ReadPresetSkins(br, size, db);
						break;

					case Global.Collisions:
						if (options.Flags.HasFlag(eOptFlags.Collisions))
							ReadCollisions(br, size, db);
						break;

					case Global.CarParts:
						if (options.Flags.HasFlag(eOptFlags.DBModelParts))
							ReadCarParts(br, size, db);
						break;

					case Global.FEngFiles:
					case Global.FNGCompress:
						if (options.Flags.HasFlag(eOptFlags.FNGroups))
							ReadFNGroup(br, db);
						break;

					case Global.STRBlocks:
						if (options.Flags.HasFlag(eOptFlags.STRBlocks))
							ReadSTRBlock(br, db);
						break;

					default:
						br.BaseStream.Position += size;
						break;
				}
			}
			return true;
		}
	}
}
