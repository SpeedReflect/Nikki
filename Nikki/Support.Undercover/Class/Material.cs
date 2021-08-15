using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Undercover.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Undercover.Class
{
    /// <summary>
    /// <see cref="Material"/> is a collection of float attributes of shaders and materials.
    /// </summary>
    public class Material : Shared.Class.Material
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
        public const int CNameOffsetAt = 0x1C;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0x124;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
        public override GameINT GameINT => GameINT.Undercover;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.Undercover.ToString();

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
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffusePower { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseClamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseFlakes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseVinylScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMaxBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseMinAlpha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseMaxAlpha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1Power { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1Flakes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1VinylScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MinRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MinGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MinBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MaxRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MaxGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular1")]
        public float Specular1MaxBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2Power { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2Flakes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2VinylScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MinRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MinGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MinBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MaxRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MaxGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Specular2")]
        public float Specular2MaxBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapPower { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapClamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapVinylScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxRed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxGreen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxBlue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float VinylLuminanceMinLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float VinylLuminanceMaxLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float MultiTextured { get; set; }

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
        public unsafe Material(BinaryReader br, MaterialManager manager)
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
            bw.Write((int)0x11C);
            bw.Write(_Unknown0);
            bw.Write(_Localizer);
            bw.Write(_Localizer);
            bw.Write(this.BinKey);
            bw.Write(_Localizer);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x40);

            // Write all settings
            bw.Write(this.DiffusePower);
            bw.Write(this.DiffuseClamp);
            bw.Write(this.DiffuseFlakes);
            bw.Write(this.DiffuseVinylScale);
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
            bw.Write(this.Specular1Power);
            bw.Write(this.Specular1Flakes);
            bw.Write(this.Specular1VinylScale);
            bw.Write(this.Specular1MinLevel);
            bw.Write(this.Specular1MinRed);
            bw.Write(this.Specular1MinGreen);
            bw.Write(this.Specular1MinBlue);
            bw.Write(this.Specular1MaxLevel);
            bw.Write(this.Specular1MaxRed);
            bw.Write(this.Specular1MaxGreen);
            bw.Write(this.Specular1MaxBlue);
            bw.Write(this.Specular2Power);
            bw.Write(this.Specular2Flakes);
            bw.Write(this.Specular2VinylScale);
            bw.Write(this.Specular2MinLevel);
            bw.Write(this.Specular2MinRed);
            bw.Write(this.Specular2MinGreen);
            bw.Write(this.Specular2MinBlue);
            bw.Write(this.Specular2MaxLevel);
            bw.Write(this.Specular2MaxRed);
            bw.Write(this.Specular2MaxGreen);
            bw.Write(this.Specular2MaxBlue);
            bw.Write(this.EnvmapPower);
            bw.Write(this.EnvmapClamp);
            bw.Write(this.EnvmapVinylScale);
            bw.Write(this.EnvmapMinLevel);
            bw.Write(this.EnvmapMinRed);
            bw.Write(this.EnvmapMinGreen);
            bw.Write(this.EnvmapMinBlue);
            bw.Write(this.EnvmapMaxLevel);
            bw.Write(this.EnvmapMaxRed);
            bw.Write(this.EnvmapMaxGreen);
            bw.Write(this.EnvmapMaxBlue);
            bw.Write(this.VinylLuminanceMinLevel);
            bw.Write(this.VinylLuminanceMaxLevel);
            bw.Write(this.MultiTextured);
        }

        /// <summary>
        /// Disassembles array into <see cref="Material"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Material"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x1C;
            this._collection_name = br.ReadNullTermUTF8(0x40);

            this.DiffusePower = br.ReadSingle();
            this.DiffuseClamp = br.ReadSingle();
            this.DiffuseFlakes = br.ReadSingle();
            this.DiffuseVinylScale = br.ReadSingle();
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
            this.Specular1Power = br.ReadSingle();
            this.Specular1Flakes = br.ReadSingle();
            this.Specular1VinylScale = br.ReadSingle();
            this.Specular1MinLevel = br.ReadSingle();
            this.Specular1MinRed = br.ReadSingle();
            this.Specular1MinGreen = br.ReadSingle();
            this.Specular1MinBlue = br.ReadSingle();
            this.Specular1MaxLevel = br.ReadSingle();
            this.Specular1MaxRed = br.ReadSingle();
            this.Specular1MaxGreen = br.ReadSingle();
            this.Specular1MaxBlue = br.ReadSingle();
            this.Specular2Power = br.ReadSingle();
            this.Specular2Flakes = br.ReadSingle();
            this.Specular2VinylScale = br.ReadSingle();
            this.Specular2MinLevel = br.ReadSingle();
            this.Specular2MinRed = br.ReadSingle();
            this.Specular2MinGreen = br.ReadSingle();
            this.Specular2MinBlue = br.ReadSingle();
            this.Specular2MaxLevel = br.ReadSingle();
            this.Specular2MaxRed = br.ReadSingle();
            this.Specular2MaxGreen = br.ReadSingle();
            this.Specular2MaxBlue = br.ReadSingle();
            this.EnvmapPower = br.ReadSingle();
            this.EnvmapClamp = br.ReadSingle();
            this.EnvmapVinylScale = br.ReadSingle();
            this.EnvmapMinLevel = br.ReadSingle();
            this.EnvmapMinRed = br.ReadSingle();
            this.EnvmapMinGreen = br.ReadSingle();
            this.EnvmapMinBlue = br.ReadSingle();
            this.EnvmapMaxLevel = br.ReadSingle();
            this.EnvmapMaxRed = br.ReadSingle();
            this.EnvmapMaxGreen = br.ReadSingle();
            this.EnvmapMaxBlue = br.ReadSingle();
            this.VinylLuminanceMinLevel = br.ReadSingle();
            this.VinylLuminanceMaxLevel = br.ReadSingle();
            this.MultiTextured = br.ReadSingle();
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
            using (var ms = new MemoryStream(0x120 + this.CollectionName.Length))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);
                writer.Write(this.DiffusePower);
                writer.Write(this.DiffuseClamp);
                writer.Write(this.DiffuseFlakes);
                writer.Write(this.DiffuseVinylScale);
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
                writer.Write(this.Specular1Power);
                writer.Write(this.Specular1Flakes);
                writer.Write(this.Specular1VinylScale);
                writer.Write(this.Specular1MinLevel);
                writer.Write(this.Specular1MinRed);
                writer.Write(this.Specular1MinGreen);
                writer.Write(this.Specular1MinBlue);
                writer.Write(this.Specular1MaxLevel);
                writer.Write(this.Specular1MaxRed);
                writer.Write(this.Specular1MaxGreen);
                writer.Write(this.Specular1MaxBlue);
                writer.Write(this.Specular2Power);
                writer.Write(this.Specular2Flakes);
                writer.Write(this.Specular2VinylScale);
                writer.Write(this.Specular2MinLevel);
                writer.Write(this.Specular2MinRed);
                writer.Write(this.Specular2MinGreen);
                writer.Write(this.Specular2MinBlue);
                writer.Write(this.Specular2MaxLevel);
                writer.Write(this.Specular2MaxRed);
                writer.Write(this.Specular2MaxGreen);
                writer.Write(this.Specular2MaxBlue);
                writer.Write(this.EnvmapPower);
                writer.Write(this.EnvmapClamp);
                writer.Write(this.EnvmapVinylScale);
                writer.Write(this.EnvmapMinLevel);
                writer.Write(this.EnvmapMinRed);
                writer.Write(this.EnvmapMinGreen);
                writer.Write(this.EnvmapMinBlue);
                writer.Write(this.EnvmapMaxLevel);
                writer.Write(this.EnvmapMaxRed);
                writer.Write(this.EnvmapMaxGreen);
                writer.Write(this.EnvmapMaxBlue);
                writer.Write(this.VinylLuminanceMinLevel);
                writer.Write(this.VinylLuminanceMaxLevel);
                writer.Write(this.MultiTextured);

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
            this.DiffusePower = reader.ReadSingle();
            this.DiffuseClamp = reader.ReadSingle();
            this.DiffuseFlakes = reader.ReadSingle();
            this.DiffuseVinylScale = reader.ReadSingle();
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
            this.Specular1Power = reader.ReadSingle();
            this.Specular1Flakes = reader.ReadSingle();
            this.Specular1VinylScale = reader.ReadSingle();
            this.Specular1MinLevel = reader.ReadSingle();
            this.Specular1MinRed = reader.ReadSingle();
            this.Specular1MinGreen = reader.ReadSingle();
            this.Specular1MinBlue = reader.ReadSingle();
            this.Specular1MaxLevel = reader.ReadSingle();
            this.Specular1MaxRed = reader.ReadSingle();
            this.Specular1MaxGreen = reader.ReadSingle();
            this.Specular1MaxBlue = reader.ReadSingle();
            this.Specular2Power = reader.ReadSingle();
            this.Specular2Flakes = reader.ReadSingle();
            this.Specular2VinylScale = reader.ReadSingle();
            this.Specular2MinLevel = reader.ReadSingle();
            this.Specular2MinRed = reader.ReadSingle();
            this.Specular2MinGreen = reader.ReadSingle();
            this.Specular2MinBlue = reader.ReadSingle();
            this.Specular2MaxLevel = reader.ReadSingle();
            this.Specular2MaxRed = reader.ReadSingle();
            this.Specular2MaxGreen = reader.ReadSingle();
            this.Specular2MaxBlue = reader.ReadSingle();
            this.EnvmapPower = reader.ReadSingle();
            this.EnvmapClamp = reader.ReadSingle();
            this.EnvmapVinylScale = reader.ReadSingle();
            this.EnvmapMinLevel = reader.ReadSingle();
            this.EnvmapMinRed = reader.ReadSingle();
            this.EnvmapMinGreen = reader.ReadSingle();
            this.EnvmapMinBlue = reader.ReadSingle();
            this.EnvmapMaxLevel = reader.ReadSingle();
            this.EnvmapMaxRed = reader.ReadSingle();
            this.EnvmapMaxGreen = reader.ReadSingle();
            this.EnvmapMaxBlue = reader.ReadSingle();
            this.VinylLuminanceMinLevel = reader.ReadSingle();
            this.VinylLuminanceMaxLevel = reader.ReadSingle();
            this.MultiTextured = reader.ReadSingle();
        }

        #endregion
    }
}