using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Parts.VinylParts
{
	/// <summary>
	/// 
	/// </summary>
	public class PathSet : SubPart
	{
		private const int max = Int32.MaxValue;

		/// <summary>
		/// Number of <see cref="PathData"/> in this <see cref="PathSet"/>.
		/// </summary>
		public int NumPathDatas
		{
			get => this.PathDatas.Count;
			set => this.PathDatas.Resize(value);
		}

		/// <summary>
		/// Number of <see cref="PathPoint"/> in this <see cref="PathSet"/>.
		/// </summary>
		public int NumPathPoints
		{
			get => this.PathPoints.Count;
			set => this.PathPoints.Resize(value);
		}

		/// <summary>
		/// List of <see cref="PathData"/> in this <see cref="PathSet"/>.
		/// </summary>
		public List<PathData> PathDatas { get; }

		/// <summary>
		/// List of <see cref="PathPoint"/> in this <see cref="PathSet"/>.
		/// </summary>
		public List<PathPoint> PathPoints { get; }

		/// <summary>
		/// Indicates whether FillEffect exists.
		/// </summary>
		public eBoolean FillEffectExists { get; set; }

		/// <summary>
		/// Indicates whether FillEffect exists.
		/// </summary>
		public eBoolean StrokeEffectExists { get; set; }

		/// <summary>
		/// Indicates whether DropShadowEffect exists.
		/// </summary>
		public eBoolean DropShadowEffectExists { get; set; }

		/// <summary>
		/// Indicates whether InnerGlowEffect exists.
		/// </summary>
		public eBoolean InnerGlowEffectExists { get; set; }

		/// <summary>
		/// Indicates whether InnerShadowEffect exists.
		/// </summary>
		public eBoolean InnerShadowEffectExists { get; set; }

		/// <summary>
		/// Indicates whether GradientEffect exists.
		/// </summary>
		public eBoolean GradientEffectExists { get; set; }

		/// <summary>
		/// <see cref="VinylParts.FillEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		public FillEffect FillEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.StrokeEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		public StrokeEffect StrokeEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.DropShadowEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		public DropShadowEffect DropShadowEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.InnerGlowEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		public InnerGlowEffect InnerGlowEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.InnerShadowEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		public InnerShadowEffect InnerShadowEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.GradientEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		public GradientEffect GradientEffect { get; }

		/// <summary>
		/// Initializes new instance of <see cref="PathSet"/>.
		/// </summary>
		public PathSet()
		{
			this.PathDatas = new List<PathData>();
			this.PathPoints = new List<PathPoint>();
			this.FillEffect = new FillEffect();
			this.StrokeEffect = new StrokeEffect();
			this.DropShadowEffect = new DropShadowEffect();
			this.InnerGlowEffect = new InnerGlowEffect();
			this.InnerShadowEffect = new InnerShadowEffect();
			this.GradientEffect = new GradientEffect();
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PathSet();
			result.CloneValues(this);
			return result;
		}

		/// <summary>
		/// Clones values of another <see cref="SubPart"/>.
		/// </summary>
		/// <param name="other"><see cref="SubPart"/> to clone.</param>
		public override void CloneValues(SubPart other)
		{
			if (other is PathSet set)
			{

				foreach (var data in set.PathDatas) this.PathDatas.Add((PathData)data.PlainCopy());
				foreach (var point in set.PathPoints) this.PathPoints.Add((PathPoint)point.PlainCopy());
				this.DropShadowEffectExists = set.DropShadowEffectExists;
				this.FillEffectExists = set.FillEffectExists;
				this.GradientEffectExists = set.GradientEffectExists;
				this.InnerGlowEffectExists = set.InnerGlowEffectExists;
				this.InnerShadowEffectExists = set.InnerShadowEffectExists;
				this.StrokeEffectExists = set.StrokeEffectExists;
				this.DropShadowEffect.CloneValues(set.DropShadowEffect);
				this.FillEffect.CloneValues(set.FillEffect);
				this.GradientEffect.CloneValues(set.GradientEffect);
				this.InnerGlowEffect.CloneValues(set.InnerGlowEffect);
				this.InnerShadowEffect.CloneValues(set.InnerShadowEffect);
				this.StrokeEffect.CloneValues(set.StrokeEffect);

			}
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="size">Size of the <see cref="PathSet"/>.</param>
		public void Read(BinaryReader br, int size)
		{
			var off = br.BaseStream.Position;
			var end = off + size;

			var offsets = this.FindOffsets(br, size);

			br.BaseStream.Position = offsets[0];
			this.ReadSetHeader(br);

			br.BaseStream.Position = offsets[1];
			this.ReadPathDatas(br);

			br.BaseStream.Position = offsets[2];
			this.ReadPathPoints(br);

			br.BaseStream.Position = offsets[3];
			this.ReadFillEffect(br);

			br.BaseStream.Position = offsets[4];
			this.ReadStrokeEffect(br);

			br.BaseStream.Position = offsets[5];
			this.ReadDropShadowEffect(br);

			br.BaseStream.Position = offsets[6];
			this.ReadInnerGlowEffect(br);

			br.BaseStream.Position = offsets[7];
			this.ReadInnerShadowEffect(br);

			br.BaseStream.Position = offsets[8];
			this.ReadGradientEffect(br);

			br.BaseStream.Position = end;
		}

		private long[] FindOffsets(BinaryReader br, int size)
		{
			var result = new long[9];
			var end = br.BaseStream.Position + size;

			for (int i = 0; i < result.Length; ++i) result[i] = max;

			while (br.BaseStream.Position < end)
			{

				var id = br.ReadEnum<BinBlockID>();
				var len = br.ReadInt32();
				var cur = br.BaseStream.Position;

				switch (id)
				{
					case BinBlockID.Vinyl_PathEntry:
						result[0] = cur;
						goto default;

					case BinBlockID.Vinyl_PathData:
						result[1] = cur;
						goto default;

					case BinBlockID.Vinyl_PathPoint:
						result[2] = cur;
						goto default;

					case BinBlockID.Vinyl_FillEffect:
						result[3] = cur;
						goto default;

					case BinBlockID.Vinyl_StrokeEffect:
						result[4] = cur;
						goto default;

					case BinBlockID.Vinyl_VectorLine:
						result[5] = cur;
						goto default;

					case BinBlockID.Vinyl_InnerGlow:
						result[6] = cur;
						goto default;

					case BinBlockID.Vinyl_ShadowEffect:
						result[7] = cur;
						goto default;

					case BinBlockID.Vinyl_Gradient:
						result[8] = cur;
						goto default;

					default:
						br.BaseStream.Position = cur + len;
						break;

				}

			}

			return result;
		}

		private void ReadSetHeader(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			this.NumPathDatas = br.ReadInt32();
			br.BaseStream.Position += 4;
			this.NumPathPoints = br.ReadInt32();
		}
	
		private void ReadPathDatas(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;

			for (int i = 0; i < this.NumPathDatas; ++i)
			{

				this.PathDatas[i].Read(br);

			}
		}

		private void ReadPathPoints(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;

			for (int i = 0; i < this.NumPathPoints; ++i)
			{

				this.PathPoints[i].Read(br);

			}
		}
	
		private void ReadFillEffect(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			this.FillEffect.Read(br);
			this.FillEffectExists = eBoolean.True;
		}
	
		private void ReadStrokeEffect(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			this.StrokeEffect.Read(br);
			this.StrokeEffectExists = eBoolean.True;
		}

		private void ReadDropShadowEffect(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			this.DropShadowEffect.Read(br);
			this.DropShadowEffectExists = eBoolean.True;
		}

		private void ReadInnerGlowEffect(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			this.InnerGlowEffect.Read(br);
			this.InnerGlowEffectExists = eBoolean.True;
		}

		private void ReadInnerShadowEffect(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			this.InnerShadowEffect.Read(br);
			this.InnerShadowEffectExists = eBoolean.True;
		}

		private void ReadGradientEffect(BinaryReader br)
		{
			if (br.BaseStream.Position == max) return;
			this.GradientEffect.Read(br);
			this.GradientEffectExists = eBoolean.True;
		}
	}
}
