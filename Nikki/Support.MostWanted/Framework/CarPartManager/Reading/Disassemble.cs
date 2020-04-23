using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using CoreExtensions.IO;
using Nikki.Reflection.ID;
using Nikki.Support.MostWanted.Class;
using Nikki.Support.MostWanted.Parts.CarParts;



namespace Nikki.Support.MostWanted.Framework
{
	public static partial class CarPartManager
	{
		public static void Disassemble(BinaryReader br, int size, Database.MostWanted db)
		{
			long position = br.BaseStream.Position;
			var offsets = FindOffsets(br, size);

			// We need to read part0 as well
			br.BaseStream.Position = offsets[0] + 0x30;
			int maxnum = br.ReadInt32();

			// Initialize stream over string block
			br.BaseStream.Position = offsets[1];
			var length = br.ReadUInt32() != CarParts.DBCARPART_STRINGS ? 0 : br.ReadInt32();
			var strarr = br.ReadBytes(length);
			using var StrStream = new MemoryStream(strarr);
			using var StrReader = new BinaryReader(StrStream);
			
			// Read all attribute offsets
			br.BaseStream.Position = offsets[2];
			var offset_dict = ReadOffsets(br);

			// Read all car part attributes
			br.BaseStream.Position = offsets[3];
			var attrib_dict = ReadAttribs(br, StrReader);

			// We need to read part4 as well

			// Read all models
			br.BaseStream.Position = offsets[5];
			var models_list = ReadModels(br, maxnum);

			// Read all temporary parts
			br.BaseStream.Position = offsets[6];
			var temp_cparts = ReadTempParts(br);

			// Generate Model Collections
			for (int a1 = 0; a1 < models_list.Length; ++a1)
			{
				var collection = new DBModelPart(models_list[a1], db);
				var tempparts = temp_cparts.FindAll(_ => _.Index == a1);

				foreach (var temppart in tempparts)
				{
					//if (offset_dict.TryGetValue(temppart.Unknown1, out var _1))
					//	Console.WriteLine($"It is Unknown1s !!! -> {_1.Offset} | {temppart.PartName}");
					//
					//if (offset_dict.TryGetValue(temppart.CarPartOffset, out var _2))
					//	Console.WriteLine($"It is CarOffset !!! -> {_2.Offset} | {temppart.PartName}");
					//
					if (offset_dict.TryGetValue(temppart.AttribOffset, out var _3))
						Console.WriteLine($"It is Unknown2s !!! -> {_3.Offset} | {temppart.PartName}");

					//if (offset_dict.TryGetValue(temppart.AttribOffset, out var _4))
					//	Console.WriteLine($"It is FECustRec !!! -> {_4.Offset} | {temppart.PartName}");
				}
				Console.WriteLine("----------------------------------------------------------");
				int aaa = 0;
			}
		}
	}
}
