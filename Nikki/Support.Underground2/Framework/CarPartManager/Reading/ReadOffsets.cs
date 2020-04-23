using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.Shared.Parts.CarParts;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		private static Dictionary<int, CPOffset> ReadOffsets(BinaryReader br)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_OFFSETS) return null;
			var size = br.ReadInt32();
			var result = new Dictionary<int, CPOffset>(size / 8); // set initial capacity

			while (br.BaseStream.Position < offset + size)
			{
				var position = (int)(br.BaseStream.Position - offset);
				var count = br.ReadUInt16();
				var cpoff = new CPOffset(count, position);
				for (int a1 = 0; a1 < count; ++a1)
					cpoff.AttribOffsets.Add(br.ReadUInt16());
				result[position >> 1] = cpoff;
			}
			return result;
		}
	}
}