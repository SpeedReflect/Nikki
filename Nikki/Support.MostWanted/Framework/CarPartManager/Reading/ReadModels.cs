using System.IO;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Support.MostWanted.Parts.CarParts;



namespace Nikki.Support.MostWanted.Framework
{
	public static partial class CarPartManager
	{
		private static string[] ReadModels(BinaryReader br, int maxlen)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_MODELS) return null;
			var size = br.ReadInt32();
			var count = size / 4;

			count = (count > maxlen) ? maxlen : count;

			var result = new string[count];

			for (int a1 = 0; a1 < count; ++a1)
			{
				var key = br.ReadUInt32();
				result[a1] = key.BinString(eLookupReturn.EMPTY);
				foreach (var part in UsageType.PartName)
					(result[a1] + part).BinHash();
			}
			return result;
		}
	}
}
