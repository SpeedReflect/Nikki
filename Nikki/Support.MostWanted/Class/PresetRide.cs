using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.MostWanted.Framework;
using Nikki.Support.MostWanted.Parts.PresetParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.MostWanted.Class
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
        public const int BaseClassSize = 0x290;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override GameINT GameINT => GameINT.MostWanted;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.MostWanted.ToString();

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
        public string LicensePlate { get; set; } = String.Empty;

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
        /// Decal size attributes of this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("BaseKit")]
        [Browsable(false)]
        public DecalSize DECAL_SIZES { get; set; }

        /// <summary>
        /// Group of paints and vinyls in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        [Browsable(false)]
        public VisualSets VISUAL_SETS { get; set; }

        /// <summary>
        /// Set of front window decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        [Browsable(false)]
        public DecalArray DECALS_FRONT_WINDOW { get; set; }

        /// <summary>
        /// Set of rear window decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        [Browsable(false)]
        public DecalArray DECALS_REAR_WINDOW { get; set; }

        /// <summary>
        /// Set of left door decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        [Browsable(false)]
        public DecalArray DECALS_LEFT_DOOR { get; set; }

        /// <summary>
        /// Set of right door decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        [Browsable(false)]
        public DecalArray DECALS_RIGHT_DOOR { get; set; }

        /// <summary>
        /// Set of left quarter decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        [Browsable(false)]
        public DecalArray DECALS_LEFT_QUARTER { get; set; }

        /// <summary>
        /// Set of right quarter decals in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Decals")]
        [Browsable(false)]
        public DecalArray DECALS_RIGHT_QUARTER { get; set; }

        /// <summary>
        /// Set of HUD elements in this <see cref="PresetRide"/>.
        /// </summary>
        [Expandable("Visuals")]
        [Browsable(false)]
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
            // Write unknown value
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
            bw.Write(this.RearLeftWindow.BinHash());
            bw.Write(this.RearRightWindow.BinHash());
            bw.Write(this.RearWindow.BinHash());
            bw.Write(this.RightBrakelight.BinHash());
            bw.Write(this.RightBrakelightGlass.BinHash());
            bw.Write(this.RightHeadlight.BinHash());
            bw.Write(this.RightHeadlightGlass.BinHash());
            bw.Write(this.RightSideMirror.BinHash());
            bw.Write(this.Driver.BinHash());
            bw.Write(this.Spoiler.BinHash());
            bw.Write(this.UniversalSpoilerBase.BinHash());

            // Read Zero Damages
            this.ZERO_DAMAGES.Write(bw);

            // Read Attachments
            this.ATTACHMENTS.Write(bw);

            // Continue reading parts
            bw.Write(this.RoofScoop.BinHash());
            bw.Write(this.Hood.BinHash());
            bw.Write(this.Headlight.BinHash());
            bw.Write(this.Brakelight.BinHash());
            bw.Write(this.FrontWheel.BinHash());
            bw.Write(this.RearWheel.BinHash());
            bw.Write(this.Spinner.BinHash());
            bw.Write(this.LicensePlate.BinHash());

            // Read Decal Sizes
            this.DECAL_SIZES.Write(bw);

            // Read Visual Sets
            this.VISUAL_SETS.Write(bw);

            // Read Decal Arrays
            this.DECALS_FRONT_WINDOW.Write(bw);
            this.DECALS_REAR_WINDOW.Write(bw);
            this.DECALS_LEFT_DOOR.Write(bw);
            this.DECALS_RIGHT_DOOR.Write(bw);
            this.DECALS_LEFT_QUARTER.Write(bw);
            this.DECALS_RIGHT_QUARTER.Write(bw);

            // Continue reading parts
            bw.Write(this.WindshieldTint.BinHash());

            // Read HUD
            this.HUD.Write(bw);

            // Finish reading parts
            bw.Write(this.CV.BinHash());
            bw.Write(this.WheelManufacturer.BinHash());
            bw.Write(this.Misc.BinHash());
            bw.Write((int)0);
        }

        /// <summary>
        /// Disassembles array into <see cref="PresetRide"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="PresetRide"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            // Read unknown values
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
            this.RearLeftWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearRightWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightBrakelight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightBrakelightGlass = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightHeadlight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightHeadlightGlass = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RightSideMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Driver = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Spoiler = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.UniversalSpoilerBase = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Zero Damages
            this.ZERO_DAMAGES.Read(br);

            // Read Attachments
            this.ATTACHMENTS.Read(br);

            // Continue reading parts
            this.RoofScoop = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Hood = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Headlight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Brakelight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FrontWheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.RearWheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Spinner = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.LicensePlate = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Decal Sizes
            this.DECAL_SIZES.Read(br);

            // Read Visual Sets
            this.VISUAL_SETS.Read(br);

            // Read Decal Arrays
            this.DECALS_FRONT_WINDOW.Read(br);
            this.DECALS_REAR_WINDOW.Read(br);
            this.DECALS_LEFT_DOOR.Read(br);
            this.DECALS_RIGHT_DOOR.Read(br);
            this.DECALS_LEFT_QUARTER.Read(br);
            this.DECALS_RIGHT_QUARTER.Read(br);

            // Continue reading parts
            this.WindshieldTint = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read HUD
            this.HUD.Read(br);

            // Finish reading parts
            this.CV = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.WheelManufacturer = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Misc = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
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
            this.ATTACHMENTS = new Attachments();
            this.DECALS_FRONT_WINDOW = new DecalArray();
            this.DECALS_LEFT_DOOR = new DecalArray();
            this.DECALS_LEFT_QUARTER = new DecalArray();
            this.DECALS_REAR_WINDOW = new DecalArray();
            this.DECALS_RIGHT_DOOR = new DecalArray();
            this.DECALS_RIGHT_QUARTER = new DecalArray();
            this.DECAL_SIZES = new DecalSize();
            this.HUD = new HUDStyle();
            this.KIT_DAMAGES = new Damages();
            this.VISUAL_SETS = new VisualSets();
            this.ZERO_DAMAGES = new ZeroDamage();
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