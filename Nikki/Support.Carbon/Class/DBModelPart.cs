using System;
using System.Collections.Generic;
using System.Text;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Support.Shared.Parts.CarParts;



namespace Nikki.Support.Carbon.Class
{
	/// <summary>
	/// 
	/// </summary>
	public class DBModelPart : Shared.Class.DBModelPart
	{
		private string _collection_name;

		/// <summary>
		/// 
		/// </summary>
		public override string CollectionName
		{
			get => this._collection_name;
			set => this._collection_name = value;
		}

		/// <summary>
		/// 
		/// </summary>
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// 
		/// </summary>
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// 
		/// </summary>
		public override GameINT GameINT => GameINT.MostWanted;

		/// <summary>
		/// 
		/// </summary>
		public override string GameSTR => this.GameINT.ToString();

		/// <summary>
		/// 
		/// </summary>
		public override List<RealCarPart> ModelCarParts { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Database.Carbon Database { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DBModelPart() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="CName"></param>
		/// <param name="db"></param>
		public DBModelPart(string CName, Database.Carbon db)
		{
			this.CollectionName = CName;
			this.Database = db;
			this.ModelCarParts = new List<RealCarPart>();
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="DBModelPart"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}
	}
}
