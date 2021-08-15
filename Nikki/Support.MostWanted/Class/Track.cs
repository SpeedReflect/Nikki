using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.MostWanted.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.MostWanted.Class
{
	/// <summary>
	/// <see cref="Track"/> is a collection of settings related to races and events.
	/// </summary>
	public class Track : Shared.Class.Track
	{
		#region Fields

		private string _collection_name;

		/// <summary>
		/// Maximum length of the CollectionName.
		/// </summary>
		public const int MaxCNameLength = 0x02;

		/// <summary>
		/// Offset of the CollectionName in the data.
		/// </summary>
		public const int CNameOffsetAt = 0x8A;

		/// <summary>
		/// Base size of a unit collection.
		/// </summary>
		public const int BaseClassSize = 0x120;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.MostWanted;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.MostWanted.ToString();

		/// <summary>
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public TrackManager Manager { get; set; }

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
				this.Manager.CreationCheck(value);
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Location index of the track.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int LocationIndex { get; set; }

		/// <summary>
		/// Location directory of the track.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string LocationDirectory { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether the race is used in performance tuning events.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean IsPerformanceTuning { get; set; }

		/// <summary>
		/// Location type of the track.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public TrackLocationType LocationType { get; set; }

		/// <summary>
		/// Drift type of the track.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public TrackDriftType DriftType { get; set; }

		/// <summary>
		/// Total length of the whole track
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public uint RaceLength { get; set; }

		/// <summary>
		/// Indicates maximum time allowed to complete the race in forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TimeLimitToBeatForward { get; set; }

		/// <summary>
		/// Indicates maximum time allowed to complete the race in reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TimeLimitToBeatReverse { get; set; }

		/// <summary>
		/// Indicates score needed to beat the drift race in forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int ScoreToBeatDriftForward { get; set; }

		/// <summary>
		/// Indicates score needed to beat the drift race in reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int ScoreToBeatDriftReverse { get; set; }

		/// <summary>
		/// Indicates number of seconds that should pass before opponents can take first shortcut.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short NumSecBeforeShorcutsAllowed { get; set; }

		/// <summary>
		/// Indicates minimum amount of seconds to drift.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short DriftSecondsMin { get; set; }

		/// <summary>
		/// Indicates maximum amount of seconds to drift.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short DriftSecondsMax { get; set; }

		/// <summary>
		/// Indicates configuration settings of the car at the start.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short CarRaceStartConfig { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationOffsetX { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationOffsetY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationWidth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationRotation { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationZoomIn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapStartgridAngle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapFinishlineAngle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float MenuMapZoomOffsetX { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float MenuMapZoomOffsetY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float MenuMapZoomWidth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int MenuMapStartZoomed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_0_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_0_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_1_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_1_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_2_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_2_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_3_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_3_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte TrafAllowedNearStartgrid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte TrafAllowedNearFinishline { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistFromStart { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistFromFinish { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistInbetweenA { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistInbetweenB { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafOncomingFraction1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafOncomingFraction2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafOncomingFraction3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafOncomingFraction4 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="Track"/>.
		/// </summary>
		public Track() { }

		/// <summary>
		/// Initializes new instance of <see cref="Track"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="TrackManager"/> to which this instance belongs to.</param>
		public Track(string CName, TrackManager manager)
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Track"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="TrackManager"/> to which this instance belongs to.</param>
		public Track(BinaryReader br, TrackManager manager)
		{
			this.Manager = manager;
			this.Disassemble(br);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Track"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Track"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			// Write all directories and locations
			bw.WriteNullTermUTF8(this.RaceDescription, 0x20);
			bw.WriteNullTermUTF8(this.TrackDirectory, 0x20);
			bw.WriteNullTermUTF8(this.RegionName, 0x8);
			bw.WriteNullTermUTF8(this.RegionDirectory, 0x20);
			bw.Write(this.LocationIndex);
			bw.WriteNullTermUTF8(this.LocationDirectory, 0x10);

			// Write race settings
			bw.WriteEnum(this.LocationType);
			bw.WriteEnum(this.DriftType);
			bw.WriteEnum(this.IsValid);
			bw.Write(this.IsLoopingRace == eBoolean.True ? (byte)0 : (byte)1);
			bw.WriteEnum(this.ReverseVersionExists);
			bw.Write((byte)0);
			bw.WriteEnum(this.IsPerformanceTuning);
			bw.Write((byte)0);
			bw.Write(UInt16.Parse(this._collection_name));
			bw.Write(UInt16.Parse(this._collection_name));
			bw.Write((short)0);

			// Write gameplay scores
			bw.Write(this.SunInfoName.BinHash());
			bw.WriteEnum(this.RaceGameplayMode);
			bw.Write(this.RaceLength);
			bw.Write(this.TimeLimitToBeatForward);
			bw.Write(this.TimeLimitToBeatReverse);
			bw.Write(this.ScoreToBeatDriftForward);
			bw.Write(this.ScoreToBeatDriftReverse);

			// Write map calibrations
			bw.Write(this.TrackMapCalibrationOffsetX);
			bw.Write(this.TrackMapCalibrationOffsetY);
			bw.Write(this.TrackMapCalibrationWidth);
			bw.Write((ushort)(this.TrackMapCalibrationRotation / 180 * 32768));
			bw.Write((ushort)(this.TrackMapStartgridAngle / 180 * 32768));
			bw.Write((ushort)(this.TrackMapFinishlineAngle / 180 * 32768));
			bw.Write((short)0);
			bw.Write(this.TrackMapCalibrationZoomIn);

			// Write difficulties and padding
			bw.Write((int)this.DifficultyForward);
			bw.Write((int)this.DifficultyReverse);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(this.NumSecBeforeShorcutsAllowed);
			bw.Write(this.DriftSecondsMin);
			bw.Write(this.DriftSecondsMax);
			bw.Write((short)0);

			// Write traffic settings
			bw.Write(this.MaxTrafficCars_0_0);
			bw.Write(this.MaxTrafficCars_0_1);
			bw.Write(this.MaxTrafficCars_1_0);
			bw.Write(this.MaxTrafficCars_1_1);
			bw.Write(this.MaxTrafficCars_2_0);
			bw.Write(this.MaxTrafficCars_2_1);
			bw.Write(this.MaxTrafficCars_3_0);
			bw.Write(this.MaxTrafficCars_3_1);
			bw.Write(this.TrafAllowedNearStartgrid);
			bw.Write(this.TrafAllowedNearFinishline);
			bw.Write(this.CarRaceStartConfig);
			bw.Write(this.TrafMinInitDistFromStart);
			bw.Write(this.TrafMinInitDistFromFinish);
			bw.Write(this.TrafMinInitDistInbetweenA);
			bw.Write(this.TrafMinInitDistInbetweenB);
			bw.Write(this.TrafOncomingFraction1);
			bw.Write(this.TrafOncomingFraction2);
			bw.Write(this.TrafOncomingFraction3);
			bw.Write(this.TrafOncomingFraction4);

			// Write menu map settings
			bw.Write(this.MenuMapZoomOffsetX);
			bw.Write(this.MenuMapZoomOffsetY);
			bw.Write(this.MenuMapZoomWidth);
			bw.Write(this.MenuMapStartZoomed);
		}

		/// <summary>
		/// Disassembles array into <see cref="Track"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Track"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			// Read all directories and locations
			this.RaceDescription = br.ReadNullTermUTF8(0x20);
			this.TrackDirectory = br.ReadNullTermUTF8(0x20);
			this.RegionName = br.ReadNullTermUTF8(0x8);
			this.RegionDirectory = br.ReadNullTermUTF8(0x20);
			this.LocationIndex = br.ReadInt32();
			this.LocationDirectory = br.ReadNullTermUTF8(0x10);

			// Read race settings
			this.LocationType = br.ReadEnum<TrackLocationType>();
			this.DriftType = br.ReadEnum<TrackDriftType>();
			this.IsValid = br.ReadEnum<eBoolean>();
			this.IsLoopingRace = br.ReadByte() == 0 ? eBoolean.True : eBoolean.False;
			this.ReverseVersionExists = br.ReadEnum<eBoolean>();
			br.BaseStream.Position += 1;
			this.IsPerformanceTuning = br.ReadEnum<eBoolean>();
			br.BaseStream.Position += 1;
			this._collection_name = br.ReadUInt16().ToString();
			br.BaseStream.Position += 4;

			// Read gameplay scores
			this.SunInfoName = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.RaceGameplayMode = br.ReadEnum<TrackGameplayMode>();
			this.RaceLength = br.ReadUInt32();
			this.TimeLimitToBeatForward = br.ReadSingle();
			this.TimeLimitToBeatReverse = br.ReadSingle();
			this.ScoreToBeatDriftForward = br.ReadInt32();
			this.ScoreToBeatDriftReverse = br.ReadInt32();

			// Read map calibrations
			this.TrackMapCalibrationOffsetX = br.ReadSingle();
			this.TrackMapCalibrationOffsetY = br.ReadSingle();
			this.TrackMapCalibrationWidth = br.ReadSingle();
			this.TrackMapCalibrationRotation = ((float)br.ReadUInt16()) * 180 / 32768;
			this.TrackMapStartgridAngle = ((float)br.ReadUInt16()) * 180 / 32768;
			this.TrackMapFinishlineAngle = ((float)br.ReadUInt16()) * 180 / 32768;
			br.BaseStream.Position += 2;
			this.TrackMapCalibrationZoomIn = br.ReadSingle();

			// Read difficulties and padding
			this.DifficultyForward = (TrackDifficulty)(br.ReadInt32());
			this.DifficultyReverse = (TrackDifficulty)(br.ReadInt32());

			// For some weird reason there is a random chance of padding being different
			// We read till we get away from 0xFFFFFFFF
			while (br.ReadInt32() == -1) { }
			br.BaseStream.Position -= 4;
			
			this.NumSecBeforeShorcutsAllowed = br.ReadInt16();
			this.DriftSecondsMin = br.ReadInt16();
			this.DriftSecondsMax = br.ReadInt16();
			br.BaseStream.Position += 2;

			// Read traffic settings
			this.MaxTrafficCars_0_0 = br.ReadByte();
			this.MaxTrafficCars_0_1 = br.ReadByte();
			this.MaxTrafficCars_1_0 = br.ReadByte();
			this.MaxTrafficCars_1_1 = br.ReadByte();
			this.MaxTrafficCars_2_0 = br.ReadByte();
			this.MaxTrafficCars_2_1 = br.ReadByte();
			this.MaxTrafficCars_3_0 = br.ReadByte();
			this.MaxTrafficCars_3_1 = br.ReadByte();
			this.TrafAllowedNearStartgrid = br.ReadByte();
			this.TrafAllowedNearFinishline = br.ReadByte();
			this.CarRaceStartConfig = br.ReadInt16();
			this.TrafMinInitDistFromStart = br.ReadSingle();
			this.TrafMinInitDistFromFinish = br.ReadSingle();
			this.TrafMinInitDistInbetweenA = br.ReadSingle();
			this.TrafMinInitDistInbetweenB = br.ReadSingle();
			this.TrafOncomingFraction1 = br.ReadSingle();
			this.TrafOncomingFraction2 = br.ReadSingle();
			this.TrafOncomingFraction3 = br.ReadSingle();
			this.TrafOncomingFraction4 = br.ReadSingle();

			// Read menu map settings
			this.MenuMapZoomOffsetX = br.ReadSingle();
			this.MenuMapZoomOffsetY = br.ReadSingle();
			this.MenuMapZoomWidth = br.ReadSingle();
			this.MenuMapStartZoomed = br.ReadInt32();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new Track(CName, this.Manager);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="Track"/> 
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
		public override void Serialize(BinaryWriter bw)
		{
			byte[] array;
			using (var ms = new MemoryStream(0x122))
			using (var writer = new BinaryWriter(ms))
			{

				// Write all directories and locations
				writer.Write(UInt16.Parse(this._collection_name));
				writer.WriteNullTermUTF8(this.RaceDescription);
				writer.WriteNullTermUTF8(this.TrackDirectory);
				writer.WriteNullTermUTF8(this.RegionName);
				writer.WriteNullTermUTF8(this.RegionDirectory);
				writer.Write(this.LocationIndex);
				writer.WriteNullTermUTF8(this.LocationDirectory);

				// Write race settings
				writer.WriteEnum(this.LocationType);
				writer.WriteEnum(this.DriftType);
				writer.WriteEnum(this.IsValid);
				writer.WriteEnum(this.IsLoopingRace);
				writer.WriteEnum(this.ReverseVersionExists);
				writer.WriteEnum(this.IsPerformanceTuning);

				// Write gameplay scores
				writer.WriteNullTermUTF8(this.SunInfoName);
				writer.WriteEnum(this.RaceGameplayMode);
				writer.Write(this.RaceLength);
				writer.Write(this.TimeLimitToBeatForward);
				writer.Write(this.TimeLimitToBeatReverse);
				writer.Write(this.ScoreToBeatDriftForward);
				writer.Write(this.ScoreToBeatDriftReverse);

				// Write map calibrations
				writer.Write(this.TrackMapCalibrationOffsetX);
				writer.Write(this.TrackMapCalibrationOffsetY);
				writer.Write(this.TrackMapCalibrationWidth);
				writer.Write(this.TrackMapCalibrationRotation);
				writer.Write(this.TrackMapStartgridAngle);
				writer.Write(this.TrackMapFinishlineAngle);
				writer.Write(this.TrackMapCalibrationZoomIn);

				// Write difficulties
				writer.WriteEnum(this.DifficultyForward);
				writer.WriteEnum(this.DifficultyReverse);
				writer.Write(this.NumSecBeforeShorcutsAllowed);
				writer.Write(this.DriftSecondsMin);
				writer.Write(this.DriftSecondsMax);

				// Write traffic settings
				writer.Write(this.MaxTrafficCars_0_0);
				writer.Write(this.MaxTrafficCars_0_1);
				writer.Write(this.MaxTrafficCars_1_0);
				writer.Write(this.MaxTrafficCars_1_1);
				writer.Write(this.MaxTrafficCars_2_0);
				writer.Write(this.MaxTrafficCars_2_1);
				writer.Write(this.MaxTrafficCars_3_0);
				writer.Write(this.MaxTrafficCars_3_1);
				writer.Write(this.TrafAllowedNearStartgrid);
				writer.Write(this.TrafAllowedNearFinishline);
				writer.Write(this.CarRaceStartConfig);
				writer.Write(this.TrafMinInitDistFromStart);
				writer.Write(this.TrafMinInitDistFromFinish);
				writer.Write(this.TrafMinInitDistInbetweenA);
				writer.Write(this.TrafMinInitDistInbetweenB);
				writer.Write(this.TrafOncomingFraction1);
				writer.Write(this.TrafOncomingFraction2);
				writer.Write(this.TrafOncomingFraction3);
				writer.Write(this.TrafOncomingFraction4);

				// Write menu map settings
				writer.Write(this.MenuMapZoomOffsetX);
				writer.Write(this.MenuMapZoomOffsetY);
				writer.Write(this.MenuMapZoomWidth);
				writer.Write(this.MenuMapStartZoomed);

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
		public override void Deserialize(BinaryReader br)
		{
			int size = br.ReadInt32();
			var array = br.ReadBytes(size);

			array = Interop.Decompress(array);

			using var ms = new MemoryStream(array);
			using var reader = new BinaryReader(ms);

			// Read all directories and locations
			this._collection_name = reader.ReadUInt16().ToString();
			this.RaceDescription = reader.ReadNullTermUTF8();
			this.TrackDirectory = reader.ReadNullTermUTF8();
			this.RegionName = reader.ReadNullTermUTF8();
			this.RegionDirectory = reader.ReadNullTermUTF8();
			this.LocationIndex = reader.ReadInt32();
			this.LocationDirectory = reader.ReadNullTermUTF8();

			// Read race settings
			this.LocationType = reader.ReadEnum<TrackLocationType>();
			this.DriftType = reader.ReadEnum<TrackDriftType>();
			this.IsValid = reader.ReadEnum<eBoolean>();
			this.IsLoopingRace = reader.ReadEnum<eBoolean>();
			this.ReverseVersionExists = reader.ReadEnum<eBoolean>();
			this.IsPerformanceTuning = reader.ReadEnum<eBoolean>();

			// Read gameplay scores
			this.SunInfoName = reader.ReadNullTermUTF8();
			this.RaceGameplayMode = reader.ReadEnum<TrackGameplayMode>();
			this.RaceLength = reader.ReadUInt32();
			this.TimeLimitToBeatForward = reader.ReadSingle();
			this.TimeLimitToBeatReverse = reader.ReadSingle();
			this.ScoreToBeatDriftForward = reader.ReadInt32();
			this.ScoreToBeatDriftReverse = reader.ReadInt32();

			// Read map calibrations
			this.TrackMapCalibrationOffsetX = reader.ReadSingle();
			this.TrackMapCalibrationOffsetY = reader.ReadSingle();
			this.TrackMapCalibrationWidth = reader.ReadSingle();
			this.TrackMapCalibrationRotation = reader.ReadSingle();
			this.TrackMapStartgridAngle = reader.ReadSingle();
			this.TrackMapFinishlineAngle = reader.ReadSingle();
			this.TrackMapCalibrationZoomIn = reader.ReadSingle();

			// Read difficulties and padding
			this.DifficultyForward = reader.ReadEnum<TrackDifficulty>();
			this.DifficultyReverse = reader.ReadEnum<TrackDifficulty>();
			this.NumSecBeforeShorcutsAllowed = reader.ReadInt16();
			this.DriftSecondsMin = reader.ReadInt16();
			this.DriftSecondsMax = reader.ReadInt16();

			// Read traffic settings
			this.MaxTrafficCars_0_0 = reader.ReadByte();
			this.MaxTrafficCars_0_1 = reader.ReadByte();
			this.MaxTrafficCars_1_0 = reader.ReadByte();
			this.MaxTrafficCars_1_1 = reader.ReadByte();
			this.MaxTrafficCars_2_0 = reader.ReadByte();
			this.MaxTrafficCars_2_1 = reader.ReadByte();
			this.MaxTrafficCars_3_0 = reader.ReadByte();
			this.MaxTrafficCars_3_1 = reader.ReadByte();
			this.TrafAllowedNearStartgrid = reader.ReadByte();
			this.TrafAllowedNearFinishline = reader.ReadByte();
			this.CarRaceStartConfig = reader.ReadInt16();
			this.TrafMinInitDistFromStart = reader.ReadSingle();
			this.TrafMinInitDistFromFinish = reader.ReadSingle();
			this.TrafMinInitDistInbetweenA = reader.ReadSingle();
			this.TrafMinInitDistInbetweenB = reader.ReadSingle();
			this.TrafOncomingFraction1 = reader.ReadSingle();
			this.TrafOncomingFraction2 = reader.ReadSingle();
			this.TrafOncomingFraction3 = reader.ReadSingle();
			this.TrafOncomingFraction4 = reader.ReadSingle();

			// Read menu map settings
			this.MenuMapZoomOffsetX = reader.ReadSingle();
			this.MenuMapZoomOffsetY = reader.ReadSingle();
			this.MenuMapZoomWidth = reader.ReadSingle();
			this.MenuMapStartZoomed = reader.ReadInt32();
		}

		#endregion
	}
}