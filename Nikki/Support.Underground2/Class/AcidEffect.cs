using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="AcidEffect"/> is a collection of vectors and attributes related to acids and xenons.
    /// </summary>
    public partial class AcidEffect : Shared.Class.AcidEffect
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
        /// Value of Matrix 1-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec1_X { get; set; }

        /// <summary>
        /// Value of Matrix 1-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec1_Y { get; set; }

        /// <summary>
        /// Value of Matrix 1-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec1_Z { get; set; }

        /// <summary>
        /// Value of Matrix 1-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec1_W { get; set; }

        /// <summary>
        /// Value of Matrix 2-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec2_X { get; set; }

        /// <summary>
        /// Value of Matrix 2-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec2_Y { get; set; }

        /// <summary>
        /// Value of Matrix 2-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec2_Z { get; set; }

        /// <summary>
        /// Value of Matrix 2-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec2_W { get; set; }

        /// <summary>
        /// Value of Matrix 3-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec3_X { get; set; }

        /// <summary>
        /// Value of Matrix 3-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec3_Y { get; set; }

        /// <summary>
        /// Value of Matrix 3-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec3_Z { get; set; }

        /// <summary>
        /// Value of Matrix 3-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec3_W { get; set; }

        /// <summary>
        /// Value of Matrix 4-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec4_X { get; set; }

        /// <summary>
        /// Value of Matrix 4-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec4_Y { get; set; }

        /// <summary>
        /// Value of Matrix 4-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec4_Z { get; set; }

        /// <summary>
        /// Value of Matrix 4-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float LocalWorld_Vec4_W { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="AcidEffect"/>.
        /// </summary>
        public AcidEffect() { }

        /// <summary>
        /// Initializes new instance of <see cref="AcidEffect"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="AcidEffectManager"/> to which this instance belongs to.</param>
        public AcidEffect(string CName, AcidEffectManager manager)
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
        public AcidEffect(BinaryReader br, AcidEffectManager manager)
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
            bw.Write(_Localizer);
            bw.Write(_Localizer);
            bw.Write(this.BinKey);
            bw.Write(this.BinKey);
            bw.Write(this.ClassKey);
            bw.Write((int)0);
            bw.Write(this.Flags);
            bw.Write(this.NumEmitters);
            bw.Write(this.SectionNumber);
            bw.Write(this.LocalWorld_Vec1_X);
            bw.Write(this.LocalWorld_Vec1_Y);
            bw.Write(this.LocalWorld_Vec1_Z);
            bw.Write(this.LocalWorld_Vec1_W);
            bw.Write(this.LocalWorld_Vec2_X);
            bw.Write(this.LocalWorld_Vec2_Y);
            bw.Write(this.LocalWorld_Vec2_Z);
            bw.Write(this.LocalWorld_Vec2_W);
            bw.Write(this.LocalWorld_Vec3_X);
            bw.Write(this.LocalWorld_Vec3_Y);
            bw.Write(this.LocalWorld_Vec3_Z);
            bw.Write(this.LocalWorld_Vec3_W);
            bw.Write(this.LocalWorld_Vec4_X);
            bw.Write(this.LocalWorld_Vec4_Y);
            bw.Write(this.LocalWorld_Vec4_Z);
            bw.Write(this.LocalWorld_Vec4_W);
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
            this.LocalWorld_Vec1_X = br.ReadSingle();
            this.LocalWorld_Vec1_Y = br.ReadSingle();
            this.LocalWorld_Vec1_Z = br.ReadSingle();
            this.LocalWorld_Vec1_W = br.ReadSingle();
            this.LocalWorld_Vec2_X = br.ReadSingle();
            this.LocalWorld_Vec2_Y = br.ReadSingle();
            this.LocalWorld_Vec2_Z = br.ReadSingle();
            this.LocalWorld_Vec2_W = br.ReadSingle();
            this.LocalWorld_Vec3_X = br.ReadSingle();
            this.LocalWorld_Vec3_Y = br.ReadSingle();
            this.LocalWorld_Vec3_Z = br.ReadSingle();
            this.LocalWorld_Vec3_W = br.ReadSingle();
            this.LocalWorld_Vec4_X = br.ReadSingle();
            this.LocalWorld_Vec4_Y = br.ReadSingle();
            this.LocalWorld_Vec4_Z = br.ReadSingle();
            this.LocalWorld_Vec4_W = br.ReadSingle();
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
                bw.Write(this.LocalWorld_Vec1_X);
                bw.Write(this.LocalWorld_Vec1_Y);
                bw.Write(this.LocalWorld_Vec1_Z);
                bw.Write(this.LocalWorld_Vec1_W);
                bw.Write(this.LocalWorld_Vec2_X);
                bw.Write(this.LocalWorld_Vec2_Y);
                bw.Write(this.LocalWorld_Vec2_Z);
                bw.Write(this.LocalWorld_Vec2_W);
                bw.Write(this.LocalWorld_Vec3_X);
                bw.Write(this.LocalWorld_Vec3_Y);
                bw.Write(this.LocalWorld_Vec3_Z);
                bw.Write(this.LocalWorld_Vec3_W);
                bw.Write(this.LocalWorld_Vec4_X);
                bw.Write(this.LocalWorld_Vec4_Y);
                bw.Write(this.LocalWorld_Vec4_Z);
                bw.Write(this.LocalWorld_Vec4_W);
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
            this.LocalWorld_Vec1_X = reader.ReadSingle();
            this.LocalWorld_Vec1_Y = reader.ReadSingle();
            this.LocalWorld_Vec1_Z = reader.ReadSingle();
            this.LocalWorld_Vec1_W = reader.ReadSingle();
            this.LocalWorld_Vec2_X = reader.ReadSingle();
            this.LocalWorld_Vec2_Y = reader.ReadSingle();
            this.LocalWorld_Vec2_Z = reader.ReadSingle();
            this.LocalWorld_Vec2_W = reader.ReadSingle();
            this.LocalWorld_Vec3_X = reader.ReadSingle();
            this.LocalWorld_Vec3_Y = reader.ReadSingle();
            this.LocalWorld_Vec3_Z = reader.ReadSingle();
            this.LocalWorld_Vec3_W = reader.ReadSingle();
            this.LocalWorld_Vec4_X = reader.ReadSingle();
            this.LocalWorld_Vec4_Y = reader.ReadSingle();
            this.LocalWorld_Vec4_Z = reader.ReadSingle();
            this.LocalWorld_Vec4_W = reader.ReadSingle();
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