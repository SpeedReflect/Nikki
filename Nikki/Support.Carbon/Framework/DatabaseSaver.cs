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



		public bool Invoke()
		{
			return this._db.Buffer == null || this._db.Buffer.Length == 0
				? this.WriteUnique()
				: this.WriteBuffered();
		}

		public bool WriteUnique()
		{

			return true;

		}

		public bool WriteBuffered()
		{
			using var ms = new MemoryStream(this._db.Buffer);
			using var br = new BinaryReader(ms);
			using var bw = new BinaryWriter(File.Open(this._options.File, FileMode.Create));



			

			return true;
		}

		private void ProcessMaterials(BinaryWriter bw, BinaryReader br, eBlockID id, int size)
		{
			if (this._options.Flags.HasFlag(eOptFlags.Materials))
			{

				this._db.Materials?.Assemble(bw, this._options.Watermark);
				br.BaseStream.Position += size;

			}
			else
			{

				bw.WriteEnum(id);
				bw.Write(size);
				bw.Write(br.ReadBytes(size));

			}
		}

		private void ProcessTPKBlocks(BinaryWriter bw, BinaryReader br, eBlockID id, int size)
		{
			if (this._options.Flags.HasFlag(eOptFlags.TPKBlocks))
			{

				this._db.TPKBlocks?.Assemble(bw, this._options.Watermark);
				br.BaseStream.Position += size;

			}
			else
			{

				bw.WriteEnum(id);
				bw.Write(size);
				bw.Write(br.ReadBytes(size));

			}
		}


	}
}
