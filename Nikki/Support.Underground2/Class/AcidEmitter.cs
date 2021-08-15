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
        private string _texture_name = String.Empty;
        private string _group_name = String.Empty;

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
        public string TextureName
		{
            get => this._texture_name;
            set
			{
                if (value?.Length >= 0x30) throw new ArgumentLengthException(0x2F);
                else this._texture_name = value ?? String.Empty;
			}
		}

        /// <summary>
        /// Emitter name of this <see cref="AcidEmitter"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string GroupName
		{
            get => this._group_name;
            set
			{
                if (value?.Length >= 0x30) throw new ArgumentLengthException(0x2F);
                else this._group_name = value ?? String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public byte SpreadAsDisc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public byte ContactSheetW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public byte ContactSheetH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public byte AnimFPS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public byte RandomStartFrame { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public byte RandomRotationDirection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public byte MotionLive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public int OnCycle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float OnCycleVariance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public int OffCycle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float OffCycleVariance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public int NumParticles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float NumParticlesVariance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float Life { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float LifeVariance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float Speed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float SpeedVariance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Angular")]
        public float InitialAngleRange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Angular")]
        public float SpreadAngle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float MotionInherit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Variance")]
        public float MotionInheritVariance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Settings")]
        public float CarPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeCenter")]
        public float VolumeCenterX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeCenter")]
        public float VolumeCenterY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeCenter")]
        public float VolumeCenterZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeCenter")]
        public float VolumeCenterW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeExtent")]
        public float VolumeExtentX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeExtent")]
        public float VolumeExtentY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeExtent")]
        public float VolumeExtentZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("VolumeExtent")]
        public float VolumeExtentW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Angular")]
        public float FarClip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Angular")]
        public float Gravity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Angular")]
        public float Drag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Angular")]
        public float MaxPixelSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("KeyPositions")]
        public float KeyPosition1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("KeyPositions")]
        public float KeyPosition2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("KeyPositions")]
        public float KeyPosition3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("KeyPositions")]
        public float KeyPosition4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Size")]
        public float SizeX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Size")]
        public float SizeY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Size")]
        public float SizeZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Size")]
        public float SizeW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("RelativeAngle")]
        public float RelativeAngleX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("RelativeAngle")]
        public float RelativeAngleY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("RelativeAngle")]
        public float RelativeAngleZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("RelativeAngle")]
        public float RelativeAngleW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix ColorMatrix { get; }

        /// <summary>
        /// 
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix ColorBasis { get; }

        /// <summary>
        /// 
        /// </summary>
        [Expandable("Matrix")]
        [Browsable(false)]
        public Matrix ExtraBasis { get; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="AcidEmitter"/>.
        /// </summary>
        public AcidEmitter()
		{
            this.ColorMatrix = new Matrix();
            this.ColorBasis = new Matrix();
            this.ExtraBasis = new Matrix();
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

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="AcidEmitter"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="AcidEmitter"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            bw.Write(0);
            bw.Write(0);
            bw.Write(this._collection_name.BinHash());
            bw.Write(this._texture_name.BinHash());
            bw.Write(this._group_name.BinHash());
            bw.Write(0);
            bw.Write(this.SpreadAsDisc);
            bw.Write(this.ContactSheetW);
            bw.Write(this.ContactSheetH);
            bw.Write(this.AnimFPS);
            bw.Write(this.RandomStartFrame);
            bw.Write(this.RandomRotationDirection);
            bw.Write(this.MotionLive);
            bw.Write((byte)0);
            bw.Write(this.OnCycle);
            bw.Write(this.OnCycleVariance);
            bw.Write(this.OffCycle);
            bw.Write(this.OffCycleVariance);
            bw.Write(this.NumParticles);
            bw.Write(this.NumParticlesVariance);
            bw.Write(this.Life);
            bw.Write(this.LifeVariance);
            bw.Write(this.Speed);
            bw.Write(this.SpeedVariance);
            bw.Write(this.InitialAngleRange);
            bw.Write(this.SpreadAngle);
            bw.Write(this.MotionInherit);
            bw.Write(this.MotionInheritVariance);
            bw.Write(this.CarPosition);
            bw.Write(0);
            bw.Write(this.VolumeCenterX);
            bw.Write(this.VolumeCenterY);
            bw.Write(this.VolumeCenterZ);
            bw.Write(this.VolumeCenterW);
            bw.Write(this.VolumeExtentX);
            bw.Write(this.VolumeExtentY);
            bw.Write(this.VolumeExtentZ);
            bw.Write(this.VolumeExtentW);
            bw.Write(this.FarClip);
            bw.Write(this.Gravity);
            bw.Write(this.Drag);
            bw.Write(this.MaxPixelSize);
            bw.Write(this.KeyPosition1);
            bw.Write(this.KeyPosition2);
            bw.Write(this.KeyPosition3);
            bw.Write(this.KeyPosition4);
            this.ColorMatrix.Write(bw);
            bw.Write(this.SizeX);
            bw.Write(this.SizeY);
            bw.Write(this.SizeZ);
            bw.Write(this.SizeW);
            bw.Write(this.RelativeAngleX);
            bw.Write(this.RelativeAngleY);
            bw.Write(this.RelativeAngleZ);
            bw.Write(this.RelativeAngleW);
            this.ColorBasis.Write(bw);
            this.ExtraBasis.Write(bw);
            bw.WriteNullTermUTF8(this._collection_name, 0x40);
            bw.WriteNullTermUTF8(this.TextureName, 0x30);
            bw.WriteNullTermUTF8(this.GroupName, 0x30);
        }

        /// <summary>
        /// Disassembles array into <see cref="AcidEmitter"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="AcidEmitter"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x18;
            this.SpreadAsDisc = br.ReadByte();
            this.ContactSheetW = br.ReadByte();
            this.ContactSheetH = br.ReadByte();
            this.AnimFPS = br.ReadByte();
            this.RandomStartFrame = br.ReadByte();
            this.RandomRotationDirection = br.ReadByte();
            this.MotionLive = br.ReadByte();
            br.BaseStream.Position += 0x01;
            this.OnCycle = br.ReadInt32();
            this.OnCycleVariance = br.ReadSingle();
            this.OffCycle = br.ReadInt32();
            this.OffCycleVariance = br.ReadSingle();
            this.NumParticles = br.ReadInt32();
            this.NumParticlesVariance = br.ReadSingle();
            this.Life = br.ReadSingle();
            this.LifeVariance = br.ReadSingle();
            this.Speed = br.ReadSingle();
            this.SpeedVariance = br.ReadSingle();
            this.InitialAngleRange = br.ReadSingle();
            this.SpreadAngle = br.ReadSingle();
            this.MotionInherit = br.ReadSingle();
            this.MotionInheritVariance = br.ReadSingle();
            this.CarPosition = br.ReadSingle();
            br.BaseStream.Position += 0x04;
            this.VolumeCenterX = br.ReadSingle();
            this.VolumeCenterY = br.ReadSingle();
            this.VolumeCenterZ = br.ReadSingle();
            this.VolumeCenterW = br.ReadSingle();
            this.VolumeExtentX = br.ReadSingle();
            this.VolumeExtentY = br.ReadSingle();
            this.VolumeExtentZ = br.ReadSingle();
            this.VolumeExtentW = br.ReadSingle();
            this.FarClip = br.ReadSingle();
            this.Gravity = br.ReadSingle();
            this.Drag = br.ReadSingle();
            this.MaxPixelSize = br.ReadSingle();
            this.KeyPosition1 = br.ReadSingle();
            this.KeyPosition2 = br.ReadSingle();
            this.KeyPosition3 = br.ReadSingle();
            this.KeyPosition4 = br.ReadSingle();
            this.ColorMatrix.Read(br);
            this.SizeX = br.ReadSingle();
            this.SizeY = br.ReadSingle();
            this.SizeZ = br.ReadSingle();
            this.SizeW = br.ReadSingle();
            this.RelativeAngleX = br.ReadSingle();
            this.RelativeAngleY = br.ReadSingle();
            this.RelativeAngleZ = br.ReadSingle();
            this.RelativeAngleW = br.ReadSingle();
            this.ColorBasis.Read(br);
            this.ExtraBasis.Read(br);
            this._collection_name = br.ReadNullTermUTF8(0x40);
            this._texture_name = br.ReadNullTermUTF8(0x30);
            this._group_name = br.ReadNullTermUTF8(0x30);
            this._texture_name.BinHash();
            this._group_name.BinHash();
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
                writer.WriteNullTermUTF8(this._texture_name);
                writer.WriteNullTermUTF8(this._group_name);

                bw.Write(this.SpreadAsDisc);
                bw.Write(this.ContactSheetW);
                bw.Write(this.ContactSheetH);
                bw.Write(this.AnimFPS);
                bw.Write(this.RandomStartFrame);
                bw.Write(this.RandomRotationDirection);
                bw.Write(this.MotionLive);
                bw.Write(this.OnCycle);
                bw.Write(this.OnCycleVariance);
                bw.Write(this.OffCycle);
                bw.Write(this.OffCycleVariance);
                bw.Write(this.NumParticles);
                bw.Write(this.NumParticlesVariance);
                bw.Write(this.Life);
                bw.Write(this.LifeVariance);
                bw.Write(this.Speed);
                bw.Write(this.SpeedVariance);
                bw.Write(this.InitialAngleRange);
                bw.Write(this.SpreadAngle);
                bw.Write(this.MotionInherit);
                bw.Write(this.MotionInheritVariance);
                bw.Write(this.CarPosition);
                bw.Write(this.VolumeCenterX);
                bw.Write(this.VolumeCenterY);
                bw.Write(this.VolumeCenterZ);
                bw.Write(this.VolumeCenterW);
                bw.Write(this.VolumeExtentX);
                bw.Write(this.VolumeExtentY);
                bw.Write(this.VolumeExtentZ);
                bw.Write(this.VolumeExtentW);
                bw.Write(this.FarClip);
                bw.Write(this.Gravity);
                bw.Write(this.Drag);
                bw.Write(this.MaxPixelSize);
                bw.Write(this.KeyPosition1);
                bw.Write(this.KeyPosition2);
                bw.Write(this.KeyPosition3);
                bw.Write(this.KeyPosition4);
                this.ColorMatrix.Write(bw);
                bw.Write(this.SizeX);
                bw.Write(this.SizeY);
                bw.Write(this.SizeZ);
                bw.Write(this.SizeW);
                bw.Write(this.RelativeAngleX);
                bw.Write(this.RelativeAngleY);
                bw.Write(this.RelativeAngleZ);
                bw.Write(this.RelativeAngleW);
                this.ColorBasis.Write(bw);
                this.ExtraBasis.Write(bw);

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
            this._texture_name = reader.ReadNullTermUTF8();
            this._group_name = reader.ReadNullTermUTF8();

            this.SpreadAsDisc = br.ReadByte();
            this.ContactSheetW = br.ReadByte();
            this.ContactSheetH = br.ReadByte();
            this.AnimFPS = br.ReadByte();
            this.RandomStartFrame = br.ReadByte();
            this.RandomRotationDirection = br.ReadByte();
            this.OnCycle = br.ReadInt32();
            this.OnCycleVariance = br.ReadSingle();
            this.OffCycle = br.ReadInt32();
            this.OffCycleVariance = br.ReadSingle();
            this.NumParticles = br.ReadInt32();
            this.NumParticlesVariance = br.ReadSingle();
            this.Life = br.ReadSingle();
            this.LifeVariance = br.ReadSingle();
            this.Speed = br.ReadSingle();
            this.SpeedVariance = br.ReadSingle();
            this.InitialAngleRange = br.ReadSingle();
            this.SpreadAngle = br.ReadSingle();
            this.MotionInherit = br.ReadSingle();
            this.MotionInheritVariance = br.ReadSingle();
            this.CarPosition = br.ReadSingle();
            this.VolumeCenterX = br.ReadSingle();
            this.VolumeCenterY = br.ReadSingle();
            this.VolumeCenterZ = br.ReadSingle();
            this.VolumeCenterW = br.ReadSingle();
            this.VolumeExtentX = br.ReadSingle();
            this.VolumeExtentY = br.ReadSingle();
            this.VolumeExtentZ = br.ReadSingle();
            this.VolumeExtentW = br.ReadSingle();
            this.FarClip = br.ReadSingle();
            this.Gravity = br.ReadSingle();
            this.Drag = br.ReadSingle();
            this.MaxPixelSize = br.ReadSingle();
            this.KeyPosition1 = br.ReadSingle();
            this.KeyPosition2 = br.ReadSingle();
            this.KeyPosition3 = br.ReadSingle();
            this.KeyPosition4 = br.ReadSingle();
            this.ColorMatrix.Read(br);
            this.SizeX = br.ReadSingle();
            this.SizeY = br.ReadSingle();
            this.SizeZ = br.ReadSingle();
            this.SizeW = br.ReadSingle();
            this.RelativeAngleX = br.ReadSingle();
            this.RelativeAngleY = br.ReadSingle();
            this.RelativeAngleZ = br.ReadSingle();
            this.RelativeAngleW = br.ReadSingle();
            this.ColorBasis.Read(br);
            this.ExtraBasis.Read(br);
        }

        #endregion
    }
}