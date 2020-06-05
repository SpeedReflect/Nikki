using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Database;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Interface;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	internal class DatabaseSaver : IInvokable
	{
		private readonly Options _options = Options.Default;
		private readonly FileBase _db;
		private List<Block> _blocks;



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

			this.ProcessMaterials(bw);


			

			return true;
		}

		private void ProcessMaterials(BinaryWriter bw)
		{



		}



	}
}
