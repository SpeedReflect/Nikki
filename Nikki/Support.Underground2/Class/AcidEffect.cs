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
        /// Class key of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public uint ClassKey { get; set; }

        /// <summary>
        /// Flags used by the effect's properties.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public uint Flags { get; set; }

        /// <summary>
        /// Number of emmiters in this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public ushort NumEmitters { get; set; }

        /// <summary>
        /// Section number of this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public ushort SectionNumber { get; set; }

        /// <summary>
        /// Key of the collection from which this <see cref="AcidEffect"/> is inherited.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string InheritanceKey { get; set; } = String.Empty;

        /// <summary>
        /// Far clip value.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float FarClip { get; set; }

        /// <summary>
        /// Intensity of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float Intensity { get; set; }

        /// <summary>
        /// Last position X of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LastPositionX { get; set; }

        /// <summary>
        /// Last position Y of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LastPositionY { get; set; }

        /// <summary>
        /// Last position Z of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LastPositionZ { get; set; }

        /// <summary>
        /// Last position W of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LastPositionW { get; set; }

        /// <summary>
        /// Number of particle frames in this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public uint NumZeroParticleFrames { get; set; }

        /// <summary>
        /// Time stamp of this <see cref="AcidEffect"/> creation.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public uint CreationTimeStamp { get; set; }

        /// <summary>
        /// LocalWorld <see cref="Matrix"/> of this <see cref="AcidEffect"/>.
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix LocalWorld { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="AcidEffect"/>.
        /// </summary>
        public AcidEffect() => this.LocalWorld = new Matrix();

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
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~AcidEffect() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="AcidEffect"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="AcidEffect"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write all settings
            bw.Write(this.Localizer);
            bw.Write(this.Localizer);
            bw.Write(this.BinKey);
            bw.Write(this.BinKey);
            bw.Write(this.ClassKey);
            bw.Write((int)0);
            bw.Write(this.Flags);
            bw.Write(this.NumEmitters);
            bw.Write(this.SectionNumber);
            this.LocalWorld.Write(bw);
            bw.Write(this.InheritanceKey.BinHash());
            bw.Write(this.FarClip);
            bw.Write(this.Intensity);
            bw.Write((int)0);
            bw.Write(this.LastPositionX);
            bw.Write(this.LastPositionY);
            bw.Write(this.LastPositionZ);
            bw.Write(this.LastPositionW);
            bw.Write((int)0);
            bw.Write(this.NumZeroParticleFrames);
            bw.Write(this.CreationTimeStamp);
            bw.Write((int)0);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x40);
        }

        /// <summary>
        /// Disassembles array into <see cref="AcidEffect"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="AcidEffect"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x10;
            this.ClassKey = br.ReadUInt32();
            br.BaseStream.Position += 4;
            this.Flags = br.ReadUInt32();
            this.NumEmitters = br.ReadUInt16();
            this.SectionNumber = br.ReadUInt16();
            this.LocalWorld.Read(br);
            this.InheritanceKey = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.FarClip = br.ReadSingle();
            this.Intensity = br.ReadSingle();
            br.BaseStream.Position += 4;
            this.LastPositionX = br.ReadSingle();
            this.LastPositionY = br.ReadSingle();
            this.LastPositionZ = br.ReadSingle();
            this.LastPositionW = br.ReadSingle();
            br.BaseStream.Position += 4;
            this.NumZeroParticleFrames = br.ReadUInt32();
            this.CreationTimeStamp = br.ReadUInt32();
            br.BaseStream.Position += 4;

            // Read CollectionName
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
                bw.Write(this.ClassKey);
                bw.Write(this.Flags);
                bw.Write(this.NumEmitters);
                bw.Write(this.SectionNumber);
                this.LocalWorld.Write(bw);
                bw.WriteNullTermUTF8(this.InheritanceKey);
                bw.Write(this.FarClip);
                bw.Write(this.Intensity);
                bw.Write(this.LastPositionX);
                bw.Write(this.LastPositionY);
                bw.Write(this.LastPositionZ);
                bw.Write(this.LastPositionW);
                bw.Write(this.NumZeroParticleFrames);
                bw.Write(this.CreationTimeStamp);

                array = ms.ToArray();

            }

            array = Interop.Compress(array, eLZCompressionType.BEST);

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
            this.ClassKey = reader.ReadUInt32();
            this.Flags = reader.ReadUInt32();
            this.NumEmitters = reader.ReadUInt16();
            this.SectionNumber = reader.ReadUInt16();
            this.LocalWorld.Read(br);
            this.InheritanceKey = reader.ReadNullTermUTF8();
            this.FarClip = reader.ReadSingle();
            this.Intensity = reader.ReadSingle();
            this.LastPositionX = reader.ReadSingle();
            this.LastPositionY = reader.ReadSingle();
            this.LastPositionZ = reader.ReadSingle();
            this.LastPositionW = reader.ReadSingle();
            this.NumZeroParticleFrames = reader.ReadUInt32();
            this.CreationTimeStamp = reader.ReadUInt32();
        }

        #endregion
    }
}