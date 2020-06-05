using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	internal class DatabaseLoader
	{
		private readonly Options _options = Options.Default;
		private readonly Datamap _db;

		private Block materials;
		private Block tpkblocks;
		private Block cartypeinfos;
		private Block presetrides;
		private Block presetskins;
		private Block collisions;
		private Block dbmodelparts;
		private Block suninfos;
		private Block tracks;
		private Block fngroups;
		private Block strblocks;
		private Block slottypes;

		public DatabaseLoader(Options options, Datamap db)
		{
			this._options = options;
			this._db = db;
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
		}

		public bool Invoke()
		{
			this._db.Buffer = Interop.Decompress(this._db.Buffer);

			using var ms = new MemoryStream(this._db.Buffer);
			using var br = new BinaryReader(ms);

			this.ReadBlockOffsets(br);

			this.ProcessSTRBlocks(br);
			this.ProcessMaterials(br);
			this.ProcessTPKBlocks(br);
			this.ProcessCarTypeInfos(br);
			this.ProcessSlotTypes(br);
			this.ProcessDBModelParts(br);
			this.ProcessTracks(br);
			this.ProcessSunInfos(br);
			this.ProcessCollisions(br);
			this.ProcessPresetRides(br);
			this.ProcessPresetSkins(br);
			this.ProcessFNGroups(br);

			return true;
		}

		private void ReadBlockOffsets(BinaryReader br)
		{
			while (br.BaseStream.Position < br.BaseStream.Length)
			{

				var off = br.BaseStream.Position;
				var id = br.ReadEnum<eBlockID>();
				var size = br.ReadInt32();

				switch (id)
				{
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
					case eBlockID.CarInfoAnimHookup:
						this.slottypes.Offsets.Add(off);
						goto default;

					default:
						br.BaseStream.Position += size;
						break;

				}

			}
		}

		private void ProcessCarTypeInfos(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.CarTypeInfos)) return;

			var manager = new CarTypeInfoManager(this._db);
			manager.Disassemble(br, this.cartypeinfos);

			this._db.Managers.Add(manager);
		}

		private void ProcessCollisions(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.Collisions)) return;

			var manager = new CollisionManager(this._db);
			manager.Disassemble(br, this.collisions);

			this._db.Managers.Add(manager);
		}

		private void ProcessDBModelParts(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.DBModelParts)) return;

			var manager = new DBModelPartManager(this._db);
			manager.Disassemble(br, this.dbmodelparts);

			this._db.Managers.Add(manager);
		}

		private void ProcessFNGroups(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.FNGroups)) return;

			var manager = new FNGroupManager(this._db);
			manager.Disassemble(br, this.fngroups);

			this._db.Managers.Add(manager);
		}

		private void ProcessMaterials(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.Materials)) return;

			var manager = new MaterialManager(this._db);
			manager.Disassemble(br, this.materials);

			this._db.Managers.Add(manager);
		}

		private void ProcessPresetRides(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.PresetRides)) return;

			var manager = new PresetRideManager(this._db);
			manager.Disassemble(br, this.presetrides);

			this._db.Managers.Add(manager);
		}

		private void ProcessPresetSkins(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.PresetSkins)) return;

			var manager = new PresetSkinManager(this._db);
			manager.Disassemble(br, this.presetskins);

			this._db.Managers.Add(manager);
		}

		private void ProcessSlotTypes(BinaryReader br)
		{

		}

		private void ProcessSTRBlocks(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.STRBlocks)) return;

			var manager = new STRBlockManager(this._db);
			manager.Disassemble(br, this.strblocks);

			this._db.Managers.Add(manager);
		}

		private void ProcessSunInfos(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.SunInfos)) return;

			var manager = new SunInfoManager(this._db);
			manager.Disassemble(br, this.suninfos);

			this._db.Managers.Add(manager);
		}

		private void ProcessTPKBlocks(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.TPKBlocks)) return;

			var manager = new TPKBlockManager(this._db);
			manager.Disassemble(br, this.tpkblocks);

			this._db.Managers.Add(manager);
		}

		private void ProcessTracks(BinaryReader br)
		{
			if (!this._options.Flags.HasFlag(eOptFlags.Tracks)) return;

			var manager = new TrackManager(this._db);
			manager.Disassemble(br, this.tracks);

			this._db.Managers.Add(manager);
		}
	}
}
