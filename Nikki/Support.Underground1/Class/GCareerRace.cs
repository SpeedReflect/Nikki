using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground1.Framework;
using Nikki.Support.Underground1.Parts.GameParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground1.Class
{
	/// <summary>
	/// <see cref="GCareerRace"/> is a collection of settings related to career races and events.
	/// </summary>
	public class GCareerRace : Collectable
	{
		#region Fields

		private string _collection_name;

		/// <summary>
		/// Maximum length of the CollectionName.
		/// </summary>
		public const int MaxCNameLength = 0x04;

		/// <summary>
		/// Offset of the CollectionName in the data.
		/// </summary>
		public const int CNameOffsetAt = 0x00;

		/// <summary>
		/// Base size of a unit collection.
		/// </summary>
		public const int BaseClassSize = 0x14C;

		#endregion

		#region Enums

		/// <summary>
		/// Enum of career race behavior types in Underground 1.
		/// </summary>
		public enum CareerRaceBehavior : int
		{
			/// <summary>
			/// Regular race behavior.
			/// </summary>
			Regular = 0,

			/// <summary>
			/// Tournament behavior type.
			/// </summary>
			Tournament = 1,

			/// <summary>
			/// Regular behavior 2.
			/// </summary>
			ChallengeTJ = 2,

			/// <summary>
			/// Regular behavior 3.
			/// </summary>
			ChallengeSamantha = 3, // Samantha Challenge

			/// <summary>
			/// Regular behavior 4.
			/// </summary>
			ChallengeMelissa = 4, // Melissa Challenge

			/// <summary>
			/// Regular behavior 5.
			/// </summary>
			ChallengeEddie = 5, // Eddie Challenge

			/// <summary>
			/// Advances player in rating table.
			/// </summary>
			RankAdvancement = 6,

			/// <summary>
			/// Unknown.
			/// </summary>
			Hmmm = 7,

			/// <summary>
			/// Invalid and unsupported race.
			/// </summary>
			InvalidRace = 8,
		}

		/// <summary>
		/// Enum of career race types in Underground 1.
		/// </summary>
		public enum CareerRaceType : int
		{
			/// <summary>
			/// Circuit race.
			/// </summary>
			Circuit = 0,

			/// <summary>
			/// Drag race.
			/// </summary>
			Drag = 1,

			/// <summary>
			/// Sprint race.
			/// </summary>
			Sprint = 2,

			/// <summary>
			/// Drift race.
			/// </summary>
			Drift = 3,

			/// <summary>
			/// Time Trial race.
			/// </summary>
			TimeTrial = 4,

			/// <summary>
			/// Lap Knockout race.
			/// </summary>
			LapKnockout = 5,
		}

		/// <summary>
		/// Enum of career rating type in Underground 1.
		/// </summary>
		public enum RatingType : short
		{
			/// <summary>
			/// Circuit rating table.
			/// </summary>
			Circuit = 0,

			/// <summary>
			/// Sprint rating table.
			/// </summary>
			Sprint = 1,

			/// <summary>
			/// Drag rating table.
			/// </summary>
			Drag = 2,

			/// <summary>
			/// Drift rating table.
			/// </summary>
			Drift = 3,
		}

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Underground1;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Underground1.ToString();

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
		/// Career race behavior.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Settings")]
		public CareerRaceBehavior RaceBehavior { get; set; }

		/// <summary>
		/// Career race type.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Settings")]
		public CareerRaceType RaceType { get; set; }

		/// <summary>
		/// Number of stars required to unlock this race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Counts")]
		public int StarsRequired { get; set; }

		/// <summary>
		/// Time limit to complete this race on easy difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Difficulty")]
		public float TimeLimitEasy { get; set; }

		/// <summary>
		/// Time limit to complete this race on normal difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Difficulty")]
		public float TimeLimitNormal { get; set; }

		/// <summary>
		/// Time limit to complete this race on hard difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Difficulty")]
		public float TimeLimitHard { get; set; }

		/// <summary>
		/// Cash value won on easy difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Difficulty")]
		public float CashValueEasy { get; set; }

		/// <summary>
		/// Cash value won on normal difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Difficulty")]
		public float CashValueNormal { get; set; }

		/// <summary>
		/// Cash value won on hard difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Difficulty")]
		public float CashValueHard { get; set; }

		/// <summary>
		/// Rating advancement type of this race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Rating")]
		public RatingType RankType { get; set; }

		/// <summary>
		/// Position in the rating table.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Rating")]
		public short RankRating { get; set; }

		/// <summary>
		/// Unlock 1 upon completion of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Unlocks")]
		public Unlock UNLOCK1 { get; set; }

		/// <summary>
		/// Unlock 2 upon completion of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Unlocks")]
		public Unlock UNLOCK2 { get; set; }

		/// <summary>
		/// Unlock 3 upon completion of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Unlocks")]
		public Unlock UNLOCK3 { get; set; }

		/// <summary>
		/// Unlock 4 upon completion of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Unlocks")]
		public Unlock UNLOCK4 { get; set; }

		/// <summary>
		/// Unlock 5 upon completion of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Unlocks")]
		public Unlock UNLOCK5 { get; set; }

		/// <summary>
		/// Stage 1 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE1 { get; set; }

		/// <summary>
		/// Stage 2 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE2 { get; set; }

		/// <summary>
		/// Stage 3 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE3 { get; set; }

		/// <summary>
		/// Stage 4 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE4 { get; set; }

		/// <summary>
		/// Stage 5 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE5 { get; set; }

		/// <summary>
		/// Stage 6 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE6 { get; set; }

		/// <summary>
		/// Stage 7 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE7 { get; set; }

		/// <summary>
		/// Stage 8 of this race.
		/// </summary>
		[Browsable(false)]
		[Expandable("Stages")]
		public Stage STAGE8 { get; set; }

		/// <summary>
		/// Number of unlocks upon completion of this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Counts")]
		public int NumberOfUnlocks { get; set; }

		/// <summary>
		/// Number of stages in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Counts")]
		public int NumberOfStages { get; set; }

		/// <summary>
		/// First required race won to unlock this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Requirements")]
		public short RequiredRaceWon1 { get; set; }

		/// <summary>
		/// Second required race won to unlock this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Requirements")]
		public short RequiredRaceWon2 { get; set; }

		/// <summary>
		/// Third required race won to unlock this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Requirements")]
		public short RequiredRaceWon3 { get; set; }

		/// <summary>
		/// Fourth required race won to unlock this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Requirements")]
		public short RequiredRaceWon4 { get; set; }

		/// <summary>
		/// Fifth required race won to unlock this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Requirements")]
		public short RequiredRaceWon5 { get; set; }

		/// <summary>
		/// Sixth required race won to unlock this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Requirements")]
		public short RequiredRaceWon6 { get; set; }

		/// <summary>
		/// True if event is valid and should be shown on the map; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean IsValidEvent { get; set; }

		/// <summary>
		/// True if initially locked; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Counts")]
		public short RequiredRacesWon { get; set; }

		/// <summary>
		/// Intro movie shown at the beginning of the race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Movies")]
		public string IntroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Inter movie shown in the middle of the race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Movies")]
		public string InterMovie { get; set; } = String.Empty;

		/// <summary>
		/// Outro movie shown at the end of the race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Movies")]
		public string OutroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Preset ride that player uses in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Settings")]
		public string PlayerCarType { get; set; } = String.Empty;

		/// <summary>
		/// Opponent 1 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT1 { get; set; }

		/// <summary>
		/// Opponent 2 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT2 { get; set; }

		/// <summary>
		/// Opponent 3 settings.
		/// </summary>
		[Browsable(false)]
		[Expandable("Opponents")]
		public Opponent OPPONENT3 { get; set; }

		/// <summary>
		/// Number of opponents in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Counts")]
		public int NumberOfOpponents { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x12C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float Unknown0x12C { get; set; }

		/// <summary>
		/// Caller photo name.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string CallerPhoto { get; set; }

		/// <summary>
		/// Allows traffic.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Settings")]
		public int TrafficLevel { get; set; }

		/// <summary>
		/// Unknown integer value at offset 0x138.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x138 { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x13C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float Unknown0x13C { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x140.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float Unknown0x140 { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x144.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float Unknown0x144 { get; set; }

		/// <summary>
		/// Unknown integer value at offset 0x148.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x148 { get; set; }

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
		/// <param name="manager"><see cref="GCareerRaceManager"/> to which this instance belongs to.</param>
		public GCareerRace(BinaryReader br, GCareerRaceManager manager) : this()
		{
			this.Manager = manager;
			this.Disassemble(br);
			this.CollectionName.BinHash();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="GCareerRace"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="GCareerRace"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			bw.Write(Int32.Parse(this._collection_name));
			bw.WriteEnum(this.RaceBehavior);
			bw.WriteEnum(this.RaceType);
			bw.Write((long)0);
			bw.Write((int)0);
			bw.Write(this.StarsRequired);
			bw.Write(this.TimeLimitEasy);
			bw.Write(this.TimeLimitNormal);
			bw.Write(this.TimeLimitHard);
			bw.Write(this.CashValueEasy);
			bw.Write(this.CashValueNormal);
			bw.Write(this.CashValueHard);
			bw.WriteEnum(this.RankType);
			bw.Write(this.RankRating);
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
			bw.Write(this.RequiredRaceWon1);
			bw.Write(this.RequiredRaceWon2);
			bw.Write(this.RequiredRaceWon3);
			bw.Write(this.RequiredRaceWon4);
			bw.Write(this.RequiredRaceWon5);
			bw.Write(this.RequiredRaceWon6);
			bw.Write((short)this.IsValidEvent);
			bw.Write((short)this.RequiredRacesWon);
			bw.WriteNullTermUTF8(this.IntroMovie, 0xC);
			bw.WriteNullTermUTF8(this.InterMovie, 0xC);
			bw.WriteNullTermUTF8(this.OutroMovie, 0xC);
			bw.Write(this.PlayerCarType.BinHash());
			this.OPPONENT1.Write(bw);
			this.OPPONENT2.Write(bw);
			this.OPPONENT3.Write(bw);
			bw.Write(this.NumberOfOpponents);
			bw.Write(this.Unknown0x12C);
			bw.Write(this.CallerPhoto.BinHash());
			bw.Write(this.TrafficLevel);
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
			this._collection_name = br.ReadInt32().ToString();

			// Settings
			this.RaceBehavior = br.ReadEnum<CareerRaceBehavior>();
			this.RaceType = br.ReadEnum<CareerRaceType>();
			br.BaseStream.Position += 0x0C;
			this.StarsRequired = br.ReadInt32();
			this.TimeLimitEasy = br.ReadSingle();
			this.TimeLimitNormal = br.ReadSingle();
			this.TimeLimitHard = br.ReadSingle();
			this.CashValueEasy = br.ReadSingle();
			this.CashValueNormal = br.ReadSingle();
			this.CashValueHard = br.ReadSingle();
			this.RankType = br.ReadEnum<RatingType>();
			this.RankRating = br.ReadInt16();
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
			this.RequiredRaceWon1 = br.ReadInt16();
			this.RequiredRaceWon2 = br.ReadInt16();
			this.RequiredRaceWon3 = br.ReadInt16();
			this.RequiredRaceWon4 = br.ReadInt16();
			this.RequiredRaceWon5 = br.ReadInt16();
			this.RequiredRaceWon6 = br.ReadInt16();
			this.IsValidEvent = (eBoolean)br.ReadInt16();
			this.RequiredRacesWon = br.ReadInt16();
			this.IntroMovie = br.ReadNullTermUTF8(0xC);
			this.InterMovie = br.ReadNullTermUTF8(0xC);
			this.OutroMovie = br.ReadNullTermUTF8(0xC);
			this.PlayerCarType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.OPPONENT1.Read(br);
			this.OPPONENT2.Read(br);
			this.OPPONENT3.Read(br);
			this.NumberOfOpponents = br.ReadInt32();
			this.Unknown0x12C = br.ReadSingle();
			this.CallerPhoto = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.TrafficLevel = br.ReadInt32();
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

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Serialize(BinaryWriter bw)
		{
			byte[] array;
			using (var ms = new MemoryStream(0x2000))
			using (var writer = new BinaryWriter(ms))
			{

				// CollectionName
				writer.WriteNullTermUTF8(this._collection_name);

				writer.WriteEnum(this.RaceBehavior);
				writer.WriteEnum(this.RaceType);
				writer.Write(this.StarsRequired);
				writer.Write(this.TimeLimitEasy);
				writer.Write(this.TimeLimitNormal);
				writer.Write(this.TimeLimitHard);
				writer.Write(this.CashValueEasy);
				writer.Write(this.CashValueNormal);
				writer.Write(this.CashValueHard);
				writer.WriteEnum(this.RankType);
				writer.Write(this.RankRating);

				this.UNLOCK1.Serialize(writer);
				this.UNLOCK2.Serialize(writer);
				this.UNLOCK3.Serialize(writer);
				this.UNLOCK4.Serialize(writer);
				this.UNLOCK5.Serialize(writer);
				
				writer.Write(this.NumberOfUnlocks);
				
				this.STAGE1.Write(writer);
				this.STAGE2.Write(writer);
				this.STAGE3.Write(writer);
				this.STAGE4.Write(writer);
				this.STAGE5.Write(writer);
				this.STAGE6.Write(writer);
				this.STAGE7.Write(writer);
				this.STAGE8.Write(writer);

				writer.Write(this.NumberOfStages);
				writer.Write(this.RequiredRaceWon1);
				writer.Write(this.RequiredRaceWon2);
				writer.Write(this.RequiredRaceWon3);
				writer.Write(this.RequiredRaceWon4);
				writer.Write(this.RequiredRaceWon5);
				writer.Write(this.RequiredRaceWon6);
				writer.WriteEnum(this.IsValidEvent);
				writer.Write(this.RequiredRacesWon);
				writer.WriteNullTermUTF8(this.IntroMovie);
				writer.WriteNullTermUTF8(this.InterMovie);
				writer.WriteNullTermUTF8(this.OutroMovie);
				writer.WriteNullTermUTF8(this.PlayerCarType);
				
				this.OPPONENT1.Serialize(writer);
				this.OPPONENT2.Serialize(writer);
				this.OPPONENT3.Serialize(writer);
				
				writer.Write(this.NumberOfOpponents);
				writer.Write(this.Unknown0x12C);
				writer.WriteNullTermUTF8(this.CallerPhoto);
				writer.Write(this.TrafficLevel);
				writer.Write(this.Unknown0x138);
				writer.Write(this.Unknown0x13C);
				writer.Write(this.Unknown0x140);
				writer.Write(this.Unknown0x144);
				writer.Write(this.Unknown0x148);

				array = ms.ToArray();

			}

			array = Interop.Compress(array, LZCompressionType.RAWW);

			var header = new SerializationHeader(array.Length, this.GameINT, this.Manager.Name);
			header.Write(bw);
			bw.Write(array.Length);
			bw.Write(array);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			int size = br.ReadInt32();
			var array = br.ReadBytes(size);

			array = Interop.Decompress(array);

			using var ms = new MemoryStream(array);
			using var reader = new BinaryReader(ms);

			// CollectionName
			this._collection_name = reader.ReadNullTermUTF8();

			this.RaceBehavior = reader.ReadEnum<CareerRaceBehavior>();
			this.RaceType = reader.ReadEnum<CareerRaceType>();
			this.StarsRequired = reader.ReadInt32();
			this.TimeLimitEasy = reader.ReadSingle();
			this.TimeLimitNormal = reader.ReadSingle();
			this.TimeLimitHard = reader.ReadSingle();
			this.CashValueEasy = reader.ReadSingle();
			this.CashValueNormal = reader.ReadSingle();
			this.CashValueHard = reader.ReadSingle();
			this.RankType = reader.ReadEnum<RatingType>();
			this.RankRating = reader.ReadInt16();

			this.UNLOCK1.Deserialize(reader);
			this.UNLOCK2.Deserialize(reader);
			this.UNLOCK3.Deserialize(reader);
			this.UNLOCK4.Deserialize(reader);
			this.UNLOCK5.Deserialize(reader);

			this.NumberOfUnlocks = reader.ReadInt32();

			this.STAGE1.Read(reader);
			this.STAGE2.Read(reader);
			this.STAGE3.Read(reader);
			this.STAGE4.Read(reader);
			this.STAGE5.Read(reader);
			this.STAGE6.Read(reader);
			this.STAGE7.Read(reader);
			this.STAGE8.Read(reader);

			this.NumberOfStages = reader.ReadInt32();
			this.RequiredRaceWon1 = reader.ReadInt16();
			this.RequiredRaceWon2 = reader.ReadInt16();
			this.RequiredRaceWon3 = reader.ReadInt16();
			this.RequiredRaceWon4 = reader.ReadInt16();
			this.RequiredRaceWon5 = reader.ReadInt16();
			this.RequiredRaceWon6 = reader.ReadInt16();
			this.IsValidEvent = reader.ReadEnum<eBoolean>();
			this.RequiredRacesWon = reader.ReadInt16();
			this.IntroMovie = reader.ReadNullTermUTF8();
			this.InterMovie = reader.ReadNullTermUTF8();
			this.OutroMovie = reader.ReadNullTermUTF8();
			this.PlayerCarType = reader.ReadNullTermUTF8();

			this.OPPONENT1.Deserialize(reader);
			this.OPPONENT2.Deserialize(reader);
			this.OPPONENT3.Deserialize(reader);

			this.NumberOfOpponents = reader.ReadInt32();
			this.Unknown0x12C = reader.ReadSingle();
			this.CallerPhoto = reader.ReadNullTermUTF8();
			this.TrafficLevel = reader.ReadInt32();
			this.Unknown0x138 = reader.ReadInt32();
			this.Unknown0x13C = reader.ReadSingle();
			this.Unknown0x140 = reader.ReadSingle();
			this.Unknown0x144 = reader.ReadSingle();
			this.Unknown0x148 = reader.ReadInt32();
		}

		#endregion
	}
}
