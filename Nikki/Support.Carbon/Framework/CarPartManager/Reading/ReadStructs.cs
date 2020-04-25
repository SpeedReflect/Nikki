using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.Underground2.Parts.CarParts;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		private static Dictionary<int, CPStruct> ReadStructs(BinaryReader br,
			BinaryReader str_reader, int maxlen)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_STRUCTS) return null;
			var size = br.ReadInt32();
			var result = new Dictionary<int, CPStruct>(size / 0xF4); // set initial capacity

			int count = 0;
			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
				var position = (int)(br.BaseStream.Position - offset);
				var cpstr = new CPStruct(br, str_reader);
				result[position / 0xF4] = cpstr;
			}
			return result;
		}
	}
}