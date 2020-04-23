using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.MostWanted.Parts.CarParts;



namespace Nikki.Support.MostWanted.Framework
{
	public static partial class CarPartManager
	{
		private static List<TempPart> ReadTempParts(BinaryReader br)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_ARRAY) return null;
			var size = br.ReadInt32() / 0xE * 0xE; // exclude padding
			var result = new List<TempPart>(size / 0xE); // set initial capacity

			while (br.BaseStream.Position < offset + size)
			{
				var part = new TempPart();
				part.Disassemble(br);
				result.Add(part);
			}
			return result;
		}
	}
}