namespace Nikki.Reflection.Enum
{
    /// <summary>
    /// Enum of IDs related to all global blocks.
    /// </summary>
	public enum BinBlockID : uint
	{
        /// <summary>
        /// 0x00000000
        /// </summary>
        Padding = 0x00000000, // none

        /// <summary>
        /// 0x00030201
        /// </summary>
        FEngFont = 0x00030201, // 0x10 Modular

        /// <summary>
        /// 0x00030203
        /// </summary>
        FEngFiles = 0x00030203, // 0x10 Modular

        /// <summary>
        /// 0x00030210
        /// </summary>
        FNGCompress = 0x00030210, // 0x10 Modular

        /// <summary>
        /// 0x00030220
        /// </summary>
        PresetRides = 0x00030220, // 0x10 Modular

        /// <summary>
        /// 0x00030230
        /// </summary>
        MagazinesFrontend = 0x00030230, // 0x10 Modular

        /// <summary>
        /// 0x00030231
        /// </summary>
        MagazinesShowcase = 0x00030231, // 0x10 Modular

        /// <summary>
        /// 0x00030240
        /// </summary>
        WideDecals = 0x00030240, // 0x10 Modular

        /// <summary>
        /// 0x00030250
        /// </summary>
        PresetSkins = 0x00030250, // 0x08 Actual

        /// <summary>
        /// 0x00034026
        /// </summary>
        Smokeables = 0x00034026, // 0x10 Modular

        /// <summary>
        /// 0x00034027
        /// </summary>
        WorldBounds = 0x00034027, // varies

        /// <summary>
        /// 0x00034107
        /// </summary>
        SceneryOverride = 0x00034107, // 0x10 Modular

        /// <summary>
        /// 0x00034108
        /// </summary>
        SceneryGroup = 0x00034108, // 0x10 Modular

        /// <summary>
        /// 0x00034110
        /// </summary>
        TrackStreamingSections = 0x00034110, // varies

        /// <summary>
        /// 0x00034146
        /// </summary>
        TrackPosMarkers = 0x00034146, // varies

        /// <summary>
        /// 0x00034201
        /// </summary>
        Tracks = 0x00034201, // 0x10 Modular

        /// <summary>
        /// 0x00034202
        /// </summary>
        SunInfos = 0x00034202, // varies

        /// <summary>
        /// 0x00034250
        /// </summary>
        Weatherman = 0x00034250, // varies

        /// <summary>
        /// 0x00034600
        /// </summary>
        CarTypeInfos = 0x00034600, // 0x10 Modular

        /// <summary>
        /// 0x00034601
        /// </summary>
        CarSkins = 0x00034601, // 0x10 Modular

        /// <summary>
        /// 0x00034603
        /// </summary>
        DBCarParts_Header = 0x00034603, // varies

        /// <summary>
        /// 0x00034604
        /// </summary>
        DBCarParts_Array = 0x00034604, // varies

        /// <summary>
        /// 0x00034605
        /// </summary>
        DBCarParts_Attribs = 0x00034605, // varies

        /// <summary>
        /// 0x00034606
        /// </summary>
        DBCarParts_Strings = 0x00034606, // varies

        /// <summary>
        /// 0x00034607
        /// </summary>
        SlotTypes = 0x00034607, // varies

        /// <summary>
        /// 0x00034608
        /// </summary>
        CarInfoAnimHookup = 0x00034608, // 0x10 Modular

        /// <summary>
        /// 0x00034609
        /// </summary>
        CarInfoAnimHideup = 0x00034609, // varies

        /// <summary>
        /// 0x0003460A
        /// </summary>
        DBCarParts_Structs = 0x0003460A, // varies

        /// <summary>
        /// 0x0003460B
        /// </summary>
        DBCarParts_Models = 0x0003460B, // varies

        /// <summary>
        /// 0x0003460C
        /// </summary>
        DBCarParts_Offsets = 0x0003460C, // varies

        /// <summary>
        /// 0x0003460D
        /// </summary>
        DBCarParts_Custom = 0x0003460D, // varies

        /// <summary>
        /// 0x00034A01
        /// </summary>
        GCareer_Upgrade = 0x00034A01, // varies

        /// <summary>
        /// 0x00034A02
        /// </summary>
        GCareer_Races_Old = 0x00034A02, // 0x08 Actual

        /// <summary>
        /// 0x00034A07
        /// </summary>
        StyleMomentsInfo = 0x00034A07, // 0x80 Modular

        /// <summary>
        /// 0x00034A08
        /// </summary>
        StylePartitions = 0x00034A08, // 0x80 Modular

        /// <summary>
        /// 0x00034A09
        /// </summary>
        GCareer_Stars = 0x00034A09, // varies

