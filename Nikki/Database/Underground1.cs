using System;
using Nikki.Core;
using Nikki.Reflection.Abstract;
using Nikki.Support.Underground1.Class;
using Nikki.Support.Underground1.Gameplay;
using Nikki.Support.Underground1.Framework;



namespace Nikki.Database
{
	/// <summary>
	/// Database of roots and collections for Need for Speed: Underground 2.
	/// </summary>
	public class Underground1 : ABasicBase
	{
        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.Underground1;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Underground1.ToString();

		#region Roots

		/// <summary>
		/// 
		/// </summary>
		public Root<Material> Materials { get; set; }
        
        //public Root<CarTypeInfo> CarTypeInfos { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<PresetRide> PresetRides { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<SunInfo> SunInfos { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<Track> Tracks { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<GCareerRace> GCareerRaces { get; set; }
                
        /// <summary>
        /// 
        /// </summary>
        public Root<AcidEffect> AcidEffects { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<FNGroup> FNGroups { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<TPKBlock> TPKBlocks { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<STRBlock> STRBlocks { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<DBModelPart> ModelParts { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="Underground1"/>.
		/// </summary>
		public Underground1() { }

		/// <summary>
		/// Initializes new instance of <see cref="Underground1"/>.
		/// </summary>
		/// <param name="initialize">True if all roots should be auto-initialized.</param>
		public Underground1(bool initialize)
        {
            if (initialize) this.Initialize();
        }

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~Underground1()
        {
			this.Buffer = null;
            //this.CarTypeInfos = null;
            this.FNGroups = null;
            this.Materials = null;
            this.PresetRides = null;
            this.SunInfos = null;
            this.Tracks = null;
            this.TPKBlocks = null;
            this.ModelParts = null;
            this.STRBlocks = null;
            this.GCareerRaces = null;
            this.AcidEffects = null;
        }

		#endregion

		#region Methods

		private void Initialize()
		{
			this.Materials = new Root<Material>
			(
				"Materials",
				Material.BaseClassSize,
				true,
				true,
				this
			);

			//this.CarTypeInfos = new Root<CarTypeInfo>
			//(
			//	"CarTypeInfos",
			//	CarTypeInfo.BaseClassSize,
			//	true,
			//	true,
			//	this
			//);

			this.PresetRides = new Root<PresetRide>
			(
				"PresetRides",
				PresetRide.BaseClassSize,
				true,
				true,
				this
			);

			this.SunInfos = new Root<SunInfo>
			(
				"SunInfos",
				SunInfo.BaseClassSize,
				true,
				true,
				this
			);

			this.Tracks = new Root<Track>
			(
				"Tracks",
				Track.BaseClassSize,
				true,
				false,
				this
			);

			this.GCareerRaces = new Root<GCareerRace>
			(
				"GCareerRaces",
				-1,
				true,
				false,
				this
			);


			this.AcidEffects = new Root<AcidEffect>
			(
				"AcidEffects",
				AcidEffect.BaseClassSize,
				true,
				true,
				this
			);

			this.FNGroups = new Root<FNGroup>
			(
				"FNGroups",
				-1,
				false,
				false,
				this
			);

			this.TPKBlocks = new Root<TPKBlock>
			(
				"TPKBlocks",
				-1,
				true,
				true,
				this
			);

			this.STRBlocks = new Root<STRBlock>
			(
				"STRBlocks",
				-1,
				true,
				true,
				this
			);

			this.ModelParts = new Root<DBModelPart>
			(
				"ModelParts",
				-1,
				true,
				false,
				this
			);
		}

		/// <summary>
		/// Returns name of the database.
		/// </summary>
		/// <returns>Name of this database as a string value.</returns>
		public override string ToString()
		{
			return "Database (Underground2)";
		}

		/// <summary>
		/// Gets information about <see cref="Underground2"/> database.
		/// </summary>
		/// <returns></returns>
		public override string GetDatabaseInfo()
		{
			string nl = Environment.NewLine;
			string equals = " = ";
			string collections = " collections.";
			string info = this.ToString() + nl;
			info += $"{this.AcidEffects.ThisName}{equals}{this.AcidEffects.Length}{collections}{nl}";
			//info += $"{this.CarTypeInfos.ThisName}{equals}{this.CarTypeInfos.Length}{collections}{nl}";
			info += $"{this.GCareerRaces.ThisName}{equals}{this.GCareerRaces.Length}{collections}{nl}";
			info += $"{this.Materials.ThisName}{equals}{this.Materials.Length}{collections}{nl}";
			info += $"{this.ModelParts.ThisName}{equals}{this.ModelParts.Length}{collections}{nl}";
			info += $"{this.PresetRides.ThisName}{equals}{this.PresetRides.Length}{collections}{nl}";
			info += $"{this.SunInfos.ThisName}{equals}{this.SunInfos.Length}{collections}{nl}";
			info += $"{this.Tracks.ThisName}{equals}{this.Tracks.Length}{collections}{nl}";
			return info;
		}

		/// <summary>
		/// Loads all data in the database using options passed.
		/// </summary>
		/// <param name="options"><see cref="Options"/> that are used to load data.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool Load(Options options) => false;// Loader.Invoke(options, this);

		/// <summary>
		/// Saves all data in the database using options passed.
		/// </summary>
		/// <param name="options"><see cref="Options"/> that are used to save data.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool Save(Options options) => false;// Saver.Invoke(options, this);

		#endregion
	}
}
