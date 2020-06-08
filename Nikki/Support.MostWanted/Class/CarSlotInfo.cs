﻿using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Reflection.Enum.SlotID;
using Nikki.Support.MostWanted.Framework;



namespace Nikki.Support.MostWanted.Class
{
    /// <summary>
    /// <see cref="CarSlotInfo"/> is a collection of settings related to car's slot overrides.
    /// </summary>
    public class CarSlotInfo : Shared.Class.CarSlotInfo
    {
        #region Fields

        private string _collection_name;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = -1;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = -1;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0x10;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.MostWanted;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.MostWanted.ToString();

        /// <summary>
        /// Manager to which the class belongs to.
        /// </summary>
        public CarSlotInfoManager Manager { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
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
        public override uint BinKey => this._collection_name.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public override uint VltKey => this._collection_name.VltHash();

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="CarSlotInfo"/>.
        /// </summary>
        public CarSlotInfo() { }

        /// <summary>
        /// Initializes new instance of <see cref="CarSlotInfo"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="CarSlotInfoManager"/> to which this instance belongs to.</param>
        public CarSlotInfo(string CName, CarSlotInfoManager manager)
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="CarSlotInfo"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="CarSlotInfoManager"/> to which this instance belongs to.</param>
        public CarSlotInfo(BinaryReader br, CarSlotInfoManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~CarSlotInfo() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="SlotType"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SlotType"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            var keys = this._collection_name.Split("_PART_", 2, StringSplitOptions.None);
            var id = (eSlotMostWanted)Enum.Parse(typeof(eSlotMostWanted), keys[1]);

            bw.Write(keys[0].BinHash());
            bw.Write((int)id);
            bw.Write(keys[0].BinHash());
            bw.Write(this.InfoMainOverride.BinHash());
        }

        /// <summary>
        /// Disassembles array into <see cref="SlotType"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="SlotType"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            var key = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            var id = (eSlotMostWanted)br.ReadInt32();
            br.BaseStream.Position += 4;
            this._collection_name = $"{key}_PART_{id}";

            this.InfoMainOverride = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new CarSlotInfo(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="CarSlotInfo"/> 
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
