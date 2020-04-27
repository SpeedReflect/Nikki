using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		private static int MakeStructList(Dictionary<int, int> string_dict, Database.Carbon db,
			out byte[] struct_buffer)
		{
			// Initialize stack
			struct_buffer = null;

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);
			
			// Iterate through every struct
			foreach (var str in db.CarPartStructs)
			{
				// If Struct should not be written, skip
				if (str.Exists == eBoolean.False) continue;
				str.Assemble(bw, string_dict);
			}			

			// Return prepared dictionary
			var dif = 0x10 - ((int)ms.Length + 8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(dif);

			struct_buffer = ms.ToArray();
			return db.CarPartStructs.Count;
		}
	}
}