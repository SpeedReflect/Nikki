using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Framework;
using Nikki.Support.Underground2.Parts.PresetParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="PresetRide"/> is a collection of specific settings of a ride.
    /// </summary>
    public class PresetRide : Shared.Class.PresetRide
	{
        #region Fields

        private string _collection_name;
        private readonly static int[] _is_in_fe = new int[]
        {
            0x431440,
            0x8B3090,
            0x8B33D0,
            0x8B3710,
            0x8D85A0,
            0x8D88E0,
            0x8D8C20,
            0x93D348,
            0xA00510,
            0xA37800,
            0xBC0300,
            0xBC0640,
        };

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = 0x1F;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0x28;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0x338;

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
        public PresetRideManager Manager { get; set; }

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
                this._collection_name = value.ToUpperInvariant();
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
        /// Model that this <see cref="PresetRide"/> uses.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public override string MODEL { get; set; } = String.Empty;

        /// <summary>
        /// True if this preset is in special frontend sponsor cars category; false otherwise.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public eBoolean IsFESponsorCar { get; set; }

        /// <summary>
        /// Performance level of the ride.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public int PerformanceLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Base { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string AutosculptFrontBumper { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string AutosculptRearBumper { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string AutosculptSkirt { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string LeftSideMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RightSideMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Body { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string AftermarketBodykit { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RoofScoop { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RoofTop { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Hood { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Trunk { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Spoiler { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Engine { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Headlight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Brakelight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Exhaust { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Fender { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Quarter { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string HoodUnder { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string TrunkUnder { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string FrontBrake { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RearBrake { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string FrontWheel { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RearWheel { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Spinner { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string WingMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string LicensePlate { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string TrunkAudio { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string KitCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string HoodCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string DoorCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string TrunkCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string WindshieldTint { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string CV { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string WheelManufacturer { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Misc { get; set; } = String.Empty;

        /// <summary>
        /// Perfomance specifications of this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("BaseKit")]
        public PerfSpecs PERF_SPECS { get; }

        /// <summary>
        /// Doorline attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("BaseKit")]
        public Doorlines KIT_DOORLINES { get; }

        /// <summary>
        /// Damage attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("BaseKit")]
        public Damages KIT_DAMAGES { get; }

        /// <summary>
        /// Audio buffer attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Visuals")]
        public AudioBuffers AUDIO_COMP { get; }

        /// <summary>
        /// Decal size attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("BaseKit")]
        public DecalSize DECAL_SIZES { get; }

        /// <summary>
        /// Group of paints appliable to parts in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Visuals")]
        public PaintTypes PAINT_TYPES { get; }

        /// <summary>
        /// Group of vinyls and their colors appliable to this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Visuals")]
        public VinylSets VINYL_SETS { get; }

        /// <summary>
        /// Set of hood decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Decals")]
        public DecalArray DECALS_HOOD { get; }

        /// <summary>
        /// Set of front window decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Decals")]
        public DecalArray DECALS_FRONT_WINDOW { get; }

        /// <summary>
        /// Set of rear window decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Decals")]
        public DecalArray DECALS_REAR_WINDOW { get; }

        /// <summary>
        /// Set of left door decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Decals")]
        public DecalArray DECALS_LEFT_DOOR { get; }

        /// <summary>
        /// Set of right door decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Decals")]
        public DecalArray DECALS_RIGHT_DOOR { get; }

        /// <summary>
        /// Set of left quarter decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Decals")]
        public DecalArray DECALS_LEFT_QUARTER { get; }

        /// <summary>
        /// Set of right quarter decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Decals")]
        public DecalArray DECALS_RIGHT_QUARTER { get; }

        /// <summary>
        /// Set of specialties and visual attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Visuals")]
        public Specialties SPECIALTIES { get; }

        /// <summary>
        /// Set of HUD elements in this <see cref="PresetRide"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Visuals")]
        public HUDStyle HUD { get; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        public PresetRide()
		{
            this.AUDIO_COMP = new AudioBuffers();
            this.DECALS_FRONT_WINDOW = new DecalArray();
            this.DECALS_HOOD = new DecalArray();
            this.DECALS_LEFT_DOOR = new DecalArray();
            this.DECALS_LEFT_QUARTER = new DecalArray();
            this.DECALS_REAR_WINDOW = new DecalArray();
            this.DECALS_RIGHT_DOOR = new DecalArray();
            this.DECALS_RIGHT_QUARTER = new DecalArray();
            this.DECAL_SIZES = new DecalSize();
            this.HUD = new HUDStyle();
            this.KIT_DAMAGES = new Damages();
            this.KIT_DOORLINES = new Doorlines();
            this.PAINT_TYPES = new PaintTypes();
            this.PERF_SPECS = new PerfSpecs();
            this.SPECIALTIES = new Specialties();
            this.VINYL_SETS = new VinylSets();
        }

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="PresetRideManager"/> to which this instance belongs to.</param>
        public PresetRide(string CName, PresetRideManager manager) : this()
        {
            this.Manager = manager;
            this.CollectionName = CName;
            this.MODEL = "SUPRA";
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="PresetRideManager"/> to which this instance belongs to.</param>
        public PresetRide(BinaryReader br, PresetRideManager manager) : this()
        {
            this.Manager = manager;
            this.Disassemble(br);
            this.CollectionName.BinHash();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="PresetRide"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PresetRide"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write IsFE value
            var isfe = this.IsFESponsorCar == eBoolean.True ? _is_in_fe[0] : 0;
            bw.Write(isfe);
            bw.Write(0);

            // MODEL
            bw.WriteNullTermUTF8(this.MODEL, 0x20);

            // CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x20);

            // Performance Level
            bw.Write(this.PerformanceLevel);

            // Start writing parts
            bw.Write(this.Base.BinHash());
            bw.Write(this.AutosculptFrontBumper.BinHash());
            bw.Write(this.AutosculptRearBumper.BinHash());
            bw.Write(this.LeftSideMirror.BinHash());
            bw.Write(this.RightSideMirror.BinHash());
            bw.Write(this.Body.BinHash());
            bw.Write(this.AftermarketBodykit.BinHash());
            bw.Write(this.RoofScoop.BinHash());
            bw.Write(this.RoofTop.BinHash());
            bw.Write(this.Hood.BinHash());
            bw.Write(this.Trunk.BinHash());
            bw.Write(this.AutosculptSkirt.BinHash());
            bw.Write(this.Spoiler.BinHash());
            bw.Write(this.Engine.BinHash());
            bw.Write(this.Headlight.BinHash());
            bw.Write(this.Brakelight.BinHash());
            bw.Write(this.Exhaust.BinHash());

            // Write Kit Doorlines
            this.KIT_DOORLINES.Write(bw);

            // Continue writing parts
            bw.Write(this.Fender.BinHash());
            bw.Write(this.Quarter.BinHash());
            bw.Write(this.HoodUnder.BinHash());
            bw.Write(this.TrunkUnder.BinHash());
            bw.Write(this.FrontBrake.BinHash());
            bw.Write(this.RearBrake.BinHash());
            bw.Write(this.FrontWheel.BinHash());
            bw.Write(this.RearWheel.BinHash());
            bw.Write(this.Spinner.BinHash());
            bw.Write(this.WingMirror.BinHash());
            bw.Write(this.LicensePlate.BinHash());
            bw.Write(this.TrunkAudio.BinHash());

            // Write Audio Comps
            this.AUDIO_COMP.Write(bw);

            // Write Kit Damages
            this.KIT_DAMAGES.Write(bw);

            // Write Decal Sizes
            this.DECAL_SIZES.Write(bw);

            // Continue writing parts
            bw.Write(this.PAINT_TYPES.BasePaintType.BinHash());
            bw.Write(this.VINYL_SETS.VinylLayer0.BinHash());
            bw.Write(this.VINYL_SETS.VinylLayer1.BinHash());
            bw.Write(this.VINYL_SETS.VinylLayer2.BinHash());
            bw.Write(this.VINYL_SETS.VinylLayer3.BinHash());
            bw.Write(this.PAINT_TYPES.EnginePaintType.BinHash());
            bw.Write(this.PAINT_TYPES.SpoilerPaintType.BinHash());
            bw.Write(this.PAINT_TYPES.BrakesPaintType.BinHash());
            bw.Write(this.PAINT_TYPES.ExhaustPaintType.BinHash());
            bw.Write(this.PAINT_TYPES.AudioPaintType.BinHash());
            bw.Write(this.PAINT_TYPES.RimsPaintType.BinHash());
            bw.Write(this.PAINT_TYPES.SpinnersPaintType.BinHash());
            bw.Write(this.PAINT_TYPES.RoofPaintType.BinHash());
            bw.Write(this.PAINT_TYPES.MirrorsPaintType.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl0_Color0.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl0_Color1.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl0_Color2.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl0_Color3.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl1_Color0.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl1_Color1.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl1_Color2.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl1_Color3.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl2_Color0.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl2_Color1.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl2_Color2.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl2_Color3.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl3_Color0.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl3_Color1.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl3_Color2.BinHash());
            bw.Write(this.VINYL_SETS.Vinyl3_Color3.BinHash());
            bw.Write(this.KitCarbon.BinHash());
            bw.Write(this.HoodCarbon.BinHash());
            bw.Write(this.DoorCarbon.BinHash());
            bw.Write(this.TrunkCarbon.BinHash());

            // Write Decal Arrays
            this.DECALS_HOOD.Write(bw);
            this.DECALS_FRONT_WINDOW.Write(bw);
            this.DECALS_REAR_WINDOW.Write(bw);
            this.DECALS_LEFT_DOOR.Write(bw);
            this.DECALS_RIGHT_DOOR.Write(bw);
            this.DECALS_LEFT_QUARTER.Write(bw);
            this.DECALS_RIGHT_QUARTER.Write(bw);

            // Continue writing parts
            bw.Write(this.WindshieldTint.BinHash());

            // Write Specialties
            this.SPECIALTIES.Write(bw);

            // Write HUD
            this.HUD.Write(bw);

            // Finish writing parts
            bw.Write(this.CV.BinHash());
            bw.Write(this.WheelManufacturer.BinHash());
            bw.Write(this.Misc.BinHash());

            // Write unknown data
            this.PERF_SPECS.Write(bw);
        }

        /// <summary>
        /// Disassembles array into <see cref="PresetRide"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="PresetRide"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            // Read IsFE value
            var isfe = br.ReadInt32();
            br.BaseStream.Position += 4;

            for (int i = 0; i < _is_in_fe.Length; ++i)
			{

                if (_is_in_fe[i] == isfe) { this.IsFESponsorCar = eBoolean.True; break; }

			}

            // MODEL
            this.MODEL = br.ReadNullTermUTF8(0x20);

            // CollectionName
            this._collection_name = br.ReadNullTermUTF8(0x20).ToUpperInvariant();

            // Performance Level
            this.PerformanceLevel = br.ReadInt32();

            // Start reading parts
            this.Base = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.AutosculptFrontBumper = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.AutosculptRearBumper = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LeftSideMirror = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RightSideMirror = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Body = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.AftermarketBodykit = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RoofScoop = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RoofTop = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Hood = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Trunk = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.AutosculptSkirt = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Spoiler = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Engine = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Headlight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Brakelight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Exhaust = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Kit Doorlines
            this.KIT_DOORLINES.Read(br);

            // Continue reading parts
            this.Fender = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Quarter = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.HoodUnder = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.TrunkUnder = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontBrake = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearBrake = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontWheel = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearWheel = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Spinner = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.WingMirror = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LicensePlate = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.TrunkAudio = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Audio Comps
            this.AUDIO_COMP.Read(br);

            // Read Kit Damages
            this.KIT_DAMAGES.Read(br);

            // Read Decal Sizes
            this.DECAL_SIZES.Read(br);

            // Continue reading parts
            this.PAINT_TYPES.BasePaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer0 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.EnginePaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.SpoilerPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.BrakesPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.ExhaustPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.AudioPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.RimsPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.SpinnersPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.RoofPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.PAINT_TYPES.MirrorsPaintType = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color0 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color0 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color0 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color0 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color3 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.KitCarbon = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.HoodCarbon = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.DoorCarbon = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.TrunkCarbon = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Decal Arrays
            this.DECALS_HOOD.Read(br);
            this.DECALS_FRONT_WINDOW.Read(br);
            this.DECALS_REAR_WINDOW.Read(br);
            this.DECALS_LEFT_DOOR.Read(br);
            this.DECALS_RIGHT_DOOR.Read(br);
            this.DECALS_LEFT_QUARTER.Read(br);
            this.DECALS_RIGHT_QUARTER.Read(br);

            // Continue reading parts
            this.WindshieldTint = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Specialties
            this.SPECIALTIES.Read(br);

            // Read HUD
            this.HUD.Read(br);

            // Finish reading parts
            this.CV = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.WheelManufacturer = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Misc = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read unknown array
            this.PERF_SPECS.Read(br);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new PresetRide(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="PresetRide"/> 
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
            using (var ms = new MemoryStream(0x2000))
            using (var writer = new BinaryWriter(ms))
            {

                // CollectionName and MODEL
                writer.WriteNullTermUTF8(this._collection_name);
                writer.WriteNullTermUTF8(this.MODEL);

                // Write IsFE value
                writer.WriteEnum(this.IsFESponsorCar);

                // Performance Level
                writer.Write(this.PerformanceLevel);

                // Start writing parts
                writer.WriteNullTermUTF8(this.Base);
                writer.WriteNullTermUTF8(this.AutosculptFrontBumper);
                writer.WriteNullTermUTF8(this.AutosculptRearBumper);
                writer.WriteNullTermUTF8(this.LeftSideMirror);
                writer.WriteNullTermUTF8(this.RightSideMirror);
                writer.WriteNullTermUTF8(this.Body);
                writer.WriteNullTermUTF8(this.AftermarketBodykit);
                writer.WriteNullTermUTF8(this.RoofScoop);
                writer.WriteNullTermUTF8(this.RoofTop);
                writer.WriteNullTermUTF8(this.Hood);
                writer.WriteNullTermUTF8(this.Trunk);
                writer.WriteNullTermUTF8(this.AutosculptSkirt);
                writer.WriteNullTermUTF8(this.Spoiler);
                writer.WriteNullTermUTF8(this.Engine);
                writer.WriteNullTermUTF8(this.Headlight);
                writer.WriteNullTermUTF8(this.Brakelight);
                writer.WriteNullTermUTF8(this.Exhaust);

                // Write Kit Doorlines
                this.KIT_DOORLINES.Serialize(writer);

                // Continue writing parts
                writer.WriteNullTermUTF8(this.Fender);
                writer.WriteNullTermUTF8(this.Quarter);
                writer.WriteNullTermUTF8(this.HoodUnder);
                writer.WriteNullTermUTF8(this.TrunkUnder);
                writer.WriteNullTermUTF8(this.FrontBrake);
                writer.WriteNullTermUTF8(this.RearBrake);
                writer.WriteNullTermUTF8(this.FrontWheel);
                writer.WriteNullTermUTF8(this.RearWheel);
                writer.WriteNullTermUTF8(this.Spinner);
                writer.WriteNullTermUTF8(this.WingMirror);
                writer.WriteNullTermUTF8(this.LicensePlate);
                writer.WriteNullTermUTF8(this.TrunkAudio);

                // Write Audio Comps
                this.AUDIO_COMP.Serialize(writer);

                // Write Kit Damages
                this.KIT_DAMAGES.Serialize(writer);

                // Write Decal Sizes
                this.DECAL_SIZES.Serialize(writer);

                // Continue writing parts
                writer.WriteNullTermUTF8(this.PAINT_TYPES.BasePaintType);
                writer.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer0);
                writer.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer1);
                writer.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer2);
                writer.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer3);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.EnginePaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.SpoilerPaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.BrakesPaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.ExhaustPaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.AudioPaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.RimsPaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.SpinnersPaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.RoofPaintType);
                writer.WriteNullTermUTF8(this.PAINT_TYPES.MirrorsPaintType);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color0);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color1);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color2);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color3);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color0);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color1);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color2);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color3);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color0);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color1);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color2);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color3);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color0);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color1);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color2);
                writer.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color3);
                writer.WriteNullTermUTF8(this.KitCarbon);
                writer.WriteNullTermUTF8(this.HoodCarbon);
                writer.WriteNullTermUTF8(this.DoorCarbon);
                writer.WriteNullTermUTF8(this.TrunkCarbon);

                // Write Decal Arrays
                this.DECALS_HOOD.Serialize(writer);
                this.DECALS_FRONT_WINDOW.Serialize(writer);
                this.DECALS_REAR_WINDOW.Serialize(writer);
                this.DECALS_LEFT_DOOR.Serialize(writer);
                this.DECALS_RIGHT_DOOR.Serialize(writer);
                this.DECALS_LEFT_QUARTER.Serialize(writer);
                this.DECALS_RIGHT_QUARTER.Serialize(writer);

                // Continue writing parts
                writer.WriteNullTermUTF8(this.WindshieldTint);

                // Write Specialties
                this.SPECIALTIES.Serialize(writer);

                // Write HUD
                this.HUD.Serialize(writer);

                // Finish writing parts
                writer.WriteNullTermUTF8(this.CV);
                writer.WriteNullTermUTF8(this.WheelManufacturer);
                writer.WriteNullTermUTF8(this.Misc);

                // Write perf spec data
                this.PERF_SPECS.Write(writer);

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

            // CollectionName and MODEL
            this._collection_name = reader.ReadNullTermUTF8();
            this.MODEL = reader.ReadNullTermUTF8();

            // Read unknown value
            this.IsFESponsorCar = reader.ReadEnum<eBoolean>();

            // Performance Level
            this.PerformanceLevel = reader.ReadInt32();

            // Start reading parts
            this.Base = reader.ReadNullTermUTF8();
            this.AutosculptFrontBumper = reader.ReadNullTermUTF8();
            this.AutosculptRearBumper = reader.ReadNullTermUTF8();
            this.LeftSideMirror = reader.ReadNullTermUTF8();
            this.RightSideMirror = reader.ReadNullTermUTF8();
            this.Body = reader.ReadNullTermUTF8();
            this.AftermarketBodykit = reader.ReadNullTermUTF8();
            this.RoofScoop = reader.ReadNullTermUTF8();
            this.RoofTop = reader.ReadNullTermUTF8();
            this.Hood = reader.ReadNullTermUTF8();
            this.Trunk = reader.ReadNullTermUTF8();
            this.AutosculptSkirt = reader.ReadNullTermUTF8();
            this.Spoiler = reader.ReadNullTermUTF8();
            this.Engine = reader.ReadNullTermUTF8();
            this.Headlight = reader.ReadNullTermUTF8();
            this.Brakelight = reader.ReadNullTermUTF8();
            this.Exhaust = reader.ReadNullTermUTF8();

            // Read Kit Doorlines
            this.KIT_DOORLINES.Deserialize(reader);

            // Continue reading parts
            this.Fender = reader.ReadNullTermUTF8();
            this.Quarter = reader.ReadNullTermUTF8();
            this.HoodUnder = reader.ReadNullTermUTF8();
            this.TrunkUnder = reader.ReadNullTermUTF8();
            this.FrontBrake = reader.ReadNullTermUTF8();
            this.RearBrake = reader.ReadNullTermUTF8();
            this.FrontWheel = reader.ReadNullTermUTF8();
            this.RearWheel = reader.ReadNullTermUTF8();
            this.Spinner = reader.ReadNullTermUTF8();
            this.WingMirror = reader.ReadNullTermUTF8();
            this.LicensePlate = reader.ReadNullTermUTF8();
            this.TrunkAudio = reader.ReadNullTermUTF8();

            // Read Audio Comps
            this.AUDIO_COMP.Deserialize(reader);

            // Read Kit Damages
            this.KIT_DAMAGES.Deserialize(reader);

            // Read Decal Sizes
            this.DECAL_SIZES.Deserialize(reader);

            // Continue reading parts
            this.PAINT_TYPES.BasePaintType = reader.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer0 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer1 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer2 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer3 = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.EnginePaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.SpoilerPaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.BrakesPaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.ExhaustPaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.AudioPaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.RimsPaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.SpinnersPaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.RoofPaintType = reader.ReadNullTermUTF8();
            this.PAINT_TYPES.MirrorsPaintType = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color0 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color1 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color2 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color3 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color0 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color1 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color2 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color3 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color0 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color1 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color2 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color3 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color0 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color1 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color2 = reader.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color3 = reader.ReadNullTermUTF8();
            this.KitCarbon = reader.ReadNullTermUTF8();
            this.HoodCarbon = reader.ReadNullTermUTF8();
            this.DoorCarbon = reader.ReadNullTermUTF8();
            this.TrunkCarbon = reader.ReadNullTermUTF8();

            // Read Decal Arrays
            this.DECALS_HOOD.Deserialize(reader);
            this.DECALS_FRONT_WINDOW.Deserialize(reader);
            this.DECALS_REAR_WINDOW.Deserialize(reader);
            this.DECALS_LEFT_DOOR.Deserialize(reader);
            this.DECALS_RIGHT_DOOR.Deserialize(reader);
            this.DECALS_LEFT_QUARTER.Deserialize(reader);
            this.DECALS_RIGHT_QUARTER.Deserialize(reader);

            // Continue reading parts
            this.WindshieldTint = reader.ReadNullTermUTF8();

            // Read Specialties
            this.SPECIALTIES.Deserialize(reader);

            // Read HUD
            this.HUD.Deserialize(reader);

            // Finish reading parts
            this.CV = reader.ReadNullTermUTF8();
            this.WheelManufacturer = reader.ReadNullTermUTF8();
            this.Misc = reader.ReadNullTermUTF8();

            // Read perf specs data
            this.PERF_SPECS.Read(reader);
        }

        #endregion
    }
}