using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="PartPerformance"/> is a collection of settings related to performance parts.
	/// </summary>
	public class PartPerformance : ACollectable
	{
		#region Fields

		private string _collection_name;
		private ePerformanceType _part_perf_type = ePerformanceType.NOS;
		private int _upgrade_level;
		private int _upgrade_part_index;
		private int _part_index;
		private readonly bool _cname_is_set = false;

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
				if (this.Database.PartPerformances.FindCollection(value) != null)
					throw new CollectionExistenceException();
				this._collection_name = value;
				if (this._cname_is_set)
					Map.PerfPartTable[(int)this._part_perf_type, this._upgrade_level, this._upgrade_part_index] = Convert.ToUInt32(value, 16);
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
		/// Price of the <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public int PerfPartCost { get; set; }

		/// <summary>
		/// Percentage of whole package performance boost.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float PerfPartAmplifierFraction { get; set; }

		/// <summary>
		/// Range X of <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float PerfPartRangeX { get; set; } = -1;

		/// <summary>
		/// Range Y of <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float PerfPartRangeY { get; set; } = -1;

		/// <summary>
		/// Range Z of <see cref="PartPerformance"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float PerfPartRangeZ { get; set; } = -1;

		/// <summary>
		/// First part index by which this <see cref="PartPerformance"/> can be replaced.
		/// </summary>
		[AccessModifiable()]
		public int BeingReplacedByIndex1 { get; set; } = -1;

		/// <summary>
		/// Second part index by which this <see cref="PartPerformance"/> can be replaced.
		/// </summary>
		[AccessModifiable()]
		public int BeingReplacedByIndex2 { get; set; } = -1;

		/// <summary>
		/// Number of brands available to choose when installing the part.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public int NumberOfBrands { get; set; }

		/// <summary>
		/// Brand name 1.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand1 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 2.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand2 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 3.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand3 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 4.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand4 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 5.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand5 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 6.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand6 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 7.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand7 { get; set; } = String.Empty;

		/// <summary>
		/// Brand name 8.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public string PerfPartBrand8 { get; set; } = String.Empty;

		/// <summary>
		/// Type of the performance.
		/// </summary>
		[AccessModifiable()]
		public ePerformanceType PartPerformanceType
		{
			get => this._part_perf_type;
			set
			{
				if (!Enum.IsDefined(typeof(ePerformanceType), value))
					throw new MappingFailException();
				if (this.CheckIfTypeCanBeSwitched(value))
					this.SwitchPerfType(value);
				else
					throw new Exception("Unable to set: no available perf part slots in this group exist.");
			}
		}

		/// <summary>
		/// Upgrade level of the part.
		/// </summary>
		[AccessModifiable()]
		public int UpgradeLevel
		{
			get => this._upgrade_level + 1;
			set
			{
				--value;
				if (this.CheckIfLevelCanBeSwitched(value))
					this.SwitchUpgradeLevel(value);
				else
					throw new Exception("Unable to set: no available perf part slots in this level exist.");
			}
		}

		/// <summary>
		/// Index of the part in the performance package.
		/// </summary>
		[AccessModifiable()]
		public int UpgradePartIndex
		{
			get => this._upgrade_part_index;
			set
			{
				if (this.CheckIfIndexCanBeSwitched(value))
					this.SwitchUpgradePartIndex(value);
				else
					throw new Exception("Unable to set: the perf slot is already taken by a different part.");
			}
		}

		/// <summary>
		/// Index of the part.
		/// </summary>
		[AccessModifiable()]
		public int PartIndex
		{
			get => this._part_index;
			set
			{
				foreach (var cla in this.Database.PartPerformances.Collections)
				{
					if (cla.PartIndex == value)
						throw new Exception("Performance Part with the same PartIndex already exists.");
				}
				this._part_index = value;
			}
		}

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
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public PartPerformance(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			this.SetToFirstAvailablePerfSlot();
			int index = 0;
			foreach (var cla in db.PartPerformances.Collections)
			{
				if (cla.PartIndex > index)
					index = cla.PartIndex;
			}
			this._part_index = index + 1;
			this._cname_is_set = true;
		}

		/// <summary>
		/// Initializes new instance of <see cref="PartPerformance"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		/// <param name="args">Performance type, upgrade level, and upgrade index of the part.</param>
		public PartPerformance(BinaryReader br, Database.Underground2 db, params int[] args)
		{
			this.Database = db;
			this._part_perf_type = (ePerformanceType)args[0];
			this._upgrade_level = args[1];
			this._upgrade_part_index = args[2];
			this.Disassemble(br);
			Map.PerfPartTable[args[0], args[1], args[2]] = this.BinKey;
			this._cname_is_set = true;
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~PartPerformance() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="PartPerformance"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PartPerformance"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			bw.Write(this._part_index);
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
			uint key = 0;
			const uint negative = 0xFFFFFFFF;

			// CollectionName and stuff
			this._part_index = br.ReadInt32();
			this._collection_name = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.PerfPartCost = br.ReadInt32();
			this.NumberOfBrands = br.ReadInt32();

			// Resolve all brands (use non-reflective for speed)
			key = br.ReadUInt32();
			this.PerfPartBrand1 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);
			key = br.ReadUInt32();
			this.PerfPartBrand2 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);
			key = br.ReadUInt32();
			this.PerfPartBrand3 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);
			key = br.ReadUInt32();
			this.PerfPartBrand4 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);
			key = br.ReadUInt32();
			this.PerfPartBrand5 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);
			key = br.ReadUInt32();
			this.PerfPartBrand6 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);
			key = br.ReadUInt32();
			this.PerfPartBrand7 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);
			key = br.ReadUInt32();
			this.PerfPartBrand8 = key == negative ? String.Empty : key.BinString(eLookupReturn.EMPTY);

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
		public override ACollectable MemoryCast(string CName)
		{
			var result = new PartPerformance(CName, this.Database)
			{
				PerfPartBrand1 = this.PerfPartBrand1,
				PerfPartBrand2 = this.PerfPartBrand2,
				PerfPartBrand3 = this.PerfPartBrand3,
				PerfPartBrand4 = this.PerfPartBrand4,
				PerfPartBrand5 = this.PerfPartBrand5,
				PerfPartBrand6 = this.PerfPartBrand6,
				PerfPartBrand7 = this.PerfPartBrand7,
				PerfPartBrand8 = this.PerfPartBrand8,
				BeingReplacedByIndex1 = this.BeingReplacedByIndex1,
				BeingReplacedByIndex2 = this.BeingReplacedByIndex2,
				NumberOfBrands = this.NumberOfBrands,
				PerfPartAmplifierFraction = this.PerfPartAmplifierFraction,
				PerfPartCost = this.PerfPartCost,
				PerfPartRangeX = this.PerfPartRangeX,
				PerfPartRangeY = this.PerfPartRangeY,
				PerfPartRangeZ = this.PerfPartRangeZ
			};

			return result;
		}

		private void ClearPartTableSlot()
		{
			int index = (int)this._part_perf_type;
			int level = this._upgrade_level;
			int value = this._upgrade_part_index;
			if (Map.PerfPartTable[index, level, value] == this.BinKey)
				Map.PerfPartTable[index, level, value] = 0;
		}

		private bool CheckIfIndexCanBeSwitched(int value)
		{
			int index = (int)this._part_perf_type;
			return Map.PerfPartTable[index, this._upgrade_level, value] == 0;
		}

		private bool CheckIfLevelCanBeSwitched(int level)
		{
			int index = (int)this._part_perf_type;
			for (int a1 = 0; a1 < 4; ++a1)
			{
				if (Map.PerfPartTable[index, level, a1] == 0)
					return true;
			}
			return false;
		}

		private bool CheckIfTypeCanBeSwitched(ePerformanceType perftype)
		{
			int index = (int)perftype;
			for (int a1 = 0; a1 < 3; ++a1)
			{
				for (int a2 = 0; a2 < 4; ++a2)
				{
					if (Map.PerfPartTable[index, a1, a2] == 0)
						return true;
				}
			}
			return false;
		}

		private void SetToFirstAvailablePerfSlot()
		{
			for (int a1 = 0; a1 < 10; ++a1)
			{
				for (int a2 = 0; a2 < 3; ++a2)
				{
					for (int a3 = 0; a3 < 4; ++a3)
					{
						if (Map.PerfPartTable[a1, a2, a3] == 0)
						{
							this._part_perf_type = (ePerformanceType)a1;
							this._upgrade_level = a2;
							this._upgrade_part_index = a3;
							Map.PerfPartTable[a1, a2, a3] = this.BinKey;
							return;
						}
					}
				}
			}
		}

		private void SwitchPerfType(ePerformanceType perftype)
		{
			// Clear slot
			this.ClearPartTableSlot();

			// Move to another
			this._part_perf_type = perftype;
			int index = (int)perftype;
			for (int a1 = 0; a1 < 3; ++a1)
			{
				for (int a2 = 0; a2 < 4; ++a2)
				{
					if (Map.PerfPartTable[index, a1, a2] == 0)
					{
						Map.PerfPartTable[index, a1, a2] = this.BinKey;
						this._upgrade_level = a1;
						this._upgrade_part_index = a2;
						return;
					}
				}
			}
		}

		private void SwitchUpgradeLevel(int level)
		{
			// Clear slot
			int index = (int)this._part_perf_type;
			this.ClearPartTableSlot();

			// Move to another
			this._upgrade_level = level;
			for (int a1 = 0; a1 < 4; ++a1)
			{
				if (Map.PerfPartTable[index, level, a1] == 0)
				{
					Map.PerfPartTable[index, level, a1] = this.BinKey;
					this._upgrade_part_index = a1;
					return;
				}
			}
		}

		private void SwitchUpgradePartIndex(int value)
		{
			// Clear slot
			int index = (int)this._part_perf_type;
			int level = this._upgrade_level;
			this.ClearPartTableSlot();

			// Move to another
			this._upgrade_part_index = value;
			Map.PerfPartTable[index, level, value] = this.BinKey;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="BankTrigger"/> 
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