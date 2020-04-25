using System;
using System.Collections.Generic;
using System.Text;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Support.Shared.Parts.CarParts;



namespace Nikki.Support.Carbon.Class
{
	public class DBModelPart : Shared.Class.DBModelPart
	{
		private string _collection_name;

		public override string CollectionName
		{
			get => this._collection_name;
			set => this._collection_name = value;
		}

		public override uint BinKey => this._collection_name.BinHash();

		public override uint VltKey => this._collection_name.VltHash();

		public override GameINT GameINT => GameINT.MostWanted;

		public override string GameSTR => this.GameINT.ToString();

		public override List<RealCarPart> ModelCarParts { get; set; }

		public Database.Carbon Database { get; set; }

		public DBModelPart(string CName, Database.Carbon db)
		{
			this.CollectionName = CName;
			this.Database = db;
			this.ModelCarParts = new List<RealCarPart>();
		}

		/// <summary>
		/// Returns CollectionName, BinKey and <see cref="RealCarPart"/> count as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() =>
			$"CollectionName: {this._collection_name} | BinKey: {this.BinKey:X8} " +
			$"| PartCount: {this.ModelCarParts.Count}";
	}
}
