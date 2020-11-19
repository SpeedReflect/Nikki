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
	/// <see cref="PartPerformance"/> is a collection of settings related to performance parts.
	/// </summary>
	public class PartPerformance : Collectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Enums

		/// <summary>
		/// Enum of <see cref="PartPerformance"/> types.
		/// </summary>
		public enum PerformanceType : int
		{
			/// <summary>
			/// Weight Reduction performance type.
			/// </summary>
			WEIGHT_REDUCTION = 0,

			/// <summary>
			/// Transmission/Drivetrain performance type.
			/// </summary>
			DRIVETRAIN = 1,

			/// <summary>
			/// Nitrous/NOS performance type.
			/// </summary>
			NOS = 2,

			/// <summary>
			/// Engine performance type.
			/// </summary>
			ENGINE = 3,

			/// <summary>
			/// Supercharger/Turbo performance type.
			/// </summary>
			TURBO = 4,

			/// <summary>
			/// Suspension performance type.
			/// </summary>
			SUSPENSION = 5,

			/// <summary>
			/// Calipers/Brakes performance type.
			/// </summary>
			BRAKES = 6,

			/// <summary>
			/// Control Unit/ECU performance type.
			/// </summary>
			ECU = 7,

			/// <summary>
			/// Tires performance type.
			/// </summary>
			TIRES = 8,

			/// <summary>
			/// Aerodynamics/Aero performance type.
			/// </summary>
			AERO = 9,
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
				if (this.Career.GetCollection(value, nameof(this.Career.PartPerformances)) != null)
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
		/// Price of the <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int PerfPartCost { get; set; }

		/// <summary>
		/// Percentage of whole package performance boost.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float PerfPartAmplifierFraction { get; set; }

		/// <summary>
		/// Range X of <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float PerfPartRangeX { get; set; } = -1;

		/// <summary>
		/// Range Y of <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float PerfPartRangeY { get; set; } = -1;

		/// <summary>
		/// Range Z of <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float PerfPartRangeZ { get; set; } = -1;

		/// <summary>
		/// First part index by which this <see cref="PartPerformance"/> can be replaced.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int BeingReplacedByIndex1 { get; set; } = -1;

		/// <summary>
		/// Second part index by which this <see cref="PartPerformance"/> can be replaced.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int BeingReplacedByIndex2 { get; set; } = -1;

		/// <summary>
		/// Number of brands available to choose when installing the part.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int NumberOfBrands { get; set; }

		/// <summary>
		/// Brand name 1.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand1 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 2.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand2 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 3.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand3 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 4.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand4 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 5.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand5 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 6.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand6 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 7.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand7 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 8.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string PerfPartBrand8 { get; set; } = String.Empty;

		/// <summary>
		/// Type of the performance.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public PerformanceType PartPerformanceType { get; set; }

		/// <summary>
		/// Upgrade level of the part.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int UpgradeLevel { get; set; }

		/// <summary>
		/// Index of the part in the performance package.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int UpgradePartIndex { get; set; }

		/// <summary>
		/// Index of the part.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int PartIndex { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="PartPerformance"/>.
		/// </summary>
		public PartPerformance() { }

		/// <summary>
		/// Initializes new instance of <see cref="PartPerformance"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public PartPerformance(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="PartPerformance"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public PartPerformance(BinaryReader br, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="PartPerformance"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PartPerformance"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			bw.Write(this.PartIndex);
			bw.Write(this.BinKey);
			bw.Write(this.PerfPartCost);
			bw.Write(this.NumberOfBrands);

			uint negative = 0xFFFFFFFF;
			uint perfkey1 = this.PerfPartBrand1.BinHash();
			uint perfkey2 = this.PerfPartBrand2.BinHash();
			uint perfkey3 = this.PerfPartBrand3.BinHash();
			uint perfkey4 = this.PerfPartBrand4.BinHash();
			uint perfkey5 = this.PerfPartBrand5.BinHash();
			uint perfkey6 = this.PerfPartBrand6.BinHash();
			uint perfkey7 = this.PerfPartBrand7.BinHash();
			uint perfkey8 = this.PerfPartBrand8.BinHash();

			bw.Write(perfkey1 == 0 ? negative : perfkey1);
			bw.Write(perfkey2 == 0 ? negative : perfkey2);
			bw.Write(perfkey3 == 0 ? negative : perfkey3);
			bw.Write(perfkey4 == 0 ? negative : perfkey4);
			bw.Write(perfkey5 == 0 ? negative : perfkey5);
			bw.Write(perfkey6 == 0 ? negative : perfkey6);
			bw.Write(perfkey7 == 0 ? negative : perfkey7);
			bw.Write(perfkey8 == 0 ? negative : perfkey8);

			bw.Write(this.PerfPartAmplifierFraction);
			bw.Write(this.PerfPartRangeX);
			bw.Write(this.PerfPartRangeY);
			bw.Write(this.PerfPartRangeZ);

			bw.Write(this.BeingReplacedByIndex1);
			bw.Write(this.BeingReplacedByIndex2);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(-1);
		}

		/// <summary>
		/// Disassembles array into <see cref="PartPerformance"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="PartPerformance"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			// CollectionName and stuff
			this.PartIndex = br.ReadInt32();
			this._collection_name = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartCost = br.ReadInt32();
			this.NumberOfBrands = br.ReadInt32();

			// Resolve all brands (use non-reflective for speed)
			this.PerfPartBrand1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartBrand2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartBrand3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartBrand4 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartBrand5 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartBrand6 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartBrand7 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.PerfPartBrand8 = br.ReadUInt32().BinString(LookupReturn.EMPTY);

			// Perf part settings
			this.PerfPartAmplifierFraction = br.ReadSingle();
			this.PerfPartRangeX = br.ReadSingle();
			this.PerfPartRangeY = br.ReadSingle();
			this.PerfPartRangeZ = br.ReadSingle();
			this.BeingReplacedByIndex1 = br.ReadInt32();
			this.BeingReplacedByIndex2 = br.ReadInt32();
			br.BaseStream.Position += 20;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new PartPerformance(CName, this.Career);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="PartPerformance"/> 
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
			bw.WriteEnum(this.PartPerformanceType);
			bw.Write(this.UpgradeLevel);
			bw.Write(this.UpgradePartIndex);
			bw.Write(this.PartIndex);
			bw.Write(this.BeingReplacedByIndex1);
			bw.Write(this.BeingReplacedByIndex2);
			bw.Write(this.PerfPartCost);
			bw.Write(this.PerfPartAmplifierFraction);
			bw.Write(this.PerfPartRangeX);
			bw.Write(this.PerfPartRangeY);
			bw.Write(this.PerfPartRangeZ);
			bw.Write(this.NumberOfBrands);

			if (this.NumberOfBrands > 0) bw.WriteNullTermUTF8(this.PerfPartBrand1);
			if (this.NumberOfBrands > 1) bw.WriteNullTermUTF8(this.PerfPartBrand2);
			if (this.NumberOfBrands > 2) bw.WriteNullTermUTF8(this.PerfPartBrand3);
			if (this.NumberOfBrands > 3) bw.WriteNullTermUTF8(this.PerfPartBrand4);
			if (this.NumberOfBrands > 4) bw.WriteNullTermUTF8(this.PerfPartBrand5);
			if (this.NumberOfBrands > 5) bw.WriteNullTermUTF8(this.PerfPartBrand6);
			if (this.NumberOfBrands > 6) bw.WriteNullTermUTF8(this.PerfPartBrand7);
			if (this.NumberOfBrands > 7) bw.WriteNullTermUTF8(this.PerfPartBrand8);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this._collection_name = br.ReadNullTermUTF8();
			this.PartPerformanceType = br.ReadEnum<PerformanceType>();
			this.UpgradeLevel = br.ReadInt32();
			this.UpgradePartIndex = br.ReadInt32();
			this.PartIndex = br.ReadInt32();
			this.BeingReplacedByIndex1 = br.ReadInt32();
			this.BeingReplacedByIndex2 = br.ReadInt32();
			this.PerfPartCost = br.ReadInt32();
			this.PerfPartAmplifierFraction = br.ReadSingle();
			this.PerfPartRangeX = br.ReadSingle();
			this.PerfPartRangeY = br.ReadSingle();
			this.PerfPartRangeZ = br.ReadSingle();
			this.NumberOfBrands = br.ReadInt32();

			if (this.NumberOfBrands > 0) this.PerfPartBrand1 = br.ReadNullTermUTF8();
			if (this.NumberOfBrands > 1) this.PerfPartBrand2 = br.ReadNullTermUTF8();
			if (this.NumberOfBrands > 2) this.PerfPartBrand3 = br.ReadNullTermUTF8();
			if (this.NumberOfBrands > 3) this.PerfPartBrand4 = br.ReadNullTermUTF8();
			if (this.NumberOfBrands > 4) this.PerfPartBrand5 = br.ReadNullTermUTF8();
			if (this.NumberOfBrands > 5) this.PerfPartBrand6 = br.ReadNullTermUTF8();
			if (this.NumberOfBrands > 6) this.PerfPartBrand7 = br.ReadNullTermUTF8();
			if (this.NumberOfBrands > 7) this.PerfPartBrand8 = br.ReadNullTermUTF8();
		}

		#endregion
	}
}
