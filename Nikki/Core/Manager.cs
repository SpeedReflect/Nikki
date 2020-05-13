using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using CoreExtensions.IO;



namespace Nikki.Core
{
	public static class Manager
	{
		public static void LoadBinKeys(IEnumerable<string> files)
		{
			foreach (var file in files)
			{
				if (!File.Exists(file)) continue;
				try
				{
					var lines = File.ReadAllLines(file);
					foreach (var line in lines)
					{
						if (line.StartsWith("//") || line.StartsWith("#")) continue;
						else line.BinHash();
					}
				}
				catch (Exception) { }
			}
		}

		public static void LoadVltKeys(IEnumerable<string> files)
		{
			foreach (var file in files)
			{
				if (!File.Exists(file)) continue;
				try
				{
					var lines = File.ReadAllLines(file);
					foreach (var line in lines)
					{
						if (line.StartsWith("//") || line.StartsWith("#")) continue;
						else line.VltHash();
					}
				}
				catch (Exception) { }
			}
		}

		public static void LoadVaultAttributes(string file)
		{
			if (!File.Exists(file)) return;
			try
			{
				using var br = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read));
				if (br.ReadUInt32() != 0x4B415056) return;

				int packs = br.ReadInt32();
				int cnameoff = br.ReadInt32();
				br.BaseStream.Position += 4;

				for (int curpack = 0; curpack < packs; ++curpack)
				{
					var position = br.BaseStream.Position;
					int VaultNameOffset = br.ReadInt32();
					br.BaseStream.Position += 8;
					int VaultOffset = br.ReadInt32();
					br.BaseStream.Position = cnameoff + VaultNameOffset;
					var VaultName = br.ReadNullTermUTF8();
					if (VaultName == "db")
					{
						br.BaseStream.Position = VaultOffset;
						var ID = br.ReadUInt32();
						int size = br.ReadInt32();
						if (ID == 0x53747245)
						{
							var offset = br.BaseStream.Position;
							while (br.BaseStream.Position < offset + size)
							{
								var str = br.ReadNullTermUTF8();
								if (!String.IsNullOrEmpty(str))
									str.VltHash();
							}
							break;
						}
					}
					br.BaseStream.Position = position + 0x14;
				}
			}
			catch (Exception) { }
		}

		public static void LoadVaultFEAttribs(string file)
		{
			if (!File.Exists(file)) return;
			try
			{
				using var br = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read));
				if (br.ReadUInt32() != 0x4B415056) return;

				int packs = br.ReadInt32();
				int cnameoff = br.ReadInt32();
				br.BaseStream.Position += 4;

				for (int curpack = 0; curpack < packs; ++curpack)
				{
					var position = br.BaseStream.Position;
					int VaultNameOffset = br.ReadInt32();
					br.BaseStream.Position += 8;
					int VaultOffset = br.ReadInt32();
					br.BaseStream.Position = cnameoff + VaultNameOffset;
					var VaultName = br.ReadNullTermUTF8();
					if (VaultName == "frontend")
					{
						br.BaseStream.Position = VaultOffset;
						var ID = br.ReadUInt32();
						int size = br.ReadInt32();
						if (ID == 0x53747245)
						{
							var offset = br.BaseStream.Position;
							while (br.BaseStream.Position < offset + size)
							{
								var str = br.ReadNullTermUTF8();
								if (!String.IsNullOrEmpty(str))
									str.VltHash();
							}
							break;
						}
					}
					br.BaseStream.Position = position + 0x14;
				}
			}
			catch (Exception) { }
		}
	}
}
