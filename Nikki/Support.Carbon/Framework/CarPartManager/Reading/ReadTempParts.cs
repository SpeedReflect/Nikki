using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.Carbon.Parts.CarParts;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		private static List<TempPart> ReadTempParts(BinaryReader br, int maxlen)
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
				part.Disassemble(br);
				result.Add(part);
				++count;
			}
			return result;
		}
	}
}