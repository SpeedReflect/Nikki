using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using Nikki.Support.Underground2.Framework;
using Nikki.Support.Underground2.Parts.InfoParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Class
{
    /// <summary>
    /// <see cref="CarTypeInfo"/> is a collection of settings related to a car's basic information.
    /// </summary>
    public class CarTypeInfo : Shared.Class.CarTypeInfo
    {
        #region Fields

        private string _collection_name;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = 0xD;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0;
        
        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0x890;

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
        public CarTypeInfoManager Manager { get; set; }

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
        /// Manufacturer name of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public override string ManufacturerName { get; set; }

        /// <summary>
        /// Default base paint of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public override string DefaultBasePaint { get; set; } = String.Empty;

        /// <summary>
        /// Second default base paint of the <see cref="CarTypeInfo"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public string DefaultBasePaint2 { get; set; } = String.Empty;

        /// <summary>
        /// Defines whether the car is an SUV.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public eBoolean IsSUV { get; set; }

        /// <summary>
        /// X value of aerodynamics vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float AerodynamicsForceX { get; set; }

        /// <summary>
        /// Y value of aerodynamics vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float AerodynamicsForceY { get; set; }

        /// <summary>
        /// Z value of aerodynamics vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float AerodynamicsForceZ { get; set; }

        /// <summary>
        /// W value of aerodynamics vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public float AerodynamicsForceW { get; set; }

        /// <summary>
        /// Car skin 1.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN01 { get; }

        /// <summary>
        /// Car skin 2.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN02 { get; }

        /// <summary>
        /// Car skin 3.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN03 { get; }

        /// <summary>
        /// Car skin 4.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN04 { get; }

        /// <summary>
        /// Car skin 5.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN05 { get; }

        /// <summary>
        /// Car skin 6.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN06 { get; }

        /// <summary>
        /// Car skin 7.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN07 { get; }

        /// <summary>
        /// Car skin 8.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN08 { get; }

        /// <summary>
        /// Car skin 9.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN09 { get; }

        /// <summary>
        /// Car skin 10.
        /// </summary>
        [Browsable(false)]
        [Expandable("CarSkins")]
        public CarSkin CARSKIN10 { get; }

        /// <summary>
        /// Ecar settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("General")]
        public Ecar ECAR { get; }

        /// <summary>
        /// Pvehicle settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("General")]
        public Pvehicle PVEHICLE { get; }

        /// <summary>
        /// Rigid control settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("General")]
        public RigidControls RIGID_CONTROLS { get; }

        /// <summary>
        /// Front left wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_FRONT_LEFT { get; }

        /// <summary>
        /// Front right wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_FRONT_RIGHT { get; }

        /// <summary>
        /// Rear right wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_REAR_RIGHT { get; }

        /// <summary>
        /// Rear left wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_REAR_LEFT { get; }

        /// <summary>
        /// Far player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_FAR { get; }

        /// <summary>
        /// Close player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_CLOSE { get; }

        /// <summary>
        /// Bumper player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_BUMPER { get; }

        /// <summary>
        /// Driver player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_DRIVER { get; }

        /// <summary>
        /// Hood player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_HOOD { get; }

        /// <summary>
        /// Drift player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_DRIFT { get; }

        /// <summary>
        /// Far AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("AICamera")]
        public Camera AI_CAMERA_FAR { get; }

        /// <summary>
        /// Close AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("AICamera")]
        public Camera AI_CAMERA_CLOSE { get; }

        /// <summary>
        /// Bumper AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("AICamera")]
        public Camera AI_CAMERA_BUMPER { get; }

        /// <summary>
        /// Driver AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("AICamera")]
        public Camera AI_CAMERA_DRIVER { get; }

        /// <summary>
        /// Hood AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("AICamera")]
        public Camera AI_CAMERA_HOOD { get; }

        /// <summary>
        /// Drift AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Browsable(false)]
        [Expandable("AICamera")]
        public Camera AI_CAMERA_DRIFT { get; }

        /// <summary>
        /// Base tires performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Tires")]
        public Tires BASE_TIRES { get; }

        /// <summary>
        /// Top tires performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Tires")]
        public Tires TOP_TIRES { get; }

        /// <summary>
        /// Base suspension performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Suspension")]
        public Suspension BASE_SUSPENSION { get; }

        /// <summary>
        /// Top suspension performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Suspension")]
        public Suspension TOP_SUSPENSION { get; }

        /// <summary>
        /// Base transmission performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Transmission")]
        public Transmission BASE_TRANSMISSION { get; }

        /// <summary>
        /// Street transmission performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Transmission")]
        public Transmission STREET_TRANSMISSION { get; }

        /// <summary>
        /// Pro transmission performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Transmission")]
        public Transmission PRO_TRANSMISSION { get; }

        /// <summary>
        /// Top transmission performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Transmission")]
        public Transmission TOP_TRANSMISSION { get; }

        /// <summary>
        /// Base RPM performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("RPM")]
        public RPM BASE_RPM { get; }

        /// <summary>
        /// Street RPM performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("RPM")]
        public RPM STREET_RPM { get; }

        /// <summary>
        /// Pro RPM performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("RPM")]
        public RPM PRO_RPM { get; }

        /// <summary>
        /// Top RPM performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("RPM")]
        public RPM TOP_RPM { get; }

        /// <summary>
        /// Street ECU performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("ECU")]
        public ECU STREET_ECU { get; }

        /// <summary>
        /// Pro ECU performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("ECU")]
        public ECU PRO_ECU { get; }

        /// <summary>
        /// Top ECU performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("ECU")]
        public ECU TOP_ECU { get; }

        /// <summary>
        /// Base engine performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Engine")]
        public Engine BASE_ENGINE { get; }

        /// <summary>
        /// Top engine performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Engine")]
        public Engine TOP_ENGINE { get; }

        /// <summary>
        /// Base turbo performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Turbo")]
        public Turbo BASE_TURBO { get; }

        /// <summary>
        /// Top turbo performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Turbo")]
        public Turbo TOP_TURBO { get; }

        /// <summary>
        /// Base brakes performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Brakes")]
        public Brakes BASE_BRAKES { get; }

        /// <summary>
        /// Top brakes performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("Brakes")]
        public Brakes TOP_BRAKES { get; }

        /// <summary>
        /// Top weight reduction performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("WeightReduction")]
        public WeightReduction TOP_WEIGHT_REDUCTION { get; }

        /// <summary>
        /// Top nitrous performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("NOS")]
        public Nitrous TOP_NITROUS { get; }

        /// <summary>
        /// Additional drift yaw control performance settings.
        /// </summary>
        [Browsable(false)]
        [Expandable("DriftControl")]
        public DriftControl DRIFT_ADD_CONTROL { get; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        public CarTypeInfo()
		{
            this.ECAR = new Ecar();
            this.PVEHICLE = new Pvehicle();
            this.RIGID_CONTROLS = new RigidControls();
            this.AI_CAMERA_DRIVER = new Camera();
            this.AI_CAMERA_CLOSE = new Camera();
            this.AI_CAMERA_DRIFT = new Camera();
            this.AI_CAMERA_BUMPER = new Camera();
            this.AI_CAMERA_FAR = new Camera();
            this.AI_CAMERA_HOOD = new Camera();
            this.PLAYER_CAMERA_DRIVER = new Camera();
            this.PLAYER_CAMERA_CLOSE = new Camera();
            this.PLAYER_CAMERA_DRIFT = new Camera();
            this.PLAYER_CAMERA_BUMPER = new Camera();
            this.PLAYER_CAMERA_FAR = new Camera();
            this.PLAYER_CAMERA_HOOD = new Camera();
            this.BASE_BRAKES = new Brakes();
            this.BASE_ENGINE = new Engine();
            this.BASE_RPM = new RPM();
            this.BASE_SUSPENSION = new Suspension();
            this.BASE_TIRES = new Tires();
            this.BASE_TRANSMISSION = new Transmission();
            this.BASE_TURBO = new Turbo();
            this.DRIFT_ADD_CONTROL = new DriftControl();
            this.STREET_ECU = new ECU();
            this.STREET_RPM = new RPM();
            this.STREET_TRANSMISSION = new Transmission();
            this.PRO_ECU = new ECU();
            this.PRO_RPM = new RPM();
            this.PRO_TRANSMISSION = new Transmission();
            this.TOP_BRAKES = new Brakes();
            this.TOP_ECU = new ECU();
            this.TOP_ENGINE = new Engine();
            this.TOP_NITROUS = new Nitrous();
            this.TOP_RPM = new RPM();
            this.TOP_SUSPENSION = new Suspension();
            this.TOP_TIRES = new Tires();
            this.TOP_TRANSMISSION = new Transmission();
            this.TOP_TURBO = new Turbo();
            this.TOP_WEIGHT_REDUCTION = new WeightReduction();
            this.WHEEL_FRONT_LEFT = new CarInfoWheel();
            this.WHEEL_FRONT_RIGHT = new CarInfoWheel();
            this.WHEEL_REAR_LEFT = new CarInfoWheel();
            this.WHEEL_REAR_RIGHT = new CarInfoWheel();
            this.CARSKIN01 = new CarSkin();
            this.CARSKIN02 = new CarSkin();
            this.CARSKIN03 = new CarSkin();
            this.CARSKIN04 = new CarSkin();
            this.CARSKIN05 = new CarSkin();
            this.CARSKIN06 = new CarSkin();
            this.CARSKIN07 = new CarSkin();
            this.CARSKIN08 = new CarSkin();
            this.CARSKIN09 = new CarSkin();
            this.CARSKIN10 = new CarSkin();
        }

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="CarTypeInfoManager"/> to which this instance belongs to.</param>
        public CarTypeInfo(string CName, CarTypeInfoManager manager) : this()
        {
            this.Manager = manager;
            this.CollectionName = CName;
            this.ManufacturerName = "GENERIC";
            this.WhatGame = 2;
            this.WheelOuterRadius = 26;
            this.WheelInnerRadiusMin = 17;
            this.WheelInnerRadiusMax = 20;
            this.DefaultSkinNumber = 1;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="CarTypeInfoManager"/> to which this instance belongs to.</param>
        public CarTypeInfo(BinaryReader br, CarTypeInfoManager manager) : this()
        {
            this.Manager = manager;
            this.Disassemble(br);
            this.CollectionName.BinHash();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="CarTypeInfo"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="CarTypeInfo"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write CollectionName and BaseModelName
            bw.WriteNullTermUTF8(this._collection_name, 0x20);
            bw.WriteNullTermUTF8(this._collection_name, 0x20);

            // Write GeometryFileName
            string path1 = Path.Combine("CARS", this.CollectionName, "GEOMETRY.BIN");
            string path2 = Path.Combine("CARS", this.CollectionName, "GEOMETRY.LZC");
            bw.WriteNullTermUTF8(path1, 0x20);
            bw.WriteNullTermUTF8(path2, 0x20);
            bw.WriteBytes(0, 0x40);

            // Write ManufacturerName
            bw.WriteNullTermUTF8(this.ManufacturerName, 0x10);

            // Secondary Properties
            bw.Write(this.BinKey);
            bw.Write(this.HeadlightFOV);
            bw.Write(this.PadHighPerformance);
            bw.Write(this.NumAvailableSkinNumbers);
            bw.Write(this.WhatGame);
            bw.Write(this.ConvertibleFlag);
            bw.Write(this.WheelOuterRadius);
            bw.Write(this.WheelInnerRadiusMin);
            bw.Write(this.WheelInnerRadiusMax);
            bw.Write((byte)0);

            // Vectors
            bw.Write(this.HeadlightPositionX);
            bw.Write(this.HeadlightPositionY);
            bw.Write(this.HeadlightPositionZ);
            bw.Write(this.HeadlightPositionW);
            bw.Write(this.DriverRenderingOffsetX);
            bw.Write(this.DriverRenderingOffsetY);
            bw.Write(this.DriverRenderingOffsetZ);
            bw.Write(this.DriverRenderingOffsetW);
            bw.Write(this.SteeringWheelRenderingX);
            bw.Write(this.SteeringWheelRenderingY);
            bw.Write(this.SteeringWheelRenderingZ);
            bw.Write(this.SteeringWheelRenderingW);
            bw.Write(this.AerodynamicsForceX);
            bw.Write(this.AerodynamicsForceY);
            bw.Write(this.AerodynamicsForceZ);
            bw.Write(this.AerodynamicsForceW);

            // Car Wheels
            this.WHEEL_FRONT_LEFT.WheelID = CarInfoWheel.CarWheelType.FRONT_LEFT;
            this.WHEEL_FRONT_RIGHT.WheelID = CarInfoWheel.CarWheelType.FRONT_RIGHT;
            this.WHEEL_REAR_RIGHT.WheelID = CarInfoWheel.CarWheelType.REAR_RIGHT;
            this.WHEEL_REAR_LEFT.WheelID = CarInfoWheel.CarWheelType.REAR_LEFT;
            this.WHEEL_FRONT_LEFT.Write(bw);
            this.WHEEL_FRONT_RIGHT.Write(bw);
            this.WHEEL_REAR_RIGHT.Write(bw);
            this.WHEEL_REAR_LEFT.Write(bw);

            // Base Tires Performance
            this.BASE_TIRES.Write(bw);

            // Pvehicle Values
            bw.Write(this.PVEHICLE.Massx1000Multiplier);
            bw.Write(this.PVEHICLE.TensorScaleX);
            bw.Write(this.PVEHICLE.TensorScaleY);
            bw.Write(this.PVEHICLE.TensorScaleZ);
            bw.Write(this.PVEHICLE.TensorScaleW);
            bw.WriteBytes(0, 0x10);
            bw.Write(this.ECAR.Unknown1);
            bw.WriteBytes(0, 0x10);
            bw.Write(this.ECAR.Unknown2);
            bw.WriteBytes(0, 0x10);
            bw.Write(this.PVEHICLE.Unknown1);
            bw.Write(this.PVEHICLE.InitialHandlingRating);
            bw.WriteBytes(0, 0xC);

            // Base Suspension Performance
            this.BASE_SUSPENSION.Write(bw);

            // Base Transmission Performance
            this.BASE_TRANSMISSION.Write(bw);

            // Base RPM Performance
            this.BASE_RPM.Write(bw);

            // Base Engine Performance
            this.BASE_ENGINE.Write(bw);

            // Base Turbo Performance
            this.BASE_TURBO.Write(bw);

            // Base Brakes Performance
            bw.Write(this.PVEHICLE.TopSpeedUnderflow);
            bw.Write(this.BASE_BRAKES.FrontDownForce);
            bw.Write(this.BASE_BRAKES.RearDownForce);
            bw.Write(this.BASE_BRAKES.BumpJumpForce);
            bw.Write(this.BASE_BRAKES.SteeringRatio);
            bw.Write(this.BASE_BRAKES.BrakeStrength);
            bw.Write(this.BASE_BRAKES.HandBrakeStrength);
            bw.Write(this.BASE_BRAKES.BrakeBias);

            // Ecar Values
            bw.Write((int)0);
            bw.Write(this.PVEHICLE.Unknown2);
            bw.Write(this.PVEHICLE.Unknown3);
            bw.Write((int)0);
            bw.Write(this.PVEHICLE.StockTopSpeedLimiter);
            bw.WriteBytes(0, 0x1C);

            // DriftAdditionalYawControl Performance
            this.DRIFT_ADD_CONTROL.Write(bw);

            // Skip Street + Pro Engine and Street Turbo, 0x03E0 - 0x0450
            bw.Write(this.TOP_ENGINE.EngineTorque1 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque2 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque3 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque4 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque5 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque6 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque7 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque8 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque9 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque1 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque2 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque3 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque4 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque5 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque6 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque7 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque8 / 3);
            bw.Write(this.TOP_ENGINE.EngineTorque9 / 3);
            bw.Write(this.TOP_TURBO.TurboBraking / 10);
            bw.Write(this.TOP_TURBO.TurboVacuum / 10);
            bw.Write(this.TOP_TURBO.TurboHeatHigh / 10);
            bw.Write(this.TOP_TURBO.TurboHeatLow / 10);
            bw.Write(this.TOP_TURBO.TurboHighBoost / 10);
            bw.Write(this.TOP_TURBO.TurboLowBoost / 10);
            bw.Write(this.TOP_TURBO.TurboSpool / 10);
            bw.Write(this.TOP_TURBO.TurboSpoolTimeDown / 10);
            bw.Write(this.TOP_TURBO.TurboSpoolTimeUp / 10);
            bw.Write((int)0);

            // Top Weight Reduction Performance
            this.TOP_WEIGHT_REDUCTION.Write(bw);

            // Street Transmission Performance
            this.STREET_TRANSMISSION.Write(bw);

            // Pro Transmission Performance
            this.PRO_TRANSMISSION.Write(bw);

            // Top Transmission Performance
            this.TOP_TRANSMISSION.Write(bw);

            // Top Engine Performance
            bw.WriteBytes(0, 0xC);
            this.TOP_ENGINE.Write(bw);

            // Street RPM Performance
            this.STREET_RPM.Write(bw);
            bw.Write(this.TOP_ENGINE.SpeedRefreshRate / 3);

            // Street ECU Performance
            this.STREET_ECU.Write(bw);

            // Pro RPM Performance
            this.PRO_RPM.Write(bw);
            bw.Write(this.TOP_ENGINE.SpeedRefreshRate * 2 / 3);

            // Pro ECU Performance
            this.PRO_ECU.Write(bw);

            // Top RPM Performance
            this.TOP_RPM.Write(bw);
            bw.Write(this.TOP_ENGINE.SpeedRefreshRate);

            // Top ECU Performance
            this.TOP_ECU.Write(bw);

            // Top Turbo Performance
            this.TOP_TURBO.Write(bw);

            // Top Tires Performance
            this.TOP_TIRES.Write(bw);

            // Top Nitrous Performance
            this.TOP_NITROUS.Write(bw);

            // Top Brakes Performance
            bw.Write((int)0);
            bw.Write(this.TOP_BRAKES.RearDownForce);
            bw.Write(this.TOP_BRAKES.BumpJumpForce);
            bw.Write((int)0);
            bw.Write(this.TOP_BRAKES.FrontDownForce);
            bw.Write(this.TOP_BRAKES.RearDownForce);
            bw.Write(this.TOP_BRAKES.BumpJumpForce);
            bw.Write((int)0);
            bw.Write(this.TOP_BRAKES.BrakeStrength);
            bw.Write(this.TOP_BRAKES.HandBrakeStrength);
            bw.Write(this.TOP_BRAKES.BrakeBias);
            bw.Write(this.TOP_BRAKES.SteeringRatio);

            // Top Suspension Performance
            this.TOP_SUSPENSION.Write(bw);

            bw.Write(this.ECAR.HandlingBuffer);
            bw.Write(this.ECAR.TopSuspFrontHeightReduce);
            bw.Write(this.ECAR.TopSuspRearHeightReduce);
            bw.Write((int)0);
            bw.Write(this.ECAR.NumPlayerCameras);
            bw.Write(this.ECAR.NumAICameras);
            bw.Write((long)0);

            // Player Cameras
            this.PLAYER_CAMERA_FAR.Type = Camera.CameraType.FAR;
            this.PLAYER_CAMERA_CLOSE.Type = Camera.CameraType.CLOSE;
            this.PLAYER_CAMERA_BUMPER.Type = Camera.CameraType.BUMPER;
            this.PLAYER_CAMERA_DRIVER.Type = Camera.CameraType.DRIVER;
            this.PLAYER_CAMERA_HOOD.Type = Camera.CameraType.HOOD;
            this.PLAYER_CAMERA_DRIFT.Type = Camera.CameraType.DRIFT;
            this.PLAYER_CAMERA_FAR.Write(bw);
            this.PLAYER_CAMERA_CLOSE.Write(bw);
            this.PLAYER_CAMERA_BUMPER.Write(bw);
            this.PLAYER_CAMERA_DRIVER.Write(bw);
            this.PLAYER_CAMERA_HOOD.Write(bw);
            this.PLAYER_CAMERA_DRIFT.Write(bw);

            // AI Cameras
            this.AI_CAMERA_FAR.Type = Camera.CameraType.FAR;
            this.AI_CAMERA_CLOSE.Type = Camera.CameraType.CLOSE;
            this.AI_CAMERA_BUMPER.Type = Camera.CameraType.BUMPER;
            this.AI_CAMERA_DRIVER.Type = Camera.CameraType.DRIVER;
            this.AI_CAMERA_HOOD.Type = Camera.CameraType.HOOD;
            this.AI_CAMERA_DRIFT.Type = Camera.CameraType.DRIFT;
            this.AI_CAMERA_FAR.Write(bw);
            this.AI_CAMERA_CLOSE.Write(bw);
            this.AI_CAMERA_BUMPER.Write(bw);
            this.AI_CAMERA_DRIVER.Write(bw);
            this.AI_CAMERA_HOOD.Write(bw);
            this.AI_CAMERA_DRIFT.Write(bw);

            // Rigid Controls
            this.RIGID_CONTROLS.Write(bw);

            // Secondary Properties
            bw.Write(this.Index);
            bw.WriteEnum(this.UsageType);
            bw.Write((int)0);
            bw.Write(this.DefaultBasePaint.BinHash());
            bw.Write(this.DefaultBasePaint2.BinHash());
            bw.Write(this.MaxInstances1);
            bw.Write(this.MaxInstances2);
            bw.Write(this.MaxInstances3);
            bw.Write(this.MaxInstances4);
            bw.Write(this.MaxInstances5);
            bw.Write(this.KeepLoaded1);
            bw.Write(this.KeepLoaded2);
            bw.Write(this.KeepLoaded3);
            bw.Write(this.KeepLoaded4);
            bw.Write(this.KeepLoaded5);
            bw.Write((short)0);
            bw.Write(this.MinTimeBetweenUses1);
            bw.Write(this.MinTimeBetweenUses2);
            bw.Write(this.MinTimeBetweenUses3);
            bw.Write(this.MinTimeBetweenUses4);
            bw.Write(this.MinTimeBetweenUses5);
            bw.Write((int)this.DefaultSkinNumber);
            bw.Write((int)0);
            bw.Write(this.ECAR.Cost);
            bw.Write(this.AvailableSkinNumbers01);
            bw.Write(this.AvailableSkinNumbers02);
            bw.Write(this.AvailableSkinNumbers03);
            bw.Write(this.AvailableSkinNumbers04);
            bw.Write(this.AvailableSkinNumbers05);
            bw.Write(this.AvailableSkinNumbers06);
            bw.Write(this.AvailableSkinNumbers07);
            bw.Write(this.AvailableSkinNumbers08);
            bw.Write(this.AvailableSkinNumbers09);
            bw.Write(this.AvailableSkinNumbers10);
            bw.Write((short)this.IsSUV);
            bw.Write((int)this.IsSkinnable);
        }

        /// <summary>
        /// Disassembles array into <see cref="CarTypeInfo"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="CarTypeInfo"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            // CollectionName
            this._collection_name = br.ReadNullTermUTF8(0x20);

            // Manufacturer name
            br.BaseStream.Position += 0xA0;
            this.ManufacturerName = br.ReadNullTermUTF8(0x10);

            // Secondary Properties
            br.BaseStream.Position += 4;
            this.HeadlightFOV = br.ReadSingle();
            this.PadHighPerformance = br.ReadByte();
            this.NumAvailableSkinNumbers = br.ReadByte();
            this.WhatGame = br.ReadByte();
            this.ConvertibleFlag = br.ReadByte();
            this.WheelOuterRadius = br.ReadByte();
            this.WheelInnerRadiusMin = br.ReadByte();
            this.WheelInnerRadiusMax = br.ReadByte();
            ++br.BaseStream.Position;

            // Vectors
            this.HeadlightPositionX = br.ReadSingle();
            this.HeadlightPositionY = br.ReadSingle();
            this.HeadlightPositionZ = br.ReadSingle();
            this.HeadlightPositionW = br.ReadSingle();
            this.DriverRenderingOffsetX = br.ReadSingle();
            this.DriverRenderingOffsetY = br.ReadSingle();
            this.DriverRenderingOffsetZ = br.ReadSingle();
            this.DriverRenderingOffsetW = br.ReadSingle();
            this.SteeringWheelRenderingX = br.ReadSingle();
            this.SteeringWheelRenderingY = br.ReadSingle();
            this.SteeringWheelRenderingZ = br.ReadSingle();
            this.SteeringWheelRenderingW = br.ReadSingle();
            this.AerodynamicsForceX = br.ReadSingle();
            this.AerodynamicsForceY = br.ReadSingle();
            this.AerodynamicsForceZ = br.ReadSingle();
            this.AerodynamicsForceW = br.ReadSingle();

            // Car Wheels
            for (int loop = 0; loop < 4; ++loop)
            {

                var wheel = new CarInfoWheel();
                wheel.Read(br);

                switch (wheel.WheelID)
                {

                    case CarInfoWheel.CarWheelType.FRONT_RIGHT: this.WHEEL_FRONT_RIGHT.CloneValuesFrom(wheel); break;
                    case CarInfoWheel.CarWheelType.REAR_RIGHT: this.WHEEL_REAR_RIGHT.CloneValuesFrom(wheel); break;
                    case CarInfoWheel.CarWheelType.REAR_LEFT: this.WHEEL_REAR_LEFT.CloneValuesFrom(wheel); break;
                    default: this.WHEEL_FRONT_LEFT.CloneValuesFrom(wheel); break;

                }

            }

            // Base Tires Performance
            this.BASE_TIRES.Read(br);

            // Pvehicle and Ecar Values
            this.PVEHICLE.Massx1000Multiplier = br.ReadSingle();
            this.PVEHICLE.TensorScaleX = br.ReadSingle();
            this.PVEHICLE.TensorScaleY = br.ReadSingle();
            this.PVEHICLE.TensorScaleZ = br.ReadSingle();
            this.PVEHICLE.TensorScaleW = br.ReadSingle();
            br.BaseStream.Position += 0x10;
            this.ECAR.Unknown1 = br.ReadSingle();
            br.BaseStream.Position += 0x10;
            this.ECAR.Unknown2 = br.ReadSingle();
            br.BaseStream.Position += 0x10;
            this.PVEHICLE.Unknown1 = br.ReadSingle();
            this.PVEHICLE.InitialHandlingRating = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Base Suspension Performance
            this.BASE_SUSPENSION.Read(br);

            // Base Transmission Performance
            this.BASE_TRANSMISSION.Read(br);

            // Base RPM Performance
            this.BASE_RPM.Read(br);

            // Base Engine Performance
            this.BASE_ENGINE.Read(br);

            // Base Turbo Performance
            this.BASE_TURBO.Read(br);

            // Base Brakes Performance
            this.PVEHICLE.TopSpeedUnderflow = br.ReadSingle();
            this.BASE_BRAKES.FrontDownForce = br.ReadSingle();
            this.BASE_BRAKES.RearDownForce = br.ReadSingle();
            this.BASE_BRAKES.BumpJumpForce = br.ReadSingle();
            this.BASE_BRAKES.SteeringRatio = br.ReadSingle();
            this.BASE_BRAKES.BrakeStrength = br.ReadSingle();
            this.BASE_BRAKES.HandBrakeStrength = br.ReadSingle();
            this.BASE_BRAKES.BrakeBias = br.ReadSingle();
            br.BaseStream.Position += 4;
            this.PVEHICLE.Unknown2 = br.ReadSingle();
            this.PVEHICLE.Unknown3 = br.ReadSingle();
            br.BaseStream.Position += 4;
            this.PVEHICLE.StockTopSpeedLimiter = br.ReadSingle();
            br.BaseStream.Position += 0x1C;

            // DriftAdditionalYawControl Performance
            this.DRIFT_ADD_CONTROL.Read(br);

            // Skip Street + Pro Engine and Street Turbo, 0x03E0 - 0x0450
            br.BaseStream.Position += 0x70;

            // Top Weight Reduction Performance
            this.TOP_WEIGHT_REDUCTION.Read(br);

            // Street Transmission Performance
            this.STREET_TRANSMISSION.Read(br);

            // Pro Transmission Performance
            this.PRO_TRANSMISSION.Read(br);

            // Top Transmission Performance
            this.TOP_TRANSMISSION.Read(br);

            // Top Engine Performance
            br.BaseStream.Position += 0xC;
            this.TOP_ENGINE.Read(br);

            // Street RPM Performance
            this.STREET_RPM.Read(br);
            br.BaseStream.Position += 4;

            // Street ECU Performance
            this.STREET_ECU.Read(br);

            // Pro RPM Performance
            this.PRO_RPM.Read(br);
            br.BaseStream.Position += 4;

            // Pro ECU Performance
            this.PRO_ECU.Read(br);

            // Top RPM Performance
            this.TOP_RPM.Read(br);
            br.BaseStream.Position += 4;

            // Top ECU Performance
            this.TOP_ECU.Read(br);

            // Top Turbo Performance
            this.TOP_TURBO.Read(br);

            // Top Tires Performance
            this.TOP_TIRES.Read(br);

            // Top Nitrous Performance
            this.TOP_NITROUS.Read(br);
            br.BaseStream.Position += 0x10;

            // Top Brakes Performance
            this.TOP_BRAKES.FrontDownForce = br.ReadSingle();
            this.TOP_BRAKES.RearDownForce = br.ReadSingle();
            this.TOP_BRAKES.BumpJumpForce = br.ReadSingle();
            br.BaseStream.Position += 4;
            this.TOP_BRAKES.BrakeStrength = br.ReadSingle();
            this.TOP_BRAKES.HandBrakeStrength = br.ReadSingle();
            this.TOP_BRAKES.BrakeBias = br.ReadSingle();
            this.TOP_BRAKES.SteeringRatio = br.ReadSingle();

            // Top Suspension Performance
            this.TOP_SUSPENSION.Read(br);

            // Ecar values
            this.ECAR.HandlingBuffer = br.ReadSingle();
            this.ECAR.TopSuspFrontHeightReduce = br.ReadSingle();
            this.ECAR.TopSuspRearHeightReduce = br.ReadSingle();
            br.BaseStream.Position += 4;
            this.ECAR.NumPlayerCameras = br.ReadInt32();
            this.ECAR.NumAICameras = br.ReadInt32();
            br.BaseStream.Position += 8;

            // Player Cameras
            for (int loop = 0; loop < 6; ++loop)
            {

                var camera = new Camera();
                camera.Read(br);
                
                switch (camera.Type)
                {

                    case Camera.CameraType.CLOSE: this.PLAYER_CAMERA_CLOSE.CloneValuesFrom(camera); break;
                    case Camera.CameraType.BUMPER: this.PLAYER_CAMERA_BUMPER.CloneValuesFrom(camera); break;
                    case Camera.CameraType.DRIVER: this.PLAYER_CAMERA_DRIVER.CloneValuesFrom(camera); break;
                    case Camera.CameraType.HOOD: this.PLAYER_CAMERA_HOOD.CloneValuesFrom(camera); break;
                    case Camera.CameraType.DRIFT: this.PLAYER_CAMERA_DRIFT.CloneValuesFrom(camera); break;
                    default: this.PLAYER_CAMERA_FAR.CloneValuesFrom(camera); break;
                
                }
            
            }

            // AI Cameras
            for (int loop = 0; loop < 6; ++loop)
            {
                
                var camera = new Camera();
                camera.Read(br);
                
                switch (camera.Type)
                {

                    case Camera.CameraType.CLOSE: this.AI_CAMERA_CLOSE.CloneValuesFrom(camera); break;
                    case Camera.CameraType.BUMPER: this.AI_CAMERA_BUMPER.CloneValuesFrom(camera); break;
                    case Camera.CameraType.DRIVER: this.AI_CAMERA_DRIVER.CloneValuesFrom(camera); break;
                    case Camera.CameraType.HOOD: this.AI_CAMERA_HOOD.CloneValuesFrom(camera); break;
                    case Camera.CameraType.DRIFT: this.AI_CAMERA_DRIFT.CloneValuesFrom(camera); break;
                    default: this.AI_CAMERA_FAR.CloneValuesFrom(camera); break;
                
                }
            
            }

            // Rigid Controls
            this.RIGID_CONTROLS.Read(br);

            // Secondary Properties
            this.Index = br.ReadInt32();
            this.UsageType = br.ReadEnum<CarUsageType>();
            br.BaseStream.Position += 4;
            this.DefaultBasePaint = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.DefaultBasePaint2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
            this.MaxInstances1 = br.ReadByte();
            this.MaxInstances2 = br.ReadByte();
            this.MaxInstances3 = br.ReadByte();
            this.MaxInstances4 = br.ReadByte();
            this.MaxInstances5 = br.ReadByte();
            this.KeepLoaded1 = br.ReadByte();
            this.KeepLoaded2 = br.ReadByte();
            this.KeepLoaded3 = br.ReadByte();
            this.KeepLoaded4 = br.ReadByte();
            this.KeepLoaded5 = br.ReadByte();
            br.BaseStream.Position += 2;
            this.MinTimeBetweenUses1 = br.ReadSingle();
            this.MinTimeBetweenUses2 = br.ReadSingle();
            this.MinTimeBetweenUses3 = br.ReadSingle();
            this.MinTimeBetweenUses4 = br.ReadSingle();
            this.MinTimeBetweenUses5 = br.ReadSingle();
            this.DefaultSkinNumber = (byte)br.ReadInt32();
            br.BaseStream.Position += 4;
            this.ECAR.Cost = br.ReadInt32();
            this.AvailableSkinNumbers01 = br.ReadByte();
            this.AvailableSkinNumbers02 = br.ReadByte();
            this.AvailableSkinNumbers03 = br.ReadByte();
            this.AvailableSkinNumbers04 = br.ReadByte();
            this.AvailableSkinNumbers05 = br.ReadByte();
            this.AvailableSkinNumbers06 = br.ReadByte();
            this.AvailableSkinNumbers07 = br.ReadByte();
            this.AvailableSkinNumbers08 = br.ReadByte();
            this.AvailableSkinNumbers09 = br.ReadByte();
            this.AvailableSkinNumbers10 = br.ReadByte();
            this.IsSUV = (br.ReadInt16() == 0)
                ? eBoolean.False
                : eBoolean.True;
            this.IsSkinnable = (br.ReadInt32() == 0)
                ? eBoolean.False
                : eBoolean.True;
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new CarTypeInfo(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Writes all <see cref="CarSkin"/> using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void GetCarSkins(BinaryWriter bw)
        {
            // Precalculate size of the array first
            var skinsused = new List<byte>();
            if (this.AvailableSkinNumbers01 > 0) skinsused.Add(1);
            if (this.AvailableSkinNumbers02 > 0) skinsused.Add(2);
            if (this.AvailableSkinNumbers03 > 0) skinsused.Add(3);
            if (this.AvailableSkinNumbers04 > 0) skinsused.Add(4);
            if (this.AvailableSkinNumbers05 > 0) skinsused.Add(5);
            if (this.AvailableSkinNumbers06 > 0) skinsused.Add(6);
            if (this.AvailableSkinNumbers07 > 0) skinsused.Add(7);
            if (this.AvailableSkinNumbers08 > 0) skinsused.Add(8);
            if (this.AvailableSkinNumbers09 > 0) skinsused.Add(9);
            if (this.AvailableSkinNumbers10 > 0) skinsused.Add(10);
            if (skinsused.Count == 0) return;

            for (int loop = 0; loop < skinsused.Count; ++loop)
            {

                switch (skinsused[loop])
                {
                    case 1: this.CARSKIN01.Write(bw, this.Index, 1); break;
                    case 2: this.CARSKIN02.Write(bw, this.Index, 2); break;
                    case 3: this.CARSKIN03.Write(bw, this.Index, 3); break;
                    case 4: this.CARSKIN04.Write(bw, this.Index, 4); break;
                    case 5: this.CARSKIN05.Write(bw, this.Index, 5); break;
                    case 6: this.CARSKIN06.Write(bw, this.Index, 6); break;
                    case 7: this.CARSKIN07.Write(bw, this.Index, 7); break;
                    case 8: this.CARSKIN08.Write(bw, this.Index, 8); break;
                    case 9: this.CARSKIN09.Write(bw, this.Index, 9); break;
                    case 10: this.CARSKIN10.Write(bw, this.Index, 10); break;
                    default: break;
                }
            
            }
        }

        /// <summary>
        /// Reads a <see cref="CarSkin"/> using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void SetCarSkins(BinaryReader br)
		{
            var skin = new CarSkin();
            skin.Read(br, out int id, out int index);

            switch (index)
			{
                case 1: this.CARSKIN01.CloneValuesFrom(skin); break;
                case 2: this.CARSKIN02.CloneValuesFrom(skin); break;
                case 3: this.CARSKIN03.CloneValuesFrom(skin); break;
                case 4: this.CARSKIN04.CloneValuesFrom(skin); break;
                case 5: this.CARSKIN05.CloneValuesFrom(skin); break;
                case 6: this.CARSKIN06.CloneValuesFrom(skin); break;
                case 7: this.CARSKIN07.CloneValuesFrom(skin); break;
                case 8: this.CARSKIN08.CloneValuesFrom(skin); break;
                case 9: this.CARSKIN09.CloneValuesFrom(skin); break;
                case 10: this.CARSKIN10.CloneValuesFrom(skin); break;
                default: break;
            }
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="CarTypeInfo"/> 
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
            using (var ms = new MemoryStream(0x1000))
            using (var writer = new BinaryWriter(ms))
            {

                this.Assemble(writer);
                this.GetCarSkins(writer);

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
            var size = br.ReadInt32();
            var array = br.ReadBytes(size);

            array = Interop.Decompress(array);

            using var ms = new MemoryStream(array);
            using var reader = new BinaryReader(ms);

            this.Disassemble(reader);

            while (reader.BaseStream.Position < reader.BaseStream.Length)
			{

                this.SetCarSkins(reader);

			}

        }

        #endregion
    }
}