using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.MostWanted.Parts.CarParts;



namespace Nikki.Support.MostWanted.Framework
{
	public static partial class CarPartManager
	{
		private static List<TempPart> ReadTempParts(BinaryReader br, BinaryReader str_reader, int maxlen)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_ARRAY) return null;

			// Remove padding at the very end
			int size = br.ReadInt32(); // read current size
			var result = new List<TempPart>(maxlen); // initialize

			int count = 0;
			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
				var part = new TempPart();
				part.Disassemble(br, str_reader);
				result.Add(part);
				++count;
			}
			return result;
		}
	}
}