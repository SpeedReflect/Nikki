using System.IO;



namespace Nikki.Support.MostWanted.Framework
{
	public static partial class CarPartManager
	{
		private static int MakeModelsList(Database.MostWanted db, out byte[] models_buffer)
		{
			// Precalculate size; offset should be at 0xC
			var size = db.DBModelPartList.Count * 4;
			var dif = 0x10 - (size + 4) % 0x10;
			if (dif != 0x10) size += dif;
			models_buffer = new byte[size];

			// Initialize streams
			using var ms = new MemoryStream(models_buffer);
			using var bw = new BinaryWriter(ms);

			// Write all BinKeys of models
			for (int a1 = 0; a1 < db.DBModelPartList.Count; ++a1)
				bw.Write(db.DBModelPartList[a1].BinKey);

			// Return prepared list
			return db.DBModelPartList.Count;
		}
	}
}
