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
		private byte _padding1 = 0;

		[MemoryCastable()]
		private int _padding2 = 0;

		[MemoryCastable()]
		private int _padding3 = 0;

		#endregion

		#region Enums

		/// <summary>
		/// Enum of <see cref="GCareerRace"/> event behavior types.
		/// </summary>
		public enum EventBehaviorType : byte
		{
			/// <summary>
			/// Circuit event.
			/// </summary>
			Circuit = 0,

			/// <summary>
			/// Sprint event.
			/// </summary>
			Sprint = 1,

			/// <summary>
			/// StreetX event.
			/// </summary>
			StreetX = 2,

			/// <summary>
			/// OpenWorld (FreeRoam) event.
			/// </summary>
			OpenWorld = 3,

			/// <summary>
			/// Drag event.
			/// </summary>
			Drag = 4,

			/// <summary>
			/// Drift event.
			/// </summary>
			Drift = 5,
		}

		/// <summary>
		/// Enum of <see cref="GCareerRace"/> event icon types.
		/// </summary>
		public enum EventIconType : byte
		{
			/// <summary>
			/// No icon.
			/// </summary>
			NONE = 0,

			/// <summary>
			/// Sponsor icon.
			/// </summary>
			SPONSOR = 1,

			/// <summary>
			/// URL icon.
			/// </summary>
			URL = 2,

			/// <summary>
			/// Regular icon.
			/// </summary>
			REGULAR = 3,
		}

		/// <summary>
		/// Enum of <see cref="GCareerRace"/> event unlock conditions.
		/// </summary>
		public enum UnlockCondition : byte
		{
			/// <summary>
			/// Event requires specific race won.
			/// </summary>
			SpecificRaceWon = 0,

			/// <summary>
			/// Event unlocks at the beginning of the stage to which it belongs to.
			/// </summary>
			AtStageStart = 1,

			/// <summary>
			/// Event requires specific sponsor index chosen.
			/// </summary>
			UnknownType = 2,

			/// <summary>
			/// Event requires specific amount of regular races won.
			/// </summary>
			ReqRegRacesWon = 3,

			/// <summary>
			/// Event requires specific amount of URL, sponsor, and regular won.
			/// </summary>
			ReqVarRacesWon = 4,
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
				if (this.Career.GetCollection(value, nameof(this.Career.GCareerRaces)) != null)
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
		[MemoryCastable()]
		[Category("Secondary")]
		public UnlockCondition UnlockMethod { get; set; }

		/// <summary>
		/// True if the race is an SUV race; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean IsSUVRace { get; set; }

		/// <summary>
		/// Unused true-false value.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean UnusedValue { get; set; }

		/// <summary>
		/// Event type.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public EventBehaviorType EventBehavior { get; set; }

		/// <summary>
		/// Unknown value at offset 0x10.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte Unknown0x10 { get; set; }

		/// <summary>
		/// Required race completed in order to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string ReqSpecRaceWon { get; set; } = String.Empty;

		/// <summary>
		/// Required number of sponsor races won to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte ReqSponRacesWon { get; set; }

		/// <summary>
		/// Required number of regular races won to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte ReqRegRacesWon { get; set; }

		/// <summary>
		/// Required number of URL races won to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte ReqURLRacesWon { get; set; }

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
		[MemoryCastable()]
		[Category("Secondary")]
		public int EarnableRespect { get; set; }

		/// <summary>
		/// Preset Ride that player uses in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string PlayerCarType { get; set; } = String.Empty;

		/// <summary>
		/// Cash amount that player gets for winning this race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int CashValue { get; set; }

		/// <summary>
		/// Event icon type of this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public EventIconType EventIcon { get; set; }

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
		[MemoryCastable()]
		[Category("Secondary")]
		public byte TrafficDensity { get; set; }

		/// <summary>
		/// Stage number to which this <see cref="GCareerRace"/> belongs to.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// Number of opponents in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte NumOpponents { get; set; }

		/// <summary>
		/// Unknown drag value.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte UnknownDragValue { get; set; }

		/// <summary>
		/// Number of track stages in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte NumStages { get; set; }

		/// <summary>
		/// True if this event is hidden on the map; false otherwise.
		/// </summary>
		[AccessModifiable()]
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
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public GCareerRace(string CName, GCareer career) : this()
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public GCareerRace(BinaryReader br, BinaryReader strr, GCareer career) : this()
		{
			this.Career = career;
			this.OPPONENT1 = new Opponent();
			this.OPPONENT2 = new Opponent();
			this.OPPONENT3 = new Opponent();
			this.OPPONENT4 = new Opponent();
			this.OPPONENT5 = new Opponent();
			this.STAGE1 = new Stage();
			this.STAGE2 = new Stage();
			this.STAGE3 = new Stage();
			this.STAGE4 = new Stage();
			this.Disassemble(br, strr);
		}

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
			bw.WriteEnum(this.UnusedValue);
			bw.WriteEnum(this.EventBehavior);

			switch (this.UnlockMethod)
			{

				case UnlockCondition.SpecificRaceWon:
					bw.Write(this.ReqSpecRaceWon.BinHash());
					break;

				case UnlockCondition.ReqRegRacesWon:
					bw.Write((short)0);
					bw.Write(this.ReqRegRacesWon);
					bw.Write((byte)0);
					break;

				case UnlockCondition.ReqVarRacesWon:
					bw.Write((byte)0);
					bw.Write(this.ReqRegRacesWon);
					bw.Write(this.ReqSponRacesWon);
					bw.Write(this.ReqURLRacesWon);
					break;

				default:
					bw.Write((int)0);
					break;

			}

			bw.Write(this.EarnableRespect);
			this.STAGE1.Write(bw);
			this.STAGE2.Write(bw);
			this.STAGE3.Write(bw);
			this.STAGE4.Write(bw);

			bw.Write(this.EventTrigger.BinHash());
			bw.Write(this.PlayerCarType.BinHash());
			bw.Write(this.CashValue);
			bw.WriteEnum(this.EventIcon);
			bw.WriteEnum(this.IsDriveToGPS);
			bw.Write(this.TrafficDensity);
			bw.Write(this.BelongsToStage);
			bw.Write(this.NumMapItems);
			bw.Write(this._padding1);
			bw.Write(this.Unknown0x3A);
			bw.Write(this.Unknown0x3B);
			bw.Write(this.GPSDestination.BinHash());

			var position = bw.BaseStream.Position + 60;

			// If none of the events are drift downhill, write opponent data based on number of the opponents
			if (this.DriftTypeIfDriftRace() != Shared.Class.Track.TrackDriftType.DOWNHILL)
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
			this.UnlockMethod = br.ReadEnum<UnlockCondition>();
			this.IsSUVRace = br.ReadEnum<eBoolean>();
			this.UnusedValue = br.ReadEnum<eBoolean>();
			this.EventBehavior = br.ReadEnum<EventBehaviorType>();

			// Unlock conditions
			switch (this.UnlockMethod)
			{

				case UnlockCondition.SpecificRaceWon:
					this.ReqSpecRaceWon = br.ReadUInt32().BinString(LookupReturn.EMPTY);
					break;

				case UnlockCondition.ReqRegRacesWon:
					br.BaseStream.Position += 2;
					this.ReqRegRacesWon = br.ReadByte();
					++br.BaseStream.Position;
					break;

				case UnlockCondition.ReqVarRacesWon:
					++br.BaseStream.Position;
					this.ReqRegRacesWon = br.ReadByte();
					this.ReqSponRacesWon = br.ReadByte();
					this.ReqURLRacesWon = br.ReadByte();
					break;

				default:
					br.BaseStream.Position += 4;
					break;

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
			this.PlayerCarType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.CashValue = br.ReadInt32();

			// Some UnknownValues
			this.EventIcon = br.ReadEnum<EventIconType>();
			this.IsDriveToGPS = br.ReadEnum<eBoolean>();
			this.TrafficDensity = br.ReadByte();
			this.BelongsToStage = br.ReadByte();

			// Some values
			this.NumMapItems = br.ReadByte();
			this._padding1 = br.ReadByte();
			this.Unknown0x3A = br.ReadByte();
			this.Unknown0x3B = br.ReadByte();

			// GPS Destination
			this.GPSDestination = br.ReadUInt32().BinString(LookupReturn.EMPTY);

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
			if (this.DriftTypeIfDriftRace() != Shared.Class.Track.TrackDriftType.DOWNHILL)
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
			var result = new GCareerRace(CName, this.Career);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Drift type of the race, if it is a drift event.
		/// </summary>
		private Shared.Class.Track.TrackDriftType DriftTypeIfDriftRace()
		{
			var vsai = Shared.Class.Track.TrackDriftType.VS_AI;
			var down = Shared.Class.Track.TrackDriftType.DOWNHILL;

			if (this.EventBehavior != EventBehaviorType.Drift) return vsai;
			if (this.Career is null) return vsai;
			if (this.Career.Manager is null) return vsai;
			if (this.Career.Manager.Database is null) return vsai;

			var db = this.Career.Manager.Database as Datamap;

			var track1 = db.Tracks[this.STAGE1.TrackID.ToString()];
			var track2 = db.Tracks[this.STAGE2.TrackID.ToString()];
			var track3 = db.Tracks[this.STAGE3.TrackID.ToString()];
			var track4 = db.Tracks[this.STAGE4.TrackID.ToString()];

			var drift1 = (!(track1 is null)) ? track1.DriftType : vsai;
			var drift2 = (!(track2 is null)) ? track2.DriftType : vsai;
			var drift3 = (!(track3 is null)) ? track3.DriftType : vsai;
			var drift4 = (!(track4 is null)) ? track4.DriftType : vsai;

			return drift1 == down || drift2 == down || drift3 == down || drift4 == down
					? down : vsai;
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
			bw.WriteNullTermUTF8(this.OutroMovie);
			bw.WriteNullTermUTF8(this.EventTrigger);
			bw.WriteEnum(this.UnlockMethod);
			bw.WriteEnum(this.IsSUVRace);
			bw.WriteEnum(this.UnusedValue);
			bw.WriteEnum(this.EventBehavior);

			bw.WriteNullTermUTF8(this.ReqSpecRaceWon);
			bw.Write(this.ReqRegRacesWon);
			bw.Write(this.ReqURLRacesWon);
			bw.Write(this.ReqSponRacesWon);

			bw.Write(this.EarnableRespect);
			this.STAGE1.Write(bw);
			this.STAGE2.Write(bw);
			this.STAGE3.Write(bw);
			this.STAGE4.Write(bw);

			bw.WriteNullTermUTF8(this.PlayerCarType);
			bw.Write(this.CashValue);
			bw.WriteEnum(this.EventIcon);
			bw.WriteEnum(this.IsDriveToGPS);
			bw.Write(this.TrafficDensity);
			bw.Write(this.BelongsToStage);
			bw.Write(this.NumMapItems);
			bw.Write(this._padding1);
			bw.Write(this.Unknown0x3A);
			bw.Write(this.Unknown0x3B);
			bw.WriteNullTermUTF8(this.GPSDestination);

			this.OPPONENT1.Serialize(bw);
			this.OPPONENT2.Serialize(bw);
			this.OPPONENT3.Serialize(bw);
			this.OPPONENT4.Serialize(bw);
			this.OPPONENT5.Serialize(bw);

			bw.Write(this.NumOpponents);
			bw.Write(this.UnknownDragValue);
			bw.Write(this.NumStages);
			bw.WriteEnum(this.IsHiddenEvent);
			bw.Write(this._padding2);
			bw.Write(this._padding3);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this._collection_name = br.ReadNullTermUTF8();
			this.IntroMovie = br.ReadNullTermUTF8();
			this.OutroMovie = br.ReadNullTermUTF8();
			this.EventTrigger = br.ReadNullTermUTF8();
			this.UnlockMethod = br.ReadEnum<UnlockCondition>();
			this.IsSUVRace = br.ReadEnum<eBoolean>();
			this.UnusedValue = br.ReadEnum<eBoolean>();
			this.EventBehavior = br.ReadEnum<EventBehaviorType>();

			this.ReqSpecRaceWon = br.ReadNullTermUTF8();
			this.ReqRegRacesWon = br.ReadByte();
			this.ReqURLRacesWon = br.ReadByte();
			this.ReqSponRacesWon = br.ReadByte();

			this.EarnableRespect = br.ReadInt32();
			this.STAGE1.Read(br);
			this.STAGE2.Read(br);
			this.STAGE3.Read(br);
			this.STAGE4.Read(br);

			this.PlayerCarType = br.ReadNullTermUTF8();
			this.CashValue = br.ReadInt32();
			this.EventIcon = br.ReadEnum<EventIconType>();
			this.IsDriveToGPS = br.ReadEnum<eBoolean>();
			this.TrafficDensity = br.ReadByte();
			this.BelongsToStage = br.ReadByte();
			this.NumMapItems = br.ReadByte();
			this._padding1 = br.ReadByte();
			this.Unknown0x3A = br.ReadByte();
			this.Unknown0x3B = br.ReadByte();
			this.GPSDestination = br.ReadNullTermUTF8();

			this.OPPONENT1.Deserialize(br);
			this.OPPONENT2.Deserialize(br);
			this.OPPONENT3.Deserialize(br);
			this.OPPONENT4.Deserialize(br);
			this.OPPONENT5.Deserialize(br);

			this.NumOpponents = br.ReadByte();
			this.UnknownDragValue = br.ReadByte();
			this.NumStages = br.ReadByte();
			this.IsHiddenEvent = br.ReadEnum<eBoolean>();
			this._padding2 = br.ReadInt32();
			this._padding3 = br.ReadInt32();
		}

		#endregion
	}
}