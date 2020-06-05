using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Interface;
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

			this.ProcessSTRBlocks(bw);
			this.ProcessMaterials(bw);
			this.ProcessTPKBlocks(bw);
			this.ProcessCarTypeInfos(bw);
			this.ProcessSlotTypes(bw);
			this.ProcessDBModelParts(bw);
			this.ProcessTracks(bw);
			this.ProcessSunInfos(bw);
			this.ProcessCollisions(bw);
			this.ProcessPresetRides(bw);
			this.ProcessPresetSkins(bw);
			this.ProcessFNGroups(bw);

			return true;
		}

		public bool WriteBuffered()
		{
			using var ms = new MemoryStream(this._db.Buffer);
			using var br = new BinaryReader(ms);
			using var bw = new BinaryWriter(File.Open(this._options.File, FileMode.Create));

			this.ProcessSTRBlocks(bw);
			this.ProcessMaterials(bw);
			this.ProcessTPKBlocks(bw);
			this.ProcessCarTypeInfos(bw);
			this.ProcessSlotTypes(bw);
			this.ProcessDBModelParts(bw);
			this.ProcessTracks(bw);
			this.ProcessSunInfos(bw);
			this.ProcessCollisions(bw);
			this.ProcessPresetRides(bw);
			this.ProcessPresetSkins(bw);
			this.ProcessFNGroups(bw);

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

					case eBlockID.STRBlocks:
						if (!this._options.Flags.HasFlag(eOptFlags.STRBlocks)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.Materials:
						if (!this._options.Flags.HasFlag(eOptFlags.Materials)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.TPKBlocks:
						if (!this._options.Flags.HasFlag(eOptFlags.TPKBlocks)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.CarTypeInfos:
						if (!this._options.Flags.HasFlag(eOptFlags.CarTypeInfos)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.SlotTypes:
					case eBlockID.CarInfoAnimHideup:
					case eBlockID.CarInfoAnimHookup:
						if (!this._options.Flags.HasFlag(eOptFlags.SlotTypes)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.DBCarParts:
						if (!this._options.Flags.HasFlag(eOptFlags.DBModelParts)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.Tracks:
						if (!this._options.Flags.HasFlag(eOptFlags.Tracks)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.SunInfos:
						if (!this._options.Flags.HasFlag(eOptFlags.SunInfos)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.DBCarBounds:
						if (!this._options.Flags.HasFlag(eOptFlags.Collisions)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.PresetRides:
						if (!this._options.Flags.HasFlag(eOptFlags.PresetRides)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.PresetSkins:
						if (!this._options.Flags.HasFlag(eOptFlags.PresetSkins)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.FEngFiles:
					case eBlockID.FNGCompress:
						if (!this._options.Flags.HasFlag(eOptFlags.FNGroups)) goto default;
						else goto case eBlockID.Nikki;

					case eBlockID.Nikki:
						br.BaseStream.Position += size;
						break;

					default:
						bw.Write(br.ReadBytes(size));
						break;

				}
			
			}
		}

		private void ProcessCarTypeInfos(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.CarTypeInfos))
			{

				this._db.CarTypeInfos?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessCollisions(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.Collisions))
			{

				this._db.Collisions?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessDBModelParts(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.DBModelParts))
			{

				this._db.DBModelParts?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessFNGroups(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.FNGroups))
			{

				this._db.FNGroups?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessMaterials(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.Materials))
			{

				this._db.Materials?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessPresetRides(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.PresetRides))
			{

				this._db.PresetRides?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessPresetSkins(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.PresetSkins))
			{

				this._db.PresetSkins?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessSlotTypes(BinaryWriter bw)
		{

		}

		private void ProcessSTRBlocks(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.STRBlocks))
			{

				this._db.STRBlocks?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessSunInfos(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.SunInfos))
			{

				this._db.SunInfos?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessTPKBlocks(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.TPKBlocks))
			{

				this._db.TPKBlocks?.Assemble(bw, this._options.Watermark);

			}
		}

		private void ProcessTracks(BinaryWriter bw)
		{
			if (this._options.Flags.HasFlag(eOptFlags.Tracks))
			{

				this._db.Tracks?.Assemble(bw, this._options.Watermark);

			}
		}
	}
}
