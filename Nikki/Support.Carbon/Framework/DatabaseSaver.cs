using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;
using CoreExtensions.Management;
using System.Diagnostics;

namespace Nikki.Support.Carbon.Framework
{
	internal class DatabaseSaver
	{
		private readonly Options _options = Options.Default;
		private readonly Datamap _db;
		private readonly Logger _logger;

		public DatabaseSaver(Options options, Datamap db)
		{
			this._options = options;
			this._db = db;
			this._logger = new Logger("MainLog.txt", "Nikki.dll : DatabaseLoader", true);
		}

		public void Invoke()
		{
			var info = new FileInfo(this._options.File);
			
			if (!info.Exists)
			{

				this.WriteUnique();
				return;

			}

			var comp = this.NeedsDecompression();
			if (!comp && info.Length > (1 << 26)) this.WriteFromStream();
			else this.WriteFromBuffer(comp);

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

		private void WriteUnique()
		{
			using var bw = new BinaryWriter(File.Open(this._options.File, FileMode.Create));

			try
			{

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

			}
			catch (Exception e)
			{

				this._logger.WriteException(e, bw.BaseStream);

			}
		}

		private void WriteFromStream()
		{
			var directory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			var filename = Path.GetFileName(this._options.File);
			filename = Path.Combine(directory, filename);

			using (var br = new BinaryReader(File.Open(this._options.File, FileMode.Open, FileAccess.Read)))
			using (var bw = new BinaryWriter(File.Open(filename, FileMode.Create, FileAccess.Write)))
			{

				this.Assemble(bw, br);

			}

			File.Move(filename, this._options.File, true);
			File.Delete(filename);
		}

		private void WriteFromBuffer(bool compressed)
		{
			var buffer = File.ReadAllBytes(this._options.File);
			if (compressed) buffer = Interop.Decompress(buffer);
			using var ms = new MemoryStream(buffer);
			using var br = new BinaryReader(ms);
			using var bw = new BinaryWriter(File.Open(this._options.File, FileMode.Create));
			this.Assemble(bw, br);
		}

		private void Assemble(BinaryWriter bw, BinaryReader br)
		{
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
					case eBlockID.PresetRides:
					case eBlockID.PresetSkins:
					case eBlockID.TPKSettings:
					case eBlockID.CarTypeInfos:
					case eBlockID.CarInfoAnimHideup:
					case eBlockID.CarInfoAnimHookup:
						br.BaseStream.Position += size;
						break;

					case eBlockID.Geometry:
						var position = br.BaseStream.Position - 8;
						BinarySaver.GenerateGeometryPadding(bw, this._options.Watermark, position);
						bw.WriteEnum(id);
						bw.Write(size);
						bw.Write(br.ReadBytes(size));
						break;

					default:
						if (Map.BlockToAlignment.TryGetValue(id, out var align))
						{

							BinarySaver.GeneratePadding(bw, this._options.Watermark, align);

						}
						else
						{

							BinarySaver.GeneratePadding(bw, this._options.Watermark, Alignment.Default);
						
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
