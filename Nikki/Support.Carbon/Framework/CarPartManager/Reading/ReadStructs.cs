using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.ID;
using Nikki.Support.Carbon.Parts.CarParts;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		private static void ReadStructs(BinaryReader br, BinaryReader str_reader,
			int maxlen, Database.Carbon db)
		{
			var offset = br.BaseStream.Position + 8;
			if (br.ReadUInt32() != CarParts.DBCARPART_STRUCTS) return;
			var size = br.ReadInt32();

			int count = 0;
			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
				var position = (int)(br.BaseStream.Position - offset);
				var cpstr = new CPStruct(br, str_reader);
				db.CarPartStructs.Add(cpstr);
			}
		}
	}
}