using System.IO;
using System.Linq;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Carbon.Parts.CarParts;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		/// <summary>
		/// Disassembles entire car parts block using <see cref="BinaryReader"/> provided 
		/// into <see cref="DBModelPart"/> collections and stores them in 
		/// <see cref="Database.Carbon"/> passed.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="size">Size of the car parts block.</param>
		/// <param name="db"><see cref="Database.Carbon"/> where all collections 
		/// should be stored.</param>
		public static void Disassemble(BinaryReader br, int size, Database.Carbon db)
		{
			long position = br.BaseStream.Position;
			var offsets = FindOffsets(br, size);

			// We need to read part0 as well
			br.BaseStream.Position = offsets[0] + 0x28;
			int maxattrib = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x30;
			int maxmodels = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x38;
			int maxstruct = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x40;
			int maxcparts = br.ReadInt32();

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
			var attrib_list = ReadAttribs(br, StrReader, maxattrib);

			// Read all models
			br.BaseStream.Position = offsets[5];
			var models_list = ReadModels(br, maxmodels);

			// Read all car part structs
			br.BaseStream.Position = offsets[4];
			ReadStructs(br, StrReader, maxstruct, db);

			// Read all temporary parts
			br.BaseStream.Position = offsets[6];
			var temp_cparts = ReadTempParts(br, maxcparts);

			// Generate Model Collections
			for (int a1 = 0; a1 < models_list.Length; ++a1)
			{
				var collection = new DBModelPart(models_list[a1], db);
				var tempparts = temp_cparts.FindAll(_ => _.Index == a1);

				int count = 0;
				foreach (var temppart in tempparts)
				{
					offset_dict.TryGetValue(temppart.AttribOffset, out var cpoff);
					var realpart = new RealCarPart(a1, cpoff?.AttribOffsets.Count ?? 0, collection)
					{
						PartName = $"{models_list[a1]}_PART_{count++.ToString()}"
					};
					foreach (var attroff in cpoff?.AttribOffsets ?? Enumerable.Empty<ushort>())
					{
						if (attroff >= attrib_list.Length) continue;
						realpart.Attributes.Add(attrib_list[attroff]);
					}
					collection.ModelCarParts.Add(realpart);
				}
				db.DBModelPartList.Add(collection);
			}

			int aaa = 0;
		}
	}
}
