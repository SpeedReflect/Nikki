using System;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;



namespace Nikki.Core
{
	/// <summary>
	/// Represents all important maps of the library.
	/// </summary>
	public static class Map
	{
		private static readonly Dictionary<uint, string> _binkeys = new Dictionary<uint, string>()
		{
			{ 0, String.Empty },
			{ 0x000004B8, "CV" },
			{ 0x0000D99A, "RED" },
			{ 0x00136707, "BLUE" },
			{ 0x001C0D0C, "RED2" },
			{ 0x00194031, "MAT0" },
			{ 0x00194032, "MAT1" },
			{ 0x00194033, "MAT2" },
			{ 0x00194034, "MAT3" },
			{ 0x00194035, "MAT4" },
			{ 0x00194036, "MAT5" },
			{ 0x00194037, "MAT6" },
			{ 0x00194038, "MAT7" },
			{ 0x0019CBC0, "NAME" },
			{ 0x001CAD5A, "SIZE" },
			{ 0x00BA7DC0, "DISPGREEN" },
			{ 0x013DB390, "AcidEffects" },
			{ 0x026E1AC5, "ALPHA" },
			{ 0x02800544, "BLEND" },
			{ 0x02804819, "BLUE2" },
			{ 0x02DDC8F0, "GREEN" },
			{ 0x02DAAB07, "GLOSS" },
			{ 0x039DD714, "REMAP" },
			{ 0x03B16390, "SHAPE" },
			{ 0x03B83203, "STOCK" },
			{ 0x06159D55, "TIREHUE" },
			{ 0x0615AE61, "TIRELUM" },
			{ 0x0615C99B, "TIRESAT" },
			{ 0x07C4C1D7, "DISPBLUE" },
			{ 0x09163F9F, "USEMARKER1" },
			{ 0x09163FA0, "USEMARKER2" },
			{ 0x0D128B87, "HOODRIGHT" },
			{ 0x0D4B85C7, "HOODUNDER" },
			{ 0x10C98090, "TEXTURE_NAME" },
			{ 0x10CB799D, "MODEL_TABLE_OFFSET" },
			{ 0x18E6DD34, "SlotOverrides" },
			{ 0x1B0EA1A9, "SPOKE_COUNT" },
			{ 0x1BC91595, "KITNOTPAINT01" },
			{ 0x1BC91597, "KITNOTPAINT03" },
			{ 0x1BC91598, "KITNOTPAINT04" },
			{ 0x1BC9159A, "KITNOTPAINT06" },
			{ 0x1BC9159B, "KITNOTPAINT07" },
			{ 0x1BC915B5, "KITNOTPAINT10" },
			{ 0x1BC915BD, "KITNOTPAINT18" },
			{ 0x21CFED6B, "GCareers" },
			{ 0x2850A03B, "MORPHTARGET_NUM" },
			{ 0x29008B14, "GROUPLANGUAGEHASH" },
			{ 0x2DFDDCBE, "Collisions" },
			{ 0x311151F8, "HUDINDEX" },
			{ 0x368A1A6A, "DISPRED" },
			{ 0x377065A6, "GCareerRaces" },
			{ 0x3882376C, "TPKBlocks" },
			{ 0x38A30E75, "ANIMSTYLE" },
			{ 0x3DA5ADAC, "VERTSPLIT" },
			{ 0x46B79643, "LOD_CHARACTERS_OFFSET" },
			{ 0x4732DA07, "LANGUAGEHASH" },
			{ 0x47AE5413, "FNGroups" },
			{ 0x48620C16, "DAMAGELEVEL" },
			{ 0x4BE3CB3D, "AcidEmitters" },
			{ 0x50317397, "ALPHA2" },
			{ 0x545C3440, "CENTER" },
			{ 0x564B8CB6, "NUMCOLOURS" },
			{ 0x579512F6, "CarTypeInfos" },
			{ 0x5937DC74, "SunInfos" },
			{ 0x5F9BCCA1, "Materials" },
			{ 0x5E96E722, "GREEN2" },
			{ 0x6212682B, "NUMREMAPCOLOURS" },
			{ 0x6223C6F9, "VINYLLANGUAGEHASH" },
			{ 0x643DABEB, "LOD_NAME_PREFIX_SELECTOR" },
			{ 0x6AB42ADF, "WHEELLEFT" },
			{ 0x6B66D0E0, "FULLBODY" },
			{ 0x6BA02C05, "LIGHT_MATERIAL_NAME" },
			{ 0x6BFA56DA, "MIRROR" },
			{ 0x6DB4AF51, "STOCK_MATERIAL" },
			{ 0x70FBB1E4, "ONLINE" },
			{ 0x721AFF7C, "CARBONFIBRE" },
			{ 0x768C36C0, "DBModelParts" },
			{ 0x7710F0E0, "RANDOM" },
			{ 0x7822E22B, "HOODHUE" },
			{ 0x7822F337, "HOODLUM" },
			{ 0x78230E71, "HOODSAT" },
			{ 0x796C0CB0, "KITNUMBER" },
			{ 0x7AED5629, "SWATCH" },
			{ 0x7C811574, "HOODLEFT" },
			{ 0x7D65A926, "NAME_OFFSET" },
			{ 0x7F3CE5A7, "Tracks" },
			{ 0x87557E1E, "COMPLEXTARGET" },
			{ 0x8C185134, "TEXTUREHASH" },
			{ 0x8E73B5DC, "SPECIFICCARNAME" },
			{ 0x8FE4C336, "STRBlocks" },
			{ 0x900449D3, "PART_NAME_BASE_HASH" },
			{ 0x9239CF16, "PARTID_UPGRADE_GROUP" },
			{ 0x927097F6, "PART_NAME_SELECTOR" },
			{ 0x931FF82E, "SPINNER_TEXTURE" },
			{ 0x956326AF, "LOD_NAME_PREFIX_NAMEHASH" },
			{ 0xA0773FA5, "SPINNEROFFSET" },
			{ 0xA0AF9D37, "VectorVinyls" },
			{ 0xA77BDCFA, "NUM_DECALS" },
			{ 0xBB318B8F, "PART_NAME_OFFSETS" },
			{ 0xBCADE4C3, "HOODEMITTER" },
			{ 0xC1A84E52, "WHEELRIGHT" },
			{ 0xC9818DFC, "LANGUAGEHASHABR" },
			{ 0xCE7D8DB5, "OUTER_RADIUS" },
			{ 0xCDAB2874, "ISDECAL" },
			{ 0xD2BCA329, "PresetRides" },
			{ 0xD2CFEADA, "PresetSkins" },
			{ 0xD68A7BAB, "SPEECHCOLOUR" },
			{ 0xD90271FB, "COLOR0ID" },
			{ 0xD902763C, "COLOR1ID" },
			{ 0xD9027A7D, "COLOR2ID" },
			{ 0xD9027EBE, "COLOR3ID" },
			{ 0xD90F9423, "MAX_LOD" },
			{ 0xE15EEFD6, "SlotTypes" },
			{ 0xE80A3B62, "EXCLUDEDECAL" },
			{ 0xEB0101E2, "INNER_RADIUS" },
			{ 0xEBB03E66, "BRAND_NAME" },
			{ 0xEDB20048, "PAINTGROUP" },
			{ 0xEDBF864E, "WHEELEMITTER" },
			{ 0xF073C523, "MATNAMEA" },
			{ 0xF073C524, "MATNAMEB" },
			{ 0xF7933C86, "EXCLUDE_SUV" },
			{ 0xF7934315, "EXCLUDE_UG1" },
			{ 0xF7934316, "EXCLUDE_UG2" },
			{ 0xF9661A07, "WARNONDELETE" },
			{ 0xFD35FE70, "TEXTURE" },
			{ 0xFE613B98, "LOD_BASE_NAME" },
			{ 0xFFFFFFFF, String.Empty },
		};
		private static readonly Dictionary<uint, string> _vltkeys = new Dictionary<uint, string>()
		{
			{ 0, String.Empty },
			{ 0x2B5B1321, "m3gtre46" }
		};
		private static readonly Dictionary<uint, CarPartAttribType> _carpartkeys = new Dictionary<uint, CarPartAttribType>()
		{
			// Boolean Attributes
			{ 0x03B83203, CarPartAttribType.Boolean }, // STOCK
			{ 0x039DD714, CarPartAttribType.Boolean }, // REMAP
			{ 0x70FBB1E4, CarPartAttribType.Boolean }, // ONLINE
			{ 0x545C3440, CarPartAttribType.Boolean }, // CENTER
			{ 0x6BFA56DA, CarPartAttribType.Boolean }, // MIRROR
			{ 0xCDAB2874, CarPartAttribType.Boolean }, // ISDECAL
			{ 0x6B66D0E0, CarPartAttribType.Boolean }, // FULLBODY
			{ 0x7C811574, CarPartAttribType.Boolean }, // HOODLEFT
			{ 0x0D128B87, CarPartAttribType.Boolean }, // HOODRIGHT
			{ 0x3DA5ADAC, CarPartAttribType.Boolean }, // VERTSPLIT
			{ 0x6AB42ADF, CarPartAttribType.Boolean }, // WHEELLEFT
			{ 0xC1A84E52, CarPartAttribType.Boolean }, // WHEELRIGHT
			{ 0x09163F9F, CarPartAttribType.Boolean }, // USEMARKER1
			{ 0x09163FA0, CarPartAttribType.Boolean }, // USEMARKER2
			{ 0x87557E1E, CarPartAttribType.Boolean }, // COMPLEXTARGET
			{ 0x6509EC92, CarPartAttribType.Boolean }, // 0x6509EC92 // ALUMINUM ???
			{ 0x721AFF7C, CarPartAttribType.Boolean }, // CARBONFIBRE
			{ 0xF7933C86, CarPartAttribType.Boolean }, // EXCLUDE_SUV
			{ 0xF7934315, CarPartAttribType.Boolean }, // EXCLUDE_UG1
			{ 0xF7934316, CarPartAttribType.Boolean }, // EXCLUDE_UG2
			{ 0xF9661A07, CarPartAttribType.Boolean }, // WARNONDELETE
			{ 0x1BC91595, CarPartAttribType.Boolean }, // KITNOTPAINT01
			{ 0x1BC91597, CarPartAttribType.Boolean }, // KITNOTPAINT03
			{ 0x1BC91598, CarPartAttribType.Boolean }, // KITNOTPAINT04
			{ 0x1BC9159A, CarPartAttribType.Boolean }, // KITNOTPAINT06
			{ 0x1BC9159B, CarPartAttribType.Boolean }, // KITNOTPAINT07
			{ 0x1BC915B5, CarPartAttribType.Boolean }, // KITNOTPAINT10
			{ 0x1BC915BD, CarPartAttribType.Boolean }, // KITNOTPAINT18
			{ 0x6DB4AF51, CarPartAttribType.Boolean }, // STOCK_MATERIAL
			
			// Floating Attributes
			{ 0x02800544, CarPartAttribType.Floating }, // BLEND
			{ 0x7822E22B, CarPartAttribType.Floating }, // HOODHUE
			{ 0x7822F337, CarPartAttribType.Floating }, // HOODLUM
			{ 0x78230E71, CarPartAttribType.Floating }, // HOODSAT
			{ 0x06159D55, CarPartAttribType.Floating }, // TIREHUE
			{ 0x0615AE61, CarPartAttribType.Floating }, // TIRELUM
			{ 0x0615C99B, CarPartAttribType.Floating }, // TIRESAT
			{ 0x9A9B6DDC, CarPartAttribType.Floating }, // 0x9A9B6DDC // ???
			{ 0xA0773FA5, CarPartAttribType.Floating }, // SPINNEROFFSET

			// Color Attributes
			{ 0xD90271FB, CarPartAttribType.Color }, // COLOR0ID
			{ 0xD902763C, CarPartAttribType.Color }, // COLOR1ID
			{ 0xD9027A7D, CarPartAttribType.Color }, // COLOR2ID
			{ 0xD9027EBE, CarPartAttribType.Color }, // COLOR3ID

			// String Attributes
			{ 0xFD35FE70, CarPartAttribType.String }, // TEXTURE
			{ 0x7D65A926, CarPartAttribType.String }, // NAME_OFFSET
			{ 0x46B79643, CarPartAttribType.String }, // LOD_CHARACTERS_OFFSET

			// TwoString Attributes
			{ 0xFE613B98, CarPartAttribType.TwoString }, // LOD_BASE_NAME
			{ 0xBB318B8F, CarPartAttribType.TwoString }, // PART_NAME_OFFSETS

			// PartID Attributes
			{ 0x9239CF16, CarPartAttribType.CarPartID }, // PARTID_UPGRADE_GROUP

			// ModelTable Attributes
			{ 0x10CB799D, CarPartAttribType.ModelTable }, // MODEL_TABLE_OFFSET

			// Key Attributes
			{ 0x000004B8, CarPartAttribType.Key }, // CV
			{ 0x00194031, CarPartAttribType.Key }, // MAT0
			{ 0x00194032, CarPartAttribType.Key }, // MAT1
			{ 0x00194033, CarPartAttribType.Key }, // MAT2
			{ 0x00194034, CarPartAttribType.Key }, // MAT3
			{ 0x00194035, CarPartAttribType.Key }, // MAT4
			{ 0x00194036, CarPartAttribType.Key }, // MAT5
			{ 0x00194037, CarPartAttribType.Key }, // MAT6
			{ 0x00194038, CarPartAttribType.Key }, // MAT7
			{ 0x0019CBC0, CarPartAttribType.Key }, // NAME
			{ 0x001CAD5A, CarPartAttribType.Key }, // SIZE
			{ 0x03B16390, CarPartAttribType.Key }, // SHAPE
			{ 0x7AED5629, CarPartAttribType.Key }, // SWATCH
			{ 0xF073C523, CarPartAttribType.Key }, // MATNAMEA
			{ 0xF073C524, CarPartAttribType.Key }, // MATNAMEB
			{ 0x0D4B85C7, CarPartAttribType.Key }, // HOODUNDER
			{ 0xEBB03E66, CarPartAttribType.Key }, // BRAND_NAME
			{ 0xEDB20048, CarPartAttribType.Key }, // PAINTGROUP
			{ 0xBCADE4C3, CarPartAttribType.Key }, // HOODEMITTER
			{ 0x8C185134, CarPartAttribType.Key }, // TEXTUREHASH
			{ 0xE80A3B62, CarPartAttribType.Key }, // EXCLUDEDECAL
			{ 0x4732DA07, CarPartAttribType.Key }, // LANGUAGEHASH
			{ 0xD68A7BAB, CarPartAttribType.Key }, // SPEECHCOLOUR
			{ 0x10C98090, CarPartAttribType.Key }, // TEXTURE_NAME
			{ 0xEDBF864E, CarPartAttribType.Key }, // WHEELEMITTER
			{ 0x8E73B5DC, CarPartAttribType.Key }, // SPECIFICCARNAME
			{ 0x931FF82E, CarPartAttribType.Key }, // SPINNER_TEXTURE
			{ 0x29008B14, CarPartAttribType.Key }, // GROUPLANGUAGEHASH
			{ 0x6223C6F9, CarPartAttribType.Key }, // VINYLLANGUAGEHASH
			{ 0x65F58556, CarPartAttribType.Key }, // COLOR0LANGUAGEHASH
			{ 0xB5548ED7, CarPartAttribType.Key }, // COLOR1LANGUAGEHASH
			{ 0x04B39858, CarPartAttribType.Key }, // COLOR2LANGUAGEHASH
			{ 0x5412A1D9, CarPartAttribType.Key }, // COLOR3LANGUAGEHASH
			{ 0x6BA02C05, CarPartAttribType.Key }, // LIGHT_MATERIAL_NAME
			{ 0x900449D3, CarPartAttribType.Key }, // PART_NAME_BASE_HASH
			{ 0x956326AF, CarPartAttribType.Key }, // LOD_NAME_PREFIX_NAMEHASH

			// Integer Attributes
			{ 0x0000D99A, CarPartAttribType.Integer }, // RED
			{ 0x001C0D0C, CarPartAttribType.Integer }, // RED2
			{ 0x00136707, CarPartAttribType.Integer }, // BLUE
			{ 0x026E1AC5, CarPartAttribType.Integer }, // ALPHA
			{ 0x02DDC8F0, CarPartAttribType.Integer }, // GREEN
			{ 0x02DAAB07, CarPartAttribType.Integer }, // GLOSS
			{ 0x02804819, CarPartAttribType.Integer }, // BLUE2
			{ 0x50317397, CarPartAttribType.Integer }, // ALPHA2
			{ 0x5E96E722, CarPartAttribType.Integer }, // GREEN2
			{ 0xD90F9423, CarPartAttribType.Integer }, // MAX_LOD
			{ 0x368A1A6A, CarPartAttribType.Integer }, // DISPRED
			{ 0x07C4C1D7, CarPartAttribType.Integer }, // DISPBLUE
			{ 0x311151F8, CarPartAttribType.Integer }, // HUDINDEX
			{ 0x38A30E75, CarPartAttribType.Integer }, // ANIMSTYLE
			{ 0x00BA7DC0, CarPartAttribType.Integer }, // DISPGREEN
			{ 0x796C0CB0, CarPartAttribType.Integer }, // KITNUMBER
			{ 0x564B8CB6, CarPartAttribType.Integer }, // NUMCOLOURS
			{ 0xA77BDCFA, CarPartAttribType.Integer }, // NUM_DECALS
			{ 0x7D29CF3E, CarPartAttribType.Integer }, // 0x7D29CF3E // ???
			{ 0x48620C16, CarPartAttribType.Integer }, // DAMAGELEVEL
			{ 0x1B0EA1A9, CarPartAttribType.Integer }, // SPOKE_COUNT
			{ 0xEB0101E2, CarPartAttribType.Integer }, // INNER_RADIUS
			{ 0xCE7D8DB5, CarPartAttribType.Integer }, // OUTER_RADIUS
			{ 0xC9818DFC, CarPartAttribType.Integer }, // LANGUAGEHASHABR
			{ 0x6212682B, CarPartAttribType.Integer }, // NUMREMAPCOLOURS
			{ 0x2850A03B, CarPartAttribType.Integer }, // MORPHTARGET_NUM
			{ 0x927097F6, CarPartAttribType.Integer }, // PART_NAME_SELECTOR
			{ 0x643DABEB, CarPartAttribType.Integer }, // LOD_NAME_PREFIX_SELECTOR
		};

