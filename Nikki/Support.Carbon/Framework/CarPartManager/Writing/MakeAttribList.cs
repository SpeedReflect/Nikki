using System.IO;
using System.Collections.Generic;
using Nikki.Support.Carbon.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		private static Dictionary<int, int> MakeAttribList(Dictionary<int, int> string_dict,
			Database.Carbon db, out byte[] attrib_buffer)
		{
			// Initialize stack
			var attrib_list = new Dictionary<int, int>();
			attrib_buffer = null;
			int length = 0;
			int key = 0;

			// Initialize attrib stream
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Iterate through each model in the database
			foreach (var model in db.DBModelPartList)
			{
				// Iterate through each RealCarPart in a model
				foreach (RealCarPart realpart in model.ModelCarParts)
				{
					// If attribute count is 0, continue
					if (realpart.Attributes.Count == 0) continue;

					// Iterate through all attributes in RealCarPart
					foreach (var attribute in realpart.Attributes)
					{
						key = attribute.GetHashCode();
						if (!attrib_list.ContainsKey(key)) // if it already exists, skip
						{
							attrib_list[key] = length++;
							attribute.Assemble(bw, string_dict);
						}
					}
				}
			}

			// Return prepared dictionary
			bw.FillBuffer(0x10);
			attrib_buffer = ms.ToArray();
			return attrib_list;
		}
	}
}