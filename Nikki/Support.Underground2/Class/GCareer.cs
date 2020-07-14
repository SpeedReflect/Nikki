using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Underground2.Gameplay;
using Nikki.Support.Underground2.Framework;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="GCareer"/> is a collection of gameplay classes.
    /// </summary>
    public class GCareer : Shared.Class.GCareer
    {
        #region Fields

        private string _collection_name;
        private List<BankTrigger> _bank_triggers;
        private List<GCareerBrand> _gcareer_brands;
        private List<GCareerRace> _gcareer_races;
        private List<GCareerStage> _gcareer_stages;
        private List<GCarUnlock> _gcar_unlocks;
        private List<GShowcase> _gshowcases;
        //private List<PartPerformance> _part_performances;
        private List<PartUnlockable> _part_unlockables;
        private List<PerfSliderTuning> _perfslider_tunings;
        private List<SMSMessage> _sms_messages;
        private List<Sponsor> _sponsors;
        private List<WorldChallenge> _world_challenges;
        private List<WorldShop> _world_shops;

        private const long max = 0x7FFFFFFF;

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
        /// Manager to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public GCareerManager Manager { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
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
        /// <see cref="List{T}"/> of <see cref="BankTrigger"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<BankTrigger> BankTriggers => this._bank_triggers;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="GCareerBrand"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<GCareerBrand> GCareerBrands => this._gcareer_brands;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="GCareerRace"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<GCareerRace> GCareerRaces => this._gcareer_races;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="GCareerStage"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<GCareerStage> GCareerStages => this._gcareer_stages;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="GCarUnlock"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<GCarUnlock> GCarUnlocks => this._gcar_unlocks;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="GShowcase"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<GShowcase> GShowcases => this._gshowcases;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="PartPerformance"/> in this <see cref="GCareer"/>.
        /// </summary>
        ///[Browsable(false)]
        ///public List<PartPerformance> PartPerformances => this._part_performances;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="PartUnlockable"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<PartUnlockable> PartUnlockables => this._part_unlockables;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="PerfSliderTuning"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<PerfSliderTuning> PerfSliderTunings => this._perfslider_tunings;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="SMSMessage"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<SMSMessage> SMSMessages => this._sms_messages;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="Sponsor"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<Sponsor> Sponsors => this._sponsors;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="WorldChallenge"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<WorldChallenge> WorldChallenges => this._world_challenges;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="WorldShop"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public List<WorldShop> WorldShops => this._world_shops;

        /// <summary>
        /// Total count of <see cref="BankTrigger"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int BankTriggerCount => this._bank_triggers.Count;

        /// <summary>
        /// Total count of <see cref="GCareerBrand"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int GCareerBrandCount => this._gcareer_brands.Count;

        /// <summary>
        /// Total count of <see cref="GCareerRace"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int GCareerRaceCount => this._gcareer_races.Count;

        /// <summary>
        /// Total count of <see cref="GCareerStage"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int GCareerStageCount => this._gcareer_stages.Count;

        /// <summary>
        /// Total count of <see cref="GCarUnlock"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int GCarUnlockCount => this._gcar_unlocks.Count;

        /// <summary>
        /// Total count of <see cref="GShowcase"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int GShowcaseCount => this._gshowcases.Count;

        /// <summary>
        /// Total count of <see cref="PartPerformance"/> in this <see cref="GCareer"/>.
        /// </summary>
        ///[Category("Primary")]
        ///public int PartPerformanceCount => this._part_performances.Count;

        /// <summary>
        /// Total count of <see cref="PartUnlockable"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int PartUnlockableCount => this._part_unlockables.Count;

        /// <summary>
        /// Total count of <see cref="PerfSliderTuning"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int PerfSliderTuningCount => this._perfslider_tunings.Count;

        /// <summary>
        /// Total count of <see cref="SMSMessage"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int SMSMessageCount => this._sms_messages.Count;

        /// <summary>
        /// Total count of <see cref="Sponsor"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int SponsorCount => this._sponsors.Count;

        /// <summary>
        /// Total count of <see cref="WorldChallenge"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int WorldChallengeCount => this._world_challenges.Count;

        /// <summary>
        /// Total count of <see cref="WorldShop"/> in this <see cref="GCareer"/>.
        /// </summary>
        [Category("Primary")]
        public int WorldShopCount => this._world_shops.Count;

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="GCareer"/>.
        /// </summary>
        public GCareer()
		{
            this._bank_triggers = new List<BankTrigger>();
            this._gcareer_brands = new List<GCareerBrand>();
            this._gcareer_races = new List<GCareerRace>();
            this._gcareer_stages = new List<GCareerStage>();
            this._gcar_unlocks = new List<GCarUnlock>();
            this._gshowcases = new List<GShowcase>();
            //this._part_performances = new List<PartPerformance>();
            this._part_unlockables = new List<PartUnlockable>();
            this._perfslider_tunings = new List<PerfSliderTuning>();
            this._sms_messages = new List<SMSMessage>();
            this._sponsors = new List<Sponsor>();
            this._world_challenges = new List<WorldChallenge>();
            this._world_shops = new List<WorldShop>();
		}

        /// <summary>
        /// Initializes new instance of <see cref="GCareer"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="GCareerManager"/> to which this instance belongs to.</param>
        public GCareer(string CName, GCareerManager manager) : this()
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="GCareer"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="GCareerManager"/> to which this instance belongs to.</param>
        public GCareer(BinaryReader br, GCareerManager manager) : this()
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Assembles <see cref="GCareer"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="TPKBlock"/> with.</param>
        /// <returns>Byte array of the tpk block.</returns>
        public override void Assemble(BinaryWriter bw)
        {

        }

        /// <summary>
        /// Disassembles <see cref="GCareer"/> array into separate properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Disassemble(BinaryReader br)
        {

        }

        /// <summary>
        /// Gets all collections of type <see cref="Collectable"/>.
        /// </summary>
        /// <typeparam name="T">A <see cref="Collectable"/> collections to get.</typeparam>
        /// <returns>Collections of type specified, if type is registered; null otherwise.</returns>
        public override IEnumerable<T> GetCollections<T>()
		{

            return null;
		}

        /// <summary>
        /// Gets collection of with CollectionName specified from a root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to get.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        /// <returns>Collection, if exists; null otherwise.</returns>
        public override Collectable GetCollection(string cname, string root)
		{

            return null;
		}

        /// <summary>
        /// Adds a unit collection at a root provided with CollectionName specified.
        /// </summary>
        /// <param name="cname">CollectionName of a new collection.</param>
        /// <param name="root">Root to which collection should belong to.</param>
        public override void AddCollection(string cname, string root)
		{

		}

        /// <summary>
        /// Removes collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to remove.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public override void RemoveCollection(string cname, string root)
		{

		}

        /// <summary>
        /// Clones collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="newname">CollectionName of a new cloned collection.</param>
        /// <param name="copyname">CollectionName of a collection to clone.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public override void CloneCollection(string newname, string copyname, string root)
		{

		}

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="TPKBlock"/> 
        /// as a string value.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return $"Collection Name: {this.CollectionName} | " +
                   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
        }

        #endregion

        #region Reading Methods

        /// <summary>
        /// Finds offsets of all partials and its parts in the <see cref="GCareer"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCareer"/> with.</param>
        /// <returns>Array of all offsets.</returns>
        protected override long[] FindOffsets(BinaryReader br)
        {



            return null;
        }




        #endregion

        #region Serialization

        /// <summary>
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public override void Serialize(BinaryWriter bw)
        {

        }

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Deserialize(BinaryReader br)
        {

        }

        /// <summary>
        /// Synchronizes all parts of this instance with another instance passed.
        /// </summary>
        /// <param name="other"><see cref="GCareer"/> to synchronize with.</param>
        internal void Synchronize(GCareer other)
        {

        }

        #endregion
    }
}