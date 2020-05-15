using System.IO;
using Nikki.Core;
using Nikki.Reflection.ID;
using Nikki.Support.Underground2.Class;



namespace Nikki.Support.Underground2.Framework
{
	internal static class Loader
	{
		#region Private Reading

		private static void ReadMaterial(BinaryReader br, Database.Underground2 db)
		{
			var Class = new Material(br, db);
			db.Materials.Collections.Add(Class);
		}

		private static void ReadTPKBlock(BinaryReader br, Database.Underground2 db)
		{
			br.BaseStream.Position -= 8;
			var Class = new TPKBlock(db.TPKBlocks.Length, db);
			Class.Disassemble(br);
			db.TPKBlocks.Collections.Add(Class);
		}

		private static void ReadCarTypeInfos(BinaryReader br, int size, Database.Underground2 db)
		{
			br.BaseStream.Position += 8;
			int len = size / CarTypeInfo.BaseClassSize;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new CarTypeInfo(br, db);
				db.CarTypeInfos.Collections.Add(Class);
			}
		}

		private static void ReadCarParts(BinaryReader br, int size, Database.Underground2 db) =>
			CarPartManager.Disassemble(br, size, db);

		private static void ReadGCareer(BinaryReader br, int size, Database.Underground2 db) =>
			CareerManager.Disassemble(br, size, db);

		private static void ReadPresetRides(BinaryReader br, int size, Database.Underground2 db)
		{
			int len = size / PresetRide.BaseClassSize;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new PresetRide(br, db);
				db.PresetRides.Collections.Add(Class);
			}
		}

		private static void ReadSunInfos(BinaryReader br, int size, Database.Underground2 db)
		{
			int len = size / SunInfo.BaseClassSize;
			br.BaseStream.Position += 8;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new SunInfo(br, db);
				db.SunInfos.Collections.Add(Class);
			}
		}

		private static void ReadTracks(BinaryReader br, int size, Database.Underground2 db)
		{
			int len = size / Track.BaseClassSize;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new Track(br, db);
				db.Tracks.Collections.Add(Class);
			}
		}

		private static void ReadAcidEffects(BinaryReader br, int size, Database.Underground2 db)
		{
			int len = size / AcidEffect.BaseClassSize;
			br.BaseStream.Position += 0x18;

			for (int a1 = 0; a1 < len; ++a1)
			{
				var Class = new AcidEffect(br, db);
				db.AcidEffects.Collections.Add(Class);
			}
		}

		private static void ReadFNGroup(BinaryReader br, Database.Underground2 db)
		{
			br.BaseStream.Position -= 8;
			var Class = new FNGroup(br, db);
			if (Class.Destroy) return;
			db.FNGroups.Collections.Add(Class);
		}

		private static void ReadSTRBlock(BinaryReader br, Database.Underground2 db)
		{
			br.BaseStream.Position -= 8;
			var Class = new STRBlock(br, db);
			db.STRBlocks.Collections.Add(Class);
		}

		#endregion

		public static bool Invoke(Options options, Database.Underground2 db)
		{
			if (!File.Exists(options.File)) return false;
			if (options.Flags.HasFlag(eOptFlags.None)) return false;
			db.Buffer = File.ReadAllBytes(options.File);

			using var ms = new MemoryStream(db.Buffer);
			using var br = new BinaryReader(ms);

			bool gcareer = !options.Flags.HasFlag(eOptFlags.GCareer);

			while (br.BaseStream.Position < br.BaseStream.Length)
			{
				var ID = br.ReadUInt32();
				var size = br.ReadInt32();

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

					case Global.CarTypeInfos:
						if (options.Flags.HasFlag(eOptFlags.CarTypeInfos))
							ReadCarTypeInfos(br, size, db);
						break;

					case Global.PresetRides:
						if (options.Flags.HasFlag(eOptFlags.PresetRides))
							ReadPresetRides(br, size, db);
						break;

					case Global.CareerInfo:
						if (!gcareer)
						{
							ReadGCareer(br, size, db);
							gcareer = true;
						}
						break;

					case Global.CarParts:
						if (options.Flags.HasFlag(eOptFlags.DBModelParts))
							ReadCarParts(br, size, db);
						break;

					case Global.SunInfos:
						if (options.Flags.HasFlag(eOptFlags.SunInfos))
							ReadSunInfos(br, size, db);
						break;

					case Global.Tracks:
						if (options.Flags.HasFlag(eOptFlags.Tracks))
							ReadTracks(br, size, db);
						break;

					case Global.AcidEffects:
						if (options.Flags.HasFlag(eOptFlags.AcidEffects))
							ReadAcidEffects(br, size, db);
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
