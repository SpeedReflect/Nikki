using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Parts.GameParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="GCareerRace"/> is a collection of settings related to career races and events.
	/// </summary>
	public class GCareerRace : Collectable
	{
		#region Fields

		private string _collection_name;

		[MemoryCastable()]
		private byte _padding0 = 0;

		[MemoryCastable()]
		private byte _padding1 = 0;

		[MemoryCastable()]
		private int _padding2 = 0;

		[MemoryCastable()]
		private int _padding3 = 0;

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
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public GCareerRaceManager Manager { get; set; }

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
				this.Manager?.CreationCheck(value);
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
		/// Engage trigger of this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string EventTrigger { get; set; } = String.Empty;

		/// <summary>
		/// Unlock method.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eUnlockCondition UnlockMethod { get; set; }

		/// <summary>
		/// True if the race is an SUV race; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean IsSUVRace { get; set; }

		/// <summary>
		/// Event type.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eEventBehaviorType EventBehaviorType { get; set; }

		/// <summary>
		/// Required race completed in order to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string RequiredSpecificRaceWon { get; set; } = String.Empty;

		/// <summary>
		/// Required URL race completer in order to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte RequiredSpecificURLWon { get; set; }

		/// <summary>
		/// Required sponsor chosen in order to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte SponsorChosenToUnlock { get; set; }

		/// <summary>
		/// Required number of races won to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte RequiredRacesWon { get; set; }

		/// <summary>
		/// Required number of URL won to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte RequiredURLWon { get; set; }

		/// <summary>
		/// Track number for stage 1.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE1 { get; }

		/// <summary>
		/// Track number for stage 2.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE2 { get; }

		/// <summary>
		/// Track number for stage 3.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE3 { get; }

		/// <summary>
		/// Track number for stage 4.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE4 { get; }

		/// <summary>
		/// Respect being earned from winning the race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int EarnableRespect { get; set; }

		/// <summary>
		/// Preset Ride that player uses in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string PlayerCarType { get; set; } = String.Empty;

		/// <summary>
		/// Cash amount that player gets for winning this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int CashValue { get; set; }

		/// <summary>
		/// Event icon type of this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eEventIconType EventIconType { get; set; }

		/// <summary>
		/// True if the race is a GPS race; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean IsDriveToGPS { get; set; }

		/// <summary>
		/// Difficulty level of this <see cref="GCareerRace"/>;
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eTrackDifficulty DifficultyLevel { get; set; }

		/// <summary>
		/// Stage number to which this <see cref="GCareerRace"/> belongs to.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// Number of opponents in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte NumOpponents { get; set; }

		/// <summary>
		/// Unknown drag value.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte UnknownDragValue { get; set; }

		/// <summary>
		/// Number of track stages in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte NumStages { get; set; }

		/// <summary>
		/// True if this event is hidden on the map; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public eBoolean IsHiddenEvent { get; set; }

		/// <summary>
		/// Movie shown pre-race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string IntroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Movie shown post-race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string OutroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Number of map items during the race flow.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte NumMapItems { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3A.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte Unknown0x3A { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3B.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte Unknown0x3B { get; set; }

		/// <summary>
		/// GPS destination trigger if the race is a GPS event.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string GPSDestination { get; set; } = String.Empty;

		/// <summary>
		/// Drift type of the race, if it is a drift event.
		/// </summary>
		private eDriftType DriftTypeIfDriftRace
		{
			get
			{
				if (this.EventBehaviorType != eEventBehaviorType.Drift)
					return eDriftType.VS_AI;

				var track1 = this.Database.Tracks.FindCollection($"Track_{this.STAGE1.TrackID}");
				var track2 = this.Database.Tracks.FindCollection($"Track_{this.STAGE2.TrackID}");
				var track3 = this.Database.Tracks.FindCollection($"Track_{this.STAGE3.TrackID}");
				var track4 = this.Database.Tracks.FindCollection($"Track_{this.STAGE4.TrackID}");

				var drift1 = (track1 != null) ? track1.DriftType : eDriftType.VS_AI;
				var drift2 = (track2 != null) ? track2.DriftType : eDriftType.VS_AI;
				var drift3 = (track3 != null) ? track3.DriftType : eDriftType.VS_AI;
				var drift4 = (track4 != null) ? track4.DriftType : eDriftType.VS_AI;

				return drift1 == eDriftType.DOWNHILL ||
					   drift2 == eDriftType.DOWNHILL ||
					   drift3 == eDriftType.DOWNHILL ||
					   drift4 == eDriftType.DOWNHILL
						? eDriftType.DOWNHILL
						: eDriftType.VS_AI;
			}
		}

		/// <summary>
		/// Opponent 1 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT1 { get; }

		/// <summary>
		/// Opponent 2 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT2 { get; }

		/// <summary>
		/// Opponent 3 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT3 { get; }

		/// <summary>
		/// Opponent 4 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT4 { get; }

		/// <summary>
		/// Opponent 5 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT5 { get; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		public GCareerRace()
		{
			this.OPPONENT1 = new Opponent();
			this.OPPONENT2 = new Opponent();
			this.OPPONENT3 = new Opponent();
			this.OPPONENT4 = new Opponent();
			this.OPPONENT5 = new Opponent();
			this.STAGE1 = new Stage();
			this.STAGE2 = new Stage();
			this.STAGE3 = new Stage();
			this.STAGE4 = new Stage();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="GCareerRaceManager"/> to which this instance belongs to.</param>
		public GCareerRace(string CName, GCareerRaceManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		/// <param name="manager"><see cref="GCareerRaceManager"/> to which this instance belongs to.</param>
		public GCareerRace(BinaryReader br, GCareerRaceManager manager) : this()
		{
			this.Manager = manager;
			this.OPPONENT1 = new Opponent();
			this.OPPONENT2 = new Opponent();
			this.OPPONENT3 = new Opponent();
			this.OPPONENT4 = new Opponent();
			this.OPPONENT5 = new Opponent();
			this.STAGE1 = new Stage();
			this.STAGE2 = new Stage();
			this.STAGE3 = new Stage();
			this.STAGE4 = new Stage();
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~GCareerRace() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="GCareerRace"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="GCareerRace"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			ushort pointer = 0; // for pointers

			pointer = (ushort)strw.BaseStream.Position;
			strw.WriteNullTermUTF8(this._collection_name);
			bw.Write(pointer);

			if (!String.IsNullOrEmpty(this.IntroMovie))
			{
				
				pointer = (ushort)strw.BaseStream.Position;
				strw.WriteNullTermUTF8(this.IntroMovie);
				bw.Write(pointer);
			
			}
			else bw.Write((ushort)0);

			if (!String.IsNullOrEmpty(this.OutroMovie))
			{
			
				pointer = (ushort)strw.BaseStream.Position;
				strw.WriteNullTermUTF8(this.OutroMovie);
				bw.Write(pointer);
			
			}
			else bw.Write((ushort)0);

			if (!String.IsNullOrEmpty(this.EventTrigger))
			{
			
				pointer = (ushort)strw.BaseStream.Position;
				strw.WriteNullTermUTF8(this.EventTrigger);
				bw.Write(pointer);
			
			}
			else bw.Write((ushort)0);

			bw.Write(this.BinKey);
			bw.WriteEnum(this.UnlockMethod);
			bw.WriteEnum(this.IsSUVRace);
			bw.Write(this._padding0);
			bw.WriteEnum(this.EventBehaviorType);

			if (this.UnlockMethod == eUnlockCondition.SPECIFIC_RACE_WON)
			{

				bw.Write(this.RequiredSpecificRaceWon.BinHash());

			}
			else
			{
			
				bw.Write(this.RequiredSpecificURLWon);
				bw.Write(this.SponsorChosenToUnlock);
				bw.Write(this.RequiredRacesWon);
				bw.Write(this.RequiredURLWon);
			
			}

			bw.Write(this.EarnableRespect);
			this.STAGE1.Write(bw);
			this.STAGE2.Write(bw);
			this.STAGE3.Write(bw);
			this.STAGE4.Write(bw);

			bw.Write(this.EventTrigger.BinHash());
			bw.Write(this.PlayerCarType.BinHash());
			bw.Write(this.CashValue);
			bw.WriteEnum(this.EventIconType);
			bw.WriteEnum(this.IsDriveToGPS);
			bw.WriteEnum(this.DifficultyLevel);
			bw.Write(this.BelongsToStage);
			bw.Write(this.NumMapItems);
			bw.Write(this._padding1);
			bw.Write(this.Unknown0x3A);
			bw.Write(this.Unknown0x3B);
			bw.Write(this.GPSDestination.BinHash());

			bw.WriteBytes(60); // fill buffer in case of skips
			var position = bw.BaseStream.Position;
			bw.BaseStream.Position -= 60;

			// If none of the events are drift downhill, write opponent data based on number of the opponents
			if (this.DriftTypeIfDriftRace != eDriftType.DOWNHILL)
			{

				if (this.NumOpponents > 0) this.OPPONENT1.Write(bw, strw);
				if (this.NumOpponents > 1) this.OPPONENT2.Write(bw, strw);
				if (this.NumOpponents > 2) this.OPPONENT3.Write(bw, strw);
				if (this.NumOpponents > 3) this.OPPONENT4.Write(bw, strw);
				if (this.NumOpponents > 4) this.OPPONENT5.Write(bw, strw);
			
			}

			// If at least one of the events is drift downhill, write at least 3 opponents
			else
			{

				this.OPPONENT1.Write(bw, strw);
				this.OPPONENT2.Write(bw, strw);
				this.OPPONENT3.Write(bw, strw);
				if (this.NumOpponents > 3) this.OPPONENT4.Write(bw, strw);
				if (this.NumOpponents > 4) this.OPPONENT5.Write(bw, strw);
			
			}

			bw.BaseStream.Position = position;
			bw.Write(this.NumOpponents);
			bw.Write(this.UnknownDragValue);
			bw.Write(this.NumStages);
			bw.WriteEnum(this.IsHiddenEvent);
			bw.Write(this._padding2);
			bw.Write(this._padding3);
		}

		/// <summary>
		/// Disassembles array into <see cref="GCareerRace"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCareerRace"/> with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public void Disassemble(BinaryReader br, BinaryReader strr)
		{
			ushort position = 0;

			// Collection Name
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this._collection_name = strr.ReadNullTermUTF8();

			// Intro Movie
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this.IntroMovie = strr.ReadNullTermUTF8();

			// Outro Movie
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this.OutroMovie = strr.ReadNullTermUTF8();

			// Event Trigger
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this.EventTrigger = strr.ReadNullTermUTF8();

			// Event behavior
			br.BaseStream.Position += 4;
			this.UnlockMethod = br.ReadEnum<eUnlockCondition>();
			this.IsSUVRace = br.ReadEnum<eBoolean>();
			this._padding0 = br.ReadByte();
			this.EventBehaviorType = br.ReadEnum<eEventBehaviorType>();

			// Unlock conditions
			if (this.UnlockMethod == eUnlockCondition.SPECIFIC_RACE_WON)
			{

				this.RequiredSpecificRaceWon = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			}
			else
			{

				this.RequiredSpecificURLWon = br.ReadByte();
				this.SponsorChosenToUnlock = br.ReadByte();
				this.RequiredRacesWon = br.ReadByte();
				this.RequiredURLWon = br.ReadByte();
			
			}

			// Earnable Respect ?
			this.EarnableRespect = br.ReadInt32();

			// Stages
			this.STAGE1.Read(br);
			this.STAGE2.Read(br);
			this.STAGE3.Read(br);
			this.STAGE4.Read(br);

			// PlayerCarType and CashValue
			br.BaseStream.Position += 4;
			this.PlayerCarType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.CashValue = br.ReadInt32();

			// Some UnknownValues
			this.EventIconType = br.ReadEnum<eEventIconType>();
			this.IsDriveToGPS = br.ReadEnum<eBoolean>();
			this.DifficultyLevel = br.ReadEnum<eTrackDifficulty>();
			this.BelongsToStage = br.ReadByte();

			// Some values
			this.NumMapItems = br.ReadByte();
			this._padding1 = br.ReadByte();
			this.Unknown0x3A = br.ReadByte();
			this.Unknown0x3B = br.ReadByte();

			// GPS Destination
			this.GPSDestination = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			// Stage Values
			br.BaseStream.Position += 0x3C;
			this.NumOpponents = br.ReadByte();
			this.UnknownDragValue = br.ReadByte();
			this.NumStages = br.ReadByte();
			this.IsHiddenEvent = br.ReadEnum<eBoolean>();
			this._padding2 = br.ReadInt32();
			this._padding3 = br.ReadInt32();
			var final = br.BaseStream.Position;
			br.BaseStream.Position -= 0x48;

			// If none of the events are drift downhill, read opponent data based on number of the opponents
			if (this.DriftTypeIfDriftRace != eDriftType.DOWNHILL)
			{

				if (this.NumOpponents > 0) this.OPPONENT1.Read(br, strr);
				if (this.NumOpponents > 1) this.OPPONENT2.Read(br, strr);
				if (this.NumOpponents > 2) this.OPPONENT3.Read(br, strr);
				if (this.NumOpponents > 3) this.OPPONENT4.Read(br, strr);
				if (this.NumOpponents > 4) this.OPPONENT5.Read(br, strr);
			
			}

			// If at least one of the events is downhill drift, read only 3 opponents
			else
			{

				this.OPPONENT1.Read(br, strr);
				this.OPPONENT2.Read(br, strr);
				this.OPPONENT3.Read(br, strr);
				if (this.NumOpponents > 3) this.OPPONENT4.Read(br, strr);
				if (this.NumOpponents > 4) this.OPPONENT5.Read(br, strr);
			
			}

			br.BaseStream.Position = final;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new GCareerRace(CName, this.Manager);
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
	}
}