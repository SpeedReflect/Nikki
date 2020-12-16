using System.IO;
using Nikki.Core;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Utils
{
	public static class BinarySaver
	{
		public static void GeneratePadding(this BinaryWriter bw, string mark, Alignment alignment)
		{
			if (bw.BaseStream.Position == 0) return;

			if (alignment.AlignType == Alignment.AlignmentType.Actual)
			{
				var start = bw.BaseStream.Position;
				var difference = 0x10 - start % 0x10;
				difference += alignment.Align;

				var size = difference + 0x50;

				var end = start + size;

				bw.Write((int)0); // write padding ID
				bw.Write((int)(size - 8));   // write size
				bw.WriteEnum(BinBlockID.Nikki); // write definition of a padding
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
				bw.WriteEnum(BinBlockID.Nikki); // write definition of a padding
				bw.Write((int)0); // write flags

				bw.WriteNullTermUTF8("Padding Block", 0x20); // write type of block
				bw.WriteNullTermUTF8(mark, 0x20); // write watermark passed

				while (bw.BaseStream.Position < end) bw.Write((byte)0); // write the rest

			}
		}
	
		private static void MakeBigAlign(this BinaryWriter bw, string mark, int alignment)
		{
			if (bw.BaseStream.Position == 0) return;

			var start = bw.BaseStream.Position;
			alignment += 0x100;

			var difference = alignment - ((start + 0x50) % 0x100);
			if (difference == alignment) difference = 0;

			var size = difference + 0x50;

			var end = start + size;

			bw.Write((int)0); // write padding ID
			bw.Write((int)(size - 8));   // write size
			bw.WriteEnum(BinBlockID.Nikki); // write definition of a padding
			bw.Write((int)0); // write flags

			bw.WriteNullTermUTF8("Padding Block", 0x20); // write type of block
			bw.WriteNullTermUTF8(mark, 0x20); // write watermark passed

			while (bw.BaseStream.Position < end) bw.Write((byte)0); // write the rest
		}

		public static void GenerateAlignment(this BinaryWriter bw, string mark, long position, BinBlockID id)
		{
			int dif;

			switch (id)
			{
				// Padding is just padding, return
				case BinBlockID.Padding:
					return;

				// Those IDs require Actual alignment based on their previous one
				case BinBlockID.AcidEffects:
				case BinBlockID.AcidEmitters:
				case BinBlockID.EAGLSkeleton:
				case BinBlockID.EAGLAnimations:
				case BinBlockID.ELights:
				case BinBlockID.EmitterLibrary:
				case BinBlockID.EmitterTriggers:
				case BinBlockID.EventSequence:
				case BinBlockID.FEngFont:
				case BinBlockID.Geometry:
				case BinBlockID.GCareer_Styles:
				case BinBlockID.NISDescription:
				case BinBlockID.NISScript:
				case BinBlockID.PCAWater0:
				case BinBlockID.QuickSpline:
				case BinBlockID.SpeedScenery:
				case BinBlockID.StyleMomentsInfo:
				case BinBlockID.TrackPosMarkers:
				case BinBlockID.WorldBounds:
				case BinBlockID.WCollisionPack:
				case BinBlockID.Weatherman:
				case BinBlockID.WWorld:
					dif = (int)(position % 0x100);
					MakeBigAlign(bw, mark, dif);
					return;

				case BinBlockID.Stream37220:
				case BinBlockID.Stream37240:
				case BinBlockID.Stream37250:
				case BinBlockID.Stream37260:
				case BinBlockID.Stream37270:
					if ((byte)position == (byte)bw.BaseStream.Position) return;
					dif = (int)(position % 0x100);
					MakeBigAlign(bw, mark, dif);
					return;

				default:
					// If we encountered known ID, do the padding accordingly
					if (Map.BlockToAlignment.TryGetValue(id, out var align))
					{

						GeneratePadding(bw, mark, align);

					}

					// Else generate padding based on previous position
					else
					{

						dif = (int)(position % 0x10);
						align = new Alignment(dif, Alignment.AlignmentType.Actual);
						GeneratePadding(bw, mark, align);

					}

					return;

			}



		}
	}
}
