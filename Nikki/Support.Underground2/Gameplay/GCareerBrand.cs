using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="GCareerBrand"/> is a collection of settings related to sponsor brands.
	/// </summary>
	public class GCareerBrand : ACollectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground2;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground2.ToString();

		/// <summary>
		/// Database to which the class belongs to.
		/// </summary>
		public Database.Underground2 Database { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("This value cannot be left left empty.");
				if (value.Contains(" "))
					throw new Exception("CollectionName cannot contain whitespace.");
				if (value.Length > 0x1F)
					throw new ArgumentLengthException("Length of the value should not exceed 31 character.");
				if (this.Database.GCareerBrands.FindCollection(value) != null)
					throw new CollectionExistenceException();
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		public uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		public uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// In-Game name of this <see cref="GCareerBrand"/>.
		/// </summary>
		[AccessModifiable()]
		public string IngameBrandName { get; set; } = String.Empty;

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="GCareerBrand"/>.
		/// </summary>
		public GCareerBrand() { }

		/// <summary>
		/// Initializes new instance of <see cref="GCareerBrand"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public GCareerBrand(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCareerBrand"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public GCareerBrand(BinaryReader br, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~GCareerBrand() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="GCareerBrand"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="GCareerBrand"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			strw.WriteNullTermUTF8(this._collection_name);

			bw.WriteNullTermUTF8(this._collection_name, 0x20);
			bw.WriteNullTermUTF8(this.IngameBrandName, 0x20);
			bw.Write(this.BinKey);
		}

		/// <summary>
		/// Disassembles array into <see cref="GCareerStage"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCareerBrand"/> with.</param
		public void Disassemble(BinaryReader br)
		{
			// CollectionName
			this._collection_name = br.ReadNullTermUTF8(0x20);

			// Ingame Brand Name
			this.IngameBrandName = br.ReadNullTermUTF8(0x20);

			br.BaseStream.Position += 4;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns
		public override ACollectable MemoryCast(string CName)
		{
			var result = new GCareerBrand(CName, this.Database)
			{
				IngameBrandName = this.IngameBrandName
			};

			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="GCareerBrand"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}

		#endregion
	}
}