using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Carbon.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="PresetSkin"/> is a collection of settings related to car skins.
    /// </summary>
    public class PresetSkin : Shared.Class.PresetSkin
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
        public PresetSkinManager Manager { get; set; }

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
        /// Generic vinyl value of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string GenericVinyl { get; set; } = String.Empty;

        /// <summary>
        /// Vector vinyl value of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string VectorVinyl { get; set; } = String.Empty;

        /// <summary>
        /// Y-Position value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short PositionY { get; set; } = 0;

        /// <summary>
        /// X-Position value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public short PositionX { get; set; } = 0;

        /// <summary>
        /// Rotation value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public sbyte Rotation { get; set; } = 0;

        /// <summary>
        /// Skew value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public sbyte Skew { get; set; } = 0;

        /// <summary>
        /// Y-Scale value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public sbyte ScaleY { get; set; } = 0;

        /// <summary>
        /// X-Scale value of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public sbyte ScaleX { get; set; } = 0;

        /// <summary>
        /// Swatch color value of the first color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SwatchFillEffect { get; set; } = String.Empty;

        /// <summary>
        /// Swatch color value of the second color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SwatchStrokeEffect { get; set; } = String.Empty;

        /// <summary>
        /// Swatch color value of the third color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SwatchInnerShadow { get; set; } = String.Empty;

        /// <summary>
        /// Swatch color value of the fourth color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SwatchInnerGlow { get; set; } = String.Empty;

        /// <summary>
        /// Saturation value of the first color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte SaturationFillEffect { get; set; } = 0;

        /// <summary>
        /// Saturation value of the second color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte SaturationStrokeEffect { get; set; } = 0;

        /// <summary>
        /// Saturation value of the third color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte SaturationInnerShadow { get; set; } = 0;

        /// <summary>
        /// Saturation value of the fourth color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte SaturationInnerGlow { get; set; } = 0;

        /// <summary>
        /// Brightness value of the first color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte BrightnessFillEffect { get; set; } = 0;

        /// <summary>
        /// Brightness value of the second color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte BrightnessStrokeEffect { get; set; } = 0;

        /// <summary>
        /// Brightness value of the third color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte BrightnessInnerShadow { get; set; } = 0;

        /// <summary>
        /// Brightness value of the fourth color of the vector vinyl of the preset skin.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte BrightnessInnerGlow { get; set; } = 0;

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="PresetSkin"/>.
        /// </summary>
        public PresetSkin() { }

        /// <summary>
        /// Initializes new instance of <see cref="PresetSkin"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="PresetSkinManager"/> to which this instance belongs to.</param>
        public PresetSkin(string CName, PresetSkinManager manager)
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="PresetSkin"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="PresetSkinManager"/> to which this instance belongs to.</param>
        public PresetSkin(BinaryReader br, PresetSkinManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
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
            bw.Write(this.SwatchFillEffect.BinHash());
            bw.Write(this.SaturationFillEffect);
            bw.Write(this.BrightnessFillEffect);
            bw.Write((short)0);
            bw.Write(this.SwatchStrokeEffect.BinHash());
            bw.Write(this.SaturationStrokeEffect);
            bw.Write(this.BrightnessStrokeEffect);
            bw.Write((short)0);
            bw.Write(this.SwatchInnerShadow.BinHash());
            bw.Write(this.SaturationInnerShadow);
            bw.Write(this.BrightnessInnerShadow);
            bw.Write((short)0);
            bw.Write(this.SwatchInnerGlow.BinHash());
            bw.Write(this.SaturationInnerGlow);
            bw.Write(this.BrightnessInnerGlow);
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
            this.SwatchFillEffect = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationFillEffect = br.ReadByte();
            this.BrightnessFillEffect = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchStrokeEffect = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationStrokeEffect = br.ReadByte();
            this.BrightnessStrokeEffect = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchInnerShadow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationInnerShadow = br.ReadByte();
            this.BrightnessInnerShadow = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchInnerGlow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationInnerGlow = br.ReadByte();
            this.BrightnessInnerGlow = br.ReadByte();
            br.BaseStream.Position += 2;
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new PresetSkin(CName, this.Manager);
            base.MemoryCast(this, result);
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
                   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
        }

        #endregion
    }
}