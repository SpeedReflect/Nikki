using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Class;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="PartUnlockable"/> is a collection of settings related to visual unlockable parts.
	/// </summary>
	public class PartUnlockable : Collectable
	{
		#region Fields

		private string _collection_name;
		private static readonly string[] PartUnlockablesList = new string[0x56]
		{
			"Engine",                // 0x01
            "ECU",                   // 0x02
            "Suspension",            // 0x03
            "NOS",                   // 0x04
            "Transmission",          // 0x05
            "WeightReduction",       // 0x06
            "Tires",                 // 0x07
            "Brakes",                // 0x08
            "Turbo",                 // 0x09
            "Aerodynamics",          // 0x0A
            "Hoods",                 // 0x0B
            "Spoilers",              // 0x0C
            "Wheels",                // 0x0D
            "Brakelights",           // 0x0E
            "Headlights",            // 0x0F
            "AutosculptSkirt",       // 0x10
            "AutosculptRearBumper",  // 0x11
            "AutosculptFrontBumper", // 0x12
            "WingMirrors",           // 0x13
            "RoofScoops",            // 0x14
            "Exhausts",              // 0x15
            "AftermarketBodykit",    // 0x16
            "Gauges",                // 0x17
            "HeadlightBulb",         // 0x18
            "NeonBody",              // 0x19
            "NeonEngine",            // 0x1A
            "NeonCabin",             // 0x1B
            "NeonTrunk",             // 0x1C
            "NOSPurge",              // 0x1D
            "TrunkAudio",            // 0x1E
            "AudioComponents",       // 0x1F
            "WindshieldTint",        // 0x20
            "Spinners",              // 0x21
            "SplitHoods",            // 0x22
            "DoorOpenings",          // 0x23
            "Hydraulics",            // 0x24
            "BodyPaintGloss",        // 0x25
            "BodyPaintMetallic",     // 0x26
            "BodyPaintPearl",        // 0x27
            "EnginePaint",           // 0x28
            "VinylsTear",            // 0x29
            "VinylsTearSet",         // 0x2A
            "VinylsStripe",          // 0x2B
            "VinylsStripeSet",       // 0x2C
            "VinylsSplash",          // 0x2D
            "VinylsSplashSet",       // 0x2E
            "VinylsModern",          // 0x2F
            "VinylsModernSet",       // 0x30
            "VinylsFlames",          // 0x31
            "VinylsFlamesSet",       // 0x32
            "VinylsLightning",       // 0x33
            "VinylsLightningSet",    // 0x34
            "VinylsRacing",          // 0x35
            "VinylsRacingSet",       // 0x36
            "VinylsTopLayer",        // 0x37
            "VinylsFlag",            // 0x38
            "VinylsFlagSet",         // 0x39
            "VinylsTribal",          // 0x3A
            "VinylsTribalSet",       // 0x3B
            "VinylsAftermarket",     // 0x3C
            "VinylsSponsor",         // 0x3D
            "VinylsUnique",          // 0x3E
            "VinylsArtFactory",      // 0x3F
            "VinylsBody",            // 0x40
            "VinylsWild",            // 0x41
            "VinylsWildSet",         // 0x42
            "VinylsHoods",           // 0x43
            "VinylsContest",         // 0x44
            "WingMirrorsCarbon",     // 0x45
            "RoofScoopsCarbon",      // 0x46
            "UnknownPart47",         // 0x47
            "UnknownPart48",         // 0x48
            "UnknownPart49",         // 0x49
            "SpoilersCarbon",        // 0x4A
            "HoodsCarbon",           // 0x4B
            "Decals",                // 0x4C
            "RimPaint",              // 0x4D
            "ExhaustPaint",          // 0x4E
            "BrakePaint",            // 0x4F
            "SpoilerPaint",          // 0x50
            "SpinnerPaint",          // 0x51
            "AudioPaint",            // 0x52
            "RoofScoopPaint",        // 0x53
            "WingMirrorPaint",       // 0x54
            "KitCarbon",             // 0x55
            "TrunkCarbon",           // 0x56
        };

		[MemoryCastable()]
		private int _unlock_index = -1;
		
		#endregion

		#region Enums

		/// <summary>
		/// Enum of <see cref="PartUnlockable"/> requirements.
		/// </summary>
		public enum PartUnlockReq : byte
		{
			/// <summary>
			/// No requirements.
			/// </summary>
			NoRequirements = 0,

			/// <summary>
			/// Need to find specific shop.
			/// </summary>
			SpecShopFound = 1,

			/// <summary>
			/// Need to win specific amount of Regular races.
			/// </summary>
			ReqRegRacesWon = 2,

			/// <summary>
			/// Need to win specific amount of URL races.
			/// </summary>
			ReqURLRacesWon = 3,

			/// <summary>
			/// Need to win specific amount of Sponsor races.
			/// </summary>
			ReqSponRacesWon = 4,

			/// <summary>
			/// Initially unlocked.
			/// </summary>
			InitiallyUnlocked = 6,
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
		[ReadOnly(true)]
		[Category("Main")]
		public override string CollectionName
		{
			get => this._collection_name;
			set => throw new Exception($"Collection names of PartUnlockables cannot be changed");
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
		/// Unlockable index of this <see cref="PartUnlockable"/>.
		/// </summary>
		[Category("Primary")]
		public int UnlockableIndex => this._unlock_index;

		/// <summary>
		/// Visual rating of level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level1")]
		public short VisualRatingLevel1 { get; set; }

		/// <summary>
		/// Visual rating of level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level2")]
		public short VisualRatingLevel2 { get; set; }

		/// <summary>
		/// Visual rating of level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level3")]
		public short VisualRatingLevel3 { get; set; }

		/// <summary>
		/// Price of level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level1")]
		public short PartPriceLevel1 { get; set; }

		/// <summary>
		/// Price of level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level2")]
		public short PartPriceLevel2 { get; set; }

		/// <summary>
		/// Price of level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level3")]
		public short PartPriceLevel3 { get; set; }

		/// <summary>
		/// Unlock method of level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level1")]
		public PartUnlockReq UnlockMethodLevel1 { get; set; }

		/// <summary>
		/// Unlock method of level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level2")]
		public PartUnlockReq UnlockMethodLevel2 { get; set; }

		/// <summary>
		/// Unlock method of level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level3")]
		public PartUnlockReq UnlockMethodLevel3 { get; set; }

		/// <summary>
		/// Shop that unlocks level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level1")]
		public string UnlocksInShopLevel1 { get; set; } = String.Empty;

		/// <summary>
		/// Shop that unlocks level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level2")]
		public string UnlocksInShopLevel2 { get; set; } = String.Empty;

		/// <summary>
		/// Shop that unlocks level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level3")]
		public string UnlocksInShopLevel3 { get; set; } = String.Empty;

		/// <summary>
		/// Required races won to unlock level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level1")]
		public byte RequiredRacesWonLevel1 { get; set; }

		/// <summary>
		/// Required races won to unlock level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level2")]
		public byte RequiredRacesWonLevel2 { get; set; }

		/// <summary>
		/// Required races won to unlock level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level3")]
		public byte RequiredRacesWonLevel3 { get; set; }

		/// <summary>
		/// Stage at which level 1 parts are being unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level1")]
		public byte BelongsToStageLevel1 { get; set; }

		/// <summary>
		/// Stage at which level 2 parts are being unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level2")]
		public byte BelongsToStageLevel2 { get; set; }

		/// <summary>
		/// Stage at which level 3 parts are being unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Level3")]
		public byte BelongsToStageLevel3 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="PartUnlockable"/>.
		/// </summary>
		public PartUnlockable() { }

		/// <summary>
		/// Initializes new instance of <see cref="PartUnlockable"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public PartUnlockable(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="PartUnlockable"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public PartUnlockable(BinaryReader br, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="PartUnlockable"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PartUnlockable"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			// Write index
			bw.Write(this.GetValidCollectionIndex());

			// Write level 1 settings
			bw.Write(this.VisualRatingLevel1);
			bw.Write(this.PartPriceLevel1);
			bw.WriteEnum(this.UnlockMethodLevel1);
			bw.Write((byte)1);
			bw.Write((short)0);
			
			if (this.UnlockMethodLevel1 == PartUnlockReq.SpecShopFound)
			{

				bw.Write(this.UnlocksInShopLevel1.BinHash());

			}
			else
			{

				bw.Write((short)this.RequiredRacesWonLevel1);
				bw.Write((short)this.BelongsToStageLevel1);
			
			}

			// Write level 2 settings
			bw.Write(this.VisualRatingLevel2);
			bw.Write(this.PartPriceLevel2);
			bw.WriteEnum(this.UnlockMethodLevel2);
			bw.Write((byte)2);
			bw.Write((short)0);

			if (this.UnlockMethodLevel2 == PartUnlockReq.SpecShopFound)
			{

				bw.Write(this.UnlocksInShopLevel2.BinHash());

			}
			else
			{
			
				bw.Write((short)this.RequiredRacesWonLevel2);
				bw.Write((short)this.BelongsToStageLevel2);
			
			}

			// Write level 3 settings
			bw.Write(this.VisualRatingLevel3);
			bw.Write(this.PartPriceLevel3);
			bw.WriteEnum(this.UnlockMethodLevel3);
			bw.Write((byte)3);
			bw.Write((short)0);

			if (this.UnlockMethodLevel3 == PartUnlockReq.SpecShopFound)
			{

				bw.Write(this.UnlocksInShopLevel3.BinHash());

			}
			else
			{

				bw.Write((short)this.RequiredRacesWonLevel3);
				bw.Write((short)this.BelongsToStageLevel3);
			
			}
		}

		/// <summary>
		/// Disassembles array into <see cref="PartUnlockable"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="PartUnlockable"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			// CollectionName
			this._unlock_index = br.ReadInt32();
			this._collection_name = this.GetValidCollectionName(this._unlock_index);

			// Read level 1 settings
			this.VisualRatingLevel1 = br.ReadInt16();
			this.PartPriceLevel1 = br.ReadInt16();
			this.UnlockMethodLevel1 = br.ReadEnum<PartUnlockReq>();
			br.BaseStream.Position += 3;

			if (this.UnlockMethodLevel1 == PartUnlockReq.SpecShopFound)
			{

				this.UnlocksInShopLevel1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);

			}
			else
			{

				this.RequiredRacesWonLevel1 = br.ReadByte();
				++br.BaseStream.Position;
				this.BelongsToStageLevel1 = br.ReadByte();
				++br.BaseStream.Position;
			
			}

			// Read level 2 settings
			this.VisualRatingLevel2 = br.ReadInt16();
			this.PartPriceLevel2 = br.ReadInt16();
			this.UnlockMethodLevel2 = br.ReadEnum<PartUnlockReq>();
			br.BaseStream.Position += 3;

			if (this.UnlockMethodLevel2 == PartUnlockReq.SpecShopFound)
			{

				this.UnlocksInShopLevel2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);

			}
			else
			{

				this.RequiredRacesWonLevel2 = br.ReadByte();
				++br.BaseStream.Position;
				this.BelongsToStageLevel2 = br.ReadByte();
				++br.BaseStream.Position;
			
			}

			// Read level 3 settings
			this.VisualRatingLevel3 = br.ReadInt16();
			this.PartPriceLevel3 = br.ReadInt16();
			this.UnlockMethodLevel3 = br.ReadEnum<PartUnlockReq>();
			br.BaseStream.Position += 3;

			if (this.UnlockMethodLevel3 == PartUnlockReq.SpecShopFound)
			{

				this.UnlocksInShopLevel3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);

			}
			else
			{

				this.RequiredRacesWonLevel3 = br.ReadByte();
				++br.BaseStream.Position;
				this.BelongsToStageLevel3 = br.ReadByte();
				++br.BaseStream.Position;
			
			}
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new PartUnlockable(CName, this.Career);
			base.MemoryCast(this, result);
			return result;
		}

		private string GetValidCollectionName(int index)
		{
			return index > PartUnlockablesList.Length
				? $"UnknownPart{index}"
				: PartUnlockablesList[index - 1];
		}

		private int GetValidCollectionIndex()
		{
			for (int i = 0; i < PartUnlockablesList.Length; ++i)
			{

				if (this._collection_name == PartUnlockablesList[i]) return i + 1;

			}

			return -1;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="BankTrigger"/> 
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
			// Write index
			bw.WriteNullTermUTF8(this._collection_name);
			bw.Write(this._unlock_index);

			// Write level 1 settings
			bw.Write(this.VisualRatingLevel1);
			bw.Write(this.PartPriceLevel1);
			bw.WriteEnum(this.UnlockMethodLevel1);

			if (this.UnlockMethodLevel1 == PartUnlockReq.SpecShopFound)
			{

				bw.WriteNullTermUTF8(this.UnlocksInShopLevel1);

			}
			else
			{

				bw.Write(this.RequiredRacesWonLevel1);
				bw.Write(this.BelongsToStageLevel1);

			}

			// Write level 2 settings
			bw.Write(this.VisualRatingLevel2);
			bw.Write(this.PartPriceLevel2);
			bw.WriteEnum(this.UnlockMethodLevel2);

			if (this.UnlockMethodLevel2 == PartUnlockReq.SpecShopFound)
			{

				bw.WriteNullTermUTF8(this.UnlocksInShopLevel2);

			}
			else
			{

				bw.Write(this.RequiredRacesWonLevel2);
				bw.Write(this.BelongsToStageLevel2);

			}

			// Write level 3 settings
			bw.Write(this.VisualRatingLevel3);
			bw.Write(this.PartPriceLevel3);
			bw.WriteEnum(this.UnlockMethodLevel3);

			if (this.UnlockMethodLevel3 == PartUnlockReq.SpecShopFound)
			{

				bw.WriteNullTermUTF8(this.UnlocksInShopLevel3);

			}
			else
			{

				bw.Write(this.RequiredRacesWonLevel3);
				bw.Write(this.BelongsToStageLevel3);

			}
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			// CollectionName
			this._collection_name = br.ReadNullTermUTF8();
			this._unlock_index = br.ReadInt32();

			// Read level 1 settings
			this.VisualRatingLevel1 = br.ReadInt16();
			this.PartPriceLevel1 = br.ReadInt16();
			this.UnlockMethodLevel1 = br.ReadEnum<PartUnlockReq>();

			if (this.UnlockMethodLevel1 == PartUnlockReq.SpecShopFound)
			{

				this.UnlocksInShopLevel1 = br.ReadNullTermUTF8();

			}
			else
			{

				this.RequiredRacesWonLevel1 = br.ReadByte();
				this.BelongsToStageLevel1 = br.ReadByte();

			}

			// Read level 2 settings
			this.VisualRatingLevel2 = br.ReadInt16();
			this.PartPriceLevel2 = br.ReadInt16();
			this.UnlockMethodLevel2 = br.ReadEnum<PartUnlockReq>();

			if (this.UnlockMethodLevel2 == PartUnlockReq.SpecShopFound)
			{

				this.UnlocksInShopLevel2 = br.ReadNullTermUTF8();

			}
			else
			{

				this.RequiredRacesWonLevel2 = br.ReadByte();
				this.BelongsToStageLevel2 = br.ReadByte();

			}

			// Read level 3 settings
			this.VisualRatingLevel3 = br.ReadInt16();
			this.PartPriceLevel3 = br.ReadInt16();
			this.UnlockMethodLevel3 = br.ReadEnum<PartUnlockReq>();

			if (this.UnlockMethodLevel3 == PartUnlockReq.SpecShopFound)
			{

				this.UnlocksInShopLevel3 = br.ReadNullTermUTF8();

			}
			else
			{

				this.RequiredRacesWonLevel3 = br.ReadByte();
				this.BelongsToStageLevel3 = br.ReadByte();

			}
		}

		#endregion
	}
}