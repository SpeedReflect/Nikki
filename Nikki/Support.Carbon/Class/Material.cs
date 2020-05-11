using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Class
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
        public const int BaseClassSize = 0xF4;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Carbon;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Carbon.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Carbon Database { get; set; }

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
        /// Disable value of reflection colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DisableReflection { get; set; }

        /// <summary>
        /// Increment value of reflection colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongerReflection { get; set; }

        /// <summary>
        /// Blend value of strong colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BlendStrongColors { get; set; }

        /// <summary>
        /// Disable value of strong colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float DisableStrongColors { get; set; }

        /// <summary>
        /// Level value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor1Level { get; set; }

        /// <summary>
        /// Red value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor1Red { get; set; }

        /// <summary>
        /// Green value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor1Green { get; set; }

        /// <summary>
        /// Blue value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor1Blue { get; set; }

        /// <summary>
        /// Level value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor2Level { get; set; }

        /// <summary>
        /// Red value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor2Red { get; set; }

        /// <summary>
        /// Green value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor2Green { get; set; }

        /// <summary>
        /// Blue value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float BrightColor2Blue { get; set; }

        /// <summary>
        /// Main grayscale value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Grayscale { get; set; }

        /// <summary>
        /// Linear negativity of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float LinearNegative { get; set; }

        /// <summary>
        /// Gradient negativity of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float GradientNegative { get; set; }

        /// <summary>
        /// Level value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float ReflectionColorLevel { get; set; }

        /// <summary>
        /// Red value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float ReflectionColorRed { get; set; }

        /// <summary>
        /// Green value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float ReflectionColorGreen { get; set; }

        /// <summary>
        /// Blue value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float ReflectionColorBlue { get; set; }

        /// <summary>
        /// Outer radius of the shadow fading.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float ShadowOuterRadius { get; set; }

        /// <summary>
        /// Value of the optimal light reflection on the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float OptimalLightReflection { get; set; }

        /// <summary>
        /// Level value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor1Level { get; set; }

        /// <summary>
        /// Red value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor1Red { get; set; }

        /// <summary>
        /// Green value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor1Green { get; set; }

        /// <summary>
        /// Blue value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor1Blue { get; set; }

        /// <summary>
        /// Level value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor2Level { get; set; }

        /// <summary>
        /// Red value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor2Red { get; set; }

        /// <summary>
        /// Green value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor2Green { get; set; }

        /// <summary>
        /// Blue value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor2Blue { get; set; }

        /// <summary>
        /// First alpha value of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float AlphaValue1 { get; set; }

        /// <summary>
        /// Second alpha value of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float AlphaValue2 { get; set; }

        /// <summary>
        /// Unknown 1 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown1 { get; set; }

        /// <summary>
        /// Unknown 2 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown2 { get; set; }

        /// <summary>
        /// Unknown 3 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown3 { get; set; }

        /// <summary>
        /// Unknown 4 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown4 { get; set; }

        /// <summary>
        /// Unknown 5 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown5 { get; set; }

        /// <summary>
        /// Unknown 6 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown6 { get; set; }

        /// <summary>
        /// Unknown 7 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Unknown7 { get; set; }

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
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public Material(string CName, Database.Carbon db)
        {
            this.Database = db;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
        public unsafe Material(BinaryReader br, Database.Carbon db)
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
            bw.Write((int)0xEC);
            bw.Write(_Unknown0);
            bw.Write(_Localizer);
            bw.Write(_Localizer);
            bw.Write(this.BinKey);
            bw.Write(_Localizer);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x1C);

            // Write all settings
            bw.WriteBytes(0x24);
            bw.Write(this.ShadowOuterRadius);
            bw.Write(this.OptimalLightReflection);
            bw.Write(this.Unknown1);
            bw.Write(this.Grayscale);
            bw.Write(this.BrightColor1Level);
            bw.Write(this.BrightColor1Red);
            bw.Write(this.BrightColor1Green);
            bw.Write(this.BrightColor1Blue);
            bw.Write(this.BrightColor2Level);
            bw.Write(this.BrightColor2Red);
            bw.Write(this.BrightColor2Green);
            bw.Write(this.BrightColor2Blue);
            bw.Write(this.AlphaValue1);
            bw.Write(this.AlphaValue2);
            bw.Write(this.DisableReflection);
            bw.Write(this.Unknown2);
            bw.Write(this.Unknown3);
            bw.Write(this.Unknown4);
            bw.Write(this.Unknown5);
            bw.Write(this.Unknown6);
            bw.Write(this.Unknown7);
            bw.Write(this.ReflectionColorLevel);
            bw.Write(this.ReflectionColorRed);
            bw.Write(this.ReflectionColorGreen);
            bw.Write(this.ReflectionColorBlue);
            bw.Write(this.StrongerReflection);
            bw.Write(this.BlendStrongColors);
            bw.Write(this.DisableStrongColors);
            bw.Write(this.StrongColor1Level);
            bw.Write(this.StrongColor1Red);
            bw.Write(this.StrongColor1Green);
            bw.Write(this.StrongColor1Blue);
            bw.Write(this.StrongColor2Level);
            bw.Write(this.StrongColor2Red);
            bw.Write(this.StrongColor2Green);
            bw.Write(this.StrongColor2Blue);
            bw.Write(this.LinearNegative);
            bw.Write(this.GradientNegative);
        }

        /// <summary>
        /// Disassembles array into <see cref="Material"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Material"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x14;
            this._collection_name = br.ReadNullTermUTF8(0x1C);
            br.BaseStream.Position += 0x24;

            this.ShadowOuterRadius = br.ReadSingle();
            this.OptimalLightReflection = br.ReadSingle();
            this.Unknown1 = br.ReadSingle();
            this.Grayscale = br.ReadSingle();
            this.BrightColor1Level = br.ReadSingle();
            this.BrightColor1Red = br.ReadSingle();
            this.BrightColor1Green = br.ReadSingle();
            this.BrightColor1Blue = br.ReadSingle();
            this.BrightColor2Level = br.ReadSingle();
            this.BrightColor2Red = br.ReadSingle();
            this.BrightColor2Green = br.ReadSingle();
            this.BrightColor2Blue = br.ReadSingle();
            this.AlphaValue1 = br.ReadSingle();
            this.AlphaValue2 = br.ReadSingle();
            this.DisableReflection = br.ReadSingle();
            this.Unknown2 = br.ReadSingle();
            this.Unknown3 = br.ReadSingle();
            this.Unknown4 = br.ReadSingle();
            this.Unknown5 = br.ReadSingle();
            this.Unknown6 = br.ReadSingle();
            this.Unknown7 = br.ReadSingle();
            this.ReflectionColorLevel = br.ReadSingle();
            this.ReflectionColorRed = br.ReadSingle();
            this.ReflectionColorGreen = br.ReadSingle();
            this.ReflectionColorBlue = br.ReadSingle();
            this.StrongerReflection = br.ReadSingle();
            this.BlendStrongColors = br.ReadSingle();
            this.DisableStrongColors = br.ReadSingle();
            this.StrongColor1Level = br.ReadSingle();
            this.StrongColor1Red = br.ReadSingle();
            this.StrongColor1Green = br.ReadSingle();
            this.StrongColor1Blue = br.ReadSingle();
            this.StrongColor2Level = br.ReadSingle();
            this.StrongColor2Red = br.ReadSingle();
            this.StrongColor2Green = br.ReadSingle();
            this.StrongColor2Blue = br.ReadSingle();
            this.LinearNegative = br.ReadSingle();
            this.GradientNegative = br.ReadSingle();
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
                ShadowOuterRadius = this.ShadowOuterRadius,
                OptimalLightReflection = this.OptimalLightReflection,
                DisableReflection = this.DisableReflection,
                StrongerReflection = this.StrongerReflection,
                BlendStrongColors = this.BlendStrongColors,
                DisableStrongColors = this.DisableStrongColors,
                StrongColor1Level = this.StrongColor1Level,
                StrongColor1Red = this.StrongColor1Red,
                StrongColor1Green = this.StrongColor1Green,
                StrongColor1Blue = this.StrongColor1Blue,
                StrongColor2Level = this.StrongColor2Level,
                StrongColor2Red = this.StrongColor2Red,
                StrongColor2Green = this.StrongColor2Green,
                StrongColor2Blue = this.StrongColor2Blue,
                BrightColor2Level = this.BrightColor2Level,
                BrightColor2Red = this.BrightColor2Red,
                BrightColor2Green = this.BrightColor2Green,
                BrightColor2Blue = this.BrightColor2Blue,
                BrightColor1Level = this.BrightColor1Level,
                BrightColor1Red = this.BrightColor1Red,
                BrightColor1Green = this.BrightColor1Green,
                BrightColor1Blue = this.BrightColor1Blue,
                Grayscale = this.Grayscale,
                LinearNegative = this.LinearNegative,
                GradientNegative = this.GradientNegative,
                ReflectionColorLevel = this.ReflectionColorLevel,
                ReflectionColorRed = this.ReflectionColorRed,
                ReflectionColorGreen = this.ReflectionColorGreen,
                ReflectionColorBlue = this.ReflectionColorBlue,
                AlphaValue1 = this.AlphaValue1,
                AlphaValue2 = this.AlphaValue2,
                Unknown1 = this.Unknown1,
                Unknown2 = this.Unknown2,
                Unknown3 = this.Unknown3,
                Unknown4 = this.Unknown4,
                Unknown5 = this.Unknown5,
                Unknown6 = this.Unknown6,
                Unknown7 = this.Unknown7
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