using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
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
        public Damages KIT_DAMAGES { get; set; }

        /// <summary>
        /// Zero damage attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        [Browsable(false)]
        public ZeroDamage ZERO_DAMAGES { get; set; }

        /// <summary>
        /// Attachment attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        [Browsable(false)]
        public Attachments ATTACHMENTS { get; set; }

        /// <summary>
        /// Visual attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        [Browsable(false)]
        public VisualSets VISUAL_SETS { get; set; }

        /// <summary>
        /// Paint attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        [Browsable(false)]
        public PaintValues PAINT_VALUES { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL01 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL02 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL03 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL04 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL05 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL06 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL07 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL08 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL09 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL10 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL11 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL12 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL13 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL14 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL15 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL16 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL17 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL18 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL19 { get; set; }

        /// <summary>
        /// Vinyl attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Vinyls")]
        [Browsable(false)]
        public Vinyl VINYL20 { get; set; }

        /// <summary>
        /// Autosculpt Front Bumper attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt FRONTBUMPER { get; set; }

        /// <summary>
        /// Autosculpt Rear Bumper attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt REARBUMPER { get; set; }

        /// <summary>
        /// Autosculpt Skirt attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt SKIRT { get; set; }

        /// <summary>
        /// Autosculpt Wheels attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt WHEELS { get; set; }

        /// <summary>
        /// Autosculpt Hood attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt HOOD { get; set; }

        /// <summary>
        /// Autosculpt Spoiler attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt SPOILER { get; set; }

        /// <summary>
        /// Autosculpt RoofScoop attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Autosculpt")]
        [Browsable(false)]
        public Autosculpt ROOFSCOOP { get; set; }

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
            this.MODEL = "SUPRA";
            this.Frontend = "supra";
            this.Pvehicle = "supra";
            this.Initialize();
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
            bw.Write((long)0);

            // MODEL
            bw.WriteNullTermUTF8(this.MODEL, 0x20);

            // CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x20);

            // Frontend and Pvehicle
            bw.Write(this.Frontend.VltHash());
            bw.Write((int)0);
            bw.Write(this.Pvehicle.VltHash());
            bw.WriteBytes(0xC);

            // Start reading parts
            bw.Write(this.Base.BinHash());

            // Read Kit Damages
            this.KIT_DAMAGES.Write(bw);

            // Continue reading parts
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

            // Read Zero Damages
            this.ZERO_DAMAGES.Write(bw);

            // Read Attachments
            this.ATTACHMENTS.Write(bw);

            // Continue reading parts
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

            // Read Visual Sets
            this.VISUAL_SETS.Write(bw);

            // Finish reading parts
            bw.Write(this.WindshieldTint.BinHash());
            bw.Write(this.CustomHUD.BinHash());
            bw.Write(this.CV.BinHash());
            bw.Write(this.Misc.BinHash());

            // Read Paint
            this.PAINT_VALUES.Write(bw);

            // Read Autosculpt
            bw.WriteBytes(0x10);
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
            bw.WriteBytes(10);
            bw.Write(this.ExhaustSizeValue);
            bw.WriteBytes(11);

            // Read Vinyls
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
            this._collection_name = br.ReadNullTermUTF8(0x20);

            // Frontend and Pvehicle
            this.Frontend = br.ReadUInt32().VltString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Pvehicle = br.ReadUInt32().VltString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 0xC;

            // Start reading parts
            this.Base = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Kit Damages
            this.KIT_DAMAGES.Read(br);

            // Continue reading parts
            this.AftermarketBodykit = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontBrake = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontRotor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontLeftWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontRightWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Interior = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LeftBrakelight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LeftBrakelightGlass = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LeftHeadlight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LeftHeadlightGlass = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LeftSideMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearBrake = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearRotor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearLeftWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearRightWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightBrakelight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightBrakelightGlass = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightHeadlight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightHeadlightGlass = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightSideMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Driver = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SteeringWheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Exhaust = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Spoiler = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.UniversalSpoilerBase = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Zero Damages
            this.ZERO_DAMAGES.Read(br);

            // Read Attachments
            this.ATTACHMENTS.Read(br);

            // Continue reading parts
            this.AutosculptFrontBumper = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontBumperBadgingSet = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.AutosculptRearBumper = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearBumperBadgingSet = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RoofTop = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RoofScoop = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Hood = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.AutosculptSkirt = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Headlight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Brakelight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.DoorLeft = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.DoorRight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontWheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearWheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LicensePlate = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Doorline = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.DecalFrontWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.DecalRearWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Visual Sets
            this.VISUAL_SETS.Read(br);

            // Finish reading parts
            this.WindshieldTint = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.CustomHUD = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.CV = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Misc = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

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

        private void Initialize()
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
    }
}