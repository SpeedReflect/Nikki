using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground1.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground1.Class
{
    /// <summary>
    /// <see cref="Material"/> is a collection of float attributes of shaders and materials.
    /// </summary>
    public class Material : Shared.Class.Material
    {
        #region Fields

        private string _collection_name;
        private const uint _class_key = 0x004114C5;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = 0x1B;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0x1C;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0xA8;

        #endregion

        #region Properties

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override GameINT GameINT => GameINT.Underground1;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.Underground1.ToString();

        /// <summary>
        /// Manager to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public MaterialManager Manager { get; set; }

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
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseMinAlpha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseMaxAlpha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float SpecularPower { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float EnvmapPower { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        public Material() { }

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="MaterialManager"/> to which this instance belongs to.</param>
        public Material(string CName, MaterialManager manager)
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="MaterialManager"/> to which this instance belongs to.</param>
        public Material(BinaryReader br, MaterialManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
            this.CollectionName.BinHash();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="Material"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Material"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write header of the material
            bw.WriteEnum(BinBlockID.Materials);
            bw.Write((int)0xA0);
            bw.Write(_class_key);
            bw.Write(_Localizer);
            bw.Write(_Localizer);
            bw.Write(this.BinKey);
            bw.Write(_Localizer);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x1C);

            // Write all settings
            bw.Write(this.DiffuseMinLevel);
            bw.Write(this.DiffuseMinRed);
            bw.Write(this.DiffuseMinGreen);
            bw.Write(this.DiffuseMinBlue);
            bw.Write(this.DiffuseMaxLevel);
            bw.Write(this.DiffuseMaxRed);
            bw.Write(this.DiffuseMaxGreen);
            bw.Write(this.DiffuseMaxBlue);
            bw.Write(this.DiffuseMinAlpha);
            bw.Write(this.DiffuseMaxAlpha);
            bw.Write(this.SpecularPower);
            bw.Write(this.SpecularMinLevel);
            bw.Write(this.SpecularMinRed);
            bw.Write(this.SpecularMinGreen);
            bw.Write(this.SpecularMinBlue);
            bw.Write(this.SpecularMaxLevel);
            bw.Write(this.SpecularMaxRed);
            bw.Write(this.SpecularMaxGreen);
            bw.Write(this.SpecularMaxBlue);
            bw.Write(this.EnvmapPower);
            bw.Write(this.EnvmapMinLevel);
            bw.Write(this.EnvmapMinRed);
            bw.Write(this.EnvmapMinGreen);
            bw.Write(this.EnvmapMinBlue);
            bw.Write(this.EnvmapMaxLevel);
            bw.Write(this.EnvmapMaxRed);
            bw.Write(this.EnvmapMaxGreen);
            bw.Write(this.EnvmapMaxBlue);
        }

        /// <summary>
        /// Disassembles array into <see cref="Material"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Material"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x1C;
            this._collection_name = br.ReadNullTermUTF8(0x1C);

            this.DiffuseMinLevel = br.ReadSingle();
            this.DiffuseMinRed = br.ReadSingle();
            this.DiffuseMinGreen = br.ReadSingle();
            this.DiffuseMinBlue = br.ReadSingle();
            this.DiffuseMaxLevel = br.ReadSingle();
            this.DiffuseMaxRed = br.ReadSingle();
            this.DiffuseMaxGreen = br.ReadSingle();
            this.DiffuseMaxBlue = br.ReadSingle();
            this.DiffuseMinAlpha = br.ReadSingle();
            this.DiffuseMaxAlpha = br.ReadSingle();
            this.SpecularPower = br.ReadSingle();
            this.SpecularMinLevel = br.ReadSingle();
            this.SpecularMinRed = br.ReadSingle();
            this.SpecularMinGreen = br.ReadSingle();
            this.SpecularMinBlue = br.ReadSingle();
            this.SpecularMaxLevel = br.ReadSingle();
            this.SpecularMaxRed = br.ReadSingle();
            this.SpecularMaxGreen = br.ReadSingle();
            this.SpecularMaxBlue = br.ReadSingle();
            this.EnvmapPower = br.ReadSingle();
            this.EnvmapMinLevel = br.ReadSingle();
            this.EnvmapMinRed = br.ReadSingle();
            this.EnvmapMinGreen = br.ReadSingle();
            this.EnvmapMinBlue = br.ReadSingle();
            this.EnvmapMaxLevel = br.ReadSingle();
            this.EnvmapMaxRed = br.ReadSingle();
            this.EnvmapMaxGreen = br.ReadSingle();
            this.EnvmapMaxBlue = br.ReadSingle();
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new Material(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="Material"/> 
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
            using (var ms = new MemoryStream(0x99 + this.CollectionName.Length))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);
                writer.Write(this.DiffuseMinLevel);
                writer.Write(this.DiffuseMinRed);
                writer.Write(this.DiffuseMinGreen);
                writer.Write(this.DiffuseMinBlue);
                writer.Write(this.DiffuseMaxLevel);
                writer.Write(this.DiffuseMaxRed);
                writer.Write(this.DiffuseMaxGreen);
                writer.Write(this.DiffuseMaxBlue);
                writer.Write(this.DiffuseMinAlpha);
                writer.Write(this.DiffuseMaxAlpha);
                writer.Write(this.SpecularPower);
                writer.Write(this.SpecularMinLevel);
                writer.Write(this.SpecularMinRed);
                writer.Write(this.SpecularMinGreen);
                writer.Write(this.SpecularMinBlue);
                writer.Write(this.SpecularMaxLevel);
                writer.Write(this.SpecularMaxRed);
                writer.Write(this.SpecularMaxGreen);
                writer.Write(this.SpecularMaxBlue);
                writer.Write(this.EnvmapPower);
                writer.Write(this.EnvmapMinLevel);
                writer.Write(this.EnvmapMinRed);
                writer.Write(this.EnvmapMinGreen);
                writer.Write(this.EnvmapMinBlue);
                writer.Write(this.EnvmapMaxLevel);
                writer.Write(this.EnvmapMaxRed);
                writer.Write(this.EnvmapMaxGreen);
                writer.Write(this.EnvmapMaxBlue);

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
            this.DiffuseMinLevel = reader.ReadSingle();
            this.DiffuseMinRed = reader.ReadSingle();
            this.DiffuseMinGreen = reader.ReadSingle();
            this.DiffuseMinBlue = reader.ReadSingle();
            this.DiffuseMaxLevel = reader.ReadSingle();
            this.DiffuseMaxRed = reader.ReadSingle();
            this.DiffuseMaxGreen = reader.ReadSingle();
            this.DiffuseMaxBlue = reader.ReadSingle();
            this.DiffuseMinAlpha = reader.ReadSingle();
            this.DiffuseMaxAlpha = reader.ReadSingle();
            this.SpecularPower = reader.ReadSingle();
            this.SpecularMinLevel = reader.ReadSingle();
            this.SpecularMinRed = reader.ReadSingle();
            this.SpecularMinGreen = reader.ReadSingle();
            this.SpecularMinBlue = reader.ReadSingle();
            this.SpecularMaxLevel = reader.ReadSingle();
            this.SpecularMaxRed = reader.ReadSingle();
            this.SpecularMaxGreen = reader.ReadSingle();
            this.SpecularMaxBlue = reader.ReadSingle();
            this.EnvmapPower = reader.ReadSingle();
            this.EnvmapMinLevel = reader.ReadSingle();
            this.EnvmapMinRed = reader.ReadSingle();
            this.EnvmapMinGreen = reader.ReadSingle();
            this.EnvmapMinBlue = reader.ReadSingle();
            this.EnvmapMaxLevel = reader.ReadSingle();
            this.EnvmapMaxRed = reader.ReadSingle();
            this.EnvmapMaxGreen = reader.ReadSingle();
            this.EnvmapMaxBlue = reader.ReadSingle();
        }

        #endregion
    }
}