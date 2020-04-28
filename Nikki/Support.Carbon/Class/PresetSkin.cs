using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="PresetSkin"/> is a collection of settings related to car skins.
    /// </summary>
    public partial class PresetSkin : Shared.Class.PresetSkin
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
        public const int CNameOffsetAt = 0x8;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0x68;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Carbon;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Carbon.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Carbon Database { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                if (value.Length > MaxCNameLength)
                    throw new ArgumentLengthException(MaxCNameLength);
                if (this.Database.PresetSkins.FindCollection(value) != null)
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
        /// Generic vinyl value of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public string GenericVinyl { get; set; } = String.Empty;

        /// <summary>
        /// Vector vinyl value of the preset skin.
        /// </summary>
        [AccessModifiable()]
        public string VectorVinyl { get; set; } = String.Empty;

        /// <summary>
        /// Y-Position value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public short PositionY { get; set; } = 0;

        /// <summary>
        /// X-Position value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public short PositionX { get; set; } = 0;

        /// <summary>
        /// Rotation value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public sbyte Rotation { get; set; } = 0;

        /// <summary>
        /// Skew value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public sbyte Skew { get; set; } = 0;

        /// <summary>
        /// Y-Scale value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public sbyte ScaleY { get; set; } = 0;

        /// <summary>
        /// X-Scale value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public sbyte ScaleX { get; set; } = 0;

        /// <summary>
        /// Swatch color value of the first color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public string SwatchColor1 { get; set; } = String.Empty;

        /// <summary>
        /// Swatch color value of the second color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public string SwatchColor2 { get; set; } = String.Empty;

        /// <summary>
        /// Swatch color value of the third color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public string SwatchColor3 { get; set; } = String.Empty;

        /// <summary>
        /// Swatch color value of the fourth color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public string SwatchColor4 { get; set; } = String.Empty;

        /// <summary>
        /// Saturation value of the first color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Saturation1 { get; set; } = 0;

        /// <summary>
        /// Saturation value of the second color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Saturation2 { get; set; } = 0;

        /// <summary>
        /// Saturation value of the third color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Saturation3 { get; set; } = 0;

        /// <summary>
        /// Saturation value of the fourth color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Saturation4 { get; set; } = 0;

        /// <summary>
        /// Brightness value of the first color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Brightness1 { get; set; } = 0;

        /// <summary>
        /// Brightness value of the second color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Brightness2 { get; set; } = 0;

        /// <summary>
        /// Brightness value of the third color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Brightness3 { get; set; } = 0;

        /// <summary>
        /// Brightness value of the fourth color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public byte Brightness4 { get; set; } = 0;

        #endregion

        #region Main

        // Default constructor
        public PresetSkin() { }

        // Default constructor: create new skin
        public PresetSkin(string CName, Database.Carbon db)
        {
            this.Database = db;
            this.CollectionName = CName;
            CName.BinHash();
        }

        // Default constructor: disassemble skin
        public unsafe PresetSkin(BinaryReader br, Database.Carbon db)
        {
            this.Database = db;
            this.Disassemble(br);
        }

        // Default destructor
        ~PresetSkin() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="PresetSkin"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PresetSkin"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write CollectionName
            bw.Write((long)0);
            bw.WriteNullTermUTF8(this._collection_name, 0x20);

            // Write all main settings
            bw.Write(this.PaintType.BinHash());
            bw.Write(this.PaintSwatch.BinHash());
            bw.Write(this.PaintSaturation);
            bw.Write(this.PaintBrightness);

            // Write Generic Vinyl
            bw.Write(this.GenericVinyl.BinHash());

            // Write Vector Vinyl
            bw.Write(this.VectorVinyl.BinHash());
            bw.Write(this.PositionY);
            bw.Write(this.PositionX);
            bw.Write((byte)this.Rotation);
            bw.Write((byte)this.Skew);
            bw.Write((byte)this.ScaleY);
            bw.Write((byte)this.ScaleX);
            bw.Write(this.SwatchColor1.BinHash());
            bw.Write(this.Saturation1);
            bw.Write(this.Brightness1);
            bw.Write((short)0);
            bw.Write(this.SwatchColor2.BinHash());
            bw.Write(this.Saturation2);
            bw.Write(this.Brightness2);
            bw.Write((short)0);
            bw.Write(this.SwatchColor3.BinHash());
            bw.Write(this.Saturation3);
            bw.Write(this.Brightness3);
            bw.Write((short)0);
            bw.Write(this.SwatchColor4.BinHash());
            bw.Write(this.Saturation4);
            bw.Write(this.Brightness4);
            bw.Write((short)0);
        }

        /// <summary>
        /// Disassembles array into <see cref="PresetSkin"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="PresetSkin"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 8;

            // Read CollectionName
            this._collection_name = br.ReadNullTermUTF8(0x20);

            // Read paint settings
            this.PaintType = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PaintSwatch = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PaintSaturation = br.ReadSingle();
            this.PaintBrightness = br.ReadSingle();

            // Generic vinyl
            this.GenericVinyl = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

            // Vinyl
            this.VectorVinyl = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PositionY = br.ReadInt16();
            this.PositionX = br.ReadInt16();
            this.Rotation = br.ReadSByte();
            this.Skew = br.ReadSByte();
            this.ScaleY = br.ReadSByte();
            this.ScaleX = br.ReadSByte();
            this.SwatchColor1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation1 = br.ReadByte();
            this.Brightness1 = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchColor2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation2 = br.ReadByte();
            this.Brightness2 = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchColor3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation3 = br.ReadByte();
            this.Brightness3 = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchColor4 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation4 = br.ReadByte();
            this.Brightness4 = br.ReadByte();
            br.BaseStream.Position += 2;
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            var result = new PresetSkin(CName, this.Database)
            {
                PositionY = this.PositionY,
                PositionX = this.PositionX,
                Rotation = this.Rotation,
                Skew = this.Skew,
                ScaleY = this.ScaleY,
                ScaleX = this.ScaleX,
                Saturation1 = this.Saturation1,
                Saturation2 = this.Saturation2,
                Saturation3 = this.Saturation3,
                Saturation4 = this.Saturation4,
                Brightness1 = this.Brightness1,
                Brightness2 = this.Brightness2,
                Brightness3 = this.Brightness3,
                Brightness4 = this.Brightness4,
                SwatchColor1 = this.SwatchColor1,
                SwatchColor2 = this.SwatchColor2,
                SwatchColor3 = this.SwatchColor3,
                SwatchColor4 = this.SwatchColor4,
                GenericVinyl = this.GenericVinyl,
                VectorVinyl = this.VectorVinyl,
                PaintSwatch = this.PaintSwatch,
                PaintBrightness = this.PaintBrightness,
                PaintSaturation = this.PaintSaturation,
                PaintType = this.PaintType
            };

            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="PresetSkin"/> 
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