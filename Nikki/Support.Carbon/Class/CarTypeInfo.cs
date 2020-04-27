using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="CarTypeInfo"/> is a collection of settings related to a car's basic information.
    /// </summary>
    public partial class CarTypeInfo : Shared.Class.CarTypeInfo
    {
        #region Fields

        private string _collection_name;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = 0xD;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0;
        
        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0xD0;

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
                if (!this.Deletable)
                    throw new CollectionExistenceException("CollectionName of a non-addon car cannot be changed.");
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                if (value.Length > MaxCNameLength)
                    throw new ArgumentLengthException(MaxCNameLength);
                if (this.Database.CarTypeInfos.FindCollection(value) != null)
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
        /// Manufacturer name of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public override string ManufacturerName { get; set; }

        /// <summary>
        /// Default base paint of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public override string DefaultBasePaint { get; set; } = String.Empty;

        /// <summary>
        /// Represents memory type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        public eMemoryType MemoryType { get; set; }

        /// <summary>
        /// Spoiler type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public string Spoiler { get; set; } = String.Empty;

        /// <summary>
        /// Autosculpt spoiler type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public string SpoilerAS { get; set; } = String.Empty;

        /// <summary>
        /// RoofScoop type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public string RoofScoop { get; set; } = String.Empty;

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        public CarTypeInfo() { }

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public CarTypeInfo(string CName, Database.Carbon db)
        {
            this.Database = db;
            this.CollectionName = CName;
            this.ManufacturerName = "GENERIC";
            this.Deletable = true;
            this.WhatGame = 1;
            this.WheelOuterRadius = 26;
            this.WheelInnerRadiusMin = 17;
            this.WheelInnerRadiusMax = 20;
            this.DefaultSkinNumber = 1;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public CarTypeInfo(BinaryReader br, Database.Carbon db)
        {
            this.Database = db;
            this.Disassemble(br);
            if (this.Index <= (int)eBoundValues.MIN_INFO_CARBON)
                this.Deletable = false;
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~CarTypeInfo() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="CarTypeInfo"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="CarTypeInfo"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write CollectionName and BaseModelName
            bw.WriteNullTermUTF8(this._collection_name, 0x10);
            bw.WriteNullTermUTF8(this._collection_name, 0x10);

            // Write GeometryFileName
            string path = Path.Combine("CARS", this.CollectionName, "GEOMETRY.BIN");
            bw.WriteNullTermUTF8(path, 0x20);

            // Write ManufacturerName
            bw.WriteNullTermUTF8(this.ManufacturerName, 0x10);

            // Write all settings
            bw.Write(this.BinKey);
            bw.Write(this.HeadlightFOV);
            bw.Write(this.PadHighPerformance);
            bw.Write(this.NumAvailableSkinNumbers);
            bw.Write(this.WhatGame);
            bw.Write(this.ConvertibleFlag);
            bw.Write(this.WheelOuterRadius);
            bw.Write(this.WheelInnerRadiusMin);
            bw.Write(this.WheelInnerRadiusMax);
            bw.Write((byte)0);

            bw.Write(this.HeadlightPositionX);
            bw.Write(this.HeadlightPositionY);
            bw.Write(this.HeadlightPositionZ);
            bw.Write(this.HeadlightPositionW);
            bw.Write(this.DriverRenderingOffsetX);
            bw.Write(this.DriverRenderingOffsetY);
            bw.Write(this.DriverRenderingOffsetZ);
            bw.Write(this.DriverRenderingOffsetW);
            bw.Write(this.SteeringWheelRenderingX);
            bw.Write(this.SteeringWheelRenderingY);
            bw.Write(this.SteeringWheelRenderingZ);
            bw.Write(this.SteeringWheelRenderingW);

            bw.Write(this.Index);
            bw.WriteEnum(this.UsageType);
            bw.WriteEnum(this.MemoryType);

            bw.Write(this.MaxInstances1);
            bw.Write(this.MaxInstances2);
            bw.Write(this.MaxInstances3);
            bw.Write(this.MaxInstances4);
            bw.Write(this.MaxInstances5);
            bw.Write(this.KeepLoaded1);
            bw.Write(this.KeepLoaded2);
            bw.Write(this.KeepLoaded3);
            bw.Write(this.KeepLoaded4);
            bw.Write(this.KeepLoaded5);
            bw.Write((short)0);

            bw.Write(this.MinTimeBetweenUses1);
            bw.Write(this.MinTimeBetweenUses2);
            bw.Write(this.MinTimeBetweenUses3);
            bw.Write(this.MinTimeBetweenUses4);
            bw.Write(this.MinTimeBetweenUses5);
            bw.Write(this.AvailableSkinNumbers01);
            bw.Write(this.AvailableSkinNumbers02);
            bw.Write(this.AvailableSkinNumbers03);
            bw.Write(this.AvailableSkinNumbers04);
            bw.Write(this.AvailableSkinNumbers05);
            bw.Write(this.AvailableSkinNumbers06);
            bw.Write(this.AvailableSkinNumbers07);
            bw.Write(this.AvailableSkinNumbers08);
            bw.Write(this.AvailableSkinNumbers09);
            bw.Write(this.AvailableSkinNumbers10);

            bw.Write(this.DefaultSkinNumber);
            bw.WriteEnum(this.IsSkinnable);
            bw.Write((int)0);
            bw.Write(this.DefaultBasePaint.BinHash());
        }

        /// <summary>
        /// Disassembles array into <see cref="CarTypeInfo"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="CarTypeInfo"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            // Get Manufacturer name
            this._collection_name = br.ReadNullTermUTF8(0x10);
            br.BaseStream.Position += 0x30;
            this.ManufacturerName = br.ReadNullTermUTF8(0x10);

            this.HeadlightFOV = br.ReadSingle();
            this.PadHighPerformance = br.ReadByte();
            this.NumAvailableSkinNumbers = br.ReadByte();
            this.WhatGame = br.ReadByte();
            this.ConvertibleFlag = br.ReadByte();
            this.WheelOuterRadius = br.ReadByte();
            this.WheelInnerRadiusMin = br.ReadByte();
            this.WheelInnerRadiusMax = br.ReadByte();
            br.BaseStream.Position += 1;

            this.HeadlightPositionX = br.ReadSingle();
            this.HeadlightPositionY = br.ReadSingle();
            this.HeadlightPositionZ = br.ReadSingle();
            this.HeadlightPositionW = br.ReadSingle();
            this.DriverRenderingOffsetX = br.ReadSingle();
            this.DriverRenderingOffsetY = br.ReadSingle();
            this.DriverRenderingOffsetZ = br.ReadSingle();
            this.DriverRenderingOffsetW = br.ReadSingle();
            this.SteeringWheelRenderingX = br.ReadSingle();
            this.SteeringWheelRenderingY = br.ReadSingle();
            this.SteeringWheelRenderingZ = br.ReadSingle();
            this.SteeringWheelRenderingW = br.ReadSingle();

            this.Index = br.ReadInt32();
            this.UsageType = br.ReadEnum<eUsageType>();
            this.MemoryType = br.ReadEnum<eMemoryType>();

            this.MaxInstances1 = br.ReadByte();
            this.MaxInstances2 = br.ReadByte();
            this.MaxInstances3 = br.ReadByte();
            this.MaxInstances4 = br.ReadByte();
            this.MaxInstances5 = br.ReadByte();
            this.KeepLoaded1 = br.ReadByte();
            this.KeepLoaded2 = br.ReadByte();
            this.KeepLoaded3 = br.ReadByte();
            this.KeepLoaded4 = br.ReadByte();
            this.KeepLoaded5 = br.ReadByte();
            br.BaseStream.Position += 2;

            this.MinTimeBetweenUses1 = br.ReadSingle();
            this.MinTimeBetweenUses2 = br.ReadSingle();
            this.MinTimeBetweenUses3 = br.ReadSingle();
            this.MinTimeBetweenUses4 = br.ReadSingle();
            this.MinTimeBetweenUses5 = br.ReadSingle();
            this.AvailableSkinNumbers01 = br.ReadByte();
            this.AvailableSkinNumbers02 = br.ReadByte();
            this.AvailableSkinNumbers03 = br.ReadByte();
            this.AvailableSkinNumbers04 = br.ReadByte();
            this.AvailableSkinNumbers05 = br.ReadByte();
            this.AvailableSkinNumbers06 = br.ReadByte();
            this.AvailableSkinNumbers07 = br.ReadByte();
            this.AvailableSkinNumbers08 = br.ReadByte();
            this.AvailableSkinNumbers09 = br.ReadByte();
            this.AvailableSkinNumbers10 = br.ReadByte();

            this.DefaultSkinNumber = br.ReadByte();
            this.IsSkinnable = br.ReadEnum<eBoolean>();
            br.BaseStream.Position += 4;

            this.DefaultBasePaint = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            var result = new CarTypeInfo(CName, this.Database)
            {
                Spoiler = this.Spoiler,
                SpoilerAS = this.SpoilerAS,
                RoofScoop = this.RoofScoop,
                UsageType = this.UsageType,
                MemoryType = this.MemoryType,
                IsSkinnable = this.IsSkinnable,
                ManufacturerName = this.ManufacturerName,
                DefaultBasePaint = this.DefaultBasePaint,
                HeadlightFOV = this.HeadlightFOV,
                PadHighPerformance = this.PadHighPerformance,
                NumAvailableSkinNumbers = this.NumAvailableSkinNumbers,
                WhatGame = this.WhatGame,
                ConvertibleFlag = this.ConvertibleFlag,
                WheelOuterRadius = this.WheelOuterRadius,
                WheelInnerRadiusMin = this.WheelInnerRadiusMin,
                WheelInnerRadiusMax = this.WheelInnerRadiusMax,
                HeadlightPositionX = this.HeadlightPositionX,
                HeadlightPositionY = this.HeadlightPositionY,
                HeadlightPositionZ = this.HeadlightPositionZ,
                HeadlightPositionW = this.HeadlightPositionW,
                DriverRenderingOffsetX = this.DriverRenderingOffsetX,
                DriverRenderingOffsetY = this.DriverRenderingOffsetY,
                DriverRenderingOffsetZ = this.DriverRenderingOffsetZ,
                DriverRenderingOffsetW = this.DriverRenderingOffsetW,
                SteeringWheelRenderingX = this.SteeringWheelRenderingX,
                SteeringWheelRenderingY = this.SteeringWheelRenderingY,
                SteeringWheelRenderingZ = this.SteeringWheelRenderingZ,
                SteeringWheelRenderingW = this.SteeringWheelRenderingW,
                MaxInstances1 = this.MaxInstances1,
                MaxInstances2 = this.MaxInstances2,
                MaxInstances3 = this.MaxInstances3,
                MaxInstances4 = this.MaxInstances4,
                MaxInstances5 = this.MaxInstances5,
                KeepLoaded1 = this.KeepLoaded1,
                KeepLoaded2 = this.KeepLoaded2,
                KeepLoaded3 = this.KeepLoaded3,
                KeepLoaded4 = this.KeepLoaded4,
                KeepLoaded5 = this.KeepLoaded5,
                MinTimeBetweenUses1 = this.MinTimeBetweenUses1,
                MinTimeBetweenUses2 = this.MinTimeBetweenUses2,
                MinTimeBetweenUses3 = this.MinTimeBetweenUses3,
                MinTimeBetweenUses4 = this.MinTimeBetweenUses4,
                MinTimeBetweenUses5 = this.MinTimeBetweenUses5,
                AvailableSkinNumbers01 = this.AvailableSkinNumbers01,
                AvailableSkinNumbers02 = this.AvailableSkinNumbers02,
                AvailableSkinNumbers03 = this.AvailableSkinNumbers03,
                AvailableSkinNumbers04 = this.AvailableSkinNumbers04,
                AvailableSkinNumbers05 = this.AvailableSkinNumbers05,
                AvailableSkinNumbers06 = this.AvailableSkinNumbers06,
                AvailableSkinNumbers07 = this.AvailableSkinNumbers07,
                AvailableSkinNumbers08 = this.AvailableSkinNumbers08,
                AvailableSkinNumbers09 = this.AvailableSkinNumbers09,
                AvailableSkinNumbers10 = this.AvailableSkinNumbers10,
                DefaultSkinNumber = this.DefaultSkinNumber
            };

            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="CarTypeInfo"/> 
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