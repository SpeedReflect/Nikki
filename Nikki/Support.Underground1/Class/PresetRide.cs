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
        public const int BaseClassSize = 0x408;

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
                    throw new CollectionExistenceException(value);
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
        [MemoryCastable()]
        public override string MODEL { get; set; } = String.Empty;

        /// <summary>
        /// Unknown key 1.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public int UnknownKey1 { get; set; }

        /// <summary>
        /// Unknown key 2.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public int UnknownKey2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Base { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string AutosculptFrontBumper { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string AutosculptRearBumper { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string AutosculptSkirt { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string LeftSideMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string RightSideMirror { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Body { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string AftermarketBodykit { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string RoofScoop { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Hood { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Trunk { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Spoiler { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Engine { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Headlight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Brakelight { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Exhaust { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Brake { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Wheel { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string LicensePlate { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string DecalTex { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string WindshieldTint { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string NeonBody { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string CV { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string WheelManufacturer { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Misc { get; set; } = String.Empty;

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
            bw.Write(this.UnknownKey1);
            bw.Write(this.UnknownKey2);

            // MODEL
            bw.WriteNullTermUTF8(this.MODEL, 0x20);

            // CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x20);

            // Start writing parts
            bw.Write((int)0x00);
            bw.Write(this.Base.BinHash());
            bw.Write((int)0x01);
            bw.Write(this.AutosculptFrontBumper.BinHash());
            bw.Write((int)0x02);
            bw.Write(this.AutosculptRearBumper.BinHash());
            bw.Write((int)0x03);
            bw.Write(this.LeftSideMirror.BinHash());
            bw.Write((int)0x04);
            bw.Write(this.RightSideMirror.BinHash());
            bw.Write((int)0x05);
            bw.Write(this.Body.BinHash());
            bw.Write((int)0x06);
            bw.Write(this.AftermarketBodykit.BinHash());
            bw.Write((int)0x07);
            bw.Write(this.RoofScoop.BinHash());
            bw.Write((int)0x08);
            bw.Write(this.Hood.BinHash());
            bw.Write((int)0x09);
            bw.Write(this.Trunk.BinHash());
            bw.Write((int)0x0A);
            bw.Write(this.AutosculptSkirt.BinHash());
            bw.Write((int)0x0B);
            bw.Write(this.Spoiler.BinHash());
            bw.Write((int)0x0C);
            bw.Write(this.Engine.BinHash());
            bw.Write((int)0x0D);
            bw.Write(this.Headlight.BinHash());
            bw.Write((int)0x0E);
            bw.Write(this.Brakelight.BinHash());
            bw.Write((int)0x0F);
            bw.Write(this.Exhaust.BinHash());
            bw.Write((int)0x10);
            bw.Write(this.Brake.BinHash());
            bw.Write((int)0x11);
            bw.Write(this.Wheel.BinHash());
            bw.Write((int)0x12);
            bw.Write(this.LicensePlate.BinHash());

            // Read Decal Sizes
            this.DECAL_SIZES.Write(bw);

            // Continue reading parts
            bw.Write((int)0x1E);
            bw.Write(this.PAINT_TYPES.BasePaintType.BinHash());
            bw.Write((int)0x1F);
            bw.Write(this.VINYL_SETS.VinylLayer0.BinHash());
            bw.Write((int)0x20);
            bw.Write(this.VINYL_SETS.VinylLayer1.BinHash());
            bw.Write((int)0x21);
            bw.Write(this.VINYL_SETS.VinylLayer2.BinHash());
            bw.Write((int)0x22);
            bw.Write(this.VINYL_SETS.VinylLayer3.BinHash());
            bw.Write((int)0x23);
            bw.Write(this.VINYL_SETS.VinylHood.BinHash());
            bw.Write((int)0x24);
            bw.Write(this.VINYL_SETS.VinylSpoiler.BinHash());
            bw.Write((int)0x25);
            bw.Write(this.PAINT_TYPES.EnginePaintType.BinHash());
            bw.Write((int)0x26);
            bw.Write(this.PAINT_TYPES.SpoilerPaintType.BinHash());
            bw.Write((int)0x27);
            bw.Write(this.PAINT_TYPES.BrakesPaintType.BinHash());
            bw.Write((int)0x28);
            bw.Write(this.PAINT_TYPES.ExhaustPaintType.BinHash());
            bw.Write((int)0x29);
            bw.Write(this.PAINT_TYPES.RimsPaintType.BinHash());
            bw.Write((int)0x2A);
            bw.Write(this.VINYL_SETS.Vinyl0_Color0.BinHash());
            bw.Write((int)0x2B);
            bw.Write(this.VINYL_SETS.Vinyl0_Color1.BinHash());
            bw.Write((int)0x2C);
            bw.Write(this.VINYL_SETS.Vinyl0_Color2.BinHash());
            bw.Write((int)0x2D);
            bw.Write(this.VINYL_SETS.Vinyl0_Color3.BinHash());
            bw.Write((int)0x2E);
            bw.Write(this.VINYL_SETS.Vinyl1_Color0.BinHash());
            bw.Write((int)0x2F);
            bw.Write(this.VINYL_SETS.Vinyl1_Color1.BinHash());
            bw.Write((int)0x30);
            bw.Write(this.VINYL_SETS.Vinyl1_Color2.BinHash());
            bw.Write((int)0x31);
            bw.Write(this.VINYL_SETS.Vinyl1_Color3.BinHash());
            bw.Write((int)0x32);
            bw.Write(this.VINYL_SETS.Vinyl2_Color0.BinHash());
            bw.Write((int)0x33);
            bw.Write(this.VINYL_SETS.Vinyl2_Color1.BinHash());
            bw.Write((int)0x34);
            bw.Write(this.VINYL_SETS.Vinyl2_Color2.BinHash());
            bw.Write((int)0x35);
            bw.Write(this.VINYL_SETS.Vinyl2_Color3.BinHash());
            bw.Write((int)0x36);
            bw.Write(this.VINYL_SETS.Vinyl3_Color0.BinHash());
            bw.Write((int)0x37);
            bw.Write(this.VINYL_SETS.Vinyl3_Color1.BinHash());
            bw.Write((int)0x38);
            bw.Write(this.VINYL_SETS.Vinyl3_Color2.BinHash());
            bw.Write((int)0x39);
            bw.Write(this.VINYL_SETS.Vinyl3_Color3.BinHash());
            bw.Write((int)0x3A);
            bw.Write(this.DecalTex.BinHash());

            // Read Decal Arrays
            this.DECALS_HOOD.Write(bw, 0x3B);
            this.DECALS_FRONT_WINDOW.Write(bw, 0x43);
            this.DECALS_REAR_WINDOW.Write(bw, 0x4B);
            this.DECALS_LEFT_DOOR.Write(bw, 0x53);
            this.DECALS_RIGHT_DOOR.Write(bw, 0x5B);
            this.DECALS_LEFT_QUARTER.Write(bw, 0x63);
            this.DECALS_RIGHT_QUARTER.Write(bw, 0x6B);

            // Finish reading parts
            bw.Write((int)0x73);
            bw.Write(this.WindshieldTint.BinHash());
            bw.Write((int)0x74);
            bw.Write(this.NeonBody.BinHash());
            bw.Write((int)0x75);
            bw.Write(this.CV.BinHash());
            bw.Write((int)0x76);
            bw.Write(this.WheelManufacturer.BinHash());
            bw.Write((int)0x77);
            bw.Write(this.Misc.BinHash());
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

            // Start reading parts
            br.BaseStream.Position += 4;
            this.Base = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.AutosculptFrontBumper = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.AutosculptRearBumper = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.LeftSideMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.RightSideMirror = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Body = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.AftermarketBodykit = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.RoofScoop = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Hood = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Trunk = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.AutosculptSkirt = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Spoiler = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Engine = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Headlight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Brakelight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Exhaust = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Brake = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Wheel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.LicensePlate = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Decal Sizes
            this.DECAL_SIZES.Read(br);

            // Continue reading parts
            br.BaseStream.Position += 4;
            this.PAINT_TYPES.BasePaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.VinylLayer0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.VinylLayer1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.VinylLayer2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.VinylLayer3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.VinylHood = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.VinylSpoiler = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.PAINT_TYPES.EnginePaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.PAINT_TYPES.SpoilerPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.PAINT_TYPES.BrakesPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.PAINT_TYPES.ExhaustPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.PAINT_TYPES.RimsPaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl0_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl0_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl0_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl0_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl1_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl1_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl1_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl1_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl2_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl2_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl2_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl2_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl3_Color0 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl3_Color1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl3_Color2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.VINYL_SETS.Vinyl3_Color3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.DecalTex = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Read Decal Arrays
            this.DECALS_HOOD.Read(br);
            this.DECALS_FRONT_WINDOW.Read(br);
            this.DECALS_REAR_WINDOW.Read(br);
            this.DECALS_LEFT_DOOR.Read(br);
            this.DECALS_RIGHT_DOOR.Read(br);
            this.DECALS_LEFT_QUARTER.Read(br);
            this.DECALS_RIGHT_QUARTER.Read(br);

            // Finish reading parts
            br.BaseStream.Position += 4;
            this.WindshieldTint = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.NeonBody = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.CV = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.WheelManufacturer = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            br.BaseStream.Position += 4;
            this.Misc = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            var result = new PresetRide(CName, this.Database);
            base.MemoryCast(this, result);
            return result;
        }

        private void Initialize()
        {
            this.DECALS_FRONT_WINDOW = new DecalArray();
            this.DECALS_HOOD = new DecalArray();
            this.DECALS_LEFT_DOOR = new DecalArray();
            this.DECALS_LEFT_QUARTER = new DecalArray();
            this.DECALS_REAR_WINDOW = new DecalArray();
            this.DECALS_RIGHT_DOOR = new DecalArray();
            this.DECALS_RIGHT_QUARTER = new DecalArray();
            this.DECAL_SIZES = new DecalSize();
            this.PAINT_TYPES = new PaintTypes();
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