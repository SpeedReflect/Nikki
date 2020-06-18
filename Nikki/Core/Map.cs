using System;
using System.Collections.Generic;
using Nikki.Reflection.Enum;



namespace Nikki.Core
{
	/// <summary>
	/// Represents all important maps of the library.
	/// </summary>
	public static class Map
	{
		/// <summary>
		/// Map of all Bin keys during runtime of library.
		/// </summary>
		public static Dictionary<uint, string> BinKeys { get; set; } = new Dictionary<uint, string>()
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
			{ 0x2850A03B, "MORPHTARGET_NUM" },
			{ 0x29008B14, "GROUPLANGUAGEHASH" },
			{ 0x311151F8, "HUDINDEX" },
			{ 0x368A1A6A, "DISPRED" },
			{ 0x38A30E75, "ANIMSTYLE" },
			{ 0x3DA5ADAC, "VERTSPLIT" },
			{ 0x46B79643, "LOD_CHARACTERS_OFFSET" },
			{ 0x4732DA07, "LANGUAGEHASH" },
			{ 0x48620C16, "DAMAGELEVEL" },
			{ 0x50317397, "ALPHA2" },
			{ 0x545C3440, "CENTER" },
			{ 0x564B8CB6, "NUMCOLOURS" },
			{ 0x5E96E722, "GREEN2" },
			{ 0x6212682B, "NUMREMAPCOLOURS" },
			{ 0x6223C6F9, "VINYLLANGUAGEHASH" },
			{ 0x643DABEB, "LOD_NAME_PREFIX_SELECTOR" },
			{ 0x6AB42ADF, "WHEELLEFT" },
			{ 0x6B66D0E0, "FULLBODY" },
			{ 0x6BFA56DA, "MIRROR" },
			{ 0x6DB4AF51, "STOCK_MATERIAL" },
			{ 0x70FBB1E4, "ONLINE" },
			{ 0x721AFF7C, "CARBONFIBRE" },
			{ 0x7822E22B, "HOODHUE" },
			{ 0x7822F337, "HOODLUM" },
			{ 0x78230E71, "HOODSAT" },
			{ 0x796C0CB0, "KITNUMBER" },
			{ 0x7AED5629, "SWATCH" },
			{ 0x7C811574, "HOODLEFT" },
			{ 0x7D65A926, "NAME_OFFSET" },
			{ 0x8C185134, "TEXTUREHASH" },
			{ 0x8E73B5DC, "SPECIFICCARNAME" },
			{ 0x900449D3, "PART_NAME_BASE_HASH" },
			{ 0x9239CF16, "PARTID_UPGRADE_GROUP" },
			{ 0x927097F6, "PART_NAME_SELECTOR" },
			{ 0x931FF82E, "SPINNER_TEXTURE" },
			{ 0x956326AF, "LOD_NAME_PREFIX_NAMEHASH" },
			{ 0xA0773FA5, "SPINNEROFFSET" },
			{ 0xA77BDCFA, "NUM_DECALS" },
			{ 0xBB318B8F, "PART_NAME_OFFSETS" },
			{ 0xBCADE4C3, "HOODEMITTER" },
			{ 0xC1A84E52, "WHEELRIGHT" },
			{ 0xCDAB2874, "ISDECAL" },
			{ 0xD90271FB, "COLOR0ID" },
			{ 0xD902763C, "COLOR1ID" },
			{ 0xD9027A7D, "COLOR2ID" },
			{ 0xD9027EBE, "COLOR3ID" },
			{ 0xD90F9423, "MAX_LOD" },
			{ 0xEDB20048, "PAINTGROUP" },
			{ 0xEDBF864E, "WHEELEMITTER" },
			{ 0xF073C523, "MATNAMEA" },
			{ 0xF073C524, "MATNAMEB" },
			{ 0xF7933C86, "EXCLUDE_SUV" },
			{ 0xF7934315, "EXCLUDE_UG1" },
			{ 0xF7934316, "EXCLUDE_UG2" },
			{ 0xFD35FE70, "TEXTURE" },
			{ 0xFE613B98, "LOD_BASE_NAME" },
		};

		/// <summary>
		/// Map of all Vlt keys during runtime of library.
		/// </summary>
		public static Dictionary<uint, string> VltKeys { get; set; } = new Dictionary<uint, string>()
		{
			{ 0, String.Empty }
		};

		/// <summary>
		/// Index table of all performance parts.
		/// </summary>
		public static uint[,,] PerfPartTable { get; set; }

