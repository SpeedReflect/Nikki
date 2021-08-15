using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;
using CoreExtensions.Management;



namespace Nikki.Support.Undercover.Framework
{
	internal class DatabaseLoader : IDisposable
	{
		private readonly Options _options = Options.Default;
		private readonly Datamap _db;
		private readonly Logger _logger;

		#if DEBUG
		private Dictionary<uint, List<long>> _offsets = new Dictionary<uint, List<long>>();
		#endif

		private Block caranimations;
		private Block cartypeinfos;
		private Block collisions;
		private Block dbmodelparts;
		private Block fngroups;
		private Block materials;
		private Block slottypes;
		private Block strblocks;
		private Block suninfos;
		private Block tpkblocks;
		private Block tracks;
		private Block vectorvinyls;

		public DatabaseLoader(Options options, Datamap db)
		{
			this._options = options;
			this._db = db;
			this._logger = new Logger("MainLog.txt", "Nikki.dll : Undercover DatabaseLoader", true);
			this.materials = new Block(BinBlockID.Materials);
			this.tpkblocks = new Block(BinBlockID.TPKBlocks);
			this.cartypeinfos = new Block(BinBlockID.CarTypeInfos);
			this.collisions = new Block(BinBlockID.DBCarBounds);
			this.dbmodelparts = new Block(BinBlockID.DBCarParts);
			this.suninfos = new Block(BinBlockID.SunInfos);
			this.tracks = new Block(BinBlockID.Tracks);
			this.fngroups = new Block(BinBlockID.FEngFiles);
			this.strblocks = new Block(BinBlockID.STRBlocks);
			this.slottypes = new Block(BinBlockID.SlotTypes);
			this.vectorvinyls = new Block(BinBlockID.VinylSystem);
			this.caranimations = new Block(BinBlockID.CarInfoAnimHookup);
		}

		public void Invoke()
		{
			var info = new FileInfo(this._options.File);
			if (!info.Exists) return;

			var comp = this.NeedsDecompression();
			if (!comp && info.Length > (1 << 26)) this.ReadFromStream();
			else this.ReadFromBuffer(comp);

			#if DEBUG
			foreach (var pair in this._offsets)
			{

				this._logger.WriteLine($"File: {this._options.File}");
				this._logger.Write($"0x{pair.Key:X8} | ");

				foreach (var off in pair.Value)
				{

					this._logger.Write($" ---> 0x{off:X8}");

				}

				this._logger.WriteLine(String.Empty);

			}
			#endif

			ForcedX.GCCollect();
		}

		private bool NeedsDecompression()
		{
			var array = new byte[4];
			using var fs = new FileStream(this._options.File, FileMode.Open, FileAccess.Read);
			fs.Read(array, 0, 4);
			var type = BitConverter.ToInt32(array, 0);
			return Enum.IsDefined(typeof(LZCompressionType), type);
		}

		private void ReadFromStream()
		{
			using var br = new BinaryReader(File.Open(this._options.File, FileMode.Open, FileAccess.Read));
			this.Disassemble(br);
		}

		private void ReadFromBuffer(bool compressed)
		{
			var buffer = File.ReadAllBytes(this._options.File);
			if (compressed) buffer = Interop.Decompress(buffer);
			using var ms = new MemoryStream(buffer);
			using var br = new BinaryReader(ms);
			this.Disassemble(br);
		}

		private void Disassemble(BinaryReader br)
		{
			this.ReadBlockOffsets(br);

			this._db.STRBlocks.Disassemble(br, this.strblocks);
			this._db.Materials.Disassemble(br, this.materials);
			this._db.TPKBlocks.Disassemble(br, this.tpkblocks);
			this._db.CarTypeInfos.Disassemble(br, this.cartypeinfos);
			this._db.DBModelParts.Disassemble(br, this.dbmodelparts);
			this._db.SunInfos.Disassemble(br, this.suninfos);
			this._db.Tracks.Disassemble(br, this.tracks);
			this._db.Collisions.Disassemble(br, this.collisions);
			this._db.FNGroups.Disassemble(br, this.fngroups);
			this._db.SlotTypes.Disassemble(br, this.slottypes);
			this._db.SlotOverrides.Disassemble(br, this.slottypes);
			this._db.VectorVinyls.Disassemble(br, this.vectorvinyls);
			this.ProcessCarAnimations(br);
		}

		private void ReadBlockOffsets(BinaryReader br)
		{
			while (br.BaseStream.Position < br.BaseStream.Length)
			{

				var off = br.BaseStream.Position;
				var id = br.ReadEnum<BinBlockID>();
				var size = br.ReadInt32();

				#if DEBUG
				if (!Enum.IsDefined(typeof(BinBlockID), (uint)id))
				{

					Console.WriteLine("Located unknown data block. Please send MailLog file to the developer!!!");

					if (this._offsets.TryGetValue((uint)id, out var list))
					{

						list.Add(off);

					}
					else
					{

						list = new List<long>() { off };
						this._offsets[(uint)id] = list;

					}

				}
				#endif

				switch (id)
				{
					case BinBlockID.Materials:
						this.materials.Offsets.Add(off);
						goto default;

					case BinBlockID.TPKBlocks:
					case BinBlockID.EmitterTexturePage:
						this.tpkblocks.Offsets.Add(off);
						goto default;

					case BinBlockID.CarTypeInfos:
						this.cartypeinfos.Offsets.Add(off);
						goto default;

					case BinBlockID.DBCarBounds:
						this.collisions.Offsets.Add(off);
						goto default;

					case BinBlockID.DBCarParts:
						this.dbmodelparts.Offsets.Add(off);
						goto default;

					case BinBlockID.SunInfos:
						this.suninfos.Offsets.Add(off);
						goto default;

					case BinBlockID.Tracks:
						this.tracks.Offsets.Add(off);
						goto default;

					case BinBlockID.STRBlocks:
						this.strblocks.Offsets.Add(off);
						goto default;

					case BinBlockID.FEngFiles:
					case BinBlockID.FNGCompress:
						this.fngroups.Offsets.Add(off);
						goto default;

					case BinBlockID.SlotTypes:
						this.slottypes.Offsets.Add(off);
						goto default;

					case BinBlockID.VinylSystem:
						this.vectorvinyls.Offsets.Add(off);
						goto default;

					case BinBlockID.CarInfoAnimHookup:
						this.caranimations.Offsets.Add(off);
						goto default;

					default:
						br.BaseStream.Position += size;
						break;

				}

			}
		}

		private void ProcessCarAnimations(BinaryReader br)
		{
			var manager = this._db.SlotTypes;
			if (manager == null) return;
			
			for (int loop = 0; loop < this.caranimations.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = this.caranimations.Offsets[loop] + 4;
				var size = br.ReadInt32();

				if (size < manager.Count)
				{

					throw new InvalidDataException("CarAnimHookup block has invalid or corrupted data");

				}

				for (int i = 0; i < manager.Count; ++i)
				{

					manager[i].PrimaryAnimation = br.ReadEnum<Shared.Class.SlotType.CarAnimLocation>();

				}

			}
		}

		public void Dispose() => this._logger.Dispose();
	}
}
