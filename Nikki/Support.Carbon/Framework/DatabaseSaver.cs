using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	internal class DatabaseSaver
	{
		private readonly Options _options = Options.Default;
		private readonly Datamap _db;

		public DatabaseSaver(Options options, Datamap db)
		{
			this._options = options;
			this._db = db;
		}

		public bool Invoke()
		{
			return this._db.Buffer == null || this._db.Buffer.Length == 0
				? this.WriteUnique()
				: this.WriteBuffered();
		}

		public bool WriteUnique()
		{
			using var bw = new BinaryWriter(File.Open(this._options.File, FileMode.Create));

			this._db.STRBlocks.Assemble(bw, this._options.Watermark);
			this._db.Materials.Assemble(bw, this._options.Watermark);
			this._db.TPKBlocks.Assemble(bw, this._options.Watermark);
			this._db.CarTypeInfos.Assemble(bw, this._options.Watermark);
			this._db.SlotTypes.Assemble(bw, this._options.Watermark);
			this._db.DBModelParts.Assemble(bw, this._options.Watermark);
			this._db.Tracks.Assemble(bw, this._options.Watermark);
			this._db.SunInfos.Assemble(bw, this._options.Watermark);
			this._db.Collisions.Assemble(bw, this._options.Watermark);
			this._db.PresetRides.Assemble(bw, this._options.Watermark);
			this._db.PresetSkins.Assemble(bw, this._options.Watermark);
			this._db.FNGroups.Assemble(bw, this._options.Watermark);

			return true;
		}

		public bool WriteBuffered()
		{
			using var ms = new MemoryStream(this._db.Buffer);
			using var br = new BinaryReader(ms);
			using var bw = new BinaryWriter(File.Open(this._options.File, FileMode.Create));

			this._db.STRBlocks.Assemble(bw, this._options.Watermark);
			this._db.Materials.Assemble(bw, this._options.Watermark);
			this._db.TPKBlocks.Assemble(bw, this._options.Watermark);
			this._db.CarTypeInfos.Assemble(bw, this._options.Watermark);
			this._db.SlotTypes.Assemble(bw, this._options.Watermark);
			this._db.DBModelParts.Assemble(bw, this._options.Watermark);
			this._db.Tracks.Assemble(bw, this._options.Watermark);
			this._db.SunInfos.Assemble(bw, this._options.Watermark);
			this._db.Collisions.Assemble(bw, this._options.Watermark);
			this._db.PresetRides.Assemble(bw, this._options.Watermark);
			this._db.PresetSkins.Assemble(bw, this._options.Watermark);
			this._db.FNGroups.Assemble(bw, this._options.Watermark);

			this.WriteBlockOffsets(bw, br);
			return true;
		}

		private void WriteBlockOffsets(BinaryWriter bw, BinaryReader br)
		{
			while (br.BaseStream.Position < br.BaseStream.Length)
			{

				var id = br.ReadEnum<eBlockID>();
				var size = br.ReadInt32();
				var next = eBlockID.Padding;

				if (br.BaseStream.Position < br.BaseStream.Length)
				{

					next = br.ReadEnum<eBlockID>();
					br.BaseStream.Position -= 4;

				}

				switch (id)
				{
					case eBlockID.Padding:
						if (next != eBlockID.Nikki) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.Nikki:
					case eBlockID.STRBlocks:
					case eBlockID.Materials:
					case eBlockID.CarTypeInfos:
					case eBlockID.SlotTypes:
					case eBlockID.CarInfoAnimHideup:
					case eBlockID.CarInfoAnimHookup:
					case eBlockID.DBCarParts:
					case eBlockID.Tracks:
					case eBlockID.SunInfos:
					case eBlockID.DBCarBounds:
					case eBlockID.PresetRides:
					case eBlockID.PresetSkins:
					case eBlockID.FEngFiles:
					case eBlockID.FNGCompress:
					case eBlockID.TPKBlocks:
					case eBlockID.TPKSettings:
						br.BaseStream.Position += size;
						break;

					case eBlockID.PCAWater0:
					case eBlockID.DDSTexture:
					case eBlockID.Geometry:
						BinarySaver.GeneratePadding(bw, this._options.Watermark,
							new Alignment(0x80, Alignment.eAlignType.Modular));
						goto default;

					case eBlockID.ColorCube:
					case eBlockID.LimitsTable:
						BinarySaver.GeneratePadding(bw, this._options.Watermark,
							new Alignment(0x10, Alignment.eAlignType.Modular));
						goto default;

					case eBlockID.EventSequence:
						BinarySaver.GeneratePadding(bw, this._options.Watermark,
							new Alignment(0x8, Alignment.eAlignType.Actual));
						goto default;

					default:
						bw.WriteEnum(id);
						bw.Write(size);
						bw.Write(br.ReadBytes(size));
						break;

				}
			
			}
		}
	}
}
