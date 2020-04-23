using System.IO;
using System.Collections.Generic;



namespace Nikki.Support.Underground2.Framework
{
	public static partial class CarPartManager
	{
		public static void Assemble(BinaryWriter bw, Database.Underground2 db)
		{
			// Get string offset dictionary
			var string_dict = MakeStringList(db, out var string_buffer);

			// Get struct offset dictionary
			var struct_dict = MakeStructList(string_dict, db, out var struct_buffer);

			// Get attribute offset dictionary
			var offset_dict = MakeAttribList(string_dict, db, out var attrib_buffer);

			int aa = 0;
		}
	}
}