		/// <summary>
		/// Map of all car part labels to <see cref="eCarPartAttribType"/>.
		/// </summary>
		public static Dictionary<uint, eCarPartAttribType> CarPartKeys => new Dictionary<uint, eCarPartAttribType>()
		{
			// Boolean Attributes
			{ 0x03B83203, eCarPartAttribType.Boolean }, // STOCK
			{ 0x039DD714, eCarPartAttribType.Boolean }, // REMAP
			{ 0x70FBB1E4, eCarPartAttribType.Boolean }, // ONLINE
			{ 0x545C3440, eCarPartAttribType.Boolean }, // CENTER
			{ 0x6BFA56DA, eCarPartAttribType.Boolean }, // MIRROR
			{ 0xCDAB2874, eCarPartAttribType.Boolean }, // ISDECAL
			{ 0x6B66D0E0, eCarPartAttribType.Boolean }, // FULLBODY
			{ 0x7C811574, eCarPartAttribType.Boolean }, // HOODLEFT
			{ 0x0D128B87, eCarPartAttribType.Boolean }, // HOODRIGHT
			{ 0x3DA5ADAC, eCarPartAttribType.Boolean }, // VERTSPLIT
			{ 0x6AB42ADF, eCarPartAttribType.Boolean }, // WHEELLEFT
			{ 0xC1A84E52, eCarPartAttribType.Boolean }, // WHEELRIGHT
			{ 0x09163F9F, eCarPartAttribType.Boolean }, // USEMARKER1
			{ 0x09163FA0, eCarPartAttribType.Boolean }, // USEMARKER2
			{ 0x87557E1E, eCarPartAttribType.Boolean }, // 0x87557E1E
			{ 0xF9661A07, eCarPartAttribType.Boolean }, // 0xF9661A07
			{ 0x1BC91595, eCarPartAttribType.Boolean }, // 0x1BC91595
			{ 0x1BC91597, eCarPartAttribType.Boolean }, // 0x1BC91597
			{ 0x1BC91598, eCarPartAttribType.Boolean }, // 0x1BC91598
			{ 0x1BC9159A, eCarPartAttribType.Boolean }, // 0x1BC9159A
			{ 0x1BC9159B, eCarPartAttribType.Boolean }, // 0x1BC9159B
			{ 0x1BC915B5, eCarPartAttribType.Boolean }, // 0x1BC915B5
			{ 0x1BC915BD, eCarPartAttribType.Boolean }, // 0x1BC915BD
			{ 0x721AFF7C, eCarPartAttribType.Boolean }, // CARBONFIBRE
			{ 0xF7933C86, eCarPartAttribType.Boolean }, // EXCLUDE_SUV
			{ 0xF7934315, eCarPartAttribType.Boolean }, // EXCLUDE_UG1
			{ 0xF7934316, eCarPartAttribType.Boolean }, // EXCLUDE_UG2
			{ 0x6DB4AF51, eCarPartAttribType.Boolean }, // STOCK_MATERIAL
			
			// Floating Attributes
			{ 0x02800544, eCarPartAttribType.Floating }, // BLEND
			{ 0x7822E22B, eCarPartAttribType.Floating }, // HOODHUE
			{ 0x7822F337, eCarPartAttribType.Floating }, // HOODLUM
			{ 0x78230E71, eCarPartAttribType.Floating }, // HOODSAT
			{ 0x06159D55, eCarPartAttribType.Floating }, // TIREHUE
			{ 0x0615AE61, eCarPartAttribType.Floating }, // TIRELUM
			{ 0x0615C99B, eCarPartAttribType.Floating }, // TIRESAT
			{ 0x9A9B6DDC, eCarPartAttribType.Floating }, // 0x9A9B6DDC
			{ 0xA0773FA5, eCarPartAttribType.Floating }, // SPINNEROFFSET
			{ 0x931FF82E, eCarPartAttribType.Floating }, // SPINNER_TEXTURE

			// String Attributes
			{ 0xFD35FE70, eCarPartAttribType.String }, // TEXTURE
			{ 0x7D65A926, eCarPartAttribType.String }, // NAME_OFFSET
			{ 0x46B79643, eCarPartAttribType.String }, // LOD_CHARACTERS_OFFSET

			// TwoString Attributes
			{ 0xFE613B98, eCarPartAttribType.TwoString }, // LOD_BASE_NAME
			{ 0xBB318B8F, eCarPartAttribType.TwoString }, // PART_NAME_OFFSETS

			// PartID Attributes
			{ 0x9239CF16, eCarPartAttribType.CarPartID }, // PARTID_UPGRADE_GROUP

			// ModelTable Attributes
			{ 0x10CB799D, eCarPartAttribType.ModelTable }, // MODEL_TABLE_OFFSET

			// Key Attributes
			{ 0x000004B8, eCarPartAttribType.Key }, // CV
			{ 0xD90271FB, eCarPartAttribType.Key }, // COLOR0ID
			{ 0xD902763C, eCarPartAttribType.Key }, // COLOR1ID
			{ 0xD9027A7D, eCarPartAttribType.Key }, // COLOR2ID
			{ 0xD9027EBE, eCarPartAttribType.Key }, // COLOR3ID
			{ 0xF073C523, eCarPartAttribType.Key }, // MATNAMEA
			{ 0xF073C524, eCarPartAttribType.Key }, // MATNAMEB
			{ 0xEDB20048, eCarPartAttribType.Key }, // PAINTGROUP
			{ 0x8C185134, eCarPartAttribType.Key }, // TEXTUREHASH
			{ 0x10C98090, eCarPartAttribType.Key }, // TEXTURE_NAME
			{ 0x4732DA07, eCarPartAttribType.Key }, // LANGUAGEHASH
			{ 0x8E73B5DC, eCarPartAttribType.Key }, // SPECIFICCARNAME
			{ 0x29008B14, eCarPartAttribType.Key }, // GROUPLANGUAGEHASH
			{ 0x6223C6F9, eCarPartAttribType.Key }, // VINYLLANGUAGEHASH
			{ 0x956326AF, eCarPartAttribType.Key }, // LOD_NAME_PREFIX_NAMEHASH
			{ 0x900449D3, eCarPartAttribType.Key }, // PART_NAME_BASE_HASH
			{ 0xEBB03E66, eCarPartAttribType.Key }, // 0xEBB03E66

			// Integer Attributes
			{ 0x0000D99A, eCarPartAttribType.Integer }, // RED
			{ 0x001C0D0C, eCarPartAttribType.Integer }, // RED2
			{ 0x00194031, eCarPartAttribType.Integer }, // MAT0
			{ 0x00194032, eCarPartAttribType.Integer }, // MAT1
			{ 0x00194033, eCarPartAttribType.Integer }, // MAT2
			{ 0x00194034, eCarPartAttribType.Integer }, // MAT3
			{ 0x00194035, eCarPartAttribType.Integer }, // MAT4
			{ 0x00194036, eCarPartAttribType.Integer }, // MAT5
			{ 0x00194037, eCarPartAttribType.Integer }, // MAT6
			{ 0x00194038, eCarPartAttribType.Integer }, // MAT7
			{ 0x0019CBC0, eCarPartAttribType.Integer }, // NAME
			{ 0x001CAD5A, eCarPartAttribType.Integer }, // SIZE
			{ 0x00136707, eCarPartAttribType.Integer }, // BLUE
			{ 0x026E1AC5, eCarPartAttribType.Integer }, // ALPHA
			{ 0x02DDC8F0, eCarPartAttribType.Integer }, // GREEN
			{ 0x02DAAB07, eCarPartAttribType.Integer }, // GLOSS
			{ 0x02804819, eCarPartAttribType.Integer }, // BLUE2
			{ 0x03B16390, eCarPartAttribType.Integer }, // SHAPE
			{ 0x50317397, eCarPartAttribType.Integer }, // ALPHA2
			{ 0x5E96E722, eCarPartAttribType.Integer }, // GREEN2
			{ 0x7AED5629, eCarPartAttribType.Integer }, // SWATCH
			{ 0xD90F9423, eCarPartAttribType.Integer }, // MAX_LOD
			{ 0x368A1A6A, eCarPartAttribType.Integer }, // DISPRED
			{ 0x07C4C1D7, eCarPartAttribType.Integer }, // DISPBLUE
			{ 0x311151F8, eCarPartAttribType.Integer }, // HUDINDEX
			{ 0x38A30E75, eCarPartAttribType.Integer }, // ANIMSTYLE
			{ 0x00BA7DC0, eCarPartAttribType.Integer }, // DISPGREEN
			{ 0x796C0CB0, eCarPartAttribType.Integer }, // KITNUMBER
			{ 0x0D4B85C7, eCarPartAttribType.Integer }, // HOODUNDER
			{ 0x564B8CB6, eCarPartAttribType.Integer }, // NUMCOLOURS
			{ 0xA77BDCFA, eCarPartAttribType.Integer }, // NUM_DECALS
			{ 0xBCADE4C3, eCarPartAttribType.Integer }, // HOODEMITTER
			{ 0x48620C16, eCarPartAttribType.Integer }, // DAMAGELEVEL
			{ 0xEDBF864E, eCarPartAttribType.Integer }, // WHEELEMITTER
			{ 0x6212682B, eCarPartAttribType.Integer }, // NUMREMAPCOLOURS
			{ 0x2850A03B, eCarPartAttribType.Integer }, // MORPHTARGET_NUM
			{ 0x927097F6, eCarPartAttribType.Integer }, // PART_NAME_SELECTOR
			{ 0x643DABEB, eCarPartAttribType.Integer }, // LOD_NAME_PREFIX_SELECTOR

			// Unknown Label Attributes
			{ 0x04B39858, eCarPartAttribType.Integer }, // 0x04B39858
			{ 0x1B0EA1A9, eCarPartAttribType.Integer }, // 0x1B0EA1A9
			{ 0x5412A1D9, eCarPartAttribType.Integer }, // 0x5412A1D9
			{ 0x6509EC92, eCarPartAttribType.Integer }, // 0x6509EC92
			{ 0x65F58556, eCarPartAttribType.Integer }, // 0x65F58556
			{ 0x6BA02C05, eCarPartAttribType.Integer }, // 0x6BA02C05
			{ 0x7D29CF3E, eCarPartAttribType.Integer }, // 0x7D29CF3E
			{ 0xB5548ED7, eCarPartAttribType.Integer }, // 0xB5548ED7
			{ 0xC9818DFC, eCarPartAttribType.Integer }, // 0xC9818DFC
			{ 0xCE7D8DB5, eCarPartAttribType.Integer }, // 0xCE7D8DB5
			{ 0xD68A7BAB, eCarPartAttribType.Integer }, // 0xD68A7BAB
			{ 0xE80A3B62, eCarPartAttribType.Integer }, // 0xE80A3B62
			{ 0xEB0101E2, eCarPartAttribType.Integer }, // 0xEB0101E2
		};

