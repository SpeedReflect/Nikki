using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Parts.GameParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="GCareerRace"/> is a collection of settings related to career races and events.
	/// </summary>
	public class GCareerRace : ACollectable
	{
		#region Fields

		private string _collection_name;
		private byte _padding0 = 0;
		private byte _padding1 = 0;
		private int _padding2 = 0;
		private int _padding3 = 0;

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
				if (this.Database.GCareerRaces.FindCollection(value) != null)
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
		/// Engage trigger of this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		public string EventTrigger { get; set; } = String.Empty;

		/// <summary>
		/// Unlock method.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eUnlockCondition UnlockMethod { get; set; }

		/// <summary>
		/// True if the race is an SUV race; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean IsSUVRace { get; set; }

		/// <summary>
		/// Event type.
		/// </summary>
		[AccessModifiable()]
		public eEventBehaviorType EventBehaviorType { get; set; }

		/// <summary>
		/// Required race completed in order to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string RequiredSpecificRaceWon { get; set; } = String.Empty;

		/// <summary>
		/// Required URL race completer in order to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte RequiredSpecificURLWon { get; set; }

		/// <summary>
		/// Required sponsor chosen in order to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte SponsorChosenToUnlock { get; set; }

		/// <summary>
		/// Required number of races won to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte RequiredRacesWon { get; set; }

		/// <summary>
		/// Required number of URL won to unlock this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte RequiredURLWon { get; set; }

		/// <summary>
		/// Track number for stage 1.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public ushort TrackID_Stage1 { get; set; }

		/// <summary>
		/// Track number for stage 2.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public ushort TrackID_Stage2 { get; set; }

		/// <summary>
		/// Track number for stage 3.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public ushort TrackID_Stage3 { get; set; }

		/// <summary>
		/// Track number for stage 4.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public ushort TrackID_Stage4 { get; set; }

		/// <summary>
		/// Number of laps in stage 1.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte NumLaps_Stage1 { get; set; }

		/// <summary>
		/// Number of laps in stage 2.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte NumLaps_Stage2 { get; set; }

		/// <summary>
		/// Number of laps in stage 3.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte NumLaps_Stage3 { get; set; }

		/// <summary>
		/// Number of laps in stage 4.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte NumLaps_Stage4 { get; set; }

		/// <summary>
		/// True if stage 1 track is in reverse; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean InReverseDirection_Stage1 { get; set; }

		/// <summary>
		/// True if stage 2 track is in reverse; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean InReverseDirection_Stage2 { get; set; }

		/// <summary>
		/// True if stage 3 track is in reverse; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean InReverseDirection_Stage3 { get; set; }

		/// <summary>
		/// True if stage 4 track is in reverse; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean InReverseDirection_Stage4 { get; set; }

		/// <summary>
		/// Respect being earned from winning the race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public int EarnableRespect { get; set; }

		/// <summary>
		/// Preset Ride that player uses in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PlayerCarType { get; set; } = String.Empty;

		/// <summary>
		/// Cash amount that player gets for winning this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public int CashValue { get; set; }

		/// <summary>
		/// Event icon type of this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eEventIconType EventIconType { get; set; }

		/// <summary>
		/// True if the race is a GPS race; false otherwise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean IsDriveToGPS { get; set; }

		/// <summary>
		/// Difficulty level of this <see cref="GCareerRace"/>;
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eTrackDifficulty DifficultyLevel { get; set; }

		/// <summary>
		/// Stage number to which this <see cref="GCareerRace"/> belongs to.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// Number of opponents in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte NumOpponents { get; set; }

		/// <summary>
		/// Unknown drag value.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte UnknownDragValue { get; set; }

		/// <summary>
		/// Number of track stages in this <see cref="GCareerRace"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte NumStages { get; set; }

		/// <summary>
		/// True if this event is hidden on the map; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean IsHiddenEvent { get; set; }

		/// <summary>
		/// Movie shown pre-race.
		/// </summary>
		[AccessModifiable()]
		public string IntroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Movie shown post-race.
		/// </summary>
		[AccessModifiable()]
		public string OutroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Number of map items during the race flow.
		/// </summary>
		[AccessModifiable()]
		public byte NumMapItems { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3A.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown0x3A { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3B.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown0x3B { get; set; }

		/// <summary>
		/// GPS destination trigger if the race is a GPS event.
		/// </summary>
		[AccessModifiable()]
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

				var track1 = this.Database.Tracks.FindCollection($"Track_{this.TrackID_Stage1}");
				var track2 = this.Database.Tracks.FindCollection($"Track_{this.TrackID_Stage2}");
				var track3 = this.Database.Tracks.FindCollection($"Track_{this.TrackID_Stage3}");
				var track4 = this.Database.Tracks.FindCollection($"Track_{this.TrackID_Stage4}");

				var drift1 = (track1 != null) ? track1.DriftType : eDriftType.VS_AI;
				var drift2 = (track2 != null) ? track2.DriftType : eDriftType.VS_AI;
				var drift3 = (track3 != null) ? track3.DriftType : eDriftType.VS_AI;
				var drift4 = (track4 != null) ? track4.DriftType : eDriftType.VS_AI;

				if (drift1 == eDriftType.DOWNHILL ||
					drift2 == eDriftType.DOWNHILL ||
					drift3 == eDriftType.DOWNHILL ||
					drift4 == eDriftType.DOWNHILL)
					return eDriftType.DOWNHILL;
				else
					return eDriftType.VS_AI;
			}
		}

		/// <summary>
		/// Opponent 1 settings.
		/// </summary>
		[Expandable("Opponents")]
		public Opponent OPPONENT1 { get; set; }

		/// <summary>
		/// Opponent 2 settings.
		/// </summary>
		[Expandable("Opponents")]
		public Opponent OPPONENT2 { get; set; }

		/// <summary>
		/// Opponent 3 settings.
		/// </summary>
		[Expandable("Opponents")]
		public Opponent OPPONENT3 { get; set; }

		/// <summary>
		/// Opponent 4 settings.
		/// </summary>
		[Expandable("Opponents")]
		public Opponent OPPONENT4 { get; set; }

		/// <summary>
		/// Opponent 5 settings.
		/// </summary>
		[Expandable("Opponents")]
		public Opponent OPPONENT5 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		public GCareerRace() { }

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public GCareerRace(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			this.OPPONENT1 = new Opponent();
			this.OPPONENT2 = new Opponent();
			this.OPPONENT3 = new Opponent();
			this.OPPONENT4 = new Opponent();
			this.OPPONENT5 = new Opponent();
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public GCareerRace(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			this.Database = db;
			this.OPPONENT1 = new Opponent();
			this.OPPONENT2 = new Opponent();
			this.OPPONENT3 = new Opponent();
			this.OPPONENT4 = new Opponent();
			this.OPPONENT5 = new Opponent();
			this.Disassemble(br, strr);
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
				bw.Write(this.RequiredSpecificRaceWon.BinHash());
			else
			{
				bw.Write(this.RequiredSpecificURLWon);
				bw.Write(this.SponsorChosenToUnlock);
				bw.Write(this.RequiredRacesWon);
				bw.Write(this.RequiredURLWon);
			}

			bw.Write(this.EarnableRespect);
			bw.Write(this.TrackID_Stage1);
			bw.WriteEnum(this.InReverseDirection_Stage1);
			bw.Write(this.NumLaps_Stage1);
			bw.Write(this.TrackID_Stage2);
			bw.WriteEnum(this.InReverseDirection_Stage2);
			bw.Write(this.NumLaps_Stage2);
			bw.Write(this.TrackID_Stage3);
			bw.WriteEnum(this.InReverseDirection_Stage3);
			bw.Write(this.NumLaps_Stage3);
			bw.Write(this.TrackID_Stage4);
			bw.WriteEnum(this.InReverseDirection_Stage4);
			bw.Write(this.NumLaps_Stage4);

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
				if (this.NumOpponents > 0)
				{
					if (!String.IsNullOrEmpty(this.OPPONENT1.Name))
					{
						pointer = (ushort)strw.BaseStream.Position;
						strw.WriteNullTermUTF8(this.OPPONENT1.Name);
						bw.Write(pointer);
					}
					else bw.Write((ushort)0);
					bw.Write(this.OPPONENT1.StatsMultiplier);
					bw.Write(this.OPPONENT1.PresetRide.BinHash());
					bw.Write(this.OPPONENT1.SkillEasy);
					bw.Write(this.OPPONENT1.SkillMedium);
					bw.Write(this.OPPONENT1.SkillHard);
					bw.Write(this.OPPONENT1.CatchUp);
				}
				if (this.NumOpponents > 1)
				{
					if (!String.IsNullOrEmpty(this.OPPONENT2.Name))
					{
						pointer = (ushort)strw.BaseStream.Position;
						strw.WriteNullTermUTF8(this.OPPONENT2.Name);
						bw.Write(pointer);
					}
					else bw.Write((ushort)0);
					bw.Write(this.OPPONENT2.StatsMultiplier);
					bw.Write(this.OPPONENT2.PresetRide.BinHash());
					bw.Write(this.OPPONENT2.SkillEasy);
					bw.Write(this.OPPONENT2.SkillMedium);
					bw.Write(this.OPPONENT2.SkillHard);
					bw.Write(this.OPPONENT2.CatchUp);
				}
				if (this.NumOpponents > 2)
				{
					if (!String.IsNullOrEmpty(this.OPPONENT3.Name))
					{
						pointer = (ushort)strw.BaseStream.Position;
						strw.WriteNullTermUTF8(this.OPPONENT3.Name);
						bw.Write(pointer);
					}
					else bw.Write((ushort)0);
					bw.Write(this.OPPONENT3.StatsMultiplier);
					bw.Write(this.OPPONENT3.PresetRide.BinHash());
					bw.Write(this.OPPONENT3.SkillEasy);
					bw.Write(this.OPPONENT3.SkillMedium);
					bw.Write(this.OPPONENT3.SkillHard);
					bw.Write(this.OPPONENT3.CatchUp);
				}
				if (this.NumOpponents > 3)
				{
					if (!String.IsNullOrEmpty(this.OPPONENT4.Name))
					{
						pointer = (ushort)strw.BaseStream.Position;
						strw.WriteNullTermUTF8(this.OPPONENT4.Name);
						bw.Write(pointer);
					}
					else bw.Write((ushort)0);
					bw.Write(this.OPPONENT4.StatsMultiplier);
					bw.Write(this.OPPONENT4.PresetRide.BinHash());
					bw.Write(this.OPPONENT4.SkillEasy);
					bw.Write(this.OPPONENT4.SkillMedium);
					bw.Write(this.OPPONENT4.SkillHard);
					bw.Write(this.OPPONENT4.CatchUp);
				}
				if (this.NumOpponents > 4)
				{
					if (!String.IsNullOrEmpty(this.OPPONENT5.Name))
					{
						pointer = (ushort)strw.BaseStream.Position;
						strw.WriteNullTermUTF8(this.OPPONENT5.Name);
						bw.Write(pointer);
					}
					else bw.Write((ushort)0);
					bw.Write(this.OPPONENT5.StatsMultiplier);
					bw.Write(this.OPPONENT5.PresetRide.BinHash());
					bw.Write(this.OPPONENT5.SkillEasy);
					bw.Write(this.OPPONENT5.SkillMedium);
					bw.Write(this.OPPONENT5.SkillHard);
					bw.Write(this.OPPONENT5.CatchUp);
				}
			}

			// If at least one of the events is drift downhill, write at least 3 opponents
			else
			{
				if (!String.IsNullOrEmpty(this.OPPONENT1.Name))
				{
					pointer = (ushort)strw.BaseStream.Position;
					strw.WriteNullTermUTF8(this.OPPONENT1.Name);
					bw.Write(pointer);
				}
				else bw.Write((ushort)0);
				bw.Write(this.OPPONENT1.StatsMultiplier);
				bw.Write(this.OPPONENT1.PresetRide.BinHash());
				bw.Write(this.OPPONENT1.SkillEasy);
				bw.Write(this.OPPONENT1.SkillMedium);
				bw.Write(this.OPPONENT1.SkillHard);
				bw.Write(this.OPPONENT1.CatchUp);

				if (!String.IsNullOrEmpty(this.OPPONENT2.Name))
				{
					pointer = (ushort)strw.BaseStream.Position;
					strw.WriteNullTermUTF8(this.OPPONENT2.Name);
					bw.Write(pointer);
				}
				else bw.Write((ushort)0);
				bw.Write(this.OPPONENT2.StatsMultiplier);
				bw.Write(this.OPPONENT2.PresetRide.BinHash());
				bw.Write(this.OPPONENT2.SkillEasy);
				bw.Write(this.OPPONENT2.SkillMedium);
				bw.Write(this.OPPONENT2.SkillHard);
				bw.Write(this.OPPONENT2.CatchUp);

				if (!String.IsNullOrEmpty(this.OPPONENT3.Name))
				{
					pointer = (ushort)strw.BaseStream.Position;
					strw.WriteNullTermUTF8(this.OPPONENT3.Name);
					bw.Write(pointer);
				}
				else bw.Write((ushort)0);
				bw.Write(this.OPPONENT3.StatsMultiplier);
				bw.Write(this.OPPONENT3.PresetRide.BinHash());
				bw.Write(this.OPPONENT3.SkillEasy);
				bw.Write(this.OPPONENT3.SkillMedium);
				bw.Write(this.OPPONENT3.SkillHard);
				bw.Write(this.OPPONENT3.CatchUp);

				if (this.NumOpponents > 3)
				{
					if (!String.IsNullOrEmpty(this.OPPONENT4.Name))
					{
						pointer = (ushort)strw.BaseStream.Position;
						strw.WriteNullTermUTF8(this.OPPONENT4.Name);
						bw.Write(pointer);
					}
					else bw.Write((ushort)0);
					bw.Write(this.OPPONENT4.StatsMultiplier);
					bw.Write(this.OPPONENT4.PresetRide.BinHash());
					bw.Write(this.OPPONENT4.SkillEasy);
					bw.Write(this.OPPONENT4.SkillMedium);
					bw.Write(this.OPPONENT4.SkillHard);
					bw.Write(this.OPPONENT4.CatchUp);
				}
				if (this.NumOpponents > 4)
				{
					if (!String.IsNullOrEmpty(this.OPPONENT5.Name))
					{
						pointer = (ushort)strw.BaseStream.Position;
						strw.WriteNullTermUTF8(this.OPPONENT5.Name);
						bw.Write(pointer);
					}
					else bw.Write((ushort)0);
					bw.Write(this.OPPONENT5.StatsMultiplier);
					bw.Write(this.OPPONENT5.PresetRide.BinHash());
					bw.Write(this.OPPONENT5.SkillEasy);
					bw.Write(this.OPPONENT5.SkillMedium);
					bw.Write(this.OPPONENT5.SkillHard);
					bw.Write(this.OPPONENT5.CatchUp);
				}
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
			// Collection Name
			strr.BaseStream.Position = br.ReadUInt16();
			this._collection_name = strr.ReadNullTermUTF8();

			// Intro Movie
			strr.BaseStream.Position = br.ReadUInt16();
			this.IntroMovie = strr.ReadNullTermUTF8();

			// Outro Movie
			strr.BaseStream.Position = br.ReadUInt16();
			this.OutroMovie = strr.ReadNullTermUTF8();

			// Event Trigger
			strr.BaseStream.Position = br.ReadUInt16();
			this.EventTrigger = strr.ReadNullTermUTF8();

			// Event behavior
			br.BaseStream.Position += 4;
			this.UnlockMethod = br.ReadEnum<eUnlockCondition>();
			this.IsSUVRace = br.ReadEnum<eBoolean>();
			this._padding0 = br.ReadByte();
			this.EventBehaviorType = br.ReadEnum<eEventBehaviorType>();

			// Unlock conditions
			if (this.UnlockMethod == eUnlockCondition.SPECIFIC_RACE_WON)
				this.RequiredSpecificRaceWon = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			else
			{
				this.RequiredSpecificURLWon = br.ReadByte();
				this.SponsorChosenToUnlock = br.ReadByte();
				this.RequiredRacesWon = br.ReadByte();
				this.RequiredURLWon = br.ReadByte();
			}

			// Earnable Respect ?
			this.EarnableRespect = br.ReadInt32();

			this.TrackID_Stage1 = br.ReadUInt16();
			this.InReverseDirection_Stage1 = (eBoolean)(br.ReadByte() % 2);
			this.NumLaps_Stage1 = br.ReadByte();
			this.TrackID_Stage2 = br.ReadUInt16();
			this.InReverseDirection_Stage2 = (eBoolean)(br.ReadByte() % 2);
			this.NumLaps_Stage2 = br.ReadByte();
			this.TrackID_Stage3 = br.ReadUInt16();
			this.InReverseDirection_Stage3 = (eBoolean)(br.ReadByte() % 2);
			this.NumLaps_Stage3 = br.ReadByte();
			this.TrackID_Stage4 = br.ReadUInt16();
			this.InReverseDirection_Stage4 = (eBoolean)(br.ReadByte() % 2);
			this.NumLaps_Stage4 = br.ReadByte();

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
			var position = br.BaseStream.Position;
			br.BaseStream.Position -= 0x48;

			// If none of the events are drift downhill, read opponent data based on number of the opponents
			if (this.DriftTypeIfDriftRace != eDriftType.DOWNHILL)
			{
				if (this.NumOpponents > 0)
				{
					strr.BaseStream.Position = br.ReadUInt16();
					this.OPPONENT1.Name = strr.ReadNullTermUTF8();
					this.OPPONENT1.StatsMultiplier = br.ReadUInt16();
					this.OPPONENT1.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
					this.OPPONENT1.SkillEasy = br.ReadByte();
					this.OPPONENT1.SkillMedium = br.ReadByte();
					this.OPPONENT1.SkillHard = br.ReadByte();
					this.OPPONENT1.CatchUp = br.ReadByte();
				}
				if (this.NumOpponents > 1)
				{
					strr.BaseStream.Position = br.ReadUInt16();
					this.OPPONENT2.Name = strr.ReadNullTermUTF8();
					this.OPPONENT2.StatsMultiplier = br.ReadUInt16();
					this.OPPONENT2.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
					this.OPPONENT2.SkillEasy = br.ReadByte();
					this.OPPONENT2.SkillMedium = br.ReadByte();
					this.OPPONENT2.SkillHard = br.ReadByte();
					this.OPPONENT2.CatchUp = br.ReadByte();
				}
				if (this.NumOpponents > 2)
				{
					strr.BaseStream.Position = br.ReadUInt16();
					this.OPPONENT3.Name = strr.ReadNullTermUTF8();
					this.OPPONENT3.StatsMultiplier = br.ReadUInt16();
					this.OPPONENT3.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
					this.OPPONENT3.SkillEasy = br.ReadByte();
					this.OPPONENT3.SkillMedium = br.ReadByte();
					this.OPPONENT3.SkillHard = br.ReadByte();
					this.OPPONENT3.CatchUp = br.ReadByte();
				}
				if (this.NumOpponents > 3)
				{
					strr.BaseStream.Position = br.ReadUInt16();
					this.OPPONENT4.Name = strr.ReadNullTermUTF8();
					this.OPPONENT4.StatsMultiplier = br.ReadUInt16();
					this.OPPONENT4.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
					this.OPPONENT4.SkillEasy = br.ReadByte();
					this.OPPONENT4.SkillMedium = br.ReadByte();
					this.OPPONENT4.SkillHard = br.ReadByte();
					this.OPPONENT4.CatchUp = br.ReadByte();
				}
				if (this.NumOpponents > 4)
				{
					strr.BaseStream.Position = br.ReadUInt16();
					this.OPPONENT5.Name = strr.ReadNullTermUTF8();
					this.OPPONENT5.StatsMultiplier = br.ReadUInt16();
					this.OPPONENT5.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
					this.OPPONENT5.SkillEasy = br.ReadByte();
					this.OPPONENT5.SkillMedium = br.ReadByte();
					this.OPPONENT5.SkillHard = br.ReadByte();
					this.OPPONENT5.CatchUp = br.ReadByte();
				}
			}

			// If at least one of the events is downhill drift, read only 3 opponents
			else
			{
				strr.BaseStream.Position = br.ReadUInt16();
				this.OPPONENT1.Name = strr.ReadNullTermUTF8();
				this.OPPONENT1.StatsMultiplier = br.ReadUInt16();
				this.OPPONENT1.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
				this.OPPONENT1.SkillEasy = br.ReadByte();
				this.OPPONENT1.SkillMedium = br.ReadByte();
				this.OPPONENT1.SkillHard = br.ReadByte();
				this.OPPONENT1.CatchUp = br.ReadByte();

				strr.BaseStream.Position = br.ReadUInt16();
				this.OPPONENT2.Name = strr.ReadNullTermUTF8();
				this.OPPONENT2.StatsMultiplier = br.ReadUInt16();
				this.OPPONENT2.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
				this.OPPONENT2.SkillEasy = br.ReadByte();
				this.OPPONENT2.SkillMedium = br.ReadByte();
				this.OPPONENT2.SkillHard = br.ReadByte();
				this.OPPONENT2.CatchUp = br.ReadByte();

				strr.BaseStream.Position = br.ReadUInt16();
				this.OPPONENT3.Name = strr.ReadNullTermUTF8();
				this.OPPONENT3.StatsMultiplier = br.ReadUInt16();
				this.OPPONENT3.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
				this.OPPONENT3.SkillEasy = br.ReadByte();
				this.OPPONENT3.SkillMedium = br.ReadByte();
				this.OPPONENT3.SkillHard = br.ReadByte();
				this.OPPONENT3.CatchUp = br.ReadByte();
			}

			br.BaseStream.Position = position;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new GCareerRace(CName, this.Database)
			{
				IntroMovie = this.IntroMovie,
				OutroMovie = this.OutroMovie,
				EventTrigger = this.EventTrigger,
				UnlockMethod = this.UnlockMethod,
				IsSUVRace = this.IsSUVRace,
				IsHiddenEvent = this.IsHiddenEvent,
				IsDriveToGPS = this.IsDriveToGPS,
				EventBehaviorType = this.EventBehaviorType,
				RequiredSpecificRaceWon = this.RequiredSpecificRaceWon,
				RequiredRacesWon = this.RequiredRacesWon,
				RequiredSpecificURLWon = this.RequiredSpecificURLWon,
				RequiredURLWon = this.RequiredURLWon,
				SponsorChosenToUnlock = this.SponsorChosenToUnlock,
				EarnableRespect = this.EarnableRespect,
				TrackID_Stage1 = this.TrackID_Stage1,
				TrackID_Stage2 = this.TrackID_Stage2,
				TrackID_Stage3 = this.TrackID_Stage3,
				TrackID_Stage4 = this.TrackID_Stage4,
				InReverseDirection_Stage1 = this.InReverseDirection_Stage1,
				InReverseDirection_Stage2 = this.InReverseDirection_Stage2,
				InReverseDirection_Stage3 = this.InReverseDirection_Stage3,
				InReverseDirection_Stage4 = this.InReverseDirection_Stage4,
				NumLaps_Stage1 = this.NumLaps_Stage1,
				NumLaps_Stage2 = this.NumLaps_Stage2,
				NumLaps_Stage3 = this.NumLaps_Stage3,
				NumLaps_Stage4 = this.NumLaps_Stage4,
				PlayerCarType = this.PlayerCarType,
				CashValue = this.CashValue,
				EventIconType = this.EventIconType,
				DifficultyLevel = this.DifficultyLevel,
				BelongsToStage = this.BelongsToStage,
				NumMapItems = this.NumMapItems,
				Unknown0x3A = this.Unknown0x3A,
				Unknown0x3B = this.Unknown0x3B,
				NumOpponents = this.NumOpponents,
				NumStages = this.NumStages,
				UnknownDragValue = this.UnknownDragValue,
				GPSDestination = this.GPSDestination,
				OPPONENT1 = this.OPPONENT1.PlainCopy(),
				OPPONENT2 = this.OPPONENT2.PlainCopy(),
				OPPONENT3 = this.OPPONENT3.PlainCopy(),
				OPPONENT4 = this.OPPONENT4.PlainCopy(),
				OPPONENT5 = this.OPPONENT5.PlainCopy(),
				_padding0 = this._padding0,
				_padding1 = this._padding1,
				_padding2 = this._padding2,
				_padding3 = this._padding3
			};

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
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}

		#endregion
	}
}