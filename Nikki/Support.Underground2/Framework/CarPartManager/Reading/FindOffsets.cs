using System.IO;
using Nikki.Reflection.ID;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		private static long[] FindOffsets(BinaryReader br, int size)
		{
			var result = new long[7];
			var offset = br.BaseStream.Position;
			while (br.BaseStream.Position < offset + size)
			{
				switch (br.ReadUInt32())
				{
					case CarParts.DBCARPART_HEADER:
						result[0] = br.BaseStream.Position - 4;
						goto default;

					case CarParts.DBCARPART_STRINGS:
						result[1] = br.BaseStream.Position - 4;
						goto default;

					case CarParts.DBCARPART_OFFSETS:
						result[2] = br.BaseStream.Position - 4;
						goto default;

					case CarParts.DBCARPART_ATTRIBS:
						result[3] = br.BaseStream.Position - 4;
						goto default;

					case CarParts.DBCARPART_STRUCTS:
						result[4] = br.BaseStream.Position - 4;
						goto default;

					case CarParts.DBCARPART_MODELS:
						result[5] = br.BaseStream.Position - 4;
						goto default;

					case CarParts.DBCARPART_ARRAY:
						result[6] = br.BaseStream.Position - 4;
						goto default;

					default:
						var skip = br.ReadInt32();
						br.BaseStream.Position += skip;
						break;
				}
			}
			br.BaseStream.Position = offset;
			return result;
		}
	}
}