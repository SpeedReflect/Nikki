using System.IO;
using Nikki.Reflection.ID;
using Nikki.Support.Carbon.Class;



namespace Nikki.Support.Carbon.Framework
{
	public static partial class CarPartManager
	{
		/// <summary>
		/// Assembles entire root of <see cref="DBModelPart"/> into a byte array and 
		/// writes it with <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="db"><see cref="Database.Carbon"/> database with roots 
		/// and collections.</param>
		public static void Assemble(BinaryWriter bw, Database.Carbon db)
		{
			// Get string map
			var string_dict = MakeStringList(db, out var string_buffer);

			// Get struct map
			var numstructs = MakeStructList(string_dict, db, out var struct_buffer);

			// Get attribute map
			var attrib_dict = MakeAttribList(string_dict, db, out var attrib_buffer);

			// Get offset map
			var offset_dict = MakeOffsetList(attrib_dict, db, out var offset_buffer);

			// Get models list
			var nummodels = MakeModelsList(db, out var models_buffer);

			// Get temppart list
			var numparts = MakeCPPartList(offset_dict, db, out var cppart_buffer);

			// Get header
			var header_buffer = MakeHeader(attrib_dict.Count, nummodels, numstructs, numparts);

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
		}
	}
}