		/// <summary>
		/// Map of all block to alignments.
		/// </summary>
		internal static Dictionary<eBlockID, Alignment> BlockToAlignment => new Dictionary<eBlockID, Alignment>()
		{
			{ eBlockID.FEngFont,          Alignment.Default },
			{ eBlockID.FEngFiles,         Alignment.Default },
			{ eBlockID.FNGCompress,       Alignment.Default },
			{ eBlockID.PresetRides,       Alignment.Default },
			{ eBlockID.MagazinesFrontend, Alignment.Default },
			{ eBlockID.MagazinesShowcase, Alignment.Default },
			{ eBlockID.WideDecals,        Alignment.Default },
			{ eBlockID.PresetSkins,       Alignment.Default },
			{ eBlockID.Tracks,            Alignment.Default },
			{ eBlockID.CarTypeInfos,      Alignment.Default },
			{ eBlockID.CarSkins,          Alignment.Default },
			{ eBlockID.CarInfoAnimHookup, Alignment.Default },
			{ eBlockID.StyleMomentsInfo,  new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.DifficultyInfo,    new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.AcidEffects,       new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.AcidEmmiters,      new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.STRBlocks,         Alignment.Default },
			{ eBlockID.LangFont,          Alignment.Default },
			{ eBlockID.Subtitles,         Alignment.Default },
			{ eBlockID.MovieCatalog,      new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.ICECatalog,        new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.TPKSettings,       new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.SkinRegionDB,      Alignment.Default },
			{ eBlockID.VinylMetaData,     Alignment.Default },
			{ eBlockID.Materials,         Alignment.Default },
			{ eBlockID.EAGLSkeleton,      new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.EAGLAnimations,    Alignment.Default },
			{ eBlockID.DDSTexture,        new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.PCAWater0,         new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.ColorCube,         Alignment.Default },
			{ eBlockID.GCareer,           new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.AnimDirectory,     Alignment.Default },
			{ eBlockID.QuickSpline,       new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.IceCameraPart0,    Alignment.Default },
			{ eBlockID.IceCameraPart1,    Alignment.Default },
			{ eBlockID.IceCameraPart2,    Alignment.Default },
			{ eBlockID.IceCameraPart3,    Alignment.Default },
			{ eBlockID.IceCameraPart4,    Alignment.Default },
			{ eBlockID.IceSettings,       Alignment.Default },
			{ eBlockID.SoundStichs,       Alignment.Default },
			{ eBlockID.EventSequence,     new Alignment(0x08, Alignment.eAlignType.Actual) },
			{ eBlockID.DBCarBounds,       new Alignment(0x08, Alignment.eAlignType.Actual) },
			{ eBlockID.VinylSystem,       new Alignment(0x800, Alignment.eAlignType.Modular) },
			{ eBlockID.LimitsTable,       Alignment.Default },
			{ eBlockID.Geometry,          new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.SpecialEffects,    new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.PCAWeights,        new Alignment(0x80, Alignment.eAlignType.Modular) },
			{ eBlockID.TPKBlocks,         new Alignment(0x80, Alignment.eAlignType.Modular) },
		};
	}
}