		/// <summary>
		/// Map of all Bin keys during runtime of library.
		/// </summary>
		public static Dictionary<uint, string> BinKeys => _binkeys;

		/// <summary>
		/// Map of all Vlt keys during runtime of library.
		/// </summary>
		public static Dictionary<uint, string> VltKeys => _vltkeys;

		/// <summary>
		/// Map of all car part labels to <see cref="CarPartAttribType"/>.
		/// </summary>
		public static Dictionary<uint, CarPartAttribType> CarPartKeys => _carpartkeys;

		/// <summary>
		/// Map of all block to alignments.
		/// </summary>
		internal static Dictionary<BinBlockID, Alignment> BlockToAlignment => new Dictionary<BinBlockID, Alignment>()
		{
			{ BinBlockID.FEngFont,           Alignment.Default },
			{ BinBlockID.FEngFiles,          Alignment.Default },
			{ BinBlockID.FNGCompress,        Alignment.Default },
			{ BinBlockID.PresetRides,        Alignment.Default },
			{ BinBlockID.MagazinesFrontend,  Alignment.Default },
			{ BinBlockID.MagazinesShowcase,  Alignment.Default },
			{ BinBlockID.WideDecals,         Alignment.Default },
			{ BinBlockID.PresetSkins,        Alignment.Default },
			{ BinBlockID.Tracks,             Alignment.Default },
			{ BinBlockID.CarTypeInfos,       Alignment.Default },
			{ BinBlockID.CarSkins,           Alignment.Default },
			{ BinBlockID.CarInfoAnimHookup,  Alignment.Default },
			{ BinBlockID.StyleMomentsInfo,   new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.StylePartitions,    new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.DifficultyInfo,     new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.AcidEffects,        new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.AcidEmitters,       new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.STRBlocks,          Alignment.Default },
			{ BinBlockID.LangFont,           Alignment.Default },
			{ BinBlockID.Subtitles,          Alignment.Default },
			{ BinBlockID.MovieCatalog,       new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.ICECatalog,         new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.WCollisionRaww,     new Alignment(0x800, Alignment.AlignmentType.Modular) },
			{ BinBlockID.EmitterTexturePage, new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.SkinRegionDB,       Alignment.Default },
			{ BinBlockID.VinylMetaData,      Alignment.Default },
			{ BinBlockID.Materials,          Alignment.Default },
			{ BinBlockID.EAGLSkeleton,       new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.EAGLAnimations,     Alignment.Default },
			{ BinBlockID.DDSTexture,         new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.PCAWater0,          new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.ColorCube,          Alignment.Default },
			{ BinBlockID.GCareer,            new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.GCareer_Old,        new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.GCareer_Styles,     new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.GLimitations,       new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.AnimDirectory,      Alignment.Default },
			{ BinBlockID.QuickSpline,        new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.IceCameraPart0,     Alignment.Default },
			{ BinBlockID.IceCameraPart1,     Alignment.Default },
			{ BinBlockID.IceCameraPart2,     Alignment.Default },
			{ BinBlockID.IceCameraPart3,     Alignment.Default },
			{ BinBlockID.IceCameraPart4,     Alignment.Default },
			{ BinBlockID.IceSettings,        Alignment.Default },
			{ BinBlockID.SoundStichs,        Alignment.Default },
			{ BinBlockID.EventSequence,      new Alignment(0x08, Alignment.AlignmentType.Actual) },
			{ BinBlockID.DBCarBounds,        new Alignment(0x08, Alignment.AlignmentType.Actual) },
			{ BinBlockID.VinylSystem,        new Alignment(0x800, Alignment.AlignmentType.Modular) },
			{ BinBlockID.VinylDataTable,     Alignment.Default },
			{ BinBlockID.GeometryPack,           new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.OldAnimationPack,     new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.PCAWeights,         new Alignment(0x80, Alignment.AlignmentType.Modular) },
			{ BinBlockID.TPKBlocks,          new Alignment(0x80, Alignment.AlignmentType.Modular) },
		};
	
