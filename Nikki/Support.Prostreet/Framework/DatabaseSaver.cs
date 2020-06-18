using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;
using CoreExtensions.Management;



namespace Nikki.Support.Prostreet.Framework
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
			return File.Exists(this._options.File)
				? this.WriteBuffered()
				: this.WriteUnique();
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
			this._db.FNGroups.Assemble(bw, this._options.Watermark);

			return true;
		}

		public bool WriteBuffered()
		{
			var buffer = File.ReadAllBytes(this._options.File);
			buffer = Interop.Decompress(buffer);

			using var ms = new MemoryStream(buffer);
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
			this._db.FNGroups.Assemble(bw, this._options.Watermark);

			this.WriteBlockOffsets(bw, br);
			buffer = null;
			ForcedX.GCCollect();
			return true;
		}

		private void WriteBlockOffsets(BinaryWriter bw, BinaryReader br)
		{
			while (br.BaseStream.Position < br.BaseStream.Length)
			{

				var id = br.ReadEnum<eBlockID>();
				var size = br.ReadInt32();
				var next = eBlockID.Padding;

				if (br.BaseStream.Position + 4 < br.BaseStream.Length)
				{

					next = br.ReadEnum<eBlockID>();
					br.BaseStream.Position -= 4;

				}
				else next = 0;

				switch (id)
				{
					case eBlockID.Padding:
						if (next != eBlockID.Nikki) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.Nikki:
					case eBlockID.Tracks:
					case eBlockID.SunInfos:
					case eBlockID.FEngFiles:
					case eBlockID.Materials:
					case eBlockID.SlotTypes:
					case eBlockID.STRBlocks:
					case eBlockID.TPKBlocks:
					case eBlockID.DBCarParts:
					case eBlockID.DBCarBounds:
					case eBlockID.FNGCompress:
					case eBlockID.TPKSettings:
					case eBlockID.CarTypeInfos:
					case eBlockID.CarInfoAnimHideup:
					case eBlockID.CarInfoAnimHookup:
						br.BaseStream.Position += size;
						break;

					default:
						if (Map.BlockToAlignment.TryGetValue(id, out var align))
						{

							BinarySaver.GeneratePadding(bw, this._options.Watermark, align);

						}

						bw.WriteEnum(id);
						bw.Write(size);
						bw.Write(br.ReadBytes(size));
						break;

				}
			
			}
		}
	}
}
