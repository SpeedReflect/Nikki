using System.IO;
using System.Collections.Generic;



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



			int aa = 0;
		}
	}
}