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
			{ 0, String.Empty }
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
		public static Dictionary<uint, eCarPartAttribType> CarPartKeys { get; set; } = new Dictionary<uint, eCarPartAttribType>()
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
			{ 0x10CB799D, eCarPartAttribType.Integer }, // MODEL_TABLE_OFFSET
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
	}
}
