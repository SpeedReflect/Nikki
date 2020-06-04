using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Carbon.Framework;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="CarTypeInfo"/> is a collection of settings related to a car's basic information.
    /// </summary>
    public class CarTypeInfo : Shared.Class.CarTypeInfo
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
        public CarTypeInfoManager Manager { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                this.Manager.CreationCheck(value);
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
        [AccessModifiable()]
        [MemoryCastable()]
        public override string ManufacturerName { get; set; } = String.Empty;

        /// <summary>
        /// Default base paint of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public override string DefaultBasePaint { get; set; } = String.Empty;

        /// <summary>
        /// Represents memory type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public eMemoryType MemoryType { get; set; }

        /// <summary>
        /// Spoiler type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string Spoiler { get; set; } = String.Empty;

        /// <summary>
        /// Autosculpt spoiler type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public string SpoilerAS { get; set; } = String.Empty;

        /// <summary>
        /// RoofScoop type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
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
        /// <param name="manager"><see cref="CarTypeInfoManager"/> to which this instance belongs to.</param>
        public CarTypeInfo(string CName, CarTypeInfoManager manager)
        {
            this.Manager = manager;
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
        /// <param name="manager"><see cref="CarTypeInfoManager"/> to which this instance belongs to.</param>
        public CarTypeInfo(BinaryReader br, CarTypeInfoManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
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

            // Hash
            this._collection_name.BinHash();
            this._collection_name.VltHash();

            br.BaseStream.Position += 4;
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
            var result = new CarTypeInfo(CName, this.Manager);
            base.MemoryCast(this, result);
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