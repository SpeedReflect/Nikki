using System.IO;
using Nikki.Reflection.ID;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		public static void Assemble(BinaryWriter bw, Database.Underground2 db)
		{
			// Get string map
			var string_dict = MakeStringList(db, out var string_buffer);

			// Get struct map
			var struct_dict = MakeStructList(string_dict, db, out var struct_buffer);

			// Get attribute map
			var attrib_dict = MakeAttribList(string_dict, db, out var attrib_buffer);

			// Get offset map
			var offset_dict = MakeOffsetList(attrib_dict, db, out var offset_buffer);

			// Get models list
			var nummodels = MakeModelsList(db, out var models_buffer);

			// Get temppart list
			var numparts = MakeCPPartList(string_dict, offset_dict, struct_dict, db, out var cppart_buffer);

			// Get header
			var header_buffer = MakeHeader(attrib_dict.Count, nummodels, struct_dict.Count, numparts);

			// Precalculate size
			int size = 0;
			size += header_buffer.Length + 8;
			size += string_buffer.Length + 8;
			size += offset_buffer.Length + 8;
			size += attrib_buffer.Length + 8;
			size += struct_buffer.Length + 8;
			size += models_buffer.Length + 8;
			size += cppart_buffer.Length + 8;

			// Write ID and Size
			bw.Write(CarParts.MAINID);
			bw.Write(size);

			// Write header
			bw.Write(CarParts.DBCARPART_HEADER);
			bw.Write(header_buffer.Length);
			bw.Write(header_buffer);

			// Write strings
			bw.Write(CarParts.DBCARPART_STRINGS);
			bw.Write(string_buffer.Length);
			bw.Write(string_buffer);

			// Write offsets
			bw.Write(CarParts.DBCARPART_OFFSETS);
			bw.Write(offset_buffer.Length);
			bw.Write(offset_buffer);

			// Write attributes
			bw.Write(CarParts.DBCARPART_ATTRIBS);
			bw.Write(attrib_buffer.Length);
			bw.Write(attrib_buffer);

			// Write structs
			bw.Write(CarParts.DBCARPART_STRUCTS);
			bw.Write(struct_buffer.Length);
			bw.Write(struct_buffer);

			// Write models
			bw.Write(CarParts.DBCARPART_MODELS);
			bw.Write(models_buffer.Length);
			bw.Write(models_buffer);

			// Write cpparts
			bw.Write(CarParts.DBCARPART_ARRAY);
			bw.Write(cppart_buffer.Length);
			bw.Write(cppart_buffer);

			int aa = 0;
		}
	}
}