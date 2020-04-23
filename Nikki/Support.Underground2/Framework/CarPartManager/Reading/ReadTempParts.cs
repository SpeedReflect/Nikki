using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.Underground2.Parts.CarParts;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		private static List<TempPart> ReadTempParts(BinaryReader br, BinaryReader str_reader)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_ARRAY) return null;

			// Remove padding at the very end
			int size = br.ReadInt32(); // read current size
			br.BaseStream.Position += size - 4; // go to the last integer
			while (br.ReadInt32() == 0) br.BaseStream.Position -= 8; // read backwards till not 0
			size = (int)(br.BaseStream.Position - offset) / 0xE * 0xE; // calculate new size
			br.BaseStream.Position = offset; // go back
			var result = new List<TempPart>(size / 0xE); // initialize

			while (br.BaseStream.Position < offset + size)
			{
				var part = new TempPart();
				part.Disassemble(br, str_reader);
				result.Add(part);
			}
			return result;
		}
	}
}