using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Class
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
        public const int MaxCNameLength = 0x1B;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0x1C;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0xB0;

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
        /// Database to which the class belongs to.
        /// </summary>
        public Database.MostWanted Database { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                if (value.Length > MaxCNameLength)
                    throw new ArgumentLengthException(MaxCNameLength);
                if (this.Database.Materials.FindCollection(value) != null)
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
        /// Level value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMinLevel { get; set; }

        /// <summary>
        /// Red value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMinRed { get; set; }

        /// <summary>
        /// Green value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMinGreen { get; set; }

        /// <summary>
        /// Blue value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMinBlue { get; set; }

        /// <summary>
        /// Level value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMaxLevel { get; set; }

        /// <summary>
        /// Red value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMaxRed { get; set; }

        /// <summary>
        /// Green value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMaxGreen { get; set; }

        /// <summary>
        /// Blue value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMaxBlue { get; set; }

        /// <summary>
        /// Linear negativity of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float EnvmapMinPower { get; set; }

        /// <summary>
        /// Gradient negativity of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float EnvmapMaxPower { get; set; }

        /// <summary>
        /// Shadow level value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float ClampLevel { get; set; }

        /// <summary>
        /// Chrome level value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float VinylScaleLevel { get; set; }

        /// <summary>
        /// Matte level value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float FlakesLevel { get; set; }

        /// <summary>
        /// First reflection value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMaxAlpha { get; set; }

        /// <summary>
        /// Second reflection value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularPower { get; set; }

        /// <summary>
        /// Third reflection value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMinLevel { get; set; }

        /// <summary>
        /// Level value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMaxLevel { get; set; }

        /// <summary>
        /// Red value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMaxRed { get; set; }

        /// <summary>
        /// Green value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMaxGreen { get; set; }

        /// <summary>
        /// Blue value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMaxBlue { get; set; }

        /// <summary>
        /// Alpha value of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DiffuseMinAlpha { get; set; }

        /// <summary>
        /// Unknown 1 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMinRed { get; set; }

        /// <summary>
        /// Unknown 2 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMinGreen { get; set; }

        /// <summary>
        /// Unknown 3 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float SpecularMinBlue { get; set; }

        /// <summary>
        /// Unknown 4 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown0 { get; set; }

        /// <summary>
        /// Unknown 5 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown1 { get; set; }

        /// <summary>
        /// Unknown 6 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown2 { get; set; }

        /// <summary>
        /// Unknown 7 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown3 { get; set; }

        /// <summary>
        /// Unknown 8 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown4 { get; set; }

        /// <summary>
        /// Unknown 9 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown5 { get; set; }

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
        /// <param name="db"><see cref="Database.MostWanted"/> to which this instance belongs to.</param>
        public Material(string CName, Database.MostWanted db)
        {
            this.Database = db;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="db"><see cref="Database.MostWanted"/> to which this instance belongs to.</param>
        public unsafe Material(BinaryReader br, Database.MostWanted db)
        {
            this.Database = db;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~Material() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="Material"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Material"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write header of the material
            bw.Write(Global.Materials);
            bw.Write((int)0xA8);
            bw.Write(_Unknown1);
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
            bw.Write(this.ClampLevel);
            bw.Write(this.FlakesLevel);
            bw.Write(this.VinylScaleLevel);
            bw.Write(this.Unknown0);
            bw.Write(this.Unknown1);
            bw.Write(this.EnvmapMinPower);
            bw.Write(this.EnvmapMaxPower);
            bw.Write(this.Unknown2);
            bw.Write(this.Unknown3);
            bw.Write(this.Unknown4);
            bw.Write(this.Unknown5);
        }

        /// <summary>
        /// Disassembles array into <see cref="Material"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Material"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x14;
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
            this.ClampLevel = br.ReadSingle();
            this.FlakesLevel = br.ReadSingle();
            this.VinylScaleLevel = br.ReadSingle();
            this.Unknown0 = br.ReadSingle();
            this.Unknown1 = br.ReadSingle();
            this.EnvmapMinPower = br.ReadSingle();
            this.EnvmapMaxPower = br.ReadSingle();
            this.Unknown2 = br.ReadSingle();
            this.Unknown3 = br.ReadSingle();
            this.Unknown4 = br.ReadSingle();
            this.Unknown5 = br.ReadSingle();
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            var result = new Material(CName, this.Database)
            {
                DiffuseMinLevel = this.DiffuseMinLevel,
                DiffuseMinRed = this.DiffuseMinRed,
                DiffuseMinGreen = this.DiffuseMinGreen,
                DiffuseMinBlue = this.DiffuseMinBlue,
                DiffuseMaxLevel = this.DiffuseMaxLevel,
                DiffuseMaxRed = this.DiffuseMaxRed,
                DiffuseMaxGreen = this.DiffuseMaxGreen,
                DiffuseMaxBlue = this.DiffuseMaxBlue,
                DiffuseMinAlpha = this.DiffuseMinAlpha,
                DiffuseMaxAlpha = this.DiffuseMaxAlpha,
                SpecularPower = this.SpecularPower,
                SpecularMinLevel = this.SpecularMinLevel,
                SpecularMaxLevel = this.SpecularMaxLevel,
                SpecularMaxRed = this.SpecularMaxRed,
                SpecularMaxGreen = this.SpecularMaxGreen,
                SpecularMaxBlue = this.SpecularMaxBlue,
                ClampLevel = this.ClampLevel,
                FlakesLevel = this.FlakesLevel,
                VinylScaleLevel = this.VinylScaleLevel,
                EnvmapMinPower = this.EnvmapMinPower,
                EnvmapMaxPower = this.EnvmapMaxPower,
                SpecularMinRed = this.SpecularMinRed,
                SpecularMinGreen = this.SpecularMinGreen,
                SpecularMinBlue = this.SpecularMinBlue,
                Unknown0 = this.Unknown0,
                Unknown1 = this.Unknown1,
                Unknown2 = this.Unknown2,
                Unknown3 = this.Unknown3,
                Unknown4 = this.Unknown4,
                Unknown5 = this.Unknown5
            };

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
                   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
        }

        #endregion
    }
}