        /// <summary>
        /// 0x00034A09
        /// </summary>
        GCareer_Styles = 0x00034A0A, // 0x10 Modular

        /// <summary>
        /// 0x00034A11
        /// </summary>
        GCareer_Races = 0x00034A11, // varies

        /// <summary>
        /// 0x00034A12
        /// </summary>
        GCareer_Shops = 0x00034A12, // varies

        /// <summary>
        /// 0x00034A14
        /// </summary>
        GCareer_Brands = 0x00034A14, // varies

        /// <summary>
        /// 0x00034A15
        /// </summary>
        GCareer_PartPerf = 0x00034A15, // varies

        /// <summary>
        /// 0x00034A16
        /// </summary>
        GCareer_Showcases = 0x00034A16, // varies

        /// <summary>
        /// 0x00034A17
        /// </summary>
        GCareer_Messages = 0x00034A17, // varies

        /// <summary>
        /// 0x00034A18
        /// </summary>
        GCareer_Stages = 0x00034A18, // varies

        /// <summary>
        /// 0x00034A19
        /// </summary>
        GCareer_Sponsors = 0x00034A19, // varies

        /// <summary>
        /// 0x00034A1A
        /// </summary>
        GCareer_PerfTun = 0x00034A1A, // varies

        /// <summary>
        /// 0x00034A1B
        /// </summary>
        GCareer_Challenges = 0x00034A1B, // varies

        /// <summary>
        /// 0x00034A1C
        /// </summary>
        GCareer_PartUnlock = 0x00034A1C, // varies

        /// <summary>
        /// 0x00034A1D
        /// </summary>
        GCareer_Strings = 0x00034A1D, // varies

        /// <summary>
        /// 0x00034A1E
        /// </summary>
        GCareer_BankTrigs = 0x00034A1E, // varies

        /// <summary>
        /// 0x00034A1F
        /// </summary>
        GCareer_CarUnlocks = 0x00034A1F, // varies

        /// <summary>
        /// 0x00034B00
        /// </summary>
        DifficultyInfo = 0x00034B00, // 0x80 Modular

        /// <summary>
        /// 0x00035020
        /// </summary>
        AcidEffects = 0x00035020, // 0x80 Modular

        /// <summary>
        /// 0x00035021
        /// </summary>
        AcidEmitters = 0x00035021, // 0x80 Modular

        /// <summary>
        /// 0x00037220
        /// </summary>
        WorldAnimHeader = 0x00037220, // varies

        /// <summary>
        /// 0x00037240
        /// </summary>
        WorldAnimMatrices = 0x00037240, // varies

        /// <summary>
        /// 0x00037250
        /// </summary>
        WorldAnimRTNode = 0x00037250, // varies

        /// <summary>
        /// 0x00037260
        /// </summary>
        WorldAnimNodeInfo = 0x00037260, // varies

        /// <summary>
        /// 0x00037270
        /// </summary>
        WorldAnimPointer = 0x00037270, // varies

        /// <summary>
        /// 0x00039000
        /// </summary>
        STRBlocks = 0x00039000, // 0x10 Modular

        /// <summary>
        /// 0x00039001
        /// </summary>
        LangFont = 0x00039001, // 0x10 Modular

        /// <summary>
        /// 0x00039010
        /// </summary>
        Subtitles = 0x00039010, // 0x10 Modular

        /// <summary>
        /// 0x00039020
        /// </summary>
        MovieCatalog = 0x00039020, // 0x80 Modular

        /// <summary>
        /// 0x0003A100
        /// </summary>
        CompTPKBlock = 0x0003A100, // 0x10 Modular

        /// <summary>
        /// 0x0003B200
        /// </summary>
        ICECatalog = 0x0003B200, // 0x80 Modular

        /// <summary>
        /// 0x0003B800
        /// </summary>
        WWorld = 0x0003B800, // varies

        /// <summary>
        /// 0x0003B801
        /// </summary>
        WCollisionPack = 0x0003B801, // varies

        /// <summary>
        /// 0x0003B802
        /// </summary>
        WCollisionRaww = 0x0003B802, // 0x800 Modular

        /// <summary>
        /// 0x0003B811
        /// </summary>
        NISScript = 0x0003B811, // varies

        /// <summary>
        /// 0x0003B901
        /// </summary>
        Collision = 0x0003B901, // 0x10 Modular

        /// <summary>
        /// 0x0003BC00
        /// </summary>
        EmitterLibrary = 0x0003BC00, // varies

        /// <summary>
        /// 0x0003BD00
        /// </summary>
        EmitterTexturePage = 0x0003BD00, // 0x80 Modular

        /// <summary>
        /// 0x0003CE01
        /// </summary>
        Vinyl_Header = 0x0003CE01, // 0x08 Actual

