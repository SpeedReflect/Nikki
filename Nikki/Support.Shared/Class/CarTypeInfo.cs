﻿using System;
using System.IO;
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
    public abstract class CarTypeInfo : ACollectable, IAssembly
    {
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
        public int Index { get; set; }

        /// <summary>
        /// Represents usage type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        public eUsageType UsageType { get; set; }

        /// <summary>
        /// Represents boolean as an int of whether <see cref="CarTypeInfo"/> is skinnable.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
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
        public float HeadlightFOV { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte PadHighPerformance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte NumAvailableSkinNumbers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte WhatGame { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte ConvertibleFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte WheelOuterRadius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte WheelInnerRadiusMin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte WheelInnerRadiusMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float HeadlightPositionX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float HeadlightPositionY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float HeadlightPositionZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float HeadlightPositionW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float DriverRenderingOffsetX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float DriverRenderingOffsetY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float DriverRenderingOffsetZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float DriverRenderingOffsetW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float SteeringWheelRenderingX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float SteeringWheelRenderingY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float SteeringWheelRenderingZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float SteeringWheelRenderingW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte MaxInstances1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte MaxInstances2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte MaxInstances3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte MaxInstances4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte MaxInstances5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte KeepLoaded1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte KeepLoaded2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte KeepLoaded3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte KeepLoaded4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte KeepLoaded5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float MinTimeBetweenUses1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float MinTimeBetweenUses2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float MinTimeBetweenUses3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float MinTimeBetweenUses4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public float MinTimeBetweenUses5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers01 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers02 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers03 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers04 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers05 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers06 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers07 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers08 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers09 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        public byte AvailableSkinNumbers10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
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
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}