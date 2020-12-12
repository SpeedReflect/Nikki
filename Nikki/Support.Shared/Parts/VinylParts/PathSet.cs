using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Shared.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="PathSet"/> that is used in <see cref="Class.VectorVinyl"/>.
	/// </summary>
	[DebuggerDisplay("PathDatas: {NumPathDatas} | PathPoints: {NumPathPoints}")]
	public abstract class PathSet : SubPart
	{
		private const int max = Int32.MaxValue;

		/// <summary>
		/// Number of <see cref="PathData"/> in this <see cref="PathSet"/>.
		/// </summary>
		[ReadOnly(true)]
		[Category("Main")]
		public int NumPathDatas
		{
			get => this.PathDatas.Count;
			set => this.PathDatas.Resize(value);
		}

		/// <summary>
		/// Number of <see cref="PathPoint"/> in this <see cref="PathSet"/>.
		/// </summary>
		[ReadOnly(true)]
		[Category("Main")]
		public int NumPathPoints
		{
			get => this.PathPoints.Count;
			set => this.PathPoints.Resize(value);
		}

		/// <summary>
		/// List of <see cref="PathData"/> in this <see cref="PathSet"/>.
		/// </summary>
		[Category("Main")]
		public List<PathData> PathDatas { get; }

		/// <summary>
		/// List of <see cref="PathPoint"/> in this <see cref="PathSet"/>.
		/// </summary>
		[Category("Main")]
		public List<PathPoint> PathPoints { get; }

		/// <summary>
		/// Indicates whether FillEffect exists.
		/// </summary>
		[Category("Existence")]
		public eBoolean FillEffectExists { get; set; }

		/// <summary>
		/// Indicates whether FillEffect exists.
		/// </summary>
		[Category("Existence")]
		public eBoolean StrokeEffectExists { get; set; }

		/// <summary>
		/// Indicates whether DropShadowEffect exists.
		/// </summary>
		[Category("Existence")]
		public eBoolean DropShadowEffectExists { get; set; }

		/// <summary>
		/// Indicates whether InnerGlowEffect exists.
		/// </summary>
		[Category("Existence")]
		public eBoolean InnerGlowEffectExists { get; set; }

		/// <summary>
		/// Indicates whether InnerShadowEffect exists.
		/// </summary>
		[Category("Existence")]
		public eBoolean InnerShadowEffectExists { get; set; }

		/// <summary>
		/// Indicates whether GradientEffect exists.
		/// </summary>
		[Category("Existence")]
		public eBoolean GradientEffectExists { get; set; }

		/// <summary>
		/// <see cref="VinylParts.FillEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		[Category("Effects")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public FillEffect FillEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.StrokeEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		[Category("Effects")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public StrokeEffect StrokeEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.DropShadowEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		[Category("Effects")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public DropShadowEffect DropShadowEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.InnerGlowEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		[Category("Effects")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public InnerGlowEffect InnerGlowEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.InnerShadowEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		[Category("Effects")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public InnerShadowEffect InnerShadowEffect { get; }

		/// <summary>
		/// <see cref="VinylParts.GradientEffect"/> of this <see cref="PathSet"/>.
		/// </summary>
		[Category("Effects")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public GradientEffect GradientEffect { get; }

		/// <summary>
		/// Initializes new instance of <see cref="PathSet"/>.
		/// </summary>
		protected PathSet(StrokeEffect stroke)
		{
			this.PathDatas = new List<PathData>();
			this.PathPoints = new List<PathPoint>();
			this.FillEffect = new FillEffect();
			this.StrokeEffect = stroke;
			this.DropShadowEffect = new DropShadowEffect();
			this.InnerGlowEffect = new InnerGlowEffect();
			this.InnerShadowEffect = new InnerShadowEffect();
			this.GradientEffect = new GradientEffect();
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			br.BaseStream.Position += 4;
			var size = br.ReadInt32();
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

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(BinBlockID.Vinyl_PathSet);
			bw.Write(this.PrecalculateSize());
			this.WriteSetHeader(bw);
			this.WritePathDatas(bw);
			this.WritePathPoints(bw);
			this.WriteFillEffect(bw);
			this.WriteStrokeEffect(bw);
			this.WriteDropShadowEffect(bw);
			this.WriteInnerGlowEffect(bw);
			this.WriteInnerShadowEffect(bw);
			this.WriteGradientEffect(bw);
		}

		#region Reading Methods

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

					case BinBlockID.Vinyl_DropShadow:
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

		#endregion

		#region Writing Methods

		private int PrecalculateSize()
		{
			var size = 0x30;
			size += 8 + (this.NumPathDatas << 2);
			size += 8 + (this.NumPathPoints << 2);
			if (this.FillEffectExists == eBoolean.True) size += this.FillEffect.BlockSize + 8;
			if (this.StrokeEffectExists == eBoolean.True) size += this.StrokeEffect.BlockSize + 8;
			if (this.DropShadowEffectExists == eBoolean.True) size += this.DropShadowEffect.BlockSize + 8;
			if (this.InnerGlowEffectExists == eBoolean.True) size += this.InnerGlowEffect.BlockSize + 8;
			if (this.InnerShadowEffectExists == eBoolean.True) size += this.InnerShadowEffect.BlockSize + 8;
			if (this.GradientEffectExists == eBoolean.True) size += this.GradientEffect.BlockSize + 8;
			return size;
		}

		private void WriteSetHeader(BinaryWriter bw)
		{
			bw.WriteEnum(BinBlockID.Vinyl_PathEntry);
			bw.Write(0x28);
			bw.Write(this.NumPathDatas);
			bw.Write((int)0);
			bw.Write(this.NumPathPoints);
			bw.WriteBytes(0, 0x1C);
		}

		private void WritePathDatas(BinaryWriter bw)
		{
			bw.WriteEnum(BinBlockID.Vinyl_PathData);
			bw.Write(this.NumPathDatas << 2);

			foreach (var data in this.PathDatas) data.Write(bw);
		}

		private void WritePathPoints(BinaryWriter bw)
		{
			bw.WriteEnum(BinBlockID.Vinyl_PathPoint);
			bw.Write(this.NumPathPoints << 2);

			foreach (var point in this.PathPoints) point.Write(bw);
		}

		private void WriteFillEffect(BinaryWriter bw)
		{
			if (this.FillEffectExists == eBoolean.False) return;
			bw.WriteEnum(BinBlockID.Vinyl_FillEffect);
			bw.Write(this.FillEffect.BlockSize);
			this.FillEffect.Write(bw);
		}

		private void WriteStrokeEffect(BinaryWriter bw)
		{
			if (this.StrokeEffectExists == eBoolean.False) return;
			bw.WriteEnum(BinBlockID.Vinyl_StrokeEffect);
			bw.Write(this.StrokeEffect.BlockSize);
			this.StrokeEffect.Write(bw);
		}

		private void WriteDropShadowEffect(BinaryWriter bw)
		{
			if (this.DropShadowEffectExists == eBoolean.False) return;
			bw.WriteEnum(BinBlockID.Vinyl_DropShadow);
			bw.Write(this.DropShadowEffect.BlockSize);
			this.DropShadowEffect.Write(bw);
		}

		private void WriteInnerGlowEffect(BinaryWriter bw)
		{
			if (this.InnerGlowEffectExists == eBoolean.False) return;
			bw.WriteEnum(BinBlockID.Vinyl_InnerGlow);
			bw.Write(this.InnerGlowEffect.BlockSize);
			this.InnerGlowEffect.Write(bw);
		}

		private void WriteInnerShadowEffect(BinaryWriter bw)
		{
			if (this.InnerShadowEffectExists == eBoolean.False) return;
			bw.WriteEnum(BinBlockID.Vinyl_ShadowEffect);
			bw.Write(this.InnerShadowEffect.BlockSize);
			this.InnerShadowEffect.Write(bw);
		}

		private void WriteGradientEffect(BinaryWriter bw)
		{
			if (this.GradientEffectExists == eBoolean.False) return;
			bw.WriteEnum(BinBlockID.Vinyl_Gradient);
			bw.Write(this.GradientEffect.BlockSize);
			this.GradientEffect.Write(bw);
		}

		#endregion
	}
}
