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

		public bool Invoke()
		{
			if (!File.Exists(this._options.File)) return false;
			this._db.Buffer = File.ReadAllBytes(this._options.File);
			this._db.Buffer = Interop.Decompress(this._db.Buffer);

			using var ms = new MemoryStream(this._db.Buffer);
			using var br = new BinaryReader(ms);

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
			this._db.CarSlotInfos.Disassemble(br, this.slottypes);
			this.ProcessCarAnimations(br);

			return true;
		}

		private void ReadBlockOffsets(BinaryReader br)
		{
			while (br.BaseStream.Position < br.BaseStream.Length)
			{

				var off = br.BaseStream.Position;
				var id = br.ReadEnum<eBlockID>();
				var size = br.ReadInt32();

				System.Console.WriteLine($"0x{off:X8} ---> {id}");

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
	}
}
