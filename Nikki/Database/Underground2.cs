using System;
using Nikki.Core;
using Nikki.Reflection.Abstract;
using Nikki.Support.Underground2.Class;
using Nikki.Support.Underground2.Gameplay;
using Nikki.Support.Underground2.Framework;



namespace Nikki.Database
{
	/// <summary>
	/// Database of roots and collections for Need for Speed: Underground 2.
	/// </summary>
	public class Underground2 : ABasicBase
	{
        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.Underground2;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Underground2.ToString();

        /// <summary>
        /// 
        /// </summary>
        public Root<Material> Materials { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<CarTypeInfo> CarTypeInfos { get; set; }
        
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
        public Root<WorldShop> WorldShops { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<GCareerBrand> GCareerBrands { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<PartPerformance> PartPerformances { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<GShowcase> GShowcases { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<SMSMessage> SMSMessages { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<Sponsor> Sponsors { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<GCareerStage> GCareerStages { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<PerfSliderTuning> PerfSliderTunings { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<WorldChallenge> WorldChallenges { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<PartUnlockable> PartUnlockables { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<BankTrigger> BankTriggers { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<GCarUnlock> GCarUnlocks { get; set; }
        
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

		/// <summary>
		/// Initializes new instance of <see cref="Underground2"/>.
		/// </summary>
		public Underground2() { }

		/// <summary>
		/// Initializes new instance of <see cref="Underground2"/>.
		/// </summary>
		/// <param name="initialize">True if all roots should be auto-initialized.</param>
		public Underground2(bool initialize)
        {
            if (initialize) this.Initialize();
        }

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~Underground2()
        {
			this.Buffer = null;
            this.CarTypeInfos = null;
            this.FNGroups = null;
            this.Materials = null;
            this.PresetRides = null;
            this.SunInfos = null;
            this.Tracks = null;
            this.TPKBlocks = null;
            this.ModelParts = null;
            this.STRBlocks = null;
            this.GCareerRaces = null;
            this.WorldShops = null;
            this.GCareerBrands = null;
            this.PartPerformances = null;
            this.GShowcases = null;
            this.SMSMessages = null;
            this.Sponsors = null;
            this.GCareerStages = null;
            this.PerfSliderTunings = null;
            this.WorldChallenges = null;
            this.PartUnlockables = null;
            this.BankTriggers = null;
            this.GCarUnlocks = null;
            this.AcidEffects = null;
        }

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

			this.CarTypeInfos = new Root<CarTypeInfo>
			(
				"CarTypeInfos",
				CarTypeInfo.BaseClassSize,
				true,
				true,
				this
			);

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

			this.WorldShops = new Root<WorldShop>
			(
				"WorldShops",
				-1,
				true,
				false,
				this
			);

			this.GCareerBrands = new Root<GCareerBrand>
			(
				"GCareerBrands",
				-1,
				true,
				false,
				this
			);


			this.PartPerformances = new Root<PartPerformance>
			(
				"PartPerformances",
				-1,
				true,
				false,
				this
			);

			this.GShowcases = new Root<GShowcase>
			(
				"GShowcases",
				-1,
				true,
				false,
				this
			);

			this.SMSMessages = new Root<SMSMessage>
			(
				"SMSMessages",
				-1,
				false,
				false,
				this
			);

			this.Sponsors = new Root<Sponsor>
			(
				"Sponsors",
				-1,
				true,
				false,
				this
			);

			this.GCareerStages = new Root<GCareerStage>
			(
				"GCareerStages",
				-1,
				false,
				false,
				this
			);

			this.PerfSliderTunings = new Root<PerfSliderTuning>
			(
				"PerfSliderTunings",
				-1,
				false,
				false,
				this
			);

			this.WorldChallenges = new Root<WorldChallenge>
			(
				"WorldChallenges",
				-1,
				true,
				false,
				this
			);

			this.PartUnlockables = new Root<PartUnlockable>
			(
				"PartUnlockables",
				-1,
				false,
				false,
				this
			);

			this.BankTriggers = new Root<BankTrigger>
			(
				"BankTriggers",
				-1,
				true,
				true,
				this
			);

			this.GCarUnlocks = new Root<GCarUnlock>
			(
				"GCarUnlocks",
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
			info += $"{this.BankTriggers.ThisName}{equals}{this.BankTriggers.Length}{collections}{nl}";
			info += $"{this.CarTypeInfos.ThisName}{equals}{this.CarTypeInfos.Length}{collections}{nl}";
			info += $"{this.GCareerBrands.ThisName}{equals}{this.GCareerBrands.Length}{collections}{nl}";
			info += $"{this.GCareerRaces.ThisName}{equals}{this.GCareerRaces.Length}{collections}{nl}";
			info += $"{this.GCareerStages.ThisName}{equals}{this.GCareerStages.Length}{collections}{nl}";
			info += $"{this.GCarUnlocks.ThisName}{equals}{this.GCarUnlocks.Length}{collections}{nl}";
			info += $"{this.GShowcases.ThisName}{equals}{this.GShowcases.Length}{collections}{nl}";
			info += $"{this.Materials.ThisName}{equals}{this.Materials.Length}{collections}{nl}";
			info += $"{this.ModelParts.ThisName}{equals}{this.ModelParts.Length}{collections}{nl}";
			info += $"{this.PartPerformances.ThisName}{equals}{this.PartPerformances.Length}{collections}{nl}";
			info += $"{this.PartUnlockables.ThisName}{equals}{this.PartUnlockables.Length}{collections}{nl}";
			info += $"{this.PerfSliderTunings.ThisName}{equals}{this.PerfSliderTunings.Length}{collections}{nl}";
			info += $"{this.PresetRides.ThisName}{equals}{this.PresetRides.Length}{collections}{nl}";
			info += $"{this.SMSMessages.ThisName}{equals}{this.SMSMessages.Length}{collections}{nl}";
			info += $"{this.Sponsors.ThisName}{equals}{this.Sponsors.Length}{collections}{nl}";
			info += $"{this.SunInfos.ThisName}{equals}{this.SunInfos.Length}{collections}{nl}";
			info += $"{this.Tracks.ThisName}{equals}{this.Tracks.Length}{collections}{nl}";
			info += $"{this.WorldChallenges.ThisName}{equals}{this.WorldChallenges.Length}{collections}{nl}";
			info += $"{this.WorldShops.ThisName}{equals}{this.WorldShops.Length}{collections}{nl}";
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
	}
}
