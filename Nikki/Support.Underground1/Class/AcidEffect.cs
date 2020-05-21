using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Class
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
        public override GameINT GameINT => GameINT.Underground1;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Underground1.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Underground1 Database { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                if (value.Length > MaxCNameLength)
                    throw new ArgumentLengthException(MaxCNameLength);
                if (this.Database.AcidEffects.FindCollection(value) != null)
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
        /// Class key of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public uint ClassKey { get; set; }

        /// <summary>
        /// Flags used by the effect's properties.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public uint Flags { get; set; }

        /// <summary>
        /// Number of emmiters in this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public ushort NumEmitters { get; set; }

        /// <summary>
        /// Section number of this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public ushort SectionNumber { get; set; }

        /// <summary>
        /// Key of the collection from which this <see cref="AcidEffect"/> is inherited.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public string InheritanceKey { get; set; } = String.Empty;

        /// <summary>
        /// Far clip value.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float FarClip { get; set; }

        /// <summary>
        /// Intensity of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Intensity { get; set; }

        /// <summary>
        /// Last position X of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LastPositionX { get; set; }

        /// <summary>
        /// Last position Y of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LastPositionY { get; set; }

        /// <summary>
        /// Last position Z of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LastPositionZ { get; set; }

        /// <summary>
        /// Last position W of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LastPositionW { get; set; }

        /// <summary>
        /// Number of particle frames in this <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public uint NumZeroParticleFrames { get; set; }

        /// <summary>
        /// Time stamp of this <see cref="AcidEffect"/> creation.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public uint CreationTimeStamp { get; set; }

        /// <summary>
        /// Value of Matrix 1-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec1_X { get; set; }

        /// <summary>
        /// Value of Matrix 1-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec1_Y { get; set; }

        /// <summary>
        /// Value of Matrix 1-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec1_Z { get; set; }

        /// <summary>
        /// Value of Matrix 1-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec1_W { get; set; }

        /// <summary>
        /// Value of Matrix 2-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec2_X { get; set; }

        /// <summary>
        /// Value of Matrix 2-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec2_Y { get; set; }

        /// <summary>
        /// Value of Matrix 2-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec2_Z { get; set; }

        /// <summary>
        /// Value of Matrix 2-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec2_W { get; set; }

        /// <summary>
        /// Value of Matrix 3-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec3_X { get; set; }

        /// <summary>
        /// Value of Matrix 3-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec3_Y { get; set; }

        /// <summary>
        /// Value of Matrix 3-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec3_Z { get; set; }

        /// <summary>
        /// Value of Matrix 3-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec3_W { get; set; }

        /// <summary>
        /// Value of Matrix 4-X of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec4_X { get; set; }

        /// <summary>
        /// Value of Matrix 4-Y of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec4_Y { get; set; }

        /// <summary>
        /// Value of Matrix 4-Z of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LocalWorld_Vec4_Z { get; set; }

        /// <summary>
        /// Value of Matrix 4-W of the <see cref="AcidEffect"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
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
        /// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
        public AcidEffect(string CName, Database.Underground1 db)
        {
            this.Database = db;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="AcidEffect"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
        public unsafe AcidEffect(BinaryReader br, Database.Underground1 db)
        {
            this.Database = db;
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
        public override ACollectable MemoryCast(string CName)
        {
            var result = new AcidEffect(CName, this.Database)
            {
                ClassKey = this.ClassKey,
                Flags = this.Flags,
                NumEmitters = this.NumEmitters,
                SectionNumber = this.SectionNumber,
                LocalWorld_Vec1_X = this.LocalWorld_Vec1_X,
                LocalWorld_Vec1_Y = this.LocalWorld_Vec1_Y,
                LocalWorld_Vec1_Z = this.LocalWorld_Vec1_Z,
                LocalWorld_Vec1_W = this.LocalWorld_Vec1_W,
                LocalWorld_Vec2_X = this.LocalWorld_Vec2_X,
                LocalWorld_Vec2_Y = this.LocalWorld_Vec2_Y,
                LocalWorld_Vec2_Z = this.LocalWorld_Vec2_Z,
                LocalWorld_Vec2_W = this.LocalWorld_Vec2_W,
                LocalWorld_Vec3_X = this.LocalWorld_Vec3_X,
                LocalWorld_Vec3_Y = this.LocalWorld_Vec3_Y,
                LocalWorld_Vec3_Z = this.LocalWorld_Vec3_Z,
                LocalWorld_Vec3_W = this.LocalWorld_Vec3_W,
                LocalWorld_Vec4_X = this.LocalWorld_Vec4_X,
                LocalWorld_Vec4_Y = this.LocalWorld_Vec4_Y,
                LocalWorld_Vec4_Z = this.LocalWorld_Vec4_Z,
                LocalWorld_Vec4_W = this.LocalWorld_Vec4_W,
                InheritanceKey = this.InheritanceKey,
                FarClip = this.FarClip,
                Intensity = this.Intensity,
                LastPositionX = this.LastPositionX,
                LastPositionY = this.LastPositionY,
                LastPositionZ = this.LastPositionZ,
                LastPositionW = this.LastPositionW,
                NumZeroParticleFrames = this.NumZeroParticleFrames,
                CreationTimeStamp = this.CreationTimeStamp
            };

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
                   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
        }

        #endregion
    }
}