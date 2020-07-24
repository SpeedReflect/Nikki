using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.GenParts;
using Nikki.Support.Underground2.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="AcidEmitter"/> is a collection of vectors and attributes related to emitters.
    /// </summary>
    public class AcidEmitter : Shared.Class.AcidEmitter
    {
        #region Fields

        private string _collection_name;
        private string _special_effect = String.Empty;
        private string _emitter_name = String.Empty;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = 0x3F;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0x180;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0x220;

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
        public AcidEmitterManager Manager { get; set; }

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
        /// Special effect name of this <see cref="AcidEmitter"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string SpecialEffect
		{
            get => this._special_effect;
            set
			{
                if (value?.Length >= 0x30) throw new ArgumentLengthException(0x2F);
                else this._special_effect = value ?? String.Empty;
			}
		}

        /// <summary>
        /// Emitter name of this <see cref="AcidEmitter"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string EmitterName
		{
            get => this._emitter_name;
            set
			{
                if (value?.Length >= 0x30) throw new ArgumentLengthException(0x2F);
                else this._emitter_name = value ?? String.Empty;
            }
        }

        /// <summary>
        /// Unknown value at offset 0x14.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public int Unknown0x14 { get; set; }

        /// <summary>
        /// Class key value of this <see cref="AcidEmitter"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public byte ClassKey { get; set; }

        /// <summary>
        /// Stamp value of this <see cref="AcidEmitter"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public byte Stamp { get; set; }

        /// <summary>
        /// Section number of this <see cref="AcidEmitter"/>.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public byte SectionNumber { get; set; }

        /// <summary>
        /// Flags used by the effect's properties.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public byte Flags { get; set; }

        /// <summary>
        /// Unknown value at offset 0x1C.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte Unknown0x1C { get; set; }

        /// <summary>
        /// Unknown value at offset 0x1D.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte Unknown0x1D { get; set; }

        /// <summary>
        /// Unknown value at offset 0x1E.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte Unknown0x1E { get; set; }

        /// <summary>
        /// Unknown value at offset 0x1F.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public byte Unknown0x1F { get; set; }

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
        /// Complexity X of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float ComplexityX { get; set; }

        /// <summary>
        /// Complexity Y of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float ComplexityY { get; set; }

        /// <summary>
        /// Complexity Z of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float ComplexityZ { get; set; }

        /// <summary>
        /// Complexity W of the effect.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float ComplexityW { get; set; }

        /// <summary>
        /// LocalWorld <see cref="Matrix"/> of this <see cref="AcidEmitter"/>.
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix LocalWorld { get; set; }

        /// <summary>
        /// Intensity <see cref="Matrix"/> of this <see cref="AcidEmitter"/>.
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix Intensity { get; set; }

        /// <summary>
        /// Range <see cref="Matrix"/> of this <see cref="AcidEmitter"/>.
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix Range { get; set; }

        /// <summary>
        /// Translation <see cref="Matrix"/> of this <see cref="AcidEmitter"/>.
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix Translation { get; set; }

        /// <summary>
        /// Magnitude <see cref="Matrix"/> of this <see cref="AcidEmitter"/>.
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix Magnitude { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="AcidEmitter"/>.
        /// </summary>
        public AcidEmitter()
		{
            this.Intensity = new Matrix();
            this.LocalWorld = new Matrix();
            this.Magnitude = new Matrix();
            this.Range = new Matrix();
            this.Translation = new Matrix();
		}

        /// <summary>
        /// Initializes new instance of <see cref="AcidEmitter"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="AcidEmitterManager"/> to which this instance belongs to.</param>
        public AcidEmitter(string CName, AcidEmitterManager manager) : this()
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="AcidEmitter"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="AcidEmitterManager"/> to which this instance belongs to.</param>
        public AcidEmitter(BinaryReader br, AcidEmitterManager manager) : this()
        {
            this.Manager = manager;
            this.Disassemble(br);
            this.CollectionName.BinHash();
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~AcidEmitter() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="AcidEmitter"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="AcidEmitter"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write header and all values
            bw.Write(this.Localizer);
            bw.Write(this.Localizer);
            bw.Write(this.BinKey);
            bw.Write(this.SpecialEffect.BinHash());
            bw.Write(this.EmitterName.BinHash());
            bw.Write(this.Unknown0x14);
            bw.Write(this.ClassKey);
            bw.Write(this.Stamp);
            bw.Write(this.SectionNumber);
            bw.Write(this.Flags);
            bw.Write(this.Unknown0x1C);
            bw.Write(this.Unknown0x1D);
            bw.Write(this.Unknown0x1E);
            bw.Write(this.Unknown0x1F);
            this.LocalWorld.Write(bw);
            this.Intensity.Write(bw);
            this.Range.Write(bw);
            bw.Write(this.LastPositionX);
            bw.Write(this.LastPositionY);
            bw.Write(this.LastPositionZ);
            bw.Write(this.LastPositionW);
            bw.Write(this.ComplexityX);
            bw.Write(this.ComplexityY);
            bw.Write(this.ComplexityZ);
            bw.Write(this.ComplexityW);
            this.Translation.Write(bw);
            this.Magnitude.Write(bw);

            // Write CollectionName, SpecialEffect, and EmitterName
            bw.WriteNullTermUTF8(this._collection_name, 0x40);
            bw.WriteNullTermUTF8(this.SpecialEffect, 0x30);
            bw.WriteNullTermUTF8(this.EmitterName, 0x30);
        }

        /// <summary>
        /// Disassembles array into <see cref="AcidEmitter"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="AcidEmitter"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            // Skip header and read values
            br.BaseStream.Position += 0x14;
            this.Unknown0x14 = br.ReadInt32();
            this.ClassKey = br.ReadByte();
            this.Stamp = br.ReadByte();
            this.SectionNumber = br.ReadByte();
            this.Flags = br.ReadByte();
            this.Unknown0x1C = br.ReadByte();
            this.Unknown0x1D = br.ReadByte();
            this.Unknown0x1E = br.ReadByte();
            this.Unknown0x1F = br.ReadByte();
            this.LocalWorld.Read(br);
            this.Intensity.Read(br);
            this.Range.Read(br);
            this.LastPositionX = br.ReadSingle();
            this.LastPositionY = br.ReadSingle();
            this.LastPositionZ = br.ReadSingle();
            this.LastPositionW = br.ReadSingle();
            this.ComplexityX = br.ReadSingle();
            this.ComplexityY = br.ReadSingle();
            this.ComplexityZ = br.ReadSingle();
            this.ComplexityW = br.ReadSingle();
            this.Translation.Read(br);
            this.Magnitude.Read(br);

            // Read CollectionName, SpecialEffect, and EmitterName
            this._collection_name = br.ReadNullTermUTF8(0x40);
            this._special_effect = br.ReadNullTermUTF8(0x30);
            this._emitter_name = br.ReadNullTermUTF8(0x30);
            this._special_effect.BinHash();
            this._emitter_name.BinHash();
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new AcidEmitter(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="AcidEmitter"/> 
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
            using (var ms = new MemoryStream(0x200 + this.CollectionName.Length))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);
                writer.WriteNullTermUTF8(this._special_effect);
                writer.WriteNullTermUTF8(this._emitter_name);

                writer.Write(this.Unknown0x14);
                writer.Write(this.ClassKey);
                writer.Write(this.Stamp);
                writer.Write(this.SectionNumber);
                writer.Write(this.Flags);
                writer.Write(this.Unknown0x1C);
                writer.Write(this.Unknown0x1D);
                writer.Write(this.Unknown0x1E);
                writer.Write(this.Unknown0x1F);
                writer.Write(this.LastPositionX);
                writer.Write(this.LastPositionY);
                writer.Write(this.LastPositionZ);
                writer.Write(this.LastPositionW);
                writer.Write(this.ComplexityX);
                writer.Write(this.ComplexityY);
                writer.Write(this.ComplexityZ);
                writer.Write(this.ComplexityW);
                this.LocalWorld.Write(writer);
                this.Intensity.Write(writer);
                this.Range.Write(writer);
                this.Translation.Write(writer);
                this.Magnitude.Write(writer);

                array = ms.ToArray();

            }

            array = Interop.Compress(array, LZCompressionType.BEST);

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
            this._special_effect = reader.ReadNullTermUTF8();
            this._emitter_name = reader.ReadNullTermUTF8();

            this.Unknown0x14 = reader.ReadInt32();
            this.ClassKey = reader.ReadByte();
            this.Stamp = reader.ReadByte();
            this.SectionNumber = reader.ReadByte();
            this.Flags = reader.ReadByte();
            this.Unknown0x1C = reader.ReadByte();
            this.Unknown0x1D = reader.ReadByte();
            this.Unknown0x1E = reader.ReadByte();
            this.Unknown0x1F = reader.ReadByte();
            this.LastPositionX = reader.ReadSingle();
            this.LastPositionY = reader.ReadSingle();
            this.LastPositionZ = reader.ReadSingle();
            this.LastPositionW = reader.ReadSingle();
            this.ComplexityX = reader.ReadSingle();
            this.ComplexityY = reader.ReadSingle();
            this.ComplexityZ = reader.ReadSingle();
            this.ComplexityW = reader.ReadSingle();
            this.LocalWorld.Read(reader);
            this.Intensity.Read(reader);
            this.Range.Read(reader);
            this.Translation.Read(reader);
            this.Magnitude.Read(reader);
        }

        #endregion
    }
}