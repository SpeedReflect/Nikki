using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground1.Parts.GameParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Gameplay
{
	/// <summary>
	/// <see cref="GCareerRace"/> is a collection of settings related to career races and events.
	/// </summary>
	public class GCareerRace : ACollectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground1;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground1.ToString();

		/// <summary>
		/// Database to which the class belongs to.
		/// </summary>
		public Database.Underground1 Database { get; set; }

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
				if (!Byte.TryParse(value, out byte id))
					throw new Exception("Unable to parse event ID from CollectionName provided.");
				if (this.Database.GCareerRaces.FindCollection(value) != null)
					throw new CollectionExistenceException();
				this._collection_name = value;
				this.ID = id;
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
		/// ID of this race.
		/// </summary>
		public byte ID { get; set; }

		/// <summary>
		/// Career race behavior.
		/// </summary>
		[AccessModifiable()]
		public eCareerRaceBehavior RaceBehavior { get; set; }

		/// <summary>
		/// Career race type.
		/// </summary>
		[AccessModifiable()]
		public eCareerRaceType RaceType { get; set; }

		/// <summary>
		/// Unknown value at offset 0x0C.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x0C { get; set; }

		/// <summary>
		/// Unknown value at offset 0x10.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x10 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x14.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x14 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x18.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x18 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x1C.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x1C { get; set; }

		/// <summary>
		/// Unknown value at offset 0x20.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x20 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x24.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x24 { get; set; }

		/// <summary>
		/// Key of the first opponent.
		/// </summary>
		[AccessModifiable()]
		public uint Opponent1Key { get; set; }

		/// <summary>
		/// Key of the second opponent.
		/// </summary>
		[AccessModifiable()]
		public uint Opponent2Key { get; set; }

		/// <summary>
		/// Key of the third opponent.
		/// </summary>
		[AccessModifiable()]
		public uint Opponent3Key { get; set; }

		/// <summary>
		/// Some switch value at offset 0x34.
		/// </summary>
		[AccessModifiable()]
		public int SwitchValue { get; set; }

		/// <summary>
		/// Unlock 1 upon completion of this race.
		/// </summary>
		[Expandable("Unlocks")]
		public Unlock UNLOCK1 { get; set; }

		/// <summary>
		/// Unlock 2 upon completion of this race.
		/// </summary>
		[Expandable("Unlocks")]
		public Unlock UNLOCK2 { get; set; }

		/// <summary>
		/// Unlock 3 upon completion of this race.
		/// </summary>
		[Expandable("Unlocks")]
		public Unlock UNLOCK3 { get; set; }

		/// <summary>
		/// Unlock 4 upon completion of this race.
		/// </summary>
		[Expandable("Unlocks")]
		public Unlock UNLOCK4 { get; set; }

		/// <summary>
		/// Unlock 5 upon completion of this race.
		/// </summary>
		[Expandable("Unlocks")]
		public Unlock UNLOCK5 { get; set; }

		/// <summary>
		/// Stage 1 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE1 { get; set; }

		/// <summary>
		/// Stage 2 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE2 { get; set; }

		/// <summary>
		/// Stage 3 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE3 { get; set; }

		/// <summary>
		/// Stage 4 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE4 { get; set; }

		/// <summary>
		/// Stage 5 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE5 { get; set; }

		/// <summary>
		/// Stage 6 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE6 { get; set; }

		/// <summary>
		/// Stage 7 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE7 { get; set; }

		/// <summary>
		/// Stage 8 of this race.
		/// </summary>
		[Expandable("Stages")]
		public Stage STAGE8 { get; set; }

		/// <summary>
		/// Number of unlocks upon completion of this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public int NumberOfUnlocks { get; set; }

		/// <summary>
		/// Number of stages in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public int NumberOfStages { get; set; }

		/// <summary>
		/// Race ID which unlocks this race.
		/// </summary>
		[AccessModifiable()]
		public int UnlockedByRace { get; set; }

		/// <summary>
		/// Unknown value at offset 0xA0.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0xA0 { get; set; }

		/// <summary>
		/// Unknown value at offset 0xA4.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0xA4 { get; set; }

		/// <summary>
		/// True if event is valid and should be shown on the map; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eBoolean IsValidEvent { get; set; }

		/// <summary>
		/// True if initially locked; false otherwise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean InitiallyLockedMayb { get; set; }

		/// <summary>
		/// Intro movie shown at the beginning of the race.
		/// </summary>
		[AccessModifiable()]
		public string IntroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Inter movie shown in the middle of the race.
		/// </summary>
		[AccessModifiable()]
		public string InterMovie { get; set; } = String.Empty;

		/// <summary>
		/// Outro movie shown at the end of the race.
		/// </summary>
		[AccessModifiable()]
		public string OutroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Preset ride that player uses in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PlayerCarType { get; set; } = String.Empty;

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
		/// Number of opponents in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public int NumberOfOpponents { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x12C.
		/// </summary>
		[AccessModifiable()]
		public float Unknown0x12C { get; set; }

		/// <summary>
		/// Some key value at offset 0x130.
		/// </summary>
		[AccessModifiable()]
		public uint SomeKey { get; set; }

		/// <summary>
		/// Allows traffic.
		/// </summary>
		[AccessModifiable()]
		public int AllowTrafficMayb { get; set; }

		/// <summary>
		/// Unknown integer value at offset 0x138.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x138 { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x13C.
		/// </summary>
		[AccessModifiable()]
		public float Unknown0x13C { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x140.
		/// </summary>
		[AccessModifiable()]
		public float Unknown0x140 { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x144.
		/// </summary>
		[AccessModifiable()]
		public float Unknown0x144 { get; set; }

		/// <summary>
		/// Unknown integer value at offset 0x148.
		/// </summary>
		[AccessModifiable()]
		public int Unknown0x148 { get; set; }

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
		/// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
		public GCareerRace(string CName, Database.Underground1 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			this.Initialize();
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRace"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
		public GCareerRace(BinaryReader br, Database.Underground1 db)
		{
			this.Database = db;
			this.Initialize();
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
		public void Assemble(BinaryWriter bw)
		{
			bw.Write((int)this.ID);
			bw.WriteEnum(this.RaceType);
			bw.WriteEnum(this.RaceBehavior);
			bw.Write(this.Unknown0x0C);
			bw.Write(this.Unknown0x10);
			bw.Write(this.Unknown0x14);
			bw.Write(this.Unknown0x18);
			bw.Write(this.Unknown0x1C);
			bw.Write(this.Unknown0x20);
			bw.Write(this.Unknown0x24);
			bw.Write(this.Opponent1Key);
			bw.Write(this.Opponent2Key);
			bw.Write(this.Opponent3Key);
			bw.Write(this.SwitchValue);
			this.UNLOCK1.Write(bw);
			this.UNLOCK2.Write(bw);
			this.UNLOCK3.Write(bw);
			this.UNLOCK4.Write(bw);
			this.UNLOCK5.Write(bw);
			bw.Write(this.NumberOfUnlocks);
			this.STAGE1.Write(bw);
			this.STAGE2.Write(bw);
			this.STAGE3.Write(bw);
			this.STAGE4.Write(bw);
			this.STAGE5.Write(bw);
			this.STAGE6.Write(bw);
			this.STAGE7.Write(bw);
			this.STAGE8.Write(bw);
			bw.Write(this.NumberOfStages);
			bw.Write(this.UnlockedByRace);
			bw.Write(this.Unknown0xA0);
			bw.Write(this.Unknown0xA4);
			bw.Write((int)this.IsValidEvent);
			bw.Write((int)this.InitiallyLockedMayb);
			bw.WriteNullTermUTF8(this.IntroMovie, 0xC);
			bw.WriteNullTermUTF8(this.InterMovie, 0xC);
			bw.WriteNullTermUTF8(this.OutroMovie, 0xC);
			bw.Write(this.PlayerCarType.BinHash());
			this.OPPONENT1.Write(bw);
			this.OPPONENT2.Write(bw);
			this.OPPONENT3.Write(bw);
			bw.Write(this.NumberOfOpponents);
			bw.Write(this.Unknown0x12C);
			bw.Write(this.SomeKey);
			bw.Write(this.AllowTrafficMayb);
			bw.Write(this.Unknown0x138);
			bw.Write(this.Unknown0x13C);
			bw.Write(this.Unknown0x140);
			bw.Write(this.Unknown0x144);
			bw.Write(this.Unknown0x148);			
		}

		/// <summary>
		/// Disassembles array into <see cref="GCareerRace"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCareerRace"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			// Collection Name
			this._collection_name = br.ReadInt32().ToString("X2");

			// Settings
			this.RaceType = br.ReadEnum<eCareerRaceType>();
			this.RaceBehavior = br.ReadEnum<eCareerRaceBehavior>();
			this.Unknown0x0C = br.ReadInt32();
			this.Unknown0x10 = br.ReadInt32();
			this.Unknown0x14 = br.ReadInt32();
			this.Unknown0x18 = br.ReadInt32();
			this.Unknown0x1C = br.ReadInt32();
			this.Unknown0x20 = br.ReadInt32();
			this.Unknown0x24 = br.ReadInt32();
			this.Opponent1Key = br.ReadUInt32();
			this.Opponent2Key = br.ReadUInt32();
			this.Opponent3Key = br.ReadUInt32();
			this.SwitchValue = br.ReadInt32();
			this.UNLOCK1.Read(br);
			this.UNLOCK2.Read(br);
			this.UNLOCK3.Read(br);
			this.UNLOCK4.Read(br);
			this.UNLOCK5.Read(br);
			this.NumberOfUnlocks = br.ReadInt32();
			this.STAGE1.Read(br);
			this.STAGE2.Read(br);
			this.STAGE3.Read(br);
			this.STAGE4.Read(br);
			this.STAGE5.Read(br);
			this.STAGE6.Read(br);
			this.STAGE7.Read(br);
			this.STAGE8.Read(br);
			this.NumberOfStages = br.ReadInt32();
			this.UnlockedByRace = br.ReadInt32();
			this.Unknown0xA0 = br.ReadInt32();
			this.Unknown0xA4 = br.ReadInt32();
			this.IsValidEvent = (eBoolean)br.ReadInt32();
			this.InitiallyLockedMayb = (eBoolean)br.ReadInt32();
			this.IntroMovie = br.ReadNullTermUTF8(0xC);
			this.InterMovie = br.ReadNullTermUTF8(0xC);
			this.OutroMovie = br.ReadNullTermUTF8(0xC);
			this.PlayerCarType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.OPPONENT1.Read(br);
			this.OPPONENT2.Read(br);
			this.OPPONENT3.Read(br);
			this.NumberOfOpponents = br.ReadInt32();
			this.Unknown0x12C = br.ReadInt32();
			this.SomeKey = br.ReadUInt32();
			this.AllowTrafficMayb = br.ReadInt32();
			this.Unknown0x138 = br.ReadInt32();
			this.Unknown0x13C = br.ReadSingle();
			this.Unknown0x140 = br.ReadSingle();
			this.Unknown0x144 = br.ReadSingle();
			this.Unknown0x148 = br.ReadInt32();
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
				AllowTrafficMayb = this.AllowTrafficMayb,
				InitiallyLockedMayb = this.InitiallyLockedMayb,
				InterMovie = this.InterMovie,
				IntroMovie = this.IntroMovie,
				IsValidEvent = this.IsValidEvent,
				NumberOfOpponents = this.NumberOfOpponents,
				NumberOfStages = this.NumberOfStages,
				NumberOfUnlocks = this.NumberOfUnlocks,
				Opponent1Key = this.Opponent1Key,
				Opponent2Key = this.Opponent2Key,
				Opponent3Key = this.Opponent3Key,
				OutroMovie = this.OutroMovie,
				PlayerCarType = this.PlayerCarType,
				RaceBehavior = this.RaceBehavior,
				RaceType = this.RaceType,
				SomeKey = this.SomeKey,
				SwitchValue = this.SwitchValue,
				UnlockedByRace = this.UnlockedByRace,
				Unknown0x0C = this.Unknown0x0C,
				Unknown0x10 = this.Unknown0x10,
				Unknown0x14 = this.Unknown0x14,
				Unknown0x18 = this.Unknown0x18,
				Unknown0x1C = this.Unknown0x1C,
				Unknown0x20 = this.Unknown0x20,
				Unknown0x24 = this.Unknown0x24,
				Unknown0xA0 = this.Unknown0xA0,
				Unknown0xA4 = this.Unknown0xA4,
				Unknown0x12C = this.Unknown0x12C,
				Unknown0x138 = this.Unknown0x138,
				Unknown0x13C = this.Unknown0x13C,
				Unknown0x140 = this.Unknown0x140,
				Unknown0x144 = this.Unknown0x144,
				Unknown0x148 = this.Unknown0x148,
				OPPONENT1 = this.OPPONENT1.PlainCopy(),
				OPPONENT2 = this.OPPONENT2.PlainCopy(),
				OPPONENT3 = this.OPPONENT3.PlainCopy(),
				STAGE1 = this.STAGE1.PlainCopy(),
				STAGE2 = this.STAGE2.PlainCopy(),
				STAGE3 = this.STAGE3.PlainCopy(),
				STAGE4 = this.STAGE4.PlainCopy(),
				STAGE5 = this.STAGE5.PlainCopy(),
				STAGE6 = this.STAGE6.PlainCopy(),
				STAGE7 = this.STAGE7.PlainCopy(),
				STAGE8 = this.STAGE8.PlainCopy(),
				UNLOCK1 = this.UNLOCK1.PlainCopy(),
				UNLOCK2 = this.UNLOCK2.PlainCopy(),
				UNLOCK3 = this.UNLOCK3.PlainCopy(),
				UNLOCK4 = this.UNLOCK4.PlainCopy(),
				UNLOCK5 = this.UNLOCK5.PlainCopy(),
			};

			return result;
		}

		private void Initialize()
		{
			this.OPPONENT1 = new Opponent();
			this.OPPONENT2 = new Opponent();
			this.OPPONENT3 = new Opponent();
			this.STAGE1 = new Stage();
			this.STAGE2 = new Stage();
			this.STAGE3 = new Stage();
			this.STAGE4 = new Stage();
			this.STAGE5 = new Stage();
			this.STAGE6 = new Stage();
			this.STAGE7 = new Stage();
			this.STAGE8 = new Stage();
			this.UNLOCK1 = new Unlock();
			this.UNLOCK2 = new Unlock();
			this.UNLOCK3 = new Unlock();
			this.UNLOCK4 = new Unlock();
			this.UNLOCK5 = new Unlock();
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
