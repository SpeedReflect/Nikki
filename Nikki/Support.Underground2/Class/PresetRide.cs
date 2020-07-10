using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
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
        [Expandable("BaseKit")]
        public PerfSpecs PERF_SPECS { get; set; }

        /// <summary>
        /// Doorline attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        public Doorlines KIT_DOORLINES { get; set; }

        /// <summary>
        /// Damage attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        public Damages KIT_DAMAGES { get; set; }

        /// <summary>
        /// Audio buffer attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        public AudioBuffers AUDIO_COMP { get; set; }

        /// <summary>
        /// Decal size attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        public DecalSize DECAL_SIZES { get; set; }

        /// <summary>
        /// Group of paints appliable to parts in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        public PaintTypes PAINT_TYPES { get; set; }

        /// <summary>
        /// Group of vinyls and their colors appliable to this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        public VinylSets VINYL_SETS { get; set; }

        /// <summary>
        /// Set of hood decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        public DecalArray DECALS_HOOD { get; set; }

        /// <summary>
        /// Set of front window decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        public DecalArray DECALS_FRONT_WINDOW { get; set; }

        /// <summary>
        /// Set of rear window decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        public DecalArray DECALS_REAR_WINDOW { get; set; }

        /// <summary>
        /// Set of left door decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        public DecalArray DECALS_LEFT_DOOR { get; set; }

        /// <summary>
        /// Set of right door decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        public DecalArray DECALS_RIGHT_DOOR { get; set; }

        /// <summary>
        /// Set of left quarter decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        public DecalArray DECALS_LEFT_QUARTER { get; set; }

        /// <summary>
        /// Set of right quarter decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        public DecalArray DECALS_RIGHT_QUARTER { get; set; }

        /// <summary>
        /// Set of specialties and visual attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        public Specialties SPECIALTIES { get; set; }

        /// <summary>
        /// Set of HUD elements in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        public HUDStyle HUD { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        public PresetRide() => this.Initialize();

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="PresetRideManager"/> to which this instance belongs to.</param>
        public PresetRide(string CName, PresetRideManager manager)
        {
            this.Manager = manager;
            this.CollectionName = CName;
            this.Initialize();
            this.MODEL = "SUPRA";
            this._unkdata = new byte[0x44];
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="PresetRideManager"/> to which this instance belongs to.</param>
        public PresetRide(BinaryReader br, PresetRideManager manager)
        {
            this.Manager = manager;
            this.Initialize();
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

            // Read Kit Doorlines
            this.KIT_DOORLINES.Write(bw);

            // Continue reading parts
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

            // Read Audio Comps
            this.AUDIO_COMP.Write(bw);

            // Read Kit Damages
            this.KIT_DAMAGES.Write(bw);

            // Read Decal Sizes
            this.DECAL_SIZES.Write(bw);

            // Continue reading parts
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

            // Read Decal Arrays
            this.DECALS_HOOD.Write(bw);
            this.DECALS_FRONT_WINDOW.Write(bw);
            this.DECALS_REAR_WINDOW.Write(bw);
            this.DECALS_LEFT_DOOR.Write(bw);
            this.DECALS_RIGHT_DOOR.Write(bw);
            this.DECALS_LEFT_QUARTER.Write(bw);
            this.DECALS_RIGHT_QUARTER.Write(bw);

            // Continue reading parts
            bw.Write(this.WindshieldTint.BinHash());

            // Read Specialties
            this.SPECIALTIES.Write(bw);

            // Read HUD
            this.HUD.Write(bw);

            // Finish reading parts
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
            this.Base = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.AutosculptFrontBumper = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.AutosculptRearBumper = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LeftSideMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightSideMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Body = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.AftermarketBodykit = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RoofScoop = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RoofTop = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Hood = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Trunk = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.AutosculptSkirt = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Spoiler = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Engine = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Headlight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Brakelight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Exhaust = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Kit Doorlines
            this.KIT_DOORLINES.Read(br);

            // Continue reading parts
            this.Fender = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Quarter = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.HoodUnder = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.TrunkUnder = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontBrake = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearBrake = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontWheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearWheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Spinner = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.WingMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LicensePlate = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.TrunkAudio = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Audio Comps
            this.AUDIO_COMP.Read(br);

            // Read Kit Damages
            this.KIT_DAMAGES.Read(br);

            // Read Decal Sizes
            this.DECAL_SIZES.Read(br);

            // Continue reading parts
            this.PAINT_TYPES.BasePaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.VinylLayer3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.EnginePaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.SpoilerPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.BrakesPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.ExhaustPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.AudioPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.RimsPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.SpinnersPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.RoofPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PAINT_TYPES.MirrorsPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl0_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl1_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl2_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.VINYL_SETS.Vinyl3_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.KitCarbon = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.HoodCarbon = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.DoorCarbon = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.TrunkCarbon = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Decal Arrays
            this.DECALS_HOOD.Read(br);
            this.DECALS_FRONT_WINDOW.Read(br);
            this.DECALS_REAR_WINDOW.Read(br);
            this.DECALS_LEFT_DOOR.Read(br);
            this.DECALS_RIGHT_DOOR.Read(br);
            this.DECALS_LEFT_QUARTER.Read(br);
            this.DECALS_RIGHT_QUARTER.Read(br);

            // Continue reading parts
            this.WindshieldTint = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Specialties
            this.SPECIALTIES.Read(br);

            // Read HUD
            this.HUD.Read(br);

            // Finish reading parts
            this.CV = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.WheelManufacturer = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Misc = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

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

        private void Initialize()
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

                // Read Kit Doorlines
                this.KIT_DOORLINES.Serialize(bw);

                // Continue reading parts
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

                // Read Audio Comps
                this.AUDIO_COMP.Serialize(bw);

                // Read Kit Damages
                this.KIT_DAMAGES.Serialize(bw);

                // Read Decal Sizes
                this.DECAL_SIZES.Serialize(bw);

                // Continue reading parts
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

                // Read Decal Arrays
                this.DECALS_HOOD.Serialize(bw);
                this.DECALS_FRONT_WINDOW.Serialize(bw);
                this.DECALS_REAR_WINDOW.Serialize(bw);
                this.DECALS_LEFT_DOOR.Serialize(bw);
                this.DECALS_RIGHT_DOOR.Serialize(bw);
                this.DECALS_LEFT_QUARTER.Serialize(bw);
                this.DECALS_RIGHT_QUARTER.Serialize(bw);

                // Continue reading parts
                bw.WriteNullTermUTF8(this.WindshieldTint);

                // Read Specialties
                this.SPECIALTIES.Write(bw);

                // Read HUD
                this.HUD.Write(bw);

                // Finish reading parts
                bw.WriteNullTermUTF8(this.CV);
                bw.WriteNullTermUTF8(this.WheelManufacturer);
                bw.WriteNullTermUTF8(this.Misc);

                // Write unknown data
                this.PERF_SPECS.Write(bw);

                array = ms.ToArray();

            }

            array = Interop.Compress(array, eLZCompressionType.BEST);

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

            // Frontend and Pvehicle
            this.Frontend = reader.ReadNullTermUTF8();
            this.Pvehicle = reader.ReadNullTermUTF8();

            // Start reading parts
            this.Base = reader.ReadNullTermUTF8();

            // Read Kit Damages
            this.KIT_DAMAGES.Deserialize(reader);

            // Continue reading parts
            this.AftermarketBodykit = reader.ReadNullTermUTF8();
            this.FrontBrake = reader.ReadNullTermUTF8();
            this.FrontRotor = reader.ReadNullTermUTF8();
            this.FrontLeftWindow = reader.ReadNullTermUTF8();
            this.FrontRightWindow = reader.ReadNullTermUTF8();
            this.FrontWindow = reader.ReadNullTermUTF8();
            this.Interior = reader.ReadNullTermUTF8();
            this.LeftBrakelight = reader.ReadNullTermUTF8();
            this.LeftBrakelightGlass = reader.ReadNullTermUTF8();
            this.LeftHeadlight = reader.ReadNullTermUTF8();
            this.LeftHeadlightGlass = reader.ReadNullTermUTF8();
            this.LeftSideMirror = reader.ReadNullTermUTF8();
            this.RearBrake = reader.ReadNullTermUTF8();
            this.RearRotor = reader.ReadNullTermUTF8();
            this.RearLeftWindow = reader.ReadNullTermUTF8();
            this.RearRightWindow = reader.ReadNullTermUTF8();
            this.RearWindow = reader.ReadNullTermUTF8();
            this.RightBrakelight = reader.ReadNullTermUTF8();
            this.RightBrakelightGlass = reader.ReadNullTermUTF8();
            this.RightHeadlight = reader.ReadNullTermUTF8();
            this.RightHeadlightGlass = reader.ReadNullTermUTF8();
            this.RightSideMirror = reader.ReadNullTermUTF8();
            this.Driver = reader.ReadNullTermUTF8();
            this.SteeringWheel = reader.ReadNullTermUTF8();
            this.Exhaust = reader.ReadNullTermUTF8();
            this.Spoiler = reader.ReadNullTermUTF8();
            this.UniversalSpoilerBase = reader.ReadNullTermUTF8();

            // Read Zero Damages
            this.ZERO_DAMAGES.Deserialize(reader);

            // Read Attachments
            this.ATTACHMENTS.Deserialize(reader);

            // Continue reading parts
            this.AutosculptFrontBumper = reader.ReadNullTermUTF8();
            this.FrontBumperBadgingSet = reader.ReadNullTermUTF8();
            this.AutosculptRearBumper = reader.ReadNullTermUTF8();
            this.RearBumperBadgingSet = reader.ReadNullTermUTF8();
            this.RoofTop = reader.ReadNullTermUTF8();
            this.RoofScoop = reader.ReadNullTermUTF8();
            this.Hood = reader.ReadNullTermUTF8();
            this.AutosculptSkirt = reader.ReadNullTermUTF8();
            this.Headlight = reader.ReadNullTermUTF8();
            this.Brakelight = reader.ReadNullTermUTF8();
            this.DoorLeft = reader.ReadNullTermUTF8();
            this.DoorRight = reader.ReadNullTermUTF8();
            this.FrontWheel = reader.ReadNullTermUTF8();
            this.RearWheel = reader.ReadNullTermUTF8();
            this.LicensePlate = reader.ReadNullTermUTF8();
            this.Doorline = reader.ReadNullTermUTF8();
            this.DecalFrontWindow = reader.ReadNullTermUTF8();
            this.DecalRearWindow = reader.ReadNullTermUTF8();

            // Read Visual Sets
            this.VISUAL_SETS.Deserialize(reader);

            // Finish reading parts
            this.WindshieldTint = reader.ReadNullTermUTF8();
            this.CustomHUD = reader.ReadNullTermUTF8();
            this.CV = reader.ReadNullTermUTF8();
            this.Misc = reader.ReadNullTermUTF8();

            // Read Paint
            this.PAINT_VALUES.Deserialize(reader);

            // Read Autosculpt
            this.FRONTBUMPER.Read(reader);
            this.REARBUMPER.Read(reader);
            this.SKIRT.Read(reader);
            this.WHEELS.Read(reader);
            this.HOOD.Read(reader);
            this.SPOILER.Read(reader);
            this.ROOFSCOOP.Read(reader);
            this.ChopTopSizeValue = reader.ReadByte();
            this.ExhaustSizeValue = reader.ReadByte();

            // Read Vinyls
            this.VINYL01.Deserialize(reader);
            this.VINYL02.Deserialize(reader);
            this.VINYL03.Deserialize(reader);
            this.VINYL04.Deserialize(reader);
            this.VINYL05.Deserialize(reader);
            this.VINYL06.Deserialize(reader);
            this.VINYL07.Deserialize(reader);
            this.VINYL08.Deserialize(reader);
            this.VINYL09.Deserialize(reader);
            this.VINYL10.Deserialize(reader);
            this.VINYL11.Deserialize(reader);
            this.VINYL12.Deserialize(reader);
            this.VINYL13.Deserialize(reader);
            this.VINYL14.Deserialize(reader);
            this.VINYL15.Deserialize(reader);
            this.VINYL16.Deserialize(reader);
            this.VINYL17.Deserialize(reader);
            this.VINYL18.Deserialize(reader);
            this.VINYL19.Deserialize(reader);
            this.VINYL20.Deserialize(reader);
        }

        #endregion
    }
}