using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Class;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="WorldShop"/> is a collection of settings related to shops and premises.
	/// </summary>
	public class WorldShop : Collectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Enums

		/// <summary>
		/// Enum of <see cref="WorldShop"/> types.
		/// </summary>
		public enum WorldShopType : byte
		{
			/// <summary>
			/// Garage.
			/// </summary>
			CribGarage = 0,

			/// <summary>
			/// Paint shop.
			/// </summary>
			PaintShop = 1,

			/// <summary>
			/// Parts shop.
			/// </summary>
			PartsShop = 2,

			/// <summary>
			/// Performance shop.
			/// </summary>
			PerfShop = 3,

			/// <summary>
			/// Car lot.
			/// </summary>
			CarLot = 4,

			/// <summary>
			/// Specialties shop.
			/// </summary>
			AudioShop = 5,

			/// <summary>
			/// Uniques shop.
			/// </summary>
			Unique = 6,
		}

		/// <summary>
		/// Enum of unlock conditions for <see cref="WorldShop"/>.
		/// </summary>
		public enum UnlockType : byte
		{
			/// <summary>
			/// Unlocked from the start.
			/// </summary>
			InitiallyUnlocked = 0,

			/// <summary>
			/// Requires specific race completed.
			/// </summary>
			SpecificRaceWon = 1,
			
			/// <summary>
			/// Requires specific number of URL races won.
			/// </summary>
			ReqURLRacesWon = 2,

			/// <summary>
			/// Requires specific number of regular races won.
			/// </summary>
			ReqRegRacesWon = 3,

			/// <summary>
			/// Requires specific number of sponsor races won.
			/// </summary>
			ReqSponRacesWon = 4,
		}

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Underground2;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Underground2.ToString();

		/// <summary>
		/// GCareer to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public GCareer Career { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				if (String.IsNullOrWhiteSpace(value))
				{

					throw new ArgumentNullException("This value cannot be left empty.");

				}
				if (value.Contains(' '))
				{

					throw new Exception("CollectionName cannot contain whitespace.");

				}
				if (this.Career.GetCollection(value, nameof(this.Career.WorldShops)) != null)
				{

					throw new CollectionExistenceException(value);

				}

				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Filename of shop in-game textures.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string ShopFilename { get; set; } = String.Empty;

		/// <summary>
		/// Movie shown when first-time entering shop.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string IntroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Event trigger of this <see cref="WorldShop"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string ShopTrigger { get; set; } = String.Empty;

		/// <summary>
		/// Type of the shop.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public WorldShopType ShopType { get; set; }

		/// <summary>
		/// True if this <see cref="WorldShop"/> is initially hidden on the world map; 
		/// false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean InitiallyHidden { get; set; }

		/// <summary>
		/// Unlock type of this <see cref="WorldShop"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public UnlockType UnlockMethod { get; set; }

		/// <summary>
		/// Stage to which this <see cref="WorldShop"/> belongs to.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// Event name that should be completed to unlock this shop.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string RequiredSpecRaceWon { get; set; } = String.Empty;

		/// <summary>
		/// Number of required races won.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int RequiredRacesWon { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="WorldShop"/>.
		/// </summary>
		public WorldShop() { }

		/// <summary>
		/// Initializes new instance of <see cref="WorldShop"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public WorldShop(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="WorldShop"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public WorldShop(BinaryReader br, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br);
			this.CollectionName.BinHash();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="WorldShop"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="WorldShop"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			// Write strings
			strw.WriteNullTermUTF8(this._collection_name);
			strw.WriteNullTermUTF8(this.ShopFilename);
			strw.WriteNullTermUTF8(this.ShopTrigger);

			// Write CollectionName
			bw.WriteNullTermUTF8(this._collection_name, 0x20);

			// Write IntroMovie
			bw.WriteNullTermUTF8(this.IntroMovie, 0x18);

			// Write Keys
			bw.Write(this.BinKey);
			bw.Write(this.ShopTrigger.BinHash());

			// Write ShopFilename
			bw.WriteNullTermUTF8(this.ShopFilename, 0x10);

			// Write settings
			bw.WriteEnum(this.ShopType);
			bw.WriteEnum(this.InitiallyHidden);
			bw.WriteBytes(0, 0x22);

			switch (this.UnlockMethod)
			{

				case UnlockType.SpecificRaceWon:
					bw.Write(this.RequiredSpecRaceWon.BinHash());
					break;

				case UnlockType.ReqRegRacesWon:
				case UnlockType.ReqURLRacesWon:
				case UnlockType.ReqSponRacesWon:
					bw.Write(this.RequiredRacesWon);
					break;

				default:
					bw.Write((int)0);
					break;

			}

			bw.WriteBytes(0, 0x24);
			bw.WriteEnum(this.UnlockMethod);
			bw.Write(this.BelongsToStage);
			bw.Write((short)0);
		}

		/// <summary>
		/// Disassembles array into <see cref="Sponsor"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Sponsor"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			uint key = 0; // for reading keys and comparison

			// Collection Name
			this._collection_name = br.ReadNullTermUTF8(0x20);

			// Intro Movie
			this.IntroMovie = br.ReadNullTermUTF8(0x18);

			// Shop Trigger
			br.BaseStream.Position += 4;
			key = br.ReadUInt32();
			var guess = $"TRIGGER_{this._collection_name}";
			this.ShopTrigger = key == guess.BinHash() ? guess : key.BinString(LookupReturn.EMPTY);

			// Shop Filename
			this.ShopFilename = br.ReadNullTermUTF8(0x10);

			// Types and Unlocks
			this.ShopType = br.ReadEnum<WorldShopType>();
			this.InitiallyHidden = br.ReadEnum<eBoolean>();

			// Event to complete
			br.BaseStream.Position += 0x22;
			var temp = br.ReadUInt32();

			// Last settings
			br.BaseStream.Position += 0x24;
			this.UnlockMethod = br.ReadEnum<UnlockType>();
			this.BelongsToStage = br.ReadByte();
			br.BaseStream.Position += 2;

			switch (this.UnlockMethod)
			{

				case UnlockType.SpecificRaceWon:
					this.RequiredSpecRaceWon = temp.BinString(LookupReturn.EMPTY);
					break;

				case UnlockType.ReqRegRacesWon:
				case UnlockType.ReqURLRacesWon:
				case UnlockType.ReqSponRacesWon:
					this.RequiredRacesWon = (int)temp;
					break;

				default:
					break;

			}
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new WorldShop(CName, this.Career);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="GCareerRace"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this._collection_name);
			bw.WriteNullTermUTF8(this.IntroMovie);
			bw.WriteNullTermUTF8(this.ShopTrigger);
			bw.WriteNullTermUTF8(this.ShopFilename);
			bw.WriteEnum(this.ShopType);
			bw.WriteEnum(this.InitiallyHidden);
			bw.WriteNullTermUTF8(this.RequiredSpecRaceWon);
			bw.Write(this.RequiredRacesWon);
			bw.WriteEnum(this.UnlockMethod);
			bw.Write(this.BelongsToStage);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this._collection_name = br.ReadNullTermUTF8();
			this.IntroMovie = br.ReadNullTermUTF8();
			this.ShopTrigger = br.ReadNullTermUTF8();
			this.ShopFilename = br.ReadNullTermUTF8();
			this.ShopType = br.ReadEnum<WorldShopType>();
			this.InitiallyHidden = br.ReadEnum<eBoolean>();
			this.RequiredSpecRaceWon = br.ReadNullTermUTF8();
			this.RequiredRacesWon = br.ReadInt32();
			this.UnlockMethod = br.ReadEnum<UnlockType>();
			this.BelongsToStage = br.ReadByte();
		}

		#endregion
	}
}