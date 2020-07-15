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
                this._collection_name = value;
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
        /// Unknown key 1.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public int UnknownKey1 { get; set; }

        /// <summary>
        /// Unknown key 2.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public int UnknownKey2 { get; set; }

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
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~PresetRide() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="PresetRide"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PresetRide"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write unknown value
            bw.Write(this.UnknownKey1);
            bw.Write(this.UnknownKey2);

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
            // Read unknown values
            this.UnknownKey1 = br.ReadInt32();
            this.UnknownKey2 = br.ReadInt32();

            // MODEL
            this.MODEL = br.ReadNullTermUTF8(0x20);

            // CollectionName
            this._collection_name = br.ReadNullTermUTF8(0x20);

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
                bw.WriteNullTermUTF8(this._collection_name);
                bw.WriteNullTermUTF8(this.MODEL);

                // Write unknown value
                bw.Write(this.UnknownKey1);
                bw.Write(this.UnknownKey2);

                // Performance Level
                bw.Write(this.PerformanceLevel);

                // Start writing parts
                bw.WriteNullTermUTF8(this.Base);
                bw.WriteNullTermUTF8(this.AutosculptFrontBumper);
                bw.WriteNullTermUTF8(this.AutosculptRearBumper);
                bw.WriteNullTermUTF8(this.LeftSideMirror);
                bw.WriteNullTermUTF8(this.RightSideMirror);
                bw.WriteNullTermUTF8(this.Body);
                bw.WriteNullTermUTF8(this.AftermarketBodykit);
                bw.WriteNullTermUTF8(this.RoofScoop);
                bw.WriteNullTermUTF8(this.RoofTop);
                bw.WriteNullTermUTF8(this.Hood);
                bw.WriteNullTermUTF8(this.Trunk);
                bw.WriteNullTermUTF8(this.AutosculptSkirt);
                bw.WriteNullTermUTF8(this.Spoiler);
                bw.WriteNullTermUTF8(this.Engine);
                bw.WriteNullTermUTF8(this.Headlight);
                bw.WriteNullTermUTF8(this.Brakelight);
                bw.WriteNullTermUTF8(this.Exhaust);

                // Write Kit Doorlines
                this.KIT_DOORLINES.Serialize(bw);

                // Continue writing parts
                bw.WriteNullTermUTF8(this.Fender);
                bw.WriteNullTermUTF8(this.Quarter);
                bw.WriteNullTermUTF8(this.HoodUnder);
                bw.WriteNullTermUTF8(this.TrunkUnder);
                bw.WriteNullTermUTF8(this.FrontBrake);
                bw.WriteNullTermUTF8(this.RearBrake);
                bw.WriteNullTermUTF8(this.FrontWheel);
                bw.WriteNullTermUTF8(this.RearWheel);
                bw.WriteNullTermUTF8(this.Spinner);
                bw.WriteNullTermUTF8(this.WingMirror);
                bw.WriteNullTermUTF8(this.LicensePlate);
                bw.WriteNullTermUTF8(this.TrunkAudio);

                // Write Audio Comps
                this.AUDIO_COMP.Serialize(bw);

                // Write Kit Damages
                this.KIT_DAMAGES.Serialize(bw);

                // Write Decal Sizes
                this.DECAL_SIZES.Serialize(bw);

                // Continue writing parts
                bw.WriteNullTermUTF8(this.PAINT_TYPES.BasePaintType);
                bw.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer0);
                bw.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer1);
                bw.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer2);
                bw.WriteNullTermUTF8(this.VINYL_SETS.VinylLayer3);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.EnginePaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.SpoilerPaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.BrakesPaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.ExhaustPaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.AudioPaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.RimsPaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.SpinnersPaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.RoofPaintType);
                bw.WriteNullTermUTF8(this.PAINT_TYPES.MirrorsPaintType);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color0);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color1);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color2);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl0_Color3);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color0);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color1);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color2);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl1_Color3);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color0);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color1);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color2);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl2_Color3);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color0);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color1);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color2);
                bw.WriteNullTermUTF8(this.VINYL_SETS.Vinyl3_Color3);
                bw.WriteNullTermUTF8(this.KitCarbon);
                bw.WriteNullTermUTF8(this.HoodCarbon);
                bw.WriteNullTermUTF8(this.DoorCarbon);
                bw.WriteNullTermUTF8(this.TrunkCarbon);

                // Write Decal Arrays
                this.DECALS_HOOD.Serialize(bw);
                this.DECALS_FRONT_WINDOW.Serialize(bw);
                this.DECALS_REAR_WINDOW.Serialize(bw);
                this.DECALS_LEFT_DOOR.Serialize(bw);
                this.DECALS_RIGHT_DOOR.Serialize(bw);
                this.DECALS_LEFT_QUARTER.Serialize(bw);
                this.DECALS_RIGHT_QUARTER.Serialize(bw);

                // Continue writing parts
                bw.WriteNullTermUTF8(this.WindshieldTint);

                // Write Specialties
                this.SPECIALTIES.Serialize(bw);

                // Write HUD
                this.HUD.Serialize(bw);

                // Finish writing parts
                bw.WriteNullTermUTF8(this.CV);
                bw.WriteNullTermUTF8(this.WheelManufacturer);
                bw.WriteNullTermUTF8(this.Misc);

                // Write unknown data
                this.PERF_SPECS.Write(bw);

                array = ms.ToArray();

            }

            array = Interop.Compress(array, LZCompressionType.BEST);

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
            this._collection_name = br.ReadNullTermUTF8();
            this.MODEL = br.ReadNullTermUTF8();

            // Write unknown value
            this.UnknownKey1 = br.ReadInt32();
            this.UnknownKey2 = br.ReadInt32();

            // Performance Level
            this.PerformanceLevel = br.ReadInt32();

            // Start writing parts
            this.Base = br.ReadNullTermUTF8();
            this.AutosculptFrontBumper = br.ReadNullTermUTF8();
            this.AutosculptRearBumper = br.ReadNullTermUTF8();
            this.LeftSideMirror = br.ReadNullTermUTF8();
            this.RightSideMirror = br.ReadNullTermUTF8();
            this.Body = br.ReadNullTermUTF8();
            this.AftermarketBodykit = br.ReadNullTermUTF8();
            this.RoofScoop = br.ReadNullTermUTF8();
            this.RoofTop = br.ReadNullTermUTF8();
            this.Hood = br.ReadNullTermUTF8();
            this.Trunk = br.ReadNullTermUTF8();
            this.AutosculptSkirt = br.ReadNullTermUTF8();
            this.Spoiler = br.ReadNullTermUTF8();
            this.Engine = br.ReadNullTermUTF8();
            this.Headlight = br.ReadNullTermUTF8();
            this.Brakelight = br.ReadNullTermUTF8();
            this.Exhaust = br.ReadNullTermUTF8();

            // Read Kit Doorlines
            this.KIT_DOORLINES.Deserialize(br);

            // Continue reading parts
            this.Fender = br.ReadNullTermUTF8();
            this.Quarter = br.ReadNullTermUTF8();
            this.HoodUnder = br.ReadNullTermUTF8();
            this.TrunkUnder = br.ReadNullTermUTF8();
            this.FrontBrake = br.ReadNullTermUTF8();
            this.RearBrake = br.ReadNullTermUTF8();
            this.FrontWheel = br.ReadNullTermUTF8();
            this.RearWheel = br.ReadNullTermUTF8();
            this.Spinner = br.ReadNullTermUTF8();
            this.WingMirror = br.ReadNullTermUTF8();
            this.LicensePlate = br.ReadNullTermUTF8();
            this.TrunkAudio = br.ReadNullTermUTF8();

            // Read Audio Comps
            this.AUDIO_COMP.Deserialize(br);

            // Read Kit Damages
            this.KIT_DAMAGES.Deserialize(br);

            // Read Decal Sizes
            this.DECAL_SIZES.Deserialize(br);

            // Continue reading parts
            this.PAINT_TYPES.BasePaintType = br.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer0 = br.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer1 = br.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer2 = br.ReadNullTermUTF8();
            this.VINYL_SETS.VinylLayer3 = br.ReadNullTermUTF8();
            this.PAINT_TYPES.EnginePaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.SpoilerPaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.BrakesPaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.ExhaustPaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.AudioPaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.RimsPaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.SpinnersPaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.RoofPaintType = br.ReadNullTermUTF8();
            this.PAINT_TYPES.MirrorsPaintType = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color0 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color1 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color2 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl0_Color3 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color0 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color1 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color2 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl1_Color3 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color0 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color1 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color2 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl2_Color3 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color0 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color1 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color2 = br.ReadNullTermUTF8();
            this.VINYL_SETS.Vinyl3_Color3 = br.ReadNullTermUTF8();
            this.KitCarbon = br.ReadNullTermUTF8();
            this.HoodCarbon = br.ReadNullTermUTF8();
            this.DoorCarbon = br.ReadNullTermUTF8();
            this.TrunkCarbon = br.ReadNullTermUTF8();

            // Read Decal Arrays
            this.DECALS_HOOD.Deserialize(br);
            this.DECALS_FRONT_WINDOW.Deserialize(br);
            this.DECALS_REAR_WINDOW.Deserialize(br);
            this.DECALS_LEFT_DOOR.Deserialize(br);
            this.DECALS_RIGHT_DOOR.Deserialize(br);
            this.DECALS_LEFT_QUARTER.Deserialize(br);
            this.DECALS_RIGHT_QUARTER.Deserialize(br);

            // Continue reading parts
            this.WindshieldTint = br.ReadNullTermUTF8();

            // Read Specialties
            this.SPECIALTIES.Deserialize(br);

            // Read HUD
            this.HUD.Deserialize(br);

            // Finish reading parts
            this.CV = br.ReadNullTermUTF8();
            this.WheelManufacturer = br.ReadNullTermUTF8();
            this.Misc = br.ReadNullTermUTF8();

            // Write unknown data
            this.PERF_SPECS.Read(br);
        }

        #endregion
    }
}