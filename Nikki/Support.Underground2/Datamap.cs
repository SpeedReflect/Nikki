using System;
using System.IO;
using Nikki.Core;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Support.Underground2.Class;
using Nikki.Support.Underground2.Framework;



namespace Nikki.Support.Underground2
{
	/// <summary>
	/// <see cref="Datamap"/> is an extension of <see cref="FileBase"/> for Carbon support.
	/// </summary>
	public class Datamap : FileBase
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground2;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => this.GameINT.ToString();

		/// <summary>
		/// Initializes new instance of <see cref="Datamap"/>.
		/// </summary>
		public Datamap() : base()
		{
			this.Managers.Add(new AcidEffectManager(this));
			this.Managers.Add(new AcidEmitterManager(this));
			this.Managers.Add(new CarTypeInfoManager(this));
			this.Managers.Add(new DBModelPartManager(this));
			this.Managers.Add(new FNGroupManager(this));
			this.Managers.Add(new GCareerManager(this));
			this.Managers.Add(new MaterialManager(this));
			this.Managers.Add(new PresetRideManager(this));
			this.Managers.Add(new SlotOverrideManager(this));
			this.Managers.Add(new SlotTypeManager(this));
			this.Managers.Add(new STRBlockManager(this));
			this.Managers.Add(new SunInfoManager(this));
			this.Managers.Add(new TPKBlockManager(this));
			this.Managers.Add(new TrackManager(this));
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="AcidEffect"/>.
		/// </summary>
		public AcidEffectManager AcidEffects
		{
			get
			{
				var manager = this.GetManager(typeof(AcidEffectManager));
				return manager == null ? null : manager as AcidEffectManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="AcidEmitter"/>.
		/// </summary>
		public AcidEmitterManager AcidEmitters
		{
			get
			{
				var manager = this.GetManager(typeof(AcidEmitterManager));
				return manager == null ? null : manager as AcidEmitterManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="CarTypeInfo"/>.
		/// </summary>
		public CarTypeInfoManager CarTypeInfos
		{
			get
			{
				var manager = this.GetManager(typeof(CarTypeInfoManager));
				return manager == null ? null : manager as CarTypeInfoManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="DBModelPart"/>.
		/// </summary>
		public DBModelPartManager DBModelParts
		{
			get
			{
				var manager = this.GetManager(typeof(DBModelPartManager));
				return manager == null ? null : manager as DBModelPartManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="FNGroup"/>.
		/// </summary>
		public FNGroupManager FNGroups
		{
			get
			{
				var manager = this.GetManager(typeof(FNGroupManager));
				return manager == null ? null : manager as FNGroupManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="GCareer"/>.
		/// </summary>
		public GCareerManager GCareers
		{
			get
			{
				var manager = this.GetManager(typeof(GCareerManager));
				return manager == null ? null : manager as GCareerManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="Material"/>.
		/// </summary>
		public MaterialManager Materials
		{
			get
			{
				var manager = this.GetManager(typeof(MaterialManager));
				return manager == null ? null : manager as MaterialManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="PresetRide"/>.
		/// </summary>
		public PresetRideManager PresetRides
		{
			get
			{
				var manager = this.GetManager(typeof(PresetRideManager));
				return manager == null ? null : manager as PresetRideManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="SlotOverride"/>.
		/// </summary>
		public SlotOverrideManager SlotOverrides
		{
			get
			{
				var manager = this.GetManager(typeof(SlotOverrideManager));
				return manager == null ? null : manager as SlotOverrideManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="SlotType"/>.
		/// </summary>
		public SlotTypeManager SlotTypes
		{
			get
			{
				var manager = this.GetManager(typeof(SlotTypeManager));
				return manager == null ? null : manager as SlotTypeManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="STRBlock"/>.
		/// </summary>
		public STRBlockManager STRBlocks
		{
			get
			{
				var manager = this.GetManager(typeof(STRBlockManager));
				return manager == null ? null : manager as STRBlockManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="SunInfo"/>.
		/// </summary>
		public SunInfoManager SunInfos
		{
			get
			{
				var manager = this.GetManager(typeof(SunInfoManager));
				return manager == null ? null : manager as SunInfoManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="TPKBlock"/>.
		/// </summary>
		public TPKBlockManager TPKBlocks
		{
			get
			{
				var manager = this.GetManager(typeof(TPKBlockManager));
				return manager == null ? null : manager as TPKBlockManager;
			}
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="Track"/>.
		/// </summary>
		public TrackManager Tracks
		{
			get
			{
				var manager = this.GetManager(typeof(TrackManager));
				return manager == null ? null : manager as TrackManager;
			}
		}

		/// <summary>
		/// Loads all data in the database using options passed.
		/// </summary>
		/// <param name="options"><see cref="Options"/> that are used to load data.</param>
		public override void Load(Options options)
		{
			using var loader = new DatabaseLoader(options, this);
			loader.Invoke();
		}

		/// <summary>
		/// Saves all data in the database using options passed.
		/// </summary>
		/// <param name="options"><see cref="Options"/> that are used to save data.</param>
		public override void Save(Options options)
		{
			using var saver = new DatabaseSaver(options, this);
			saver.Invoke();
		}

		/// <summary>
		/// Exports collection by writing its data to a <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="manager">Name of <see cref="IManager"/> to which collection belongs to.</param>
		/// <param name="cname">CollectionName of collection to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="serialized">True if collection should be serialized; false if plainly 
		/// exported.</param>
		public override void Export(string manager, string cname, BinaryWriter bw, bool serialized = true)
		{
			var root = this.GetManager(manager);

			if (manager == null)
			{

				throw new Exception($"Cannot find manager named {manager}");

			}

			root.Export(cname, bw, serialized);
		}

		/// <summary>
		/// Imports collection by reading its data from a <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="type"><see cref="SerializeType"/> type of importing collection.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Import(SerializeType type, BinaryReader br)
		{
			var position = br.BaseStream.Position;
			var header = new SerializationHeader();
			header.Read(br);

			if (header.ID != BinBlockID.Nikki)
			{

				throw new Exception($"Missing serialized header in the imported collection");

			}

			if (header.Game != this.GameINT)
			{

				throw new Exception($"Stated game inside collection is {header.Game}, while should be {this.GameINT}");

			}

			var manager = this.GetManager(header.Name);

			if (manager == null)
			{

				throw new Exception($"Cannot find manager named {header.Name}");

			}

			br.BaseStream.Position = position;
			manager.Import(type, br);
		}

		/// <summary>
		/// Imports collection by reading its data from a <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="type"><see cref="SerializeType"/> type of importing collection.</param>
		/// <param name="manager">Name of <see cref="IManager"/> to invoke for import.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Import(SerializeType type, string manager, BinaryReader br)
		{
			var root = this.GetManager(manager);

			if (manager == null)
			{

				throw new Exception($"Cannot find manager named {manager}");

			}

			root.Import(type, br);
		}

		/// <summary>
		/// Gets information about <see cref="Datamap"/> database.
		/// </summary>
		/// <returns>Info about this database as a string value.</returns>
		public override string GetDatabaseInfo() => throw new NotImplementedException();
	}
}
