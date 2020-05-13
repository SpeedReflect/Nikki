using System.IO;
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

		private static void ReadTPKBlock(BinaryReader br, string file, Database.Carbon db)
		{
			br.BaseStream.Position -= 8;
			var Class = new TPKBlock(db.TPKBlocks.Length, db);
			Class.Disassemble(br);
			Class.BelongsToFile = file;
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
			db.FNGroups.Collections.Add(Class);
		}

		private static void ReadSTRBlock(BinaryReader br, Database.Carbon db)
		{
			br.BaseStream.Position -= 8;
			var Class = new STRBlock(br, db);
			db.STRBlocks.Collections.Add(Class);
		}

		#endregion

		public static bool Invoke(CarbonOptions options, Database.Carbon db)
		{
			if (!File.Exists(options.File)) return false;
			var data = File.ReadAllBytes(options.File);

			using var ms = new MemoryStream(data);
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
						if (options.Materials)
							ReadMaterial(br, db);
						break;

					case Global.TPKBlocks:
						if (options.TPKBlocks)
							ReadTPKBlock(br, options.File, db);
						break;

					case Global.CarTypeInfo:
						if (options.CarTypeInfos)
							ReadCarTypeInfos(br, size, db);
						break;

					case Global.PresetRides:
						if (options.PresetRides)
							ReadPresetRides(br, size, db);
						break;

					case Global.PresetSkins:
						if (options.PresetSkins)
							ReadPresetSkins(br, size, db);
						break;

					case Global.Collisions:
						if (options.Collisions)
							ReadCollisions(br, size, db);
						break;

					case Global.FEngFiles:
					case Global.FNGCompress:
						if (options.FNGroups)
							ReadFNGroup(br, db);
						break;

					case Global.STRBlocks:
						if (options.STRBlocks)
							ReadSTRBlock(br, db);
						break;

					case Global.CarParts:
						if (options.ModelParts)
							ReadCarParts(br, size, db);
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