        /// <summary>
        /// 0x0003CE02
        /// </summary>
        Vinyl_PointerTable = 0x0003CE02, // 0x0C Actual

        /// <summary>
        /// 0x0003CE04
        /// </summary>
        Vinyl_PathEntry = 0x0003CE04, // varies

        /// <summary>
        /// 0x0003CE05
        /// </summary>
        Vinyl_PathData = 0x0003CE05, // varies

        /// <summary>
        /// 0x0003CE06
        /// </summary>
        Vinyl_PathPoint = 0x0003CE06, // varies

        /// <summary>
        /// 0x0003CE07
        /// </summary>
        Vinyl_FillEffect = 0x0003CE07, // varies

        /// <summary>
        /// 0x0003CE08
        /// </summary>
        Vinyl_StrokeEffect = 0x0003CE08, // varies

        /// <summary>
        /// 0x0003CE09
        /// </summary>
        Vinyl_DropShadow = 0x0003CE09, // varies

        /// <summary>
        /// 0x0003CE0A
        /// </summary>
        Vinyl_InnerGlow = 0x0003CE0A, // varies

        /// <summary>
        /// 0x0003CE0B
        /// </summary>
        Vinyl_ShadowEffect = 0x0003CE0B, // varies

        /// <summary>
        /// 0x0003CE0C
        /// </summary>
        Vinyl_Gradient = 0x0003CE0C, // varies

        /// <summary>
        /// 0x0003CE0E
        /// </summary>
        VinylDataHeader = 0x0003CE0E, // varies

        /// <summary>
        /// 0x0003CE0F
        /// </summary>
        VinylCarEntries = 0x0003CE0F, // varies

        /// <summary>
        /// 0x0003CE10
        /// </summary>
        VinylFloatMatrix = 0x0003CE10, // varies

        /// <summary>
        /// 0x0003CE11
        /// </summary>
        VinylVectorEntries = 0x0003CE11, // varies

        /// <summary>
        /// 0x0003CE12
        /// </summary>
        SkinRegionDB = 0x0003CE12, // 0x10 Modular

        /// <summary>
        /// 0x0003CE13
        /// </summary>
        VinylMetaData = 0x0003CE13, // 0x10 Modular

        /// <summary>
        /// 0x000B5846
        /// </summary>
        FX = 0x000B5846, // not supported

        /// <summary>
        /// 0x00135200
        /// </summary>
        Materials = 0x00135200, // 0x10 Modular

        /// <summary>
        /// 0x00E34009
        /// </summary>
        EAGLSkeleton = 0x00E34009, // varies

        /// <summary>
        /// 0x00E34010
        /// </summary>
        EAGLAnimations = 0x00E34010, // varies

        /// <summary>
        /// 0x30300200
        /// </summary>
        DDSTexture = 0x30300200, // 0x80 Modular

        /// <summary>
        /// 0x30300201
        /// </summary>
        ColorCube = 0x30300201, // 0x10 Modular

        /// <summary>
        /// 0x30300300
        /// </summary>
        PCAWater0 = 0x30300300, // 0x80 Modular

        /// <summary>
        /// 0x33310001
        /// </summary>
        TPK_InfoPart1 = 0x33310001, // 0x08 Actual

        /// <summary>
        /// 0x33310002
        /// </summary>
        TPK_InfoPart2 = 0x33310002, // 0x0C Actual

        /// <summary>
        /// 0x33310003
        /// </summary>
        TPK_InfoPart3 = 0x33310003, // varies

        /// <summary>
        /// 0x33310004
        /// </summary>
        TPK_InfoPart4 = 0x33310004, // varies

        /// <summary>
        /// 0x33310005
        /// </summary>
        TPK_InfoPart5 = 0x33310005, // varies

        /// <summary>
        /// 0x33312001
        /// </summary>
        TPK_AnimPart1 = 0x33312001, // varies

        /// <summary>
        /// 0x33312002
        /// </summary>
        TPK_AnimPart2 = 0x33312002, // varies

        /// <summary>
        /// 0x33320001
        /// </summary>
        TPK_DataPart1 = 0x33320001, // 0x08 Actual

        /// <summary>
        /// 0x33320002
        /// </summary>
        TPK_DataPart2 = 0x33320002, // 0x80 Modular

        /// <summary>
        /// 0x33320003
        /// </summary>
        TPK_DataPart3 = 0x33320003, // ????

        /// <summary>
        /// 0x42704D67
        /// </summary>
        Nikki = 0x42704D67, // varies

        /// <summary>
        /// 0x434B4241
        /// </summary>
        ABKC = 0x434B4241, // not supported

