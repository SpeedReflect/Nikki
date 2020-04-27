using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Support.Carbon.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		private static int MakeCPPartList(Dictionary<int, int> offset_dict, Database.Carbon db,
			out byte[] cppart_buffer)
		{
			// Initialize stack
			cppart_buffer = null;
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
					bw.Write((byte)0);
					bw.Write(count);

					// Write attribute offset
					if (realpart.Attributes.Count == 0) bw.Write(negative);
					else bw.Write((ushort)offset_dict[realpart.GetHashCode()]);

					++length;
				}
				++count;
			}

			// Return number of parts and buffer
			var dif = 0x10 - ((int)ms.Length + 8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(dif);

			cppart_buffer = ms.ToArray();
			return length;
		}
	}
}