		/// <summary>
		/// Reloads entire <see cref="BinKeys"/> dictionary to its entry runtime state.
		/// </summary>
		public static void ReloadBinKeys()
		{
			var state = Hashing.PauseHashSave;
			Hashing.PauseHashSave = false;

			_binkeys.Clear();
			_binkeys[0] = String.Empty;
			_binkeys[0xFFFFFFFF] = String.Empty;
			"CV".BinHash();
			"RED".BinHash();
			"BLUE".BinHash();
			"RED2".BinHash();
			"MAT0".BinHash();
			"MAT1".BinHash();
			"MAT2".BinHash();
			"MAT3".BinHash();
			"MAT4".BinHash();
			"MAT5".BinHash();
			"MAT6".BinHash();
			"MAT7".BinHash();
			"NAME".BinHash();
			"SIZE".BinHash();
			"DISPGREEN".BinHash();
			"AcidEffects".BinHash();
			"ALPHA".BinHash();
			"BLEND".BinHash();
			"BLUE2".BinHash();
			"GREEN".BinHash();
			"GLOSS".BinHash();
			"REMAP".BinHash();
			"SHAPE".BinHash();
			"STOCK".BinHash();
			"TIREHUE".BinHash();
			"TIRELUM".BinHash();
			"TIRESAT".BinHash();
			"DISPBLUE".BinHash();
			"USEMARKER1".BinHash();
			"USEMARKER2".BinHash();
			"HOODRIGHT".BinHash();
			"HOODUNDER".BinHash();
			"TEXTURE_NAME".BinHash();
			"MODEL_TABLE_OFFSET".BinHash();
			"SlotOverrides".BinHash();
			"SPOKE_COUNT".BinHash();
			"KITNOTPAINT01".BinHash();
			"KITNOTPAINT03".BinHash();
			"KITNOTPAINT04".BinHash();
			"KITNOTPAINT06".BinHash();
			"KITNOTPAINT07".BinHash();
			"KITNOTPAINT10".BinHash();
			"KITNOTPAINT18".BinHash();
			"GCareers".BinHash();
			"MORPHTARGET_NUM".BinHash();
			"GROUPLANGUAGEHASH".BinHash();
			"Collisions".BinHash();
			"HUDINDEX".BinHash();
			"DISPRED".BinHash();
			"GCareerRaces".BinHash();
			"TPKBlocks".BinHash();
			"ANIMSTYLE".BinHash();
			"VERTSPLIT".BinHash();
			"LOD_CHARACTERS_OFFSET".BinHash();
			"LANGUAGEHASH".BinHash();
			"FNGroups".BinHash();
			"DAMAGELEVEL".BinHash();
			"AcidEmitters".BinHash();
			"ALPHA2".BinHash();
			"CENTER".BinHash();
			"NUMCOLOURS".BinHash();
			"CarTypeInfos".BinHash();
			"SunInfos".BinHash();
			"Materials".BinHash();
			"GREEN2".BinHash();
			"NUMREMAPCOLOURS".BinHash();
			"VINYLLANGUAGEHASH".BinHash();
			"LOD_NAME_PREFIX_SELECTOR".BinHash();
			"WHEELLEFT".BinHash();
			"FULLBODY".BinHash();
			"LIGHT_MATERIAL_NAME".BinHash();
			"MIRROR".BinHash();
			"STOCK_MATERIAL".BinHash();
			"ONLINE".BinHash();
			"CARBONFIBRE".BinHash();
			"DBModelParts".BinHash();
			"RANDOM".BinHash();
			"HOODHUE".BinHash();
			"HOODLUM".BinHash();
			"HOODSAT".BinHash();
			"KITNUMBER".BinHash();
			"SWATCH".BinHash();
			"HOODLEFT".BinHash();
			"NAME_OFFSET".BinHash();
			"Tracks".BinHash();
			"COMPLEXTARGET".BinHash();
			"TEXTUREHASH".BinHash();
			"SPECIFICCARNAME".BinHash();
			"STRBlocks".BinHash();
			"PART_NAME_BASE_HASH".BinHash();
			"PARTID_UPGRADE_GROUP".BinHash();
			"PART_NAME_SELECTOR".BinHash();
			"SPINNER_TEXTURE".BinHash();
			"LOD_NAME_PREFIX_NAMEHASH".BinHash();
			"SPINNEROFFSET".BinHash();
			"VectorVinyls".BinHash();
			"NUM_DECALS".BinHash();
			"PART_NAME_OFFSETS".BinHash();
			"HOODEMITTER".BinHash();
			"WHEELRIGHT".BinHash();
			"LANGUAGEHASHABR".BinHash();
			"OUTER_RADIUS".BinHash();
			"ISDECAL".BinHash();
			"PresetRides".BinHash();
			"PresetSkins".BinHash();
			"SPEECHCOLOUR".BinHash();
			"COLOR0ID".BinHash();
			"COLOR1ID".BinHash();
			"COLOR2ID".BinHash();
			"COLOR3ID".BinHash();
			"MAX_LOD".BinHash();
			"SlotTypes".BinHash();
			"EXCLUDEDECAL".BinHash();
			"INNER_RADIUS".BinHash();
			"BRAND_NAME".BinHash();
			"PAINTGROUP".BinHash();
			"WHEELEMITTER".BinHash();
			"MATNAMEA".BinHash();
			"MATNAMEB".BinHash();
			"EXCLUDE_SUV".BinHash();
			"EXCLUDE_UG1".BinHash();
			"EXCLUDE_UG2".BinHash();
			"WARNONDELETE".BinHash();
			"TEXTURE".BinHash();
			"LOD_BASE_NAME".BinHash();

			Hashing.PauseHashSave = state;
		}
	}
}