        /// <summary>
        /// 0x48434F4C
        /// </summary>
        LOCH = 0x48434F4C, // not supported

        /// <summary>
        /// 0x4B415056
        /// </summary>
        VPAK = 0x4B415056, // not supported

        /// <summary>
        /// 0x52494F4D
        /// </summary>
        MOIR = 0x52494F4D, // not supported

        /// <summary>
        /// 0x53219999
        /// </summary>
        MEMO = 0x53219999, // not supported

        /// <summary>
        /// 0x55441122
        /// </summary>
        LZCompressed = 0x55441122, // varies

        /// <summary>
        /// 0x6468564D
        /// </summary>
        MVhd = 0x6468564D, // not supported

        /// <summary>
        /// 0x75736E47
        /// </summary>
        Gnsu = 0x75736E47, // not supported

        /// <summary>
        /// 0x80034100
        /// </summary>
        ScenerySection = 0x80034100, // varies

        /// <summary>
        /// 0x80034602
        /// </summary>
        DBCarParts = 0x80034602, // varies

        /// <summary>
        /// 0x80034A00
        /// </summary>
        GCareer_Old = 0x80034A00, // 0x80 Modular

        /// <summary>
        /// 0x80034A10
        /// </summary>
        GCareer = 0x80034A10, // 0x80 Modular

        /// <summary>
        /// 0x80034A30
        /// </summary>
        GLimitations = 0x80034A30, // 0x80 Modular

        /// <summary>
        /// 0x80036000
        /// </summary>
        EventTriggers = 0x80036000, // varies

        /// <summary>
        /// 0x80037020
        /// </summary>
        NISDescription = 0x80037020, // varies

        /// <summary>
        /// 0x80037050
        /// </summary>
        AnimDirectory = 0x80037050, // 0x10 Modular

        /// <summary>
        /// 0x8003B000
        /// </summary>
        QuickSpline = 0x8003B000, // varies

        /// <summary>
        /// 0x8003B200
        /// </summary>
        IceCameraPart0 = 0x8003B200, // 0x10 Modular

        /// <summary>
        /// 0x8003B201
        /// </summary>
        IceCameraPart1 = 0x8003B201, // 0x10 Modular

        /// <summary>
        /// 0x8003B202
        /// </summary>
        IceCameraPart2 = 0x8003B202, // 0x10 Modular

        /// <summary>
        /// 0x8003B203
        /// </summary>
        IceCameraPart3 = 0x8003B203, // 0x10 Modular

        /// <summary>
        /// 0x8003B204
        /// </summary>
        IceCameraPart4 = 0x8003B204, // 0x10 Modular

        /// <summary>
        /// 0x8003B209
        /// </summary>
        IceSettings = 0x8003B209, // 0x10 Modular

        /// <summary>
        /// 0x8003B500
        /// </summary>
        SoundStichs = 0x8003B500, // 0x10 Modular

        /// <summary>
        /// 0x8003B810
        /// </summary>
        EventSequence = 0x8003B810, // varies

        /// <summary>
        /// 0x8003B900
        /// </summary>
        DBCarBounds = 0x8003B900, // 0x08 Actual

        /// <summary>
        /// 0x8003CE00
        /// </summary>
        VinylSystem = 0x8003CE00, // 0x800 Modular

        /// <summary>
        /// 0x8003CE03
        /// </summary>
        Vinyl_PathSet = 0x8003CE03, // varies

        /// <summary>
        /// 0x8003CE0D
        /// </summary>
        VinylDataTable = 0x8003CE0D, // 0x10 Modular

        /// <summary>
        /// 0x80134000
        /// </summary>
        GeometryPack = 0x80134000, // varies

        /// <summary>
        /// 0x80135000
        /// </summary>
        LightSourcePack = 0x80135000, // varies

        /// <summary>
        /// 0xB0300100
        /// </summary>
        OldAnimationPack = 0xB0300100, // 0x80 Modular

        /// <summary>
        /// 0xB0300300
        /// </summary>
        PCAWeights = 0xB0300300, // 0x80 Modular

        /// <summary>
        /// 0xB3300000
        /// </summary>
        TPKBlocks = 0xB3300000, // 0x80 Modular

        /// <summary>
        /// 0xB3310000
        /// </summary>
        TPK_InfoBlock = 0xB3310000, // 0x40 Modular

        /// <summary>
        /// 0xB3312000
        /// </summary>
        TPK_BinData = 0xB3312000, // varies

        /// <summary>
        /// 0xB3312004
        /// </summary>
        TPK_AnimBlock = 0xB3312004, // varies

        /// <summary>
        /// 0xB3320000
        /// </summary>
        TPK_DataBlock = 0xB3320000, // 0x80 Modular
    }
}
