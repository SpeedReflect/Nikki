using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="CarTypeInfo"/> is a collection of settings related to a car's basic information.
    /// </summary>
    public abstract class CarTypeInfo : Collectable, IAssembly
    {
        #region Shared Enums

        /// <summary>
        /// Enum of usage types.
        /// </summary>
        public enum CarUsageType : int
        {
            /// <summary>
            /// Car is used by racers.
            /// </summary>
            Racer = 0,

            /// <summary>
            /// Car is used by cops.
            /// </summary>
            Cop = 1,

            /// <summary>
            /// Car is used by traffic.
            /// </summary>
            Traffic = 2,

            /// <summary>
            /// CarTypeInfo is a wheels part.
            /// </summary>
            Wheels = 3,

            /// <summary>
            /// CarTypeInfo is a universal part.
            /// </summary>
            Universal = 4,
        }

        /// <summary>
        /// Enum of car memory types.
        /// </summary>
        public enum CarMemoryType : uint
        {
            /// <summary>
            /// Player Memory Type.
            /// </summary>
            Player = 0x757C0CEC,

            /// <summary>
            /// Racing Memory Type.
            /// </summary>
            Racing = 0x79602673,

            /// <summary>
            /// Cop Memory Type.
            /// </summary>
            Cop = 0x00009F61,

            /// <summary>
            /// Traffic Memory Type.
            /// </summary>
            Traffic = 0x66DB297E,

            /// <summary>
            /// BigTraffic Memory Type.
            /// </summary>
            BigTraffic = 0xAE00B390,

            /// <summary>
            /// HugeTraffic Memory Type.
            /// </summary>
            HugeTraffic = 0x911B6387,
        }

        #endregion

        #region Main Properties

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        public override string CollectionName { get; set; }

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.None;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.None.ToString();

        /// <summary>
        /// Binary memory hash of the collection name.
        /// </summary>
        public virtual uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public virtual uint VltKey => this.CollectionName.VltHash();

        #endregion

        #region AccessModifiable Properties

        /// <summary>
        /// Represents manufacturer name of the cartypeinfo.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public abstract string ManufacturerName { get; set; }

        /// <summary>
        /// Represents index of the <see cref="CarTypeInfo"/> in Global data.
        /// </summary>
        internal int Index { get; set; }

        /// <summary>
        /// Represents usage type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public CarUsageType UsageType { get; set; }

        /// <summary>
        /// Represents boolean as an int of whether <see cref="CarTypeInfo"/> is skinnable.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public eBoolean IsSkinnable { get; set; }

        /// <summary>
        /// Represents paint type of the cartypeinfo.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public abstract string DefaultBasePaint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float HeadlightFOV { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte PadHighPerformance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte NumAvailableSkinNumbers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte WhatGame { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte ConvertibleFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte WheelOuterRadius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte WheelInnerRadiusMin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte WheelInnerRadiusMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float HeadlightPositionX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float HeadlightPositionY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float HeadlightPositionZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float HeadlightPositionW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float DriverRenderingOffsetX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float DriverRenderingOffsetY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float DriverRenderingOffsetZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float DriverRenderingOffsetW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float SteeringWheelRenderingX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float SteeringWheelRenderingY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float SteeringWheelRenderingZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float SteeringWheelRenderingW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte MaxInstances1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte MaxInstances2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte MaxInstances3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte MaxInstances4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte MaxInstances5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte KeepLoaded1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte KeepLoaded2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte KeepLoaded3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte KeepLoaded4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte KeepLoaded5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float MinTimeBetweenUses1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float MinTimeBetweenUses2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float MinTimeBetweenUses3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float MinTimeBetweenUses4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float MinTimeBetweenUses5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers01 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers02 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers03 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers04 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers05 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers06 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers07 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers08 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers09 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte AvailableSkinNumbers10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte DefaultSkinNumber { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="CarTypeInfo"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="CarTypeInfo"/> with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="CarTypeInfo"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="CarTypeInfo"/> with.</param>
        public abstract void Disassemble(BinaryReader br);

        /// <summary>
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public abstract void Serialize(BinaryWriter bw);

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public abstract void Deserialize(BinaryReader br);

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}