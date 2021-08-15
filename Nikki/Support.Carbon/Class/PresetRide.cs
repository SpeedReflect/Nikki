using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Carbon.Framework;
using Nikki.Support.Carbon.Parts.PresetParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Class
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
        public const int BaseClassSize = 0x600;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override GameINT GameINT => GameINT.Carbon;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.Carbon.ToString();

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
        /// Frontend value of this <see cref="PresetRide"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string Frontend { get; set; } = String.Empty;

        /// <summary>
        /// Pvehicle value of this <see cref="PresetRide"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string Pvehicle { get; set; } = String.Empty;

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
        public string AftermarketBodykit { get; set; } = String.Empty;

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
        public string FrontRotor { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string FrontLeftWindow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string FrontRightWindow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string FrontWindow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Interior { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string LeftBrakelight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string LeftBrakelightGlass { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string LeftHeadlight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string LeftHeadlightGlass { get; set; } = String.Empty;

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
        public string RearBrake { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RearRotor { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RearLeftWindow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RearRightWindow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RearWindow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RightBrakelight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RightBrakelightGlass { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RightHeadlight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string RightHeadlightGlass { get; set; } = String.Empty;

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
        public string Driver { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SteeringWheel { get; set; } = String.Empty;

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
        public string Spoiler { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string UniversalSpoilerBase { get; set; } = String.Empty;

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
        public string FrontBumperBadgingSet { get; set; } = String.Empty;

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
        public string RearBumperBadgingSet { get; set; } = String.Empty;

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
        public string RoofScoop { get; set; } = String.Empty;

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
        public string AutosculptSkirt { get; set; } = String.Empty;

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
        public string DoorLeft { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string DoorRight { get; set; } = String.Empty;

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
        public string LicensePlate { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string Doorline { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string DecalFrontWindow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string DecalRearWindow { get; set; } = String.Empty;

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
        public string CustomHUD { get; set; } = String.Empty;

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
        public string Misc { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte ChopTopSizeValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte ExhaustSizeValue { get; set; }

        /// <summary>
        /// Damage attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        [Browsable(false)]
        public Damages KIT_DAMAGES { get; }

        /// <summary>
        /// Zero damage attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        [Browsable(false)]
        public ZeroDamage ZERO_DAMAGES { get; }

        /// <summary>
        /// Attachment attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        [Browsable(false)]
        public Attachments ATTACHMENTS { get; }

        /// <summary>
        /// Visual attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        [Browsable(false)]
        public VisualSets VISUAL_SETS { get; }

        /// <summary>
        /// Paint attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        [Browsable(false)]
        public PaintValues PAINT_VALUES { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL01 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL02 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL03 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL04 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL05 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL06 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL07 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL08 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL09 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL10 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL11 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL12 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL13 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL14 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL15 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL16 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL17 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL18 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL19 { get; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL20 { get; }

        /// <summary>
        /// Autosculpt Front Bumper attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt FRONTBUMPER { get; }

        /// <summary>
        /// Autosculpt Rear Bumper attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt REARBUMPER { get; }

        /// <summary>
        /// Autosculpt Skirt attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt SKIRT { get; }

        /// <summary>
        /// Autosculpt Wheels attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt WHEELS { get; }

        /// <summary>
        /// Autosculpt Hood attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt HOOD { get; }

        /// <summary>
        /// Autosculpt Spoiler attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt SPOILER { get; }

        /// <summary>
        /// Autosculpt RoofScoop attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt ROOFSCOOP { get; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="PresetRide"/>.
        /// </summary>
        public PresetRide()
		{
            this.PAINT_VALUES = new PaintValues();
            this.ZERO_DAMAGES = new ZeroDamage();
            this.ATTACHMENTS = new Attachments();
            this.KIT_DAMAGES = new Damages();
            this.VISUAL_SETS = new VisualSets();
            this.FRONTBUMPER = new Autosculpt();
            this.REARBUMPER = new Autosculpt();
            this.ROOFSCOOP = new Autosculpt();
            this.SPOILER = new Autosculpt();
            this.WHEELS = new Autosculpt();
            this.SKIRT = new Autosculpt();
            this.HOOD = new Autosculpt();
            this.VINYL01 = new Vinyl();
            this.VINYL02 = new Vinyl();
            this.VINYL03 = new Vinyl();
            this.VINYL04 = new Vinyl();
            this.VINYL05 = new Vinyl();
            this.VINYL06 = new Vinyl();
            this.VINYL07 = new Vinyl();
            this.VINYL08 = new Vinyl();
            this.VINYL09 = new Vinyl();
            this.VINYL10 = new Vinyl();
            this.VINYL11 = new Vinyl();
            this.VINYL12 = new Vinyl();
            this.VINYL13 = new Vinyl();
            this.VINYL14 = new Vinyl();
            this.VINYL15 = new Vinyl();
            this.VINYL16 = new Vinyl();
            this.VINYL17 = new Vinyl();
            this.VINYL18 = new Vinyl();
            this.VINYL19 = new Vinyl();
            this.VINYL20 = new Vinyl();
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
            this.Frontend = "supra";
            this.Pvehicle = "supra";
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
            bw.Write((long)0);

            // MODEL
            bw.WriteNullTermUTF8(this.MODEL, 0x20);

            // CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x20);

            // Frontend and Pvehicle
            bw.Write(this.Frontend.VltHash());
            bw.Write((int)0);
            bw.Write(this.Pvehicle.VltHash());
            bw.WriteBytes(0, 0xC);

            // Start writing parts
            bw.Write(this.Base.BinHash());

            // Write Kit Damages
            this.KIT_DAMAGES.Write(bw);

            // Continue writing parts
            bw.Write(this.AftermarketBodykit.BinHash());
            bw.Write(this.FrontBrake.BinHash());
            bw.Write(this.FrontRotor.BinHash());
            bw.Write(this.FrontLeftWindow.BinHash());
            bw.Write(this.FrontRightWindow.BinHash());
            bw.Write(this.FrontWindow.BinHash());
            bw.Write(this.Interior.BinHash());
            bw.Write(this.LeftBrakelight.BinHash());
            bw.Write(this.LeftBrakelightGlass.BinHash());
            bw.Write(this.LeftHeadlight.BinHash());
            bw.Write(this.LeftHeadlightGlass.BinHash());
            bw.Write(this.LeftSideMirror.BinHash());
            bw.Write(this.RearBrake.BinHash());
            bw.Write(this.RearRotor.BinHash());
            bw.Write(this.RearLeftWindow.BinHash());
            bw.Write(this.RearRightWindow.BinHash());
            bw.Write(this.RearWindow.BinHash());
            bw.Write(this.RightBrakelight.BinHash());
            bw.Write(this.RightBrakelightGlass.BinHash());
            bw.Write(this.RightHeadlight.BinHash());
            bw.Write(this.RightHeadlightGlass.BinHash());
            bw.Write(this.RightSideMirror.BinHash());
            bw.Write(this.Driver.BinHash());
            bw.Write(this.SteeringWheel.BinHash());
            bw.Write(this.Exhaust.BinHash());
            bw.Write(this.Spoiler.BinHash());
            bw.Write(this.UniversalSpoilerBase.BinHash());

            // Write Zero Damages
            this.ZERO_DAMAGES.Write(bw);

            // Write Attachments
            this.ATTACHMENTS.Write(bw);

            // Continue writing parts
            bw.Write(this.AutosculptFrontBumper.BinHash());
            bw.Write(this.FrontBumperBadgingSet.BinHash());
            bw.Write(this.AutosculptRearBumper.BinHash());
            bw.Write(this.RearBumperBadgingSet.BinHash());
            bw.Write(this.RoofTop.BinHash());
            bw.Write(this.RoofScoop.BinHash());
            bw.Write(this.Hood.BinHash());
            bw.Write(this.AutosculptSkirt.BinHash());
            bw.Write(this.Headlight.BinHash());
            bw.Write(this.Brakelight.BinHash());
            bw.Write(this.DoorLeft.BinHash());
            bw.Write(this.DoorRight.BinHash());
            bw.Write(this.FrontWheel.BinHash());
            bw.Write(this.RearWheel.BinHash());
            bw.Write(this.LicensePlate.BinHash());
            bw.Write(this.Doorline.BinHash());
            bw.Write(this.DecalFrontWindow.BinHash());
            bw.Write(this.DecalRearWindow.BinHash());

            // Write Visual Sets
            this.VISUAL_SETS.Write(bw);

            // Finish writing parts
            bw.Write(this.WindshieldTint.BinHash());
            bw.Write(this.CustomHUD.BinHash());
            bw.Write(this.CV.BinHash());
            bw.Write(this.Misc.BinHash());

            // Write Paint
            this.PAINT_VALUES.Write(bw);

            // Write Autosculpt
            bw.WriteBytes(0, 0x10);
            this.FRONTBUMPER.Write(bw);
            bw.Write((short)0);
            this.REARBUMPER.Write(bw);
            bw.Write((short)0);
            this.SKIRT.Write(bw);
            bw.Write((short)0);
            this.WHEELS.Write(bw);
            bw.Write((short)0);
            this.HOOD.Write(bw);
            bw.Write((short)0);
            this.SPOILER.Write(bw);
            bw.Write((short)0);
            this.ROOFSCOOP.Write(bw);
            bw.Write((short)0);
            bw.Write(this.ChopTopSizeValue);
            bw.WriteBytes(0, 10);
            bw.Write(this.ExhaustSizeValue);
            bw.WriteBytes(0, 11);

            // Write Vinyls
            this.VINYL01.Write(bw);
            this.VINYL02.Write(bw);
            this.VINYL03.Write(bw);
            this.VINYL04.Write(bw);
            this.VINYL05.Write(bw);
            this.VINYL06.Write(bw);
            this.VINYL07.Write(bw);
            this.VINYL08.Write(bw);
            this.VINYL09.Write(bw);
            this.VINYL10.Write(bw);
            this.VINYL11.Write(bw);
            this.VINYL12.Write(bw);
            this.VINYL13.Write(bw);
            this.VINYL14.Write(bw);
            this.VINYL15.Write(bw);
            this.VINYL16.Write(bw);
            this.VINYL17.Write(bw);
            this.VINYL18.Write(bw);
            this.VINYL19.Write(bw);
            this.VINYL20.Write(bw);
        }

        /// <summary>
        /// Disassembles array into <see cref="PresetRide"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="PresetRide"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 8;

            // MODEL
            this.MODEL = br.ReadNullTermUTF8(0x20);

            // CollectionName
            this._collection_name = br.ReadNullTermUTF8(0x20).ToUpperInvariant();

            // Frontend and Pvehicle
            this.Frontend = br.ReadUInt32().VltString(LookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Pvehicle = br.ReadUInt32().VltString(LookupReturn.EMPTY);
            br.BaseStream.Position += 0xC;

            // Start reading parts
            this.Base = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Kit Damages
            this.KIT_DAMAGES.Read(br);

            // Continue reading parts
            this.AftermarketBodykit = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontBrake = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontRotor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontLeftWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontRightWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Interior = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LeftBrakelight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LeftBrakelightGlass = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LeftHeadlight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LeftHeadlightGlass = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LeftSideMirror = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearBrake = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearRotor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearLeftWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearRightWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RightBrakelight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RightBrakelightGlass = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RightHeadlight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RightHeadlightGlass = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RightSideMirror = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Driver = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.SteeringWheel = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Exhaust = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Spoiler = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.UniversalSpoilerBase = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Zero Damages
            this.ZERO_DAMAGES.Read(br);

            // Read Attachments
            this.ATTACHMENTS.Read(br);

            // Continue reading parts
            this.AutosculptFrontBumper = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontBumperBadgingSet = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.AutosculptRearBumper = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearBumperBadgingSet = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RoofTop = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RoofScoop = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Hood = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.AutosculptSkirt = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Headlight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Brakelight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.DoorLeft = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.DoorRight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.FrontWheel = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.RearWheel = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.LicensePlate = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Doorline = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.DecalFrontWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.DecalRearWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Visual Sets
            this.VISUAL_SETS.Read(br);

            // Finish reading parts
            this.WindshieldTint = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.CustomHUD = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.CV = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.Misc = br.ReadUInt32().BinString(LookupReturn.EMPTY);

            // Read Paint
            this.PAINT_VALUES.Read(br);

            // Read Autosculpt
            br.BaseStream.Position += 0x10;
            this.FRONTBUMPER.Read(br);
            br.BaseStream.Position += 2;
            this.REARBUMPER.Read(br);
            br.BaseStream.Position += 2;
            this.SKIRT.Read(br);
            br.BaseStream.Position += 2;
            this.WHEELS.Read(br);
            br.BaseStream.Position += 2;
            this.HOOD.Read(br);
            br.BaseStream.Position += 2;
            this.SPOILER.Read(br);
            br.BaseStream.Position += 2;
            this.ROOFSCOOP.Read(br);
            br.BaseStream.Position += 2;
            this.ChopTopSizeValue = br.ReadByte();
            br.BaseStream.Position += 10;
            this.ExhaustSizeValue = br.ReadByte();
            br.BaseStream.Position += 11;

            // Read Vinyls
            this.VINYL01.Read(br);
            this.VINYL02.Read(br);
            this.VINYL03.Read(br);
            this.VINYL04.Read(br);
            this.VINYL05.Read(br);
            this.VINYL06.Read(br);
            this.VINYL07.Read(br);
            this.VINYL08.Read(br);
            this.VINYL09.Read(br);
            this.VINYL10.Read(br);
            this.VINYL11.Read(br);
            this.VINYL12.Read(br);
            this.VINYL13.Read(br);
            this.VINYL14.Read(br);
            this.VINYL15.Read(br);
            this.VINYL16.Read(br);
            this.VINYL17.Read(br);
            this.VINYL18.Read(br);
            this.VINYL19.Read(br);
            this.VINYL20.Read(br);
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

                // Frontend and Pvehicle
                writer.WriteNullTermUTF8(this.Frontend);
                writer.WriteNullTermUTF8(this.Pvehicle);

                // Start reading parts
                writer.WriteNullTermUTF8(this.Base);

                // Write Kit Damages
                this.KIT_DAMAGES.Serialize(writer);

                // Continue reading parts
                writer.WriteNullTermUTF8(this.AftermarketBodykit);
                writer.WriteNullTermUTF8(this.FrontBrake);
                writer.WriteNullTermUTF8(this.FrontRotor);
                writer.WriteNullTermUTF8(this.FrontLeftWindow);
                writer.WriteNullTermUTF8(this.FrontRightWindow);
                writer.WriteNullTermUTF8(this.FrontWindow);
                writer.WriteNullTermUTF8(this.Interior);
                writer.WriteNullTermUTF8(this.LeftBrakelight);
                writer.WriteNullTermUTF8(this.LeftBrakelightGlass);
                writer.WriteNullTermUTF8(this.LeftHeadlight);
                writer.WriteNullTermUTF8(this.LeftHeadlightGlass);
                writer.WriteNullTermUTF8(this.LeftSideMirror);
                writer.WriteNullTermUTF8(this.RearBrake);
                writer.WriteNullTermUTF8(this.RearRotor);
                writer.WriteNullTermUTF8(this.RearLeftWindow);
                writer.WriteNullTermUTF8(this.RearRightWindow);
                writer.WriteNullTermUTF8(this.RearWindow);
                writer.WriteNullTermUTF8(this.RightBrakelight);
                writer.WriteNullTermUTF8(this.RightBrakelightGlass);
                writer.WriteNullTermUTF8(this.RightHeadlight);
                writer.WriteNullTermUTF8(this.RightHeadlightGlass);
                writer.WriteNullTermUTF8(this.RightSideMirror);
                writer.WriteNullTermUTF8(this.Driver);
                writer.WriteNullTermUTF8(this.SteeringWheel);
                writer.WriteNullTermUTF8(this.Exhaust);
                writer.WriteNullTermUTF8(this.Spoiler);
                writer.WriteNullTermUTF8(this.UniversalSpoilerBase);

                // Read Zero Damages
                this.ZERO_DAMAGES.Serialize(writer);

                // Read Attachments
                this.ATTACHMENTS.Serialize(writer);

                // Continue reading parts
                writer.WriteNullTermUTF8(this.AutosculptFrontBumper);
                writer.WriteNullTermUTF8(this.FrontBumperBadgingSet);
                writer.WriteNullTermUTF8(this.AutosculptRearBumper);
                writer.WriteNullTermUTF8(this.RearBumperBadgingSet);
                writer.WriteNullTermUTF8(this.RoofTop);
                writer.WriteNullTermUTF8(this.RoofScoop);
                writer.WriteNullTermUTF8(this.Hood);
                writer.WriteNullTermUTF8(this.AutosculptSkirt);
                writer.WriteNullTermUTF8(this.Headlight);
                writer.WriteNullTermUTF8(this.Brakelight);
                writer.WriteNullTermUTF8(this.DoorLeft);
                writer.WriteNullTermUTF8(this.DoorRight);
                writer.WriteNullTermUTF8(this.FrontWheel);
                writer.WriteNullTermUTF8(this.RearWheel);
                writer.WriteNullTermUTF8(this.LicensePlate);
                writer.WriteNullTermUTF8(this.Doorline);
                writer.WriteNullTermUTF8(this.DecalFrontWindow);
                writer.WriteNullTermUTF8(this.DecalRearWindow);

                // Read Visual Sets
                this.VISUAL_SETS.Serialize(writer);

                // Finish reading parts
                writer.WriteNullTermUTF8(this.WindshieldTint);
                writer.WriteNullTermUTF8(this.CustomHUD);
                writer.WriteNullTermUTF8(this.CV);
                writer.WriteNullTermUTF8(this.Misc);

                // Read Paint
                this.PAINT_VALUES.Serialize(writer);

                // Read Autosculpt
                this.FRONTBUMPER.Write(writer);
                this.REARBUMPER.Write(writer);
                this.SKIRT.Write(writer);
                this.WHEELS.Write(writer);
                this.HOOD.Write(writer);
                this.SPOILER.Write(writer);
                this.ROOFSCOOP.Write(writer);
                writer.Write(this.ChopTopSizeValue);
                writer.Write(this.ExhaustSizeValue);

                // Read Vinyls
                this.VINYL01.Serialize(writer);
                this.VINYL02.Serialize(writer);
                this.VINYL03.Serialize(writer);
                this.VINYL04.Serialize(writer);
                this.VINYL05.Serialize(writer);
                this.VINYL06.Serialize(writer);
                this.VINYL07.Serialize(writer);
                this.VINYL08.Serialize(writer);
                this.VINYL09.Serialize(writer);
                this.VINYL10.Serialize(writer);
                this.VINYL11.Serialize(writer);
                this.VINYL12.Serialize(writer);
                this.VINYL13.Serialize(writer);
                this.VINYL14.Serialize(writer);
                this.VINYL15.Serialize(writer);
                this.VINYL16.Serialize(writer);
                this.VINYL17.Serialize(writer);
                this.VINYL18.Serialize(writer);
                this.VINYL19.Serialize(writer);
                this.VINYL20.Serialize(writer);

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