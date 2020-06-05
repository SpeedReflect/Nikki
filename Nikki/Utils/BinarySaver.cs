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
				var difference = alignment.Align - ((start + 0x70) % alignment.Align);

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

	}
}
