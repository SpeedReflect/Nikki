using System.IO;
using System.Collections.Generic;
using Nikki.Support.Underground2.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		private static Dictionary<int, int> MakeOffsetList(Dictionary<int, int> attrib_dict,
			Database.Underground2 db, out byte[] offset_buffer)
		{
			// Initialize stack
			var offset_map = new Dictionary<int, int>();  // CPOffset to AttribOffset
			var offset_dict = new Dictionary<int, int>(); // RealCarPart to AttribOffset
			offset_buffer = null;
			int length = 0;
			int key = 0;

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Iterate through every model in the database
			foreach (var model in db.DBModelPartList)
			{
				// Iterate through every RealCarPart in a model
				foreach (RealCarPart realpart in model.ModelCarParts)
				{
					// Skip if no attributes
					if (realpart.Attributes.Count == 0)
					{
						offset_dict[realpart.GetHashCode()] = -1;
						continue;
					}

					// Initialize new CPOffset and store all attribute offsets in it
					var offset = new Shared.Parts.CarParts.CPOffset(realpart.Attributes.Count);
					foreach (var attrib in realpart.Attributes)
					{
						var index = attrib_dict[attrib.GetHashCode()]; // get index
						offset.AttribOffsets.Add((ushort)index);       // add to CPOffset
					}

					key = offset.GetHashCode();
					if (!offset_map.ContainsKey(key)) // if CPOffset exists, skip
					{
						offset_map[key] = length; // write length to map
						bw.Write((ushort)offset.AttribOffsets.Count); // write count
						foreach (var attrib in offset.AttribOffsets)  // write all attributes
							bw.Write(attrib);
						length += 1 + offset.AttribOffsets.Count; // increase length
					}

					offset_dict[realpart.GetHashCode()] = offset_map[key]; // store to main map
				}
			}

			// Return prepared dictionary
			var dif = 0x10 - ((int)ms.Length + 0xC) % 0x10;
			if (dif != 0x10) bw.WriteBytes(dif);

			offset_buffer = ms.ToArray();
			return offset_dict;
		}
	}
}