using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="GCareerStage"/> is a collection of settings related to career stages.
	/// </summary>
	public class GCareerStage : ACollectable
	{
		#region Fields

		private string _collection_name;

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
				if (!value.StartsWith("STAGE_") && !value.GetFormattedValue("STAGE_{X}", out byte _))
					throw new Exception("Unable to parse stage number from CollectionName.");
				if (this.Database.GCareerStages.FindCollection(value) != null)
					throw new CollectionExistenceException(value);
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
		/// Cash value given to the player for winning an outrun event.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short OutrunCashValue { get; set; }

		/// <summary>
		/// Last career event in the stage.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string LastStageEvent { get; set; }

		/// <summary>
		/// Maximum amount of circuit events shown on the map.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxCircuitsShownOnMap { get; set; }

		/// <summary>
		/// Maximum amount of drag events shown on the map.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxDragsShownOnMap { get; set; }

		/// <summary>
		/// Maximum amount of street X events shown on the map.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxStreetXShownOnMap { get; set; }

		/// <summary>
		/// Maximum amount of drift events shown on the map.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxDriftsShownOnMap { get; set; }

		/// <summary>
		/// Maximum amount of sprint events shown on the map.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxSprintsShownOnMap { get; set; }

		/// <summary>
		/// Maximum amount of outrun events in the stage.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxOutrunEvents { get; set; }

		/// <summary>
		/// Sponsor 1.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string StageSponsor1 { get; set; } = String.Empty;

		/// <summary>
		/// Sponsor 2.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string StageSponsor2 { get; set; } = String.Empty;

		/// <summary>
		/// Sponsor 3.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string StageSponsor3 { get; set; } = String.Empty;

		/// <summary>
		/// Sponsor 4.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string StageSponsor4 { get; set; } = String.Empty;

		/// <summary>
		/// Sponsor 5.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string StageSponsor5 { get; set; } = String.Empty;

		/// <summary>
		/// Number of sponsors in the stage.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte NumberOfSponsorsToChoose { get; set; }

		/// <summary>
		/// Sponsor 1 attribute.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short AttribSponsor1 { get; set; }

		/// <summary>
		/// Sponsor 2 attribute.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short AttribSponsor2 { get; set; }

		/// <summary>
		/// Sponsor 3 attribute.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short AttribSponsor3 { get; set; }

		/// <summary>
		/// Sponsor 4 attribute.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short AttribSponsor4 { get; set; }

		/// <summary>
		/// Sponsor 5 attribute.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short AttribSponsor5 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x04.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public short Unknown0x04 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x06.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public short Unknown0x06 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x26.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public short Unknown0x26 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x2C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x2C { get; set; }

		/// <summary>
		/// Unknown value at offset 0x2D.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x2D { get; set; }

		/// <summary>
		/// Unknown value at offset 0x2E.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x2E { get; set; }

		/// <summary>
		/// Unknown value at offset 0x2F.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x2F { get; set; }

		/// <summary>
		/// Unknown value at offset 0x35.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x35 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x36.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x36 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x37.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x37 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x38.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x38 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x39.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x39 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3A.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x3A { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3B.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x3B { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x3C { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3D.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x3D { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3E.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x3E { get; set; }

		/// <summary>
		/// Unknown value at offset 0x3F.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x3F { get; set; }

		/// <summary>
		/// Unknown value at offset 0x41.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x41 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x42
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x42 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x43
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte Unknown0x43 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x44.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public float Unknown0x44 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x48.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public float Unknown0x48 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x4C.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public float Unknown0x4C { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="GCareerStage"/>.
		/// </summary>
		public GCareerStage() { }

		/// <summary>
		/// Initializes new instance of <see cref="GCareerStage"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public GCareerStage(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCareerStage"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public unsafe GCareerStage(BinaryReader br, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~GCareerStage() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="GCareerStage"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="GCareerStage"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			this._collection_name.GetFormattedValue("STAGE_{X}", out byte stage);
			bw.Write(stage);
			bw.Write(this.NumberOfSponsorsToChoose);
			bw.Write(this.OutrunCashValue);
			bw.Write(this.Unknown0x04);
			bw.Write(this.Unknown0x06);

			bw.Write(this.StageSponsor1.BinHash());
			bw.Write(this.StageSponsor2.BinHash());
			bw.Write(this.StageSponsor3.BinHash());
			bw.Write(this.StageSponsor4.BinHash());
			bw.Write(this.StageSponsor5.BinHash());

			bw.Write(this.AttribSponsor1);
			bw.Write(this.AttribSponsor2);
			bw.Write(this.AttribSponsor3);
			bw.Write(this.AttribSponsor4);
			bw.Write(this.AttribSponsor5);
			bw.Write(this.Unknown0x26);

			bw.Write(this.LastStageEvent.BinHash());

			bw.Write(this.Unknown0x2C);
			bw.Write(this.Unknown0x2D);
			bw.Write(this.Unknown0x2E);
			bw.Write(this.Unknown0x2F);

			bw.Write(this.MaxCircuitsShownOnMap);
			bw.Write(this.MaxDragsShownOnMap);
			bw.Write(this.MaxStreetXShownOnMap);
			bw.Write(this.MaxDriftsShownOnMap);
			bw.Write(this.MaxSprintsShownOnMap);

			bw.Write(this.Unknown0x35);
			bw.Write(this.Unknown0x36);
			bw.Write(this.Unknown0x37);
			bw.Write(this.Unknown0x38);
			bw.Write(this.Unknown0x39);
			bw.Write(this.Unknown0x3A);
			bw.Write(this.Unknown0x3B);
			bw.Write(this.Unknown0x3C);
			bw.Write(this.Unknown0x3D);
			bw.Write(this.Unknown0x3E);
			bw.Write(this.Unknown0x3F);

			bw.Write(this.MaxOutrunEvents);
			bw.Write(this.Unknown0x41);
			bw.Write(this.Unknown0x42);
			bw.Write(this.Unknown0x43);
			bw.Write(this.Unknown0x44);
			bw.Write(this.Unknown0x48);
			bw.Write(this.Unknown0x4C);
		}

		/// <summary>
		/// Disassembles array into <see cref="GCareerStage"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCareerStage"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			// CollectionName
			this._collection_name = $"STAGE_{br.ReadByte()}";

			// Regular settings
			this.NumberOfSponsorsToChoose = br.ReadByte();
			this.OutrunCashValue = br.ReadInt16();
			this.Unknown0x04 = br.ReadInt16();
			this.Unknown0x06 = br.ReadInt16();

			// Sponsor Settings
			this.StageSponsor1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.StageSponsor2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.StageSponsor3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.StageSponsor4 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.StageSponsor5 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.AttribSponsor1 = br.ReadInt16();
			this.AttribSponsor2 = br.ReadInt16();
			this.AttribSponsor3 = br.ReadInt16();
			this.AttribSponsor4 = br.ReadInt16();
			this.AttribSponsor5 = br.ReadInt16();
			this.Unknown0x26 = br.ReadInt16();

			// Last stage event
			this.LastStageEvent = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			// Race Settings
			this.Unknown0x2C = br.ReadByte();
			this.Unknown0x2D = br.ReadByte();
			this.Unknown0x2E = br.ReadByte();
			this.Unknown0x2F = br.ReadByte();
			this.MaxCircuitsShownOnMap = br.ReadByte();
			this.MaxDragsShownOnMap = br.ReadByte();
			this.MaxStreetXShownOnMap = br.ReadByte();
			this.MaxDriftsShownOnMap = br.ReadByte();
			this.MaxSprintsShownOnMap = br.ReadByte();

			// Unknown Yet Values
			this.Unknown0x35 = br.ReadByte();
			this.Unknown0x36 = br.ReadByte();
			this.Unknown0x37 = br.ReadByte();
			this.Unknown0x38 = br.ReadByte();
			this.Unknown0x39 = br.ReadByte();
			this.Unknown0x3A = br.ReadByte();
			this.Unknown0x3B = br.ReadByte();
			this.Unknown0x3C = br.ReadByte();
			this.Unknown0x3D = br.ReadByte();
			this.Unknown0x3E = br.ReadByte();
			this.Unknown0x3F = br.ReadByte();
			this.MaxOutrunEvents = br.ReadByte();
			this.Unknown0x41 = br.ReadByte();
			this.Unknown0x42 = br.ReadByte();
			this.Unknown0x43 = br.ReadByte();
			this.Unknown0x44 = br.ReadSingle();
			this.Unknown0x48 = br.ReadSingle();
			this.Unknown0x4C = br.ReadSingle();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new GCareerStage(CName, this.Database);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="GCareerBrand"/> 
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