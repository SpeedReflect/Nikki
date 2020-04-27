using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Support.Carbon.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		private static Dictionary<int, int> MakeStringList(Database.Carbon db, out byte[] string_buffer)
		{
			// Prepare stack
			var string_dict = new Dictionary<int, int>();
			string_buffer = null;
			int length = 0;
			int empty = String.Empty.GetHashCode();

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Fill empty string in the beginning
			string_dict[empty] = 1;
			length += 8;
			bw.Write(0);
			bw.Write(0);

			// Function to write strings to dictionary and return its length
			var Inject = new Func<string, int, int>((value, len) =>
			{
				int key = value?.GetHashCode() ?? empty; // null = String.Empty in this case
				if (!string_dict.ContainsKey(key)) // skip if string is in the dictionary
				{
					string_dict[key] = len >> 2; // write position to dictionary
					len += value.Length + 1;     // increase length
					int diff = 4 - len % 4;      // calculate padding
					if (diff != 4) len += diff;  // add padding
					bw.WriteNullTermUTF8(value); // write string value
					bw.FillBuffer(4);            // fill buffer to % 4
				}
				return len;
			});

			// Iterate through each model in the database
			foreach (var model in db.DBModelPartList)
			{
				// Iterate through each RealCarPart in a model
				foreach (RealCarPart realpart in model.ModelCarParts)
				{
					// Iterate through attributes
					foreach (var attrib in realpart.Attributes)
					{
						// If attribute is a StringAttribute, write its value
						if (attrib is Shared.Parts.CarParts.StringAttribute str_attrib)
							length = Inject(str_attrib.Value, length);

						// Else if attribute is a TwoStringAttribute, write its values
						else if (attrib is Shared.Parts.CarParts.TwoStringAttribute two_attrib)
						{
							length = Inject(two_attrib.Value1, length);
							length = Inject(two_attrib.Value2, length);
						}
					}
				}
			}

			// Write struct geometry names if it is templated and exists
			foreach (var str in db.CarPartStructs)
			{
				if (str.Exists == eBoolean.True && str.Templated == eBoolean.True)
				{
					for (int a1 = 0; a1 < CPStruct.StructNamesSize; ++a1)
						length = Inject(str.GeometryName[a1], length);
				}
			}

			// Return prepared dictionary
			bw.FillBuffer(0x10);
			string_buffer = ms.ToArray();
			return string_dict;
		}
	}
}