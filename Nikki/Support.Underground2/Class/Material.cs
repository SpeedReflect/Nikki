using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="Material"/> is a collection of float attributes of shaders and materials.
    /// </summary>
    public class Material : Shared.Class.Material
    {
        #region Fields

        private string _collection_name;
        private const uint _class_key = 0x0041440F;

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
		public override GameINT GameINT => GameINT.Underground2;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Underground2.ToString();

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Underground2 Database { get; set; }

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
        /// Level value of the third strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor3Level { get; set; }

        /// <summary>
        /// Red value of the third strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor3Red { get; set; }

        /// <summary>
        /// Green value of the third strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor3Green { get; set; }

        /// <summary>
        /// Blue value of the third strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor3Blue { get; set; }

        /// <summary>
        /// Level value of the fourth strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor4Level { get; set; }

        /// <summary>
        /// Red value of the fourth strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor4Red { get; set; }

        /// <summary>
        /// Green value of the fourth strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor4Green { get; set; }

        /// <summary>
        /// Blue value of the fourth strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float StrongColor4Blue { get; set; }

        /// <summary>
        /// Ratio between first and second strong colors of the material
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Strong1to2Ratio { get; set; }

        /// <summary>
        /// Ratio between third and fourth strong colors of the material
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float Strong3to4Ratio { get; set; }

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
        /// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
        public Material(string CName, Database.Underground2 db)
        {
            this.Database = db;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
        public unsafe Material(BinaryReader br, Database.Underground2 db)
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
            bw.Write((int)0xA0);
            bw.Write(_class_key);
            bw.Write(_Localizer);
            bw.Write(_Localizer);
            bw.Write(this.BinKey);
            bw.Write(_Localizer);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x1C);

            // Write all settings
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
            bw.Write(this.Strong1to2Ratio);
            bw.Write(this.StrongColor1Level);
            bw.Write(this.StrongColor1Red);
            bw.Write(this.StrongColor1Green);
            bw.Write(this.StrongColor1Blue);
            bw.Write(this.StrongColor2Level);
            bw.Write(this.StrongColor2Red);
            bw.Write(this.StrongColor2Green);
            bw.Write(this.StrongColor2Blue);
            bw.Write(this.Strong3to4Ratio);
            bw.Write(this.StrongColor3Level);
            bw.Write(this.StrongColor3Red);
            bw.Write(this.StrongColor3Green);
            bw.Write(this.StrongColor3Blue);
            bw.Write(this.StrongColor4Level);
            bw.Write(this.StrongColor4Red);
            bw.Write(this.StrongColor4Green);
            bw.Write(this.StrongColor4Blue);
        }

        /// <summary>
        /// Disassembles array into <see cref="Material"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Material"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x14;
            this._collection_name = br.ReadNullTermUTF8(0x1C);

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
            this.Strong1to2Ratio = br.ReadSingle();
            this.StrongColor1Level = br.ReadSingle();
            this.StrongColor1Red = br.ReadSingle();
            this.StrongColor1Green = br.ReadSingle();
            this.StrongColor1Blue = br.ReadSingle();
            this.StrongColor2Level = br.ReadSingle();
            this.StrongColor2Red = br.ReadSingle();
            this.StrongColor2Green = br.ReadSingle();
            this.StrongColor2Blue = br.ReadSingle();
            this.Strong3to4Ratio = br.ReadSingle();
            this.StrongColor3Level = br.ReadSingle();
            this.StrongColor3Red = br.ReadSingle();
            this.StrongColor3Green = br.ReadSingle();
            this.StrongColor3Blue = br.ReadSingle();
            this.StrongColor4Level = br.ReadSingle();
            this.StrongColor4Red = br.ReadSingle();
            this.StrongColor4Green = br.ReadSingle();
            this.StrongColor4Blue = br.ReadSingle();
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
                BrightColor1Level = this.BrightColor1Level,
                BrightColor1Red = this.BrightColor1Red,
                BrightColor1Green = this.BrightColor1Green,
                BrightColor1Blue = this.BrightColor1Blue,
                BrightColor2Level = this.BrightColor2Level,
                BrightColor2Red = this.BrightColor2Red,
                BrightColor2Green = this.BrightColor2Green,
                BrightColor2Blue = this.BrightColor2Blue,
                AlphaValue1 = this.AlphaValue1,
                AlphaValue2 = this.AlphaValue2,
                Strong1to2Ratio = this.Strong1to2Ratio,
                StrongColor1Level = this.StrongColor1Level,
                StrongColor1Red = this.StrongColor1Red,
                StrongColor1Green = this.StrongColor1Green,
                StrongColor1Blue = this.StrongColor1Blue,
                StrongColor2Level = this.StrongColor2Level,
                StrongColor2Red = this.StrongColor2Red,
                StrongColor2Green = this.StrongColor2Green,
                StrongColor2Blue = this.StrongColor2Blue,
                Strong3to4Ratio = this.Strong3to4Ratio,
                StrongColor3Level = this.StrongColor3Level,
                StrongColor3Red = this.StrongColor3Red,
                StrongColor3Green = this.StrongColor3Green,
                StrongColor3Blue = this.StrongColor3Blue,
                StrongColor4Level = this.StrongColor4Level,
                StrongColor4Red = this.StrongColor4Red,
                StrongColor4Green = this.StrongColor4Green,
                StrongColor4Blue = this.StrongColor4Blue,
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