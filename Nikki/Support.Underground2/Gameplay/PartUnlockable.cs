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
            "Transmission",          // 0x04
            "NOS",                   // 0x05
            "Tires",                 // 0x06
            "Brakes",                // 0x07
            "WeightReduction",       // 0x08
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

					throw new ArgumentNullException("This value cannot be left left empty.");

				}
				if (value.Contains(' '))
				{

					throw new Exception("CollectionName cannot contain whitespace.");

				}
				if (this.Career.GetCollection(value, nameof(this.Career.PartUnlockables)) != null)
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
		/// Visual rating of level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short VisualRating_Level1 { get; set; }

		/// <summary>
		/// Visual rating of level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short VisualRating_Level2 { get; set; }

		/// <summary>
		/// Visual rating of level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short VisualRating_Level3 { get; set; }

		/// <summary>
		/// Price of level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short PartPrice_Level1 { get; set; }

		/// <summary>
		/// Price of level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short PartPrice_Level2 { get; set; }

		/// <summary>
		/// Price of level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short PartPrice_Level3 { get; set; }

		/// <summary>
		/// Unlock method of level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public ePartUnlockReq UnlockMethod_Level1 { get; set; }

		/// <summary>
		/// Unlock method of level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public ePartUnlockReq UnlockMethod_Level2 { get; set; }

		/// <summary>
		/// Unlock method of level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public ePartUnlockReq UnlockMethod_Level3 { get; set; }

		/// <summary>
		/// Shop that unlocks level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string UnlocksInShop_Level1 { get; set; } = String.Empty;

		/// <summary>
		/// Shop that unlocks level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string UnlocksInShop_Level2 { get; set; } = String.Empty;

		/// <summary>
		/// Shop that unlocks level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string UnlocksInShop_Level3 { get; set; } = String.Empty;

		/// <summary>
		/// Required races won to unlock level 1 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte RequiredRacesWon_Level1 { get; set; }

		/// <summary>
		/// Required races won to unlock level 2 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte RequiredRacesWon_Level2 { get; set; }

		/// <summary>
		/// Required races won to unlock level 3 parts.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte RequiredRacesWon_Level3 { get; set; }

		/// <summary>
		/// Stage at which level 1 parts are being unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage_Level1 { get; set; }

		/// <summary>
		/// Stage at which level 2 parts are being unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage_Level2 { get; set; }

		/// <summary>
		/// Stage at which level 3 parts are being unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage_Level3 { get; set; }

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
			this._collection_name = CName;
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

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~PartUnlockable() { }

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
			bw.Write(this.VisualRating_Level1);
			bw.Write(this.PartPrice_Level1);
			bw.WriteEnum(this.UnlockMethod_Level1);
			bw.Write((byte)1);
			bw.Write((short)0);
			
			if (this.UnlockMethod_Level1 == ePartUnlockReq.SPECIFIC_SHOP_FOUND)
			{

				bw.Write(this.UnlocksInShop_Level1.BinHash());

			}
			else
			{

				bw.Write((short)this.RequiredRacesWon_Level1);
				bw.Write((short)this.BelongsToStage_Level1);
			
			}

			// Write level 2 settings
			bw.Write(this.VisualRating_Level2);
			bw.Write(this.PartPrice_Level2);
			bw.WriteEnum(this.UnlockMethod_Level2);
			bw.Write((byte)2);
			bw.Write((short)0);

			if (this.UnlockMethod_Level2 == ePartUnlockReq.SPECIFIC_SHOP_FOUND)
			{

				bw.Write(this.UnlocksInShop_Level2.BinHash());

			}
			else
			{
			
				bw.Write((short)this.RequiredRacesWon_Level2);
				bw.Write((short)this.BelongsToStage_Level2);
			
			}

			// Write level 3 settings
			bw.Write(this.VisualRating_Level3);
			bw.Write(this.PartPrice_Level3);
			bw.WriteEnum(this.UnlockMethod_Level3);
			bw.Write((byte)3);
			bw.Write((short)0);

			if (this.UnlockMethod_Level3 == ePartUnlockReq.SPECIFIC_SHOP_FOUND)
			{

				bw.Write(this.UnlocksInShop_Level3.BinHash());

			}
			else
			{

				bw.Write((short)this.RequiredRacesWon_Level3);
				bw.Write((short)this.BelongsToStage_Level3);
			
			}
		}

		/// <summary>
		/// Disassembles array into <see cref="PartUnlockable"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="PartUnlockable"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			// CollectionName
			this._collection_name = this.GetValidCollectionName(br.ReadInt32());

			// Read level 1 settings
			this.VisualRating_Level1 = br.ReadInt16();
			this.PartPrice_Level1 = br.ReadInt16();
			this.UnlockMethod_Level1 = br.ReadEnum<ePartUnlockReq>();
			br.BaseStream.Position += 3;

			if (this.UnlockMethod_Level1 == ePartUnlockReq.SPECIFIC_SHOP_FOUND)
			{

				this.UnlocksInShop_Level1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			}
			else
			{

				this.RequiredRacesWon_Level1 = br.ReadByte();
				++br.BaseStream.Position;
				this.BelongsToStage_Level1 = br.ReadByte();
				++br.BaseStream.Position;
			
			}

			// Read level 2 settings
			this.VisualRating_Level2 = br.ReadInt16();
			this.PartPrice_Level2 = br.ReadInt16();
			this.UnlockMethod_Level2 = br.ReadEnum<ePartUnlockReq>();
			br.BaseStream.Position += 3;

			if (this.UnlockMethod_Level2 == ePartUnlockReq.SPECIFIC_SHOP_FOUND)
			{

				this.UnlocksInShop_Level2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			}
			else
			{

				this.RequiredRacesWon_Level2 = br.ReadByte();
				++br.BaseStream.Position;
				this.BelongsToStage_Level2 = br.ReadByte();
				++br.BaseStream.Position;
			
			}

			// Read level 3 settings
			this.VisualRating_Level3 = br.ReadInt16();
			this.PartPrice_Level3 = br.ReadInt16();
			this.UnlockMethod_Level3 = br.ReadEnum<ePartUnlockReq>();
			br.BaseStream.Position += 3;

			if (this.UnlockMethod_Level3 == ePartUnlockReq.SPECIFIC_SHOP_FOUND)
			{

				this.UnlocksInShop_Level3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			}
			else
			{

				this.RequiredRacesWon_Level3 = br.ReadByte();
				++br.BaseStream.Position;
				this.BelongsToStage_Level3 = br.ReadByte();
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
	}
}