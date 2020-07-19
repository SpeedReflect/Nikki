using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Class;
using CoreExtensions.IO;



namespace Nikki.Core
{
	/// <summary>
	/// Static manager to load keys and various files.
	/// </summary>
	public static class Loader
	{
		/// <summary>
		/// Loads all BIN keys from files specified.
		/// </summary>
		/// <param name="files">Files to load.</param>
		public static void LoadBinKeys(IEnumerable<string> files)
		{
			var state = Hashing.PauseHashSave;
			Hashing.PauseHashSave = false;

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
				catch { }
			
			}

			Hashing.PauseHashSave = state;
		}

		/// <summary>
		/// Loads all VLT keys from files specified.
		/// </summary>
		/// <param name="files">Files to load.</param>
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
				catch { }
			
			}
		}

		/// <summary>
		/// Loads all VLT keys from attributes.bin file.
		/// </summary>
		/// <param name="file">attributes.bin file path.</param>
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
								if (!String.IsNullOrEmpty(str)) str.VltHash();
							
							}
							
							break;
						
						}
					
					}
					
					br.BaseStream.Position = position + 0x14;
				
				}
			
			}
			catch { }
		}

		/// <summary>
		/// Loads all VLT keys from fe_attrib.bin file.
		/// </summary>
		/// <param name="file">fe_attrib.bin file path.</param>
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
								if (!String.IsNullOrEmpty(str)) str.VltHash();
							
							}
							
							break;
						
						}
					
					}
					
					br.BaseStream.Position = position + 0x14;
				
				}
			
			}
			catch { }
		}
	
		/// <summary>
		/// Loads all labels from a language file and hashes them.
		/// </summary>
		/// <param name="file">Language label file path.</param>
		/// <param name="game"><see cref="GameINT"/> of the file.</param>
		public static void LoadLangLabels(string file, GameINT game)
		{
			var options = new Options(file);
			FileBase db = game switch
			{
				GameINT.Carbon => new Support.Carbon.Datamap(),
				GameINT.MostWanted => new Support.MostWanted.Datamap(),
				GameINT.Underground2 => new Support.Underground2.Datamap(),
				GameINT.Prostreet => new Support.Prostreet.Datamap(),
				_ => null
			};

			if (db == null) return;
			db.Load(options);
			
			foreach (STRBlock str in db.GetManager("STRBlocks"))
			{
			
				foreach (var record in str.GetRecords())
				{
			
					record.Text.BinHash();
			
				}
			
			}
		}
	}
}
