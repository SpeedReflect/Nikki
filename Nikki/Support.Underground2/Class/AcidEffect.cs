using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.GenParts;
using Nikki.Support.Underground2.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="AcidEffect"/> is a collection of vectors and attributes related to acids and xenons.
    /// </summary>
    public class AcidEffect : Shared.Class.AcidEffect
    {
        #region Fields

        private string _collection_name;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = 0x3F;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0x90;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0xD0;

        /// <summary>
        /// Constant value used in the header.
        /// </summary>
        protected override int Localizer => 0xB;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
        public override GameINT GameINT => GameINT.Underground2;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
		[Browsable(false)]
        public override string GameSTR => GameINT.Underground2.ToString();

        /// <summary>
        /// Manager to which the class belongs to.
        /// </summary>
		[Browsable(false)]
        public AcidEffectManager Manager { get; set; }

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
        /// Emitter name of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string EmitterName { get; set; } = String.Empty;

        /// <summary>
        /// State of this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public int State { get; set; }

        /// <summary>
        /// Section number of this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public ushort SectionNumber { get; set; }

        /// <summary>
        /// Scenery barrier group to which this <see cref="AcidEffect"/> belongs to.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string SceneryBarrierGroup { get; set; }

        /// <summary>
        /// LocalWorld <see cref="Matrix"/> of this <see cref="AcidEffect"/>.
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix Transform { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="AcidEffect"/>.
        /// </summary>
        public AcidEffect() => this.Transform = new Matrix();

        /// <summary>
        /// Initializes new instance of <see cref="AcidEffect"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="AcidEffectManager"/> to which this instance belongs to.</param>
        public AcidEffect(string CName, AcidEffectManager manager) : this()
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="AcidEffect"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="AcidEffectManager"/> to which this instance belongs to.</param>
        public AcidEffect(BinaryReader br, AcidEffectManager manager) : this()
        {
            this.Manager = manager;
            this.Disassemble(br);
            this.CollectionName.BinHash();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="AcidEffect"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="AcidEffect"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            bw.WriteBytes(0, 0x08);
            bw.Write(this.BinKey);
            bw.Write(this.EmitterName.BinHash());
            bw.Write(this.State);
            bw.WriteBytes(0, 0x16);
            bw.Write(this.SectionNumber);
            bw.Write(this.SceneryBarrierGroup.BinHash());
            bw.WriteBytes(0, 0x10);
            this.Transform.Write(bw);
            bw.WriteBytes(0, 0x10);
            bw.WriteNullTermUTF8(this._collection_name, 0x40);
        }

        /// <summary>
        /// Disassembles array into <see cref="AcidEffect"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="AcidEffect"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x0C;
            this.EmitterName = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.State = br.ReadInt32();
            br.BaseStream.Position += 0x16;
            this.SectionNumber = br.ReadUInt16();
            this.SceneryBarrierGroup = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            br.BaseStream.Position += 0x10;
            this.Transform.Read(br);
            br.BaseStream.Position += 0x10;
            this._collection_name = br.ReadNullTermUTF8(0x40);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new AcidEffect(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="AcidEffect"/> 
        /// as a string value.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return $"Collection Name: {this.CollectionName} | " +
                   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public override void Serialize(BinaryWriter bw)
        {
            byte[] array;
            using (var ms = new MemoryStream(0x80 + this.CollectionName.Length))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);
                writer.WriteNullTermUTF8(this.EmitterName);
                writer.WriteNullTermUTF8(this.SceneryBarrierGroup);
                writer.Write(this.State);
                writer.Write(this.SectionNumber);
                this.Transform.Write(writer);

                array = ms.ToArray();

            }

            array = Interop.Compress(array, LZCompressionType.RAWW);

            var header = new SerializationHeader(array.Length, this.GameINT, this.Manager.Name);
            header.Write(bw);
            bw.Write(array.Length);
            bw.Write(array);
        }

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Deserialize(BinaryReader br)
        {
            int size = br.ReadInt32();
            var array = br.ReadBytes(size);

            array = Interop.Decompress(array);

            using var ms = new MemoryStream(array);
            using var reader = new BinaryReader(ms);

            this._collection_name = reader.ReadNullTermUTF8();
            this.EmitterName = reader.ReadNullTermUTF8();
            this.SceneryBarrierGroup = reader.ReadNullTermUTF8();
            this.State = reader.ReadInt32();
            this.SectionNumber = reader.ReadUInt16();
            this.Transform.Read(reader);
        }

        #endregion
    }
}