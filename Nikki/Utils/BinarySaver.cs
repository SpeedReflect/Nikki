using System.IO;
using Nikki.Core;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Utils
{
	internal static class BinarySaver
	{
		public static void GeneratePadding(this BinaryWriter bw, string mark, Alignment alignment)
		{
			if (bw.BaseStream.Position == 0) return;

			if (alignment.AlignType == Alignment.eAlignType.Actual)
			{
				var start = bw.BaseStream.Position;
				var difference = 0x10 - start % 0x10;
				difference += alignment.Align;

				var size = difference + 0x50;

				var end = start + size;

				bw.Write((int)0); // write padding ID
				bw.Write((int)(size - 8));   // write size
				bw.WriteEnum(eBlockID.Nikki); // write definition of a padding
				bw.Write((int)0); // write flags

				bw.WriteNullTermUTF8("Padding Block", 0x20); // write type of block
				bw.WriteNullTermUTF8(mark, 0x20); // write watermark passed

				while (bw.BaseStream.Position < end) bw.Write((byte)0); // write the rest

			}
			else
			{

				var start = bw.BaseStream.Position;
				var difference = alignment.Align - ((start + 0x50) % alignment.Align);

				if (difference == alignment.Align) difference = 0;

				var size = difference + 0x50;

				var end = start + size;

				bw.Write((int)0); // write padding ID
				bw.Write((int)(size - 8));   // write size
				bw.WriteEnum(eBlockID.Nikki); // write definition of a padding
				bw.Write((int)0); // write flags

				bw.WriteNullTermUTF8("Padding Block", 0x20); // write type of block
				bw.WriteNullTermUTF8(mark, 0x20); // write watermark passed

				while (bw.BaseStream.Position < end) bw.Write((byte)0); // write the rest

			}
		}
	
		public static void GenerateGeometryPadding(BinaryWriter bw, string mark, long position)
		{
			var dif = (int)(position % 0x10);
			var align = new Alignment(dif, Alignment.eAlignType.Actual);
			GeneratePadding(bw, mark, align);
		}

		public static void GenerateAlignment(BinaryWriter bw, string mark, long position)
		{
			/*
			Requires Actual alignment:
				0x00034027 WorldBounds
				0x00037220 Stream37220
				0x00037240 Stream37240
				0x00037250 Stream37250
				0x00037260 Stream37260
				0x00037270 Stream37270
				0x0003B800 WWorld
				0x0003B801 WCollisionPack
				0x0003BC00 EmitterLibrary
				0x80034100 SpeedScenery
				0x80036000 EmitterTriggers
				0x8003B810 EventSequence
				0x80134000 Geometry
				0x80135000 ELights
			*/



		}
	}
}
