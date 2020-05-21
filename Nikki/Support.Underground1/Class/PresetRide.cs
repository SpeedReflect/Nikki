using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground1.Parts.PresetParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Class
{
    /// <summary>
    /// <see cref="PresetRide"/> is a collection of specific settings of a ride.
    /// </summary>
    public class PresetRide : Shared.Class.PresetRide
	{
        #region Fields

        private string _collection_name;
        private uint _unknown1 = 0;
        private uint _unknown2 = 0;
        private byte[] _unkdata;

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
        public override GameINT GameINT => GameINT.Underground1;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Underground1.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Underground1 Database { get; set; }

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
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                if (value.Length > MaxCNameLength)
                    throw new ArgumentLengthException(MaxCNameLength);
                if (this.Database.PresetRides.FindCollection(value) != null)
                    throw new CollectionExistenceException();
                this._collection_name = value;
            }
        }

        /// <summary>
        /// Binary memory hash of the collection name.
        /// </summary>
        public override uint BinKey => this._collection_name.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public override uint VltKey => this._collection_name.VltHash();

        /// <summary>
        /// Model that this <see cref="PresetRide"/> uses.
        /// </summary>
        [AccessModifiable()]
        public override string MODEL { get; set; } = String.Empty;

        /// <summary>
        /// Performance level of the ride.
        /// </summary>
        [StaticModifiable()]
        [AccessModifiable()]
        public int PerformanceLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Base { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string AutosculptFrontBumper { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string AutosculptRearBumper { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string AutosculptSkirt { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string LeftSideMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string RightSideMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Body { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string AftermarketBodykit { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string RoofScoop { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string RoofTop { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Hood { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Trunk { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Spoiler { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Engine { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Headlight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Brakelight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Exhaust { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Fender { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Quarter { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string HoodUnder { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string TrunkUnder { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string FrontBrake { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string RearBrake { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string FrontWheel { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string RearWheel { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Spinner { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string WingMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string LicensePlate { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string TrunkAudio { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string KitCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string HoodCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string DoorCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string TrunkCarbon { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string WindshieldTint { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string CV { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string WheelManufacturer { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string Misc { get; set; } = String.Empty;

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
        public PresetRide() { }

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
        public PresetRide(string CName, Database.Underground1 db)
        {
            this.Database = db;
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
        /// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
        public PresetRide(BinaryReader br, Database.Underground1 db)
        {
            this.Database = db;
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
            bw.Write(this._unknown1);
            bw.Write(this._unknown2);

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
            bw.Write(this._unkdata);
        }

        /// <summary>
        /// Disassembles array into <see cref="PresetRide"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="PresetRide"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            // Read unknown values
            this._unknown1 = br.ReadUInt32();
            this._unknown2 = br.ReadUInt32();

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
            this._unkdata = br.ReadBytes(0x44);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            var result = new PresetRide(CName, this.Database)
            {
                AftermarketBodykit = this.AftermarketBodykit,
                AUDIO_COMP = this.AUDIO_COMP.PlainCopy(),
                AutosculptFrontBumper = this.AutosculptFrontBumper,
                AutosculptRearBumper = this.AutosculptRearBumper,
                AutosculptSkirt = this.AutosculptSkirt,
                Base = this.Base,
                Body = this.Body,
                Brakelight = this.Brakelight,
                CV = this.CV,
                DECALS_FRONT_WINDOW = this.DECALS_FRONT_WINDOW.PlainCopy(),
                DECALS_HOOD = this.DECALS_HOOD.PlainCopy(),
                DECALS_LEFT_DOOR = this.DECALS_LEFT_DOOR.PlainCopy(),
                DECALS_LEFT_QUARTER = this.DECALS_LEFT_QUARTER.PlainCopy(),
                DECALS_REAR_WINDOW = this.DECALS_REAR_WINDOW.PlainCopy(),
                DECALS_RIGHT_DOOR = this.DECALS_RIGHT_DOOR.PlainCopy(),
                DECALS_RIGHT_QUARTER = this.DECALS_RIGHT_QUARTER.PlainCopy(),
                DECAL_SIZES = this.DECAL_SIZES.PlainCopy(),
                DoorCarbon = this.DoorCarbon,
                Engine = this.Engine,
                Exhaust = this.Exhaust,
                Fender = this.Fender,
                FrontBrake = this.FrontBrake,
                FrontWheel = this.FrontWheel,
                Headlight = this.Headlight,
                Hood = this.Hood,
                HoodCarbon = this.HoodCarbon,
                HoodUnder = this.HoodUnder,
                HUD = this.HUD.PlainCopy(),
                KitCarbon = this.KitCarbon,
                KIT_DAMAGES = this.KIT_DAMAGES.PlainCopy(),
                KIT_DOORLINES = this.KIT_DOORLINES.PlainCopy(),
                LeftSideMirror = this.LeftSideMirror,
                LicensePlate = this.LicensePlate,
                Misc = this.Misc,
                MODEL = this.MODEL,
                PAINT_TYPES = this.PAINT_TYPES.PlainCopy(),
                PerformanceLevel = this.PerformanceLevel,
                Quarter = this.Quarter,
                RearBrake = this.RearBrake,
                RearWheel = this.RearWheel,
                RightSideMirror = this.RightSideMirror,
                RoofScoop = this.RoofScoop,
                RoofTop = this.RoofTop,
                SPECIALTIES = this.SPECIALTIES.PlainCopy(),
                Spinner = this.Spinner,
                Spoiler = this.Spoiler,
                Trunk = this.Trunk,
                TrunkAudio = this.TrunkAudio,
                TrunkCarbon = this.TrunkCarbon,
                TrunkUnder = this.TrunkUnder,
                VINYL_SETS = this.VINYL_SETS.PlainCopy(),
                WheelManufacturer = this.WheelManufacturer,
                WindshieldTint = this.WindshieldTint,
                WingMirror = this.WingMirror,
            };

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
                   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
        }

        #endregion
    }
}