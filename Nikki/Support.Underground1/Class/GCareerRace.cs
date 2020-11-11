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
			/// Tournament behavior type.
			/// </summary>
			Tournament = 1,

			/// <summary>
			/// Regular behavior 2.
			/// </summary>
			Regular2 = 2,

			/// <summary>
			/// Regular behavior 3.
			/// </summary>
			Regular3 = 3,

			/// <summary>
			/// Regular behavior 4.
			/// </summary>
			Regular4 = 4,

			/// <summary>
			/// Regular behavior 5.
			/// </summary>
			Regular5 = 5,

			/// <summary>
			/// Regular behavior 6.
			/// </summary>
			Regular6 = 6,

			/// <summary>
			/// Unknown.
			/// </summary>
			Hmmm = 7,

			/// <summary>
			/// Crash game.
			/// </summary>
			CrashGame = 8,
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
		[Category("Secondary")]
		public CareerRaceBehavior RaceBehavior { get; set; }

		/// <summary>
		/// Career race type.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public CareerRaceType RaceType { get; set; }

		/// <summary>
		/// Unknown value at offset 0x0C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x0C { get; set; }

		/// <summary>
		/// Unknown value at offset 0x10.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x10 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x14.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x14 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x18.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x18 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x1C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x1C { get; set; }

		/// <summary>
		/// Unknown value at offset 0x20.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x20 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x24.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0x24 { get; set; }

		/// <summary>
		/// Cash value won on easy difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float CashValueEasy { get; set; }

		/// <summary>
		/// Cash value won on normal difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float CashValueNormal { get; set; }

		/// <summary>
		/// Cash value won on hard difficulty.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float CashValueHard { get; set; }

		/// <summary>
		/// Some switch value at offset 0x34.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int SwitchValue { get; set; }

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
		[Category("Primary")]
		public int NumberOfUnlocks { get; set; }

		/// <summary>
		/// Number of stages in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int NumberOfStages { get; set; }

		/// <summary>
		/// Race ID which unlocks this race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int UnlockedByRace { get; set; }

		/// <summary>
		/// Unknown value at offset 0xA0.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0xA0 { get; set; }

		/// <summary>
		/// Unknown value at offset 0xA4.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int Unknown0xA4 { get; set; }

		/// <summary>
		/// True if event is valid and should be shown on the map; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public eBoolean IsValidEvent { get; set; }

		/// <summary>
		/// True if initially locked; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean InitiallyLockedMayb { get; set; }

		/// <summary>
		/// Intro movie shown at the beginning of the race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string IntroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Inter movie shown in the middle of the race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string InterMovie { get; set; } = String.Empty;

		/// <summary>
		/// Outro movie shown at the end of the race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string OutroMovie { get; set; } = String.Empty;

		/// <summary>
		/// Preset ride that player uses in this race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
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
		[Category("Primary")]
		public int NumberOfOpponents { get; set; }

		/// <summary>
		/// Unknown float value at offset 0x12C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float Unknown0x12C { get; set; }

		/// <summary>
		/// Some key value at offset 0x130.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public uint SomeKey { get; set; }

		/// <summary>
		/// Allows traffic.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int AllowTrafficMayb { get; set; }

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
			bw.Write(Int32.Parse(this._collection_name));
			bw.WriteEnum(this.RaceType);
			bw.WriteEnum(this.RaceBehavior);
			bw.Write(this.Unknown0x0C);
			bw.Write(this.Unknown0x10);
			bw.Write(this.Unknown0x14);
			bw.Write(this.Unknown0x18);
			bw.Write(this.Unknown0x1C);
			bw.Write(this.Unknown0x20);
			bw.Write(this.Unknown0x24);
			bw.Write(this.CashValueEasy);
			bw.Write(this.CashValueNormal);
			bw.Write(this.CashValueHard);
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
			bw.Write((short)this.IsValidEvent);
			bw.Write((short)this.InitiallyLockedMayb);
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
			this._collection_name = br.ReadInt32().ToString();

			// Settings
			this.RaceType = br.ReadEnum<CareerRaceType>();
			this.RaceBehavior = br.ReadEnum<CareerRaceBehavior>();
			this.Unknown0x0C = br.ReadInt32();
			this.Unknown0x10 = br.ReadInt32();
			this.Unknown0x14 = br.ReadInt32();
			this.Unknown0x18 = br.ReadInt32();
			this.Unknown0x1C = br.ReadInt32();
			this.Unknown0x20 = br.ReadInt32();
			this.Unknown0x24 = br.ReadInt32();
			this.CashValueEasy = br.ReadSingle();
			this.CashValueNormal = br.ReadSingle();
			this.CashValueHard = br.ReadSingle();
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
			this.IsValidEvent = (eBoolean)br.ReadInt16();
			this.InitiallyLockedMayb = (eBoolean)br.ReadInt16();
			this.IntroMovie = br.ReadNullTermUTF8(0xC);
			this.InterMovie = br.ReadNullTermUTF8(0xC);
			this.OutroMovie = br.ReadNullTermUTF8(0xC);
			this.PlayerCarType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.OPPONENT1.Read(br);
			this.OPPONENT2.Read(br);
			this.OPPONENT3.Read(br);
			this.NumberOfOpponents = br.ReadInt32();
			this.Unknown0x12C = br.ReadSingle();
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

				writer.WriteEnum(this.RaceType);
				writer.WriteEnum(this.RaceBehavior);
				writer.Write(this.Unknown0x0C);
				writer.Write(this.Unknown0x10);
				writer.Write(this.Unknown0x14);
				writer.Write(this.Unknown0x18);
				writer.Write(this.Unknown0x1C);
				writer.Write(this.Unknown0x20);
				writer.Write(this.Unknown0x24);
				writer.Write(this.CashValueEasy);
				writer.Write(this.CashValueNormal);
				writer.Write(this.CashValueHard);
				writer.Write(this.SwitchValue);

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
				writer.Write(this.UnlockedByRace);
				writer.Write(this.Unknown0xA0);
				writer.Write(this.Unknown0xA4);
				writer.WriteEnum(this.IsValidEvent);
				writer.WriteEnum(this.InitiallyLockedMayb);
				writer.WriteNullTermUTF8(this.IntroMovie);
				writer.WriteNullTermUTF8(this.InterMovie);
				writer.WriteNullTermUTF8(this.OutroMovie);
				writer.WriteNullTermUTF8(this.PlayerCarType);
				
				this.OPPONENT1.Serialize(writer);
				this.OPPONENT2.Serialize(writer);
				this.OPPONENT3.Serialize(writer);
				
				writer.Write(this.NumberOfOpponents);
				writer.Write(this.Unknown0x12C);
				writer.Write(this.SomeKey);
				writer.Write(this.AllowTrafficMayb);
				writer.Write(this.Unknown0x138);
				writer.Write(this.Unknown0x13C);
				writer.Write(this.Unknown0x140);
				writer.Write(this.Unknown0x144);
				writer.Write(this.Unknown0x148);

				array = ms.ToArray();

			}

			array = Interop.Compress(array, LZCompressionType.BEST);

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

			this.RaceType = reader.ReadEnum<CareerRaceType>();
			this.RaceBehavior = reader.ReadEnum<CareerRaceBehavior>();
			this.Unknown0x0C = reader.ReadInt32();
			this.Unknown0x10 = reader.ReadInt32();
			this.Unknown0x14 = reader.ReadInt32();
			this.Unknown0x18 = reader.ReadInt32();
			this.Unknown0x1C = reader.ReadInt32();
			this.Unknown0x20 = reader.ReadInt32();
			this.Unknown0x24 = reader.ReadInt32();
			this.CashValueEasy = reader.ReadSingle();
			this.CashValueNormal = reader.ReadSingle();
			this.CashValueHard = reader.ReadSingle();
			this.SwitchValue = reader.ReadInt32();

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
			this.UnlockedByRace = reader.ReadInt32();
			this.Unknown0xA0 = reader.ReadInt32();
			this.Unknown0xA4 = reader.ReadInt32();
			this.IsValidEvent = reader.ReadEnum<eBoolean>();
			this.InitiallyLockedMayb = reader.ReadEnum<eBoolean>();
			this.IntroMovie = reader.ReadNullTermUTF8();
			this.InterMovie = reader.ReadNullTermUTF8();
			this.OutroMovie = reader.ReadNullTermUTF8();
			this.PlayerCarType = reader.ReadNullTermUTF8();

			this.OPPONENT1.Deserialize(reader);
			this.OPPONENT2.Deserialize(reader);
			this.OPPONENT3.Deserialize(reader);

			this.NumberOfOpponents = reader.ReadInt32();
			this.Unknown0x12C = reader.ReadSingle();
			this.SomeKey = reader.ReadUInt32();
			this.AllowTrafficMayb = reader.ReadInt32();
			this.Unknown0x138 = reader.ReadInt32();
			this.Unknown0x13C = reader.ReadSingle();
			this.Unknown0x140 = reader.ReadSingle();
			this.Unknown0x144 = reader.ReadSingle();
			this.Unknown0x148 = reader.ReadInt32();
		}

		#endregion
	}
}
