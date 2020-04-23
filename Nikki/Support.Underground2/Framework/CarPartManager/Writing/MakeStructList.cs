﻿using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Support.Underground2.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		private static Dictionary<int, int> MakeStructList(Dictionary<int, int> string_dict, 
			Database.Underground2 db, out byte[] struct_buffer)
		{
			// Initialize stack
			var struct_dict = new Dictionary<int, int>();
			struct_buffer = null;
			int length = 0;

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);
			
			// Iterate through every model in the database
			foreach (var model in db.DBModelPartList)
			{
				// Iterate through every RealCarPart in a model
				foreach (RealCarPart realpart in model.ModelCarParts)
				{
					// If Struct should not be written, skip
					if (realpart.Struct.Exists == eBoolean.False) continue;

					int key = realpart.Struct.GetHashCode(); // get HashCode of the struct
					if (!struct_dict.ContainsKey(key))       // if it exists, skip
					{
						struct_dict[key] = length++; // add to dictionary
						((CPStruct)realpart.Struct).Assemble(bw, string_dict);
					}
				}			
			}

			// Return prepared dictionary
			bw.FillBuffer(0x10);
			struct_buffer = ms.ToArray();
			return struct_dict;
		}
	}
}