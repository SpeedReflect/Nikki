using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.Underground2.Parts.CarParts;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		private static Dictionary<int, CPStruct> ReadStructs(BinaryReader br, BinaryReader str_reader)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_STRUCTS) return null;
			var size = br.ReadInt32();
			var result = new Dictionary<int, CPStruct>(size / 24); // set initial capacity

			while (br.BaseStream.Position < offset + size)
			{
				var position = (int)(br.BaseStream.Position - offset);
				var cpstr = new CPStruct(br, str_reader);
				result[position] = cpstr;
			}
			return result;
		}
	}
}