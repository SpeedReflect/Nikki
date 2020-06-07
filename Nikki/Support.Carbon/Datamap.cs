using Nikki.Core;
using Nikki.Reflection.Abstract;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Carbon.Framework;



namespace Nikki.Support.Carbon
{
	/// <summary>
	/// <see cref="Datamap"/> is an extension of <see cref="FileBase"/> for Carbon support.
	/// </summary>
	public class Datamap : FileBase
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Carbon;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => this.GameINT.ToString();

		/// <summary>
		/// Initializes new instance of <see cref="Datamap"/>.
		/// </summary>
		public Datamap() : base()
		{
			this.Managers.Add(new CarSlotInfoManager(this));
			this.Managers.Add(new CarTypeInfoManager(this));
			this.Managers.Add(new CollisionManager(this));
			this.Managers.Add(new DBModelPartManager(this));
			this.Managers.Add(new FNGroupManager(this));
			this.Managers.Add(new MaterialManager(this));
			this.Managers.Add(new PresetRideManager(this));
			this.Managers.Add(new PresetSkinManager(this));
			this.Managers.Add(new SlotTypeManager(this));
			this.Managers.Add(new STRBlockManager(this));
			this.Managers.Add(new SunInfoManager(this));
			this.Managers.Add(new TPKBlockManager(this));
			this.Managers.Add(new TrackManager(this));
		}

		/// <summary>
		/// <see cref="Manager{T}"/> that manages <see cref="CarSlotInfo"/>.
		/// </summary>
		public CarSlotInfoManager CarSlotInfos
		{
			get
			{
				var manager = this.GetManager(typeof(CarSlotInfoManager));
				return manager == null ? null : manager as CarSlotInfoManager;
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
		/// <see cref="Manager{T}"/> that manages <see cref="Collision"/>.
		/// </summary>
		public CollisionManager Collisions
		{
			get
			{
				var manager = this.GetManager(typeof(CollisionManager));
				return manager == null ? null : manager as CollisionManager;
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
		/// <see cref="Manager{T}"/> that manages <see cref="PresetSkin"/>.
		/// </summary>
		public PresetSkinManager PresetSkins
		{
			get
			{
				var manager = this.GetManager(typeof(PresetSkinManager));
				return manager == null ? null : manager as PresetSkinManager;
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
		/// <returns>True on success; false otherwise.</returns>
		public override bool Load(Options options)
		{
			var loader = new DatabaseLoader(options, this);
			return loader.Invoke();
		}

		/// <summary>
		/// Saves all data in the database using options passed.
		/// </summary>
		/// <param name="options"><see cref="Options"/> that are used to save data.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool Save(Options options)
		{
			var saver = new DatabaseSaver(options, this);
			return saver.Invoke();
		}

		/// <summary>
		/// Gets information about <see cref="Datamap"/> database.
		/// </summary>
		/// <returns>Info about this database as a string value.</returns>
		public override string GetDatabaseInfo() => throw new System.NotImplementedException();
	}
}
