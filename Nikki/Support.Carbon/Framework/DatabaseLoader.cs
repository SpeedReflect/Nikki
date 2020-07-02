using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;
using CoreExtensions.Management;



namespace Nikki.Support.Carbon.Framework
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
		private Block presetrides;
		private Block presetskins;
		private Block slottypes;
		private Block strblocks;
		private Block suninfos;
		private Block tpkblocks;
		private Block tracks;

		public DatabaseLoader(Options options, Datamap db)
		{
			this._options = options;
			this._db = db;
			this._logger = new Logger("MainLog.txt", "Nikki.dll : Carbon DatabaseLoader", true);
			this.materials = new Block(eBlockID.Materials);
			this.tpkblocks = new Block(eBlockID.TPKBlocks);
			this.cartypeinfos = new Block(eBlockID.CarTypeInfos);
			this.presetrides = new Block(eBlockID.PresetRides);
			this.presetskins = new Block(eBlockID.PresetSkins);
			this.collisions = new Block(eBlockID.DBCarBounds);
			this.dbmodelparts = new Block(eBlockID.DBCarParts);
			this.suninfos = new Block(eBlockID.SunInfos);
			this.tracks = new Block(eBlockID.Tracks);
			this.fngroups = new Block(eBlockID.FEngFiles);
			this.strblocks = new Block(eBlockID.STRBlocks);
			this.slottypes = new Block(eBlockID.SlotTypes);
			this.caranimations = new Block(eBlockID.CarInfoAnimHookup);
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
			return Enum.IsDefined(typeof(eLZCompressionType), type);
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
			try
			{

				this.ReadBlockOffsets(br);

				this._db.STRBlocks.Disassemble(br, this.strblocks);
				this._db.Materials.Disassemble(br, this.materials);
				this._db.TPKBlocks.Disassemble(br, this.tpkblocks);
				this._db.CarTypeInfos.Disassemble(br, this.cartypeinfos);
				this._db.DBModelParts.Disassemble(br, this.dbmodelparts);
				this._db.Tracks.Disassemble(br, this.tracks);
				this._db.SunInfos.Disassemble(br, this.suninfos);
				this._db.Collisions.Disassemble(br, this.collisions);
				this._db.PresetRides.Disassemble(br, this.presetrides);
				this._db.PresetSkins.Disassemble(br, this.presetskins);
				this._db.FNGroups.Disassemble(br, this.fngroups);
				this._db.SlotTypes.Disassemble(br, this.slottypes);
				this._db.SlotOverrides.Disassemble(br, this.slottypes);
				this.ProcessCarAnimations(br);

			}
			catch (Exception e)
			{

				this._logger.WriteException(e, br.BaseStream);

			}
		}

		private void ReadBlockOffsets(BinaryReader br)
		{
			while (br.BaseStream.Position < br.BaseStream.Length)
			{

				var off = br.BaseStream.Position;
				var id = br.ReadEnum<eBlockID>();
				var size = br.ReadInt32();

				#if DEBUG
				if (!Enum.IsDefined(typeof(eBlockID), (uint)id))
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
					case eBlockID.FX:
						throw new NotSupportedException("FX Effects files are not supported");

					case eBlockID.ABKC:
						throw new NotSupportedException("ABKC Sound files are not supported");

					case eBlockID.LOCH:
						throw new NotSupportedException("LOCH Localization files are not supported");

					case eBlockID.VPAK:
						throw new NotSupportedException("VPAK Vault files are not supported");

					case eBlockID.MOIR:
						throw new NotSupportedException("MOIR Sound files are not supported");

					case eBlockID.MEMO:
						throw new NotSupportedException("Memory Data files are not supported");

					case eBlockID.MVhd:
						throw new NotSupportedException("VP6 Encoded files are not supported");

					case eBlockID.Gnsu:
						throw new NotSupportedException("GNSU Sound files are not supported");

					case eBlockID.Materials:
						this.materials.Offsets.Add(off);
						goto default;

					case eBlockID.TPKBlocks:
					case eBlockID.TPKSettings:
						this.tpkblocks.Offsets.Add(off);
						goto default;

					case eBlockID.CarTypeInfos:
						this.cartypeinfos.Offsets.Add(off);
						goto default;

					case eBlockID.PresetRides:
						this.presetrides.Offsets.Add(off);
						goto default;

					case eBlockID.PresetSkins:
						this.presetskins.Offsets.Add(off);
						goto default;

					case eBlockID.DBCarBounds:
						this.collisions.Offsets.Add(off);
						goto default;

					case eBlockID.DBCarParts:
						this.dbmodelparts.Offsets.Add(off);
						goto default;

					case eBlockID.SunInfos:
						this.suninfos.Offsets.Add(off);
						goto default;

					case eBlockID.Tracks:
						this.tracks.Offsets.Add(off);
						goto default;

					case eBlockID.STRBlocks:
						this.strblocks.Offsets.Add(off);
						goto default;

					case eBlockID.FEngFiles:
					case eBlockID.FNGCompress:
						this.fngroups.Offsets.Add(off);
						goto default;

					case eBlockID.SlotTypes:
						this.slottypes.Offsets.Add(off);
						goto default;

					case eBlockID.CarInfoAnimHookup:
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

					manager[i].PrimaryAnimation = br.ReadEnum<eCarAnimLocation>();

				}

			}
		}

		public void Dispose() => this._logger.Dispose();
	}
}
