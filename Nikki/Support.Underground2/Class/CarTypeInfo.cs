using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Parts.CarParts;
using Nikki.Support.Underground2.Parts.InfoParts;
using CoreExtensions.IO;



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

        private const float _float_1pt0 = 1;
        private const float _float_2pt5 = 2.5F;
        private const float _float_17pt0 = 17;
        private const int _int32_6 = 6;
        private ushort[] _rigid_controls;

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
                if (!this.Deletable)
                    throw new CollectionExistenceException("CollectionName of a non-addon car cannot be changed.");
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("This value cannot be left empty.");
                if (value.Contains(" "))
                    throw new Exception("CollectionName cannot contain whitespace.");
                if (value.Length > MaxCNameLength)
                    throw new ArgumentLengthException(MaxCNameLength);
                if (this.Database.CarTypeInfos.FindCollection(value) != null)
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
        /// Manufacturer name of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public override string ManufacturerName { get; set; }

        /// <summary>
        /// Default base paint of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public override string DefaultBasePaint { get; set; } = String.Empty;

        /// <summary>
        /// Second default base paint of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public string DefaultBasePaint2 { get; set; } = String.Empty;

        /// <summary>
        /// Spoiler type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public string Spoiler { get; set; } = String.Empty;

        /// <summary>
        /// RoofScoop type of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public string RoofScoop { get; set; } = String.Empty;

        /// <summary>
        /// Mirrors typ of the <see cref="CarTypeInfo"/>.
        /// </summary>
        public string Mirrors { get; set; } = String.Empty;

        /// <summary>
        /// Defines whether the car is an SUV.
        /// </summary>
        [AccessModifiable()]
        public eBoolean IsSUV { get; set; }

        /// <summary>
        /// X value of unknown vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float UnknownVectorValX { get; set; }

        /// <summary>
        /// Y value of unknown vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float UnknownVectorValY { get; set; }

        /// <summary>
        /// Z value of unknown vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float UnknownVectorValZ { get; set; }

        /// <summary>
        /// W value of unknown vector.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        public float UnknownVectorValW { get; set; }

        /// <summary>
        /// Car skin 1.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN01 { get; set; }

        /// <summary>
        /// Car skin 2.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN02 { get; set; }

        /// <summary>
        /// Car skin 3.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN03 { get; set; }

        /// <summary>
        /// Car skin 4.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN04 { get; set; }

        /// <summary>
        /// Car skin 5.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN05 { get; set; }

        /// <summary>
        /// Car skin 6.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN06 { get; set; }

        /// <summary>
        /// Car skin 7.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN07 { get; set; }

        /// <summary>
        /// Car skin 8.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN08 { get; set; }

        /// <summary>
        /// Car skin 9.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN09 { get; set; }

        /// <summary>
        /// Car skin 10.
        /// </summary>
        [Expandable("CarSkins")]
        public CarSkin CARSKIN10 { get; set; }

        /// <summary>
        /// Ecar settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("General")]
        public Ecar ECAR { get; set; }

        /// <summary>
        /// Pvehicle settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("General")]
        public Pvehicle PVEHICLE { get; set; }

        /// <summary>
        /// Front left wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_FRONT_LEFT { get; set; }

        /// <summary>
        /// Front right wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_FRONT_RIGHT { get; set; }

        /// <summary>
        /// Rear right wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_REAR_RIGHT { get; set; }

        /// <summary>
        /// Rear left wheel settings of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("Wheels")]
        public CarInfoWheel WHEEL_REAR_LEFT { get; set; }

        /// <summary>
        /// Far player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_FAR { get; set; }

        /// <summary>
        /// Close player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_CLOSE { get; set; }

        /// <summary>
        /// Bumper player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_BUMPER { get; set; }

        /// <summary>
        /// Driver player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_DRIVER { get; set; }

        /// <summary>
        /// Hood player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_HOOD { get; set; }

        /// <summary>
        /// Drift player camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("PlayerCamera")]
        public Camera PLAYER_CAMERA_DRIFT { get; set; }

        /// <summary>
        /// Far AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("AICamera")]
        public Camera AI_CAMERA_FAR { get; set; }

        /// <summary>
        /// Close AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("AICamera")]
        public Camera AI_CAMERA_CLOSE { get; set; }

        /// <summary>
        /// Bumper AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("AICamera")]
        public Camera AI_CAMERA_BUMPER { get; set; }

        /// <summary>
        /// Driver AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("AICamera")]
        public Camera AI_CAMERA_DRIVER { get; set; }

        /// <summary>
        /// Hood AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("AICamera")]
        public Camera AI_CAMERA_HOOD { get; set; }

        /// <summary>
        /// Drift AI camera of this <see cref="CarTypeInfo"/>.
        /// </summary>
        [Expandable("AICamera")]
        public Camera AI_CAMERA_DRIFT { get; set; }

        /// <summary>
        /// Base tires performance settings.
        /// </summary>
        [Expandable("Tires")]
        public Tires BASE_TIRES { get; set; }

        /// <summary>
        /// Top tires performance settings.
        /// </summary>
        [Expandable("Tires")]
        public Tires TOP_TIRES { get; set; }

        /// <summary>
        /// Base suspension performance settings.
        /// </summary>
        [Expandable("Suspension")]
        public Suspension BASE_SUSPENSION { get; set; }

        /// <summary>
        /// Top suspension performance settings.
        /// </summary>
        [Expandable("Suspension")]
        public Suspension TOP_SUSPENSION { get; set; }

        /// <summary>
        /// Base transmission performance settings.
        /// </summary>
        [Expandable("Transmission")]
        public Transmission BASE_TRANSMISSION { get; set; }

        /// <summary>
        /// Street transmission performance settings.
        /// </summary>
        [Expandable("Transmission")]
        public Transmission STREET_TRANSMISSION { get; set; }

        /// <summary>
        /// Pro transmission performance settings.
        /// </summary>
        [Expandable("Transmission")]
        public Transmission PRO_TRANSMISSION { get; set; }

        /// <summary>
        /// Top transmission performance settings.
        /// </summary>
        [Expandable("Transmission")]
        public Transmission TOP_TRANSMISSION { get; set; }

        /// <summary>
        /// Base RPM performance settings.
        /// </summary>
        [Expandable("RPM")]
        public RPM BASE_RPM { get; set; }

        /// <summary>
        /// Street RPM performance settings.
        /// </summary>
        [Expandable("RPM")]
        public RPM STREET_RPM { get; set; }

        /// <summary>
        /// Pro RPM performance settings.
        /// </summary>
        [Expandable("RPM")]
        public RPM PRO_RPM { get; set; }

        /// <summary>
        /// Top RPM performance settings.
        /// </summary>
        [Expandable("RPM")]
        public RPM TOP_RPM { get; set; }

        /// <summary>
        /// Street ECU performance settings.
        /// </summary>
        [Expandable("ECU")]
        public ECU STREET_ECU { get; set; }

        /// <summary>
        /// Pro ECU performance settings.
        /// </summary>
        [Expandable("ECU")]
        public ECU PRO_ECU { get; set; }

        /// <summary>
        /// Top ECU performance settings.
        /// </summary>
        [Expandable("ECU")]
        public ECU TOP_ECU { get; set; }

        /// <summary>
        /// Base engine performance settings.
        /// </summary>
        [Expandable("Engine")]
        public Engine BASE_ENGINE { get; set; }

        /// <summary>
        /// Top engine performance settings.
        /// </summary>
        [Expandable("Engine")]
        public Engine TOP_ENGINE { get; set; }

        /// <summary>
        /// Base turbo performance settings.
        /// </summary>
        [Expandable("Turbo")]
        public Turbo BASE_TURBO { get; set; }

        /// <summary>
        /// Top turbo performance settings.
        /// </summary>
        [Expandable("Turbo")]
        public Turbo TOP_TURBO { get; set; }

        /// <summary>
        /// Base brakes performance settings.
        /// </summary>
        [Expandable("Brakes")]
        public Brakes BASE_BRAKES { get; set; }

        /// <summary>
        /// Top brakes performance settings.
        /// </summary>
        [Expandable("Brakes")]
        public Brakes TOP_BRAKES { get; set; }

        /// <summary>
        /// Top weight reduction performance settings.
        /// </summary>
        [Expandable("WeightReduction")]
        public WeightReduction TOP_WEIGHT_REDUCTION { get; set; }

        /// <summary>
        /// Top nitrous performance settings.
        /// </summary>
        [Expandable("NOS")]
        public Nitrous TOP_NITROUS { get; set; }

        /// <summary>
        /// Additional drift yaw control performance settings.
        /// </summary>
        [Expandable("DriftControl")]
        public DriftControl DRIFT_ADD_CONTROL { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        public CarTypeInfo() { }

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
        public CarTypeInfo(string CName, Database.Underground2 db)
        {
            this.Database = db;
            this.CollectionName = CName;
            this.ManufacturerName = "GENERIC";
            this.Deletable = true;
            this.WhatGame = 2;
            this.WheelOuterRadius = 26;
            this.WheelInnerRadiusMin = 17;
            this.WheelInnerRadiusMax = 20;
            this.DefaultSkinNumber = 1;
            this.Initialize();
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="CarTypeInfo"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
        public CarTypeInfo(BinaryReader br, Database.Underground2 db)
        {
            this.Database = db;
            this.Initialize();
            this.Disassemble(br);
            if (this.Index <= (int)eBoundValues.MIN_INFO_UNDERGROUND2)
                this.Deletable = false;
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~CarTypeInfo() { }

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
            bw.WriteBytes(0x40);

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
            bw.Write(this.UnknownVectorValX);
            bw.Write(this.UnknownVectorValY);
            bw.Write(this.UnknownVectorValZ);
            bw.Write(this.UnknownVectorValW);

            // Front Left Wheel
            bw.Write(this.WHEEL_FRONT_LEFT.XValue);
            bw.Write(this.WHEEL_FRONT_LEFT.Springs);
            bw.Write(this.WHEEL_FRONT_LEFT.RideHeight);
            bw.Write(this.WHEEL_FRONT_LEFT.UnknownVal);
            bw.Write(this.WHEEL_FRONT_LEFT.Diameter);
            bw.Write(this.WHEEL_FRONT_LEFT.TireSkidWidth);
            bw.Write(this.WHEEL_FRONT_LEFT.WheelID);
            bw.Write(this.WHEEL_FRONT_LEFT.YValue);
            bw.Write(this.WHEEL_FRONT_LEFT.WideBodyYValue);
            bw.WriteBytes(0xC);

            // Front Left Wheel
            bw.Write(this.WHEEL_FRONT_RIGHT.XValue);
            bw.Write(this.WHEEL_FRONT_RIGHT.Springs);
            bw.Write(this.WHEEL_FRONT_RIGHT.RideHeight);
            bw.Write(this.WHEEL_FRONT_RIGHT.UnknownVal);
            bw.Write(this.WHEEL_FRONT_RIGHT.Diameter);
            bw.Write(this.WHEEL_FRONT_RIGHT.TireSkidWidth);
            bw.Write(this.WHEEL_FRONT_RIGHT.WheelID);
            bw.Write(this.WHEEL_FRONT_RIGHT.YValue);
            bw.Write(this.WHEEL_FRONT_RIGHT.WideBodyYValue);
            bw.WriteBytes(0xC);

            // Front Left Wheel
            bw.Write(this.WHEEL_REAR_RIGHT.XValue);
            bw.Write(this.WHEEL_REAR_RIGHT.Springs);
            bw.Write(this.WHEEL_REAR_RIGHT.RideHeight);
            bw.Write(this.WHEEL_REAR_RIGHT.UnknownVal);
            bw.Write(this.WHEEL_REAR_RIGHT.Diameter);
            bw.Write(this.WHEEL_REAR_RIGHT.TireSkidWidth);
            bw.Write(this.WHEEL_REAR_RIGHT.WheelID);
            bw.Write(this.WHEEL_REAR_RIGHT.YValue);
            bw.Write(this.WHEEL_REAR_RIGHT.WideBodyYValue);
            bw.WriteBytes(0xC);

            // Front Left Wheel
            bw.Write(this.WHEEL_REAR_LEFT.XValue);
            bw.Write(this.WHEEL_REAR_LEFT.Springs);
            bw.Write(this.WHEEL_REAR_LEFT.RideHeight);
            bw.Write(this.WHEEL_REAR_LEFT.UnknownVal);
            bw.Write(this.WHEEL_REAR_LEFT.Diameter);
            bw.Write(this.WHEEL_REAR_LEFT.TireSkidWidth);
            bw.Write(this.WHEEL_REAR_LEFT.WheelID);
            bw.Write(this.WHEEL_REAR_LEFT.YValue);
            bw.Write(this.WHEEL_REAR_LEFT.WideBodyYValue);
            bw.WriteBytes(0xC);

            // Base Tires Performance
            bw.Write(this.BASE_TIRES.StaticGripScale);
            bw.Write(this.BASE_TIRES.YawSpeedScale);
            bw.Write(this.BASE_TIRES.SteeringAmplifier);
            bw.Write(this.BASE_TIRES.DynamicGripScale);
            bw.Write(this.BASE_TIRES.SteeringResponse);
            bw.WriteBytes(0xC);
            bw.Write(this.BASE_TIRES.DriftYawControl);
            bw.Write(this.BASE_TIRES.DriftCounterSteerBuildUp);
            bw.Write(this.BASE_TIRES.DriftCounterSteerReduction);
            bw.Write(this.BASE_TIRES.PowerSlideBreakThru1);
            bw.Write(this.BASE_TIRES.PowerSlideBreakThru2);
            bw.WriteBytes(0xC);

            // Pvehicle Values
            bw.Write(this.PVEHICLE.Massx1000Multiplier);
            bw.Write(this.PVEHICLE.TensorScaleX);
            bw.Write(this.PVEHICLE.TensorScaleY);
            bw.Write(this.PVEHICLE.TensorScaleZ);
            bw.Write(this.PVEHICLE.TensorScaleW);
            bw.WriteBytes(0x10);
            bw.Write(this.ECAR.EcarUnknown1);
            bw.WriteBytes(0x10);
            bw.Write(this.ECAR.EcarUnknown2);
            bw.WriteBytes(0x10);
            bw.Write(_float_1pt0);
            bw.Write(this.PVEHICLE.InitialHandlingRating);
            bw.WriteBytes(0xC);

            // Base Suspension Performance
            bw.Write(this.BASE_SUSPENSION.ShockStiffnessFront);
            bw.Write(this.BASE_SUSPENSION.ShockExtStiffnessFront);
            bw.Write(this.BASE_SUSPENSION.SpringProgressionFront);
            bw.Write(this.BASE_SUSPENSION.ShockValvingFront);
            bw.Write(this.BASE_SUSPENSION.SwayBarFront);
            bw.Write(this.BASE_SUSPENSION.TrackWidthFront);
            bw.Write(this.BASE_SUSPENSION.CounterBiasFront);
            bw.Write(this.BASE_SUSPENSION.ShockDigressionFront);
            bw.Write(this.BASE_SUSPENSION.ShockStiffnessRear);
            bw.Write(this.BASE_SUSPENSION.ShockExtStiffnessRear);
            bw.Write(this.BASE_SUSPENSION.SpringProgressionRear);
            bw.Write(this.BASE_SUSPENSION.ShockValvingRear);
            bw.Write(this.BASE_SUSPENSION.SwayBarRear);
            bw.Write(this.BASE_SUSPENSION.TrackWidthRear);
            bw.Write(this.BASE_SUSPENSION.CounterBiasRear);
            bw.Write(this.BASE_SUSPENSION.ShockDigressionRear);

            // Base Transmission Performance
            bw.Write(this.BASE_TRANSMISSION.ClutchSlip);
            bw.Write(this.BASE_TRANSMISSION.OptimalShift);
            bw.Write(this.BASE_TRANSMISSION.FinalDriveRatio);
            bw.Write(this.BASE_TRANSMISSION.FinalDriveRatio2);
            bw.Write(this.BASE_TRANSMISSION.TorqueSplit);
            bw.Write(this.BASE_TRANSMISSION.BurnoutStrength);
            bw.Write(this.BASE_TRANSMISSION.NumberOfGears);
            bw.Write(this.BASE_TRANSMISSION.GearEfficiency);
            bw.Write(this.BASE_TRANSMISSION.GearRatioR);
            bw.Write(this.BASE_TRANSMISSION.GearRatioN);
            bw.Write(this.BASE_TRANSMISSION.GearRatio1);
            bw.Write(this.BASE_TRANSMISSION.GearRatio2);
            bw.Write(this.BASE_TRANSMISSION.GearRatio3);
            bw.Write(this.BASE_TRANSMISSION.GearRatio4);
            bw.Write(this.BASE_TRANSMISSION.GearRatio5);
            bw.Write(this.BASE_TRANSMISSION.GearRatio6);

            // Base RPM Performance
            bw.Write(this.BASE_RPM.IdleRPMAdd);
            bw.Write(this.BASE_RPM.RedLineRPMAdd);
            bw.Write(this.BASE_RPM.MaxRPMAdd);

            // Base Engine Performance
            bw.Write(this.BASE_ENGINE.SpeedRefreshRate);
            bw.Write(this.BASE_ENGINE.EngineTorque1);
            bw.Write(this.BASE_ENGINE.EngineTorque2);
            bw.Write(this.BASE_ENGINE.EngineTorque3);
            bw.Write(this.BASE_ENGINE.EngineTorque4);
            bw.Write(this.BASE_ENGINE.EngineTorque5);
            bw.Write(this.BASE_ENGINE.EngineTorque6);
            bw.Write(this.BASE_ENGINE.EngineTorque7);
            bw.Write(this.BASE_ENGINE.EngineTorque8);
            bw.Write(this.BASE_ENGINE.EngineTorque9);
            bw.Write(this.BASE_ENGINE.EngineBraking1);
            bw.Write(this.BASE_ENGINE.EngineBraking2);
            bw.Write(this.BASE_ENGINE.EngineBraking3);

            // Base Turbo Performance
            bw.Write(this.BASE_TURBO.TurboBraking);
            bw.Write(this.BASE_TURBO.TurboVacuum);
            bw.Write(this.BASE_TURBO.TurboHeatHigh);
            bw.Write(this.BASE_TURBO.TurboHeatLow);
            bw.Write(this.BASE_TURBO.TurboHighBoost);
            bw.Write(this.BASE_TURBO.TurboLowBoost);
            bw.Write(this.BASE_TURBO.TurboSpool);
            bw.Write(this.BASE_TURBO.TurboSpoolTimeDown);
            bw.Write(this.BASE_TURBO.TurboSpoolTimeUp);
            bw.WriteBytes(0xC);

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
            bw.Write(_float_2pt5);
            bw.Write(_float_17pt0);
            bw.Write((int)0);
            bw.Write(this.PVEHICLE.StockTopSpeedLimiter);
            bw.WriteBytes(0x1C);

            // DriftAdditionalYawControl Performance
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl1);
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl2);
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl3);
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl4);
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl5);
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl6);
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl7);
            bw.Write(this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl8);

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
            bw.Write(this.TOP_WEIGHT_REDUCTION.WeightReductionMassMultiplier);
            bw.Write(this.TOP_WEIGHT_REDUCTION.WeightReductionGripAddon);
            bw.Write(this.TOP_WEIGHT_REDUCTION.WeightReductionHandlingRating);
            bw.Write((int)0);

            // Street Transmission Performance
            bw.Write(this.STREET_TRANSMISSION.ClutchSlip);
            bw.Write(this.STREET_TRANSMISSION.OptimalShift);
            bw.Write(this.STREET_TRANSMISSION.FinalDriveRatio);
            bw.Write(this.STREET_TRANSMISSION.FinalDriveRatio2);
            bw.Write(this.STREET_TRANSMISSION.TorqueSplit);
            bw.Write(this.STREET_TRANSMISSION.BurnoutStrength);
            bw.Write(this.STREET_TRANSMISSION.NumberOfGears);
            bw.Write(this.STREET_TRANSMISSION.GearEfficiency);
            bw.Write(this.STREET_TRANSMISSION.GearRatioR);
            bw.Write(this.STREET_TRANSMISSION.GearRatioN);
            bw.Write(this.STREET_TRANSMISSION.GearRatio1);
            bw.Write(this.STREET_TRANSMISSION.GearRatio2);
            bw.Write(this.STREET_TRANSMISSION.GearRatio3);
            bw.Write(this.STREET_TRANSMISSION.GearRatio4);
            bw.Write(this.STREET_TRANSMISSION.GearRatio5);
            bw.Write(this.STREET_TRANSMISSION.GearRatio6);

            // Pro Transmission Performance
            bw.Write(this.PRO_TRANSMISSION.ClutchSlip);
            bw.Write(this.PRO_TRANSMISSION.OptimalShift);
            bw.Write(this.PRO_TRANSMISSION.FinalDriveRatio);
            bw.Write(this.PRO_TRANSMISSION.FinalDriveRatio2);
            bw.Write(this.PRO_TRANSMISSION.TorqueSplit);
            bw.Write(this.PRO_TRANSMISSION.BurnoutStrength);
            bw.Write(this.PRO_TRANSMISSION.NumberOfGears);
            bw.Write(this.PRO_TRANSMISSION.GearEfficiency);
            bw.Write(this.PRO_TRANSMISSION.GearRatioR);
            bw.Write(this.PRO_TRANSMISSION.GearRatioN);
            bw.Write(this.PRO_TRANSMISSION.GearRatio1);
            bw.Write(this.PRO_TRANSMISSION.GearRatio2);
            bw.Write(this.PRO_TRANSMISSION.GearRatio3);
            bw.Write(this.PRO_TRANSMISSION.GearRatio4);
            bw.Write(this.PRO_TRANSMISSION.GearRatio5);
            bw.Write(this.PRO_TRANSMISSION.GearRatio6);

            // Top Transmission Performance
            bw.Write(this.TOP_TRANSMISSION.ClutchSlip);
            bw.Write(this.TOP_TRANSMISSION.OptimalShift);
            bw.Write(this.TOP_TRANSMISSION.FinalDriveRatio);
            bw.Write(this.TOP_TRANSMISSION.FinalDriveRatio2);
            bw.Write(this.TOP_TRANSMISSION.TorqueSplit);
            bw.Write(this.TOP_TRANSMISSION.BurnoutStrength);
            bw.Write(this.TOP_TRANSMISSION.NumberOfGears);
            bw.Write(this.TOP_TRANSMISSION.GearEfficiency);
            bw.Write(this.TOP_TRANSMISSION.GearRatioR);
            bw.Write(this.TOP_TRANSMISSION.GearRatioN);
            bw.Write(this.TOP_TRANSMISSION.GearRatio1);
            bw.Write(this.TOP_TRANSMISSION.GearRatio2);
            bw.Write(this.TOP_TRANSMISSION.GearRatio3);
            bw.Write(this.TOP_TRANSMISSION.GearRatio4);
            bw.Write(this.TOP_TRANSMISSION.GearRatio5);
            bw.Write(this.TOP_TRANSMISSION.GearRatio6);

            // Top Engine Performance
            bw.WriteBytes(0xC);
            bw.Write(this.TOP_ENGINE.SpeedRefreshRate);
            bw.Write(this.TOP_ENGINE.EngineTorque1);
            bw.Write(this.TOP_ENGINE.EngineTorque2);
            bw.Write(this.TOP_ENGINE.EngineTorque3);
            bw.Write(this.TOP_ENGINE.EngineTorque4);
            bw.Write(this.TOP_ENGINE.EngineTorque5);
            bw.Write(this.TOP_ENGINE.EngineTorque6);
            bw.Write(this.TOP_ENGINE.EngineTorque7);
            bw.Write(this.TOP_ENGINE.EngineTorque8);
            bw.Write(this.TOP_ENGINE.EngineTorque9);
            bw.Write(this.TOP_ENGINE.EngineBraking1);
            bw.Write(this.TOP_ENGINE.EngineBraking2);
            bw.Write(this.TOP_ENGINE.EngineBraking3);

            // Street RPM Performance
            bw.Write(this.STREET_RPM.IdleRPMAdd);
            bw.Write(this.STREET_RPM.RedLineRPMAdd);
            bw.Write(this.STREET_RPM.MaxRPMAdd);
            bw.Write(this.TOP_ENGINE.SpeedRefreshRate / 3);

            // Street ECU Performance
            bw.Write(this.STREET_ECU.ECUx1000Add);
            bw.Write(this.STREET_ECU.ECUx2000Add);
            bw.Write(this.STREET_ECU.ECUx3000Add);
            bw.Write(this.STREET_ECU.ECUx4000Add);
            bw.Write(this.STREET_ECU.ECUx5000Add);
            bw.Write(this.STREET_ECU.ECUx6000Add);
            bw.Write(this.STREET_ECU.ECUx7000Add);
            bw.Write(this.STREET_ECU.ECUx8000Add);
            bw.Write(this.STREET_ECU.ECUx9000Add);
            bw.Write(this.STREET_ECU.ECUx10000Add);
            bw.Write(this.STREET_ECU.ECUx11000Add);
            bw.Write(this.STREET_ECU.ECUx12000Add);

            // Pro RPM Performance
            bw.Write(this.PRO_RPM.IdleRPMAdd);
            bw.Write(this.PRO_RPM.RedLineRPMAdd);
            bw.Write(this.PRO_RPM.MaxRPMAdd);
            bw.Write(this.TOP_ENGINE.SpeedRefreshRate * 2 / 3);

            // Pro ECU Performance
            bw.Write(this.PRO_ECU.ECUx1000Add);
            bw.Write(this.PRO_ECU.ECUx2000Add);
            bw.Write(this.PRO_ECU.ECUx3000Add);
            bw.Write(this.PRO_ECU.ECUx4000Add);
            bw.Write(this.PRO_ECU.ECUx5000Add);
            bw.Write(this.PRO_ECU.ECUx6000Add);
            bw.Write(this.PRO_ECU.ECUx7000Add);
            bw.Write(this.PRO_ECU.ECUx8000Add);
            bw.Write(this.PRO_ECU.ECUx9000Add);
            bw.Write(this.PRO_ECU.ECUx10000Add);
            bw.Write(this.PRO_ECU.ECUx11000Add);
            bw.Write(this.PRO_ECU.ECUx12000Add);

            // Top RPM Performance
            bw.Write(this.TOP_RPM.IdleRPMAdd);
            bw.Write(this.TOP_RPM.RedLineRPMAdd);
            bw.Write(this.TOP_RPM.MaxRPMAdd);
            bw.Write(this.TOP_ENGINE.SpeedRefreshRate);

            // Top ECU Performance
            bw.Write(this.TOP_ECU.ECUx1000Add);
            bw.Write(this.TOP_ECU.ECUx2000Add);
            bw.Write(this.TOP_ECU.ECUx3000Add);
            bw.Write(this.TOP_ECU.ECUx4000Add);
            bw.Write(this.TOP_ECU.ECUx5000Add);
            bw.Write(this.TOP_ECU.ECUx6000Add);
            bw.Write(this.TOP_ECU.ECUx7000Add);
            bw.Write(this.TOP_ECU.ECUx8000Add);
            bw.Write(this.TOP_ECU.ECUx9000Add);
            bw.Write(this.TOP_ECU.ECUx10000Add);
            bw.Write(this.TOP_ECU.ECUx11000Add);
            bw.Write(this.TOP_ECU.ECUx12000Add);

            // Top Turbo Performance
            bw.Write(this.TOP_TURBO.TurboBraking);
            bw.Write(this.TOP_TURBO.TurboVacuum);
            bw.Write(this.TOP_TURBO.TurboHeatHigh);
            bw.Write(this.TOP_TURBO.TurboHeatLow);
            bw.Write(this.TOP_TURBO.TurboHighBoost);
            bw.Write(this.TOP_TURBO.TurboLowBoost);
            bw.Write(this.TOP_TURBO.TurboSpool);
            bw.Write(this.TOP_TURBO.TurboSpoolTimeDown);
            bw.Write(this.TOP_TURBO.TurboSpoolTimeUp);
            bw.WriteBytes(0xC);

            // Top Tires Performance
            bw.Write(this.TOP_TIRES.StaticGripScale);
            bw.Write(this.TOP_TIRES.YawSpeedScale);
            bw.Write(this.TOP_TIRES.SteeringAmplifier);
            bw.Write(this.TOP_TIRES.DynamicGripScale);
            bw.Write(this.TOP_TIRES.SteeringResponse);
            bw.WriteBytes(0xC);
            bw.Write(this.TOP_TIRES.DriftYawControl);
            bw.Write(this.TOP_TIRES.DriftCounterSteerBuildUp);
            bw.Write(this.TOP_TIRES.DriftCounterSteerReduction);
            bw.Write(this.TOP_TIRES.PowerSlideBreakThru1);
            bw.Write(this.TOP_TIRES.PowerSlideBreakThru2);
            bw.WriteBytes(0xC);

            // Top Nitrous Performance
            bw.Write(this.TOP_NITROUS.NOSCapacity);
            bw.Write(_int32_6);
            bw.Write((int)0);
            bw.Write(this.TOP_NITROUS.NOSTorqueBoost);

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
            bw.Write(this.TOP_SUSPENSION.ShockStiffnessFront);
            bw.Write(this.TOP_SUSPENSION.ShockExtStiffnessFront);
            bw.Write(this.TOP_SUSPENSION.SpringProgressionFront);
            bw.Write(this.TOP_SUSPENSION.ShockValvingFront);
            bw.Write(this.TOP_SUSPENSION.SwayBarFront);
            bw.Write(this.TOP_SUSPENSION.TrackWidthFront);
            bw.Write(this.TOP_SUSPENSION.CounterBiasFront);
            bw.Write(this.TOP_SUSPENSION.ShockDigressionFront);
            bw.Write(this.TOP_SUSPENSION.ShockStiffnessRear);
            bw.Write(this.TOP_SUSPENSION.ShockExtStiffnessRear);
            bw.Write(this.TOP_SUSPENSION.SpringProgressionRear);
            bw.Write(this.TOP_SUSPENSION.ShockValvingRear);
            bw.Write(this.TOP_SUSPENSION.SwayBarRear);
            bw.Write(this.TOP_SUSPENSION.TrackWidthRear);
            bw.Write(this.TOP_SUSPENSION.CounterBiasRear);
            bw.Write(this.TOP_SUSPENSION.ShockDigressionRear);

            bw.Write(this.ECAR.HandlingBuffer);
            bw.Write(this.ECAR.TopSuspFrontHeightReduce);
            bw.Write(this.ECAR.TopSuspRearHeightReduce);
            bw.Write((int)0);
            bw.Write(this.ECAR.NumPlayerCameras);
            bw.Write(this.ECAR.NumAICameras);
            bw.Write((long)0);

            // Player Cameras
            bw.Write((short)eCameraType.FAR);
            bw.Write((short)(this.PLAYER_CAMERA_FAR.CameraAngle / 180 * 32768));
            bw.Write(this.PLAYER_CAMERA_FAR.CameraLag);
            bw.Write(this.PLAYER_CAMERA_FAR.CameraHeight);
            bw.Write(this.PLAYER_CAMERA_FAR.CameraLatOffset);
            bw.Write((short)eCameraType.CLOSE);
            bw.Write((short)(this.PLAYER_CAMERA_CLOSE.CameraAngle / 180 * 32768));
            bw.Write(this.PLAYER_CAMERA_CLOSE.CameraLag);
            bw.Write(this.PLAYER_CAMERA_CLOSE.CameraHeight);
            bw.Write(this.PLAYER_CAMERA_CLOSE.CameraLatOffset);
            bw.Write((short)eCameraType.BUMPER);
            bw.Write((short)(this.PLAYER_CAMERA_BUMPER.CameraAngle / 180 * 32768));
            bw.Write(this.PLAYER_CAMERA_BUMPER.CameraLag);
            bw.Write(this.PLAYER_CAMERA_BUMPER.CameraHeight);
            bw.Write(this.PLAYER_CAMERA_BUMPER.CameraLatOffset);
            bw.Write((short)eCameraType.DRIVER);
            bw.Write((short)(this.PLAYER_CAMERA_DRIVER.CameraAngle / 180 * 32768));
            bw.Write(this.PLAYER_CAMERA_DRIVER.CameraLag);
            bw.Write(this.PLAYER_CAMERA_DRIVER.CameraHeight);
            bw.Write(this.PLAYER_CAMERA_DRIVER.CameraLatOffset);
            bw.Write((short)eCameraType.HOOD);
            bw.Write((short)(this.PLAYER_CAMERA_HOOD.CameraAngle / 180 * 32768));
            bw.Write(this.PLAYER_CAMERA_HOOD.CameraLag);
            bw.Write(this.PLAYER_CAMERA_HOOD.CameraHeight);
            bw.Write(this.PLAYER_CAMERA_HOOD.CameraLatOffset);
            bw.Write((short)eCameraType.DRIFT);
            bw.Write((short)(this.PLAYER_CAMERA_DRIFT.CameraAngle / 180 * 32768));
            bw.Write(this.PLAYER_CAMERA_DRIFT.CameraLag);
            bw.Write(this.PLAYER_CAMERA_DRIFT.CameraHeight);
            bw.Write(this.PLAYER_CAMERA_DRIFT.CameraLatOffset);

            // AI Cameras
            bw.Write((short)eCameraType.FAR);
            bw.Write((short)(this.AI_CAMERA_FAR.CameraAngle / 180 * 32768));
            bw.Write(this.AI_CAMERA_FAR.CameraLag);
            bw.Write(this.AI_CAMERA_FAR.CameraHeight);
            bw.Write(this.AI_CAMERA_FAR.CameraLatOffset);
            bw.Write((short)eCameraType.CLOSE);
            bw.Write((short)(this.AI_CAMERA_CLOSE.CameraAngle / 180 * 32768));
            bw.Write(this.AI_CAMERA_CLOSE.CameraLag);
            bw.Write(this.AI_CAMERA_CLOSE.CameraHeight);
            bw.Write(this.AI_CAMERA_CLOSE.CameraLatOffset);
            bw.Write((short)eCameraType.BUMPER);
            bw.Write((short)(this.AI_CAMERA_BUMPER.CameraAngle / 180 * 32768));
            bw.Write(this.AI_CAMERA_BUMPER.CameraLag);
            bw.Write(this.AI_CAMERA_BUMPER.CameraHeight);
            bw.Write(this.AI_CAMERA_BUMPER.CameraLatOffset);
            bw.Write((short)eCameraType.DRIVER);
            bw.Write((short)(this.AI_CAMERA_DRIVER.CameraAngle / 180 * 32768));
            bw.Write(this.AI_CAMERA_DRIVER.CameraLag);
            bw.Write(this.AI_CAMERA_DRIVER.CameraHeight);
            bw.Write(this.AI_CAMERA_DRIVER.CameraLatOffset);
            bw.Write((short)eCameraType.HOOD);
            bw.Write((short)(this.AI_CAMERA_HOOD.CameraAngle / 180 * 32768));
            bw.Write(this.AI_CAMERA_HOOD.CameraLag);
            bw.Write(this.AI_CAMERA_HOOD.CameraHeight);
            bw.Write(this.AI_CAMERA_HOOD.CameraLatOffset);
            bw.Write((short)eCameraType.DRIFT);
            bw.Write((short)(this.AI_CAMERA_DRIFT.CameraAngle / 180 * 32768));
            bw.Write(this.AI_CAMERA_DRIFT.CameraLag);
            bw.Write(this.AI_CAMERA_DRIFT.CameraHeight);
            bw.Write(this.AI_CAMERA_DRIFT.CameraLatOffset);

            // Rigid Controls (if an added car, or usagetype modified, or rigid controls are missing or broken
            if (this.Deletable || this._rigid_controls == null || this._rigid_controls.Length != 40)
            {
                if (this.UsageType == eUsageType.Traffic)
                {
                    for (int a1 = 0; a1 < 40; ++a1)
                        bw.Write(RigidControls.RigidTrafControls[a1]);
                }
                else
                {
                    for (int a1 = 0; a1 < 40; ++a1)
                        bw.Write(RigidControls.RigidRacerControls[a1]);
                }
            }
            else
            {
                for (int a1 = 0; a1 < 40; ++a1)
                    bw.Write(this._rigid_controls[a1]);
            }
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
            this.UnknownVectorValX = br.ReadSingle();
            this.UnknownVectorValY = br.ReadSingle();
            this.UnknownVectorValZ = br.ReadSingle();
            this.UnknownVectorValW = br.ReadSingle();

            // Front Left Wheel
            this.WHEEL_FRONT_LEFT.XValue = br.ReadSingle();
            this.WHEEL_FRONT_LEFT.Springs = br.ReadSingle();
            this.WHEEL_FRONT_LEFT.RideHeight = br.ReadSingle();
            this.WHEEL_FRONT_LEFT.UnknownVal = br.ReadSingle();
            this.WHEEL_FRONT_LEFT.Diameter = br.ReadSingle();
            this.WHEEL_FRONT_LEFT.TireSkidWidth = br.ReadSingle();
            this.WHEEL_FRONT_LEFT.WheelID = br.ReadInt32();
            this.WHEEL_FRONT_LEFT.YValue = br.ReadSingle();
            this.WHEEL_FRONT_LEFT.WideBodyYValue = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Front Left Wheel
            this.WHEEL_FRONT_RIGHT.XValue = br.ReadSingle();
            this.WHEEL_FRONT_RIGHT.Springs = br.ReadSingle();
            this.WHEEL_FRONT_RIGHT.RideHeight = br.ReadSingle();
            this.WHEEL_FRONT_RIGHT.UnknownVal = br.ReadSingle();
            this.WHEEL_FRONT_RIGHT.Diameter = br.ReadSingle();
            this.WHEEL_FRONT_RIGHT.TireSkidWidth = br.ReadSingle();
            this.WHEEL_FRONT_RIGHT.WheelID = br.ReadInt32();
            this.WHEEL_FRONT_RIGHT.YValue = br.ReadSingle();
            this.WHEEL_FRONT_RIGHT.WideBodyYValue = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Front Left Wheel
            this.WHEEL_REAR_RIGHT.XValue = br.ReadSingle();
            this.WHEEL_REAR_RIGHT.Springs = br.ReadSingle();
            this.WHEEL_REAR_RIGHT.RideHeight = br.ReadSingle();
            this.WHEEL_REAR_RIGHT.UnknownVal = br.ReadSingle();
            this.WHEEL_REAR_RIGHT.Diameter = br.ReadSingle();
            this.WHEEL_REAR_RIGHT.TireSkidWidth = br.ReadSingle();
            this.WHEEL_REAR_RIGHT.WheelID = br.ReadInt32();
            this.WHEEL_REAR_RIGHT.YValue = br.ReadSingle();
            this.WHEEL_REAR_RIGHT.WideBodyYValue = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Front Left Wheel
            this.WHEEL_REAR_LEFT.XValue = br.ReadSingle();
            this.WHEEL_REAR_LEFT.Springs = br.ReadSingle();
            this.WHEEL_REAR_LEFT.RideHeight = br.ReadSingle();
            this.WHEEL_REAR_LEFT.UnknownVal = br.ReadSingle();
            this.WHEEL_REAR_LEFT.Diameter = br.ReadSingle();
            this.WHEEL_REAR_LEFT.TireSkidWidth = br.ReadSingle();
            this.WHEEL_REAR_LEFT.WheelID = br.ReadInt32();
            this.WHEEL_REAR_LEFT.YValue = br.ReadSingle();
            this.WHEEL_REAR_LEFT.WideBodyYValue = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Base Tires Performance
            this.BASE_TIRES.StaticGripScale = br.ReadSingle();
            this.BASE_TIRES.YawSpeedScale = br.ReadSingle();
            this.BASE_TIRES.SteeringAmplifier = br.ReadSingle();
            this.BASE_TIRES.DynamicGripScale = br.ReadSingle();
            this.BASE_TIRES.SteeringResponse = br.ReadSingle();
            br.BaseStream.Position += 0xC;
            this.BASE_TIRES.DriftYawControl = br.ReadSingle();
            this.BASE_TIRES.DriftCounterSteerBuildUp = br.ReadSingle();
            this.BASE_TIRES.DriftCounterSteerReduction = br.ReadSingle();
            this.BASE_TIRES.PowerSlideBreakThru1 = br.ReadSingle();
            this.BASE_TIRES.PowerSlideBreakThru2 = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Pvehicle and Ecar Values
            this.PVEHICLE.Massx1000Multiplier = br.ReadSingle();
            this.PVEHICLE.TensorScaleX = br.ReadSingle();
            this.PVEHICLE.TensorScaleY = br.ReadSingle();
            this.PVEHICLE.TensorScaleZ = br.ReadSingle();
            this.PVEHICLE.TensorScaleW = br.ReadSingle();
            br.BaseStream.Position += 0x10;
            this.ECAR.EcarUnknown1 = br.ReadSingle();
            br.BaseStream.Position += 0x10;
            this.ECAR.EcarUnknown2 = br.ReadSingle();
            br.BaseStream.Position += 0x14;
            this.PVEHICLE.InitialHandlingRating = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Base Suspension Performance
            this.BASE_SUSPENSION.ShockStiffnessFront = br.ReadSingle();
            this.BASE_SUSPENSION.ShockExtStiffnessFront = br.ReadSingle();
            this.BASE_SUSPENSION.SpringProgressionFront = br.ReadSingle();
            this.BASE_SUSPENSION.ShockValvingFront = br.ReadSingle();
            this.BASE_SUSPENSION.SwayBarFront = br.ReadSingle();
            this.BASE_SUSPENSION.TrackWidthFront = br.ReadSingle();
            this.BASE_SUSPENSION.CounterBiasFront = br.ReadSingle();
            this.BASE_SUSPENSION.ShockDigressionFront = br.ReadSingle();
            this.BASE_SUSPENSION.ShockStiffnessRear = br.ReadSingle();
            this.BASE_SUSPENSION.ShockExtStiffnessRear = br.ReadSingle();
            this.BASE_SUSPENSION.SpringProgressionRear = br.ReadSingle();
            this.BASE_SUSPENSION.ShockValvingRear = br.ReadSingle();
            this.BASE_SUSPENSION.SwayBarRear = br.ReadSingle();
            this.BASE_SUSPENSION.TrackWidthRear = br.ReadSingle();
            this.BASE_SUSPENSION.CounterBiasRear = br.ReadSingle();
            this.BASE_SUSPENSION.ShockDigressionRear = br.ReadSingle();

            // Base Transmission Performance
            this.BASE_TRANSMISSION.ClutchSlip = br.ReadSingle();
            this.BASE_TRANSMISSION.OptimalShift = br.ReadSingle();
            this.BASE_TRANSMISSION.FinalDriveRatio = br.ReadSingle();
            this.BASE_TRANSMISSION.FinalDriveRatio2 = br.ReadSingle();
            this.BASE_TRANSMISSION.TorqueSplit = br.ReadSingle();
            this.BASE_TRANSMISSION.BurnoutStrength = br.ReadSingle();
            this.BASE_TRANSMISSION.NumberOfGears = br.ReadInt32();
            this.BASE_TRANSMISSION.GearEfficiency = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatioR = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatioN = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatio1 = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatio2 = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatio3 = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatio4 = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatio5 = br.ReadSingle();
            this.BASE_TRANSMISSION.GearRatio6 = br.ReadSingle();

            // Base RPM Performance
            this.BASE_RPM.IdleRPMAdd = br.ReadSingle();
            this.BASE_RPM.RedLineRPMAdd = br.ReadSingle();
            this.BASE_RPM.MaxRPMAdd = br.ReadSingle();

            // Base Engine Performance
            this.BASE_ENGINE.SpeedRefreshRate = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque1 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque2 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque3 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque4 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque5 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque6 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque7 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque8 = br.ReadSingle();
            this.BASE_ENGINE.EngineTorque9 = br.ReadSingle();
            this.BASE_ENGINE.EngineBraking1 = br.ReadSingle();
            this.BASE_ENGINE.EngineBraking2 = br.ReadSingle();
            this.BASE_ENGINE.EngineBraking3 = br.ReadSingle();

            // Base Turbo Performance
            this.BASE_TURBO.TurboBraking = br.ReadSingle();
            this.BASE_TURBO.TurboVacuum = br.ReadSingle();
            this.BASE_TURBO.TurboHeatHigh = br.ReadSingle();
            this.BASE_TURBO.TurboHeatLow = br.ReadSingle();
            this.BASE_TURBO.TurboHighBoost = br.ReadSingle();
            this.BASE_TURBO.TurboLowBoost = br.ReadSingle();
            this.BASE_TURBO.TurboSpool = br.ReadSingle();
            this.BASE_TURBO.TurboSpoolTimeDown = br.ReadSingle();
            this.BASE_TURBO.TurboSpoolTimeUp = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Base Brakes Performance
            this.PVEHICLE.TopSpeedUnderflow = br.ReadSingle();
            this.BASE_BRAKES.FrontDownForce = br.ReadSingle();
            this.BASE_BRAKES.RearDownForce = br.ReadSingle();
            this.BASE_BRAKES.BumpJumpForce = br.ReadSingle();
            this.BASE_BRAKES.SteeringRatio = br.ReadSingle();
            this.BASE_BRAKES.BrakeStrength = br.ReadSingle();
            this.BASE_BRAKES.HandBrakeStrength = br.ReadSingle();
            this.BASE_BRAKES.BrakeBias = br.ReadSingle();
            br.BaseStream.Position += 0x10;
            this.PVEHICLE.StockTopSpeedLimiter = br.ReadSingle();
            br.BaseStream.Position += 0x1C;

            // DriftAdditionalYawControl Performance
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl1 = br.ReadSingle();
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl2 = br.ReadSingle();
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl3 = br.ReadSingle();
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl4 = br.ReadSingle();
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl5 = br.ReadSingle();
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl6 = br.ReadSingle();
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl7 = br.ReadSingle();
            this.DRIFT_ADD_CONTROL.DriftAdditionalYawControl8 = br.ReadSingle();

            // Skip Street + Pro Engine and Street Turbo, 0x03E0 - 0x0450
            br.BaseStream.Position += 0x70;

            // Top Weight Reduction Performance
            this.TOP_WEIGHT_REDUCTION.WeightReductionMassMultiplier = br.ReadSingle();
            this.TOP_WEIGHT_REDUCTION.WeightReductionGripAddon = br.ReadSingle();
            this.TOP_WEIGHT_REDUCTION.WeightReductionHandlingRating = br.ReadSingle();
            br.BaseStream.Position += 4;

            // Street Transmission Performance
            this.STREET_TRANSMISSION.ClutchSlip = br.ReadSingle();
            this.STREET_TRANSMISSION.OptimalShift = br.ReadSingle();
            this.STREET_TRANSMISSION.FinalDriveRatio = br.ReadSingle();
            this.STREET_TRANSMISSION.FinalDriveRatio2 = br.ReadSingle();
            this.STREET_TRANSMISSION.TorqueSplit = br.ReadSingle();
            this.STREET_TRANSMISSION.BurnoutStrength = br.ReadSingle();
            this.STREET_TRANSMISSION.NumberOfGears = br.ReadInt32();
            this.STREET_TRANSMISSION.GearEfficiency = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatioR = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatioN = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatio1 = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatio2 = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatio3 = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatio4 = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatio5 = br.ReadSingle();
            this.STREET_TRANSMISSION.GearRatio6 = br.ReadSingle();

            // Pro Transmission Performance
            this.PRO_TRANSMISSION.ClutchSlip = br.ReadSingle();
            this.PRO_TRANSMISSION.OptimalShift = br.ReadSingle();
            this.PRO_TRANSMISSION.FinalDriveRatio = br.ReadSingle();
            this.PRO_TRANSMISSION.FinalDriveRatio2 = br.ReadSingle();
            this.PRO_TRANSMISSION.TorqueSplit = br.ReadSingle();
            this.PRO_TRANSMISSION.BurnoutStrength = br.ReadSingle();
            this.PRO_TRANSMISSION.NumberOfGears = br.ReadInt32();
            this.PRO_TRANSMISSION.GearEfficiency = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatioR = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatioN = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatio1 = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatio2 = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatio3 = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatio4 = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatio5 = br.ReadSingle();
            this.PRO_TRANSMISSION.GearRatio6 = br.ReadSingle();

            // Top Transmission Performance
            this.TOP_TRANSMISSION.ClutchSlip = br.ReadSingle();
            this.TOP_TRANSMISSION.OptimalShift = br.ReadSingle();
            this.TOP_TRANSMISSION.FinalDriveRatio = br.ReadSingle();
            this.TOP_TRANSMISSION.FinalDriveRatio2 = br.ReadSingle();
            this.TOP_TRANSMISSION.TorqueSplit = br.ReadSingle();
            this.TOP_TRANSMISSION.BurnoutStrength = br.ReadSingle();
            this.TOP_TRANSMISSION.NumberOfGears = br.ReadInt32();
            this.TOP_TRANSMISSION.GearEfficiency = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatioR = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatioN = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatio1 = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatio2 = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatio3 = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatio4 = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatio5 = br.ReadSingle();
            this.TOP_TRANSMISSION.GearRatio6 = br.ReadSingle();

            // Top Engine Performance
            br.BaseStream.Position += 0xC;
            this.TOP_ENGINE.SpeedRefreshRate = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque1 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque2 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque3 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque4 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque5 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque6 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque7 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque8 = br.ReadSingle();
            this.TOP_ENGINE.EngineTorque9 = br.ReadSingle();
            this.TOP_ENGINE.EngineBraking1 = br.ReadSingle();
            this.TOP_ENGINE.EngineBraking2 = br.ReadSingle();
            this.TOP_ENGINE.EngineBraking3 = br.ReadSingle();

            // Street RPM Performance
            this.STREET_RPM.IdleRPMAdd = br.ReadSingle();
            this.STREET_RPM.RedLineRPMAdd = br.ReadSingle();
            this.STREET_RPM.MaxRPMAdd = br.ReadSingle();
            br.BaseStream.Position += 4;

            // Street ECU Performance
            this.STREET_ECU.ECUx1000Add = br.ReadSingle();
            this.STREET_ECU.ECUx2000Add = br.ReadSingle();
            this.STREET_ECU.ECUx3000Add = br.ReadSingle();
            this.STREET_ECU.ECUx4000Add = br.ReadSingle();
            this.STREET_ECU.ECUx5000Add = br.ReadSingle();
            this.STREET_ECU.ECUx6000Add = br.ReadSingle();
            this.STREET_ECU.ECUx7000Add = br.ReadSingle();
            this.STREET_ECU.ECUx8000Add = br.ReadSingle();
            this.STREET_ECU.ECUx9000Add = br.ReadSingle();
            this.STREET_ECU.ECUx10000Add = br.ReadSingle();
            this.STREET_ECU.ECUx11000Add = br.ReadSingle();
            this.STREET_ECU.ECUx12000Add = br.ReadSingle();

            // Pro RPM Performance
            this.PRO_RPM.IdleRPMAdd = br.ReadSingle();
            this.PRO_RPM.RedLineRPMAdd = br.ReadSingle();
            this.PRO_RPM.MaxRPMAdd = br.ReadSingle();
            br.BaseStream.Position += 4;

            // Pro ECU Performance
            this.PRO_ECU.ECUx1000Add = br.ReadSingle();
            this.PRO_ECU.ECUx2000Add = br.ReadSingle();
            this.PRO_ECU.ECUx3000Add = br.ReadSingle();
            this.PRO_ECU.ECUx4000Add = br.ReadSingle();
            this.PRO_ECU.ECUx5000Add = br.ReadSingle();
            this.PRO_ECU.ECUx6000Add = br.ReadSingle();
            this.PRO_ECU.ECUx7000Add = br.ReadSingle();
            this.PRO_ECU.ECUx8000Add = br.ReadSingle();
            this.PRO_ECU.ECUx9000Add = br.ReadSingle();
            this.PRO_ECU.ECUx10000Add = br.ReadSingle();
            this.PRO_ECU.ECUx11000Add = br.ReadSingle();
            this.PRO_ECU.ECUx12000Add = br.ReadSingle();

            // Top RPM Performance
            this.TOP_RPM.IdleRPMAdd = br.ReadSingle();
            this.TOP_RPM.RedLineRPMAdd = br.ReadSingle();
            this.TOP_RPM.MaxRPMAdd = br.ReadSingle();
            br.BaseStream.Position += 4;

            // Top ECU Performance
            this.TOP_ECU.ECUx1000Add = br.ReadSingle();
            this.TOP_ECU.ECUx2000Add = br.ReadSingle();
            this.TOP_ECU.ECUx3000Add = br.ReadSingle();
            this.TOP_ECU.ECUx4000Add = br.ReadSingle();
            this.TOP_ECU.ECUx5000Add = br.ReadSingle();
            this.TOP_ECU.ECUx6000Add = br.ReadSingle();
            this.TOP_ECU.ECUx7000Add = br.ReadSingle();
            this.TOP_ECU.ECUx8000Add = br.ReadSingle();
            this.TOP_ECU.ECUx9000Add = br.ReadSingle();
            this.TOP_ECU.ECUx10000Add = br.ReadSingle();
            this.TOP_ECU.ECUx11000Add = br.ReadSingle();
            this.TOP_ECU.ECUx12000Add = br.ReadSingle();

            // Top Turbo Performance
            this.TOP_TURBO.TurboBraking = br.ReadSingle();
            this.TOP_TURBO.TurboVacuum = br.ReadSingle();
            this.TOP_TURBO.TurboHeatHigh = br.ReadSingle();
            this.TOP_TURBO.TurboHeatLow = br.ReadSingle();
            this.TOP_TURBO.TurboHighBoost = br.ReadSingle();
            this.TOP_TURBO.TurboLowBoost = br.ReadSingle();
            this.TOP_TURBO.TurboSpool = br.ReadSingle();
            this.TOP_TURBO.TurboSpoolTimeDown = br.ReadSingle();
            this.TOP_TURBO.TurboSpoolTimeUp = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Top Tires Performance
            this.TOP_TIRES.StaticGripScale = br.ReadSingle();
            this.TOP_TIRES.YawSpeedScale = br.ReadSingle();
            this.TOP_TIRES.SteeringAmplifier = br.ReadSingle();
            this.TOP_TIRES.DynamicGripScale = br.ReadSingle();
            this.TOP_TIRES.SteeringResponse = br.ReadSingle();
            br.BaseStream.Position += 0xC;
            this.TOP_TIRES.DriftYawControl = br.ReadSingle();
            this.TOP_TIRES.DriftCounterSteerBuildUp = br.ReadSingle();
            this.TOP_TIRES.DriftCounterSteerReduction = br.ReadSingle();
            this.TOP_TIRES.PowerSlideBreakThru1 = br.ReadSingle();
            this.TOP_TIRES.PowerSlideBreakThru2 = br.ReadSingle();
            br.BaseStream.Position += 0xC;

            // Top Nitrous Performance
            this.TOP_NITROUS.NOSCapacity = br.ReadSingle();
            br.BaseStream.Position += 8;
            this.TOP_NITROUS.NOSTorqueBoost = br.ReadSingle();
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
            this.TOP_SUSPENSION.ShockStiffnessFront = br.ReadSingle();
            this.TOP_SUSPENSION.ShockExtStiffnessFront = br.ReadSingle();
            this.TOP_SUSPENSION.SpringProgressionFront = br.ReadSingle();
            this.TOP_SUSPENSION.ShockValvingFront = br.ReadSingle();
            this.TOP_SUSPENSION.SwayBarFront = br.ReadSingle();
            this.TOP_SUSPENSION.TrackWidthFront = br.ReadSingle();
            this.TOP_SUSPENSION.CounterBiasFront = br.ReadSingle();
            this.TOP_SUSPENSION.ShockDigressionFront = br.ReadSingle();
            this.TOP_SUSPENSION.ShockStiffnessRear = br.ReadSingle();
            this.TOP_SUSPENSION.ShockExtStiffnessRear = br.ReadSingle();
            this.TOP_SUSPENSION.SpringProgressionRear = br.ReadSingle();
            this.TOP_SUSPENSION.ShockValvingRear = br.ReadSingle();
            this.TOP_SUSPENSION.SwayBarRear = br.ReadSingle();
            this.TOP_SUSPENSION.TrackWidthRear = br.ReadSingle();
            this.TOP_SUSPENSION.CounterBiasRear = br.ReadSingle();
            this.TOP_SUSPENSION.ShockDigressionRear = br.ReadSingle();

            // Ecar values
            this.ECAR.HandlingBuffer = br.ReadSingle();
            this.ECAR.TopSuspFrontHeightReduce = br.ReadSingle();
            this.ECAR.TopSuspRearHeightReduce = br.ReadSingle();
            br.BaseStream.Position += 4;
            this.ECAR.NumPlayerCameras = br.ReadInt32();
            this.ECAR.NumAICameras = br.ReadInt32();
            br.BaseStream.Position += 8;

            // Player Cameras
            br.BaseStream.Position += 2;
            this.PLAYER_CAMERA_FAR.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.PLAYER_CAMERA_FAR.CameraLag = br.ReadSingle();
            this.PLAYER_CAMERA_FAR.CameraHeight = br.ReadSingle();
            this.PLAYER_CAMERA_FAR.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.PLAYER_CAMERA_CLOSE.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.PLAYER_CAMERA_CLOSE.CameraLag = br.ReadSingle();
            this.PLAYER_CAMERA_CLOSE.CameraHeight = br.ReadSingle();
            this.PLAYER_CAMERA_CLOSE.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.PLAYER_CAMERA_BUMPER.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.PLAYER_CAMERA_BUMPER.CameraLag = br.ReadSingle();
            this.PLAYER_CAMERA_BUMPER.CameraHeight = br.ReadSingle();
            this.PLAYER_CAMERA_BUMPER.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.PLAYER_CAMERA_DRIVER.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.PLAYER_CAMERA_DRIVER.CameraLag = br.ReadSingle();
            this.PLAYER_CAMERA_DRIVER.CameraHeight = br.ReadSingle();
            this.PLAYER_CAMERA_DRIVER.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.PLAYER_CAMERA_HOOD.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.PLAYER_CAMERA_HOOD.CameraLag = br.ReadSingle();
            this.PLAYER_CAMERA_HOOD.CameraHeight = br.ReadSingle();
            this.PLAYER_CAMERA_HOOD.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.PLAYER_CAMERA_DRIFT.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.PLAYER_CAMERA_DRIFT.CameraLag = br.ReadSingle();
            this.PLAYER_CAMERA_DRIFT.CameraHeight = br.ReadSingle();
            this.PLAYER_CAMERA_DRIFT.CameraLatOffset = br.ReadSingle();

            // AI Cameras
            br.BaseStream.Position += 2;
            this.AI_CAMERA_FAR.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.AI_CAMERA_FAR.CameraLag = br.ReadSingle();
            this.AI_CAMERA_FAR.CameraHeight = br.ReadSingle();
            this.AI_CAMERA_FAR.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.AI_CAMERA_CLOSE.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.AI_CAMERA_CLOSE.CameraLag = br.ReadSingle();
            this.AI_CAMERA_CLOSE.CameraHeight = br.ReadSingle();
            this.AI_CAMERA_CLOSE.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.AI_CAMERA_BUMPER.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.AI_CAMERA_BUMPER.CameraLag = br.ReadSingle();
            this.AI_CAMERA_BUMPER.CameraHeight = br.ReadSingle();
            this.AI_CAMERA_BUMPER.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.AI_CAMERA_DRIVER.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.AI_CAMERA_DRIVER.CameraLag = br.ReadSingle();
            this.AI_CAMERA_DRIVER.CameraHeight = br.ReadSingle();
            this.AI_CAMERA_DRIVER.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.AI_CAMERA_HOOD.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.AI_CAMERA_HOOD.CameraLag = br.ReadSingle();
            this.AI_CAMERA_HOOD.CameraHeight = br.ReadSingle();
            this.AI_CAMERA_HOOD.CameraLatOffset = br.ReadSingle();
            br.BaseStream.Position += 2;
            this.AI_CAMERA_DRIFT.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
            this.AI_CAMERA_DRIFT.CameraLag = br.ReadSingle();
            this.AI_CAMERA_DRIFT.CameraHeight = br.ReadSingle();
            this.AI_CAMERA_DRIFT.CameraLatOffset = br.ReadSingle();

            // Rigid Controls
            this._rigid_controls = new ushort[40];
            for (int a1 = 0; a1 < 40; ++a1)
                this._rigid_controls[a1] = br.ReadUInt16();

            // Secondary Properties
            this.Index = br.ReadInt32();
            this.UsageType = br.ReadEnum<eUsageType>();
            br.BaseStream.Position += 4;
            this.DefaultBasePaint = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.DefaultBasePaint2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
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
        public override ACollectable MemoryCast(string CName)
        {
            var result = new CarTypeInfo(CName, this.Database)
            {
                Spoiler = this.Spoiler,
                Mirrors = this.Mirrors,
                RoofScoop = this.RoofScoop,
                UsageType = this.UsageType,
                IsSkinnable = this.IsSkinnable,
                ManufacturerName = this.ManufacturerName,
                DefaultBasePaint = this.DefaultBasePaint,
                DefaultBasePaint2 = this.DefaultBasePaint2,
                IsSUV = this.IsSUV,
                HeadlightFOV = this.HeadlightFOV,
                PadHighPerformance = this.PadHighPerformance,
                NumAvailableSkinNumbers = this.NumAvailableSkinNumbers,
                WhatGame = this.WhatGame,
                ConvertibleFlag = this.ConvertibleFlag,
                WheelOuterRadius = this.WheelOuterRadius,
                WheelInnerRadiusMin = this.WheelInnerRadiusMin,
                WheelInnerRadiusMax = this.WheelInnerRadiusMax,
                HeadlightPositionX = this.HeadlightPositionX,
                HeadlightPositionY = this.HeadlightPositionY,
                HeadlightPositionZ = this.HeadlightPositionZ,
                HeadlightPositionW = this.HeadlightPositionW,
                DriverRenderingOffsetX = this.DriverRenderingOffsetX,
                DriverRenderingOffsetY = this.DriverRenderingOffsetY,
                DriverRenderingOffsetZ = this.DriverRenderingOffsetZ,
                DriverRenderingOffsetW = this.DriverRenderingOffsetW,
                SteeringWheelRenderingX = this.SteeringWheelRenderingX,
                SteeringWheelRenderingY = this.SteeringWheelRenderingY,
                SteeringWheelRenderingZ = this.SteeringWheelRenderingZ,
                SteeringWheelRenderingW = this.SteeringWheelRenderingW,
                UnknownVectorValX = this.UnknownVectorValX,
                UnknownVectorValY = this.UnknownVectorValY,
                UnknownVectorValZ = this.UnknownVectorValZ,
                UnknownVectorValW = this.UnknownVectorValW,
                MaxInstances1 = this.MaxInstances1,
                MaxInstances2 = this.MaxInstances2,
                MaxInstances3 = this.MaxInstances3,
                MaxInstances4 = this.MaxInstances4,
                MaxInstances5 = this.MaxInstances5,
                KeepLoaded1 = this.KeepLoaded1,
                KeepLoaded2 = this.KeepLoaded2,
                KeepLoaded3 = this.KeepLoaded3,
                KeepLoaded4 = this.KeepLoaded4,
                KeepLoaded5 = this.KeepLoaded5,
                MinTimeBetweenUses1 = this.MinTimeBetweenUses1,
                MinTimeBetweenUses2 = this.MinTimeBetweenUses2,
                MinTimeBetweenUses3 = this.MinTimeBetweenUses3,
                MinTimeBetweenUses4 = this.MinTimeBetweenUses4,
                MinTimeBetweenUses5 = this.MinTimeBetweenUses5,
                AvailableSkinNumbers01 = this.AvailableSkinNumbers01,
                AvailableSkinNumbers02 = this.AvailableSkinNumbers02,
                AvailableSkinNumbers03 = this.AvailableSkinNumbers03,
                AvailableSkinNumbers04 = this.AvailableSkinNumbers04,
                AvailableSkinNumbers05 = this.AvailableSkinNumbers05,
                AvailableSkinNumbers06 = this.AvailableSkinNumbers06,
                AvailableSkinNumbers07 = this.AvailableSkinNumbers07,
                AvailableSkinNumbers08 = this.AvailableSkinNumbers08,
                AvailableSkinNumbers09 = this.AvailableSkinNumbers09,
                AvailableSkinNumbers10 = this.AvailableSkinNumbers10,
                DefaultSkinNumber = this.DefaultSkinNumber,
                AI_CAMERA_BUMPER = this.AI_CAMERA_BUMPER.PlainCopy(),
                AI_CAMERA_CLOSE = this.AI_CAMERA_CLOSE.PlainCopy(),
                AI_CAMERA_DRIFT = this.AI_CAMERA_DRIFT.PlainCopy(),
                AI_CAMERA_DRIVER = this.AI_CAMERA_DRIVER.PlainCopy(),
                AI_CAMERA_FAR = this.AI_CAMERA_FAR.PlainCopy(),
                AI_CAMERA_HOOD = this.AI_CAMERA_HOOD.PlainCopy(),
                PLAYER_CAMERA_BUMPER = this.PLAYER_CAMERA_BUMPER.PlainCopy(),
                PLAYER_CAMERA_CLOSE = this.PLAYER_CAMERA_CLOSE.PlainCopy(),
                PLAYER_CAMERA_DRIFT = this.PLAYER_CAMERA_DRIFT.PlainCopy(),
                PLAYER_CAMERA_DRIVER = this.PLAYER_CAMERA_DRIVER.PlainCopy(),
                PLAYER_CAMERA_FAR = this.PLAYER_CAMERA_FAR.PlainCopy(),
                PLAYER_CAMERA_HOOD = this.PLAYER_CAMERA_HOOD.PlainCopy(),
                BASE_BRAKES = this.BASE_BRAKES.PlainCopy(),
                BASE_ENGINE = this.BASE_ENGINE.PlainCopy(),
                BASE_RPM = this.BASE_RPM.PlainCopy(),
                BASE_SUSPENSION = this.BASE_SUSPENSION.PlainCopy(),
                BASE_TIRES = this.BASE_TIRES.PlainCopy(),
                BASE_TRANSMISSION = this.BASE_TRANSMISSION.PlainCopy(),
                BASE_TURBO = this.BASE_TURBO.PlainCopy(),
                DRIFT_ADD_CONTROL = this.DRIFT_ADD_CONTROL.PlainCopy(),
                ECAR = this.ECAR.PlainCopy(),
                PVEHICLE = this.PVEHICLE.PlainCopy(),
                PRO_ECU = this.PRO_ECU.PlainCopy(),
                PRO_RPM = this.PRO_RPM.PlainCopy(),
                PRO_TRANSMISSION = this.PRO_TRANSMISSION.PlainCopy(),
                STREET_ECU = this.STREET_ECU.PlainCopy(),
                STREET_RPM = this.STREET_RPM.PlainCopy(),
                STREET_TRANSMISSION = this.STREET_TRANSMISSION.PlainCopy(),
                TOP_BRAKES = this.TOP_BRAKES.PlainCopy(),
                TOP_ECU = this.TOP_ECU.PlainCopy(),
                TOP_ENGINE = this.TOP_ENGINE.PlainCopy(),
                TOP_NITROUS = this.TOP_NITROUS.PlainCopy(),
                TOP_RPM = this.TOP_RPM.PlainCopy(),
                TOP_SUSPENSION = this.TOP_SUSPENSION.PlainCopy(),
                TOP_TIRES = this.TOP_TIRES.PlainCopy(),
                TOP_TRANSMISSION = this.TOP_TRANSMISSION.PlainCopy(),
                TOP_TURBO = this.TOP_TURBO.PlainCopy(),
                TOP_WEIGHT_REDUCTION = this.TOP_WEIGHT_REDUCTION.PlainCopy(),
                WHEEL_FRONT_LEFT = this.WHEEL_FRONT_LEFT.PlainCopy(),
                WHEEL_FRONT_RIGHT = this.WHEEL_FRONT_RIGHT.PlainCopy(),
                WHEEL_REAR_LEFT = this.WHEEL_REAR_LEFT.PlainCopy(),
                WHEEL_REAR_RIGHT = this.WHEEL_REAR_RIGHT.PlainCopy(),
                CARSKIN01 = this.CARSKIN01.PlainCopy(),
                CARSKIN02 = this.CARSKIN02.PlainCopy(),
                CARSKIN03 = this.CARSKIN03.PlainCopy(),
                CARSKIN04 = this.CARSKIN04.PlainCopy(),
                CARSKIN05 = this.CARSKIN05.PlainCopy(),
                CARSKIN06 = this.CARSKIN06.PlainCopy(),
                CARSKIN07 = this.CARSKIN07.PlainCopy(),
                CARSKIN08 = this.CARSKIN08.PlainCopy(),
                CARSKIN09 = this.CARSKIN09.PlainCopy(),
                CARSKIN10 = this.CARSKIN10.PlainCopy(),
            };

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

            for (int a1 = 0; a1 < skinsused.Count; ++a1)
            {
                switch (skinsused[a1])
                {
                    case 1:
                        this.CARSKIN01.Write(bw, this.Index, 1);
                        break;
                    case 2:
                        this.CARSKIN02.Write(bw, this.Index, 2);
                        break;
                    case 3:
                        this.CARSKIN03.Write(bw, this.Index, 3);
                        break;
                    case 4:
                        this.CARSKIN04.Write(bw, this.Index, 4);
                        break;
                    case 5:
                        this.CARSKIN05.Write(bw, this.Index, 5);
                        break;
                    case 6:
                        this.CARSKIN06.Write(bw, this.Index, 6);
                        break;
                    case 7:
                        this.CARSKIN07.Write(bw, this.Index, 7);
                        break;
                    case 8:
                        this.CARSKIN08.Write(bw, this.Index, 8);
                        break;
                    case 9:
                        this.CARSKIN09.Write(bw, this.Index, 9);
                        break;
                    case 10:
                        this.CARSKIN10.Write(bw, this.Index, 10);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Initialize()
        {
            this.ECAR = new Ecar();
            this.PVEHICLE = new Pvehicle();
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
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="CarTypeInfo"/> 
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