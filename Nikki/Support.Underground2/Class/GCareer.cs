using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Gameplay;
using Nikki.Support.Underground2.Framework;
using CoreExtensions.IO;



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
        private List<PartPerformance> _part_performances;
        private List<PartUnlockable> _part_unlockables;
        private List<PerfSliderTuning> _perfslider_tunings;
        private List<SMSMessage> _sms_messages;
        private List<Sponsor> _sponsors;
        private List<WorldChallenge> _world_challenges;
        private List<WorldShop> _world_shops;
        private Dictionary<string, ConstructorInfo> _activators;

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
        /// Watermark written during assembly.
        /// </summary>
        internal string Watermark { get; set; }

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
        [Browsable(false)]
        public List<PartPerformance> PartPerformances => this._part_performances;

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
        [Category("Primary")]
        public int PartPerformanceCount => this._part_performances.Count;

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

        /// <summary>
        /// Represents array of <see cref="IList"/> of <see cref="Collectable"/> collections.
        /// </summary>
        [Browsable(false)]
        public override IList[] AllCollections => new IList[]
        {
            this.BankTriggers,
            this.GCareerBrands,
            this.GCareerRaces,
            this.GCareerStages,
            this.GCarUnlocks,
            this.GShowcases,
            this.PartPerformances,
            this.PartUnlockables,
            this.PerfSliderTunings,
            this.SMSMessages,
            this.Sponsors,
            this.WorldChallenges,
            this.WorldShops,
        };

        /// <summary>
        /// Represents array of all root names in this <see cref="GCareer"/>.
        /// </summary>
        [Browsable(false)]
        public override string[] AllRootNames => this.GetRootNames();

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
            this._part_performances = new List<PartPerformance>();
            this._part_unlockables = new List<PartUnlockable>();
            this._perfslider_tunings = new List<PerfSliderTuning>();
            this._sms_messages = new List<SMSMessage>();
            this._sponsors = new List<Sponsor>();
            this._world_challenges = new List<WorldChallenge>();
            this._world_shops = new List<WorldShop>();
            this._activators = new Dictionary<string, ConstructorInfo>(13);
            this.GenerateActivatorMap();
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
            using var ms = new MemoryStream(0x8000);
            using var strw = new BinaryWriter(ms);
            strw.Write((byte)0);
            strw.WriteEnum(BinBlockID.Nikki);
            strw.WriteNullTermUTF8(this._collection_name);
            strw.WriteNullTermUTF8(this.Watermark);

            var GCareerRacesBlock = this.WriteGCareerRaces(strw);
            var WorldShopBlock = this.WriteWorldShops(strw);
            var GCareerBrandsBlock = this.WriteGCareerBrands(strw);
            var PartPerformancesBlock = this.WritePartPerformances();
            var GShowcasesBlock = this.WriteGShowcases(strw);
            var SMSMessagesBlock = this.WriteSMSMessages(strw);
            var SponsorsBlock = this.WriteSponsors(strw);
            var GCareerStagesBlock = this.WriteGCareerStages();
            var PerfSliderTuningsBlock = this.WritePerfSliderTunings();
            var WorldChallengesBlock = this.WriteWorldChallenges(strw);
            var PartUnlockablesBlock = this.WritePartUnlockables();
            var BankTriggersBlock = this.WriteBankTriggers();
            var GCarUnlocksBlock = this.WriteGCarUnlocks();

            strw.FillBuffer(4);
            var StringBlock = ms.ToArray();

            var size = 8 + StringBlock.Length;
            size += GCareerRacesBlock.Length;
            size += WorldShopBlock.Length;
            size += GCareerBrandsBlock.Length;
            size += PartPerformancesBlock.Length;
            size += GShowcasesBlock.Length;
            size += SMSMessagesBlock.Length;
            size += SponsorsBlock.Length;
            size += GCareerStagesBlock.Length;
            size += PerfSliderTuningsBlock.Length;
            size += WorldChallengesBlock.Length;
            size += PartUnlockablesBlock.Length;
            size += BankTriggersBlock.Length;
            size += GCarUnlocksBlock.Length;

            bw.WriteEnum(BinBlockID.GCareer);
            bw.Write(size);

            bw.WriteEnum(BinBlockID.GCareer_Strings);
            bw.Write(StringBlock.Length);
            bw.Write(StringBlock);

            bw.Write(GCareerRacesBlock);
            bw.Write(WorldShopBlock);
            bw.Write(GCareerBrandsBlock);
            bw.Write(PartPerformancesBlock);
            bw.Write(GShowcasesBlock);
            bw.Write(SMSMessagesBlock);
            bw.Write(SponsorsBlock);
            bw.Write(GCareerStagesBlock);
            bw.Write(PerfSliderTuningsBlock);
            bw.Write(WorldChallengesBlock);
            bw.Write(PartUnlockablesBlock);
            bw.Write(BankTriggersBlock);
            bw.Write(GCarUnlocksBlock);
        }

        /// <summary>
        /// Disassembles <see cref="GCareer"/> array into separate properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 4;
            var size = br.ReadInt32();
            var start = br.BaseStream.Position;
            var offsets = this.FindOffsets(br, size);

            MemoryStream ms = null;
            BinaryReader strr = null;

            if (offsets[0] != max)
			{

                br.BaseStream.Position = offsets[0];
                var total = br.ReadInt32();
                ms = new MemoryStream(br.ReadBytes(total));
                strr = new BinaryReader(ms);

			}

            br.BaseStream.Position = offsets[0];
            this.ReadStrings(br);

            br.BaseStream.Position = offsets[1];
            this.ReadGCareerRaces(br, strr);

            br.BaseStream.Position = offsets[2];
            this.ReadWorldShops(br);

            br.BaseStream.Position = offsets[3];
            this.ReadGCareerBrands(br);

            br.BaseStream.Position = offsets[4];
            this.ReadPartPerformances(br);

            br.BaseStream.Position = offsets[5];
            this.ReadGShowcases(br);

            br.BaseStream.Position = offsets[6];
            this.ReadSMSMessages(br, strr);

            br.BaseStream.Position = offsets[7];
            this.ReadSponsors(br, strr);

            br.BaseStream.Position = offsets[8];
            this.ReadGCareerStages(br);

            br.BaseStream.Position = offsets[9];
            this.ReadPerfSliderTunings(br);

            br.BaseStream.Position = offsets[10];
            this.ReadWorldChallenges(br, strr);

            br.BaseStream.Position = offsets[11];
            this.ReadPartUnlockables(br);

            br.BaseStream.Position = offsets[12];
            this.ReadBankTriggers(br);

            br.BaseStream.Position = offsets[13];
            this.ReadGCarUnlocks(br);

            br.BaseStream.Position = start + size;
        }

        /// <summary>
        /// Returns an <see cref="IList"/> root with name specified.
        /// </summary>
        /// <param name="root">Name of a root to get.</param>
        /// <returns>Root with name specified as an <see cref="IList"/>.</returns>
        public override IList GetRoot(string root)
		{
            return root switch
            {
                nameof(this.BankTriggers) => this.BankTriggers,
                nameof(this.GCareerBrands) => this.GCareerBrands,
                nameof(this.GCareerRaces) => this.GCareerRaces,
                nameof(this.GCareerStages) => this.GCareerStages,
                nameof(this.GCarUnlocks) => this.GCarUnlocks,
                nameof(this.GShowcases) => this.GShowcases,
                nameof(this.PartPerformances) => this.PartPerformances,
                nameof(this.PartUnlockables) => this.PartUnlockables,
                nameof(this.PerfSliderTunings) => this.PerfSliderTunings,
                nameof(this.SMSMessages) => this.SMSMessages,
                nameof(this.Sponsors) => this.Sponsors,
                nameof(this.WorldChallenges) => this.WorldChallenges,
                nameof(this.WorldShops) => this.WorldShops,
                _ => null,
            };
        }

        /// <summary>
        /// Gets collection of with CollectionName specified from a root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to get.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        /// <returns>Collection, if exists; null otherwise.</returns>
        public override Collectable GetCollection(string cname, string root)
		{
            return root switch
            {
                nameof(this.BankTriggers) => this.BankTriggers.Find(_ => _.CollectionName == cname),
                nameof(this.GCareerBrands) => this.GCareerBrands.Find(_ => _.CollectionName == cname),
                nameof(this.GCareerRaces) => this.GCareerRaces.Find(_ => _.CollectionName == cname),
                nameof(this.GCareerStages) => this.GCareerStages.Find(_ => _.CollectionName == cname),
                nameof(this.GCarUnlocks) => this.GCarUnlocks.Find(_ => _.CollectionName == cname),
                nameof(this.GShowcases) => this.GShowcases.Find(_ => _.CollectionName == cname),
                nameof(this.PartPerformances) => this.PartPerformances.Find(_ => _.CollectionName == cname),
                nameof(this.PartUnlockables) => this.PartUnlockables.Find(_ => _.CollectionName == cname),
                nameof(this.PerfSliderTunings) => this.PerfSliderTunings.Find(_ => _.CollectionName == cname),
                nameof(this.SMSMessages) => this.SMSMessages.Find(_ => _.CollectionName == cname),
                nameof(this.Sponsors) => this.Sponsors.Find(_ => _.CollectionName == cname),
                nameof(this.WorldChallenges) => this.WorldChallenges.Find(_ => _.CollectionName == cname),
                nameof(this.WorldShops) => this.WorldShops.Find(_ => _.CollectionName == cname),
                _ => null,
            };
		}

        /// <summary>
        /// Adds a unit collection at a root provided with CollectionName specified.
        /// </summary>
        /// <param name="cname">CollectionName of a new collection.</param>
        /// <param name="root">Root to which collection should belong to.</param>
        public override void AddCollection(string cname, string root)
		{
            if (root == nameof(this.PartUnlockables))
			{

                throw new Exception("PartUnlockables cannot be added");

			}

            if (!this._activators.TryGetValue(root, out var constructor))
			{

                throw new Exception($"Root named {cname} does not exist");

			}
            else
			{

                var collection = constructor.Invoke(new object[] { cname, this });
                var manager = this.GetRoot(root);
                manager.Add(collection);

			}
		}

        /// <summary>
        /// Removes collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="cname">CollectionName of a collection to remove.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public override void RemoveCollection(string cname, string root)
		{
            if (root == nameof(this.PartUnlockables))
			{

                throw new Exception("PartUnlockables cannot be removed");

			}

            var manager = this.GetRoot(root);
            if (manager is null)
			{

                throw new Exception($"Root named {root} does not exist");

			}

            int index = 0;

            foreach (Collectable collection in manager)
			{

                if (collection.CollectionName == cname) break;
                ++index;

			}

            if (index < manager.Count) manager.RemoveAt(index);
            else throw new Exception($"Collection named {cname} does not exist");
        }

        /// <summary>
        /// Clones collection with CollectionName specified at the root provided.
        /// </summary>
        /// <param name="newname">CollectionName of a new cloned collection.</param>
        /// <param name="copyname">CollectionName of a collection to clone.</param>
        /// <param name="root">Root to which collection belongs to.</param>
        public override void CloneCollection(string newname, string copyname, string root)
		{
            if (root == nameof(this.PartUnlockables))
			{

                throw new Exception("PartUnlockables cannot be copied");

			}

            var manager = this.GetRoot(root);
            var collection = this.GetCollection(copyname, root);

            if (manager is null)
			{

                throw new Exception($"Root named {root} does not exist");

			}

            if (collection is null)
			{

                throw new Exception($"Collection {copyname} does not exist");

			}

            var instance = collection.MemoryCast(newname);
            manager.Add(instance);
		}

        private void GenerateActivatorMap()
        {
            Type type;
            ConstructorInfo constructor;

            type = typeof(BankTrigger);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.BankTriggers)] = constructor;

            type = typeof(GCareerBrand);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.GCareerBrands)] = constructor;

            type = typeof(GCareerRace);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.GCareerRaces)] = constructor;

            type = typeof(GCareerStage);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.GCareerStages)] = constructor;

            type = typeof(GCarUnlock);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.GCarUnlocks)] = constructor;

            type = typeof(GShowcase);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.GShowcases)] = constructor;

            type = typeof(PartPerformance);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.PartPerformances)] = constructor;

            type = typeof(PartUnlockable);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.PartUnlockables)] = constructor;

            type = typeof(PerfSliderTuning);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.PerfSliderTunings)] = constructor;

            type = typeof(SMSMessage);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.SMSMessages)] = constructor;

            type = typeof(Sponsor);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.Sponsors)] = constructor;

            type = typeof(WorldChallenge);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.WorldChallenges)] = constructor;

            type = typeof(WorldShop);
            constructor = type.GetConstructor(new Type[] { typeof(string), typeof(GCareer) });
            this._activators[nameof(this.WorldShops)] = constructor;
        }

        private string[] GetRootNames() => new string[]
        {
            nameof(this.BankTriggers),
            nameof(this.GCareerBrands),
            nameof(this.GCareerRaces),
            nameof(this.GCareerStages),
            nameof(this.GCarUnlocks),
            nameof(this.GShowcases),
            nameof(this.PartPerformances),
            nameof(this.PartUnlockables),
            nameof(this.PerfSliderTunings),
            nameof(this.SMSMessages),
            nameof(this.Sponsors),
            nameof(this.WorldChallenges),
            nameof(this.WorldShops),
        };

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

        private long[] FindOffsets(BinaryReader br, int size)
        {
            var result = new long[14];
            var offset = br.BaseStream.Position;
            for (int i = 0; i < 14; ++i) result[i] = max;

            while (br.BaseStream.Position < offset + size)
            {

                var id = br.ReadEnum<BinBlockID>();

                switch (id)
                {
                    case BinBlockID.GCareer_Strings:
                        result[0] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Races:
                        result[1] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Shops:
                        result[2] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Brands:
                        result[3] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_PartPerf:
                        result[4] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Showcases:
                        result[5] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Messages:
                        result[6] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Sponsors:
                        result[7] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Stages:
                        result[8] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_PerfTun:
                        result[9] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_Challenges:
                        result[10] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_PartUnlock:
                        result[11] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_BankTrigs:
                        result[12] = br.BaseStream.Position;
                        goto default;

                    case BinBlockID.GCareer_CarUnlocks:
                        result[13] = br.BaseStream.Position;
                        goto default;

                    default:
                        var skip = br.ReadInt32();
                        br.BaseStream.Position += skip;
                        break;

                }

            }

            br.BaseStream.Position = offset;
            return result;
        }

        private void ReadStrings(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();
            var offset = br.BaseStream.Position;
            bool is_named = false;

            for (int i = 0; i < 4 && br.BaseStream.Position < offset + size; ++i)
			{

                string str = String.Empty;

                switch (i)
				{

                    case 0:
                        str = br.ReadNullTermUTF8();
                        str.BinHash();
                        break;

                    case 1:
                        var num = br.ReadEnum<BinBlockID>();
                        if (num == BinBlockID.Nikki) is_named = true;
                        break;

                    case 2:
                        if (is_named) this._collection_name = br.ReadNullTermUTF8();
                        break;

                    case 3:
                        if (is_named) br.ReadNullTermUTF8();
                        break;

                    default:
                        break;

                }

			}

            if (!is_named)
            {

                br.BaseStream.Position = offset;

                if (!(this.Manager is null)) this._collection_name = this.Manager.Count switch
                {
                    0 => "Main",
                    1 => "Demo",
                    _ => $"Career{this.Manager.Count}",
                };

            }

            while (br.BaseStream.Position < offset + size)
			{

                var str = br.ReadNullTermUTF8();
                str.BinHash();

			}
		}

        private void ReadGCareerRaces(BinaryReader br, BinaryReader strr)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x88;
            this.GCareerRaces.Capacity = count;

            for (int i = 0; i < count; ++i)
			{

                var collection = new GCareerRace(br, strr, this);
                this.GCareerRaces.Add(collection);

			}
		}

        private void ReadWorldShops(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0xA0;
            this.WorldShops.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new WorldShop(br, this);
                this.WorldShops.Add(collection);

            }
        }

        private void ReadGCareerBrands(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x44;
            this.GCareerBrands.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new GCareerBrand(br, this);
                this.GCareerBrands.Add(collection);

            }
        }

        private void ReadPartPerformances(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x17C;

            for (int loop = 0; loop < count; ++loop)
            {
                int index = br.ReadInt32();
                int level = br.ReadInt32();
                int total = br.ReadInt32();

                for (int i = 0; i < total; ++i)
                {

                    var collection = new PartPerformance(br, this)
                    {
                        PartPerformanceType = (PartPerformance.PerformanceType)index,
                        UpgradeLevel = level,
                        UpgradePartIndex = i,
                    };

                    this.PartPerformances.Add(collection);
                
                }

                for (int i = total; i < 4; ++i)
                {

                    br.BaseStream.Position += 0x5C;

                }
            }
        }

        private void ReadGShowcases(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size >> 6;
            this.GShowcases.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new GShowcase(br, this);
                this.GShowcases.Add(collection);

            }
        }

        private void ReadSMSMessages(BinaryReader br, BinaryReader strr)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x14;
            this.SMSMessages.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new SMSMessage(br, strr, this);
                this.SMSMessages.Add(collection);

            }
        }

        private void ReadSponsors(BinaryReader br, BinaryReader strr)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size >> 4;
            this.Sponsors.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new Sponsor(br, strr, this);
                this.Sponsors.Add(collection);

            }
        }

        private void ReadGCareerStages(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x50;
            this.GCareerStages.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new GCareerStage(br, this);
                this.GCareerStages.Add(collection);

            }
        }

        private void ReadPerfSliderTunings(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x18;
            this.PerfSliderTunings.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new PerfSliderTuning(br, this);
                this.PerfSliderTunings.Add(collection);

            }
        }

        private void ReadWorldChallenges(BinaryReader br, BinaryReader strr)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x18;
            this.WorldChallenges.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new WorldChallenge(br, strr, this);
                this.WorldChallenges.Add(collection);

            }
        }

        private void ReadPartUnlockables(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0x28;
            this.PartUnlockables.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new PartUnlockable(br, this);
                this.PartUnlockables.Add(collection);

            }
        }

        private void ReadBankTriggers(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0xC;
            this.BankTriggers.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new BankTrigger(br, this);
                this.BankTriggers.Add(collection);

            }
        }

        private void ReadGCarUnlocks(BinaryReader br)
		{
            if (br.BaseStream.Position == max) return;
            var size = br.ReadInt32();

            var count = size / 0xC;
            this.GCarUnlocks.Capacity = count;

            for (int i = 0; i < count; ++i)
            {

                var collection = new GCarUnlock(br, this);
                this.GCarUnlocks.Add(collection);

            }
        }

		#endregion

		#region Writing Methods

        private byte[] WriteGCareerRaces(BinaryWriter strw)
		{
            var size = this.GCareerRaceCount * 0x88;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Races);
            bw.Write(size);

            foreach (var collection in this.GCareerRaces)
			{

                collection.Assemble(bw, strw);

			}

            return result;
		}

        private byte[] WriteWorldShops(BinaryWriter strw)
        {
            var size = this.WorldShopCount * 0xA0;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Shops);
            bw.Write(size);

            foreach (var collection in this.WorldShops)
            {

                collection.Assemble(bw, strw);

            }

            return result;
        }

        private byte[] WriteGCareerBrands(BinaryWriter strw)
        {
            var size = this.GCareerBrandCount * 0x44;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Brands);
            bw.Write(size);

            foreach (var collection in this.GCareerBrands)
            {

                collection.Assemble(bw, strw);

            }

            return result;
        }

        private byte[] WritePartPerformances()
		{
            var result = new byte[0x2C90];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_PartPerf);
            bw.Write(0x2C88);

            for (int loop = 0; loop < 10; ++loop)
			{

                for (int i = 1; i < 4; ++i)
                {

                    int count = 0;
                    bw.Write(loop);
                    bw.Write(i);
                    var start = bw.BaseStream.Position;
                    bw.Write(-1);
                    PartPerformance part = null;

                    part = this.PartPerformances.Find(_ =>
                        (int)_.PartPerformanceType == loop &&
                        _.UpgradeLevel == i &&
                        _.UpgradePartIndex == 0);

                    if (part is null) bw.BaseStream.Position += 0x5C;
                    else { part.Assemble(bw); ++count; }

                    part = this.PartPerformances.Find(_ =>
                        (int)_.PartPerformanceType == loop &&
                        _.UpgradeLevel == i &&
                        _.UpgradePartIndex == 1);

                    if (part is null) bw.BaseStream.Position += 0x5C;
                    else { part.Assemble(bw); ++count; }

                    part = this.PartPerformances.Find(_ =>
                        (int)_.PartPerformanceType == loop &&
                        _.UpgradeLevel == i &&
                        _.UpgradePartIndex == 2);

                    if (part is null) bw.BaseStream.Position += 0x5C;
                    else { part.Assemble(bw); ++count; }

                    part = this.PartPerformances.Find(_ =>
                        (int)_.PartPerformanceType == loop &&
                        _.UpgradeLevel == i &&
                        _.UpgradePartIndex == 3);

                    if (part is null) bw.BaseStream.Position += 0x5C;
                    else { part.Assemble(bw); ++count; }

                    var final = bw.BaseStream.Position;
                    bw.BaseStream.Position = start;
                    bw.Write(count);
                    bw.BaseStream.Position = final;

                }

            }

            return result;
		}

        private byte[] WriteGShowcases(BinaryWriter strw)
        {
            var size = this.GShowcaseCount << 6;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Showcases);
            bw.Write(size);

            foreach (var collection in this.GShowcases)
            {

                collection.Assemble(bw, strw);

            }

            return result;
        }

        private byte[] WriteSMSMessages(BinaryWriter strw)
        {
            var size = this.SMSMessageCount * 0x14;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Messages);
            bw.Write(size);

            foreach (var collection in this.SMSMessages)
            {

                collection.Assemble(bw, strw);

            }

            return result;
        }

        private byte[] WriteSponsors(BinaryWriter strw)
        {
            var size = this.SponsorCount << 4;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Sponsors);
            bw.Write(size);

            foreach (var collection in this.Sponsors)
            {

                collection.Assemble(bw, strw);

            }

            return result;
        }

        private byte[] WriteGCareerStages()
        {
            var size = this.GCareerStageCount * 0x50;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Stages);
            bw.Write(size);

            foreach (var collection in this.GCareerStages)
            {

                collection.Assemble(bw);

            }

            return result;
        }

        private byte[] WritePerfSliderTunings()
        {
            var size = this.PerfSliderTuningCount * 0x18;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_PerfTun);
            bw.Write(size);

            foreach (var collection in this.PerfSliderTunings)
            {

                collection.Assemble(bw);

            }

            return result;
        }

        private byte[] WriteWorldChallenges(BinaryWriter strw)
        {
            var size = this.WorldChallengeCount * 0x18;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_Challenges);
            bw.Write(size);

            foreach (var collection in this.WorldChallenges)
            {

                collection.Assemble(bw, strw);

            }

            return result;
        }

        private byte[] WritePartUnlockables()
        {
            var size = this.PartUnlockableCount * 0x28;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_PartUnlock);
            bw.Write(size);

            foreach (var collection in this.PartUnlockables)
            {

                collection.Assemble(bw);

            }

            return result;
        }

        private byte[] WriteBankTriggers()
        {
            var size = this.BankTriggerCount * 0xC;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_BankTrigs);
            bw.Write(size);

            foreach (var collection in this.BankTriggers)
            {

                collection.Assemble(bw);

            }

            return result;
        }

        private byte[] WriteGCarUnlocks()
        {
            var size = this.GCarUnlockCount * 0xC;
            var result = new byte[size + 8];
            using var ms = new MemoryStream(result);
            using var bw = new BinaryWriter(ms);

            bw.WriteEnum(BinBlockID.GCareer_CarUnlocks);
            bw.Write(size);

            foreach (var collection in this.GCarUnlocks)
            {

                collection.Assemble(bw);

            }

            return result;
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
            using (var ms = new MemoryStream(0x18000))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);

                writer.Write(this.BankTriggerCount);
                writer.Write(this.GCareerBrandCount);
                writer.Write(this.GCareerRaceCount);
                writer.Write(this.GCareerStageCount);
                writer.Write(this.GCarUnlockCount);
                writer.Write(this.GShowcaseCount);
                writer.Write(this.PartPerformanceCount);
                writer.Write(this.PartUnlockableCount);
                writer.Write(this.PerfSliderTuningCount);
                writer.Write(this.SMSMessageCount);
                writer.Write(this.SponsorCount);
                writer.Write(this.WorldChallengeCount);
                writer.Write(this.WorldShopCount);

                foreach (var c in this.BankTriggers) c.Serialize(writer);
                foreach (var c in this.GCareerBrands) c.Serialize(writer);
                foreach (var c in this.GCareerRaces) c.Serialize(writer);
                foreach (var c in this.GCareerStages) c.Serialize(writer);
                foreach (var c in this.GCarUnlocks) c.Serialize(writer);
                foreach (var c in this.GShowcases) c.Serialize(writer);
                foreach (var c in this.PartPerformances) c.Serialize(writer);
                foreach (var c in this.PartUnlockables) c.Serialize(writer);
                foreach (var c in this.PerfSliderTunings) c.Serialize(writer);
                foreach (var c in this.SMSMessages) c.Serialize(writer);
                foreach (var c in this.Sponsors) c.Serialize(writer);
                foreach (var c in this.WorldChallenges) c.Serialize(writer);
                foreach (var c in this.WorldShops) c.Serialize(writer);

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

            this._collection_name = reader.ReadNullTermUTF8();

            this.BankTriggers.Capacity = reader.ReadInt32();
            this.GCareerBrands.Capacity = reader.ReadInt32();
            this.GCareerRaces.Capacity = reader.ReadInt32();
            this.GCareerStages.Capacity = reader.ReadInt32();
            this.GCarUnlocks.Capacity = reader.ReadInt32();
            this.GShowcases.Capacity = reader.ReadInt32();
            this.PartPerformances.Capacity = reader.ReadInt32();
            this.PartUnlockables.Capacity = reader.ReadInt32();
            this.PerfSliderTunings.Capacity = reader.ReadInt32();
            this.SMSMessages.Capacity = reader.ReadInt32();
            this.Sponsors.Capacity = reader.ReadInt32();
            this.WorldChallenges.Capacity = reader.ReadInt32();
            this.WorldShops.Capacity = reader.ReadInt32();

            for (int i = 0, max = this.BankTriggers.Capacity; i < max; ++i)
			{

                var collection = new BankTrigger() { Career = this };
                collection.Deserialize(reader);
                this.BankTriggers.Add(collection);

			}

            for (int i = 0, max = this.GCareerBrands.Capacity; i < max; ++i)
            {

                var collection = new GCareerBrand() { Career = this };
                collection.Deserialize(reader);
                this.GCareerBrands.Add(collection);

            }

            for (int i = 0, max = this.GCareerRaces.Capacity; i < max; ++i)
            {

                var collection = new GCareerRace() { Career = this };
                collection.Deserialize(reader);
                this.GCareerRaces.Add(collection);

            }

            for (int i = 0, max = this.GCareerStages.Capacity; i < max; ++i)
            {

                var collection = new GCareerStage() { Career = this };
                collection.Deserialize(reader);
                this.GCareerStages.Add(collection);

            }

            for (int i = 0, max = this.GCarUnlocks.Capacity; i < max; ++i)
            {

                var collection = new GCarUnlock() { Career = this };
                collection.Deserialize(reader);
                this.GCarUnlocks.Add(collection);

            }

            for (int i = 0, max = this.GShowcases.Capacity; i < max; ++i)
            {

                var collection = new GShowcase() { Career = this };
                collection.Deserialize(reader);
                this.GShowcases.Add(collection);

            }

            for (int i = 0, max = this.PartPerformances.Capacity; i < max; ++i)
            {

                var collection = new PartPerformance() { Career = this };
                collection.Deserialize(reader);
                this.PartPerformances.Add(collection);

            }

            for (int i = 0, max = this.PartUnlockables.Capacity; i < max; ++i)
            {

                var collection = new PartUnlockable() { Career = this };
                collection.Deserialize(reader);
                this.PartUnlockables.Add(collection);

            }

            for (int i = 0, max = this.PerfSliderTunings.Capacity; i < max; ++i)
            {

                var collection = new PerfSliderTuning() { Career = this };
                collection.Deserialize(reader);
                this.PerfSliderTunings.Add(collection);

            }

            for (int i = 0, max = this.SMSMessages.Capacity; i < max; ++i)
            {

                var collection = new SMSMessage() { Career = this };
                collection.Deserialize(reader);
                this.SMSMessages.Add(collection);

            }

            for (int i = 0, max = this.Sponsors.Capacity; i < max; ++i)
            {

                var collection = new Sponsor() { Career = this };
                collection.Deserialize(reader);
                this.Sponsors.Add(collection);

            }

            for (int i = 0, max = this.WorldChallenges.Capacity; i < max; ++i)
            {

                var collection = new WorldChallenge() { Career = this };
                collection.Deserialize(reader);
                this.WorldChallenges.Add(collection);

            }

            for (int i = 0, max = this.WorldShops.Capacity; i < max; ++i)
            {

                var collection = new WorldShop() { Career = this };
                collection.Deserialize(reader);
                this.WorldShops.Add(collection);

            }
        }

        /// <summary>
        /// Synchronizes all parts of this instance with another instance passed.
        /// </summary>
        /// <param name="other"><see cref="GCareer"/> to synchronize with.</param>
        internal void Synchronize(GCareer other)
        {
            this._bank_triggers = this.MergeCollectionLists(other.BankTriggers, this.BankTriggers);
            this._gcareer_brands = this.MergeCollectionLists(other.GCareerBrands, this.GCareerBrands);
            this._gcareer_races = this.MergeCollectionLists(other.GCareerRaces, this.GCareerRaces);
            this._gcareer_stages = this.MergeCollectionLists(other.GCareerStages, this.GCareerStages);
            this._gcar_unlocks = this.MergeCollectionLists(other.GCarUnlocks, this.GCarUnlocks);
            this._gshowcases = this.MergeCollectionLists(other.GShowcases, this.GShowcases);
            this._part_performances = this.MergeCollectionLists(other.PartPerformances, this.PartPerformances);
            this._part_unlockables = this.MergeCollectionLists(other.PartUnlockables, this.PartUnlockables);
            this._perfslider_tunings = this.MergeCollectionLists(other.PerfSliderTunings, this.PerfSliderTunings);
            this._sms_messages = this.MergeCollectionLists(other.SMSMessages, this.SMSMessages);
            this._sponsors = this.MergeCollectionLists(other.Sponsors, this.Sponsors);
            this._world_challenges = this.MergeCollectionLists(other.WorldChallenges, this.WorldChallenges);
            this._world_shops = this.MergeCollectionLists(other.WorldShops, this.WorldShops);
        }

        #endregion
    }
}
