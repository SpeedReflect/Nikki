using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Support.Underground2.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		private static int MakeCPPartList(Dictionary<int, int> string_dict, Dictionary<int, int> offset_dict,
			Dictionary<int, int> struct_dict, Database.Underground2 db, out byte[] cppart_buffer)
		{
			// Initialize stack
			cppart_buffer = null;
			int key = 0;
			int length = 0;
			int empty = String.Empty.GetHashCode();
			const ushort negative = 0xFFFF;

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			byte count = 0;
			// Iterate through every model in the database
			foreach (var model in db.DBModelPartList)
			{
				// Iterate through every RealCarPart in a model
				foreach (RealCarPart realpart in model.ModelCarParts)
				{
					// Write main properties
					bw.Write(realpart.PartName.BinHash());
					bw.Write(realpart.CarPartGroupID);
					bw.Write(realpart.UpgradeGroupID);
					bw.Write(count);

					// Write offsets
					key = realpart.DebugName?.GetHashCode() ?? empty;
					bw.Write((ushort)string_dict[key]);
					if (realpart.Attributes.Count == 0) bw.Write(negative);
					else bw.Write((ushort)offset_dict[realpart.GetHashCode()]);
					if (realpart.Struct.Exists == eBoolean.False) bw.Write(negative);
					else bw.Write((ushort)struct_dict[realpart.Struct.GetHashCode()]);

					++length;
				}
				++count;
			}

			// Return number of parts and buffer
			var dif = 0x10 - ((int)ms.Length + 0xC) % 0x10;
			if (dif != 0x10) bw.WriteBytes(dif);

			cppart_buffer = ms.ToArray();
			return length;
		}
	}
}